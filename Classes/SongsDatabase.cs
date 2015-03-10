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

            foreach(SongData song in this.songs) {
                if (song == null) continue;

                List<string> info = new List<string>();
                ListViewItem lvi = new ListViewItem(song.GetValue("song", "artist")); // adding artist as first element

                info.Add(song.GetValue("song", "name")); // title
                info.Add(song.GetValue("song", "album")); // album
                info.Add(song.GetValue("song", "icon")); // game_of_origin
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
