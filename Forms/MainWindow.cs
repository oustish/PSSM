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

            lvwColumnSorter = new ListViewColumnSorter();
            this.songsListView.ListViewItemSorter = lvwColumnSorter;
        }

        /// <summary>
        /// Scans pre-selected songs folder.
        /// Avoids using PSSM.Classes.PSSM.selectPSexe() method - be careful!
        /// </summary>
        private void scanSongsFolder(object sender = null, EventArgs e = null)
        {
            string path = Properties.Settings.Default.songsFolder;

            // exit if path is empty or null
            if (path == "" || path == null) return;

            string[] files = Directory.GetFiles(@path, "song.ini", SearchOption.AllDirectories);

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

        /*
         * FILE menu items actions
         */

        /// <summary>
        /// Method for showing user a dialog allowing him to point program to Phase Shift executable file.
        /// </summary>
        /// <param name="sender">Default sender parameter (defaults to null)</param>
        /// <param name="e">Default EventArgs parameter (defaults to null)</param>
        private void selectPSexe(object sender = null, EventArgs e = null)
        {
            Classes.PSSM.selectPSexe(true);
        }

        /// <summary>
        /// Method for showing user a dialog allowing him to point directory where Phase Shift songs are kept.
        /// </summary>
        /// <param name="sender">Default sender parameter (defaults to null)</param>
        /// <param name="e">Default EventArgs parameter (defaults to null)</param>
        private void selectSongsFolder(object sender = null, EventArgs e = null)
        {
            Classes.PSSM.selectSongsFolder(true);
        }

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
    }
}
