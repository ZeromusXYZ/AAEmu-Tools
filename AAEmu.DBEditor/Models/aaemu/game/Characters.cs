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
                case 1: return "Nuian";
                case 2: return "Fairy";
                case 3: return "Dwarf";
                case 4: return "Elf";
                case 5: return "Harani";
                case 6: return "Firran";
                case 7: return "Returned";
                case 8: return "Warborn";
                default: return "none";
            }
        }

        public string GetGenderName()
        {
            switch (this.Gender)
            {
                case 1: return "Male";
                case 2: return "Female";
                default: return "none";
            }
        }
    }
}
