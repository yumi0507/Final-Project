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
        int ansID;
        int left;
        string Ans;
        Thread Connection;

        List<ClientState> players;

        public Server()
        {
            InitializeComponent();
            Address = GetLocalIpAddress();
            Port = "8080";
            tb_IP.Text = Address + ":" + Port;

            clientID = 0;
            quesID = 0;
            ansID = 1;
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
                        Thread Listen_to_Client = new Thread(Listen); 
                        Listen_to_Client.IsBackground = true;
                        Listen_to_Client.Start(new_client);
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

        public void Listen(object obj)
        {
            TcpClient this_client = (TcpClient)obj;
            NetworkStream networkStream = this_client.GetStream();
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
                        int ID = int.Parse(iD);
                        ADD_TO_LIST("RCV- '" + Message + "' from Client " + iD);
                        switch (Command)
                        {
                            case "ID":
                                {
                                    string name = Message.Substring(3);
                                    players[ID].Name = name;
                                    break;
                                }
                            case "PL":
                                {
                                    for(int i = 0; i < players.Count; i++)
                                    {
                                        for(int j = 0; j < players.Count; j++)
                                        {
                                            SendToClient(i, "AP" + players[j].Name, players[i].Socket);
                                        }
                                    }
                                    NewRound(true);
                                    break;
                                }
                            case "QU":
                                {
                                    Ans = Message.Substring(3);
                                    for (int i = 0; i < players.Count; i++)
                                    {
                                        SendToClient(i, "FQ" + ID, players[i].Socket);    // ID Finished Questioning
                                    }
                                    NextAns(true);
                                    break;
                                }
                            case "AN":
                                {
                                    string temp = Message.Substring(3);
                                    if(Check(temp, ID))
                                    {
                                        for(int i = 0; i < players.Count; i++)
                                        {
                                            SendToClient(i, "RE" + ID, players[i].Socket);  // Round End (ID is correct)
                                        }
                                        NewRound();
                                    }
                                    else
                                    {
                                        for (int i = 0; i < players.Count; i++)
                                        {
                                            SendToClient(i, "WA" + ID, players[i].Socket);   // Wrong Answewr
                                        }
                                        if (left > 0)
                                            NextAns();
                                        else
                                        {
                                            for (int i = 0; i < players.Count; i++)
                                            {
                                                SendToClient(i, "NO" + Ans, players[i].Socket);
                                            }
                                            NewRound();
                                        }
                                    }
                                    break;
                                }
                            case "EN":
                                {
                                    for (int i = 0; i < players.Count; i++)
                                    {
                                        SendToClient(i, "EN", players[i].Socket);
                                    }
                                    break;
                                }
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
                    ADD_TO_LIST("SND- '" + Mesaage + "' to Client " + Client_ID);
                    Thread.Sleep(100);
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
        public bool Check(string temp, int ID)
        {
            int a = 0;
            int b = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (temp[i] == Ans[j])
                    {
                        if (i == j) a++;
                        else b++;
                    }
                }
            }

            for (int i = 0; i < players.Count; i++)
            {
                SendToClient(i, "FA" + ID.ToString() + temp, players[i].Socket); // Finish answering
                SendToClient(i, "AB" + a.ToString() + "A" + b.ToString() + "B", players[i].Socket);
            }

            if (a == 4)
                return true;
            else
            {
                players[ID].Time--;
                return false;
            }
        }

        public void Next(bool ques = false)
        {
            if (quesID < 0)
                return;
            if (ques)
            {
                if (quesID == players.Count - 1)
                    quesID = 0;
                else
                    quesID++;
            }

            if (ansID == players.Count - 1)
                ansID = 0;
            else
                ansID++;

            if (players[ansID].Time == 0)
            {
                left--;
                if(left > 0)
                    Next();
            }
            if (ansID == quesID)
                Next();
        }

        public void NextAns(bool first = false)
        {
            if(!first)
                Next();
            for(int i = 0; i < players.Count; i++)
            {
                if (i == ansID)
                    SendToClient(i, "YTA", players[i].Socket);    // Your Turn to Answer
                else
                    SendToClient(i, "NA" + ansID.ToString(), players[i].Socket);    // Next Answer from ansID
            }
        }

        public void NewRound(bool first = false)
        {
            if(!first)
                Next(true);
            for (int i = 0; i < players.Count; i++)
            {
                if (quesID < 0)
                    break;
                else if (i == quesID)
                    SendToClient(i, "YTQ", players[i].Socket);    // Your Turn to Qusetion
                else
                    SendToClient(i, "NQ" + quesID.ToString(), players[i].Socket);    // Next Question from quesID

                players[i].Time = 4;
            }
            left = players.Count - 1;
        }
        #endregion
        #region Button
        private void btn_ServerStart_Click(object sender, EventArgs e)
        {
            tcpListener = new TcpListener(IPAddress.Parse(Address), int.Parse(Port));
            tcpListener.Start();
            ADD_TO_LIST("Waiting For Connections");
            Connection = new Thread(AcceptConnections);
            ADD_TO_LIST("Server is listening at " + Address + ":" + Port);
            Connection.IsBackground = true;
            Connection.Start();
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