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
        string myiD;
        string Address;
        string Port;
        TcpClient client;
        List<ClientState> players;
        Thread Listen_to_Server;

        private delegate void DelYourTurn();
        private delegate void DelStopAction(bool end);
        private delegate void DelAddPlayer(string name);
        private delegate void DelRefresh(int ID);
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
            byte[] data = Encoding.UTF8.GetBytes(Message);
            NetworkStream networkStream = client.GetStream();
            try
            {
                if (networkStream.CanWrite)
                {
                    networkStream.Write(data, 0, data.Length);
                    Thread.Sleep(50);
                }
                else
                    ADD_TO_LIST("Fail to send to server ");
            }
            catch (IOException ex)
            {
                ADD_TO_LIST(ex.Message);
            }
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
                        string Command = Message.Substring(0, 2);
                        switch (Command)
                        {
                            case "ID":
                                {
                                    myiD = Message.Substring(2);
                                    myID = int.Parse(myiD);
                                    ADD_TO_LIST("You are added into the game.");
                                    ADD_TO_LIST("NO." + myiD);
                                    break;
                                }
                            case "AP":
                                {
                                    string tempName = Message.Substring(2);
                                    AddPlayer(tempName);
                                    ADD_TO_LIST("Player" + (players.Count - 1).ToString() + " " + tempName + " is in the game.");
                                    break;
                                }
                            case "YT":
                                {
                                    string func = Message.Substring(2);
                                    endl();
                                    if (func == "Q")
                                        Question();
                                    else
                                        Answer();
                                    break;
                                }
                            case "NQ":
                                {
                                    string iD = Message.Substring(2);
                                    int ID = int.Parse(iD);
                                    endl();
                                    ADD_TO_LIST("It's " + players[ID].Name + " turn to ask the question.");
                                    break;
                                }
                            case "NA":
                                {
                                    string iD = Message.Substring(2);
                                    int ID = int.Parse(iD);
                                    endl();
                                    ADD_TO_LIST("It's " + players[ID].Name + " turn to answer the question.");
                                    break;
                                }
                            case "FQ":
                                {
                                    string iD = Message.Substring(2, 1);
                                    int ID = int.Parse(iD);
                                    ADD_TO_LIST(players[ID].Name + " finished asking a question.");
                                    break;
                                }
                            case "FA":
                                {
                                    string iD = Message.Substring(2, 1);
                                    string result = Message.Substring(3);
                                    int ID = int.Parse(iD);
                                    ADD_TO_LIST(players[ID].Name + "'s answer is " + result + ".");
                                    break;
                                }
                            case "AB":
                                {
                                    string result = Message.Substring(2);
                                    ADD_TO_LIST("= " + result);
                                    break;
                                }
                            case "WA":
                                {
                                    string iD = Message.Substring(2, 1);
                                    int ID = int.Parse(iD);
                                    ADD_TO_LIST(players[ID].Name + " got the wrong answer.");
                                    break;
                                }
                            case "RE":
                                {
                                    string iD = Message.Substring(2, 1);
                                    int ID = int.Parse(iD);
                                    ADD_TO_LIST(players[ID].Name + " got the correct answer.");
                                    players[ID].Ans++;
                                    Refresh(ID);
                                    break;
                                }
                            case "NO":
                                {
                                    string ans = Message.Substring(2);
                                    ADD_TO_LIST("No one got the answer.");
                                    ADD_TO_LIST("The Ans: " + ans);
                                    break;
                                }
                        }
                    }
                }
            }
        }

        #endregion
        #region Game

        public void AddPlayer(string name)
        {
            if (InvokeRequired)
            {
                DelAddPlayer del = new DelAddPlayer(AddPlayer);
                this.Invoke(del, name);
            }
            else
            {
                if (btn_play.Enabled)
                {
                    btn_play.Invoke((MethodInvoker)(() => { btn_play.Enabled = false; }));
                    btn_play.Invoke((MethodInvoker)(() => { btn_play.Visible = false; }));
                }
                players.Add(new ClientState(name));
                if (players.Count == 1)
                {
                    HudPlayer1.Visible = true;
                    lbl_player1_name.Text = players[0].Name;
                    lbl_player1_name.Visible = true;
                    lbl_player1_ans.Visible = true;
                }
                else if (players.Count == 2)
                {
                    HudPlayer2.Visible = true;
                    lbl_player2_name.Text = players[1].Name;
                    lbl_player2_name.Visible = true;
                    lbl_player2_ans.Visible = true;
                }
                else if (players.Count == 3)
                {
                    HudPlayer3.Visible = true;
                    lbl_player3_name.Text = players[2].Name;
                    lbl_player3_name.Visible = true;
                    lbl_player3_ans.Visible = true;
                }
                else if (players.Count == 4)
                {
                    HudPlayer4.Visible = true;
                    lbl_player4_name.Text = players[3].Name;
                    lbl_player4_name.Visible = true;
                    lbl_player4_ans.Visible = true;
                }
            }
        }

        public void YourTurn()
        {
            if (InvokeRequired)
            {
                DelYourTurn del = new DelYourTurn(YourTurn);
                this.Invoke(del);
            }
            else
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
                    ADD_TO_LIST("It's your turn to ask the question.");

                }
                else
                {
                    lbl_state.Text = "Answer";
                    ADD_TO_LIST("It's your turn to answer the question.");
                }
            }
        }

        public void StopAction(bool end = false)
        {
            if (InvokeRequired)
            {
                DelStopAction del = new DelStopAction(StopAction);
                this.Invoke(del, end);
            }
            else
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
                if (end)
                    lbl_state.Text = "End";
                else
                    lbl_state.Text = "Waiting";
                tb_play.Text = string.Empty;
            }
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

        public void Refresh(int ID)
        {
            if (InvokeRequired)
            {
                DelRefresh del = new DelRefresh(Refresh);
                this.Invoke(del, ID);
            }
            else
            {
                if (ID == 0)
                    lbl_player1_ans.Text = players[ID].Ans.ToString();
                else if (ID == 1)
                    lbl_player2_ans.Text = players[ID].Ans.ToString();
                else if (ID == 2)
                    lbl_player3_ans.Text = players[ID].Ans.ToString();
                else if (ID == 3)
                    lbl_player4_ans.Text = players[ID].Ans.ToString();

                if (players[ID].Ans == 5)
                {
                    endl();
                    ADD_TO_LIST(players[ID].Name + " is the winner!");
                    MessageBox.Show(players[ID].Name + " is the winner!");
                    StopAction(true);
                }
            }
        }
        #endregion
        #region Num Button
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
        #endregion
        #region Connection Button
        private void btn_connect_Click(object sender, EventArgs e)
        {
            btn_connect.Enabled = false;
            lbl_IP.Visible = false;
            tb_IP.Visible = false;
            btn_connect.Visible = false;
            btn_play.Enabled = true;


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
                    Listen_to_Server = new Thread(Listen);
                    Listen_to_Server.IsBackground = true;
                    Listen_to_Server.Start();
                    Thread.Sleep(50);
                    Send("ID" + myiD + tb_name.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }
            else
                MessageBox.Show("Can't connect to server");

            lbl_state.Text = "Waiting";
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            btn_play.Invoke((MethodInvoker)(() => { btn_play.Enabled = false; }));
            btn_play.Invoke((MethodInvoker)(() => { btn_play.Visible = false; }));
            Send("PL" + myiD);
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
                ADD_TO_LIST("The correct ans: " + tb_play.Text);
                Send("QU" + myiD + tb_play.Text);
            }
            else
            {
                Send("AN" + myiD + tb_play.Text);
            }

            StopAction();
        }
        #endregion
        #region List

        void endl()
        {
            ADD_TO_LIST("");
        }

        public void ADD_TO_LIST(string message)
        {
            if (listBox1.InvokeRequired)
            {
                Invoke(new Action<string>(ADD_TO_LIST), message);
                return;
            }
            else
            {
                listBox1.Items.Add(message);
                listBox1.TopIndex = listBox1.Items.Count - 1;
            }
        }
        #endregion

    }
}