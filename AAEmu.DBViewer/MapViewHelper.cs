using AAEmu.DBViewer.DbDefs;
using AAPacker;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Xml;

namespace AAEmu.DBViewer
{
    public static class MapViewHelper
    {
        public static List<MapViewImageRef> MapRefs = new List<MapViewImageRef>();
        public static List<MapViewMiniMapRef> MiniMapRefs = new List<MapViewMiniMapRef>();
        public static Dictionary<string, string> GFileVars = new Dictionary<string, string>();
        public static int GFileAreaIdCounter = 0;

        public static void AddRef(MapLevel level, int id, string baseFileName, int image_map)
        {
            var newRef = new MapViewImageRef();
            newRef.Level = level;
            newRef.Id = id;
            newRef.ImageId = image_map;
            newRef.BaseFileName = baseFileName;
            newRef.MapRect = new RectangleF();

            MapRefs.Add(newRef);
        }

        public static void AddMiniMapRef(MapLevel level, int zoneGroupId, int mapScale, string baseMapFileName, int offsetX, int offsetY, int imgX, int imgY, int imgW, int imgH)
        {
            var newRef = new MapViewMiniMapRef();
            newRef.Level = level;
            newRef.ZoneGroup = zoneGroupId;
            newRef.Scale = mapScale;
            newRef.BaseFileName = baseMapFileName; // + "_road_" + mapScale.ToString() + ".dds";
            newRef.Offset = new PointF(offsetX, offsetY);
            newRef.Rect = new RectangleF(imgX, imgY, imgW, imgH);
            MiniMapRefs.Add(newRef);
        }


        public static MapViewImageRef GetRefByImageMapId(long image_map)
        {
            foreach (var r in MapRefs)
                if (r.ImageId == image_map)
                    return r;
            return null;
        }

        public static MapViewMiniMapRef GetMiniMapRefByZoneGroup(long zonegroup_id, int scale, MapLevel level)
        {
            foreach (var r in MiniMapRefs)
                if ((r.ZoneGroup == zonegroup_id) && (r.Scale == scale) && (r.Level == level))
                    return r;
            return null;
        }

        public static void PopulateList()
        {
            MapRefs.Clear();

            // For now this is not needed as they filename are mostly the same as the DB names anyway
            AddRef(MapLevel.WorldMap, 2, "main_world", 1);

            AddRef(MapLevel.Continent, 3, "land_west", 1001);
            AddRef(MapLevel.Continent, 4, "land_east", 1002);
            AddRef(MapLevel.Continent, 5, "land_origin", 1003);
            AddRef(MapLevel.Continent, 6, "land_silent_sea", 1004);
            AddRef(MapLevel.Continent, 39, "s_gold_sea", 4001);
            AddRef(MapLevel.Continent, 30, "s_lost_road_sea", 4002);
            AddRef(MapLevel.Continent, 40, "s_crescent_moon_sea", 4003);

            AddRef(MapLevel.Zone, 31, "instance_training_camp", 5001);
            AddRef(MapLevel.Zone, 45, "instance_burntcastle_armory", 6001);
            AddRef(MapLevel.Zone, 46, "instance_hadir_farm", 6002);
            AddRef(MapLevel.Zone, 47, "instance_sal_temple", 6003);
            AddRef(MapLevel.Zone, 50, "instance_cuttingwind_deadmine", 6004);
            AddRef(MapLevel.Zone, 51, "instance_howling_abyss", 6005);
            AddRef(MapLevel.Zone, 58, "instance_howling_abyss", 6007);
            AddRef(MapLevel.Zone, 52, "instance_cradle_of_destruction", 6006);
            AddRef(MapLevel.Zone, 49, "arche_mall", 7001);

            AddRef(MapLevel.City, 1063, "instance_nachashgar_room_01", 60081);
            AddRef(MapLevel.City, 1064, "instance_nachashgar_room_02", 60082);
            AddRef(MapLevel.City, 1065, "instance_nachashgar_room_03", 60083);
            AddRef(MapLevel.City, 1066, "instance_nachashgar_room_04", 60084);
            AddRef(MapLevel.City, 1067, "instance_nachashgar_room_05", 60085);
            AddRef(MapLevel.City, 1068, "instance_nachashgar_room_06", 60086);
            AddRef(MapLevel.City, 1069, "instance_nachashgar_room_07", 60087);
            AddRef(MapLevel.City, 1070, "instance_nachashgar_room_08", 60088);
            AddRef(MapLevel.City, 1071, "instance_nachashgar_room_09_larman", 60089);
            AddRef(MapLevel.City, 1084, "instance_nachashgar_room_09_carmilla", 60089);
            AddRef(MapLevel.City, 1085, "instance_nachashgar_room_09_anerin", 60089);
            AddRef(MapLevel.City, 1081, "instance_nachashgar_room_10", 600810);

            AddRef(MapLevel.Zone, 62, "instance_immortal_isle", 6009);
            AddRef(MapLevel.Zone, 64, "instance_immortal_isle", 6010);
            AddRef(MapLevel.Zone, 70, "instance_library_1", 6011);
            AddRef(MapLevel.Zone, 71, "instance_library_2", 6012);
            AddRef(MapLevel.Zone, 72, "instance_library_3", 6013);

            AddRef(MapLevel.City, 1101, "instance_library_boss_1_bossroom", 60141);
            AddRef(MapLevel.City, 1105, "instance_library_boss_1_waitingroom", 60142);
            AddRef(MapLevel.City, 1102, "instance_library_boss_2_bossroom", 60151);
            AddRef(MapLevel.City, 1106, "instance_library_boss_2_waitingroom", 60152);
            AddRef(MapLevel.City, 1103, "instance_library_boss_3_bossroom", 60161);
            AddRef(MapLevel.City, 1107, "instance_library_boss_3_waitingroom", 60162);
            AddRef(MapLevel.City, 1104, "instance_library_towerdefense_bossroom", 60171);
            AddRef(MapLevel.City, 1108, "instance_library_towerdefense_waitingroom", 60172);

            AddRef(MapLevel.Zone, 1, "w_gweonid_forest", 2101);
            AddRef(MapLevel.Zone, 27, "w_long_sand", 2111);
            AddRef(MapLevel.Zone, 5, "w_solzreed", 2102);
            AddRef(MapLevel.Zone, 6, "w_lilyut_meadow", 2103);
            AddRef(MapLevel.Zone, 3, "w_garangdol_plains", 2104);
            AddRef(MapLevel.Zone, 18, "w_white_forest", 2105);
            AddRef(MapLevel.Zone, 2, "w_marianople", 2106);
            AddRef(MapLevel.Zone, 8, "w_two_crowns", 2107);
            AddRef(MapLevel.Zone, 20, "w_cross_plains", 2108);
            AddRef(MapLevel.Zone, 22, "w_golden_plains", 2109);
            AddRef(MapLevel.Zone, 26, "w_hell_swamp", 2110);
            AddRef(MapLevel.Zone, 19, "w_the_carcass", 2112);
            AddRef(MapLevel.Zone, 11, "e_falcony_plateau", 2201);
            AddRef(MapLevel.Zone, 24, "e_tiger_spine_mountains", 2202);
            AddRef(MapLevel.Zone, 9, "e_mahadevi", 2203);
            AddRef(MapLevel.Zone, 4, "e_sunrise_peninsula", 2204);
            AddRef(MapLevel.Zone, 25, "e_ancient_forest", 2206);
            AddRef(MapLevel.Zone, 12, "e_singing_land", 2205);
            AddRef(MapLevel.Zone, 7, "e_rainbow_field", 2212);
            AddRef(MapLevel.Zone, 17, "e_ynystere", 2207);
            AddRef(MapLevel.Zone, 14, "e_steppe_belt", 2209);
            AddRef(MapLevel.Zone, 16, "e_lokas_checkers", 2208);
            AddRef(MapLevel.Zone, 15, "e_ruins_of_hariharalaya", 2210);
            AddRef(MapLevel.Zone, 23, "e_hasla", 2211);
            AddRef(MapLevel.Zone, 33, "o_salpimari", 3001);
            AddRef(MapLevel.Zone, 34, "o_nuimari", 3002);
            AddRef(MapLevel.Zone, 43, "o_seonyeokmari", 3003);
            AddRef(MapLevel.Zone, 44, "o_rest_land", 3004);
            AddRef(MapLevel.Zone, 54, "o_abyss_gate", 3005);
            AddRef(MapLevel.Zone, 56, "o_land_of_sunlights", 3006);
            AddRef(MapLevel.Zone, 67, "o_library_1", 3007);
            AddRef(MapLevel.Zone, 65, "o_library_2", 3008);
            AddRef(MapLevel.Zone, 69, "o_library_3", 3009);
            AddRef(MapLevel.Zone, 61, "o_shining_shore", 3010);
            AddRef(MapLevel.Zone, 39, "s_gold_sea", 40011);
            AddRef(MapLevel.Zone, 30, "s_lost_road_sea", 40021);
            AddRef(MapLevel.Zone, 40, "s_crescent_moon_sea", 40031);
            AddRef(MapLevel.Zone, 36, "s_silent_sea", 8001);
            AddRef(MapLevel.Zone, 59, "s_freedom_island", 8002);

            AddRef(MapLevel.City, 866, "s_freedom_island_detail", 80021);

            AddRef(MapLevel.Zone, 60, "s_pirate_island", 8003);

            AddRef(MapLevel.City, 14, "w_gweonid_forest_city", 21011);
            AddRef(MapLevel.City, 335, "w_solzreed_city", 21021);
            AddRef(MapLevel.City, 37, "w_marianople_city", 21061);
            AddRef(MapLevel.City, 144, "w_two_crowns_city", 21071);
            AddRef(MapLevel.City, 252, "w_lilyut_meadow_west_ronbann_mine", 21031);
            AddRef(MapLevel.City, 253, "w_lilyut_meadow_east_ronbann_mine", 21032);
            AddRef(MapLevel.City, 155, "w_white_forest_red_moss_cave", 21051);
            AddRef(MapLevel.City, 278, "e_mahadevi_city", 22031);
            AddRef(MapLevel.City, 578, "e_sunrise_peninsula_city", 22041);
            AddRef(MapLevel.City, 542, "e_singing_land_city", 22051);
            AddRef(MapLevel.City, 468, "e_ynystere_city", 22071);
            AddRef(MapLevel.City, 996, "e_mahadevi_astra_cave", 22032);
            AddRef(MapLevel.City, 688, "e_tiger_spine_mountains_kobold_cave", 22021);
            AddRef(MapLevel.City, 749, "e_falcony_plateau_misty_cave", 22011);
            AddRef(MapLevel.City, 735, "e_falcony_plateau_bat_cave", 22012);
            AddRef(MapLevel.City, 95, "e_steppe_belt_snowlion_rock", 22091);
            AddRef(MapLevel.City, 1057, "e_hasla_cemetery_underground", 22112);
            AddRef(MapLevel.City, 1075, "o_abyss_gate_ruin_of_vanishing_snake_1f", 30051);
            AddRef(MapLevel.City, 1076, "o_abyss_gate_ruin_of_vanishing_snake_2f", 30052);

            AddRef(MapLevel.Zone, 10, "w_bronze_rock", 2113);
            AddRef(MapLevel.Zone, 21, "w_cradle_of_genesis", 2114);
        }

        public static void PopulateMiniMapList()
        {
            // Some defaults for older client versions where we can't grab it from the .g files
            AddMiniMapRef(MapLevel.Zone, 31, 50, "i_training_camp", 119, 31, 0, 0, 232, 228);
            AddMiniMapRef(MapLevel.Zone, 31, 70, "i_training_camp", 167, 43, 0, 0, 324, 320);
            AddMiniMapRef(MapLevel.Zone, 31, 100, "i_training_camp", 238, 62, 0, 0, 464, 456);
            AddMiniMapRef(MapLevel.Zone, 45, 50, "i_burntcastle_armory", 140, 13, 0, 0, 164, 264);
            AddMiniMapRef(MapLevel.Zone, 45, 70, "i_burntcastle_armory", 193, 23, 0, 0, 240, 364);
            AddMiniMapRef(MapLevel.Zone, 45, 100, "i_burntcastle_armory", 273, 22, 0, 0, 344, 532);
            AddMiniMapRef(MapLevel.Zone, 46, 50, "i_hadir_farm", 26, 1, 0, 0, 396, 276);
            AddMiniMapRef(MapLevel.Zone, 46, 70, "i_hadir_farm", 36, 3, 0, 0, 556, 384);
            AddMiniMapRef(MapLevel.Zone, 46, 100, "i_hadir_farm", 64, 18, 0, 0, 752, 528);
            AddMiniMapRef(MapLevel.Zone, 47, 50, "i_sal_temple", 71, 27, 0, 0, 264, 240);
            AddMiniMapRef(MapLevel.Zone, 47, 70, "i_sal_temple", 99, 39, 0, 0, 340, 332);
            AddMiniMapRef(MapLevel.Zone, 47, 100, "i_sal_temple", 140, 52, 0, 0, 484, 472);
            AddMiniMapRef(MapLevel.Zone, 50, 50, "i_cuttingwind_deadmine", 99, 11, 0, 0, 248, 240);
            AddMiniMapRef(MapLevel.Zone, 50, 70, "i_cuttingwind_deadmine", 138, 15, 0, 0, 348, 336);
            AddMiniMapRef(MapLevel.Zone, 50, 100, "i_cuttingwind_deadmine", 198, 21, 0, 0, 496, 480);
            AddMiniMapRef(MapLevel.Zone, 49, 50, "i_arche_mall", 74, 6, 0, 0, 280, 272);
            AddMiniMapRef(MapLevel.Zone, 49, 70, "i_arche_mall", 104, 9, 0, 0, 388, 380);
            AddMiniMapRef(MapLevel.Zone, 49, 100, "i_arche_mall", 149, 12, 0, 0, 556, 544);
            AddMiniMapRef(MapLevel.Zone, 51, 50, "i_howling_abyss", 87, 23, 0, 0, 304, 236);
            AddMiniMapRef(MapLevel.Zone, 51, 70, "i_howling_abyss", 125, 31, 0, 0, 416, 328);
            AddMiniMapRef(MapLevel.Zone, 51, 100, "i_howling_abyss", 178, 45, 0, 0, 596, 468);
            AddMiniMapRef(MapLevel.Zone, 58, 50, "i_howling_abyss", 87, 23, 0, 0, 304, 236);
            AddMiniMapRef(MapLevel.Zone, 58, 70, "i_howling_abyss", 125, 31, 0, 0, 416, 328);
            AddMiniMapRef(MapLevel.Zone, 58, 100, "i_howling_abyss", 178, 45, 0, 0, 596, 468);
            AddMiniMapRef(MapLevel.Zone, 52, 50, "i_cradle_of_destruction", 76, 1, 0, 0, 340, 272);
            AddMiniMapRef(MapLevel.Zone, 52, 70, "i_cradle_of_destruction", 106, 2, 0, 0, 476, 380);
            AddMiniMapRef(MapLevel.Zone, 52, 100, "i_cradle_of_destruction", 151, 4, 0, 0, 680, 540);
            AddMiniMapRef(MapLevel.City, 1063, 50, "i_nachashgar_room_01", 156, 68, 0, 0, 152, 160);
            AddMiniMapRef(MapLevel.City, 1063, 70, "i_nachashgar_room_01", 218, 95, 0, 0, 212, 224);
            AddMiniMapRef(MapLevel.City, 1063, 100, "i_nachashgar_room_01", 312, 136, 0, 0, 300, 316);
            AddMiniMapRef(MapLevel.City, 1064, 50, "i_nachashgar_room_02", 165, 62, 0, 0, 136, 164);
            AddMiniMapRef(MapLevel.City, 1064, 70, "i_nachashgar_room_02", 230, 86, 0, 0, 192, 232);
            AddMiniMapRef(MapLevel.City, 1064, 100, "i_nachashgar_room_02", 329, 123, 0, 0, 272, 332);
            AddMiniMapRef(MapLevel.City, 1065, 50, "i_nachashgar_room_03", 159, 79, 0, 0, 128, 144);
            AddMiniMapRef(MapLevel.City, 1065, 70, "i_nachashgar_room_03", 223, 110, 0, 0, 180, 200);
            AddMiniMapRef(MapLevel.City, 1065, 100, "i_nachashgar_room_03", 318, 157, 0, 0, 256, 288);
            AddMiniMapRef(MapLevel.City, 1066, 50, "i_nachashgar_room_04", 167, 83, 0, 0, 124, 132);
            AddMiniMapRef(MapLevel.City, 1066, 70, "i_nachashgar_room_04", 234, 115, 0, 0, 172, 188);
            AddMiniMapRef(MapLevel.City, 1066, 100, "i_nachashgar_room_04", 334, 165, 0, 0, 244, 264);
            AddMiniMapRef(MapLevel.City, 1067, 50, "i_nachashgar_room_05", 159, 75, 0, 0, 128, 152);
            AddMiniMapRef(MapLevel.City, 1067, 70, "i_nachashgar_room_05", 223, 105, 0, 0, 180, 208);
            AddMiniMapRef(MapLevel.City, 1067, 100, "i_nachashgar_room_05", 318, 150, 0, 0, 256, 300);
            AddMiniMapRef(MapLevel.City, 1068, 50, "i_nachashgar_room_06", 173, 66, 0, 0, 132, 148);
            AddMiniMapRef(MapLevel.City, 1068, 70, "i_nachashgar_room_06", 242, 92, 0, 0, 184, 208);
            AddMiniMapRef(MapLevel.City, 1068, 100, "i_nachashgar_room_06", 346, 132, 0, 0, 264, 296);
            AddMiniMapRef(MapLevel.City, 1069, 50, "i_nachashgar_room_07", 145, 79, 0, 0, 168, 152);
            AddMiniMapRef(MapLevel.City, 1069, 70, "i_nachashgar_room_07", 202, 111, 0, 0, 236, 208);
            AddMiniMapRef(MapLevel.City, 1069, 100, "i_nachashgar_room_07", 287, 158, 0, 0, 332, 300);
            AddMiniMapRef(MapLevel.City, 1070, 50, "i_nachashgar_room_08", 137, 80, 0, 0, 176, 148);
            AddMiniMapRef(MapLevel.City, 1070, 70, "i_nachashgar_room_08", 192, 111, 0, 0, 244, 204);
            AddMiniMapRef(MapLevel.City, 1070, 100, "i_nachashgar_room_08", 274, 159, 0, 0, 348, 292);
            AddMiniMapRef(MapLevel.City, 1071, 50, "i_nachashgar_room_09_larman", 160, 73, 0, 0, 144, 132);
            AddMiniMapRef(MapLevel.City, 1071, 70, "i_nachashgar_room_09_larman", 224, 101, 0, 0, 200, 184);
            AddMiniMapRef(MapLevel.City, 1071, 100, "i_nachashgar_room_09_larman", 320, 145, 0, 0, 284, 260);
            AddMiniMapRef(MapLevel.City, 1084, 50, "i_nachashgar_room_09_carmilla", 160, 72, 0, 0, 144, 132);
            AddMiniMapRef(MapLevel.City, 1084, 70, "i_nachashgar_room_09_carmilla", 224, 100, 0, 0, 200, 184);
            AddMiniMapRef(MapLevel.City, 1084, 100, "i_nachashgar_room_09_carmilla", 317, 144, 0, 0, 284, 260);
            AddMiniMapRef(MapLevel.City, 1085, 50, "i_nachashgar_room_09_anerin", 164, 75, 0, 0, 144, 132);
            AddMiniMapRef(MapLevel.City, 1085, 70, "i_nachashgar_room_09_anerin", 229, 105, 0, 0, 200, 184);
            AddMiniMapRef(MapLevel.City, 1085, 100, "i_nachashgar_room_09_anerin", 328, 151, 0, 0, 284, 260);
            AddMiniMapRef(MapLevel.City, 1081, 50, "i_nachashgar_room_10", 158, 65, 0, 0, 156, 144);
            AddMiniMapRef(MapLevel.City, 1081, 70, "i_nachashgar_room_10", 221, 90, 0, 0, 216, 204);
            AddMiniMapRef(MapLevel.City, 1081, 100, "i_nachashgar_room_10", 314, 130, 0, 0, 308, 288);
            AddMiniMapRef(MapLevel.Zone, 62, 50, "i_immortal_isle", 136, 37, 0, 0, 200, 208);
            AddMiniMapRef(MapLevel.Zone, 62, 70, "i_immortal_isle", 190, 51, 0, 0, 282, 292);
            AddMiniMapRef(MapLevel.Zone, 62, 100, "i_immortal_isle", 273, 73, 0, 0, 396, 416);
            AddMiniMapRef(MapLevel.Zone, 64, 50, "i_immortal_isle", 136, 37, 0, 0, 200, 208);
            AddMiniMapRef(MapLevel.Zone, 64, 70, "i_immortal_isle", 190, 51, 0, 0, 282, 292);
            AddMiniMapRef(MapLevel.Zone, 64, 100, "i_immortal_isle", 273, 73, 0, 0, 396, 416);
            AddMiniMapRef(MapLevel.Zone, 70, 50, "instance_library_1", 125, 19, 0, 0, 240, 240);
            AddMiniMapRef(MapLevel.Zone, 70, 70, "instance_library_1", 175, 27, 0, 0, 336, 336);
            AddMiniMapRef(MapLevel.Zone, 70, 100, "instance_library_1", 252, 40, 0, 0, 476, 476);
            AddMiniMapRef(MapLevel.Zone, 71, 50, "instance_library_2", 125, 19, 0, 0, 240, 240);
            AddMiniMapRef(MapLevel.Zone, 71, 70, "instance_library_2", 175, 27, 0, 0, 336, 336);
            AddMiniMapRef(MapLevel.Zone, 71, 100, "instance_library_2", 252, 40, 0, 0, 476, 476);
            AddMiniMapRef(MapLevel.Zone, 72, 50, "instance_library_3", 125, 19, 0, 0, 240, 240);
            AddMiniMapRef(MapLevel.Zone, 72, 70, "instance_library_3", 175, 27, 0, 0, 336, 336);
            AddMiniMapRef(MapLevel.Zone, 72, 100, "instance_library_3", 252, 40, 0, 0, 476, 476);
            AddMiniMapRef(MapLevel.City, 1101, 50, "instance_library_boss_1_bossroom", 143, 49, 0, 0, 184, 184);
            AddMiniMapRef(MapLevel.City, 1101, 70, "instance_library_boss_1_bossroom", 202, 71, 0, 0, 252, 252);
            AddMiniMapRef(MapLevel.City, 1101, 100, "instance_library_boss_1_bossroom", 287, 100, 0, 0, 364, 360);
            AddMiniMapRef(MapLevel.City, 1105, 50, "instance_library_boss_1_waitingroom", 143, 53, 0, 0, 176, 176);
            AddMiniMapRef(MapLevel.City, 1105, 70, "instance_library_boss_1_waitingroom", 202, 75, 0, 0, 244, 244);
            AddMiniMapRef(MapLevel.City, 1105, 100, "instance_library_boss_1_waitingroom", 288, 107, 0, 0, 348, 348);
            AddMiniMapRef(MapLevel.City, 1102, 50, "instance_library_boss_2_bossroom", 134, 27, 0, 0, 212, 232);
            AddMiniMapRef(MapLevel.City, 1102, 70, "instance_library_boss_2_bossroom", 188, 38, 0, 0, 296, 324);
            AddMiniMapRef(MapLevel.City, 1102, 100, "instance_library_boss_2_bossroom", 268, 54, 0, 0, 420, 460);
            AddMiniMapRef(MapLevel.City, 1106, 50, "instance_library_boss_2_waitingroom", 149, 53, 0, 0, 180, 176);
            AddMiniMapRef(MapLevel.City, 1106, 70, "instance_library_boss_2_waitingroom", 209, 74, 0, 0, 252, 248);
            AddMiniMapRef(MapLevel.City, 1106, 100, "instance_library_boss_2_waitingroom", 298, 106, 0, 0, 364, 356);
            AddMiniMapRef(MapLevel.City, 1103, 50, "instance_library_boss_3_bossroom", 106, 17, 0, 0, 260, 236);
            AddMiniMapRef(MapLevel.City, 1103, 70, "instance_library_boss_3_bossroom", 148, 24, 0, 0, 364, 332);
            AddMiniMapRef(MapLevel.City, 1103, 100, "instance_library_boss_3_bossroom", 211, 34, 0, 0, 520, 472);
            AddMiniMapRef(MapLevel.City, 1107, 50, "instance_library_boss_3_waitingroom", 155, 58, 0, 0, 172, 172);
            AddMiniMapRef(MapLevel.City, 1107, 70, "instance_library_boss_3_waitingroom", 218, 83, 0, 0, 240, 236);
            AddMiniMapRef(MapLevel.City, 1107, 100, "instance_library_boss_3_waitingroom", 313, 119, 0, 0, 340, 336);
            AddMiniMapRef(MapLevel.City, 1104, 50, "instance_library_towerdefense_bossroom", 136, 39, 0, 0, 204, 204);
            AddMiniMapRef(MapLevel.City, 1104, 70, "instance_library_towerdefense_bossroom", 190, 54, 0, 0, 284, 284);
            AddMiniMapRef(MapLevel.City, 1104, 100, "instance_library_towerdefense_bossroom", 272, 77, 0, 0, 408, 404);
            AddMiniMapRef(MapLevel.City, 1108, 50, "instance_library_towerdefense_waitingroom", 135, 38, 0, 0, 204, 204);
            AddMiniMapRef(MapLevel.City, 1108, 70, "instance_library_towerdefense_waitingroom", 189, 52, 0, 0, 288, 288);
            AddMiniMapRef(MapLevel.City, 1108, 100, "instance_library_towerdefense_waitingroom", 270, 75, 0, 0, 412, 416);
            AddMiniMapRef(MapLevel.Zone, 1, 50, "w_gweonid_forest", 92, 21, 0, 0, 364, 244);
            AddMiniMapRef(MapLevel.Zone, 1, 70, "w_gweonid_forest", 127, 0, 0, 0, 480, 388);
            AddMiniMapRef(MapLevel.Zone, 1, 100, "w_gweonid_forest", 182, 50, 0, 0, 672, 440);
            AddMiniMapRef(MapLevel.Zone, 5, 50, "w_solzreed", 24, 3, 0, 0, 388, 256);
            AddMiniMapRef(MapLevel.Zone, 5, 70, "w_solzreed", 36, 4, 0, 0, 540, 360);
            AddMiniMapRef(MapLevel.Zone, 5, 100, "w_solzreed", 43, 6, 0, 0, 780, 532);
            AddMiniMapRef(MapLevel.Zone, 6, 50, "w_lilyut_meadow", 50, 0, 0, 0, 392, 268);
            AddMiniMapRef(MapLevel.Zone, 6, 70, "w_lilyut_meadow", 67, 0, 0, 0, 548, 376);
            AddMiniMapRef(MapLevel.Zone, 6, 100, "w_lilyut_meadow", 68, 0, 0, 0, 804, 516);
            AddMiniMapRef(MapLevel.Zone, 3, 50, "w_garangdol_plains", 73, 0, 0, 0, 304, 280);
            AddMiniMapRef(MapLevel.Zone, 3, 70, "w_garangdol_plains", 100, 0, 0, 0, 428, 392);
            AddMiniMapRef(MapLevel.Zone, 3, 100, "w_garangdol_plains", 143, 0, 0, 0, 612, 556);
            AddMiniMapRef(MapLevel.Zone, 18, 50, "w_white_forest", 61, 0, 0, 0, 332, 248);
            AddMiniMapRef(MapLevel.Zone, 18, 70, "w_white_forest", 85, 0, 0, 0, 464, 348);
            AddMiniMapRef(MapLevel.Zone, 18, 100, "w_white_forest", 122, 9, 0, 0, 688, 488);
            AddMiniMapRef(MapLevel.Zone, 2, 50, "w_marianople", 44, 7, 0, 0, 352, 260);
            AddMiniMapRef(MapLevel.Zone, 2, 70, "w_marianople", 60, 10, 0, 0, 496, 364);
            AddMiniMapRef(MapLevel.Zone, 2, 100, "w_marianople", 86, 14, 0, 0, 708, 520);
            AddMiniMapRef(MapLevel.Zone, 8, 50, "w_two_crowns", 111, 5, 0, 0, 256, 268);
            AddMiniMapRef(MapLevel.Zone, 8, 70, "w_two_crowns", 156, 9, 0, 0, 356, 372);
            AddMiniMapRef(MapLevel.Zone, 8, 100, "w_two_crowns", 223, 12, 0, 0, 508, 532);
            AddMiniMapRef(MapLevel.Zone, 20, 50, "w_cross_plains", 79, 14, 0, 0, 372, 260);
            AddMiniMapRef(MapLevel.Zone, 20, 70, "w_cross_plains", 106, 24, 0, 0, 528, 360);
            AddMiniMapRef(MapLevel.Zone, 20, 100, "w_cross_plains", 152, 36, 0, 0, 712, 512);
            AddMiniMapRef(MapLevel.Zone, 22, 50, "w_golden_plains", 22, 19, 0, 0, 444, 248);
            AddMiniMapRef(MapLevel.Zone, 22, 70, "w_golden_plains", 42, 41, 0, 0, 608, 332);
            AddMiniMapRef(MapLevel.Zone, 22, 100, "w_golden_plains", 68, 60, 0, 0, 860, 460);
            AddMiniMapRef(MapLevel.Zone, 26, 50, "w_hell_swamp", 134, 0, 0, 0, 208, 276);
            AddMiniMapRef(MapLevel.Zone, 26, 70, "w_hell_swamp", 189, 1, 0, 0, 328, 380);
            AddMiniMapRef(MapLevel.Zone, 26, 100, "w_hell_swamp", 265, 2, 0, 0, 420, 548);
            AddMiniMapRef(MapLevel.Zone, 27, 50, "w_long_sand", 69, 11, 0, 0, 328, 268);
            AddMiniMapRef(MapLevel.Zone, 27, 70, "w_long_sand", 107, 14, 0, 0, 440, 376);
            AddMiniMapRef(MapLevel.Zone, 27, 100, "w_long_sand", 133, 23, 0, 0, 648, 524);
            AddMiniMapRef(MapLevel.Zone, 19, 50, "w_the_carcass", 25, 0, 0, 0, 440, 288);
            AddMiniMapRef(MapLevel.Zone, 19, 70, "w_the_carcass", 38, 0, 0, 0, 612, 400);
            AddMiniMapRef(MapLevel.Zone, 19, 100, "w_the_carcass", 57, 0, 0, 0, 804, 556);
            AddMiniMapRef(MapLevel.Zone, 25, 50, "e_ancient_forest", 23, 0, 0, 0, 384, 244);
            AddMiniMapRef(MapLevel.Zone, 25, 70, "e_ancient_forest", 30, 0, 0, 0, 524, 340);
            AddMiniMapRef(MapLevel.Zone, 25, 100, "e_ancient_forest", 109, 14, 0, 0, 680, 472);
            AddMiniMapRef(MapLevel.Zone, 11, 50, "e_falcony_plateau", 97, 17, 0, 0, 224, 248);
            AddMiniMapRef(MapLevel.Zone, 11, 70, "e_falcony_plateau", 136, 13, 0, 0, 312, 360);
            AddMiniMapRef(MapLevel.Zone, 11, 100, "e_falcony_plateau", 194, 45, 0, 0, 444, 472);
            AddMiniMapRef(MapLevel.Zone, 9, 50, "e_mahadevi", 39, 22, 0, 0, 356, 256);
            AddMiniMapRef(MapLevel.Zone, 9, 70, "e_mahadevi", 55, 1, 0, 0, 496, 388);
            AddMiniMapRef(MapLevel.Zone, 9, 100, "e_mahadevi", 79, 47, 0, 0, 704, 500);
            AddMiniMapRef(MapLevel.Zone, 7, 50, "e_rainbow_field", 14, 53, 0, 0, 448, 188);
            AddMiniMapRef(MapLevel.Zone, 7, 70, "e_rainbow_field", 18, 68, 0, 0, 624, 272);
            AddMiniMapRef(MapLevel.Zone, 7, 100, "e_rainbow_field", 28, 98, 0, 0, 896, 388);
            AddMiniMapRef(MapLevel.Zone, 12, 50, "e_singing_land", 20, 8, 0, 0, 388, 264);
            AddMiniMapRef(MapLevel.Zone, 12, 70, "e_singing_land", 29, 12, 0, 0, 532, 368);
            AddMiniMapRef(MapLevel.Zone, 12, 100, "e_singing_land", 42, 19, 0, 0, 740, 524);
            AddMiniMapRef(MapLevel.Zone, 14, 50, "e_steppe_belt", 72, 10, 0, 0, 356, 224);
            AddMiniMapRef(MapLevel.Zone, 14, 70, "e_steppe_belt", 101, 14, 0, 0, 496, 312);
            AddMiniMapRef(MapLevel.Zone, 14, 100, "e_steppe_belt", 153, 20, 0, 0, 692, 428);
            AddMiniMapRef(MapLevel.Zone, 4, 50, "e_sunrise_peninsula", 62, 8, 0, 0, 352, 268);
            AddMiniMapRef(MapLevel.Zone, 4, 70, "e_sunrise_peninsula", 85, 11, 0, 0, 496, 376);
            AddMiniMapRef(MapLevel.Zone, 4, 100, "e_sunrise_peninsula", 124, 16, 0, 0, 704, 540);
            AddMiniMapRef(MapLevel.Zone, 24, 50, "e_tiger_spine_mountains", 30, 19, 0, 0, 360, 248);
            AddMiniMapRef(MapLevel.Zone, 24, 70, "e_tiger_spine_mountains", 48, 32, 0, 0, 500, 336);
            AddMiniMapRef(MapLevel.Zone, 24, 100, "e_tiger_spine_mountains", 84, 46, 0, 0, 696, 472);
            AddMiniMapRef(MapLevel.Zone, 17, 50, "e_ynystere", 80, 8, 0, 0, 376, 256);
            AddMiniMapRef(MapLevel.Zone, 17, 70, "e_ynystere", 112, 12, 0, 0, 528, 360);
            AddMiniMapRef(MapLevel.Zone, 17, 100, "e_ynystere", 164, 21, 0, 0, 764, 508);
            AddMiniMapRef(MapLevel.Zone, 15, 50, "e_ruins_of_hariharalaya", 44, 13, 0, 0, 372, 264);
            AddMiniMapRef(MapLevel.Zone, 15, 70, "e_ruins_of_hariharalaya", 61, 20, 0, 0, 516, 368);
            AddMiniMapRef(MapLevel.Zone, 15, 100, "e_ruins_of_hariharalaya", 87, 29, 0, 0, 744, 524);
            AddMiniMapRef(MapLevel.Zone, 16, 50, "e_lokas_checker", 87, 15, 0, 0, 276, 264);
            AddMiniMapRef(MapLevel.Zone, 16, 70, "e_lokas_checker", 122, 21, 0, 0, 388, 368);
            AddMiniMapRef(MapLevel.Zone, 16, 100, "e_lokas_checker", 173, 34, 0, 0, 556, 512);
            AddMiniMapRef(MapLevel.Zone, 23, 50, "e_hasla", 129, 1, 0, 0, 264, 268);
            AddMiniMapRef(MapLevel.Zone, 23, 70, "e_hasla", 181, 1, 0, 0, 368, 376);
            AddMiniMapRef(MapLevel.Zone, 23, 100, "e_hasla", 215, 2, 0, 0, 568, 532);
            AddMiniMapRef(MapLevel.City, 278, 50, "e_mahadevi_city", 73, 0, 0, 0, 344, 284);
            AddMiniMapRef(MapLevel.City, 278, 70, "e_mahadevi_city", 104, 0, 0, 0, 492, 400);
            AddMiniMapRef(MapLevel.City, 278, 100, "e_mahadevi_city", 164, 8, 0, 0, 688, 546);
            AddMiniMapRef(MapLevel.City, 542, 50, "e_singing_land_city", 60, 8, 0, 0, 284, 272);
            AddMiniMapRef(MapLevel.City, 542, 70, "e_singing_land_city", 83, 12, 0, 0, 400, 380);
            AddMiniMapRef(MapLevel.City, 542, 100, "e_singing_land_city", 119, 17, 0, 0, 556, 536);
            AddMiniMapRef(MapLevel.City, 578, 50, "e_sunrise_peninsula_city", 70, 8, 0, 0, 340, 272);
            AddMiniMapRef(MapLevel.City, 578, 70, "e_sunrise_peninsula_city", 98, 12, 0, 0, 464, 372);
            AddMiniMapRef(MapLevel.City, 578, 100, "e_sunrise_peninsula_city", 140, 16, 0, 0, 676, 528);
            AddMiniMapRef(MapLevel.City, 468, 50, "e_ynystere_road_city", 52, 12, 0, 0, 372, 264);
            AddMiniMapRef(MapLevel.City, 468, 70, "e_ynystere_road_city", 67, 18, 0, 0, 528, 364);
            AddMiniMapRef(MapLevel.City, 468, 100, "e_ynystere_road_city", 88, 32, 0, 0, 760, 512);
            AddMiniMapRef(MapLevel.City, 735, 50, "e_falcony_plateau_bat_cave", 73, 31, 0, 0, 280, 208);
            AddMiniMapRef(MapLevel.City, 735, 70, "e_falcony_plateau_bat_cave", 103, 43, 0, 0, 392, 292);
            AddMiniMapRef(MapLevel.City, 735, 100, "e_falcony_plateau_bat_cave", 148, 58, 0, 0, 560, 420);
            AddMiniMapRef(MapLevel.City, 749, 50, "e_falcony_plateau_misty_cave", 164, 20, 0, 0, 176, 236);
            AddMiniMapRef(MapLevel.City, 749, 70, "e_falcony_plateau_misty_cave", 227, 30, 0, 0, 248, 324);
            AddMiniMapRef(MapLevel.City, 749, 100, "e_falcony_plateau_misty_cave", 326, 41, 0, 0, 352, 464);
            AddMiniMapRef(MapLevel.City, 996, 50, "e_mahadevi_astra_cave", 85, 4, 0, 0, 328, 264);
            AddMiniMapRef(MapLevel.City, 996, 70, "e_mahadevi_astra_cave", 119, 7, 0, 0, 460, 368);
            AddMiniMapRef(MapLevel.City, 996, 100, "e_mahadevi_astra_cave", 170, 11, 0, 0, 652, 524);
            AddMiniMapRef(MapLevel.City, 95, 50, "e_steppe_belt_snowlion_rock", 60, 13, 0, 0, 364, 236);
            AddMiniMapRef(MapLevel.City, 95, 70, "e_steppe_belt_snowlion_rock", 82, 18, 0, 0, 512, 328);
            AddMiniMapRef(MapLevel.City, 95, 100, "e_steppe_belt_snowlion_rock", 119, 27, 0, 0, 728, 468);
            AddMiniMapRef(MapLevel.City, 688, 50, "e_tiger_spine_mountains_kobold_cave", 133, 23, 0, 0, 224, 220);
            AddMiniMapRef(MapLevel.City, 688, 70, "e_tiger_spine_mountains_kobold_cave", 187, 34, 0, 0, 312, 304);
            AddMiniMapRef(MapLevel.City, 688, 100, "e_tiger_spine_mountains_kobold_cave", 268, 48, 0, 0, 444, 436);
            AddMiniMapRef(MapLevel.City, 1057, 50, "e_hasla_cemetery_underground", 147, 49, 0, 0, 180, 188);
            AddMiniMapRef(MapLevel.City, 1057, 70, "e_hasla_cemetery_underground", 205, 67, 0, 0, 252, 264);
            AddMiniMapRef(MapLevel.City, 1057, 100, "e_hasla_cemetery_underground", 294, 96, 0, 0, 360, 376);
            AddMiniMapRef(MapLevel.Zone, 33, 50, "o_salpimari", 60, 2, 0, 0, 404, 256);
            AddMiniMapRef(MapLevel.Zone, 33, 70, "o_salpimari", 84, 0, 0, 0, 564, 360);
            AddMiniMapRef(MapLevel.Zone, 33, 100, "o_salpimari", 120, 38, 0, 0, 808, 483);
            AddMiniMapRef(MapLevel.Zone, 34, 50, "o_nuimari", 1, 11, 0, 0, 464, 236);
            AddMiniMapRef(MapLevel.Zone, 34, 70, "o_nuimari", 2, 12, 0, 0, 648, 332);
            AddMiniMapRef(MapLevel.Zone, 34, 100, "o_nuimari", 4, 38, 0, 0, 924, 472);
            AddMiniMapRef(MapLevel.Zone, 43, 50, "o_seonyeokmari", 38, 10, 0, 0, 429, 268);
            AddMiniMapRef(MapLevel.Zone, 43, 70, "o_seonyeokmari", 55, 14, 0, 0, 592, 376);
            AddMiniMapRef(MapLevel.Zone, 43, 100, "o_seonyeokmari", 80, 20, 0, 0, 848, 536);
            AddMiniMapRef(MapLevel.Zone, 44, 50, "o_rest_land", 0, 0, 0, 0, 464, 276);
            AddMiniMapRef(MapLevel.Zone, 44, 70, "o_rest_land", 0, 0, 0, 0, 652, 384);
            AddMiniMapRef(MapLevel.Zone, 44, 100, "o_rest_land", 4, 7, 0, 0, 844, 548);
            AddMiniMapRef(MapLevel.Zone, 54, 50, "o_abyss_gate", 0, 0, 0, 0, 376, 280);
            AddMiniMapRef(MapLevel.Zone, 54, 70, "o_abyss_gate", 0, 0, 0, 0, 528, 392);
            AddMiniMapRef(MapLevel.Zone, 54, 100, "o_abyss_gate", 0, 0, 0, 0, 752, 556);
            AddMiniMapRef(MapLevel.Zone, 56, 50, "o_land_of_sunlights", 0, 6, 0, 0, 400, 244);
            AddMiniMapRef(MapLevel.Zone, 56, 70, "o_land_of_sunlights", 0, 8, 0, 0, 560, 340);
            AddMiniMapRef(MapLevel.Zone, 56, 100, "o_land_of_sunlights", 0, 11, 0, 0, 800, 484);
            AddMiniMapRef(MapLevel.Zone, 61, 50, "o_shining_shore", 22, 24, 0, 0, 432, 248);
            AddMiniMapRef(MapLevel.Zone, 61, 70, "o_shining_shore", 31, 34, 0, 0, 604, 348);
            AddMiniMapRef(MapLevel.Zone, 61, 100, "o_shining_shore", 43, 50, 0, 0, 860, 492);
            AddMiniMapRef(MapLevel.Zone, 67, 50, "o_library_1", 128, 43, 0, 0, 188, 188);
            AddMiniMapRef(MapLevel.Zone, 67, 70, "o_library_1", 179, 60, 0, 0, 264, 264);
            AddMiniMapRef(MapLevel.Zone, 67, 100, "o_library_1", 256, 86, 0, 0, 376, 376);
            AddMiniMapRef(MapLevel.Zone, 65, 50, "o_library_2", 129, 41, 0, 0, 188, 188);
            AddMiniMapRef(MapLevel.Zone, 65, 70, "o_library_2", 181, 58, 0, 0, 264, 264);
            AddMiniMapRef(MapLevel.Zone, 65, 100, "o_library_2", 258, 84, 0, 0, 376, 376);
            AddMiniMapRef(MapLevel.Zone, 69, 50, "o_library_2", 129, 41, 0, 0, 188, 188);
            AddMiniMapRef(MapLevel.Zone, 69, 70, "o_library_2", 181, 58, 0, 0, 264, 264);
            AddMiniMapRef(MapLevel.Zone, 69, 100, "o_library_2", 258, 84, 0, 0, 376, 376);
            AddMiniMapRef(MapLevel.City, 1075, 50, "o_abyss_gate_ruin_of_vanishing_snake_1f", 80, 43, 0, 0, 280, 220);
            AddMiniMapRef(MapLevel.City, 1075, 70, "o_abyss_gate_ruin_of_vanishing_snake_1f", 112, 59, 0, 0, 392, 312);
            AddMiniMapRef(MapLevel.City, 1075, 100, "o_abyss_gate_ruin_of_vanishing_snake_1f", 160, 85, 0, 0, 556, 440);
            AddMiniMapRef(MapLevel.City, 1076, 50, "o_abyss_gate_ruin_of_vanishing_snake_2f", 75, 46, 0, 0, 268, 208);
            AddMiniMapRef(MapLevel.City, 1076, 70, "o_abyss_gate_ruin_of_vanishing_snake_2f", 104, 65, 0, 0, 372, 292);
            AddMiniMapRef(MapLevel.City, 1076, 100, "o_abyss_gate_ruin_of_vanishing_snake_2f", 134, 81, 0, 0, 556, 432);
            AddMiniMapRef(MapLevel.Zone, 36, 50, "s_silent_sea", 0, 0, 0, 0, 464, 280);
            AddMiniMapRef(MapLevel.Zone, 36, 70, "s_silent_sea", 0, 0, 0, 0, 652, 392);
            AddMiniMapRef(MapLevel.Zone, 36, 100, "s_silent_sea", 0, 0, 0, 0, 928, 556);
            AddMiniMapRef(MapLevel.Zone, 39, 50, "s_gold_sea", 0, 0, 0, 0, 472, 284);
            AddMiniMapRef(MapLevel.Zone, 39, 70, "s_gold_sea", 0, 0, 0, 0, 656, 396);
            AddMiniMapRef(MapLevel.Zone, 39, 100, "s_gold_sea", 0, 0, 0, 0, 928, 556);
            AddMiniMapRef(MapLevel.Zone, 30, 50, "s_lost_road_sea", 0, 0, 0, 0, 464, 280);
            AddMiniMapRef(MapLevel.Zone, 30, 70, "s_lost_road_sea", 0, 0, 0, 0, 648, 388);
            AddMiniMapRef(MapLevel.Zone, 30, 100, "s_lost_road_sea", 0, 0, 0, 0, 928, 556);
            AddMiniMapRef(MapLevel.Zone, 40, 50, "s_crescent_moon_sea", 0, 0, 0, 0, 460, 280);
            AddMiniMapRef(MapLevel.Zone, 40, 70, "s_crescent_moon_sea", 0, 0, 0, 0, 644, 392);
            AddMiniMapRef(MapLevel.Zone, 40, 100, "s_crescent_moon_sea", 0, 0, 0, 0, 928, 556);
            AddMiniMapRef(MapLevel.Zone, 59, 50, "s_freedom_island", 154, 14, 0, 0, 152, 224);
            AddMiniMapRef(MapLevel.Zone, 59, 70, "s_freedom_island", 217, 19, 0, 0, 212, 312);
            AddMiniMapRef(MapLevel.Zone, 59, 100, "s_freedom_island", 310, 29, 0, 0, 300, 440);
            AddMiniMapRef(MapLevel.City, 866, 50, "s_freedom_island_detail", 126, 26, 0, 0, 232, 208);
            AddMiniMapRef(MapLevel.City, 866, 70, "s_freedom_island_detail", 177, 36, 0, 0, 324, 296);
            AddMiniMapRef(MapLevel.City, 866, 100, "s_freedom_island_detail", 251, 51, 0, 0, 464, 420);
            AddMiniMapRef(MapLevel.Zone, 60, 50, "s_pirate_island", 72, 12, 0, 0, 348, 256);
            AddMiniMapRef(MapLevel.Zone, 60, 70, "s_pirate_island", 103, 18, 0, 0, 484, 356);
            AddMiniMapRef(MapLevel.Zone, 60, 100, "s_pirate_island", 147, 25, 0, 0, 692, 508);
            AddMiniMapRef(MapLevel.City, 14, 50, "w_gweonid_forest_city", 60, 2, 0, 0, 352, 276);
            AddMiniMapRef(MapLevel.City, 14, 70, "w_gweonid_forest_city", 89, 12, 0, 0, 480, 376);
            AddMiniMapRef(MapLevel.City, 14, 100, "w_gweonid_forest_city", 127, 1, 0, 0, 684, 548);
            AddMiniMapRef(MapLevel.City, 335, 50, "w_solzreed_city", 9, 0, 0, 0, 392, 264);
            AddMiniMapRef(MapLevel.City, 335, 70, "w_solzreed_city", 13, 0, 0, 0, 548, 368);
            AddMiniMapRef(MapLevel.City, 335, 100, "w_solzreed_city", 21, 0, 0, 0, 780, 540);
            AddMiniMapRef(MapLevel.City, 37, 50, "w_marianople_city", 30, 0, 0, 0, 388, 280);
            AddMiniMapRef(MapLevel.City, 37, 70, "w_marianople_city", 44, 0, 0, 0, 496, 392);
            AddMiniMapRef(MapLevel.City, 37, 100, "w_marianople_city", 75, 0, 0, 0, 696, 556);
            AddMiniMapRef(MapLevel.City, 144, 50, "w_two_crowns_city", 145, 0, 0, 0, 220, 220);
            AddMiniMapRef(MapLevel.City, 144, 70, "w_two_crowns_city", 205, 0, 0, 0, 304, 304);
            AddMiniMapRef(MapLevel.City, 144, 100, "w_two_crowns_city", 292, 0, 0, 0, 436, 436);
            AddMiniMapRef(MapLevel.City, 155, 50, "w_white_forest_red_moss_cave", 92, 29, 0, 0, 300, 212);
            AddMiniMapRef(MapLevel.City, 155, 70, "w_white_forest_red_moss_cave", 129, 42, 0, 0, 424, 292);
            AddMiniMapRef(MapLevel.City, 155, 100, "w_white_forest_red_moss_cave", 185, 61, 0, 0, 596, 416);
            AddMiniMapRef(MapLevel.City, 253, 50, "w_lilyut_meadow_east_ronbann_mine", 57, 56, 0, 0, 380, 192);
            AddMiniMapRef(MapLevel.City, 253, 70, "w_lilyut_meadow_east_ronbann_mine", 82, 81, 0, 0, 520, 264);
            AddMiniMapRef(MapLevel.City, 253, 100, "w_lilyut_meadow_east_ronbann_mine", 116, 116, 0, 0, 744, 376);
            AddMiniMapRef(MapLevel.City, 252, 50, "w_lilyut_meadow_west_ronbann_mine", 55, 25, 0, 0, 340, 220);
            AddMiniMapRef(MapLevel.City, 252, 70, "w_lilyut_meadow_west_ronbann_mine", 79, 34, 0, 0, 476, 312);
            AddMiniMapRef(MapLevel.City, 252, 100, "w_lilyut_meadow_west_ronbann_mine", 112, 52, 0, 0, 680, 436);

            // Read .g file data for roads
            if ((MainForm.ThisForm.Pak != null) && MainForm.ThisForm.Pak.IsOpen)
            {
                foreach (var zg in AaDb.DbZoneGroups)
                {
                    var refs = MapViewMiniMapRef.ListPossibleFileNames(zg.Value.Name, 100, Properties.Settings.Default.DefaultGameLanguage, ".g");
                    foreach (var r in refs)
                        if (MainForm.ThisForm.Pak.FileExists(r))
                            LoadGFileFromPak(MainForm.ThisForm.Pak, r);
                }
            }
            // Override refs if needed
            foreach (var zg in AaDb.DbZoneGroups)
            {
                if (GFileVars.ContainsKey(zg.Value.Name + "_road_100.coords.x") &&
                    GFileVars.ContainsKey(zg.Value.Name + "_road_100.coords.y") &&
                    GFileVars.ContainsKey(zg.Value.Name + "_road_100.coords.w") &&
                    GFileVars.ContainsKey(zg.Value.Name + "_road_100.coords.h") &&
                    GFileVars.ContainsKey(zg.Value.Name + "_road_100.offset.x") &&
                    GFileVars.ContainsKey(zg.Value.Name + "_road_100.offset.y"))
                {
                    var roadX = GFileValInt(zg.Value.Name + "_road_100.coords.x");
                    var roadY = GFileValInt(zg.Value.Name + "_road_100.coords.y");
                    var roadW = GFileValInt(zg.Value.Name + "_road_100.coords.w");
                    var roadH = GFileValInt(zg.Value.Name + "_road_100.coords.h");
                    var roadXOff = GFileValInt(zg.Value.Name + "_road_100.offset.x");
                    var roadYOff = GFileValInt(zg.Value.Name + "_road_100.offset.y");

                    AddMiniMapRef(MapLevel.Zone, (int)zg.Value.Id, 100, zg.Value.Name, roadXOff, roadYOff, roadX, roadY, roadW, roadH);
                }
            }

        }

        public static string GFileValString(string key)
        {
            if (GFileVars.TryGetValue(key, out var res))
                return res;
            return string.Empty;
        }

        public static int GFileValInt(string key)
        {
            var s = GFileValString(key);
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int i))
                return i;
            return 0;
        }

        public static void LoadGFileFromPak(AAPak pak, string fileName)
        {
            if ((pak == null) || (!pak.IsOpen) || !pak.FileExists(fileName))
                return;

            var lines = new List<string>();
            using (var fs = pak.ExportFileAsStream(fileName))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                        lines.Add(sr.ReadLine());
                }
            }
            var gf = ConvertGFileStringList(lines);
            foreach (var gl in gf)
            {
                if (GFileVars.ContainsKey(gl.Key))
                    GFileVars.Remove(gl.Key);
                GFileVars.Add(gl.Key, gl.Value);
            }
        }

        static Dictionary<string, string> ConvertGFileStringList(List<string> lines)
        {
            var res = new Dictionary<string, string>();
            var currentObjectName = string.Empty;
            var areaName = string.Empty;

            foreach (var l in lines)
            {
                if (l.Trim() == string.Empty)
                    continue;
                if (!l.StartsWith("  "))
                {
                    currentObjectName = l.Trim().ToLower();
                    Console.WriteLine(currentObjectName);
                }
                else
                {
                    var words = l.Split(' ').ToList();
                    for (var i = 0; i < words.Count; i++)
                        words[i] = words[i].Trim();
                    var containedValues = string.Empty;
                    var remainderString = string.Join(" ", words.GetRange(1, words.Count - 1)).Replace(',', ' ').Split(' ').ToList();
                    for (var i = 0; i < remainderString.Count; i++)
                        remainderString[i] = remainderString[i].Replace(',', ' ').Trim();
                    var remainder = new List<string>();
                    foreach (var s in remainderString)
                        if (s.Trim() != string.Empty)
                            remainder.Add(s.Trim());
                    var para = new List<string>();
                    if ((remainder.Count > 2) && (remainder[1] == "(") && (remainder[remainder.Count - 1] == ")"))
                    {
                        for (var i = 2; i < remainder.Count - 1; i++)
                            para.Add(remainder[i]);
                    }
                    var varName = remainder.Count > 1 ? remainder[0].ToLower() : string.Empty;

                    switch (varName)
                    {
                        // Used in quest_sign_sphere.g
                        case "qtype":
                            // qtype is always the first entry in the block, increment a number here to prevent duplicates
                            // We want unique identifiers here so we can add it to a dictionary
                            GFileAreaIdCounter++;
                            areaName = GFileAreaIdCounter.ToString() + ".";
                            res.Add(currentObjectName + "." + areaName + "?", "area");
                            res.Add(currentObjectName + "." + areaName + varName, para[0]);
                            break;
                        case "ctype":
                            res.Add(currentObjectName + "." + areaName + varName, para[0]);
                            break;
                        case "radius":
                            res.Add(currentObjectName + "." + areaName + varName, para[0]);
                            // radius is normally the last entry in a area block, reset the name here
                            areaName = string.Empty;
                            break;
                        case "pos":
                            res.Add(currentObjectName + "." + areaName + varName + ".x", para[1]);
                            res.Add(currentObjectName + "." + areaName + varName + ".y", para[3]);
                            res.Add(currentObjectName + "." + areaName + varName + ".z", para[5]);
                            break;
                        case "coords":
                            if (para.Count == 4)
                            {
                                res.Add(currentObjectName + "." + varName + ".x", para[0]);
                                res.Add(currentObjectName + "." + varName + ".y", para[1]);
                                res.Add(currentObjectName + "." + varName + ".w", para[2]);
                                res.Add(currentObjectName + "." + varName + ".h", para[3]);
                            }
                            else
                            {
                                Console.Error.WriteLine("Invalid number of parameters for " + currentObjectName + ".coords");
                            }
                            break;
                        case "offset":
                            if (para.Count == 2)
                            {
                                res.Add(currentObjectName + "." + varName + ".x", para[0]);
                                res.Add(currentObjectName + "." + varName + ".y", para[1]);
                            }
                            else
                            {
                                Console.Error.WriteLine("Invalid number of parameters for " + currentObjectName + ".offset");
                            }
                            break;
                        default:
                            Console.Error.WriteLine("Unknown variable name " + varName + " for " + currentObjectName);
                            break;
                    }
                }

            }
            return res;
        }


    }

    public class MapViewMiniMapRef
    {
        public MapLevel Level;
        public long ZoneGroup;
        public int Scale;
        public string BaseFileName;
        public PointF Offset;
        public RectangleF Rect;

        public List<string> GetPossibleFileNames(string locale)
        {
            return ListPossibleFileNames(BaseFileName, Scale, locale);
        }

        public static List<string> ListPossibleFileNames(string baseFileName, int scale, string locale, string ext = ".dds")
        {
            var res = new List<string>();
            // List possible files in order of prefered
            // Version 1.2
            res.Add("game/ui/map/road/" + locale + "/" + baseFileName + "_road_" + scale.ToString() + ext);
            res.Add("game/ui/map/road/" + baseFileName + "_road_" + scale.ToString() + ext);
            // starting from Version ?.?
            res.Add("game/ui/map/map_resources/" + baseFileName + "/" + locale + "/road_" + scale.ToString() + ext);
            res.Add("game/ui/map/map_resources/" + baseFileName + "/road_" + scale.ToString() + ext);
            // The following is to capture a error from XLGames on some maps like sanddeep
            res.Add("game/ui/map/road/" + locale + "/" + baseFileName + "_" + scale.ToString() + ext);
            res.Add("game/ui/map/road/" + baseFileName + "_" + scale.ToString() + ext);
            return res;
        }
    }

    public class MapViewImageRef
    {
        public MapLevel Level;
        public long Id;
        public long ImageId;
        public RectangleF MapRect;
        public string BaseFileName;

        public List<string> GetPossibleFileNames(string locale)
        {
            return ListPossibleFileNames(BaseFileName, locale);
        }

        public static List<string> ListPossibleFileNames(string baseFileName, string locale)
        {
            var res = new List<string>();
            // List possible files in order of prefered
            // Version 1.2
            res.Add("game/ui/map/world/" + locale + "/" + baseFileName + ".dds");
            res.Add("game/ui/map/world/" + baseFileName + ".dds");
            // starting from Version ?.?
            res.Add("game/ui/map/map_resources/" + baseFileName + "/" + locale + "/world.dds");
            res.Add("game/ui/map/map_resources/" + baseFileName + "/world.dds");
            return res;
        }
    }

    public class MapViewZonePathOffsets
    {
        private static Dictionary<long, PointF> zones = new Dictionary<long, PointF>();

        public static void LoadOffsetsFromFile()
        {
            zones = new Dictionary<long, PointF>();
            try
            {
                var xml = new XmlDocument();
                xml.Load("data/zone_path_offsets.xml");
                var nodes = xml.SelectNodes("/pathoffsets/zone");
                for (var i = 0; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    long key = 0;
                    float x = 0;
                    float y = 0;
                    for (var a = 0; a < node.Attributes.Count; a++)
                    {
                        var attrib = node.Attributes[a];
                        if (attrib.Name == "key")
                            key = long.Parse(attrib.Value, CultureInfo.InvariantCulture);
                        if (attrib.Name == "x")
                            x = float.Parse(attrib.Value, CultureInfo.InvariantCulture);
                        if (attrib.Name == "y")
                            y = float.Parse(attrib.Value, CultureInfo.InvariantCulture);
                    }

                    if (key > 0)
                    {
                        var zoneOffset = new PointF(x, y);
                        zones.Add(key, zoneOffset);
                    }
                }
            }
            catch
            {

            }
        }

        public static PointF GetZoneOffset(long zone_key)
        {
            if (zones.TryGetValue(zone_key, out var z))
                return z;
            else
                return new PointF();
        }

    }

    public enum MapLevel : byte
    {
        None = 0,
        WorldMap = 1,
        Continent = 2,
        Zone = 3,
        City = 4
    }

    public class MapViewPoI
    {
        public Vector3 Coord = Vector3.Zero;
        public Color PoIColor = Color.Yellow;
        public string Name = string.Empty;
        public float Radius = 0f;
        public string TypeName = string.Empty;
        public long TypeId = 0;
        public object SourceObject = null;
    }

    public class MapViewMap
    {
        public MapLevel MapLevel = MapLevel.None;
        public RectangleF ZoneCoords = new RectangleF();
        public RectangleF ImgCoords = new RectangleF();
        public Color MapBorderColor = Color.Yellow;
        public string Name = string.Empty;
        public string InstanceName = string.Empty;
        public string MapImageFile = string.Empty;
        public Bitmap MapBitmapImage = null;

        public string RoadImageFile = string.Empty;
        public PointF RoadMapOffset = new PointF();
        public RectangleF RoadMapCoords = new RectangleF();
        public Bitmap RoadBitmapImage = null;

        public long ZoneGroup = 0;
    }

    public class MapViewPath
    {
        public string PathName = string.Empty;
        public Color Color = Color.White;
        public long TypeId = 0;
        public List<Vector3> allpoints { get; private set; } = new List<Vector3>();
        // Helper data for drawing, not actually related to the data
        public byte DrawStyle = 0;
        public RectangleF BoundingBox { get; private set; } = new RectangleF();
        public void AddPoint(Vector3 point)
        {
            // Add point to list
            allpoints.Add(point);

            if (allpoints.Count <= 1)
            {
                BoundingBox = new RectangleF(point.X, point.Y, 0, 0);
                return;
            }

            // Adjust bounding box if needed
            var x1 = BoundingBox.Left;
            var y1 = BoundingBox.Top;
            var x2 = BoundingBox.Right;
            var y2 = BoundingBox.Bottom;

            if (point.X < x1)
                x1 = point.X;
            if (point.Y < y1)
                y1 = point.Y;
            if (point.X > x2)
                x2 = point.X;
            if (point.Y > y2)
                y2 = point.Y;

            BoundingBox = new RectangleF(x1, y1, x2 - x1, y2 - y1);
        }

        private bool AreLinesIntersects(Vector2 v1Start, Vector2 v1End, Vector2 v2Start, Vector2 v2End)
        {
            float d1, d2;
            float a1, a2, b1, b2, c1, c2;

            // Convert vector 1 to a line (line 1) of infinite length.
            // We want the line in linear equation standard form: A*x + B*y + C = 0
            // See: http://en.wikipedia.org/wiki/Linear_equation
            a1 = v1End.Y - v1Start.Y;
            b1 = v1Start.X - v1End.X;
            c1 = (v1End.X * v1Start.Y) - (v1Start.X * v1End.Y);

            // Every point (x,y), that solves the equation above, is on the line,
            // every point that does not solve it, is not. The equation will have a
            // positive result if it is on one side of the line and a negative one
            // if is on the other side of it. We insert (x1,y1) and (x2,y2) of vector
            // 2 into the equation above.
            d1 = (a1 * v2Start.X) + (b1 * v2Start.Y) + c1;
            d2 = (a1 * v2End.X) + (b1 * v2End.Y) + c1;

            // If d1 and d2 both have the same sign, they are both on the same side
            // of our line 1 and in that case no intersection is possible. Careful,
            // 0 is a special case, that's why we don't test ">=" and "<=",
            // but "<" and ">".
            if (d1 > 0 && d2 > 0) return false;
            if (d1 < 0 && d2 < 0) return false;

            // The fact that vector 2 intersected the infinite line 1 above doesn't
            // mean it also intersects the vector 1. Vector 1 is only a subset of that
            // infinite line 1, so it may have intersected that line before the vector
            // started or after it ended. To know for sure, we have to repeat the
            // the same test the other way round. We start by calculating the
            // infinite line 2 in linear equation standard form.
            a2 = v2End.Y - v2Start.Y;
            b2 = v2Start.X - v2End.X;
            c2 = (v2End.X * v2Start.Y) - (v2Start.X * v2End.Y);

            // Calculate d1 and d2 again, this time using points of vector 1.
            d1 = (a2 * v1Start.X) + (b2 * v1Start.Y) + c2;
            d2 = (a2 * v1End.X) + (b2 * v1End.Y) + c2;

            // Again, if both have the same sign (and neither one is 0),
            // no intersection is possible.
            if (d1 > 0 && d2 > 0) return false;
            if (d1 < 0 && d2 < 0) return false;

            // If we get here, only two possibilities are left. Either the two
            // vectors intersect in exactly one point or they are collinear, which
            // means they intersect in any number of points from zero to infinite.
            if ((a1 * b2) - (a2 * b1) == 0.0f) return false; // COLLINEAR;

            // If they are not collinear, they must intersect in exactly one point.
            return true;
        }

        public bool Contains(float x, float y)
        {
            // Info: https://stackoverflow.com/questions/217578/how-can-i-determine-whether-a-2d-point-is-within-a-polygon

            // Very rough test for better speed
            if (!BoundingBox.Contains(x, y))
                return false;

            // Test the ray against all sides
            var intersections = 0;
            for (var side = 0; side < allpoints.Count - 1; side++)
            {
                var sideStart = new Vector2(allpoints[side].X, allpoints[side].Y);
                var sideEnd = new Vector2(allpoints[side + 1].X, allpoints[side + 1].Y);
                var rayStart = new Vector2(BoundingBox.X - 1f, BoundingBox.Y - 1f);
                var rayEnd = new Vector2(x, y);

                // Test if current side intersects with ray.
                // If yes, intersections++;
                if (AreLinesIntersects(rayStart, rayEnd, sideStart, sideEnd))
                    intersections++;
            }

            return ((intersections & 1) == 1);
        }

        public bool Contains(Vector3 pos) => Contains(pos.X, pos.Y);
        public bool Contains(Vector2 pos) => Contains(pos.X, pos.Y);
        public bool Contains(Point pos) => Contains(pos.X, pos.Y);
    }

    public class MapViewWorldXMLZoneCellInfo
    {
        public int X = 0;
        public int Y = 0;
        public Rectangle bounds = new Rectangle();
        public long zone_key = 0;
    }

    public class MapViewWorldXMLZoneInfo
    {
        public string name = string.Empty;
        public long zone_key = 0;
        public int originCellX = 0;
        public int originCellY = 0;
        public List<MapViewWorldXMLZoneCellInfo> Cells = new List<MapViewWorldXMLZoneCellInfo>();
        public MapViewWorldXMLZoneCellInfo FindCell(int coordX, int coordY)
        {
            foreach (var cell in Cells)
                if (cell.bounds.Contains(coordX, coordY))
                    return cell;
            return null;
        }
    }
}
