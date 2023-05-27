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
    partial class GameUI
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(GameUI));
            lbl_name = new Label();
            tb_name = new TextBox();
            tb_IP = new TextBox();
            lbl_IP = new Label();
            btn_connect = new Button();
            tb_play = new TextBox();
            lbl_state = new Label();
            listBox1 = new ListBox();
            btn_send = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            button0 = new Button();
            btn_clear = new Button();
            btn_back = new Button();
            HudPlayer1 = new PictureBox();
            HudPlayer2 = new PictureBox();
            HudPlayer3 = new PictureBox();
            HudPlayer4 = new PictureBox();
            btn_play = new Button();
            ((ISupportInitialize)HudPlayer1).BeginInit();
            ((ISupportInitialize)HudPlayer2).BeginInit();
            ((ISupportInitialize)HudPlayer3).BeginInit();
            ((ISupportInitialize)HudPlayer4).BeginInit();
            SuspendLayout();
            // 
            // lbl_name
            // 
            lbl_name.AutoSize = true;
            lbl_name.BackColor = Color.Transparent;
            lbl_name.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_name.Location = new Point(146, 81);
            lbl_name.Name = "lbl_name";
            lbl_name.Size = new Size(111, 36);
            lbl_name.TabIndex = 0;
            lbl_name.Text = "Name";
            // 
            // tb_name
            // 
            tb_name.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tb_name.Location = new Point(263, 74);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(200, 48);
            tb_name.TabIndex = 1;
            // 
            // tb_IP
            // 
            tb_IP.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tb_IP.Location = new Point(533, 74);
            tb_IP.Name = "tb_IP";
            tb_IP.Size = new Size(300, 48);
            tb_IP.TabIndex = 3;
            // 
            // lbl_IP
            // 
            lbl_IP.AutoSize = true;
            lbl_IP.BackColor = Color.Transparent;
            lbl_IP.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_IP.Location = new Point(478, 81);
            lbl_IP.Name = "lbl_IP";
            lbl_IP.Size = new Size(49, 36);
            lbl_IP.TabIndex = 2;
            lbl_IP.Text = "IP";
            // 
            // btn_connect
            // 
            btn_connect.BackgroundImage = Properties.Resources.yellow_button00;
            btn_connect.BackgroundImageLayout = ImageLayout.Stretch;
            btn_connect.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_connect.ForeColor = Color.Peru;
            btn_connect.Location = new Point(876, 64);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(193, 71);
            btn_connect.TabIndex = 4;
            btn_connect.Text = "Connect";
            btn_connect.UseVisualStyleBackColor = true;
            btn_connect.Click += btn_connect_Click;
            // 
            // tb_play
            // 
            tb_play.Font = new Font("KenVector Future Thin", 16.125F, FontStyle.Regular, GraphicsUnit.Point);
            tb_play.Location = new Point(146, 205);
            tb_play.Name = "tb_play";
            tb_play.Size = new Size(230, 56);
            tb_play.TabIndex = 5;
            // 
            // lbl_state
            // 
            lbl_state.AutoSize = true;
            lbl_state.BackColor = Color.Transparent;
            lbl_state.Font = new Font("KenVector Future Thin", 13.875F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_state.Location = new Point(146, 153);
            lbl_state.Name = "lbl_state";
            lbl_state.Size = new Size(209, 41);
            lbl_state.TabIndex = 6;
            lbl_state.Text = "Waiting...";
            // 
            // listBox1
            // 
            listBox1.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 36;
            listBox1.Location = new Point(932, 205);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(417, 508);
            listBox1.TabIndex = 7;
            // 
            // btn_send
            // 
            btn_send.BackgroundImage = Properties.Resources.yellow_button00;
            btn_send.BackgroundImageLayout = ImageLayout.Stretch;
            btn_send.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_send.ForeColor = Color.Peru;
            btn_send.Location = new Point(401, 171);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(142, 90);
            btn_send.TabIndex = 8;
            btn_send.Text = "Send";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += btn_send_Click;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.blue_button08;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.AliceBlue;
            button1.Location = new Point(146, 283);
            button1.Name = "button1";
            button1.Size = new Size(90, 90);
            button1.TabIndex = 9;
            button1.Text = "1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackgroundImage = Properties.Resources.blue_button08;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.AliceBlue;
            button2.Location = new Point(286, 283);
            button2.Name = "button2";
            button2.Size = new Size(90, 90);
            button2.TabIndex = 10;
            button2.Text = "2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackgroundImage = Properties.Resources.blue_button08;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button3.ForeColor = Color.AliceBlue;
            button3.Location = new Point(423, 283);
            button3.Name = "button3";
            button3.Size = new Size(90, 90);
            button3.TabIndex = 11;
            button3.Text = "3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackgroundImage = Properties.Resources.blue_button08;
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button4.ForeColor = Color.AliceBlue;
            button4.Location = new Point(146, 395);
            button4.Name = "button4";
            button4.Size = new Size(90, 90);
            button4.TabIndex = 12;
            button4.Text = "4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackgroundImage = Properties.Resources.blue_button08;
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button5.ForeColor = Color.AliceBlue;
            button5.Location = new Point(286, 395);
            button5.Name = "button5";
            button5.Size = new Size(90, 90);
            button5.TabIndex = 13;
            button5.Text = "5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackgroundImage = Properties.Resources.blue_button08;
            button6.BackgroundImageLayout = ImageLayout.Stretch;
            button6.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button6.ForeColor = Color.AliceBlue;
            button6.Location = new Point(423, 395);
            button6.Name = "button6";
            button6.Size = new Size(90, 90);
            button6.TabIndex = 14;
            button6.Text = "6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.BackgroundImage = Properties.Resources.blue_button08;
            button7.BackgroundImageLayout = ImageLayout.Stretch;
            button7.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button7.ForeColor = Color.AliceBlue;
            button7.Location = new Point(146, 507);
            button7.Name = "button7";
            button7.Size = new Size(90, 90);
            button7.TabIndex = 15;
            button7.Text = "7";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.BackgroundImage = Properties.Resources.blue_button08;
            button8.BackgroundImageLayout = ImageLayout.Stretch;
            button8.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button8.ForeColor = Color.AliceBlue;
            button8.Location = new Point(286, 507);
            button8.Name = "button8";
            button8.Size = new Size(90, 90);
            button8.TabIndex = 16;
            button8.Text = "8";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.BackgroundImage = Properties.Resources.blue_button08;
            button9.BackgroundImageLayout = ImageLayout.Stretch;
            button9.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button9.ForeColor = Color.AliceBlue;
            button9.Location = new Point(423, 507);
            button9.Name = "button9";
            button9.Size = new Size(90, 90);
            button9.TabIndex = 17;
            button9.Text = "9";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button0
            // 
            button0.BackgroundImage = Properties.Resources.blue_button08;
            button0.BackgroundImageLayout = ImageLayout.Stretch;
            button0.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button0.ForeColor = Color.AliceBlue;
            button0.Location = new Point(286, 620);
            button0.Name = "button0";
            button0.Size = new Size(90, 90);
            button0.TabIndex = 18;
            button0.Text = "0";
            button0.UseVisualStyleBackColor = true;
            button0.Click += button0_Click;
            // 
            // btn_clear
            // 
            btn_clear.BackgroundImage = Properties.Resources.green_button07;
            btn_clear.BackgroundImageLayout = ImageLayout.Stretch;
            btn_clear.Font = new Font("KenVector Future Thin", 16.125F, FontStyle.Regular, GraphicsUnit.Point);
            btn_clear.ForeColor = Color.AliceBlue;
            btn_clear.Location = new Point(146, 620);
            btn_clear.Name = "btn_clear";
            btn_clear.Size = new Size(90, 90);
            btn_clear.TabIndex = 19;
            btn_clear.Text = "C";
            btn_clear.UseVisualStyleBackColor = true;
            btn_clear.Click += btn_clear_Click;
            // 
            // btn_back
            // 
            btn_back.BackgroundImage = Properties.Resources.green_button07;
            btn_back.BackgroundImageLayout = ImageLayout.Stretch;
            btn_back.Font = new Font("KenVector Future Thin", 16.125F, FontStyle.Regular, GraphicsUnit.Point);
            btn_back.ForeColor = Color.AliceBlue;
            btn_back.Location = new Point(423, 620);
            btn_back.Name = "btn_back";
            btn_back.Size = new Size(90, 90);
            btn_back.TabIndex = 20;
            btn_back.Text = "(-";
            btn_back.UseVisualStyleBackColor = true;
            btn_back.Click += btn_back_Click;
            // 
            // HudPlayer1
            // 
            HudPlayer1.BackColor = Color.Transparent;
            HudPlayer1.BackgroundImage = Properties.Resources.hudPlayer_pink;
            HudPlayer1.BackgroundImageLayout = ImageLayout.Stretch;
            HudPlayer1.Location = new Point(589, 205);
            HudPlayer1.Name = "HudPlayer1";
            HudPlayer1.Size = new Size(100, 100);
            HudPlayer1.TabIndex = 21;
            HudPlayer1.TabStop = false;
            HudPlayer1.Visible = false;
            // 
            // HudPlayer2
            // 
            HudPlayer2.BackColor = Color.Transparent;
            HudPlayer2.BackgroundImage = Properties.Resources.hudPlayer_yellow;
            HudPlayer2.BackgroundImageLayout = ImageLayout.Stretch;
            HudPlayer2.Location = new Point(789, 205);
            HudPlayer2.Name = "HudPlayer2";
            HudPlayer2.Size = new Size(100, 100);
            HudPlayer2.TabIndex = 22;
            HudPlayer2.TabStop = false;
            HudPlayer2.Visible = false;
            // 
            // HudPlayer3
            // 
            HudPlayer3.BackColor = Color.Transparent;
            HudPlayer3.BackgroundImage = Properties.Resources.hudPlayer_green;
            HudPlayer3.BackgroundImageLayout = ImageLayout.Stretch;
            HudPlayer3.Location = new Point(589, 449);
            HudPlayer3.Name = "HudPlayer3";
            HudPlayer3.Size = new Size(100, 100);
            HudPlayer3.TabIndex = 23;
            HudPlayer3.TabStop = false;
            HudPlayer3.Visible = false;
            // 
            // HudPlayer4
            // 
            HudPlayer4.BackColor = Color.Transparent;
            HudPlayer4.BackgroundImage = Properties.Resources.hudPlayer_blue;
            HudPlayer4.BackgroundImageLayout = ImageLayout.Stretch;
            HudPlayer4.Location = new Point(789, 449);
            HudPlayer4.Name = "HudPlayer4";
            HudPlayer4.Size = new Size(100, 100);
            HudPlayer4.TabIndex = 24;
            HudPlayer4.TabStop = false;
            HudPlayer4.Visible = false;
            // 
            // btn_play
            // 
            btn_play.BackgroundImage = Properties.Resources.yellow_button00;
            btn_play.BackgroundImageLayout = ImageLayout.Stretch;
            btn_play.Enabled = false;
            btn_play.Font = new Font("KenVector Future Thin", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_play.ForeColor = Color.Peru;
            btn_play.Location = new Point(1100, 64);
            btn_play.Name = "btn_play";
            btn_play.Size = new Size(193, 71);
            btn_play.TabIndex = 25;
            btn_play.Text = "Play";
            btn_play.UseVisualStyleBackColor = true;
            btn_play.Click += btn_play_Click;
            // 
            // GameUI
            // 
            AutoScaleDimensions = new SizeF(14F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.blue_land;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1414, 819);
            Controls.Add(btn_play);
            Controls.Add(HudPlayer4);
            Controls.Add(HudPlayer3);
            Controls.Add(HudPlayer2);
            Controls.Add(HudPlayer1);
            Controls.Add(btn_back);
            Controls.Add(btn_clear);
            Controls.Add(button0);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btn_send);
            Controls.Add(listBox1);
            Controls.Add(lbl_state);
            Controls.Add(tb_play);
            Controls.Add(btn_connect);
            Controls.Add(tb_IP);
            Controls.Add(lbl_IP);
            Controls.Add(tb_name);
            Controls.Add(lbl_name);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GameUI";
            Text = "幾A幾B";
            ((ISupportInitialize)HudPlayer1).EndInit();
            ((ISupportInitialize)HudPlayer2).EndInit();
            ((ISupportInitialize)HudPlayer3).EndInit();
            ((ISupportInitialize)HudPlayer4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_name;
        private TextBox tb_name;
        private TextBox tb_IP;
        private Label lbl_IP;
        private Button btn_connect;
        private TextBox tb_play;
        private Label lbl_state;
        private ListBox listBox1;
        private Button btn_send;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button0;
        private Button btn_clear;
        private Button btn_back;
        private PictureBox HudPlayer1;
        private PictureBox HudPlayer2;
        private PictureBox HudPlayer3;
        private PictureBox HudPlayer4;
        private Button btn_play;
    }
}