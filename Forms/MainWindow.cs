using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using PSSM.Classes;

namespace PSSM
{
    /// <summary>
    /// MainWindows class (inherited from Form) is main app class executing most of the work.
    /// </summary>
    public partial class MainWindow : Form
    {
        private SongsDatabase sDatabase = null;
        private ListViewColumnSorter lvwColumnSorter;

        /// <summary>
        /// MainWindow constructor
        /// </summary>
        public MainWindow()
        {
            sDatabase = new SongsDatabase();

            InitializeComponent();
            statusLabel.Text = "";
            songsListView.Items.Clear();
            Application.DoEvents();

            InitializeSettingsMenu();

            lvwColumnSorter = new ListViewColumnSorter();
            this.songsListView.ListViewItemSorter = lvwColumnSorter;
        }

        
        #region INITIALIZATION methods

        /// <summary>
        /// Scans pre-selected songs folder.
        /// </summary>
        private void ScanSongsFolder(object sender = null, EventArgs e = null)
        {
            string path = Classes.PSSM.selectSongsFolder();

            // exit if path is empty or null
            if (path == "" || path == null) return;
            
            // scan for songs
            var files = Directory.EnumerateFiles(@path, "song.ini", SearchOption.AllDirectories);

            // disabling MainWindow so no action (event) can be raised
            this.Enabled = false;

            // informing user about initialization
            statusLabel.Text = "Scanning folder for songs, please wait...";
            Application.DoEvents();

            // using parallelized foreach because files are small (<1KB) and fetching them takes more time than processing them
            // scratch that, it does not work as expected - reverting back to regular foreach
            /*Parallel.ForEach(files,  cFile => {
                sDatabase.AddSong(cFile);
                Application.DoEvents(); // no problem since MainWindow is !Enabled
            });*/
            foreach (var cFile in files)
            {
                sDatabase.AddSong(cFile);
                Application.DoEvents(); // no problem since MainWindow is !Enabled
            }

            // statuslabel update
            statusLabel.Text = "Songs folder scanned (found " + sDatabase.Count + " songs). Now filling list view, please wait...";
            Application.DoEvents();

            // filling listview
            sDatabase.FillListView(songsListView);

            // reenabling MainWindow
            this.Enabled = true;

            // statuslabel update
            statusLabel.Text = "Finished scan (found " + sDatabase.Count + " songs) and filling list view.";
            Application.DoEvents();
        }

        /// <summary>
        /// Method initializing settings menu
        /// </summary>
        private void InitializeSettingsMenu()
        {
            // find out id of current default action
            // 1 - play in PS (normal)
            // 2 - play in PS (practice)
            // 3 - launch in EOF
            // 4 - show in file explorer
            // 5 - open tag editor (unimplemented)

            int current_action = Properties.Settings.Default.defaultDbcAction;

            // clear all checkboxes
            openInExplorerToolStripMenuItem.Checked = false;
            openInExplorerToolStripMenuItem.CheckState = CheckState.Unchecked;

            openInEOFToolStripMenuItem.Checked = false;
            openInEOFToolStripMenuItem.CheckState = CheckState.Unchecked;

            playInNormalModeToolStripMenuItem.Checked = false;
            playInNormalModeToolStripMenuItem.CheckState = CheckState.Unchecked;

            playInPracticeModeToolStripMenuItem.Checked = false;
            playInPracticeModeToolStripMenuItem.CheckState = CheckState.Unchecked;

            //editTagsToolStripMenuItem.Checked = false;
            //editTagsToolStripMenuItem.CheckState = CheckState.Unchecked;

            // set proper menu element to be checked
            if (current_action == 1)
            {
                playInNormalModeToolStripMenuItem.Checked = true;
                playInNormalModeToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else if (current_action == 2)
            {
                playInPracticeModeToolStripMenuItem.Checked = true;
                playInPracticeModeToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else if (current_action == 3)
            {
                openInEOFToolStripMenuItem.Checked = true;
                openInEOFToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else if (current_action == 4)
            {
                openInExplorerToolStripMenuItem.Checked = true;
                openInExplorerToolStripMenuItem.CheckState = CheckState.Checked;
            }
            /*else if (current_action == 5)
            {
                editTagsToolStripMenuItem.Checked = true;
                editTagsToolStripMenuItem.CheckState = CheckState.Checked;
            }*/

        }

        #endregion INITIALIZATION methods

        #region SETTINGS menu items actions

        /// <summary>
        /// Method used to change default action taken after double click on song on the songListView
        /// </summary>
        /// <param name="sender">Default sender parameter</param>
        /// <param name="e">Default EventArgs parameter (defaults to null)</param>
        private void ChangeDefaultDbcAction(object sender, EventArgs e = null)
        {
            if (sender == null) { throw new ArgumentException("`sender` argument cannot be null!"); }

            ToolStripMenuItem prsd_menu_item = (ToolStripMenuItem)sender;
            int id = 0;
            if (prsd_menu_item.Tag.ToString() == "play_in_normal_mode") // id: 1
            {
                id = 1;
            }
            else if (prsd_menu_item.Tag.ToString() == "play_in_practice_mode") // id: 2
            {
                id = 2;
            }
            else if (prsd_menu_item.Tag.ToString() == "open_in_eof") // id: 3
            {
                id = 3;
            }
            else if (prsd_menu_item.Tag.ToString() == "open_in_file_explorer") // id: 4
            {
                id = 4;
            }
            /*else if (prsd_menu_item.Tag.ToString() == "open_in_tag_editor") // id: 5
            {
                id = 5;
            }*/

            if (id > 0 && id <= 4) // <= 5)
            {
                Properties.Settings.Default.defaultDbcAction = id;
                Properties.Settings.Default.Save();
                InitializeSettingsMenu();
            }
        }

        /// <summary>
        /// Method for showing user a dialog allowing him to point program to Phase Shift executable file.
        /// </summary>
        /// <param name="sender">Default sender parameter (defaults to null)</param>
        /// <param name="e">Default EventArgs parameter (defaults to null)</param>
        private void SelectPSexe(object sender = null, EventArgs e = null)
        {
            Classes.PSSM.selectPSexe(true);
        }

        /// <summary>
        /// Method for showing user a dialog allowing him to point directory where Phase Shift songs are kept.
        /// </summary>
        /// <param name="sender">Default sender parameter (defaults to null)</param>
        /// <param name="e">Default EventArgs parameter (defaults to null)</param>
        private void SelectSongsFolder(object sender = null, EventArgs e = null)
        {
            Classes.PSSM.selectSongsFolder(true);
        }

        /// <summary>
        /// Method for showing user a dialog allowing him to point program to EOF executable file.
        /// </summary>
        /// <param name="sender">Default sender parameter (defaults to null)</param>
        /// <param name="e">Default EventArgs parameter (defaults to null)</param>
        private void SelectEOFexe(object sender = null, EventArgs e = null)
        {
            Classes.PSSM.selectEOFexe(true);
        }

        #endregion SETTINGS menu items action
        
        #region songListView event handlers
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInColumns(object sender, ColumnClickEventArgs e)
        {
            ListView myListView = (ListView)sender;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SongDoubleClick(object sender, EventArgs e)
        {
            // find out id ofthe action
            int action_id = Properties.Settings.Default.defaultDbcAction;
            string path = songsListView.SelectedItems[0].SubItems[4].Text;

            if (action_id == 1) // play in normal mode
            {
                Process.Start(Classes.PSSM.selectPSexe(), "\"" + path + "\"");
            }
            else if (action_id == 2) // play in practice mode
            {
                Process.Start(Classes.PSSM.selectPSexe(), "\"" + path + "\" /p");
            }
            else if (action_id == 3) // open in EOF
            {
                Process.Start(Classes.PSSM.selectEOFexe(), "\"" + path + "\"\\notes.mid");
            }
            else if (action_id == 4) // open in file explorer
            {
                Process.Start(path);
            }
            /*else if (action_id == 5)
            {

            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedSongChanged(object sender, EventArgs e)
        {
            if (songsListView.SelectedItems.Count > 0)
            {
                songToolStripMenuItem.Enabled = true;
            }
            else
            {
                songToolStripMenuItem.Enabled = false;
            }
        }

        #endregion songListView event handlers

        #region OTHER

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region SONG menu items actions

        private void PlaySelectedSong(object sender, EventArgs e = null)
        {
            if (sender == null) { throw new ArgumentException("`sender` cannot be null!"); }

            string tag = ((ToolStripMenuItem)sender).Tag.ToString();
            string path = songsListView.SelectedItems[0].SubItems[4].Text;
            string name = songsListView.SelectedItems[0].SubItems[0].Text + " - " + songsListView.SelectedItems[0].SubItems[1].Text;
            if (tag == "play_in_normal_mode" || tag == "play_in_practice_mode")
            {
                string status = "Playing \"";
                string arg = "\"" + path + "\"";
                if (tag == "play_in_practice_mode")
                {
                    arg = arg + " /p";
                    status += name + "\" in Phase Shift (practice mode)";
                }
                else if (tag == "play_in_normal_mode")
                {
                    status += name + "\" in Phase Shift (normal mode)";
                }

                Process.Start(Classes.PSSM.selectPSexe(), arg);
                statusLabel.Text = status;
            }
            else if (tag == "browse_song_folder")
            {
                Process.Start(path);
            }
            else if (tag == "open_in_eof")
            {
                Process.Start(Classes.PSSM.selectEOFexe(), "\"" + path + "\"\\notes.mid");
            }
            /*else if (tag == "open_in_tags_editor")
            {

            }*/
        }

        #endregion

        #region GAME menu items actions

        private void LaunchExternalProgram(object sender, EventArgs e = null)
        {
            if (sender == null) { throw new ArgumentException("`sender` cannot be null!"); }

            string tag = ((ToolStripMenuItem)sender).Tag.ToString();

            if (tag == "launch_phase_shift")
            {
                Process.Start(Classes.PSSM.selectPSexe());
            }
            else if (tag == "launch_eof")
            {
                Process.Start(Classes.PSSM.selectEOFexe());
            }
            else if (tag == "open_songs_folder")
            {
                Process.Start(Classes.PSSM.selectSongsFolder());
            }
        }

        #endregion
    }
}
