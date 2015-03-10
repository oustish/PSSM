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
        private IniParser.Model.IniData _ini = null;
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
            this._path = Path.GetDirectoryName(path);

            var parser = new IniParser.FileIniDataParser();
            this._ini = parser.ReadFile(path);
        }

        public string GetValue(string section, string key)
        {
            return this._ini[section][key];
        }

        public void SetValue(string section, string key, string value)
        {
            var parser = new IniParser.FileIniDataParser();
            this._ini[section][key] = value;
            parser.WriteFile(this._path + @"\song.ini", this._ini);
            
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
            var stream = File.OpenRead(this._path + @"\notes.mid");
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
