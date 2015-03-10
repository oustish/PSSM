using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PSSM.Classes
{
    class SongData
    {
        private string _path = null;
        private IniFile _ini = null;
        private string md5hash = null;

        public string path
        {
            get
            {
                return _path;
            }
        }
        public string hash
        {
            get
            {
                return md5hash;
            }
        }

        /// <summary>
        /// Creates SongData object from given path.
        /// </summary>
        /// <param name="path">Path to song.ini file</param>
        public SongData(string path)
        {
            this._path = @Path.GetDirectoryName(path);
            this._ini = new IniFile(path);
        }

        /// <summary>
        /// Creates SongData object from given IniFile object.
        /// </summary>
        /// <param name="inifile">IniFile object containing path to song.ini file</param>
        public SongData(IniFile inifile)
        {
            this._ini = inifile;
            this._path = @Path.GetDirectoryName(inifile.path);
        }

        public string GetValue(string section, string key)
        {
            return this._ini.IniReadValue(section, key);
        }

        public void SetValue(string section, string key, string value)
        {
            this._ini.IniWriteValue(section, key, value);
        }

        public void SetValue(string section, string key, int value)
        {
            this.SetValue(section, key, value.ToString());
        }

        public string GenerateMD5Hash()
        {
            // if hash was already generated - skip it and return value
            if (this.md5hash != "" && this.md5hash != null) return this.md5hash;

            var md5 = MD5.Create();
            var stream = File.OpenRead(this._path + "/notes.mid");
            string hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();

            // dispose everything not needed
            md5.Dispose();
            stream.Close();
            stream.Dispose();

            this.md5hash = hash;

            return hash;
        }
    }
}
