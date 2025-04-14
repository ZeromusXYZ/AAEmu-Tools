using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AAEmu.DBEditor.tools.ahbot;

public class TreeNodeForAhCategory : TreeNode
{
    public long ThisKey => MakeKey(CategoryA, CategoryB, CategoryC);
    public TreeNodeForAhCategory ParentCategory { get; set; }
    public long CategoryA { get; set; }
    public long CategoryB { get; set; }
    public long CategoryC { get; set; }

    public static long MakeKey(long? a, long? b, long? c)
    {
        return (a ?? 0) + (0x100 * b ?? 0) + (0x10000 * c ?? 0);
    }

    public string GetUiTextKey()
    {
        var res = string.Empty;
        if (CategoryA > 0 && CategoryB == 0 && CategoryC == 0)
        {
            switch (CategoryA)
            {
                case 1: res = "item_group_weapon"; break; // Weapon "weapon"
                case 2: res = "item_group_armor"; break; // Armor "armor"
                case 3: res = "item_group_accessory"; break; // Accessory "accessory"
                case 4: res = "item_category_instrument"; break; // Instrument "instrument"
                case 5: res = "item_category_armor"; break; // Costume "costume"
                case 6: res = "item_group_consume"; break; // Consumables "consumable"
                case 7: res = "item_group_craft"; break; // Crafting "craft"
                case 8: res = "item_category_machine"; break; // Machining "machine"
                case 9: res = "item_group_vehicle"; break; // Pets "pet"
                case 10: res = "item_group_etc"; break; // Other "etc."
                case 11: res = "item_group_camp"; break; // Scroll: Siege Base "declare_siege"
                case 12: res = "item_category_crescent_stone"; break; // Lunagem
                case 13: res = "item_category_moon_stone"; break; // Lunastone
            }
        }
        if (CategoryA > 0 && CategoryB > 0 && CategoryC == 0)
        {
            switch (CategoryB)
            {
                // Cat A1 - Weapons
                case 1: res = "item_category_one_hand"; break; // 1H Weapon "one_handed"
                case 2: res = "item_category_two_hand"; break; // 2H Weapon "two_handed"
                case 3: res = "item_category_ranged"; break; // Ranged Weapon "ranged"
                case 7: res = "item_category_shield"; break; // Shield "shield"
                // Cat A2 - Armor
                case 4: res = "item_category_light_armor"; break; // Cloth "cloth"
                case 5: res = "item_category_normal_armor"; break; // Leather "leather"
                case 6: res = "item_category_heavy_armor"; break; // Plate "plate"
                case 8: res = "item_category_cloak"; break; // Cloak "cape"
                // Cat A3 - Accessories
                case 9: res = "item_category_earring"; break; // Earrings "ear"
                case 10: res = "item_category_necklace"; break; // Necklace "neck"
                case 11: res = "item_category_ring"; break; // Ring "finger"
                // Cat A4 - Instrument
                case 12: res = "item_category_tube_inst"; break; // Flute "tube"
                case 13: res = "item_category_string_inst"; break; // Lute "string"
                case 14: res = "item_category_percssion_inst"; break; // Drums "percussion"
                // Cat A5 - Costume
                // Cat A6 - Consumables
                case 15: res = "item_category_potion"; break; // Potion "potion"
                case 16: res = "item_category_food"; break; // Food "food"
                case 17: res = "item_category_drink"; break; // Drink "drink"
                case 18: res = "item_category_tool"; break; // Tool "tool"
                case 19: res = "item_category_throw"; break; // Explosive "bomb"
                case 20: res = "item_category_moon_stone"; break; // Lunastone "moonstone"
                case 21: res = "item_category_recipe"; break; // Recipe "recipe"
                case 22: res = "item_category_magical_product"; break; // Talisman "alchemy"
                case 39: res = "item_category_crescent_stone"; break; // Lunagem "crescentstone"
                // Cat A7 - Crafting
                case 23: res = "item_category_archium"; break; // Archeum "archium" (maybe "item_group_archium"?)
                case 24: res = "item_group_raw_material"; break; // Raw Goods "primary_ingredient"
                case 25: res = "item_group_material"; break; // Materials "secondary_ingredient"
                case 26: res = "item_group_animal"; break; // Animals "animal"
                case 27: res = "item_group_plant"; break; // Plants "plant"
                case 28: res = "item_group_inner_design"; break; // Indoor Decor "house_deco"
                case 29: res = "item_group_book"; break; // Books "book"
                // Cat A8 - Machining
                case 30: res = "item_category_vehicle"; break; // Vehicle "vehicle"
                case 31: res = "item_category_ship"; break; // Ship "ship"
                case 32: res = "item_category_glider"; break; // Glider "glider"
                case 33: res = "item_category_siege"; break; // Siege Gear "siege"
                // Cat A9 - Pets
                case 34: res = "item_group_vehicle"; break; // Pets "mount" (same as CatA name?)
                case 35: res = "item_group_vehicleinfo"; break; // Pet Gear "mount_gear" (maybe "item_category_vehicle_equip"?)
                // Cat A10 - Other
                case 36: res = "item_group_quest"; break; // Quest Items "quest"
                case 37: res = "item_group_miscellaneous"; break; // Miscellaneous "junk"
                case 38: res = "item_category_coin"; break; // Coins "coin"
            }
        }
        else
        {
            //
        }

        return res;
    }
}