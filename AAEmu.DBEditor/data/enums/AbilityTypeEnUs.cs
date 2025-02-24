using AAEmu.DBEditor.data.gamedb;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AAEmu.DBEditor.data.enums
{
    public enum AbilityTypeEnUs : byte
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
        Malediction = 11, // Hatred
        Swiftblade = 12, // Assassin
        Gunslinger = 13, // Madness
        Spelldancer = 14, // Pleasure
        // Ravager = ??, // Predator (Warborn Transformation)
        // Juggernaut = ??, // Trooper (Dwarf Transformation)
        None1250 = 11,
        None3030 = 13,
        None5070 = 30,
        None = 30,
    }
}
