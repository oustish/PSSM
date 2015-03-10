using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSSM.Classes
{
    class SongsDatabase
    {
        private List<SongData> songs = new List<SongData>();
        public int Count
        {
            get
            {
                return this.songs.Count;
            }
        }

        public SongsDatabase()
        {
            songs.Clear();
        }

        public void AddSong(string path)
        {
            SongData sd = new SongData(path);
            songs.Add(sd);
        }

        public void AddSong(IniFile ini)
        {
            SongData sd = new SongData(ini);
            songs.Add(sd);
        }

        public void AddSong(SongData sd)
        {
            songs.Add(sd);
        }

        public ListViewItem[] GetListViewFormatedData()
        {
            List<ListViewItem> output = new List<ListViewItem>();
            SeriesNames sn = new SeriesNames();

            foreach(SongData song in this.songs) {
                if (song == null) continue;

                string artist = song.GetValue("song", "artist");
                string name   = song.GetValue("song", "name");
                string album  = song.GetValue("song", "album");
                string icon   = song.GetValue("song", "icon");

                if ((artist == "" || artist == null) && (name == "" || name == null))
                {
                    // debug
                    Console.WriteLine("Song '" + song.path + "' does not contain any data (?)");
                    continue;
                }

                List<string> info = new List<string>();
                ListViewItem lvi = new ListViewItem(artist); // adding artist as first element

                info.Add(name); // title
                info.Add(album); // album
                info.Add(sn.GetFullName(icon)); // game_of_origin
                info.Add(song.path); // path to file - outside of songsListView flow

                lvi.SubItems.AddRange(info.ToArray());
                lvi.ToolTipText = song.path;

                output.Add(lvi);
            }
            return output.ToArray();
        }

        public void FillListView(ListView lv)
        {
            lv.BeginUpdate();

            lv.Items.Clear();
            lv.Items.AddRange(this.GetListViewFormatedData());

            lv.EndUpdate();
        }
    }
}
