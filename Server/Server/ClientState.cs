using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class ClientState
    {
        int iD;
        string name;
        int time;

        public TcpClient tcpClient;

        public ClientState(int iD, TcpClient tcpClient)
        {
            this.iD = iD;
            this.name = new string("");
            time = 3;
            this.tcpClient = tcpClient;
        }

        #region Get Data
        public int ID { get { return iD; } } 
        public string Name { get { return name; } set { name = value; } }
        public int Time { get { return time; } set { time = value; } }
        public TcpClient Socket { get { return tcpClient; } }
        #endregion

    }
}
