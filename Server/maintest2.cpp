#include"GameServer.h"

int main() {
	GameServer server("0.0.0.0",12345);
	std::cout << "Server Start" << std::endl;
	std::cout << "MicroTime:"<<GetTime() << std::endl;
	while (1) {
		server.ClientProcess();
	}
	
	return 0;
}