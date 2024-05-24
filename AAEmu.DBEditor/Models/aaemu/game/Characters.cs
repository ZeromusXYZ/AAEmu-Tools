using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAEmu.DBEditor.data.aaemu.game
{
    partial class Characters
    {
        public string GetRaceName()
        {
            switch (this.Race)
            {
                case 1: return DBEditor.data.Data.Server.GetText("ui_texts", "text", 1105, "<Nuian>");
                case 2: return DBEditor.data.Data.Server.GetText("ui_texts", "text", 2490, "<Fairy>");
                case 3: return "<Dwarf>";
                case 4: return DBEditor.data.Data.Server.GetText("ui_texts", "text", 1107, "<Elf>");
                case 5: return DBEditor.data.Data.Server.GetText("ui_texts","text", 2178, "<Harani>");
                case 6: return DBEditor.data.Data.Server.GetText("ui_texts", "text", 1106, "<Firran>");
                case 7: return "<Returned>";
                case 8: return "<Warborn>";
                default: return "Unknown Race";
            }
        }

        public string GetGenderName()
        {
            switch (this.Gender)
            {
                case 1: return DBEditor.data.Data.Server.GetText("ui_texts", "text", 1108, "<Male>");
                case 2: return DBEditor.data.Data.Server.GetText("ui_texts", "text", 1109, "<Female>");
                default: return "<No Gender>";
            }
        }

        public string GetClassName()
        {
            var s = DBEditor.data.Data.Server.GetText("ui_texts", "text", 1110 + this.Ability1, "<" + this.Ability1 + ">");
            if (this.Ability2 != 11)
                s += "  " + DBEditor.data.Data.Server.GetText("ui_texts", "text", 1110 + this.Ability2, "<" + this.Ability2 + ">");

            if (this.Ability3 != 11)
                s += "  " + DBEditor.data.Data.Server.GetText("ui_texts", "text", 1110 + this.Ability3, "<" + this.Ability3 + ">");

            return s;
        }
    }
}
