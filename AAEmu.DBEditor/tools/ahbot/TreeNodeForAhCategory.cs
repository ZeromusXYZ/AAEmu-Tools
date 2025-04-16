using System.Windows.Forms;
using AAEmu.DBEditor.data;
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
        if (CategoryC > 0)
        {
            switch (CategoryC)
            {
                // Cat B1 - 1H Weapons
                case 1: res = "item_category_dagger"; break; // Dagger "dagger"
                case 2: res = "item_category_sword"; break; // Sword "sword"
                case 3: res = "item_category_blade"; break; // Katana "blade"
                case 4: res = "item_category_spear"; break; // Shortspear "spear"
                case 5: res = "item_category_axe"; break; // Axe "axe"
                case 6: res = "item_category_mace"; break; // Club "mace"
                case 7: res = "item_category_staff"; break; // Scepter "staff"
                
                // Cat B2 - 2H Weapons
                case 8: res = "item_category_twohand_sword"; break; // Greatsword "sword"
                case 9: res = "item_category_twohand_blade"; break; // Nodachi "blade"
                case 10: res = "item_category_twohand_spear"; break; // Longspear "spear"
                case 11: res = "item_category_twohand_axe"; break; // Longspear "axe"
                case 12: res = "item_category_twohand_mace"; break; // Greatclub "mace"
                case 13: res = "item_category_twohand_staff"; break; // Staff "staff"

                // Cat B3 - Ranged Weapons
                case 14: res = "item_category_bow"; break; // Bow "bow"
                case 80: res = "item_category_shotgun"; break; // Shotgun

                // Cat B4 - Cloth, B5 - Leather, B6 - Plate
                case 15: // Head "cloth_head"
                case 22: // Head "leather_head"
                case 29: // Head "plate_head"
                    res = "item_category_head"; 
                    break;
                case 16: // Chest "cloth_chest"
                case 23: // Chest "leather_chest"
                case 30: // Chest "plate_chest"
                    res = "item_category_chest";
                    break;
                case 17: // Waist "cloth_waist"
                case 24: // Waist "leather_waist"
                case 31: // Waist "plate_waist"
                    res = "item_category_waist";
                    break;
                case 18: // Wrists "cloth_arms"
                case 25: // Wrists "leather_arms"
                case 32: // Wrists "plate_arms"
                    res = "item_category_arms";
                    break;
                case 19: // Hands "cloth_hands"
                case 26: // Hands "leather_hands"
                case 33: // Hands "plate_hands"
                    res = "item_category_hands";
                    break;
                case 20: // Legs "cloth_legs"
                case 27: // Legs "leather_legs"
                case 34: // Legs "plate_legs"
                    res = "item_category_legs";
                    break;
                case 21: // Feet "cloth_feet"
                case 28: // Feet "leather_feet"
                case 35: // Feet "plate_feet"
                    res = "item_category_feet";
                    break;

                // Cat B24 - Raw Goods (primary_ingredient)
                case 36: res = "item_category_ore"; break; // Ore "bow"
                case 37: res = "item_category_wood"; break; // Wood "wood"
                case 38: res = "item_category_rock"; break; // Raw stone "rock"
                case 39: res = "item_category_rawhide"; break; // Pelt "leather"
                case 40: res = "item_category_fiber"; break; // Textile "fur"
                case 41: res = "item_category_parts"; break; // Parts "machine_part"
                case 42: res = "item_category_meat"; break; // Meat "meat"
                case 43: res = "item_category_marine_product"; break; // Seafood "aquatic"
                case 44: res = "item_category_grain"; break; // Grain "grain"
                case 45: res = "item_category_vegetables"; break; // Vegetables "vegetable"
                case 46: res = "item_category_fruit"; break; // Fruit "fruit"
                case 47: res = "item_category_spice"; break; // Spice "spice"
                case 48: res = "item_category_drug_material"; break; // Herb "medicine"
                case 49: res = "item_category_flower"; break; // Flower "flower"
                case 50: res = "item_category_soil"; break; // Soil "soil"
                case 51: res = "item_category_jewel"; break; // Gem "gem"

                // Cat B25 - Materials (secondary_ingredient)
                case 52: res = "item_category_paper"; break; // Paper "paper"
                case 53: res = "item_category_metal"; break; // Metal "metal"
                case 54: res = "item_category_wood"; break; // Lumber "wood"
                case 55: res = "item_category_stone"; break; // Stone Brick "stone"
                case 56: res = "item_category_leather"; break; // Hide "hide"
                case 57: res = "item_category_cloth"; break; // Fabric "cloth"
                case 58: res = "item_category_machine"; break; // Machine "machine"
                case 59: res = "item_category_glass"; break; // Glass "glass"
                case 60: res = "item_category_rubber"; break; // Rubber "rubber"
                case 61: res = "item_category_noble_metal"; break; // Precious Metal "jewel"
                case 62: res = "item_category_alchemy_material"; break; // Alchemy "alchemy"
                case 63: res = "item_category_craft_material"; break; // Crafting "misc"
                case 70: res = "item_category_dye"; break; // Dye "dye"
                case 71: res = "item_category_cooking_oil"; break; // Cooking oil "cooking_oil"
                case 72: res = "item_category_seasoning"; break; // Spice "seasoning"
                
                // Cat B26 - Animals
                case 64: res = "item_category_animal"; break; // Livestock "livestock"

                // Cat B27 - // Plants
                case 65: res = "item_category_young_plant"; break; // Sapling "sappling"
                case 66: res = "item_category_seed"; break; // Seed "seed"

                // Cat B28 - // Indoor Decor
                case 67: res = "item_category_furniture"; break; // Furniture "furniture"

                // Cat B36 - // Quest Items
                case 68: res = "item_category_adventure"; break; // Adventure "adventure"

                // Cat B37 - // Miscellaneous
                case 69: res = "item_category_toy"; break; // Toy "toy"
                
                // Cat B47 - item_category_moon_stone_scale
                case 73: res = "item_category_moon_stone_scale_red"; break;
                case 74: res = "item_category_moon_stone_scale_yellow"; break;
                case 75: res = "item_category_moon_stone_scale_green"; break;
                case 76: res = "item_category_moon_stone_scale_blue"; break;
                case 77: res = "item_category_moon_stone_scale_purple"; break;
                
                // Cat B47 - item_category_moon_stone_scale
                case 78: res = "item_category_moon_stone_shadow_craft"; break;
                case 79: res = "item_category_moon_stone_shadow_honor"; break;
            }
            return res;
        }

        if (CategoryB > 0)
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
                case 54: res = "item_category_armor"; break;
                case 55: res = "item_category_evolving_armor"; break;
                case 56: res = "item_category_underpants"; break;

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
                case 34: // Pets "mount" // item_group_vehicle or item_category_monut_pet
                    // At some point in development, the original ui_texts key was changed for this group to be able
                    // to account for pets and battle pets being separate
                    res = Data.Server.UiTextExists("item_category_monut_pet") ? "item_category_monut_pet" : "item_group_vehicle";
                    break; 
                case 35: res = "item_group_vehicleinfo"; break; // Pet Gear "mount_gear" (maybe "item_category_vehicle_equip"?)
                case 57: res = "item_category_battle_pet"; break; // Battle Pets

                // Cat A10 - Other
                case 36: res = "item_group_quest"; break; // Quest Items "quest"
                case 37: res = "item_group_miscellaneous"; break; // Miscellaneous "junk"
                case 38: res = "item_category_coin"; break; // Coins "coin"
                case 58: res = "item_category_etc_emotion"; break; // Emotes
                case 59: res = "item_category_etc_score"; break;
                
                // Cat A12 - Lunagems
                case 40: res = "item_category_crescent_stone_red"; break;
                case 41: res = "item_category_crescent_stone_yellow"; break;
                case 42: res = "item_category_crescent_stone_green"; break;
                case 43: res = "item_category_crescent_stone_blue"; break;
                case 44: res = "item_category_crescent_stone_ability1"; break;
                case 45: res = "item_category_crescent_stone_ability2"; break;
                case 46: res = "item_category_crescent_stone_etc"; break;
                case 60: res = "item_category_crescent_stone_white"; break;

                // Cat A13 - Lunastones
                case 47: res = "item_category_moon_stone_scale"; break;
                case 48: res = "item_category_moon_stone_shadow"; break;
                case 49: res = "item_category_moon_stone_etc"; break;
                // case 50:
                // case 51:
                // case 52:
                // case 53:
                //     break;
            }

            return res;
        }
        
        if (CategoryA > 0)
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
                // 14
                case 15: res = "item_category_special"; break;
            }
            return res;
        }

        return res;
    }
}