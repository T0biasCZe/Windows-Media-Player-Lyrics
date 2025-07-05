namespace PlaylistAddedManager {
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_save = new System.Windows.Forms.Button();
            this.button_loaddir = new System.Windows.Forms.Button();
            this.label_progress = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Artist2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vybirac = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(784, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(16, 48);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(304, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = ".mp3;.opus;.ogg;.webm;.m4a;.flac;.wav";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.Artist,
            this.Artist2,
            this.Album,
            this.Title,
            this.TrackID,
            this.vybirac});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(16, 88);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1192, 640);
            this.dataGridView1.TabIndex = 3;
            // 
            // button_save
            // 
            this.button_save.Image = global::PlaylistAddedManager.Properties.Resources.imageres_28;
            this.button_save.Location = new System.Drawing.Point(856, 8);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(40, 40);
            this.button_save.TabIndex = 4;
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_loaddir
            // 
            this.button_loaddir.Image = global::PlaylistAddedManager.Properties.Resources.shell32_165;
            this.button_loaddir.Location = new System.Drawing.Point(808, 8);
            this.button_loaddir.Name = "button_loaddir";
            this.button_loaddir.Size = new System.Drawing.Size(40, 40);
            this.button_loaddir.TabIndex = 1;
            this.button_loaddir.UseVisualStyleBackColor = true;
            this.button_loaddir.Click += new System.EventHandler(this.button_loaddir_Click);
            // 
            // label_progress
            // 
            this.label_progress.AutoSize = true;
            this.label_progress.Location = new System.Drawing.Point(352, 56);
            this.label_progress.Name = "label_progress";
            this.label_progress.Size = new System.Drawing.Size(35, 13);
            this.label_progress.TabIndex = 5;
            this.label_progress.Text = "label1";
            // 
            // FileName
            // 
            this.FileName.HeaderText = "fileName";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 400;
            // 
            // Artist
            // 
            this.Artist.HeaderText = "Interpret";
            this.Artist.Name = "Artist";
            this.Artist.ReadOnly = true;
            // 
            // Artist2
            // 
            this.Artist2.HeaderText = "Interpret Alba";
            this.Artist2.Name = "Artist2";
            this.Artist2.ReadOnly = true;
            // 
            // Album
            // 
            this.Album.HeaderText = "Album";
            this.Album.Name = "Album";
            this.Album.ReadOnly = true;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // TrackID
            // 
            this.TrackID.HeaderText = "Track ID";
            this.TrackID.Name = "TrackID";
            this.TrackID.ReadOnly = true;
            // 
            // vybirac
            // 
            this.vybirac.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.vybirac.HeaderText = "Vybírač songu";
            this.vybirac.Items.AddRange(new object[] {
            "rewzik",
            "Martin Kozar",
            "Denis_Postulka",
            "VALIS",
            "interlol",
            "Patrick",
            "eleanor",
            "Adam._.",
            "Tobik the Mii",
            "Monika Vinci",
            "Jakub Ďurajka",
            "Filks",
            "-"});
            this.vybirac.MinimumWidth = 50;
            this.vybirac.Name = "vybirac";
            this.vybirac.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.vybirac.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 746);
            this.Controls.Add(this.label_progress);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button_loaddir);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_loaddir;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label_progress;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Artist;
        private System.Windows.Forms.DataGridViewTextBoxColumn Artist2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Album;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackID;
        private System.Windows.Forms.DataGridViewComboBoxColumn vybirac;
    }
}

