gameservertest:maintest2.o GameServer.o MyClient.o MyServer.o MySocket.o
	g++ -std=c++11 -o gameservertest maintest2.o GameServer.o MySocket.o MyClient.o MyServer.o -lpthread
MySocket.o:MySocket.cpp
	g++ -std=c++11 -c MySocket.cpp
MyClient.o:MyClient.cpp MySocket.h
	g++ -std=c++11 -c MyClient.cpp
MyServer.o:MyServer.cpp MyClient.h
	g++ -std=c++11 -c MyServer.cpp 
GameServer.o:GameServer.cpp MyServer.h
	g++ -std=c++11 -c GameServer.cpp
maintest2.o:maintest2.cpp GameServer.h
	g++ -std=c++11 -c maintest2.cpp


clean:
	rm MyClient.o MyServer.o MySocket.o maintest2.o GameServer.o gameservertest 
