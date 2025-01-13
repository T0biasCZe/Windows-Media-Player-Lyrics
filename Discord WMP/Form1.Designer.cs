namespace Discord_WMP {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.update = new System.Windows.Forms.Timer(this.components);
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.richTextBox_lyrics = new System.Windows.Forms.RichTextBox();
			this.label_song = new System.Windows.Forms.Label();
			this.label_album = new System.Windows.Forms.Label();
			this.label_kapela = new System.Windows.Forms.Label();
			this.label_vybral = new System.Windows.Forms.Label();
			this.label_tlyrics = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// update
			// 
			this.update.Enabled = true;
			this.update.Interval = 1000;
			this.update.Tick += new System.EventHandler(this.update_Tick);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(320, 152);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(16, 96);
			this.panel1.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(323, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 17;
			this.label3.Text = "label3";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(323, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 13);
			this.label4.TabIndex = 18;
			this.label4.Text = "label4";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(323, 67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 13);
			this.label5.TabIndex = 19;
			this.label5.Text = "label5";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(323, 91);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 13);
			this.label6.TabIndex = 20;
			this.label6.Text = "label6";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(310, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(117, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Debug values:  (ignore)";
			// 
			// toolTip1
			// 
			this.toolTip1.ToolTipTitle = "settings";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(323, 111);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(35, 13);
			this.label7.TabIndex = 24;
			this.label7.Text = "label7";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(323, 130);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(35, 13);
			this.label8.TabIndex = 25;
			this.label8.Text = "label8";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(16, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(216, 216);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 26;
			this.pictureBox1.TabStop = false;
			// 
			// richTextBox_lyrics
			// 
			this.richTextBox_lyrics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.richTextBox_lyrics.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.richTextBox_lyrics.Location = new System.Drawing.Point(16, 320);
			this.richTextBox_lyrics.Name = "richTextBox_lyrics";
			this.richTextBox_lyrics.ReadOnly = true;
			this.richTextBox_lyrics.Size = new System.Drawing.Size(224, 376);
			this.richTextBox_lyrics.TabIndex = 27;
			this.richTextBox_lyrics.Text = "";
			// 
			// label_song
			// 
			this.label_song.AutoSize = true;
			this.label_song.BackColor = System.Drawing.Color.Transparent;
			this.label_song.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label_song.ForeColor = System.Drawing.SystemColors.MenuHighlight;
			this.label_song.Location = new System.Drawing.Point(16, 232);
			this.label_song.Name = "label_song";
			this.label_song.Size = new System.Drawing.Size(92, 21);
			this.label_song.TabIndex = 28;
			this.label_song.Text = "label_song";
			// 
			// label_album
			// 
			this.label_album.AutoSize = true;
			this.label_album.BackColor = System.Drawing.Color.Transparent;
			this.label_album.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label_album.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label_album.Location = new System.Drawing.Point(16, 256);
			this.label_album.Name = "label_album";
			this.label_album.Size = new System.Drawing.Size(71, 15);
			this.label_album.TabIndex = 29;
			this.label_album.Text = "label_album";
			// 
			// label_kapela
			// 
			this.label_kapela.AutoSize = true;
			this.label_kapela.BackColor = System.Drawing.Color.Transparent;
			this.label_kapela.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label_kapela.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label_kapela.Location = new System.Drawing.Point(16, 272);
			this.label_kapela.Name = "label_kapela";
			this.label_kapela.Size = new System.Drawing.Size(71, 15);
			this.label_kapela.TabIndex = 30;
			this.label_kapela.Text = "label_kapela";
			// 
			// label_vybral
			// 
			this.label_vybral.AutoSize = true;
			this.label_vybral.BackColor = System.Drawing.Color.Transparent;
			this.label_vybral.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label_vybral.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.label_vybral.Location = new System.Drawing.Point(16, 288);
			this.label_vybral.Name = "label_vybral";
			this.label_vybral.Size = new System.Drawing.Size(69, 15);
			this.label_vybral.TabIndex = 31;
			this.label_vybral.Text = "label_vybral";
			// 
			// label_tlyrics
			// 
			this.label_tlyrics.AutoSize = true;
			this.label_tlyrics.BackColor = System.Drawing.Color.Transparent;
			this.label_tlyrics.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label_tlyrics.ForeColor = System.Drawing.Color.Blue;
			this.label_tlyrics.Location = new System.Drawing.Point(24, 312);
			this.label_tlyrics.Name = "label_tlyrics";
			this.label_tlyrics.Size = new System.Drawing.Size(92, 19);
			this.label_tlyrics.TabIndex = 32;
			this.label_tlyrics.Text = "TLyric3000™";
			this.label_tlyrics.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(248, 708);
			this.Controls.Add(this.label_tlyrics);
			this.Controls.Add(this.label_vybral);
			this.Controls.Add(this.label_kapela);
			this.Controls.Add(this.label_album);
			this.Controls.Add(this.label_song);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.richTextBox_lyrics);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "WMP Lyrics";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer update;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
		public System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.RichTextBox richTextBox_lyrics;
		private System.Windows.Forms.Label label_song;
		private System.Windows.Forms.Label label_album;
		private System.Windows.Forms.Label label_kapela;
		private System.Windows.Forms.Label label_vybral;
		private System.Windows.Forms.Label label_tlyrics;
	}
}

