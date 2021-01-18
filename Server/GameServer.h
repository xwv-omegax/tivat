#pragma once
#include "MyServer.h"
#include <map>
class GameServer {
public:
	GameServer();
	GameServer(std::string ip, int port);
	~GameServer();

	void SendToAll(std::string msg);

	void MessageProcess(MyClient* sock,char* msg, int lenth);

	void MessageTypeA(MyClient* sock, char* msg, int lenth);

	void MessageTypeB(MyClient* sock, char* msg, int lenth);

	void MessageTypeS(MyClient* sock, char* msg, int lenth);

	void MatchListDelete(MyClient* sock);

	void MatchListAdd(MyClient* sock);

	int MatchListFind(MyClient* sock);

	void MatchListExtend();

	void ClientProcess();

	void PlayingPairDelete(MyClient* sock);

	void ClearDeadClient();

private:
	MyServer server;
	MyClient** MatchList;
	int MathchListCount;
	int MaxMathchListCount;
	std::map<MyClient*, MyClient*> playingPair;
	std::string ip;
	int port;
};
