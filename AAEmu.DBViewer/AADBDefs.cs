using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AAEmu.DBDefs
{
    class GameTranslation
    {
        public long idx = 0;
        public string table = string.Empty;
        public string field = string.Empty;
        public string value = string.Empty;
    }

    public enum GameItemImplId
    {
        Misc = 0,
        Weapon = 1,
        Armor = 2,
        Body = 3,
        Bag = 4,
        Housing = 5,
        HousingDecoration = 6,
        Tool = 7,
        SummonSlave = 8,
        SpawnDoodad = 9,
        AcceptQuest = 10,
        SummonMate = 11,
        Recipe = 12,
        Crafting = 13,
        Portal = 14,
        EnchantingGem = 15,
        ReportCrime = 16,
        LogicDoodad = 17,
        HasUcc = 18,
        OpenEmblemUi = 19,
        Shipyard = 20,
        Socket = 21,
        Backpack = 22,
        OpenPaper = 23,
        Accessory = 24,
        Treasure = 25,
        MusicSheet = 26,
        Dyeing = 27,
        SlaveEquipment = 28,
        GradeEnchantingSupport = 29,
        MateArmor = 30,
        Location = 31,
    }

    public enum WorldInteractionType
    {
        Looting = 0,
        Cutdown = 1,
        Seeding = 2,
        Watering = 3,
        Harvest = 4,
        Remove = 5,
        Cancel = 6,
        Error = 7,
        CheckWater = 8,
        CheckGrowth = 9,
        DigTerrain = 10,
        Spray = 11,
        LineSpray = 12,
        WaterLevel = 13,
        SummonMineSpot = 14,
        DigMine = 15,
        SummonCattle = 16,
        Shearing = 17,
        Feeding = 18,
        Use = 19,
        Butcher = 20,
        CraftStart = 21,
        CraftAct = 22,
        CraftInfo = 23,
        CraftGetItem = 24,
        CraftCancel = 25,
        DirectLoot = 26,
        Hang = 27,
        Binding = 28,
        SummonBeanstalk = 29,
        GiveQuest = 30,
        SummonDoodad = 31,
        CraftDefInteraction = 32,
        Mow = 33,
        CompleteQuest = 34,
        Building = 35,
        Dooring = 36,
        FurnitureMake = 37,
        RubberProcess = 38,
        SiegeWeaponMake = 39,
        MachineryAssemble = 40,
        ToolMake = 41,
        LumberProcess = 42,
        WeaponMake = 43,
        Tanning = 44,
        ArmorMake = 45,
        FodderMake = 46,
        DailyproductMake = 47,
        StoneProcess = 48,
        ArchiumExtract = 49,
        PotionMake = 50,
        Alchemy = 51,
        DyePurify = 52,
        Cooking = 53,
        GlassceramicMake = 54,
        OilExtract = 55,
        CostumeMake = 56,
        AccessoryMake = 57,
        BookBind = 58,
        FlourMill = 59,
        PaperMill = 60,
        SeasoningPurify = 61,
        MetalCast = 62,
        Weave = 63,
        MountMake = 64,
        PulpProcess = 65,
        GasExtract = 66,
        SkinOff = 67,
        CrystalCollect = 68,
        TreeproductCollect = 69,
        DairyCollect = 70,
        Catch = 71,
        FiberCollect = 72,
        OreMine = 73,
        RockMine = 74,
        MedicalingredientMine = 75,
        FruitPick = 76,
        DyeingredientCollect = 77,
        CropHarvest = 78,
        SeedCollect = 79,
        CerealHarvest = 80,
        SoilCollect = 81,
        SpiceCollect = 82,
        PlantCollect = 83,
        GroundBuild = 84,
        SoilFrameworkBuild = 85,
        PulpFrameworkBuild = 86,
        StoneFrameworkBuild = 87,
        InteriorFinishBuild = 88,
        ExteriorFinishBuild = 89,
        RepairHouse = 90,
        MachinePartsCollect = 91,
        MagicalEnchant = 92,
        RecoverItem = 93,
        Demolish = 94,
        CraftStartShip = 96,
        SummonDoodadWithUcc = 97,
        NaviDoodadRemove = 98,
        Throw = 99,
        Putdown = 100,
        Kick = 101,
        Grasp = 102,
        DeclareSiege = 103,
        BuySiegeTicket = 104,
        SellBackpack = 105
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
        public long impl_id = 0;

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

        public override string ToString()
        {
            return nameLocalized + " (" + id.ToString() + ")";
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
        public long plot_id = 0;

        // Helpers
        public string nameLocalized = string.Empty;
        public string descriptionLocalized = string.Empty;
        public string webDescriptionLocalized = string.Empty;
        public string SearchString = string.Empty;
    }

    class GameSkillEffects
    {
        // Actual DB entries skill_effects
        public long id = 0;
        public long skill_id = 0;
        public long effect_id = 0;
        public long weight = 0;
        public long start_level = 0;
        public long end_level = 0;
        public bool friendly = false;
        public bool non_friendly = false;
        public long target_buff_tag_id = 0;
        public long target_nobuff_tag_id = 0;
        public long source_buff_tag_id = 0;
        public long source_nobuff_tag_id = 0;
        public long chance = 0;
        public bool front = false;
        public bool back = false;
        public long target_npc_tag_id = 0;
        public long application_method_id = 0;
        public bool synergy_text = false;
        public bool consume_source_item = false;
        public long consume_item_id = 0;
        public long consume_item_count = 0;
        public bool always_hit = false;
        public long item_set_id = 0;
        public bool interaction_success_hit = false;
    }

    class GameEffects
    {
        // Actual DB entries effects
        public long id = 0;
        public long actual_id = 0;
        public string actual_type = "";
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
        private static string main_world = "main_world";

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
        public string GamePakZoneHousingXML
        {
            get
            {
                return "game/worlds/" + main_world + "/level_design/zone/" + zone_key.ToString() + "/client/housing_area.xml";
            }
        }
        public string GamePakSubZoneXML
        {
            get
            {
                return "game/worlds/" + main_world + "/level_design/zone/" + zone_key.ToString() + "/client/subzone_area.xml";
            }
        }
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

        public string GamePakZoneNPCsDat(string instanceName = "main_world")
        {
            return "game/worlds/" + instanceName + "/map_data/npc_map/" + name + ".dat";
        }

        public string GamePakZoneDoodadsDat(string instanceName = "main_world")
        {
            return "game/worlds/" + instanceName + "/map_data/doodad_map/" + name + ".dat";
        }

        public override string ToString()
        {
            return AADB.GetTranslationByID(id, "zone_groups", "display_text", name);
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

    class GameDoodadPhaseFunc
    {
        // TABLE doodad_phase_funcs
        public long id = 0;
        public long doodad_func_group_id = 0;
        public long actual_func_id = 0;
        public string actual_func_type = string.Empty;
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
        // public long milestone_id = 0;
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

    class GameQuestContextText
    {
        // TABLE quest_context_texts
        public long id = 0;
        public long quest_context_text_kind_id = 0;
        public long quest_context_id = 0;
        public string text = string.Empty;

        public string textLocalized = string.Empty;
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

    class GameQuestComponentText
    {
        // TABLE quest_component_texts
        public long id = 0;
        public long quest_component_id = 0;
        public long quest_component_text_kind_id = 0;
        public string text = string.Empty;

        public string textLocalized = string.Empty;
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

    public class GameBuffTrigger
    {
        public long id = 0;
        public long buff_id = 0;
        public long event_id = 0;
        public long effect_id = 0;
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
        public string usage = string.Empty;
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
        // public long id = 0;
        public long owner_id = 0;
        public string owner_type = string.Empty;
        public string path_name = string.Empty;
        public float wait_time_start = 0f;
        public float wait_time_end = 0f;
    }

    public class QuestSphereEntry
    {
        public string worldID = string.Empty;
        public int zoneID = -1;
        public int questID = -1;
        public int componentID = -1;
        public float X = 0.0f;
        public float Y = 0.0f;
        public float Z = 0.0f;
        public float radius = 0.0f;
    }

    public class GamePlot
    {
        public long id = 0;
        public string name = string.Empty;
        public long target_type_id = 0;
    }

    public class GamePlotEvent
    {
        public long id = 0;
        public long plot_id = 0;
        public long postion = 0;
        public string name = string.Empty;
        public long source_update_method_id = 0;
        public long target_update_method_id = 0;
        public long target_update_method_param1 = 0;
        public long target_update_method_param2 = 0;
        public long target_update_method_param3 = 0;
        public long target_update_method_param4 = 0;
        public long target_update_method_param5 = 0;
        public long target_update_method_param6 = 0;
        public long target_update_method_param7 = 0;
        public long target_update_method_param8 = 0;
        public long target_update_method_param9 = 0;
        public long tickets = 0;
        public bool aeo_diminishing = false;
    }

    public class GamePlotNextEvent
    {
        public long id = 0;
        public long event_id = 0;
        public long postion = 0;
        public long next_event_id = 0;
        public long delay = 0;
        public long speed = 0;
        // Not loading the rest for now
    }

    public class GamePlotEventCondition
    {
        public long id = 0;
        public long event_id = 0;
        public long condition_id = 0;
        public long postion = 0;
        public long source_id = 0;
        public long target_id = 0;
        public bool notify_failure = false;
    }

    public class GamePlotEffect
    {
        public long id = 0;
        public long event_id = 0;
        public long position = 0;
        public long source_id = 0;
        public long target_id = 0;
        public long actual_id = 0;
        public string actual_type = "";
    }

    public class GamePlotCondition
    {
        public long id = 0;
        public bool not_condition = false;
        public long kind_id = 0;
        public long param1 = 0;
        public long param2 = 0;
        public long param3 = 0;
    }

    public class GameNpcSpawnerNpc
    {
        public long id = 0;
        public long npc_spawner_id = 0;
        public long member_id = 0;
        public string member_type = string.Empty;
        public float weight = 0f;
    }

    public class GameNpcSpawner
    {
        public long id = 0;
        public long npc_spawner_category_id = 0;
        public string name = string.Empty;
        public string comment = string.Empty;
        public long maxPopulation = 0;
        public float startTime = 0f;
        public float endTime = 0f;
        public float destroyTime = 0f;
        public float spawn_delay_min = 0f;
        public bool activation_state = false;
        public bool save_indun = false;
        public long min_population = 0;
        public float test_radius_npc = 0f;
        public float test_radius_pc = 0f;
        public long suspend_spawn_count = 0;
        public float spawn_delay_max = 0f;
    }

    public class GameSpecialties
    {
        public long id = 0;
        public long row_zone_group_id = 0;
        public long col_zone_group_id = 0;
        public long ratio = 0;
        public long profit = 0;
        public bool vendor_exist = false;
    }

    public class GameLoot
    {
        public long id = 0;
        public long group = 0;
        public long item_id = 0;
        public long drop_rate = 0;
        public long min_amount = 1;
        public long max_amount = 1;
        public long loot_pack_id = 0;
        public long grade_id = 0;
        public bool always_drop = false;
    }

    public class GameLootPackDroppingNpc
    {
        public long id = 0;
        public long npc_id = 0;
        public long loot_pack_id = 0;
        public bool default_pack = false;
    }

    public class GameLootActAbilityGroup
    {
        public long id = 0;
        public long loot_pack_id = 0;
        public long loot_group_id = 0;
        public long max_dice = 0;
        public long min_dice = 0;
    }

    static class AADB
    {
        public static Dictionary<string, GameTranslation> DB_Translations = new Dictionary<string, GameTranslation>();
        public static Dictionary<long, GameItemCategories> DB_ItemsCategories = new Dictionary<long, GameItemCategories>();
        public static Dictionary<long, GameItem> DB_Items = new Dictionary<long, GameItem>();
        public static Dictionary<long, GameItemArmors> DB_Item_Armors = new Dictionary<long, GameItemArmors>();
        public static Dictionary<long, GameEffects> DB_Effects = new Dictionary<long, GameEffects>();
        public static Dictionary<long, GameSkills> DB_Skills = new Dictionary<long, GameSkills>();
        public static Dictionary<long, GameSkillEffects> DB_Skill_Effects = new Dictionary<long, GameSkillEffects>();
        public static Dictionary<long, GameNPC> DB_NPCs = new Dictionary<long, GameNPC>();
        public static Dictionary<long, string> DB_Icons = new Dictionary<long, string>();
        public static Dictionary<long, GameSkillItems> DB_Skill_Reagents = new Dictionary<long, GameSkillItems>();
        public static Dictionary<long, GameSkillItems> DB_Skill_Products = new Dictionary<long, GameSkillItems>();
        public static Dictionary<long, GameZone> DB_Zones = new Dictionary<long, GameZone>();
        public static Dictionary<long, GameZone_Groups> DB_Zone_Groups = new Dictionary<long, GameZone_Groups>();
        public static Dictionary<long, GameWorld_Groups> DB_World_Groups = new Dictionary<long, GameWorld_Groups>();
        public static Dictionary<long, GameSystemFaction> DB_GameSystem_Factions = new Dictionary<long, GameSystemFaction>();
        public static Dictionary<long, GameSystemFactionRelation> DB_GameSystem_Faction_Relations = new Dictionary<long, GameSystemFactionRelation>();
        public static Dictionary<long, GameDoodad> DB_Doodad_Almighties = new Dictionary<long, GameDoodad>();
        public static Dictionary<long, GameDoodadGroup> DB_Doodad_Groups = new Dictionary<long, GameDoodadGroup>();
        public static Dictionary<long, GameDoodadFunc> DB_Doodad_Funcs = new Dictionary<long, GameDoodadFunc>();
        public static Dictionary<long, GameDoodadFuncGroup> DB_Doodad_Func_Groups = new Dictionary<long, GameDoodadFuncGroup>();
        public static Dictionary<long, GameDoodadPhaseFunc> DB_Doodad_Phase_Funcs = new Dictionary<long, GameDoodadPhaseFunc>();
        public static Dictionary<long, GameQuestCategory> DB_Quest_Categories = new Dictionary<long, GameQuestCategory>();
        public static Dictionary<long, GameQuestContexts> DB_Quest_Contexts = new Dictionary<long, GameQuestContexts>();
        public static Dictionary<long, GameQuestContextText> DB_Quest_Context_Texts = new Dictionary<long, GameQuestContextText>();
        public static Dictionary<long, GameQuestAct> DB_Quest_Acts = new Dictionary<long, GameQuestAct>();
        public static Dictionary<long, GameQuestComponent> DB_Quest_Components = new Dictionary<long, GameQuestComponent>();
        public static Dictionary<long, GameQuestComponentText> DB_Quest_Component_Texts = new Dictionary<long, GameQuestComponentText>();
        public static Dictionary<long, GameTags> DB_Tags = new Dictionary<long, GameTags>();
        public static Dictionary<long, GameTaggedValues> DB_Tagged_Buffs = new Dictionary<long, GameTaggedValues>();
        public static Dictionary<long, GameTaggedValues> DB_Tagged_Items = new Dictionary<long, GameTaggedValues>();
        public static Dictionary<long, GameTaggedValues> DB_Tagged_NPCs = new Dictionary<long, GameTaggedValues>();
        public static Dictionary<long, GameTaggedValues> DB_Tagged_Skills = new Dictionary<long, GameTaggedValues>();
        public static Dictionary<long, GameZoneGroupBannedTags> DB_Zone_Group_Banned_Tags = new Dictionary<long, GameZoneGroupBannedTags>();
        public static Dictionary<long, GameBuff> DB_Buffs = new Dictionary<long, GameBuff>();
        public static Dictionary<long, GameBuffTrigger> DB_BuffTriggers = new Dictionary<long, GameBuffTrigger>();
        public static Dictionary<long, GameTransfers> DB_Transfers = new Dictionary<long, GameTransfers>();
        public static List<GameTransferPaths> DB_TransferPaths = new List<GameTransferPaths>();
        public static List<QuestSphereEntry> PAK_QuestSignSpheres = new List<QuestSphereEntry>();
        public static Dictionary<long, GamePlot> DB_Plots = new Dictionary<long, GamePlot>();
        public static Dictionary<long, GamePlotEvent> DB_Plot_Events = new Dictionary<long, GamePlotEvent>();
        public static Dictionary<long, GamePlotNextEvent> DB_Plot_Next_Events = new Dictionary<long, GamePlotNextEvent>();
        public static Dictionary<long, GamePlotEventCondition> DB_Plot_Event_Conditions = new Dictionary<long, GamePlotEventCondition>();
        public static Dictionary<long, GamePlotEffect> DB_Plot_Effects = new Dictionary<long, GamePlotEffect>();
        public static Dictionary<long, GamePlotCondition> DB_Plot_Conditions = new Dictionary<long, GamePlotCondition>();
        public static Dictionary<long, GameNpcSpawnerNpc> DB_Npc_Spawner_Npcs = new Dictionary<long, GameNpcSpawnerNpc>();
        public static Dictionary<long, GameNpcSpawner> DB_Npc_Spawners = new Dictionary<long, GameNpcSpawner>();
        public static Dictionary<long, GameSpecialties> DB_Specialities = new Dictionary<long, GameSpecialties>();
        public static Dictionary<long, GameLoot> DB_Loots = new Dictionary<long, GameLoot>();
        public static Dictionary<long, GameLootPackDroppingNpc> DB_Loot_Pack_Dropping_Npc = new Dictionary<long, GameLootPackDroppingNpc>();
        public static Dictionary<long, GameLootActAbilityGroup> DB_Loot_ActAbility_Groups = new Dictionary<long, GameLootActAbilityGroup>();

        public static string GetTranslationByID(long idx, string table, string field, string defaultValue = "$NODEFAULT")
        {
            string res = string.Empty;
            string k = table + ":" + field + ":" + idx.ToString();
            if ((DB_Translations != null) && (DB_Translations.TryGetValue(k, out GameTranslation val)))
                res = val.value;
            // If no translation found ...
            if (res == string.Empty)
            {
                if (defaultValue == "$NODEFAULT")
                    return "<NT:" + table + ":" + field + ":" + idx.ToString() + ">";
                else
                    return defaultValue;
            }
            else
            {
                return res;
            }
        }

        public static string GetFactionName(long faction_id, bool addID = false)
        {
            if (DB_GameSystem_Factions.TryGetValue(faction_id, out var faction))
            {
                if (addID)
                    return faction.nameLocalized + " (" + faction_id + ")";
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

        public static long GetFactionHostility(long f1, long f2)
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

        public static string GetFactionHostileName(long f)
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

        public static void SetFactionRelationLabel(GameSystemFaction thisFaction, long targetFactionID, ref Label targetLabel)
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


        private static string FloatToCoord(double f)
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

        public static string CoordToSextant(float x, float y)
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

        public static PointF SextantToCoord(float longitude, float latitude)
        {
            var ux = ((longitude + 21f) / 0.00097657363894522145695357130138029f);
            var uy = ((latitude + 28f) / 0.00097657363894522145695357130138029f);
            return new PointF(ux, uy);
        }

        public static GameZone GetZoneByKey(long zone_key)
        {
            foreach (var z in DB_Zones)
                if (z.Value.zone_key == zone_key)
                    return z.Value;
            return null;
        }

        public static List<GameNpcSpawnerNpc> GetNpcSpawnerNpcsByNpcId(long id)
        {
            var res = new List<GameNpcSpawnerNpc>();
            foreach (var nsn in AADB.DB_Npc_Spawner_Npcs)
            {
                if ((nsn.Value.member_id == id) && (nsn.Value.member_type.ToLower() == "npc"))
                    res.Add(nsn.Value);
            }
            return res;
        }

    }
}
