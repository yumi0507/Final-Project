using System.Net.Sockets;
using System.Text;

namespace Client_test
{
    public partial class Form1 : Form
    {
        int myID;
        TcpClient client;
  //      List<ClientState> players;
        Thread Listen_to_Server;
        public Form1()
        {
            InitializeComponent();
        }
        public void SentToServer(string Message)
        {
            NetworkStream network = client.GetStream();
            byte[] Data = Encoding.UTF8.GetBytes(Message);
            if (network.CanWrite)
                network.Write(Data, 0, Data.Length);
            else
                MessageBox.Show("Can't Write");
        }
    }
}
