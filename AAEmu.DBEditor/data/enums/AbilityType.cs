using AAEmu.DBEditor.data.gamedb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAEmu.DBEditor.data.enums
{
    public enum AbilityType : byte
    {
        General = 0,
        BattleRage = 1, // Fight
        Witchcraft = 2, // Illusion
        Defense = 3, // Adamant
        Auramancy = 4, // Will
        Occultism = 5, // Death
        Archery = 6, // Wild
        Sorcery = 7, // Magic
        Shadowplay = 8, // Vocation
        Songcraft = 9, // Romance
        Vitalism = 10, // Love
        None = 11
    }

    public class AbilityNames
    {
        private static readonly List<(sbyte, sbyte, sbyte)> Lookup = new();

        private static void InitializeLookup()
        {
            Lookup.Clear();
            for (sbyte a = 0; a < 10; a++)
            {
                for (sbyte b = (sbyte)(a+1); b < 10; b++)
                {
                    for (sbyte c = (sbyte)(b+1); c < 10; c++)
                    {
                        Lookup.Add((a, b, c));
                    }
                }
            }
        }

        /// <summary>
        /// Returns the offset for ui_texts for class names
        /// </summary>
        /// <param name="ability1"></param>
        /// <param name="ability2"></param>
        /// <param name="ability3"></param>
        /// <returns></returns>
        public static int GetClassNameId(sbyte ability1, sbyte ability2, sbyte ability3)
        {
            if ((ability1 == 0) || (ability2 == 0) || (ability3 == 0))
                return -1;

            var abilityList = new List<sbyte>() { (sbyte)(ability1 - 1), (sbyte)(ability2 - 1), (sbyte)(ability3 - 1)};
            abilityList.Sort();

            if (Lookup.Count <= 0)
                InitializeLookup();

            return Lookup.IndexOf((abilityList[0], abilityList[1], abilityList[2]));
        }

        public static string GetClassName(sbyte ability1, sbyte ability2, sbyte ability3)
        {
            var classNameOffset = AbilityNames.GetClassNameId(ability1, ability2, ability3);
            return classNameOffset >= 0
            ? Data.Server.GetText("ui_texts", "text", 1504 + classNameOffset, "???", true)
            : "Novice";
        }
    }
}
