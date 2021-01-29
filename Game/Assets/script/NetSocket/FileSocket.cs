using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using System.Threading;
using System.Text;
public class FileSocket
{
    public  Socket socket;

    public string serverIP = "139.224.131.58";

    public int serverPort = 23456;

    public bool reciveFlag;
    Thread reciveThread;
    public string[] recivedMsgs = new string[200];
    public int reciveCount;
    public int MAXCOUNT = 200;

    public bool Connect(string ip, int port)//连接服务器
    {
        try
        {
            Debug.Log("start connect server");
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, port);
            reciveThread = new Thread(Recive);
            reciveThread.IsBackground = true;
            reciveFlag = true;
            reciveThread.Start(socket);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CloseSocket()//关闭套接字
    {
        if (socket != null)
        {
            reciveFlag = false;
            socket.Close();
            return true;
        }
        return false;
    }

    public void Recive(object obj)//接收服务器发来的信息
    {
        Socket sock = obj as Socket;
        byte[] buffer = new byte[8192];
        int lenth;
        while (reciveFlag && (lenth = sock.Receive(buffer)) > 0)
        {
                string content = Encoding.ASCII.GetString(buffer, 0, lenth);
                if (reciveCount < MAXCOUNT)
                {
                    recivedMsgs[reciveCount++] = content;
                }
        }
        reciveFlag = false;
    }

    public bool Send(string msg)//给服务器发送信息
    {
        msg += '\0';
        byte[] buffer;
        buffer = Encoding.ASCII.GetBytes(msg);
        if (socket != null)
        {
            socket.Send(buffer);
            Debug.Log("Send:" + msg);
            return true;
        }
        return false;
    }

    public string GetMessage() {
        if (reciveCount > 0)
        {
            string tmp = recivedMsgs[0];
            for(int i = 1; i < reciveCount; i++)
            {
                recivedMsgs[i - 1] = recivedMsgs[i];
            }
            recivedMsgs[reciveCount - 1] = null;
            reciveCount--;
            return tmp;
        }
        return null;
    }

    public bool TryGetMsg(out string msg)
    {
        msg = GetMessage();
        if (msg == null) return false;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
