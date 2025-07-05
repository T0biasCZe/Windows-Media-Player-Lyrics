using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaylistAddedManager {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button_loaddir_Click(object sender, EventArgs e) {
            string infolder = textBox1.Text;
            //check if the folder exists
            if(!System.IO.Directory.Exists(infolder)) {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Folder does not exist", "OKUREK", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] fileExtensions = textBox2.Text.Split(';');
            //load all the files that match some of the extensions
            List<string> filteredFiles = new List<string>();
            foreach(string ext in fileExtensions) {
                filteredFiles.AddRange(System.IO.Directory.GetFiles(infolder, "*" + ext, System.IO.SearchOption.AllDirectories));
            }
            //load the files into the grid. the grid has cells in order FileName, Artist, Artist2, Album, Title, TrackID, this.vybirac
            dataGridView1.Rows.Clear();
            foreach(string file in filteredFiles) {
                string[] row = new string[7];
                row[0] = file;
                dataGridView1.Rows.Add(row);
                label_progress.Text = "Loading metadata for " + file;
            }
            //sort the dgv by column Track ID
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);
            Application.DoEvents();
            LoadFileMetadata();
        }
        public async void LoadFileMetadata() {
            await Task.Delay(1);
            //load the metadata of the files in the grid using TagLibSharp
            for(int i = 0; i < dataGridView1.Rows.Count; i++) {
                //skip if the row is empty row for new row entering
                if(dataGridView1.Rows[i].IsNewRow) {
                    continue;
                }
                label_progress.Text = "Loading metadata for " + dataGridView1.Rows[i].Cells[0].Value.ToString();
                string file = dataGridView1.Rows[i].Cells[0].Value.ToString();
                TagLib.File tagFile = TagLib.File.Create(file);
                string[] performersArray = tagFile.Tag.Performers;
                string performers = string.Join(", ", performersArray);

                string[] albumArtistsArray = tagFile.Tag.AlbumArtists;
                string albumArtists = string.Join(", ", albumArtistsArray);

                dataGridView1.Rows[i].Cells[1].Value = performers;
                dataGridView1.Rows[i].Cells[2].Value = albumArtists;
                dataGridView1.Rows[i].Cells[3].Value = tagFile.Tag.Album;
                dataGridView1.Rows[i].Cells[4].Value = tagFile.Tag.Title;
                dataGridView1.Rows[i].Cells[5].Value = tagFile.Tag.Track;

                //check if file with same name as the audio but with .pick extensions instead of audio extension exists, and if it does, load the contents of the file into the last cell of the row
                string pathWithoutExtension = System.IO.Path.ChangeExtension(file, null);
                string pickFile = pathWithoutExtension + ".pick";
                if(File.Exists(pickFile)) {
                    label_progress.Text = "Loading pick for " + file;
                    //dataGridView1.Rows[i].Cells[6].Value = File.ReadAllText(pickFile);
                    string loadedText = File.ReadAllText(pickFile);

                    //set the combobox to the value of the loaded text. if the loaded text is not in the combobox, add it to the combobox
                    if(!string.IsNullOrEmpty(loadedText)) {
                        if(!this.vybirac.Items.Contains(loadedText)) {
                            this.vybirac.Items.Add(loadedText);
                        }
                        //set the value of the combobox on the cell to the loaded text
                        dataGridView1.Rows[i].Cells[6].Value = loadedText;
                    }

                }
                Application.DoEvents();
            }

        }
        public void SaveAllPicks() {
            //save the contents of the last cell of each row into a file with the same name as the audio but with .pick extensions instead of audio extension
            for(int i = 0; i < dataGridView1.Rows.Count; i++) {
                //skip if the row is empty row for new row entering
                if(dataGridView1.Rows[i].IsNewRow) {
                    continue;
                }

                label_progress.Text = "Saving pick for row " + i.ToString() + " " + dataGridView1.Rows[i].Cells[0].Value.ToString();
                string file = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string pathWithoutExtension = System.IO.Path.ChangeExtension(file, null);
                string pickFile = pathWithoutExtension + ".pick";
                var pickContentObject = dataGridView1.Rows[i].Cells[6].Value;
                string pickContent = pickContentObject?.ToString() ?? "";
                if(string.IsNullOrEmpty(pickContent)) {
                    if(File.Exists(pickFile)) {
                        File.Delete(pickFile);
                    }
                }
                else {
                    File.WriteAllText(pickFile, pickContent);
                }
            }
        }
        private void button_save_Click(object sender, EventArgs e) {
            SaveAllPicks();
        }
        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void Form1_Resize(object sender, EventArgs e) {
            dataGridView1.Width = this.Width - 44;
            dataGridView1.Height = this.Height - 144;
        }
    }
}
