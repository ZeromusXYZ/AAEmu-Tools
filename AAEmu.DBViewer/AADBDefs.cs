using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public long faction_id = 0;
        public long model_id = 0;

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
        public long pirate_desperado = 0;
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
    }

}
