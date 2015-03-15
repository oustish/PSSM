using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSSM.Classes
{
    /// <summary>
    /// Class that houses static methods used acros project in various places
    /// </summary>
    public static class PSSM
    {
        /// <summary>
        /// Main static method to select Phase Shift executable file.
        /// </summary>
        /// <param name="force">Determines if method should display a dialog even if the path was already specified.</param>
        /// <returns>String containing path to PS exe file.</returns>
        public static string selectPSexe(bool force = false)
        {
            string psexe = Properties.Settings.Default.PSexe;

            if (psexe == "" || force)
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.CheckFileExists = true;
                opf.CheckPathExists = true;
                opf.DereferenceLinks = true;
                opf.Filter = "Phase Shift executable|phase_shift.exe";
                opf.Multiselect = false;
                opf.Title = "Select Phase Shift executable file (phase_shift.exe)";

                DialogResult opf_dr = opf.ShowDialog();

                if (opf_dr == DialogResult.OK)
                {
                    psexe = opf.FileName;
                    Properties.Settings.Default.PSexe = psexe;
                    Properties.Settings.Default.Save();
                }
                return psexe;
            }
            else
            {
                return psexe;
            }
        }

        /// <summary>
        /// Main static method to select Editor On Fire executable file.
        /// </summary>
        /// <param name="force">Determines if method should display a dialog even if the path was already specified.</param>
        /// <returns>String containing path to EOF exe file.</returns>
        public static string selectEOFexe(bool force = false)
        {
            string eofexe = Properties.Settings.Default.EOFexe;

            if (eofexe == "" || force)
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.CheckFileExists = true;
                opf.CheckPathExists = true;
                opf.DereferenceLinks = true;
                opf.Filter = "Editor On Fire executable|eof.exe";
                opf.Multiselect = false;
                opf.Title = "Select Editor On Fire executable file (eof.exe)";

                DialogResult opf_dr = opf.ShowDialog();

                if (opf_dr == DialogResult.OK)
                {
                    eofexe = opf.FileName;
                    Properties.Settings.Default.EOFexe = eofexe;
                    Properties.Settings.Default.Save();
                }
                return eofexe;
            }
            else
            {
                return eofexe;
            }
        }

        /// <summary>
        /// Main static method to select folder containing PS songs.
        /// </summary>
        /// <param name="force">Determines if method should display a dialog even if the path was already specified.</param>
        /// <returns>String containing path to PS songs folder.</returns>
        public static string selectSongsFolder(bool force = false)
        {
            string songsFolder = Properties.Settings.Default.songsFolder;

            if (songsFolder == "" || force)
            {
                FolderBrowserDialog opf = new FolderBrowserDialog();
                opf.Description = "Select folder in which you keep your Phase Shift songs";
                opf.ShowNewFolderButton = false;

                DialogResult opf_dr = opf.ShowDialog();

                if (opf_dr == DialogResult.OK)
                {
                    songsFolder = opf.SelectedPath;
                    Properties.Settings.Default.songsFolder = songsFolder;
                    Properties.Settings.Default.Save();
                }
                return songsFolder;
            }
            else
            {
                return songsFolder;
            }
        }
    }
}
