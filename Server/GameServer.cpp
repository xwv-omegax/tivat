#include "GameServer.h"

GameServer::GameServer()
{
	ip = "0.0.0.0";
	port = 12345;
	server.Bind(ip, port);
	server.StartAccept();
	MaxMathchListCount = 100;
	MathchListCount = 0;
	MatchList = new MyClient * [MaxMathchListCount];
}

GameServer::GameServer(std::string ip, int port)
{
	this->ip = ip;
	this->port = port;
	server.Bind(ip, port);
	server.StartAccept();
	MaxMathchListCount = 100;
	MathchListCount = 0;
	MatchList = new MyClient * [MaxMathchListCount];
}

GameServer::~GameServer()
{
	SendToAll("S");
	delete[] MatchList;
	MySocket tmp;
	tmp.WIN_CLEAN();
}

void GameServer::SendToAll(std::string msg)
{
	int lenth = msg.length();
	for (int i = 0; i < server.clientcount; i++) {
		server.clients[i]->Send(msg.c_str(), lenth+1);
	}
}

void GameServer::MessageProcess(MyClient* sock, char* msg, int lenth)
{
	std::string ip;
	int port;
	sock->GetAddr(&ip, &port);
	
	std::cout << ip << "(" << port << "): ";
	std::cout << msg << std::endl;

	switch (msg[0])
	{
	case 'A':
		MessageTypeA(sock, msg, lenth);
		break;
	case 'B':
		MessageTypeB(sock, msg, lenth);
		break;
	case 'S':
		MessageTypeS(sock, msg, lenth);
		break;
	default:
		break;
	}
}

void GameServer::MessageTypeA(MyClient* sock, char* msg, int lenth)
{
	switch (msg[1])
	{
	case 'A':
		if (MatchListFind(sock) < 0) {
			sock->Send("AA", 3);
			MatchListAdd(sock);
		}
		break;
	case 'B':
		if (MatchListFind(sock) > -1) {
			sock->Send("AB", 3);
			MatchListDelete(sock);
		}
		break;
	case 'C':
		break;
	case 'D':
		if (playingPair.find(sock) != playingPair.end())
		{
			std::cout << "Add to pair" << std::endl;
			playingPair[sock]->Send("AD", 3);
			PlayingPairDelete(sock);
		}
		break;
	default:
		break;
	}
}

void GameServer::MessageTypeB(MyClient* sock, char* msg, int lenth)
{
	std::cout << "Send to other" << std::endl;
	if (playingPair.find(sock) != playingPair.end())
	{
		playingPair[sock]->Send(msg, lenth);
	}
}

void GameServer::MessageTypeS(MyClient* sock, char* msg, int lenth)
{

	PlayingPairDelete(sock);

	MatchListDelete(sock);

	server.ClientsDelete(sock);
	std::cout << "close socket" << std::endl;
}

void GameServer::MatchListDelete(MyClient* sock)
{
	std::cout << "delete from match list" << std::endl;
	for (int i = 0; i < MathchListCount; i++) {
		if (sock == MatchList[i]) {
			for (int j = i; j < MathchListCount -1; j++) {
				MatchList[j] = MatchList[j + 1];
			}
			MatchList[MathchListCount - 1] = nullptr;
			MathchListCount--;
			break;
		}
	}
}

void GameServer::MatchListAdd(MyClient* sock)
{
	std::cout << "add to match list"<<std::endl;
	if (MathchListCount >= MaxMathchListCount - 1)MatchListExtend();
	MatchList[MathchListCount++] = sock;
}

int GameServer::MatchListFind(MyClient* sock)
{
	int index = MathchListCount-1;
	while (index > -1 && MatchList[index] != sock)index--;
	return index;
}

void GameServer::MatchListExtend()
{
	MyClient** old = MatchList;
	MaxMathchListCount += 100;
	MatchList = new MyClient * [MaxMathchListCount];
	for (int i = 0; i < MathchListCount; i++)MatchList[i] = old[i];
	delete[] old;
}



void GameServer::ClientProcess()
{

	ClearDeadClient(); 

	for (int i = 0; i < server.clientcount; i++) {
		SocketMessage msg;
		if (server.clients[i]->GetFirstMessage(msg)) {
			MessageProcess(server.clients[i], msg.GetContent(), msg.GetLenth());
		}
	}
	while (MathchListCount>=2)
	{
		MatchList[0]->Send("ACA", 4);
		MatchList[1]->Send("ACB", 4);
		playingPair[MatchList[0]] = MatchList[1];
		playingPair[MatchList[1]] = MatchList[0];

		std::string seed = GetTime();
		char msg[9];
		msg[0] = 'A';
		msg[1] = 'E';
		for (int i = 0; i < 6; i++) {
			msg[i + 2] = seed[i];
		}
		msg[8] = '\0';
		MatchList[0]->Send(msg, 9);
		MatchList[1]->Send(msg, 9);

		std::string ip1, ip2;
		int port1, port2;
		MatchList[0]->GetAddr(&ip1, &port1);
		MatchList[1]->GetAddr(&ip2, &port2);
		std::cout << ip1 << "(" << port1 << ") VS " << ip2 << "(" << port2 << ")"<<std::endl;

		std::cout << "RandomSeed:" << seed << std::endl;

		MatchListDelete(MatchList[0]);
		MatchListDelete(MatchList[0]);

	}
}

void GameServer::PlayingPairDelete(MyClient* sock)
{
	std::cout << "delete from pair" << std::endl;
	if (playingPair.find(sock) != playingPair.end()) {
		MyClient* other = playingPair[sock];
		other->Send("AD", 3);
		playingPair.erase(sock);
		playingPair.erase(other);
	}
}

void GameServer::ClearDeadClient()
{
	for (int i = 0; i < server.clientcount; i++) {
		if (!server.clients[i]->IsAlive()) {
			MessageTypeS(server.clients[i], nullptr, 0);
		}
	}
}
