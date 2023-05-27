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
using System.Media;
using System.Numerics;
using System.Drawing.Printing;

namespace Client
{
    public partial class GameUI : Form
    {
        int myID;
        string ID;
        string Address;
        string Port;
        TcpClient client;
        List<ClientState> players;
        Thread Listen_to_Server;

        public GameUI()
        {
            InitializeComponent();
            client = new TcpClient();
            players = new List<ClientState>();
            StopAction();

        }

        #region Connection
        public void Send(string Message)
        {
            NetworkStream network = client.GetStream();
            byte[] Data = Encoding.UTF8.GetBytes(Message);
            if (network.CanWrite)
                network.Write(Data, 0, Data.Length);
            else
                MessageBox.Show("Can't Write");
        }

        public void Listen()
        {
            NetworkStream networkStream = client.GetStream();
            while (true)
            {
                if (networkStream.CanRead)
                {
                    byte[] buffer = new byte[2048];
                    int BytesReaded = networkStream.Read(buffer, 0, buffer.Length);
                    if (BytesReaded > 0)
                    {
                        string Message = Encoding.UTF8.GetString(buffer, 0, BytesReaded);
                        //ADD_TO_LOG("Receive '" + Message + "' from server");
                        string Command = Message.Substring(0, 2);
                        switch (Command)
                        {
                            case "ID":
                                {
                                    ID = Message.Substring(2);
                                    myID = int.Parse(ID);
                                }
                                break;
                            case "AP":
                                {
                                    string tempName = Message.Substring(2);
                                    AddPlayer(tempName);
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            btn_connect.Enabled = false;
            lbl_IP.Visible = false;
            tb_IP.Visible = false;
            btn_connect.Visible = false;
            btn_play.Enabled = true;

            /*
            for (int i = 0; i < tb_IP.Text.Length; i++)
            {
                if (tb_IP.Text[i] == ':')
                {
                    Address = tb_IP.Text.Substring(0, i);
                    Port = tb_IP.Text.Substring(i + 1);
                    break;
                }
            }
            client.Connect(IPAddress.Parse(Address), int.Parse(Port));
            if (client.Connected)
            {
                try
                {
                    Send("ID" + tb_name.Text);
                    Listen_to_Server = new Thread(Listen);
                    Listen_to_Server.IsBackground = true;
                    Listen_to_Server.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }
            else
                MessageBox.Show("Can't connect to server");
            */
            lbl_state.Text = "Waiting";
            YourTurn();
        }
        #endregion
        #region Game

        public void AddPlayer(string name)
        {
            players.Add(new ClientState(name));
            ADD_TO_LIST(name + " is in the game.");
            if (players.Count == 1)
                HudPlayer1.Visible = true;
            else if (players.Count == 2)
                HudPlayer2.Visible = true;
            else if (players.Count == 3)
                HudPlayer3.Visible = true;
            else if (players.Count == 4)
                HudPlayer4.Visible = true;
        }

        public void realYourTurn()
        {
            button0.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            btn_back.Enabled = true;
            btn_clear.Enabled = true;
            btn_send.Enabled = true;
            if (players[myID].Ques)
            {
                lbl_state.Text = "Ask Ques";
            }
            else
            {
                lbl_state.Text = "Answer";
            }
        }
        public void YourTurn()
        {
            button0.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            btn_back.Enabled = true;
            btn_clear.Enabled = true;
            btn_send.Enabled = true;
            if (true)
            {
                lbl_state.Text = "Ask Ques";
            }
            else
            {
                lbl_state.Text = "Answer";
            }
        }

        public void StopAction()
        {
            button0.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            btn_back.Enabled = false;
            btn_clear.Enabled = false;
            btn_send.Enabled = false;
            lbl_state.Text = "Waiting";
            tb_play.Text = string.Empty;
        }

        public void Question()
        {
            players[myID].Ques = true;
            YourTurn();
        }

        public void Answer()
        {
            players[myID].Ques = false;
            YourTurn();
        }
        #endregion
        #region Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '1')
                        return;
                }
                tb_play.Text += "1";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '2')
                        return;
                }
                tb_play.Text += "2";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '3')
                        return;
                }
                tb_play.Text += "3";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '4')
                        return;
                }
                tb_play.Text += "4";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '5')
                        return;
                }
                tb_play.Text += "5";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '6')
                        return;
                }
                tb_play.Text += "6";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '7')
                        return;
                }
                tb_play.Text += "7";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '8')
                        return;
                }
                tb_play.Text += "8";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '9')
                        return;
                }
                tb_play.Text += "9";
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                foreach (char c in tb_play.Text)
                {
                    if (c == '0')
                        return;
                }
                tb_play.Text += "0";
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_play.Text = string.Empty;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length > 0)
                tb_play.Text = tb_play.Text.Remove(tb_play.Text.Length - 1);
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (tb_play.Text.Length < 4)
            {
                MessageBox.Show("You haven't done your answering.");
                return;
            }

            if (players[myID].Ques)
            {
                Send("QU" + ID + tb_play.Text);
            }
            else
            {
                Send("AN" + ID + tb_play.Text);
            }

            StopAction();
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            btn_play.Enabled = false;
            btn_play.Visible = false;
            Send("RD");
        }
        #endregion
        #region List
        void ADD_TO_LIST(string message)
        {
            listBox1.Items.Add(message);
        }
        #endregion

    }
}