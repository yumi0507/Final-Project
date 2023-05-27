using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Client
{
    internal class ClientState
    {
        int iD;
        string name;

        int ans;

        bool question;

        public ClientState(string name)
        {
            this.name = name;
            ans = 0;
            question = false;
        }

        public int ID { get { return iD; } set { iD = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Ans { get { return ans; } set { ans = value; } }
        public bool Ques { get { return question; } set { question = value; } }



    }
}
