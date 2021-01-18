#include"MyServer.h"

MyServer::MyServer()
{
	server.WIN_INIT();
	MAXCOUNT = 256;
	clients = new MyClient*[256];
	for (int i = 0; i < MAXCOUNT; i++)clients[i] = nullptr;
	clientcount = 0;
	acceptcycleflag = false;
}

MyServer::~MyServer()
{
	for (int i = 0; i < MAXCOUNT; i++) {
		delete clients[i];
	}
	delete[] clients;
}

bool MyServer::Bind(std::string ip, int port)
{
	return server.Bind(ip, port);
}

bool MyServer::Accept(SOCKET* sock, SOCKADDR_IN* addr)
{
	if (!server.IsOpen())
	{
		return false;
	}
	server.Accept(sock,addr);
	return true;
}

bool MyServer::Recive(char* buffer, int* lenth)
{
	return server.Recive(buffer, lenth);
}

bool MyServer::StartAccept()
{
	if (!server.IsOpen() || acceptcycleflag)return false;
	acceptcycleflag = true;
	std::thread t(&MyServer::acceptcycle, this);
	t.detach();
	return true;
}

bool MyServer::StopAccept()
{
	if (!acceptcycleflag)return false;
	acceptcycleflag = false;
	return true;
}

void MyServer::Initial(SOCKET* sock)
{
	server.Initial(sock);
}

void MyServer::ClientsDelete(MyClient* client)
{
	for (int i = 0; i < clientcount; i++) {
		if (client == clients[i]) {
			for (int j = i; j < clientcount - 1; j++) {
				clients[j] = clients[j + 1];
			}
			clients[clientcount - 1] = nullptr;
			clientcount--;
		}
	}
}

void MyServer::ExtendClients()
{
	MAXCOUNT += 256;
	MyClient** old = clients;
	clients = new MyClient*[MAXCOUNT];
	for (int i = 0; i < clientcount; i++)clients[i] = old[i];
	for (int i = clientcount; i++; i < MAXCOUNT)clients[i] = nullptr;
	delete[] old;
}

void MyServer::acceptcycle()
{
	SOCKET* sock = new SOCKET;
	*sock = socket(AF_INET, SOCK_STREAM, 0);
	SOCKADDR_IN* addr = new SOCKADDR_IN;
	while (acceptcycleflag && Accept(sock,addr))
	{
		this->AcceptClient(sock);
	}
	this->StopAccept();
	delete addr;
}

void MyServer::AcceptClient(SOCKET* sock)
{
	if (clientcount > MAXCOUNT - 2)ExtendClients();
	clients[clientcount++] = new MyClient;
	clients[clientcount-1]->Initial(sock);
	clients[clientcount - 1]->StartRecive();
}
