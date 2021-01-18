#pragma once
#include"MyClient.h"

class  MyServer
{
public:
	 MyServer();
	~ MyServer();

	bool Bind(std::string ip, int port);
	bool Accept(SOCKET* sock, SOCKADDR_IN* addr);

	bool Recive(char* buffer, int* lenth);

	bool StartAccept();
	bool StopAccept();

	void Initial(SOCKET* sock);

	void ClientsDelete(MyClient* client);

	MyClient** clients;
	int clientcount;
	int MAXCOUNT;

private:
	void ExtendClients();
	void acceptcycle();
	
	void AcceptClient(SOCKET* sock);

	MySocket server;

	bool acceptcycleflag;
};