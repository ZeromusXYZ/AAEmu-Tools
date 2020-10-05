using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AAEmu.DBViewer
{
    public static class MapViewImageHelper
    {
        public static List<MapViewImageRef> ImageRefs = new List<MapViewImageRef>();


        public static void AddRef(MapLevel level, int id, string fileName, int image_map)
        {
            var newRef = new MapViewImageRef();
            newRef.Level = level;
            newRef.Id = id;
            newRef.ImageId = image_map;
            newRef.FileName = fileName;
            newRef.Rect = new RectangleF();

            ImageRefs.Add(newRef);
        }

        public static MapViewImageRef GetRefByImageMapId(long image_map)
        {
            foreach (var r in ImageRefs)
                if (r.ImageId == image_map)
                    return r;
            return null;
        }

        public static void PopulateList()
        {
            ImageRefs.Clear();

            // For now this is not needed as they filename are mostly the same as the DB names anyway
            AddRef(MapLevel.WorldMap, 2, "main_world.dds", 1);

            AddRef(MapLevel.Continent, 3, "land_west.dds", 1001);
            AddRef(MapLevel.Continent, 4, "land_east.dds", 1002);
            AddRef(MapLevel.Continent, 5, "land_origin.dds", 1003);
            AddRef(MapLevel.Continent, 6, "land_silent_sea.dds", 1004);
            AddRef(MapLevel.Continent, 39, "s_gold_sea.dds", 4001);
            AddRef(MapLevel.Continent, 30, "s_lost_road_sea.dds", 4002);
            AddRef(MapLevel.Continent, 40, "s_crescent_moon_sea.dds", 4003);

            AddRef(MapLevel.Zone, 31, "instance_training_camp.dds", 5001);
            AddRef(MapLevel.Zone, 45, "instance_burntcastle_armory.dds", 6001);
            AddRef(MapLevel.Zone, 46, "instance_hadir_farm.dds", 6002);
            AddRef(MapLevel.Zone, 47, "instance_sal_temple.dds", 6003);
            AddRef(MapLevel.Zone, 50, "instance_cuttingwind_deadmine.dds", 6004);
            AddRef(MapLevel.Zone, 51, "instance_howling_abyss.dds", 6005);
            AddRef(MapLevel.Zone, 58, "instance_howling_abyss.dds", 6007);
            AddRef(MapLevel.Zone, 52, "instance_cradle_of_destruction.dds", 6006);
            AddRef(MapLevel.Zone, 49, "arche_mall.dds", 7001);

            AddRef(MapLevel.City, 1063, "instance_nachashgar_room_01.dds", 60081);
            AddRef(MapLevel.City, 1064, "instance_nachashgar_room_02.dds", 60082);
            AddRef(MapLevel.City, 1065, "instance_nachashgar_room_03.dds", 60083);
            AddRef(MapLevel.City, 1066, "instance_nachashgar_room_04.dds", 60084);
            AddRef(MapLevel.City, 1067, "instance_nachashgar_room_05.dds", 60085);
            AddRef(MapLevel.City, 1068, "instance_nachashgar_room_06.dds", 60086);
            AddRef(MapLevel.City, 1069, "instance_nachashgar_room_07.dds", 60087);
            AddRef(MapLevel.City, 1070, "instance_nachashgar_room_08.dds", 60088);
            AddRef(MapLevel.City, 1071, "instance_nachashgar_room_09_larman.dds", 60089);
            AddRef(MapLevel.City, 1084, "instance_nachashgar_room_09_carmilla.dds", 60089);
            AddRef(MapLevel.City, 1085, "instance_nachashgar_room_09_anerin.dds", 60089);
            AddRef(MapLevel.City, 1081, "instance_nachashgar_room_10.dds", 600810);

            AddRef(MapLevel.Zone, 62, "instance_immortal_isle.dds", 6009);
            AddRef(MapLevel.Zone, 64, "instance_immortal_isle.dds", 6010);
            AddRef(MapLevel.Zone, 70, "instance_library_1.dds", 6011);
            AddRef(MapLevel.Zone, 71, "instance_library_2.dds", 6012);
            AddRef(MapLevel.Zone, 72, "instance_library_3.dds", 6013);

            AddRef(MapLevel.City, 1101, "instance_library_boss_1_bossroom.dds", 60141);
            AddRef(MapLevel.City, 1105, "instance_library_boss_1_waitingroom.dds", 60142);
            AddRef(MapLevel.City, 1102, "instance_library_boss_2_bossroom.dds", 60151);
            AddRef(MapLevel.City, 1106, "instance_library_boss_2_waitingroom.dds", 60152);
            AddRef(MapLevel.City, 1103, "instance_library_boss_3_bossroom.dds", 60161);
            AddRef(MapLevel.City, 1107, "instance_library_boss_3_waitingroom.dds", 60162);
            AddRef(MapLevel.City, 1104, "instance_library_towerdefense_bossroom.dds", 60171);
            AddRef(MapLevel.City, 1108, "instance_library_towerdefense_waitingroom.dds", 60172);

            AddRef(MapLevel.Zone, 1, "w_gweonid_forest.dds", 2101);
            AddRef(MapLevel.Zone, 27, "w_long_sand.dds", 2111);
            AddRef(MapLevel.Zone, 5, "w_solzreed.dds", 2102);
            AddRef(MapLevel.Zone, 6, "w_lilyut_meadow.dds", 2103);
            AddRef(MapLevel.Zone, 3, "w_garangdol_plains.dds", 2104);
            AddRef(MapLevel.Zone, 18, "w_white_forest.dds", 2105);
            AddRef(MapLevel.Zone, 2, "w_marianople.dds", 2106);
            AddRef(MapLevel.Zone, 8, "w_two_crowns.dds", 2107);
            AddRef(MapLevel.Zone, 20, "w_cross_plains.dds", 2108);
            AddRef(MapLevel.Zone, 22, "w_golden_plains.dds", 2109);
            AddRef(MapLevel.Zone, 26, "w_hell_swamp.dds", 2110);
            AddRef(MapLevel.Zone, 19, "w_the_carcass.dds", 2112);
            AddRef(MapLevel.Zone, 11, "e_falcony_plateau.dds", 2201);
            AddRef(MapLevel.Zone, 24, "e_tiger_spine_mountains.dds", 2202);
            AddRef(MapLevel.Zone, 9, "e_mahadevi.dds", 2203);
            AddRef(MapLevel.Zone, 4, "e_sunrise_peninsula.dds", 2204);
            AddRef(MapLevel.Zone, 25, "e_ancient_forest.dds", 2206);
            AddRef(MapLevel.Zone, 12, "e_singing_land.dds", 2205);
            AddRef(MapLevel.Zone, 7, "e_rainbow_field.dds", 2212);
            AddRef(MapLevel.Zone, 17, "e_ynystere.dds", 2207);
            AddRef(MapLevel.Zone, 14, "e_steppe_belt.dds", 2209);
            AddRef(MapLevel.Zone, 16, "e_lokas_checkers.dds", 2208);
            AddRef(MapLevel.Zone, 15, "e_ruins_of_hariharalaya.dds", 2210);
            AddRef(MapLevel.Zone, 23, "e_hasla.dds", 2211);
            AddRef(MapLevel.Zone, 33, "o_salpimari.dds", 3001);
            AddRef(MapLevel.Zone, 34, "o_nuimari.dds", 3002);
            AddRef(MapLevel.Zone, 43, "o_seonyeokmari.dds", 3003);
            AddRef(MapLevel.Zone, 44, "o_rest_land.dds", 3004);
            AddRef(MapLevel.Zone, 54, "o_abyss_gate.dds", 3005);
            AddRef(MapLevel.Zone, 56, "o_land_of_sunlights.dds", 3006);
            AddRef(MapLevel.Zone, 67, "o_library_1.dds", 3007);
            AddRef(MapLevel.Zone, 65, "o_library_2.dds", 3008);
            AddRef(MapLevel.Zone, 69, "o_library_3.dds", 3009);
            AddRef(MapLevel.Zone, 61, "o_shining_shore.dds", 3010);
            AddRef(MapLevel.Zone, 39, "s_gold_sea.dds", 40011);
            AddRef(MapLevel.Zone, 30, "s_lost_road_sea.dds", 40021);
            AddRef(MapLevel.Zone, 40, "s_crescent_moon_sea.dds", 40031);
            AddRef(MapLevel.Zone, 36, "s_silent_sea.dds", 8001);
            AddRef(MapLevel.Zone, 59, "s_freedom_island.dds", 8002);

            AddRef(MapLevel.City, 866, "s_freedom_island_detail.dds", 80021);

            AddRef(MapLevel.Zone, 60, "s_pirate_island.dds", 8003);

            AddRef(MapLevel.City, 14, "w_gweonid_forest_city.dds", 21011);
            AddRef(MapLevel.City, 335, "w_solzreed_city.dds", 21021);
            AddRef(MapLevel.City, 37, "w_marianople_city.dds", 21061);
            AddRef(MapLevel.City, 144, "w_two_crowns_city.dds", 21071);
            AddRef(MapLevel.City, 252, "w_lilyut_meadow_west_ronbann_mine.dds", 21031);
            AddRef(MapLevel.City, 253, "w_lilyut_meadow_east_ronbann_mine.dds", 21032);
            AddRef(MapLevel.City, 155, "w_white_forest_red_moss_cave.dds", 21051);
            AddRef(MapLevel.City, 278, "e_mahadevi_city.dds", 22031);
            AddRef(MapLevel.City, 578, "e_sunrise_peninsula_city.dds", 22041);
            AddRef(MapLevel.City, 542, "e_singing_land_city.dds", 22051);
            AddRef(MapLevel.City, 468, "e_ynystere_city.dds", 22071);
            AddRef(MapLevel.City, 996, "e_mahadevi_astra_cave.dds", 22032);
            AddRef(MapLevel.City, 688, "e_tiger_spine_mountains_kobold_cave.dds", 22021);
            AddRef(MapLevel.City, 749, "e_falcony_plateau_misty_cave.dds", 22011);
            AddRef(MapLevel.City, 735, "e_falcony_plateau_bat_cave.dds", 22012);
            AddRef(MapLevel.City, 95, "e_steppe_belt_snowlion_rock.dds", 22091);
            AddRef(MapLevel.City, 1057, "e_hasla_cemetery_underground.dds", 22112);
            AddRef(MapLevel.City, 1075, "o_abyss_gate_ruin_of_vanishing_snake_1f.dds", 30051);
            AddRef(MapLevel.City, 1076, "o_abyss_gate_ruin_of_vanishing_snake_2f.dds", 30052);

            AddRef(MapLevel.Zone, 10, "w_bronze_rock.dds", 2113);
            AddRef(MapLevel.Zone, 21, "w_cradle_of_genesis.dds", 2114);
        }
    }

    public class MapViewImageRef
    {
        public MapLevel Level;
        public long Id;
        public long ImageId;
        public RectangleF Rect ;
        public string FileName;
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

}
