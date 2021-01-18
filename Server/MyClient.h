#pragma once
#include"MySocket.h"
#include<thread>
#include <utility>
#include <chrono>
#include <functional>
#include <atomic>
#include <cstring>

class SocketMessage {
public:
	SocketMessage();
	~SocketMessage();

	void Initial(char* msgs, int lenth);

	int GetLenth();

	char* GetContent();

private:
	char* msg;
	int lenth;
};

class MyClient {
public:
	MyClient();
	~MyClient();

	bool Connect(std::string ip, int port);
	bool Send(char* msgs, int lenth);
	bool Send(const char* msgs, int lenth);
	bool Recive(char* msgs, int* lenth);
	bool StartRecive();//开始接受数据
	bool StopRecive();//停止接收数据

	void Initial(SOCKET* sock);

	bool GetAddr(SOCKADDR_IN* addr);
	bool GetAddr(std::string* ip, int* port);

	bool IsAlive();

	bool GetFirstMessage(SocketMessage& msg);

	SocketMessage* recvMsgs;
	int recvCount;

private:
	void recivecycle();
	void reciveMsg(char* msg, int lenth);
	MySocket mySocket;
	std::thread* recvThread;
	int MAXCOUNT=256;
	bool recivecycleflag;
};