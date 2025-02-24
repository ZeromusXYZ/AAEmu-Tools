using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAEmu.DBEditor.data.enums;

namespace AAEmu.DBEditor.data.aaemu.game
{
    partial class Characters
    {
        public string GetRaceName()
        {
            switch (this.Race)
            {
                case 1: return Data.Server.GetUiText(47, "nuian", "<Nuian>");
                case 2: return Data.Server.GetUiText(47, "fairy", "<Fairy>");
                case 3: return Data.Server.GetUiText(47, "dwarf", "<Dwarf>");
                case 4: return Data.Server.GetUiText(47, "elf", "<Elf>");
                case 5: return Data.Server.GetUiText(47, "hariharan", "<Harani>");
                case 6: return Data.Server.GetUiText(47, "ferre", "<Firran>");
                case 7: return Data.Server.GetUiText(47, "returned", "<Returned>");
                case 8: return Data.Server.GetUiText(47, "warborn", "<Warborn>");
                default: return "<Unknown Race>";
            }
        }

        public string GetGenderName()
        {
            switch (this.Gender)
            {
                case 1: return DBEditor.data.Data.Server.GetText("ui_texts", "text", 1108, "<Male>");
                case 2: return DBEditor.data.Data.Server.GetText("ui_texts", "text", 1109, "<Female>");
                default:
                    return "???";
            }
        }

        public string GetClassName()
        {
            var max = AbilityNames.GetMaxClassId();
            var s = AbilityNames.GetClassName(this.Ability1);
            if (this.Ability2 > 0 && this.Ability2 <= max)
                s += "  " + AbilityNames.GetClassName(this.Ability2);

            if (this.Ability3 > 0 && this.Ability3 <= max)
                s += "  " + AbilityNames.GetClassName(this.Ability3);

            return s;
        }
    }
}
