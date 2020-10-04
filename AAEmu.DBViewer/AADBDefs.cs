using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBDefs
{
    class GameTranslation
    {
        public long idx = 0;
        public string table = string.Empty;
        public string field = string.Empty;
        public string value = string.Empty;
    }

    class GameItem
    {
        // Actual DB entries
        public long id = 0;
        public string name = string.Empty;
        public long catgegory_id = 1;
        public long level = 1;
        public string description = string.Empty;
        public long price = 0;
        public long refund = 0;
        public long max_stack_size = 1;
        public long icon_id = 1;
        public bool sellable = false;
        public long fixed_grade = -1;
        public long use_skill_id = 0;

        // Linked
        public GameItemArmors item_armors = null;

        // Helpers
        public string nameLocalized = string.Empty;
        public string descriptionLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameItemCategories
    {
        // Actual DB entries
        public long id = 0;
        public string name = "None";

        // Helpers
        public string nameLocalized = "None";

        override public string ToString()
        {
            return nameLocalized + " ("+id.ToString()+")";
        }

        public string DisplayListName
        {
            get
            {
                return ToString();
            }
        }

        public string DisplayListValue
        {

            get
            {
                return id.ToString();
            }
        }
    }

    class GameItemArmors
    {
        // Actual DB entries
        public long id = 0;
        public long item_id = 0;
        public long slot_type_id = 0;
    }


    class GameSkills
    {
        // Actual DB entries
        public long id = 0;
        public string name = string.Empty;
        public string desc = string.Empty;
        public string web_desc = string.Empty;
        public long cost = 0;
        public long icon_id = 0;
        public bool show = false;
        public long cooldown_time = 0;
        public long casting_time = 0;
        public bool ignore_global_cooldown = false;
        public bool default_gcd = true;
        public long custom_gcd = 0;
        public long effect_delay = 0;
        public long ability_id = 0;
        public long mana_cost = 0;
        public long timing_id = 0;
        public long consume_lp = 0;
        public bool first_reagent_only = false;

        // Helpers
        public string nameLocalized = string.Empty;
        public string descriptionLocalized = string.Empty;
        public string webDescriptionLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameNPC
    {
        // Actual DB entries
        public long id = 0;
        public string name = string.Empty;
        public long char_race_id = 0;
        public long npc_grade_id = 0;
        public long npc_kind_id = 0;
        public long level = 0;
        public long model_id = 0;
        public long faction_id = 0;
        public long npc_template_id = 0;
        public long equip_bodies_id = 0;
        public long equip_cloths_id = 0;
        public long equip_weapons_id = 0;
        public bool skill_trainer = false;
        public long ai_file_id = 0;
        public bool merchant = false;
        public long npc_nickname_id = 0;
        public bool auctioneer = false;
        public bool show_name_tag = false;
        public bool visible_to_creator_only = false;
        public bool no_exp = false;
        public long pet_item_id = 0;
        public long base_skill_id = 0;
        public bool track_friendship = false;
        public bool priest = false;
        public string comment1 = string.Empty;
        public long npc_tendency_id = 0;
        public bool blacksmith = false;
        public bool teleporter = false;
        public float opacity = 0.0f;
        public bool ability_changer = false;
        public float scale = 1.0f;
        public string comment2 = string.Empty;
        public string comment3 = string.Empty;
        public float sight_range_scale = 0.0f;
        public float sight_fov_scale = 0.0f;
        public long milestone_id = 0;
        public float attack_start_range_scale = 0.0f;
        public bool aggression = false;
        public float exp_multiplier = 1.0f;
        public long exp_adder = 0;
        public bool stabler = false;
        public bool accept_aggro_link = false;
        public long recruiting_battle_field_id = 0;
        public float return_distance = 0;
        public long npc_ai_param_id = 0;
        public bool non_pushable_by_actor = false;
        public bool banker = false;
        public long aggro_link_special_rule_id = 0;
        public float aggro_link_help_dist = 0.0f;
        public bool aggro_link_sight_check = false;
        public bool expedition = false;
        public long honor_point = 0;
        public bool trader = false;
        public bool aggro_link_special_guard = false;
        public bool aggro_link_special_ignore_npc_attacker = false;
        public string comment_wear = string.Empty;
        public float absolute_return_distance = 0.0f;
        public bool repairman = false;
        public bool activate_ai_always = false;
        public string so_state = string.Empty;
        public bool specialty = false;
        public long sound_pack_id = 0;
        public long specialty_coin_id = 0;
        public bool use_range_mod = false;
        public long npc_posture_set_id = 0;
        public long mate_equip_slot_pack_id = 0;
        public long mate_kind_id = 0;
        public long engage_combat_give_quest_id = 0;
        public long total_custom_id = 0;
        public bool no_apply_total_custom = false;
        public bool base_skill_strafe = false;
        public float base_skill_delay = 0.0f;
        public long npc_interaction_set_id = 0;
        public bool use_abuser_list = false;
        public bool return_when_enter_housing_area = false;
        public bool look_converter = false;
        public bool use_ddcms_mount_skill = false;
        public bool crowd_effect = false;
        public float fx_scale = 1.0f;
        public bool translate = false;
        public bool no_penalty = false;
        public bool show_faction_tag = false;

        // Helpers
        public string nameLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameSkillItems
    {
        public long id = 0;
        public long skill_id = 0;
        public long item_id = 0;
        public long amount = 0;
    }

    class GameZone
    {
        static private string main_world = "main_world";

        public long id = 0;
        public string name = string.Empty;
        public long zone_key = 0;
        public long group_id = 0;
        public bool closed = false;
        public string display_text = string.Empty;
        public long faction_id = 0;
        public long zone_climate_id = 0;
        public bool abox_show = false; // no idea what this is, seems to be always set to false

        // Helpers
        public string display_textLocalized = string.Empty;
        public string SearchString = string.Empty;
        public string GamePakZoneTransferPathXML
        {
            get
            {
                return "game/worlds/" + main_world + "/level_design/zone/" + zone_key.ToString() + "/client/transfer_path.xml";
            }
        }
    }

    class GameZone_Groups
    {
        static private string main_world = "main_world";

        public long id = 0;
        public string name = string.Empty;
        public RectangleF PosAndSize = new RectangleF();
        public long image_map = 0;
        public long sound_id = 0;
        public long target_id = 0;
        public string display_text = string.Empty;
        public long faction_chat_region_id = 0;
        public long sound_pack_id = 0;
        public bool pirate_desperado = false;
        public long fishing_sea_loot_pack_id = 0;
        public long fishing_land_loot_pack_id = 0;
        public long buff_id = 0;

        // Helpers
        public string display_textLocalized = string.Empty;
        public string SearchString = string.Empty;
        public string GamePakZoneNPCsDat
        {
            get
            {
                if (target_id != 1)
                {
                    return "game/worlds/" + main_world + "/map_data/npc_map/" + name + ".dat";
                }
                else
                {
                    return "game/worlds/" + name + "/map_data/npc_map/" + name + ".dat";
                };
            }
        }
    }

    class GameWorld_Groups
    {
        public long id = 0;
        public string name = string.Empty;
        public long target_id = 0;
        public long image_map = 0;
        public Rectangle PosAndSize = new Rectangle();
        public Rectangle Image_PosAndSize = new Rectangle();

        // Helpers
        public string SearchString = string.Empty;
    }

    class GameSystemFaction
    {
        public long id = 0;
        public string name = string.Empty;
        public string owner_name = string.Empty;
        public long owner_type_id = 0;
        public long owner_id = 0;
        public long political_system_id = 0;
        public long mother_id = 0;
        public bool aggro_link = false;
        public bool guard_help = false;
        public bool is_diplomacy_tgt = false;
        public long diplomacy_link_id = 0;

        // Helpers
        public string nameLocalized = string.Empty;
        public string owner_nameLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameSystemFactionRelation
    {
        public long id = 0;
        public long faction1_id = 0;
        public long faction2_id = 0;
        public long state_id = 0;
    }

    class GameDoodadGroup
    {
        // TABLE doodad_groups
        public long id = 0;
        public string name = string.Empty;
        public bool is_export = false;
        public long guard_on_field_time = 0;
        public bool removed_by_house = false;

        // Helpers
        public string nameLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameDoodad
    {
        // TABLE doodad_almighties
        public long id = 0;
        public string name = string.Empty;
        public string model = string.Empty;
        public bool once_one_man = false;
        public bool once_one_interaction = false;
        public bool show_name = false;
        public bool mgmt_spawn = false;
        public long percent = 0;
        public long min_time = 0;
        public long max_time = 0;
        public long model_kind_id = 0;
        public bool use_creator_faction = false;
        public bool force_tod_top_priority = false;
        public long milestone_id = 0;
        public long group_id = 0;
        public bool show_minimap = false;
        public bool use_target_decal = false;
        public bool use_target_silhouette = false;
        public bool use_target_highlight = false;
        public float target_decal_size = 0.0f;
        public long sim_radius = 0;
        public bool collide_ship = false;
        public bool collide_vehicle = false;
        public long climate_id = 0;
        public bool save_indun = false;
        public string mark_model = string.Empty;
        public bool force_up_action = false;
        public bool load_model_from_world = false;
        public bool parentable = false;
        public bool childable = false;
        public long faction_id = 0;
        public long growth_time = 0;
        public bool despawn_on_collision = false;
        public bool no_collision = false;
        public long restrict_zone_id = 0;
        public bool translate = false;

        // Helpers
        public string nameLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameDoodadFunc
    {
        // TABLE doodad_funcs
        public long id = 0;
        public long doodad_func_group_id = 0;
        public long actual_func_id = 0;
        public string actual_func_type = string.Empty;
        public long next_phase = 0;
        public long sound_id = 0;
        public long func_skill_id = 0;
        public long perm_id = 0;
        public long act_count = 0;
        public bool popup_warn = false;
        public bool forbid_on_climb = false;
    }

    class GameDoodadFuncGroup
    {
        // TABLE doodad_func_groups
        public long id = 0;
        public string model = string.Empty;
        public long doodad_almighty_id = 0;
        public long doodad_func_group_kind_id = 0;
        public string phase_msg = string.Empty;
        public long sound_id = 0;
        public string name = string.Empty;
        public long sound_time = 0;
        public string comment = string.Empty;
        public bool is_msg_to_zone = false;

        // Helpers
        public string nameLocalized = string.Empty;
        public string phase_msgLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameQuestContexts
    {
        // TABLE quest_contexts
        public long id = 0;
        public string name = string.Empty;
        public long category_id = 0;
        public bool repeatable = false;
        public long level = 0;
        public bool selective = false;
        public bool successive = false;
        public bool restart_on_fail = false;
        public long chapter_idx = 0;
        public long quest_idx = 0;
        public long milestone_id = 0;
        public bool let_it_done = false;
        public long detail_id = 0;
        public long zone_id = 0;
        public long degree = 0;
        public bool use_quest_camera = false;
        public long score = 0;
        public bool use_accept_message = false;
        public bool use_complete_message = false;
        public long grade_id = 0;

        public string nameLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameQuestCategory
    {
        // TABLE quest_category
        public long id = 0;
        public string name = string.Empty;

        public string nameLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameQuestAct
    {
        // TABLE quest_acts
        public long id = 0;
        public long quest_component_id = 0;
        public long act_detail_id = 0;
        public string act_detail_type = string.Empty;
    }

    class GameQuestComponent
    {
        // TABLE quest_components
        public long id = 0;
        public long quest_context_id = 0;
        public long component_kind_id = 0;
        public long next_component = 0;
        public long npc_ai_id = 0;
        public long npc_id = 0;
        public long skill_id = 0;
        public bool skill_self = false;
        public string ai_path_name = string.Empty;
        public long ai_path_type_id = 0;
        public long sound_id = 0;
        public long npc_spawner_id = 0;
        public bool play_cinema_before_bubble = false;
        public long ai_command_set_id = 0;
        public bool or_unit_reqs = false;
        public long cinema_id = 0;
        public long summary_voice_id = 0;
        public bool hide_quest_marker = false;
        public long buff_id = 0;
    }


    public class GameTags
    {
        public long id = 0;
        public string name = string.Empty;
        public string desc = string.Empty;

        // Helpers
        public string nameLocalized = string.Empty;
        public string descLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    public class GameTaggedValues
    {
        public long id = 0;
        public long tag_id = 0;
        public long target_id = 0;
    }


    public class GameBuff
    {
        public long id = 0;
        public string name = string.Empty;
        public string desc = string.Empty;
        public long icon_id = 0;
        public long duration = 0;
        public Dictionary<string, string> _others = new Dictionary<string, string>();

        // Helpers
        public string nameLocalized = string.Empty;
        public string descLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    public class GameZoneGroupBannedTags
    {
        /*
        TABLE zone_group_banned_tags(
          id INT,
          zone_group_id INT,
          tag_id INT,
          banned_periods_id INT,
          usage TEXT
        )
        */
        public long id = 0;
        public long zone_group_id = 0;
        public long tag_id = 0;
        public long banned_periods_id = 0;
        public string usage = string.Empty ;
    }

    public class GameTransfers
    {
        /*
          CREATE TABLE transfers(
          id INT,
          comment TEXT,
          model_id INT,
          wait_time REAL,
          cyclic NUM,
          path_smoothing REAL
        )
        */
        public long id = 0;
        public long model_id = 0;
        public float path_smoothing = 8f;
    }

    public class GameTransferPaths
    {
        /*
          CREATE TABLE transfer_paths(
          id INT,
          owner_id INT,
          owner_type TEXT,
          path_name TEXT,
          wait_time_start REAL,
          wait_time_end REAL
          )
        */
        public long id = 0;
        public long owner_id = 0;
        public string owner_type = string.Empty;
        public string path_name = string.Empty;
        public float wait_time_start = 0f;
        public float wait_time_end = 0f;
    }


    static class AADB
    {
        static public Dictionary<string, GameTranslation> DB_Translations = new Dictionary<string, GameTranslation>();
        static public Dictionary<long, GameItemCategories> DB_ItemsCategories = new Dictionary<long, GameItemCategories>();
        static public Dictionary<long, GameItem> DB_Items = new Dictionary<long, GameItem>();
        static public Dictionary<long, GameItemArmors> DB_Item_Armors = new Dictionary<long, GameItemArmors>();
        static public Dictionary<long, GameSkills> DB_Skills = new Dictionary<long, GameSkills>();
        static public Dictionary<long, GameNPC> DB_NPCs = new Dictionary<long, GameNPC>();
        static public Dictionary<long, string> DB_Icons = new Dictionary<long, string>();
        static public Dictionary<long, GameSkillItems> DB_Skill_Reagents = new Dictionary<long, GameSkillItems>();
        static public Dictionary<long, GameSkillItems> DB_Skill_Products = new Dictionary<long, GameSkillItems>();
        static public Dictionary<long, GameZone> DB_Zones = new Dictionary<long, GameZone>();
        static public Dictionary<long, GameZone_Groups> DB_Zone_Groups = new Dictionary<long, GameZone_Groups>();
        static public Dictionary<long, GameWorld_Groups> DB_World_Groups = new Dictionary<long, GameWorld_Groups>();
        static public Dictionary<long, GameSystemFaction> DB_GameSystem_Factions = new Dictionary<long, GameSystemFaction>();
        static public Dictionary<long, GameSystemFactionRelation> DB_GameSystem_Faction_Relations = new Dictionary<long, GameSystemFactionRelation>();
        static public Dictionary<long, GameDoodad> DB_Doodad_Almighties = new Dictionary<long, GameDoodad>();
        static public Dictionary<long, GameDoodadGroup> DB_Doodad_Groups = new Dictionary<long, GameDoodadGroup>();
        static public Dictionary<long, GameDoodadFunc> DB_Doodad_Funcs = new Dictionary<long, GameDoodadFunc>();
        static public Dictionary<long, GameDoodadFuncGroup> DB_Doodad_Func_Groups = new Dictionary<long, GameDoodadFuncGroup>();
        static public Dictionary<long, GameQuestCategory> DB_Quest_Categories = new Dictionary<long, GameQuestCategory>();
        static public Dictionary<long, GameQuestContexts> DB_Quest_Contexts = new Dictionary<long, GameQuestContexts>();
        static public Dictionary<long, GameQuestAct> DB_Quest_Acts = new Dictionary<long, GameQuestAct>();
        static public Dictionary<long, GameQuestComponent> DB_Quest_Components = new Dictionary<long, GameQuestComponent>();
        static public Dictionary<long, GameTags> DB_Tags = new Dictionary<long, GameTags>();
        static public Dictionary<long, GameTaggedValues> DB_Tagged_Buffs = new Dictionary<long, GameTaggedValues>();
        static public Dictionary<long, GameTaggedValues> DB_Tagged_Items = new Dictionary<long, GameTaggedValues>();
        static public Dictionary<long, GameTaggedValues> DB_Tagged_NPCs = new Dictionary<long, GameTaggedValues>();
        static public Dictionary<long, GameTaggedValues> DB_Tagged_Skills = new Dictionary<long, GameTaggedValues>();
        static public Dictionary<long, GameZoneGroupBannedTags> DB_Zone_Group_Banned_Tags = new Dictionary<long, GameZoneGroupBannedTags>();
        static public Dictionary<long, GameBuff> DB_Buffs = new Dictionary<long, GameBuff>();
        static public Dictionary<long, GameTransfers> DB_Transfers = new Dictionary<long, GameTransfers>();
        static public Dictionary<long, GameTransferPaths> DB_TransferPaths = new Dictionary<long, GameTransferPaths>();

        static public string GetFactionName(long faction_id, bool addID = false)
        {
            if (DB_GameSystem_Factions.TryGetValue(faction_id, out var faction))
            {
                if (addID)
                    return faction.nameLocalized + " ("+ faction_id + ")";
                else
                    return faction.nameLocalized;
            }
            else
            if (faction_id == 0)
            {
                if (addID)
                    return "None (0)";
                else
                    return "None";
            }
            else
            {
                if (addID)
                    return "FactionID " + faction_id.ToString();
                else
                    return string.Empty;
            }
        }

        static public long GetFactionHostility(long f1, long f2)
        {
            foreach (var fr in DB_GameSystem_Faction_Relations)
            {
                if ((fr.Value.faction1_id == f1) && (fr.Value.faction2_id == f2))
                {
                    return fr.Value.state_id;
                }
                if ((fr.Value.faction1_id == f2) && (fr.Value.faction2_id == f1))
                {
                    return fr.Value.state_id;
                }
            }
            return 0;
        }

        static public string GetFactionHostileName(long f)
        {
            switch (f)
            {
                case 0: return "Normal";
                case 1: return "Hostile";
                case 2: return "Neutral";
                case 3: return "Friendly";
                default:
                    return "UnknownID: " + f.ToString();
            }
        }

        static public void SetFactionRelationLabel(GameSystemFaction thisFaction, long targetFactionID, ref Label targetLabel)
        {
            var n = GetFactionHostility(thisFaction.id, targetFactionID);
            if ((n == 0) && (thisFaction.mother_id != 0))
            {
                n = GetFactionHostility(thisFaction.mother_id, targetFactionID);
            }
            if ((n == 0) && (thisFaction.diplomacy_link_id != 0))
            {
                n = GetFactionHostility(thisFaction.diplomacy_link_id, targetFactionID);
            }
            targetLabel.Text = AADB.GetFactionHostileName(n);
            switch (n)
            {
                case 0:
                    targetLabel.ForeColor = SystemColors.ControlText;
                    break;
                case 1:
                    targetLabel.ForeColor = Color.Red;
                    break;
                case 2:
                    targetLabel.ForeColor = Color.Orange;
                    break;
                case 3:
                    targetLabel.ForeColor = Color.Green;
                    break;
                default:
                    targetLabel.ForeColor = SystemColors.ControlText;
                    break;
            }

        }


        static private string FloatToCoord(double f)
        {
            var f1 = Math.Floor(f);
            f -= f1;
            f *= 60;
            var f2 = Math.Floor(f);
            f -= f2;
            f *= 60;
            var f3 = Math.Floor(f);

            return f1.ToString("0") + "°" + f2.ToString("00") + "'" + f3.ToString("00") + "\"";
        }

        static public string CoordToSextant(float x, float y)
        {
            var res = string.Empty;
            // https://www.reddit.com/r/archeage/comments/3dak17/datamining_every_location_of_everything_in/
            // (0.00097657363894522145695357130138029 * (X - Coordinate)) - 21 = (Longitude in degrees)
            // (0.00097657363894522145695357130138029 * (Y - Coordinate)) - 28 = (Latitude in degrees)

            var fx = (0.00097657363894522145695357130138029f * x) - 21f;
            var fy = (0.00097657363894522145695357130138029f * y) - 28f;
            // X - Longitude
            if (fx >= 0f)
            {
                res += "E ";
            }
            else
            {
                res += "W ";
                fx *= -1f;
            }
            res += FloatToCoord(fx);
            res += " , ";
            // Y - Latitude
            if (fy >= 0f)
            {
                res += "N ";
            }
            else
            {
                res += "S ";
                fy *= -1f;
            }
            res += FloatToCoord(fy);

            return res;
        }
    }

}
