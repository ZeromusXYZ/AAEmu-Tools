using System;

namespace AAEmu.DBViewer;

public partial class MainForm
{
    public class MapSpawnLocation
    {
        public long id;
        public float x;
        public float y;
        public float z;
        public long count = 1;
        public long zoneGroupId; // Used internally, not actually in the files directly

        private static string FloatToCoord(double f)
        {
            var f1 = Math.Floor(f);
            f -= f1;
            f *= 60;
            var f2 = Math.Floor(f);
            f -= f2;
            f *= 60;
            var f3 = Math.Floor(f);

            return f1.ToString("0") + "° " + f2.ToString("00") + "' " + f3.ToString("00") + "\"";
        }

        public string AsSextant()
        {
            // https://www.reddit.com/r/archeage/comments/3dak17/datamining_every_location_of_everything_in/
            // (0.00097657363894522145695357130138029 * (X - Coordinate)) - 21 = (Longitude in degrees)
            // (0.00097657363894522145695357130138029 * (Y - Coordinate)) - 28 = (Latitude in degrees)

            var fx = (0.00097657363894522145695357130138029f * x) - 21f;
            var fy = (0.00097657363894522145695357130138029f * y) - 28f;
            string res = string.Empty;
            // X - Longitude
            if (fx >= 0f)
            {
                res += "E ";
            }
            else
            {
                res += "W ";
                fx *= -1f;
            }

            res += FloatToCoord(fx);
            res += " , ";
            // Y - Latitude
            if (fy >= 0f)
            {
                res += "N ";
            }
            else
            {
                res += "S ";
                fy *= -1f;
            }

            res += FloatToCoord(fy);

            return res;
        }
    }
}