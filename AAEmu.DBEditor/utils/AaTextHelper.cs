using AAEmu.DBEditor.data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAEmu.DBEditor.utils
{
    internal class AaTextHelper
    {
        /// <summary>
        /// Takes formatted text in ArcheAge text format and enters it into a RichTextBox
        /// </summary>
        /// <param name="formattedText"></param>
        /// <param name="rt"></param>
        public static void FormattedTextToRichText(string formattedText, RichTextBox rt)
        {
            rt.Text = string.Empty;
            rt.SelectionColor = rt.ForeColor;
            rt.SelectionBackColor = rt.BackColor;
            string restText = formattedText;
            //restText = restText.Replace("\r\n\r\n", "\r");
            restText = restText.Replace("\r\n", "\r");
            restText = restText.Replace("\n", "\r");
            var resetColor = rt.ForeColor;

            while (restText.Length > 0)
            {
                var nextEndBracket = restText.IndexOf(")");
                var nextEndAccolade = restText.IndexOf("}");
                if (restText.StartsWith("\r")) // linefeed
                {
                    var currentCol = rt.SelectionColor;
                    rt.AppendText("\r");
                    rt.SelectionColor = currentCol;
                    restText = restText.Substring(1);
                }
                else if (restText.StartsWith("|r")) // reset color
                {
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(2);
                }
                else if (restText.StartsWith("[START_DESCRIPTION]")) // reset color
                {
                    resetColor = Color.LimeGreen;
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(19);
                }
                else if (restText.StartsWith("|nc;")) // named color ?
                {
                    rt.SelectionColor = Color.White;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|ni;")) // named color ?
                {
                    rt.SelectionColor = Color.GreenYellow;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|nd;")) // named color ?
                {
                    rt.SelectionColor = Color.OrangeRed;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|nr;")) // named color ?
                {
                    rt.SelectionColor = Color.Red;
                    restText = restText.Substring(4);
                }
                else if (restText.StartsWith("|c")) // hexcode color
                {
                    rt.SelectionColor = HexStringToArgbColor(restText.Substring(2, 8));
                    restText = restText.Substring(10);
                }
                else if (restText.StartsWith("@ITEM_NAME(") && (nextEndBracket > 11))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(11, nextEndBracket - 11);
                    if (long.TryParse(valueText, out var value))
                        rt.AppendText(Data.Server.GetText("items", "name", value, "@ITEM_NAME(" + valueText + ")"));
                    else
                        rt.AppendText("@ITEM_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@DOODAD_NAME(") && (nextEndBracket > 13))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(13, nextEndBracket - 13);
                    if (long.TryParse(valueText, out var value))
                        rt.AppendText(Data.Server.GetText("doodad_almighties", "name", value, "@DOODAD_NAME(" + valueText + ")"));
                    else
                        rt.AppendText("@DOODAD_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@NPC_NAME(") && (nextEndBracket > 10))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(10, nextEndBracket - 10);
                    if (long.TryParse(valueText, out var value))
                        rt.AppendText(Data.Server.GetText("npcs", "name", value, "@NPC_NAME(" + valueText + ")"));
                    else
                        rt.AppendText("@NPC_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@NPC_GROUP_NAME(") && (nextEndBracket > 17))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(17, nextEndBracket - 17);
                    if (long.TryParse(valueText, out var value))
                        rt.AppendText(Data.Server.GetText("quest_monster_groups", "name", value, "@NPC_GROUP_NAME(" + valueText + ")"));
                    else
                        rt.AppendText("@NPC_GROUP_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("@QUEST_NAME(") && (nextEndBracket > 12))
                {
                    rt.SelectionColor = Color.Yellow;
                    var valueText = restText.Substring(12, nextEndBracket - 12);
                    if (long.TryParse(valueText, out var value))
                        rt.AppendText(Data.Server.GetText("quest_contexts", "name", value, "@QUEST_NAME(" + valueText + ")"));
                    else
                        rt.AppendText("@QUEST_NAME(" + valueText + ")");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndBracket + 1);
                }
                else if (restText.StartsWith("#{") && (nextEndAccolade > 2))
                {
                    rt.SelectionColor = Color.Pink;
                    var valueText = restText.Substring(2, nextEndAccolade - 2);
                    rt.AppendText("#{" + valueText + "}");
                    rt.SelectionColor = resetColor;
                    restText = restText.Substring(nextEndAccolade + 1);
                }
                else
                {
                    rt.AppendText(restText.Substring(0, 1));
                    restText = restText.Substring(1);
                }
            }
        }

        /// <summary>
        /// Convert a hex string to a .NET Color object.
        /// </summary>
        /// <param name="hexArgb">a hex string: "FFFFFFFF", "00000000"</param>
        public static Color HexStringToArgbColor(string hexArgb)
        {
            string a;
            string r;
            string g;
            string b;
            if (hexArgb.Length == 8)
            {
                a = hexArgb.Substring(0, 2);
                r = hexArgb.Substring(2, 2);
                g = hexArgb.Substring(4, 2);
                b = hexArgb.Substring(6, 2);
            }
            else if (hexArgb.Length == 6)
            {
                a = hexArgb.Substring(0, 2);
                r = hexArgb.Substring(2, 2);
                g = hexArgb.Substring(4, 2);
                b = hexArgb.Substring(6, 2);
            }
            else
            {
                // you can choose whether to throw an exception
                //throw new ArgumentException("hexColor is not exactly 6 digits.");
                return Color.Empty;
            }

            Color color = Color.Empty;
            try
            {
                int ai = Int32.Parse(a, System.Globalization.NumberStyles.HexNumber);
                int ri = Int32.Parse(r, System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse(g, System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse(b, System.Globalization.NumberStyles.HexNumber);
                color = Color.FromArgb(ai, ri, gi, bi);
            }
            catch
            {
                // you can choose whether to throw an exception
                //throw new ArgumentException("Conversion failed.");
                return Color.Empty;
            }

            return color;
        }

        /// <summary>
        /// Converts copper coins count to a more human friendly string notation
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string CopperToString(long amount)
        {
            var gold = amount;
            var copper = gold % 100;
            gold /= 100;
            var silver = gold % 100;
            gold /= 100;

            var res = string.Empty;
            if (gold > 0)
                res += $"{gold}g";
            if (silver > 0)
            {
                if (res.Length > 0)
                    res += " ";
                res += $"{silver}s";
            }
            if (copper > 0 || amount < 100)
            {
                if (res.Length > 0)
                    res += " ";
                res += $"{copper}c";
            }

            return res;
        }
    }
}
