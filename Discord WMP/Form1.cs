using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media;
using Windows.Media.Playback;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Discord_WMP {
    public partial class Form1 : Form {

        const string version = "v2.2.4b";
        const string date = "19.10.24";

        public static string url = "https://github.com/T0biasCZe/Windows-Media-Player-Discord-RPC/";

        //public RemotedWindowsMediaPlayer rm = new RemotedWindowsMediaPlayer();
        public RemotedWindowsMediaPlayer rm;
        public bool wmpConnected = false;

        //RemotedWindowsMediaPlayer rm;
        private bool show_author;
        private bool show_title;
        private bool show_album;
        private bool show_albumart;
        private bool show_progressbar;
        private bool send_media_info;
        private bool show_console = true;
        public static bool albummanageropen = false;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        string playeddata = "played data:";
        private void SmoothingText_Paint(object sender, PaintEventArgs e) {
            //Console.WriteLine("draw function ran");
            //clear previously drawed text
            e.Graphics.Clear(Color.White);
            Font TextFont = new Font("Terminal", 8);
            e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            e.Graphics.DrawString(playeddata, TextFont, Brushes.DarkGray, 10, 32);
        }

        public Form1() {

            Console.SetWindowSize(50, 15);
            InitializeComponent();

            this.FormClosing += Form1_Closing;
            //change console size

            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Restore").Click += (s, e) => RestoreForm();
            notifyIcon1.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => Application.Exit();
            notifyIcon1.MouseClick += (s, e) => { if(e.Button == MouseButtons.Left) RestoreForm(); };


            //run function settingsload when Settings1 finish loading
            //settingsload();
            var handle = GetConsoleWindow();
            handle = GetConsoleWindow();
            // Show console during boot
            ShowWindow(handle, SW_SHOW);

            Console.WriteLine("Loading app, please wait:)");
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SmoothingText_Paint);
        }

        private void RestoreForm() {
            if(show_console) {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_SHOW);
            }
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            notifyIcon1.Dispose();
            base.OnFormClosing(e);
        }
        private void Form1_Load(object sender, EventArgs e) {
            Console.WriteLine("veemo");
            if(!show_console) {
                var handle = GetConsoleWindow();
                // Show console during boot
                ShowWindow(handle, SW_HIDE);
            }
        }


        private static bool loadingsettings = false;
        private void settingsload() {
            loadingsettings = true;
            show_console = Settings1.Default.show_console;

            Console.WriteLine("loaded settings");
            loadingsettings = false;
        }
        private void Form1_Closing(object sender, FormClosingEventArgs e) {
            Console.WriteLine("saved settings");
            //save settings
            Settings1.Default.Save();
            //close the console window
            //get handlr
            var handle = GetConsoleWindow();
            Application.Exit();
        }

        private void checkBox_changed(object sender, EventArgs e) {
            //do nothing if settings are loading
            if(loadingsettings) return;
            Settings1.Default.show_console = show_console;

            var handle = GetConsoleWindow();
            if(show_console) ShowWindow(handle, SW_SHOW);
            else ShowWindow(handle, SW_HIDE);

            Console.WriteLine("saved settings");
            Settings1.Default.Save();
        }
        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            string URL = ((LinkLabel)sender).Links[0].LinkData.ToString();
            Console.WriteLine(URL);
            System.Diagnostics.Process.Start(URL);
        }

        public bool ConnectWmp() {
            //check if windows media player is running
            Process[] pname = Process.GetProcessesByName("wmplayer");
            if(pname.Length == 0) {
                Console.WriteLine("Windows Media Player not running");
                return false;
            }
            //check if remotedwindowsmediaplayer is already connected
            if(wmpConnected == false) {
                Console.WriteLine("WMP found, connecting to it");
                rm = new RemotedWindowsMediaPlayer();
                rm.Dock = DockStyle.Fill;
                panel1.Controls.Add(rm);
                panel1.Refresh();
                panel1.Update();
                wmpConnected = true;
            }
            return true;
        }
        public struct playback_data {
            public string title;
            public string album;
            public string artist;
            public string audiofilename;
            public string audiofilepath;

            public string lenght;
            public string position;
            public double lenght_sec;
            public double position_sec;
            public WMPLib.WMPPlayState play_state;

            public string guid;
            public string path;
            public string media_type;
        }
        //gets current information from Windows Media Player
        public playback_data Data() {
            playback_data data = new playback_data();
            data.artist = ""; data.album = ""; data.title = ""; data.lenght = ""; data.position = ""; data.lenght_sec = -1; data.position_sec = -1; data.play_state = WMPLib.WMPPlayState.wmppsStopped; data.guid = ""; data.path = "";
            // Get the currently playing media information
            int retrycount = 0;
            WMPLib.IWMPPlayer4 player = (WMPLib.IWMPPlayer4)rm.GetOcx();
        veemo:
            try {
                player = ((WMPLib.IWMPPlayer4)rm.GetOcx());
                var a = player.currentMedia.duration;
            }
            catch {
                Thread.Sleep(20);
                retrycount++;
                ConsoleColor old = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error trying to read WMP data, retrying");
                Console.ForegroundColor = old;
                if(retrycount < 1) goto veemo;
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    playeddata = "Couldnt find WMP";
                    Console.WriteLine("aborting");
                    Console.ForegroundColor = old;
                    goto abort;
                }
            }

            retrycount = 0;
            while(retrycount < 1) {
                try {
                    data.title = player.currentMedia.getItemInfo("Title");
                    data.album = player.currentMedia.getItemInfo("WM/AlbumTitle");
                    data.artist = player.currentMedia.getItemInfo("WM/AlbumArtist");
                    data.audiofilepath = player.controls.currentItem.sourceURL;
                    //get filename from the path
                    data.audiofilename = Path.GetFileName(data.audiofilepath);
                    data.lenght = player.currentMedia.durationString;
                    data.position = player.controls.currentPositionString;
                    data.lenght_sec = player.currentMedia.duration;
                    data.position_sec = player.controls.currentPosition;
                    data.play_state = player.playState;
                    data.guid = player.currentMedia.getItemInfo("WMCollectionID");
                    data.path = player.currentMedia.sourceURL;
                    data.media_type = player.currentMedia.getItemInfo("MediaType");


                    break;
                }
                catch {
                    retrycount++;
                    Thread.Sleep(1);
                    ConsoleColor old = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("error while reading data from WMP (this can happen if song changes when the data is being read)");
                    Console.ForegroundColor = old;
                }
            }

        abort:;
            return data;
        }
        //displays the current information from Windows Media Player
        private void debug(playback_data data) {
            label3.Text = "initialized " + initialized.ToString();
            label4.Text = "send_data_lasttime " + send_data_lasttime.ToString();
            label5.Text = "play_state " + data.play_state.ToString();
            label6.Text = "mediatype " + data.media_type;
            label7.Text = "audiofilename " + data.audiofilename;
        }

        bool initialized = false;
        bool send_data_lasttime = false;
        Stopwatch pause_stopwatch = new Stopwatch();
        private void update_Tick(object sender, EventArgs e) {
            bool wmpConnected = ConnectWmp();
            if(!wmpConnected) {
                Console.WriteLine("WMP not connected. WMP must be running. (debug: update_Tick() ConnectWmp() returned false))");
                playeddata = "WMP not connected\nWMP must be running.";
                this.Refresh();
                return;
            }
            var data = Data();
            debug(data);
            if(data.lenght_sec == -1 || data.position_sec == -1) {
                playeddata = "Couldnt find WMP";
                this.Refresh();
            }
            bool stopped = data.play_state.In(WMPLib.WMPPlayState.wmppsStopped, WMPLib.WMPPlayState.wmppsMediaEnded, WMPLib.WMPPlayState.wmppsUndefined);
            if(data.play_state == WMPPlayState.wmppsPaused) {
                //if stopwatch is not running, start it. then if music is paused for more than 5 seconds, stop sending data
                if(!pause_stopwatch.IsRunning) {
                    pause_stopwatch.Start();
                }
                if(pause_stopwatch.ElapsedMilliseconds > 30000) {
                    stopped = true;
                }
            }
            else {
                pause_stopwatch.Reset();
            }
            if(stopped) {
                playeddata = "Stopped";
                this.Refresh();
            }
            SetAlbumArt(data);
        }

        public void SetAlbumArt(playback_data data) {
            string albumartpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "noalbumart2.png";
            string guid = data.guid;
            Console.WriteLine("guid: " + guid);
            string directory = Path.GetDirectoryName(data.audiofilepath);
			string filePathWithoutExtension = Path.GetDirectoryName(data.audiofilepath) + "\\" + Path.GetFileNameWithoutExtension(data.audiofilename);
            string guidlarge = $"AlbumArt_{guid}_Large.jpg";
			string guidsmall = $"AlbumArt_{guid}_Small.jpg";
			if(File.Exists(filePathWithoutExtension + ".png")) {
                albumartpath = filePathWithoutExtension + ".png";
                Console.WriteLine("found png album art");
			}
            else if(File.Exists(filePathWithoutExtension + ".jpg")) {
                albumartpath = filePathWithoutExtension + ".jpg";
				Console.WriteLine("found jpg album art");
			}
            else if(File.Exists(directory + $"\\AlbumArt_{guid}_Large.jpg")) {
                albumartpath = directory + $"\\AlbumArt_{guid}_Large.jpg";
                Console.WriteLine("found WMP guid album art large");
            }
            else if(File.Exists(directory + $"\\AlbumArt_{guid}_Small.jpg")) {
                albumartpath = directory + $"\\AlbumArt_{guid}_Small.jpg";
				Console.WriteLine("found WMP guid album art");
			}
            else {
                Console.WriteLine("no album art found, using default");
			}

            pictureBox1.Image = Image.FromFile(albumartpath);
        }
    }
	//create new class for extension method
	public static class ExtensionMethods {
        public static string Truncate(this string value, int maxChars) {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars);
        }
        public static string Extend(this string value, int minChars) {
            if (value.Length >= minChars) return value;
			return value + new string('​', minChars - value.Length);
		}
		public static bool In<T>(this T obj, params T[] args) {
			return args.Contains(obj);
		}
	}
}
