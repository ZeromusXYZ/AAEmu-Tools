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

        // Helpers
        public string nameLocalized = string.Empty;
        public string descriptionLocalized = string.Empty;
        public string SearchString = string.Empty;
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
    }

    class GameZone_Groups
    {
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


    static class AADB
    {
        static public Dictionary<string, GameTranslation> DB_Translations = new Dictionary<string, GameTranslation>();
        static public Dictionary<long, GameItem> DB_Items = new Dictionary<long, GameItem>();
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

        static public string GetFactionName(long faction_id)
        {
            if (DB_GameSystem_Factions.TryGetValue(faction_id, out var faction))
            {
                return faction.nameLocalized;
            }
            else
            {
                return "FactionID " + faction.ToString();
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
    }

}
