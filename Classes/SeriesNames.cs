using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSM.Classes
{
    class SeriesNames
    {
        private Dictionary<string, string> series = new Dictionary<string, string>();

        public static string bh = "Band Hero";
        public static string bhdlc = "Band Hero DLC";
        public static string gh1 = "Guitar Hero 1";
        public static string gh2 = "Guitar Hero 2";
        public static string gh2dlc = "Guitar Hero 2 DLC";
        public static string gh3 = "Guitar Hero 3";
        public static string gh3dlc = "Guitar Hero 3 DLC";
        public static string gha = "Guitar Hero Aerosmith";
        public static string gh80s = "Guitar Hero Encore - Rock the 80's";
        public static string ghm = "Guitar Hero Metallica";
        public static string ghmdlc = "Guitar Hero Metallica DLC";
        public static string gh5 = "Guitar Hero 5";
        public static string gh5dlc = "Guitar Hero 5 DLC";
        public static string ghsh = "Guitar Hero Smash Hits";
        public static string ghvh = "Guitar Hero Van Hallen";
        public static string ghwor = "Guitar Hero Warriors of Rock";
        public static string ghwordlc = "Guitar Hero Warriors of Rock DLC";
        public static string ghwt = "Guitar Hero World Tour";
        public static string ghwtdlc = "Guitar Hero World Tour DLC";

        public static string rb = "Rock Band";
        public static string rbdlc = "Rock Band DLC";
        public static string ctpk = "Rock Band Country Track Pack";
        public static string ctpk2 = "Rock Band Country Track Pack 2";
        public static string gdrb = "Green Day Rock Band";
        public static string gdrbdlc = "Green Day Rock Band DLC";
        public static string tbrb = "The Beatles Rock Band";
        public static string rbtb = "The Beatles Rock Band";
        public static string tbrbdlc = "The Beatles Rock Band DLC";
        public static string acdcrb = "AC/DC Rock Band";
        public static string rbtpk = "Rock Band Track Pack 1";
        public static string rbtpk2 = "Rock Band Track Pack 2";
        public static string lrb = "LEGO Rock Band";
        public static string lrbdlc = "LEGO Rock Band DLC";
        public static string rbb = "Rock Band BLITZ";
        public static string rbbdlc = "Rock Band BLITZ DLC";
        public static string mlpfim = "Pony Rock Band";
        public static string mtpk = "Rock Band Metal Track Pack";
        public static string crtp = "Rock Band Classic Rock Track Pack";
        public static string rbu = "Rock Band Unplugged";
        public static string rb1 = "Rock Band 1";
        public static string rb1dlc = "Rock Band 1 DLC";
        public static string rb2 = "Rock Band 2";
        public static string rb2dlc = "Rock Band 2 DLC";
        public static string rb3 = "Rock Band 3";
        public static string rb3dlc = "Rock Band 3 DLC";
        public static string rbn = "Rock Band Network";

        public static string ccc = "Custom Creators Collective";

        public SeriesNames()
        {
            series.Add("bh", bh);
            series.Add("bhdlc", bhdlc);
            series.Add("gh1", gh1);
            series.Add("gh2", gh2);
            series.Add("gh2dlc", gh2dlc);
            series.Add("gh3", gh3);
            series.Add("gh3dlc", gh3dlc);
            series.Add("gha", gha);
            series.Add("gh80s", gh80s);
            series.Add("ghm", ghm);
            series.Add("ghmdlc", ghmdlc);
            series.Add("gh5", gh5);
            series.Add("gh5dlc", gh5dlc);
            series.Add("ghsh", ghsh);
            series.Add("ghvh", ghvh);
            series.Add("ghwor", ghwor);
            series.Add("ghwordlc", ghwordlc);
            series.Add("ghwt", ghwt);
            series.Add("ghwtdlc", ghwtdlc);

            series.Add("rb", rb);
            series.Add("rbdlc", rbdlc);
            series.Add("ctpk", ctpk);
            series.Add("ctpk2", ctpk2);
            series.Add("gdrb", gdrb);
            series.Add("gdrbalt", gdrb);
            series.Add("gdrbdlc", gdrbdlc);
            series.Add("tbrb", tbrb);
            series.Add("rbtb", tbrb);
            series.Add("tbrbdlc", tbrbdlc);
            series.Add("rbtbdlc", tbrbdlc);
            series.Add("acdcrb", acdcrb);
            series.Add("rbtpk", rbtpk);
            series.Add("rbtpk2", rbtpk2);
            series.Add("lrb", lrb);
            series.Add("lrbdlc", lrbdlc);
            series.Add("rbb", rbb);
            series.Add("rbbdlc", rbbdlc);
            series.Add("mlpfim", mlpfim);
            series.Add("mtpk", mtpk);
            series.Add("crtp", crtp);
            series.Add("rbu", rbu);
            series.Add("rb1", rb1);
            series.Add("rb1dlc", rb1dlc);
            series.Add("rb2", rb2);
            series.Add("rb2dlc", rb2dlc);
            series.Add("rb3", rb3);
            series.Add("rb3dlc", rb3dlc);
            series.Add("rbn", rbn);

            series.Add("ccc", ccc);
        }

        public string GetFullName(string _series)
        {
            if (_series.Trim() == "") return "";

            else if (series.ContainsKey(_series.Trim()))
            {
                return series[_series.Trim()];
            }

            else return "";
        }
    }
}
