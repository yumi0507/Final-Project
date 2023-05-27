namespace Server
{
    partial class Server
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            btn_ServerStart = new Button();
            list_Connect = new ListBox();
            btn_Exit = new Button();
            lbl_IP = new Label();
            tb_IP = new TextBox();
            lbl_Status = new Label();
            SuspendLayout();
            // 
            // btn_ServerStart
            // 
            btn_ServerStart.Font = new Font("Microsoft JhengHei UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            btn_ServerStart.Location = new Point(200, 202);
            btn_ServerStart.Name = "btn_ServerStart";
            btn_ServerStart.Size = new Size(150, 80);
            btn_ServerStart.TabIndex = 0;
            btn_ServerStart.Text = "Server Start";
            btn_ServerStart.UseVisualStyleBackColor = true;
            btn_ServerStart.Click += btn_ServerStart_Click;
            // 
            // list_Connect
            // 
            list_Connect.FormattingEnabled = true;
            list_Connect.ItemHeight = 30;
            list_Connect.Location = new Point(100, 329);
            list_Connect.Name = "list_Connect";
            list_Connect.Size = new Size(587, 334);
            list_Connect.TabIndex = 1;
            // 
            // btn_Exit
            // 
            btn_Exit.Font = new Font("Microsoft JhengHei UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Exit.Location = new Point(437, 202);
            btn_Exit.Name = "btn_Exit";
            btn_Exit.Size = new Size(150, 80);
            btn_Exit.TabIndex = 3;
            btn_Exit.Text = "Exit";
            btn_Exit.UseVisualStyleBackColor = true;
            btn_Exit.Click += btn_Exit_Click;
            // 
            // lbl_IP
            // 
            lbl_IP.AutoSize = true;
            lbl_IP.Font = new Font("Microsoft JhengHei UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_IP.Location = new Point(200, 120);
            lbl_IP.Name = "lbl_IP";
            lbl_IP.Size = new Size(39, 35);
            lbl_IP.TabIndex = 5;
            lbl_IP.Text = "IP";
            // 
            // tb_IP
            // 
            tb_IP.Font = new Font("Microsoft JhengHei UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            tb_IP.Location = new Point(287, 117);
            tb_IP.Name = "tb_IP";
            tb_IP.Size = new Size(300, 42);
            tb_IP.TabIndex = 6;
            // 
            // lbl_Status
            // 
            lbl_Status.AutoSize = true;
            lbl_Status.Font = new Font("Microsoft JhengHei UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_Status.Location = new Point(166, 38);
            lbl_Status.Name = "lbl_Status";
            lbl_Status.Size = new Size(209, 35);
            lbl_Status.TabIndex = 9;
            lbl_Status.Text = "Server Stopped";
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(14F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 729);
            Controls.Add(lbl_Status);
            Controls.Add(tb_IP);
            Controls.Add(lbl_IP);
            Controls.Add(btn_Exit);
            Controls.Add(list_Connect);
            Controls.Add(btn_ServerStart);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Server";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_ServerStart;
        private ListBox list_Connect;
        private Button btn_Exit;
        private Label lbl_IP;
        private TextBox tb_IP;
        private Label lbl_Status;
    }
}