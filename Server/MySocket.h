#pragma once

#include<iostream>
#include<string>
#include<sys/types.h>
#include<sys/socket.h>
#include<stdlib.h>
#include<netinet/in.h>
#include<errno.h>
#include<arpa/inet.h>
#include<time.h>
#include<sys/time.h>
#include<unistd.h>
#include<chrono>


typedef unsigned int SOCKET; 
typedef sockaddr_in SOCKADDR_IN;
typedef sockaddr SOCKADDR;

std::string GetTime();



class MySocket
{
public:
	MySocket();
	~MySocket();
	bool Bind(std::string ip, int port);//绑定ip和端口
	bool Connect(std::string ip, int port);//连接服务器

	void Accept( SOCKET* client, SOCKADDR_IN* addr);//仅服务器使用，接受一个连接,输出socket和地址
	
	bool Send(char* msg, int lenth);//发送长度为lenth的消息给socket
	bool Recive( char* msg,  int* lenth);//从服务器接受消息，长度为lenth, buff上限4096字节

	bool MassiveSend(char* msg, int lenth);//批量发送长度为lenth的消息（lenth较长时使用）
	bool MassiveRecive( char* msg, int lenth);//批量接收长度为lenth的消息（lenth较长时使用）

	bool GetAddr(SOCKADDR_IN* addr);

	void Close();//关闭socket

	bool IsOpen();

	void Initial(SOCKET* sock);

	static bool WIN_INIT();//winapi初始化
	static bool WIN_CLEAN();//winapi释放资源

private:
	SOCKET mySocket;
};
