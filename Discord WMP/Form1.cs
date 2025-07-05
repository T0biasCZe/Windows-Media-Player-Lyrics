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

            pictureBox1.ImageLocation = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + "noalbumart2.png";
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
            public string helpingArtist;
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
            //Console.WriteLine("obtaining data");
            playback_data data = new playback_data();
            data.artist = ""; data.helpingArtist = "";  data.album = ""; data.title = ""; data.lenght = ""; data.position = ""; data.lenght_sec = -1; data.position_sec = -1; data.play_state = WMPLib.WMPPlayState.wmppsStopped; data.guid = ""; data.path = "";
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
                    data.helpingArtist = player.currentMedia.getItemInfo("Author");
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

            label8.Text = "title " + data.title;
            label9.Text = "artist " + data.artist;
            label9.Text = "helpint " + data.helpingArtist;
            label10.Text = "album " + data.album;
        }

        bool initialized = false;
        bool send_data_lasttime = false;
        Stopwatch pause_stopwatch = new Stopwatch();
        string lastSongPath = "";
		private void update_Tick(object sender, EventArgs e) {
            //Console.WriteLine("tick");
            bool wmpConnected = ConnectWmp();
            showCd = false;
            if(!wmpConnected) {
                Console.WriteLine("WMP not connected. WMP must be running. (debug: update_Tick() ConnectWmp() returned false))");
                playeddata = "WMP not connected\nWMP must be running.";
                this.Refresh();
                return;
            }
            var data = Data();

            if(data.path.Contains("flac") || data.path.Contains("wav")) {
                showCd = true;
            }
            debug(data);
            if(data.lenght_sec == -1 || data.position_sec == -1) {
                playeddata = "Couldnt find WMP";
                //this.Refresh();
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
			else {
				if(data.path != lastSongPath) {
					lastSongPath = data.path;
                    SongChanged(data);
				}
				playeddata = data.artist + " - " + data.title;
				//this.Refresh();
			}

            if(subtitle != null) {
				richTextBox_lyrics.Rtf = subtitle.GetFormattedForRichTextBox(data.position_sec);
			}
            else {
                richTextBox_lyrics.Text = "\nText není pro tuhle písničku k dispozici";
			}

			HideCaret(richTextBox_lyrics.Handle);

		}

        string lastAlbumArt = "";
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

            if(albumartpath != lastAlbumArt) {
                pictureBox1.Image = Image.FromFile(albumartpath);
            }
            lastAlbumArt = albumartpath;
        }
        Subtitle subtitle;
        public void SongChanged(playback_data data) {
            Console.WriteLine("song change detected");
            SetAlbumArt(data);
            string filepath = data.audiofilepath;
			//check if json file with the same name as the audio file exists
			string jsonpath = Path.GetDirectoryName(filepath) + "\\" + Path.GetFileNameWithoutExtension(filepath) + ".json";
			string lrcpath = Path.GetDirectoryName(filepath) + "\\" + Path.GetFileNameWithoutExtension(filepath) + ".lrc";
			if(File.Exists(lrcpath)) {
				Console.WriteLine("found lyrics lrc");
				string lrc = File.ReadAllText(lrcpath);
				subtitle = Subtitle.LoadFromLrc(lrc);
				richTextBox_lyrics.Rtf = subtitle.GetFormattedForRichTextBox(data.position_sec);
			} else if(File.Exists(jsonpath)) {
                Console.WriteLine("found lyrics json");
				string json = File.ReadAllText(jsonpath);
				subtitle = Subtitle.LoadFromJson(json);
				richTextBox_lyrics.Rtf = subtitle.GetFormattedForRichTextBox(data.position_sec);
			}
			else {
				richTextBox_lyrics.Text = data.artist + " - " + data.title;
			}

            //label_song.Text = data.title;
			//label_kapela.Text = data.artist;
			//label_album.Text = data.album;

			string metapath = Path.GetDirectoryName(filepath) + "\\" + Path.GetFileNameWithoutExtension(filepath) + ".meta.csv";
            if(File.Exists(metapath)) {
                //parse the csv. if csv has key "Artist", set its value to label_kapela, and //if csv has key "Album", set its value to label_album, and if csv has key "Title", set its value to label_song
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                Console.WriteLine("found meta csv");
                string[] lines = File.ReadAllLines(metapath);
                foreach(string line in lines) {
                    string[] parts = line.Split(';');
                    if(parts.Length < 2) continue; // skip if line is not valid
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    switch(key.ToLower()) {
                        case "artist":
                            label_kapela.Text = value.UnicodeFix();
                            Console.WriteLine("set kapela based on csv");
                            break;
                        case "album":
                            label_album.Text = value.UnicodeFix();
							Console.WriteLine("set album based on csv");
							break;
                        case "title":
                            label_song.Text = value.UnicodeFix();
							Console.WriteLine("set nazev based on csv");
							break;
                        default:
                            Console.WriteLine($"Unknown key in meta csv: {key}");
                            break;
                    }
                }
            }

			string txtpath = Path.GetDirectoryName(filepath) + "\\" + Path.GetFileNameWithoutExtension(filepath) + ".pick";
			if(File.Exists(txtpath)) {
				Console.WriteLine("found vybirac songu txt");
				string txt = File.ReadAllText(txtpath);
				label_vybral.Text = "vybral: " + txt;
			}
			else {
				label_vybral.Text = "";
			}

		}

		[DllImport("user32.dll")]
		static extern bool HideCaret(IntPtr hWnd);

		private void Form1_Resize(object sender, EventArgs e) {
            int width = Math.Min(this.Width, 400);
            pictureBox1.Width = width - 48;
            pictureBox1.Height = width - 48;

            int pictureBoxSizeDif = 0 - (216 - pictureBox1.Height);

            label_song.Top = 232 + pictureBoxSizeDif;
            label_album.Top = 256 + pictureBoxSizeDif;
			label_kapela.Top = 272 + pictureBoxSizeDif;
			label_vybral.Top = 288 + pictureBoxSizeDif;
            label_tlyrics.Top = 312 + pictureBoxSizeDif;
			richTextBox_lyrics.Top = 320 + pictureBoxSizeDif;

            richTextBox_lyrics.Height = this.Height - 380 - pictureBoxSizeDif;
            richTextBox_lyrics.Width = width - 48;




		}

        int gifFrame = 0;
        bool showCd = true;
        bool showCdLast = true;
        List<Bitmap> frames;

        private void drawCd() {
            if(frames == null) {
                // Load cd1.png, cd2.png, and cd3.png, which are next to the exe
                frames = new List<Bitmap>();
                for(int i = 1; i <= 3; i++) {
                    string path = Path.GetDirectoryName(Application.ExecutablePath) + $"\\cd{i}.png";
                    if(File.Exists(path)) {
                        frames.Add(new Bitmap(path));
                    }
                }
            }
            if(showCd) {
                var g = pictureBox1.CreateGraphics();
                // Draw the current frame
                if(showCd && frames.Count > 0) {
                    g.DrawImage(frames[gifFrame], new Rectangle(0, 0, 64, 64));
                    gifFrame = (gifFrame + 1) % frames.Count;
                }
            }
            if(showCdLast == true && showCd == false) {
                pictureBox1.Refresh();
                Console.WriteLine("Refreshing picture box because cd last was true and show cd false");
            }
            showCdLast = showCd;
        }

        private void timer_cd_Tick(object sender, EventArgs e) {
            //Console.WriteLine("CD Draw tick");
            drawCd();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            //Console.WriteLine("picture box paint");
            drawCd();
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

        public static string UnicodeFix(this string text) {
            Dictionary<string, string> replacements = new Dictionary<string, string> {
                { "├í", "á" },
                { "┼í", "š" },
                { "─Ť", "ě" },
                {"├Ż", "ý"},
                {"├ę", "é"},
                {"┼ż", "ž"},
            };

            foreach (var replacement in replacements) {
                text = text.Replace(replacement.Key, replacement.Value);
            }

            return text;
        }
	}
}
