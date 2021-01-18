#include"MyClient.h"

SocketMessage::SocketMessage()
{
	msg = nullptr;
}

SocketMessage::~SocketMessage()
{
	if (msg != nullptr) {
		delete[] msg;
		msg = nullptr;
	}
}


void SocketMessage::Initial(char* msgs, int lenth)
{
	if(msg != nullptr)delete[] msg;
	msg = nullptr;
	msg = new char[lenth];
	this->lenth = lenth;
	for (int i = 0; i < lenth; i++)msg[i] = msgs[i];
}

int SocketMessage::GetLenth()
{
	return lenth;
}

char* SocketMessage::GetContent()
{
	return msg;
}

MyClient::MyClient()
{
	mySocket.WIN_INIT();
	recvMsgs = new SocketMessage[100];
	recvCount = 0;
	recvThread = nullptr;
	recivecycleflag = false;
}


MyClient::~MyClient()
{
	delete[] recvMsgs;
	if (mySocket.IsOpen()) {
		char stop[] = "ClientStop";
		mySocket.Send(stop, strlen(stop) + 1);
	}
}

bool MyClient::Connect(std::string ip, int port)
{
	return mySocket.Connect(ip,port);
}

bool MyClient::Send(char* msgs, int lenth)
{
	if (lenth < 4096)
	{
		return mySocket.Send(msgs, lenth);
	}
	else
	{
		return mySocket.MassiveSend(msgs, lenth);
	}
}

bool MyClient::Send(const char* msgs, int lenth) {
	char* buffer = new char[lenth];
	for (int i = 0; i < lenth; i++)buffer[i] = msgs[i];
	bool b = Send(buffer, lenth);
	delete[] buffer;
	return b;
}

bool MyClient::Recive(char* msgs, int* lenth)
{
	return mySocket.Recive(msgs, lenth);
}

bool MyClient::StartRecive()
{
	if (!mySocket.IsOpen() || recivecycleflag)return false;
	recivecycleflag = true;
	recvThread = new std::thread(&MyClient::recivecycle,this);
	recvThread->detach();
	return true;
}

bool MyClient::StopRecive()
{
	if (recivecycleflag)
	{
		recivecycleflag = false;
		return true;
	}
	return false;
}

void MyClient::Initial(SOCKET* sock)
{
	mySocket.Initial(sock);
}

bool MyClient::GetAddr(SOCKADDR_IN* addr)
{
	return mySocket.GetAddr(addr);
}

bool MyClient::GetAddr(std::string* ip, int* port)
{
	char buffer[20];
	SOCKADDR_IN* addr = new SOCKADDR_IN();
	if (!mySocket.GetAddr(addr)) {
		inet_ntop(AF_INET,&addr->sin_addr,buffer,16);
		*port = ntohs(addr->sin_port);
		delete addr;
		*ip = buffer;
		return true;
	}
	delete addr;
	return false;
}

bool MyClient::IsAlive()
{
	return recivecycleflag;
}

bool MyClient::GetFirstMessage(SocketMessage& msg)
{
	if (recvCount < 1)return false;
	msg =  recvMsgs[0];
	for (int i = 1; i < recvCount; i++)recvMsgs[i - 1] = recvMsgs[i];
	recvMsgs[--recvCount] = SocketMessage();
	return true;
}

void MyClient::recivecycle()
{
	char buff[4096];
	int lenth=0;
	int WrongCount = 0;
	while (recivecycleflag && Recive(buff, &lenth)) {
			reciveMsg(buff, lenth);
	}
	StopRecive();
}

void MyClient::reciveMsg(char* msg, int lenth)
{
	if (recvCount < 256 && lenth > 0) {
		recvMsgs[recvCount++].Initial(msg, lenth);
	}
}
