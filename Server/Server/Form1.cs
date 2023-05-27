using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Numerics;

namespace Server
{
    public partial class Server : Form
    {
        TcpListener tcpListener;
        string Address;
        string Port;
        int clientID;
        int quesID;
        string Ans;

        List<ClientState> players;

        public Server()
        {
            InitializeComponent();
            Address = GetLocalIpAddress();
            Port = "8080";
            tb_IP.Text = Address + ":" + Port;
            tcpListener = new TcpListener(IPAddress.Parse(Address), int.Parse(Port));

            clientID = 0;
            quesID = 0;
            players = new List<ClientState>();

        }

        public string GetLocalIpAddress()
        {
            string ipAddress = string.Empty;
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var address in hostEntry.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address.ToString();
                }
            }
            return ipAddress;
        }

        #region
        public void AcceptConnections()
        {
            TcpClient new_client;
            while (true)
            {
                new_client = tcpListener.AcceptTcpClient();
                try
                {
                    if (new_client.Connected)
                    {
                        players.Add(new ClientState(clientID, new_client));
                        SendToClient(clientID, "ID" + clientID.ToString(), new_client);
                        ADD_TO_LIST("Client " + players[clientID].Name + " :" + new_client.Client.RemoteEndPoint + " is joined");
                        clientID++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Listening(object obj)
        {
            TcpClient this_client = (TcpClient)obj;
            NetworkStream networkStream = this_client.GetStream();
 //           int this_client_ID = current_client_id_count;
            while (true)
            {
                if (networkStream.CanRead)
                {
                    byte[] buffer = new byte[2048];
                    int BytesReaded = networkStream.Read(buffer, 0, buffer.Length);
                    if (BytesReaded > 0)
                    {
                        string Message = Encoding.UTF8.GetString(buffer, 0, BytesReaded);
                        string Command = Message.Substring(0, 2);
                        string iD = Message.Substring(2, 1);
                        ADD_TO_LIST("Receive- '" + Message + "' from Client " + iD);
                        switch (Command)
                        {
                            case "RD":
                                {
                                    for (int i = 0; i < players.Count; i++)
                                    {
                                        if (i == 0)
                                            SendToClient(i, "YTA", players[0].Socket);    // Your Turn to Answer
                                        else
                                            SendToClient(i, "NA0", players[i].Socket);    // Next Answer from Next(id)
                                    }
                                }
                                break;
                            case "QU":
                                {
                                    string ID = Message.Substring(2);
                                    int id = int.Parse(ID);
                                    Ans = Message.Substring(3);
                                    for (int i = 0; i < players.Count; i++)
                                    {
                                        SendToClient(i, "FQ"+ID, players[i].Socket);    // ID Finished Questioning
                                        if (i == Next(id))
                                            SendToClient(i, "YTA", players[Next(id)].Socket);    // Your Turn to Answer
                                        else
                                            SendToClient(i, "NA" + Next(id).ToString(), players[Next(id)].Socket);    // Next Answer from Next(id)
                                    }
                                }
                                break;
                            case "AN":
                                {
                                    string ID = Message.Substring(2);
                                    int id = int.Parse(ID);
                                    string temp = Message.Substring(3);
                                    if(temp == Ans)
                                    {
                                        Next();
                                        for(int i = 0; i < players.Count; i++)
                                        {
                                            SendToClient(i, "RE" + ID, players[i].Socket);  // Round End
                                            if(i == quesID)
                                                SendToClient(quesID, "YTQ", players[Next(id)].Socket);    // Your Turn to Question
                                            else
                                                SendToClient(i, "NQ" + quesID.ToString(), players[i].Socket);    // Next Question from quesID
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < players.Count; i++)
                                        {
                                            SendToClient(i, "WA" + ID + temp, players[i].Socket);   // Wrong Answewr
                                            if (i == Next(id))
                                                SendToClient(i, "YTA", players[Next(id)].Socket);    // Your Turn to Answer
                                            else
                                                SendToClient(i, "NA" + Next(id).ToString(), players[i].Socket);    // Next Answer from Next(id)
                                        }
                                    }
                                }
                                break;
                            default:
                                MessageBox.Show("Wrong instruction");
                                break;
                        }
                    }
                }
            }
        }

        public void SendToClient(int Client_ID, string Mesaage, TcpClient client_socket)
        {
            byte[] data = Encoding.UTF8.GetBytes(Mesaage);
            NetworkStream networkStream = client_socket.GetStream();
            try
            {
                if (networkStream.CanWrite)
                {
                    networkStream.Write(data, 0, data.Length);
                    ADD_TO_LIST("Send- '" + Mesaage + "' to Client " + Client_ID);
                    Thread.Sleep(50);
                }
                else
                    ADD_TO_LIST("Fail to sent to Client " + Client_ID);
            }
            catch (IOException ex)
            {
                ADD_TO_LIST(ex.Message);
            }
        }
        #endregion
        #region Game
        public void Next()
        {
            if (quesID == players.Count - 1)
                quesID = 0;
            else
                quesID++;
        }

        public int Next(int now)
        {
            if(now + 1 == quesID)
                return Next(now+1);
            else if (now == players.Count - 1)
                return 0;
            else
                return now + 1;
        }

        public string Check(string temp)
        {
            int a = 0;
            int b = 0;
            for(int i = 0; i < 4; i++) 
            {
                for(int j = 0; j < 4; j++)
                {

                }
            }

            return a.ToString() + "A" + b.ToString() + "B";
        }
        #endregion
        #region Button
        private void btn_ServerStart_Click(object sender, EventArgs e)
        {
            tcpListener.Start();
            ADD_TO_LIST("Waiting For Connections");
            Thread connectionThread = new Thread(AcceptConnections);
            ADD_TO_LIST("Server is listening at " + Address + ":" + Port);
            connectionThread.IsBackground = true;
            connectionThread.Start();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
        #region List
        void ADD_TO_LIST(string message)
        {
            if (list_Connect.InvokeRequired)
            {
                list_Connect.Invoke((MethodInvoker)(() => list_Connect.Items.Add(message)));
            }
            else
            {
                list_Connect.Items.Add(message);
            }
        }
        #endregion

    }
}