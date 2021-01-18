#include"MySocket.h"


std::string GetTime()
{
	char utime[7];
	int usec = std::chrono::duration_cast<std::chrono::microseconds>(std::chrono::system_clock::now().time_since_epoch()).count()%1000000;
	utime[0] ='0' +  usec/100000;
	usec = usec%100000;
	utime[1] = '0' + usec/10000;
	usec = usec%10000;
	utime[2] = '0'+usec/1000;
	usec = usec%1000;
	utime[3] = '0' + usec/100;
	usec = usec%100;
	utime[4] = '0' + usec/10;
	usec = usec%10;
	utime[5] = '0' + usec;
	utime[6] = '\0';
	return std::string(utime);
}


MySocket::MySocket()
{
	mySocket =NULL;
}

MySocket::~MySocket()
{
	if (mySocket!= NULL)
	{
		Close();
	}
}

bool MySocket::Bind(std::string ip, int port)
{
	if (mySocket != NULL)
	{
		Close();
	}
	mySocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	sockaddr_in addr;
	addr.sin_family = AF_INET;
	inet_pton(AF_INET,ip.c_str(), &addr.sin_addr);
	addr.sin_port = htons(port);
	if (bind(mySocket, (sockaddr*)&addr, sizeof(sockaddr))>-1 &&
		listen(mySocket, 10000)>-1)
		return true;
	Close();
	return false;
}

bool MySocket::Connect(std::string ip, int port)
{
	if (mySocket != NULL)
	{
		Close();
	}
	mySocket = socket(AF_INET, SOCK_STREAM,IPPROTO_TCP);
	sockaddr_in addr;
	addr.sin_family = AF_INET;
	inet_pton(AF_INET, ip.c_str(), &addr.sin_addr);
	addr.sin_port = htons(port);

	if (connect(mySocket, (sockaddr*)&addr, sizeof(sockaddr)) > -1)return true;
	Close();
	return false;
}

void MySocket::Accept(SOCKET* client, SOCKADDR_IN*  addr)
{
	unsigned int len = sizeof(sockaddr);
	*client = accept(mySocket, (sockaddr*)addr, &len);
}

bool MySocket::Send(char* msg, int lenth)
{
	if (send(mySocket, msg, lenth, 0) > -1)return true;
	return false;
}

bool MySocket::Recive( char* msg,  int * lenth)
{
	*lenth =  recv(mySocket, msg, 2048, 0);
	if (*lenth>0)
	{
		return true;
	}
	return false;
}

bool MySocket::MassiveSend(char* msg, int lenth)
{
	int sendcount = 0, sendSize=0;
	while (lenth > 0) {
		sendSize = send(mySocket, msg + sendcount, lenth, 0);
		if (sendSize < 0)return false;
		sendcount += sendSize;
		lenth -= sendSize;
	}
	return true;
}

bool MySocket::MassiveRecive( char* msg,  int lenth)
{
	int recvcount = 0, recvsize = 0;
	while (lenth>0)
	{
		recvsize = recv(mySocket, msg + recvcount, lenth, 0);
		if (recvsize < 0)return false;
		recvcount += recvsize;
		lenth -= recvsize;
	}
	return false;
}

bool MySocket::GetAddr(SOCKADDR_IN* addr)
{
	unsigned int len = sizeof(sockaddr_in);
	if (IsOpen() && getpeername(mySocket, (sockaddr*)addr, &len))return true;
	return false;
}

void MySocket::Close()
{
	close(mySocket);
	mySocket = NULL;
}

bool MySocket::IsOpen()
{
	return mySocket != NULL;
}

void MySocket::Initial(SOCKET* sock)
{
	if (mySocket != NULL)Close();
	mySocket = *sock;
}

bool MySocket::WIN_INIT()
{
	return true;
}

bool MySocket::WIN_CLEAN()
{
	return  true;
}
