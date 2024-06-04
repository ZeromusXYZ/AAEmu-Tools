using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBDefs;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer
{
    public partial class MainForm
    {
        private void LoadNpcs()
        {
            string sql = "SELECT * FROM npcs ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_NPCs.Clear();

                        var columnNames = reader.GetColumnNames();
                        bool hasEquip_bodies_id = (columnNames.IndexOf("equip_bodies_id") > 0);
                        bool hasMileStoneID = (columnNames.IndexOf("milestone_id") > 0);
                        bool hasComments = ((columnNames.IndexOf("comment1") > 0) &&
                                            (columnNames.IndexOf("comment2") > 0) &&
                                            (columnNames.IndexOf("comment3") > 0) &&
                                            (columnNames.IndexOf("comment_wear") > 0));
                        bool hasNpc_tendency_id = (columnNames.IndexOf("npc_tendency_id") > 0);
                        bool hasRecruiting_battle_field_id = (columnNames.IndexOf("recruiting_battle_field_id") > 0);
                        bool hasFX_scale = (columnNames.IndexOf("fx_scale") > 0);
                        bool hasTranslate = (columnNames.IndexOf("translate") > 0);

                        while (reader.Read())
                        {
                            var t = new GameNPC();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.char_race_id = GetInt64(reader, "char_race_id");
                            t.npc_grade_id = GetInt64(reader, "npc_grade_id");
                            t.npc_kind_id = GetInt64(reader, "npc_kind_id");
                            t.level = GetInt64(reader, "level");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.model_id = GetInt64(reader, "model_id");
                            t.npc_template_id = GetInt64(reader, "npc_template_id");
                            if (hasEquip_bodies_id)
                                t.equip_bodies_id = GetInt64(reader, "equip_bodies_id");
                            else
                                t.equip_bodies_id = -1;
                            t.equip_cloths_id = GetInt64(reader, "equip_cloths_id");
                            t.equip_weapons_id = GetInt64(reader, "equip_weapons_id");
                            t.skill_trainer = GetBool(reader, "skill_trainer");
                            t.ai_file_id = GetInt64(reader, "ai_file_id");
                            t.merchant = GetBool(reader, "merchant");
                            t.npc_nickname_id = GetInt64(reader, "npc_nickname_id");
                            t.auctioneer = GetBool(reader, "auctioneer");
                            t.show_name_tag = GetBool(reader, "show_name_tag");
                            t.visible_to_creator_only = GetBool(reader, "visible_to_creator_only");
                            t.no_exp = GetBool(reader, "no_exp");
                            t.pet_item_id = GetInt64(reader, "pet_item_id");
                            t.base_skill_id = GetInt64(reader, "base_skill_id");
                            t.track_friendship = GetBool(reader, "track_friendship");
                            t.priest = GetBool(reader, "priest");
                            if (hasNpc_tendency_id)
                                t.npc_tendency_id = GetInt64(reader, "npc_tendency_id");
                            else
                                t.npc_tendency_id = -1;
                            t.blacksmith = GetBool(reader, "blacksmith");
                            t.teleporter = GetBool(reader, "teleporter");
                            t.opacity = GetFloat(reader, "opacity");
                            t.ability_changer = GetBool(reader, "ability_changer");
                            t.scale = GetFloat(reader, "scale");
                            if (hasComments)
                            {
                                t.comment1 = GetString(reader, "comment1");
                                t.comment2 = GetString(reader, "comment2");
                                t.comment3 = GetString(reader, "comment3");
                                t.comment_wear = GetString(reader, "comment_wear");
                            }
                            else
                            {
                                t.comment1 = string.Empty;
                                t.comment2 = string.Empty;
                                t.comment3 = string.Empty;
                                t.comment_wear = string.Empty;
                            }

                            t.sight_range_scale = GetFloat(reader, "sight_range_scale");
                            t.sight_fov_scale = GetFloat(reader, "sight_fov_scale");
                            if (hasMileStoneID)
                                t.milestone_id = GetInt64(reader, "milestone_id");
                            else
                                t.milestone_id = -1;
                            t.attack_start_range_scale = GetFloat(reader, "attack_start_range_scale");
                            t.aggression = GetBool(reader, "aggression");
                            t.exp_multiplier = GetFloat(reader, "exp_multiplier");
                            t.exp_adder = GetInt64(reader, "exp_adder");
                            t.stabler = GetBool(reader, "stabler");
                            t.accept_aggro_link = GetBool(reader, "accept_aggro_link");
                            if (hasRecruiting_battle_field_id)
                                t.recruiting_battle_field_id = GetInt64(reader, "recruiting_battle_field_id");
                            else
                                t.recruiting_battle_field_id = -1;
                            t.return_distance = GetInt64(reader, "return_distance");
                            t.npc_ai_param_id = GetInt64(reader, "npc_ai_param_id");
                            t.non_pushable_by_actor = GetBool(reader, "non_pushable_by_actor");
                            t.banker = GetBool(reader, "banker");
                            t.aggro_link_special_rule_id = GetInt64(reader, "aggro_link_special_rule_id");
                            t.aggro_link_help_dist = GetFloat(reader, "aggro_link_help_dist");
                            t.aggro_link_sight_check = GetBool(reader, "aggro_link_sight_check");
                            t.expedition = GetBool(reader, "expedition");
                            t.honor_point = GetInt64(reader, "honor_point");
                            t.trader = GetBool(reader, "trader");
                            t.aggro_link_special_guard = GetBool(reader, "aggro_link_special_guard");
                            t.aggro_link_special_ignore_npc_attacker =
                                GetBool(reader, "aggro_link_special_ignore_npc_attacker");
                            t.absolute_return_distance = GetFloat(reader, "absolute_return_distance");
                            t.repairman = GetBool(reader, "repairman");
                            t.activate_ai_always = GetBool(reader, "activate_ai_always");
                            t.so_state = GetString(reader, "so_state");
                            t.specialty = GetBool(reader, "specialty");
                            t.sound_pack_id = GetInt64(reader, "sound_pack_id");
                            t.specialty_coin_id = GetInt64(reader, "specialty_coin_id");
                            t.use_range_mod = GetBool(reader, "use_range_mod");
                            t.npc_posture_set_id = GetInt64(reader, "npc_posture_set_id");
                            t.mate_equip_slot_pack_id = GetInt64(reader, "mate_equip_slot_pack_id");
                            t.mate_kind_id = GetInt64(reader, "mate_kind_id");
                            t.engage_combat_give_quest_id = GetInt64(reader, "engage_combat_give_quest_id");
                            t.total_custom_id = GetInt64(reader, "total_custom_id");
                            t.no_apply_total_custom = GetBool(reader, "no_apply_total_custom");
                            t.base_skill_strafe = GetBool(reader, "base_skill_strafe");
                            t.base_skill_delay = GetFloat(reader, "base_skill_delay");
                            t.npc_interaction_set_id = GetInt64(reader, "npc_interaction_set_id");
                            t.use_abuser_list = GetBool(reader, "use_abuser_list");
                            t.return_when_enter_housing_area = GetBool(reader, "return_when_enter_housing_area");
                            t.look_converter = GetBool(reader, "look_converter");
                            t.use_ddcms_mount_skill = GetBool(reader, "use_ddcms_mount_skill");
                            t.crowd_effect = GetBool(reader, "crowd_effect");
                            if (hasFX_scale)
                                t.fx_scale = GetFloat(reader, "fx_scale");
                            else
                                t.fx_scale = 1.0f;
                            if (hasTranslate)
                                t.translate = GetBool(reader, "translate");
                            else
                                t.translate = false;
                            t.no_penalty = GetBool(reader, "no_penalty");
                            t.show_faction_tag = GetBool(reader, "show_faction_tag");


                            t.nameLocalized = AADB.GetTranslationByID(t.id, "npcs", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AADB.DB_NPCs.Add(t.id, t);
                        }
                    }
                }
            }

            sql = "SELECT * FROM npc_spawner_npcs ORDER BY id ASC";

            AADB.DB_Npc_Spawner_Npcs.Clear();
            if (allTableNames.Contains("npc_spawner_npcs"))
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            // AADB.DB_Npc_Spawner_Npcs.Clear();

                            var columnNames = reader.GetColumnNames();

                            while (reader.Read())
                            {
                                var t = new GameNpcSpawnerNpc();
                                // Actual DB entries
                                t.id = GetInt64(reader, "id");
                                t.npc_spawner_id = GetInt64(reader, "npc_spawner_id");
                                t.member_id = GetInt64(reader, "member_id");
                                t.member_type = GetString(reader, "member_type");
                                t.weight = GetFloat(reader, "weight");
                                AADB.DB_Npc_Spawner_Npcs.Add(t.id, t);
                            }
                        }
                    }
                }
            }

            sql = "SELECT * FROM npc_spawners ORDER BY id ASC";

            AADB.DB_Npc_Spawners.Clear();
            if (allTableNames.Contains("npc_spawners"))
            {
                using (var connection = SQLite.CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            // AADB.DB_Npc_Spawners.Clear();

                            var columnNames = reader.GetColumnNames();

                            while (reader.Read())
                            {
                                var t = new GameNpcSpawner();
                                // Actual DB entries
                                t.id = GetInt64(reader, "id");
                                t.npc_spawner_category_id = GetInt64(reader, "npc_spawner_category_id");
                                t.name = GetString(reader, "name");
                                t.comment = GetString(reader, "comment");
                                t.maxPopulation = GetInt64(reader, "maxPopulation");
                                t.startTime = GetFloat(reader, "startTime");
                                t.endTime = GetFloat(reader, "endTime");
                                t.destroyTime = GetFloat(reader, "destroyTime");
                                t.spawn_delay_min = GetFloat(reader, "spawn_delay_min");
                                t.activation_state = GetBool(reader, "activation_state");
                                t.save_indun = GetBool(reader, "save_indun");
                                t.min_population = GetInt64(reader, "min_population");
                                t.test_radius_npc = GetFloat(reader, "test_radius_npc");
                                t.test_radius_pc = GetFloat(reader, "test_radius_pc");
                                t.suspend_spawn_count = GetInt64(reader, "suspend_spawn_count");
                                t.spawn_delay_max = GetFloat(reader, "spawn_delay_max");

                                AADB.DB_Npc_Spawners.Add(t.id, t);
                            }
                        }
                    }
                }
            }

            sql = "SELECT * FROM quest_monster_groups ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Quest_Monster_Groups.Clear();

                        var readCatId = false;
                        List<string> columnNames = null;
                        while (reader.Read())
                        {
                            // category_id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readCatId = (columnNames.IndexOf("category_id") >= 0);
                            }

                            var t = new GameQuestMonsterGroups();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.category_id = readCatId ? GetInt64(reader, "category_id") : 0;
                            t.nameLocalized = AADB.GetTranslationByID(t.id, "quest_monster_groups", "name", t.name);

                            AADB.DB_Quest_Monster_Groups.Add(t.id, t);
                        }
                    }
                }
            }

            sql = "SELECT * FROM quest_monster_npcs ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Quest_Monster_Npcs.Clear();

                        while (reader.Read())
                        {
                            var t = new GameQuestMonsterNpcs();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.quest_monster_group_id = GetInt64(reader, "quest_monster_group_id");
                            t.npc_id = GetInt64(reader, "npc_id");

                            AADB.DB_Quest_Monster_Npcs.Add(t.id, t);
                        }
                    }
                }
            }

            sql = "SELECT * FROM npc_interactions ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_NpcInteractions.Clear();

                        while (reader.Read())
                        {
                            var t = new GameNpcInteractions();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.npc_interaction_set_id = GetInt64(reader, "npc_interaction_set_id");
                            t.skill_id = GetInt64(reader, "skill_id");

                            AADB.DB_NpcInteractions.Add(t.id, t);
                        }
                    }
                }
            }

            sql = "SELECT * FROM ai_files ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_AiFiles.Clear();

                        while (reader.Read())
                        {
                            var t = new GameAiFiles();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.param_template = GetString(reader, "param_template");

                            AADB.DB_AiFiles.Add(t.id, t);
                        }
                    }
                }
            }

        }

        private void LoadDoodads()
        {
            // doodad_almighties
            string sql = "SELECT * FROM doodad_almighties ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Almighties.Clear();

                        var columnNames = reader.GetColumnNames();
                        bool hasMileStoneID = (columnNames.IndexOf("milestone_id") > 0);
                        bool hasTranslate = (columnNames.IndexOf("translate") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodad();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.model = GetString(reader, "model");
                            t.once_one_man = GetBool(reader, "once_one_man");
                            t.once_one_interaction = GetBool(reader, "once_one_interaction");
                            t.show_name = GetBool(reader, "show_name");
                            t.mgmt_spawn = GetBool(reader, "mgmt_spawn");
                            t.percent = GetInt64(reader, "percent");
                            t.min_time = GetInt64(reader, "min_time");
                            t.max_time = GetInt64(reader, "max_time");
                            t.model_kind_id = GetInt64(reader, "model_kind_id");
                            t.use_creator_faction = GetBool(reader, "use_creator_faction");
                            t.force_tod_top_priority = GetBool(reader, "force_tod_top_priority");
                            if (hasMileStoneID)
                                t.milestone_id = GetInt64(reader, "milestone_id");
                            else
                                t.milestone_id = -1;
                            t.group_id = GetInt64(reader, "group_id");
                            t.show_minimap = GetBool(reader, "show_minimap");
                            t.use_target_decal = GetBool(reader, "use_target_decal");
                            t.use_target_silhouette = GetBool(reader, "use_target_silhouette");
                            t.use_target_highlight = GetBool(reader, "use_target_highlight");
                            t.target_decal_size = GetFloat(reader, "target_decal_size");
                            t.sim_radius = GetInt64(reader, "sim_radius");
                            t.collide_ship = GetBool(reader, "collide_ship");
                            t.collide_vehicle = GetBool(reader, "collide_vehicle");
                            t.climate_id = GetInt64(reader, "climate_id");
                            t.save_indun = GetBool(reader, "save_indun");
                            t.mark_model = GetString(reader, "mark_model");
                            t.force_up_action = GetBool(reader, "force_up_action");
                            t.load_model_from_world = GetBool(reader, "load_model_from_world");
                            t.parentable = GetBool(reader, "parentable");
                            t.childable = GetBool(reader, "childable");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.growth_time = GetInt64(reader, "growth_time");
                            t.despawn_on_collision = GetBool(reader, "despawn_on_collision");
                            t.no_collision = GetBool(reader, "no_collision");
                            t.restrict_zone_id = GetInt64(reader, "restrict_zone_id");
                            if (hasTranslate)
                                t.translate = GetBool(reader, "translate");
                            else
                                t.translate = false;

                            // Helpers
                            t.nameLocalized = AADB.GetTranslationByID(t.id, "doodad_almighties", "name", t.name);
                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AADB.DB_Doodad_Almighties.Add(t.id, t);
                        }
                    }
                }
            }

            // doodad_groups
            sql = "SELECT * FROM doodad_groups ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();

                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Groups.Clear();

                        var columnNames = reader.GetColumnNames();
                        bool hasName = (columnNames.IndexOf("name") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodadGroup();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            if (hasName)
                                t.name = GetString(reader, "name");
                            else
                                t.name = string.Empty;
                            t.is_export = GetBool(reader, "is_export");
                            t.guard_on_field_time = GetInt64(reader, "guard_on_field_time");
                            t.removed_by_house = GetBool(reader, "removed_by_house");

                            // Helpers
                            t.nameLocalized = AADB.GetTranslationByID(t.id, "doodad_groups", "name", t.name);
                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AADB.DB_Doodad_Groups.Add(t.id, t);
                        }
                    }
                }
            }

            // doodad_funcs
            sql = "SELECT * FROM doodad_funcs ORDER BY doodad_func_group_id ASC, actual_func_id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Funcs.Clear();
                        while (reader.Read())
                        {
                            var t = new GameDoodadFunc();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.doodad_func_group_id = GetInt64(reader, "doodad_func_group_id");
                            t.actual_func_id = GetInt64(reader, "actual_func_id");
                            t.actual_func_type = GetString(reader, "actual_func_type");
                            t.next_phase = GetInt64(reader, "next_phase");
                            t.sound_id = GetInt64(reader, "sound_id");
                            t.func_skill_id = GetInt64(reader, "func_skill_id");
                            t.perm_id = GetInt64(reader, "perm_id");
                            t.act_count = GetInt64(reader, "act_count");
                            t.popup_warn = GetBool(reader, "popup_warn");
                            t.forbid_on_climb = GetBool(reader, "forbid_on_climb");

                            AADB.DB_Doodad_Funcs.Add(t.id, t);
                        }
                    }
                }
            }

            // doodad_func_groups
            sql = "SELECT * FROM doodad_func_groups ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Func_Groups.Clear();

                        var columnNames = reader.GetColumnNames();
                        bool hasComment = (columnNames.IndexOf("comment") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodadFuncGroup();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.model = GetString(reader, "model");
                            t.doodad_almighty_id = GetInt64(reader, "doodad_almighty_id");
                            t.doodad_func_group_kind_id = GetInt64(reader, "doodad_func_group_kind_id");
                            t.phase_msg = GetString(reader, "phase_msg");
                            t.sound_id = GetInt64(reader, "sound_id");
                            t.name = GetString(reader, "name");
                            t.sound_time = GetInt64(reader, "sound_time");
                            if (hasComment)
                                t.comment = GetString(reader, "comment");
                            else
                                t.comment = string.Empty;
                            t.is_msg_to_zone = GetBool(reader, "is_msg_to_zone");

                            // Helpers
                            if (t.name != string.Empty)
                                t.nameLocalized = AADB.GetTranslationByID(t.id, "doodad_func_groups", "name");
                            else
                                t.nameLocalized = "";
                            if (t.phase_msgLocalized != string.Empty)
                                t.phase_msgLocalized = AADB.GetTranslationByID(t.id, "doodad_func_groups", "phase_msg");
                            else
                                t.phase_msgLocalized = "";
                            t.SearchString = t.name + " " + t.phase_msg + " " + t.nameLocalized + " " +
                                             t.phase_msgLocalized + " " + t.comment;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Doodad_Func_Groups.Add(t.id, t);
                        }
                    }
                }
            }

            // doodad_phase_func
            sql = "SELECT * FROM doodad_phase_funcs ORDER BY id ASC";
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        AADB.DB_Doodad_Phase_Funcs.Clear();

                        while (reader.Read())
                        {
                            var t = new GameDoodadPhaseFunc();
                            // Actual DB entries
                            t.id = GetInt64(reader, "id");
                            t.doodad_func_group_id = GetInt64(reader, "doodad_func_group_id");
                            t.actual_func_id = GetInt64(reader, "actual_func_id");
                            t.actual_func_type = GetString(reader, "actual_func_type");

                            AADB.DB_Doodad_Phase_Funcs.Add(t.id, t);
                        }
                    }
                }
            }
        }

        private void LoadTransfers()
        {
            string sql = "SELECT * FROM transfers ORDER BY id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Transfers.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            GameTransfers t = new GameTransfers();
                            t.id = GetInt64(reader, "id");
                            t.model_id = GetInt64(reader, "model_id");
                            t.path_smoothing = GetFloat(reader, "path_smoothing");

                            AADB.DB_Transfers.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        private void LoadTransferPaths()
        {
            string sql = "SELECT * FROM transfer_paths ORDER BY owner_id ASC";

            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_TransferPaths.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            GameTransferPaths t = new GameTransferPaths();
                            //t.id = GetInt64(reader, "id");
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.owner_type = GetString(reader, "owner_type");
                            t.path_name = GetString(reader, "path_name");
                            t.wait_time_start = GetFloat(reader, "wait_time_start");
                            t.wait_time_end = GetFloat(reader, "wait_time_end");

                            AADB.DB_TransferPaths.Add(t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        private void LoadSlaves()
        {
            using (var connection = SQLite.CreateConnection())
            {
                // Slaves
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slaves.Clear();

                    command.CommandText = "SELECT * FROM slaves ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameSlaves();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.model_id = GetInt64(reader, "model_id");
                            t.mountable = GetBool(reader, "mountable");
                            t.offset_x = GetFloat(reader, "offset_x");
                            t.offset_y = GetFloat(reader, "offset_y");
                            t.offset_z = GetFloat(reader, "offset_z");
                            t.obb_pos_x = GetFloat(reader, "obb_pos_x");
                            t.obb_pos_y = GetFloat(reader, "obb_pos_y");
                            t.obb_pos_z = GetFloat(reader, "obb_pos_z");
                            t.obb_size_x = GetFloat(reader, "obb_size_x");
                            t.obb_size_y = GetFloat(reader, "obb_size_y");
                            t.obb_size_z = GetFloat(reader, "obb_size_z");
                            t.portal_spawn_fx_id = GetInt64(reader, "portal_spawn_fx_id");
                            t.portal_scale = GetFloat(reader, "portal_scale");
                            t.portal_time = GetFloat(reader, "portal_time");
                            t.portal_despawn_fx_id = GetInt64(reader, "portal_despawn_fx_id");
                            t.hp25_doodad_count = GetInt64(reader, "hp25_doodad_count");
                            t.hp50_doodad_count = GetInt64(reader, "hp50_doodad_count");
                            t.hp75_doodad_count = GetInt64(reader, "hp75_doodad_count");
                            t.spawn_x_offset = GetFloat(reader, "spawn_x_offset");
                            t.spawn_y_offset = GetFloat(reader, "spawn_y_offset");
                            t.faction_id = GetInt64(reader, "faction_id");
                            t.level = GetInt64(reader, "level");
                            t.cost = GetInt64(reader, "cost");
                            t.slave_kind_id = GetInt64(reader, "slave_kind_id");
                            t.spawn_valid_area_range = GetInt64(reader, "spawn_valid_area_range");
                            t.slave_initial_item_pack_id = GetInt64(reader, "slave_initial_item_pack_id");
                            t.slave_customizing_id = GetInt64(reader, "slave_customizing_id");
                            t.customizable = GetBool(reader, "customizable");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "slaves", "name", t.name);
                            t.searchText = t.name.ToLower() + " " + t.nameLocalized.ToLower();

                            AADB.DB_Slaves.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // Slave Bindings
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slave_Bindings.Clear();

                    command.CommandText = "SELECT * FROM slave_bindings ORDER BY owner_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        List<string> columnNames = null;
                        var readId = false;
                        var indx = 1L;
                        while (reader.Read())
                        {
                            // id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readId = (columnNames.IndexOf("id") >= 0);
                            }

                            var t = new GameSlaveBinding();
                            t.id = readId ? GetInt64(reader, "id") : indx;
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.owner_type = GetString(reader, "owner_type");
                            t.slave_id = GetInt64(reader, "slave_id");
                            t.attach_point_id = GetInt64(reader, "attach_point_id");

                            AADB.DB_Slave_Bindings.Add(t.id, t);
                            indx++;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // Slave Doodad Bindings
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Slave_Doodad_Bindings.Clear();

                    command.CommandText = "SELECT * FROM slave_doodad_bindings ORDER BY owner_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        List<string> columnNames = null;
                        var readId = false;
                        var indx = 1L;
                        while (reader.Read())
                        {
                            // id field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readId = (columnNames.IndexOf("id") >= 0);
                            }

                            var t = new GameSlaveDoodadBinding();
                            t.id = readId ? GetInt64(reader, "id") : indx;
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.owner_type = GetString(reader, "owner_type");
                            t.attach_point_id = GetInt64(reader, "attach_point_id");
                            t.doodad_id = GetInt64(reader, "doodad_id");
                            t.persist = GetBool(reader, "persist");
                            t.scale = GetFloat(reader, "scale");

                            AADB.DB_Slave_Doodad_Bindings.Add(t.id, t);
                            indx++;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

        }

        private void LoadModels()
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Models.Clear();

                    command.CommandText = "SELECT * FROM models ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        List<string> columnNames = null;
                        var readComment = false;
                        while (reader.Read())
                        {
                            // comment field is not present after in 3.0.3.0
                            if (columnNames == null)
                            {
                                columnNames = reader.GetColumnNames();
                                readComment = (columnNames.IndexOf("comment") >= 0);
                            }

                            var t = new GameModel();

                            t.id = GetInt64(reader, "id");
                            if (readComment)
                                t.comment = GetString(reader, "comment");
                            t.sub_id = GetInt64(reader, "sub_id");
                            t.sub_type = GetString(reader, "sub_type");
                            t.dying_time = GetFloat(reader, "dying_time");
                            t.sound_material_id = GetInt64(reader, "sound_material_id");
                            t.big = GetBool(reader, "big");
                            t.target_decal_size = GetFloat(reader, "target_decal_size");
                            t.use_target_decal = GetBool(reader, "use_target_decal");
                            t.use_target_silhouette = GetBool(reader, "use_target_silhouette");
                            t.use_target_highlight = GetBool(reader, "use_target_highlight");
                            t.name = GetString(reader, "name");
                            t.camera_distance = GetFloat(reader, "camera_distance");
                            t.show_name_tag = GetBool(reader, "show_name_tag");
                            t.name_tag_offset = GetFloat(reader, "name_tag_offset");
                            t.sound_pack_id = GetInt64(reader, "sound_pack_id");
                            t.despawn_doodad_on_collision = GetBool(reader, "despawn_doodad_on_collision");
                            t.play_mount_animation = GetBool(reader, "play_mount_animation");
                            t.selectable = GetBool(reader, "selectable");
                            t.mount_pose_id = GetInt64(reader, "mount_pose_id");
                            t.camera_distance_for_wide_angle = GetFloat(reader, "camera_distance_for_wide_angle");
                            AADB.DB_Models.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

            }

        }

        private void BtnSearchNPC_Click(object sender, EventArgs e)
        {
            string searchText = cbSearchNPC.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvNPCs.Rows.Clear();
            int c = 0;
            foreach (var t in AADB.DB_NPCs)
            {
                var z = t.Value;
                if ((z.id == searchID) || (z.model_id == searchID) || (z.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvNPCs.Rows.Add();
                    var row = dgvNPCs.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.nameLocalized;
                    row.Cells[2].Value = z.level.ToString();
                    row.Cells[3].Value = z.npc_kind_id.ToString();
                    row.Cells[4].Value = z.npc_grade_id.ToString();
                    row.Cells[5].Value = AADB.GetFactionName(z.faction_id, true);
                    row.Cells[6].Value = "???";

                    if (c == 0)
                    {
                        ShowDbNpcInfo(z.id);
                    }

                    c++;
                    if (c >= 250)
                    {
                        MessageBox.Show(
                            "The results were cut off at " + c.ToString() + " items, please refine your search !",
                            "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }

            if (c > 0)
                AddToSearchHistory(cbSearchNPC, searchText);

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void TSearchNPC_TextChanged(object sender, EventArgs e)
        {
            btnSearchNPC.Enabled = (cbSearchNPC.Text != string.Empty);
        }

        private void TSearchNPC_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSearchNPC.Enabled))
                BtnSearchNPC_Click(null, null);
        }

        private void ShowDbNpcInfo(long id)
        {
            if (AADB.DB_NPCs.TryGetValue(id, out var npc))
            {
                lNPCTemplate.Text = npc.id.ToString();
                lNPCTags.Text = TagsAsString(id, AADB.DB_Tagged_NPCs);
                tvNPCInfo.Nodes.Clear();

                if (npc.npc_nickname_id > 0)
                {
                    var nickNode = tvNPCInfo.Nodes.Add("[" + AADB.GetTranslationByID(npc.npc_nickname_id, "npc_nicknames", "name") + "]");
                    nickNode.ForeColor = Color.Yellow;
                }

                if (npc.ai_file_id > 0)
                {
                    if (AADB.DB_AiFiles.TryGetValue(npc.ai_file_id, out var aiFile))
                    {
                        var aiNode = tvNPCInfo.Nodes.Add("AI: " + aiFile.name);
                        aiNode.ForeColor = Color.White;
                        // aiNode.ImageIndex = 3;
                        // aiNode.SelectedImageIndex = aiNode.ImageIndex;
                    }
                    else
                    {
                        tvNPCInfo.Nodes.Add("AI Unknown FileId: " + npc.ai_file_id);
                    }
                }

                #region spawners
                // Spawners
                var spawnersNode = tvNPCInfo.Nodes.Add("Spawners");
                spawnersNode.ImageIndex = 1;
                spawnersNode.SelectedImageIndex = 1;
                var spawners = AADB.DB_Npc_Spawner_Npcs.Values.Where(x => x.member_type == "Npc" && x.member_id == npc.id).ToList();
                foreach (var npcSpawner in spawners)
                {
                    if (AADB.DB_Npc_Spawners.TryGetValue(npcSpawner.npc_spawner_id, out var spawner))
                    {
                        var spawnerNode = spawnersNode.Nodes.Add("ID:" + npcSpawner.npc_spawner_id + (spawner.activation_state ? " (active)" : ""));
                        spawnerNode.Nodes.Add($"Category: {spawner.npc_spawner_category_id}");
                        if (!string.IsNullOrWhiteSpace(spawner.name))
                            spawnerNode.Nodes.Add($"Name: {spawner.name}");
                        if (!string.IsNullOrWhiteSpace(spawner.comment))
                            spawnerNode.Nodes.Add($"Comment: {spawner.comment}");

                        spawnerNode.Nodes.Add($"Activation State: {spawner.activation_state}");
                        spawnerNode.Nodes.Add($"Save Indun: {spawner.save_indun}");

                        if (spawner.spawn_delay_min == spawner.spawn_delay_max)
                        {
                            spawnerNode.Nodes.Add($"Spawn Delay: {MSToString((long)spawner.spawn_delay_min * 1000, true)}");
                        }
                        else
                        {
                            spawnerNode.Nodes.Add($"Spawn Delay Min: {MSToString((long)spawner.spawn_delay_min * 1000, true)}");
                            spawnerNode.Nodes.Add($"Spawn Delay Max: {MSToString((long)spawner.spawn_delay_max * 1000, true)}");
                        }

                        if (spawner.min_population != 0)
                            spawnerNode.Nodes.Add($"Min Population: {spawner.min_population}");
                        if (spawner.maxPopulation != 1)
                            spawnerNode.Nodes.Add($"Max Population: {spawner.maxPopulation}");
                        if (spawner.startTime > 0)
                            spawnerNode.Nodes.Add($"Start Time: {spawner.startTime}");
                        if (spawner.endTime > 0)
                            spawnerNode.Nodes.Add($"End Time: {spawner.endTime}");
                        if (spawner.test_radius_npc > 0)
                            spawnerNode.Nodes.Add($"Test Radius NPC: {spawner.test_radius_npc}");
                        if (spawner.test_radius_pc > 0)
                            spawnerNode.Nodes.Add($"Test Radius PC: {spawner.test_radius_pc}");
                        if (spawner.suspend_spawn_count > 0)
                            spawnerNode.Nodes.Add($"Suspend Spawn Count: {spawner.suspend_spawn_count}");
                    }
                    else
                    {
                        spawnersNode.Nodes.Add("NOT found!:" + npcSpawner.npc_spawner_id);
                    }
                    spawnersNode.Expand();
                }
                #endregion

                #region interactions
                var interactionNode = tvNPCInfo.Nodes.Add("Interaction");
                interactionNode.ImageIndex = 2;
                interactionNode.SelectedImageIndex = 2;
                if (npc.npc_interaction_set_id > 0)
                {
                    var interactions = AADB.DB_NpcInteractions.Values.Where(x => x.npc_interaction_set_id == npc.npc_interaction_set_id).ToList();
                    foreach (var interaction in interactions)
                    {
                        AddCustomPropertyNode("skill_id", interaction.skill_id.ToString(), false, interactionNode);
                    }
                }
                if (npc.auctioneer)
                    interactionNode.Nodes.Add("Auction");
                if (npc.banker)
                    interactionNode.Nodes.Add("Warehouse");
                if (npc.blacksmith)
                    interactionNode.Nodes.Add("Blacksmith");
                if (npc.expedition)
                    interactionNode.Nodes.Add("Guild Manager");
                if (npc.merchant)
                    interactionNode.Nodes.Add("Merchant");
                if (npc.priest)
                    interactionNode.Nodes.Add("Priest");
                if (npc.repairman)
                    interactionNode.Nodes.Add("Repairs");
                if (npc.skill_trainer)
                    interactionNode.Nodes.Add("Skillmanager");
                if (npc.specialty)
                    interactionNode.Nodes.Add("Speciality");
                if (npc.stabler)
                    interactionNode.Nodes.Add("Stablemaster");
                if (npc.teleporter)
                    interactionNode.Nodes.Add("Teleporter");
                if (npc.trader)
                    interactionNode.Nodes.Add("Trader");

                if (interactionNode.Nodes.Count > 0)
                    interactionNode.Expand();
                else
                    tvNPCInfo.Nodes.Remove(interactionNode);

                #endregion

                #region skills
                // Base Skill
                if (npc.base_skill_id > 0)
                {
                    var baseSkillNode = tvNPCInfo.Nodes.Add("Base Skill");
                    baseSkillNode.ImageIndex = 2;
                    baseSkillNode.SelectedImageIndex = 2;
                    AddCustomPropertyNode("skill_id", npc.base_skill_id.ToString(), false, baseSkillNode);
                    AddCustomPropertyNode("base_skill_delay", npc.base_skill_delay.ToString(), true, baseSkillNode);
                    AddCustomPropertyNode("base_skill_strafe", npc.base_skill_strafe.ToString(), false, baseSkillNode);
                    baseSkillNode.Expand();
                }

                // NP Skills
                var npSkillsNode = tvNPCInfo.Nodes.Add("NP Skills");
                npSkillsNode.ImageIndex = 2;
                npSkillsNode.SelectedImageIndex = 2;
                var npSkills = AADB.DB_NpSkills.Values.Where(x => x.owner_id == npc.id && x.owner_type == "Npc").ToList();
                foreach (var npSkill in npSkills)
                {
                    var npSkillNode = AddCustomPropertyNode("skill_id", npSkill.skill_id.ToString(), false, npSkillsNode);
                    npSkillNode.Text += $", {npSkill.skill_use_condition_id}( {npSkill.skill_use_param1:F1} | {npSkill.skill_use_param2:F1} )";
                }
                if (npSkillsNode.Nodes.Count > 0)
                    npSkillsNode.Expand();
                else
                    tvNPCInfo.Nodes.Remove(npSkillsNode);
                #endregion

                #region initial_buffs
                var initialBuffsNode = tvNPCInfo.Nodes.Add("Initial Buffs");
                initialBuffsNode.ImageIndex = 2;
                initialBuffsNode.SelectedImageIndex = 2;
                var initialBuffs = AADB.DB_NpcInitialBuffs.Values.Where(x => x.npc_id == npc.id).ToList();
                foreach (var initialBuff in initialBuffs)
                {
                    AddCustomPropertyNode("buff_id", initialBuff.buff_id.ToString(), false, initialBuffsNode);
                }
                if (initialBuffsNode.Nodes.Count > 0)
                    initialBuffsNode.Expand();
                else
                    tvNPCInfo.Nodes.Remove(initialBuffsNode);


                #endregion

                #region loot_drops
                btnShowNpcLoot.Tag = npc.id;
                btnShowNpcLoot.Enabled = AADB.DB_Loot_Pack_Dropping_Npc.Any(pl => pl.Value.npc_id == npc.id);
                if (btnShowNpcLoot.Enabled)
                {
                    var lootNode = tvNPCInfo.Nodes.Add("Loot");
                    lootNode.ImageIndex = 2;
                    lootNode.SelectedImageIndex = 2;

                    var allPacksForNpc = AADB.DB_Loot_Pack_Dropping_Npc.Where(lp => lp.Value.npc_id == npc.id).ToList();
                    var nonDefaultPackCount = allPacksForNpc.Count(p => p.Value.default_pack == false);

                    // GroupId, (List<GameLoot>, TotalValue)
                    var resultLootGroups = new Dictionary<long, List<GameLoot>>();

                    // Check all loot connected to this NPC
                    foreach (var (_, pack) in allPacksForNpc)
                    {
                        var lootPacks = AADB.DB_Loots.Values.Where(x => x.loot_pack_id == pack.loot_pack_id);
                        if ((lootPacks == null) || (lootPacks.Count() <= 0))
                        {
                            // Pack does not exist
                            lootNode.Nodes.Add($"Missing loot_pack_id {pack.loot_pack_id}");
                            continue;
                        }

                        foreach (var lootPack in lootPacks)
                        {
                            if (!resultLootGroups.TryGetValue(lootPack.group, out var lootGroup))
                            {
                                lootGroup = new List<GameLoot>();
                                resultLootGroups.Add(lootPack.group, lootGroup);
                            }
                            lootGroup.Add(lootPack);
                        }
                    }

                    var groupKeys = resultLootGroups.Keys.ToList();
                    groupKeys.Sort();

                    // Create group nodes
                    var groupNodes = new Dictionary<long, TreeNode>();
                    foreach (var groupId in groupKeys)
                    {
                        var groupName = $"Group {groupId}";
                        if (groupId == 0)
                            groupName = "Quest Items";
                        if (groupId == 1)
                            groupName = "Always Drop";
                        groupNodes.Add(groupId, lootNode.Nodes.Add(groupName));
                    }

                    // List each item and sort by group
                    foreach (var lootGroup in resultLootGroups)
                    {
                        if (!groupNodes.TryGetValue(lootGroup.Key, out var groupNode))
                            continue;

                        var totalWeight = lootGroup.Value.Sum(x => x.drop_rate);
                        if (totalWeight <= 0)
                            totalWeight = 1;

                        groupNode.Text += $" (Weight {totalWeight})";

                        foreach (var loot in lootGroup.Value)
                        {
                            var baseDropRate = loot.group switch
                            {
                                1 => 1f / (float)lootGroup.Value.Count,
                                4 => 1f,
                                _ => loot.drop_rate / (float)totalWeight,
                            };
                            if (loot.drop_rate == 1)
                                baseDropRate = 1f;

                            var lootGroupData = AADB.DB_Loot_Groups.Values.FirstOrDefault(x => x.pack_id == loot.loot_pack_id && x.group_no == loot.group);

                            var groupDropRate = 1f;
                            if (lootGroupData != null)
                            {
                                switch (loot.group)
                                {
                                    case 1:
                                        groupDropRate = 1f;
                                        break;
                                    case 4:
                                        groupDropRate = (lootGroupData.drop_rate / 10_000_000f);
                                        break;
                                    default:
                                        groupDropRate = (lootGroupData.drop_rate / 10_000f);
                                        break;
                                }
                            }
                            if (groupDropRate > 1f)
                                groupDropRate = 1f;

                            var dropRate = baseDropRate * groupDropRate;

                            var visibleRate = dropRate * 100f;

                            var itemNode = AddCustomPropertyNode("item_id", loot.item_id.ToString(), false, groupNode);

                            if (visibleRate > 50f)
                                itemNode.Text = $"{visibleRate:F0}% {itemNode.Text}";
                            else
                            if (visibleRate > 5f)
                                itemNode.Text = $"{visibleRate:F1}% {itemNode.Text}";
                            else
                                itemNode.Text = $"{visibleRate:F2}% {itemNode.Text}";
                        }

                    }

                    lootNode.ExpandAll();
                    if (cbNpcCollapseLoot.Checked)
                        lootNode.Collapse();
                }
                #endregion

                #region events
                var eventSpawns = AADB.DB_TowerDefProgSpawnTargets.Values.Where(x => x.spawn_target_type == "NpcSpawner" && spawners.Select(s => s.npc_spawner_id).Contains(x.spawn_target_id));
                var eventMainSpawns = AADB.DB_TowerDefs.Values.Where(x => spawners.Select(s => s.npc_spawner_id).Contains(x.target_npc_spawner_id));
                if (eventSpawns.Any() || eventMainSpawns.Any())
                {
                    var eventNode = tvNPCInfo.Nodes.Add("Event Spawns");
                    foreach (var eventMainSpawn in eventMainSpawns)
                    {
                        eventNode.Nodes.Add($"Spawner: {eventMainSpawn.target_npc_spawner_id}, Event: {eventMainSpawn.title_msgLocalized}");
                    }

                    foreach (var eventSpawn in eventSpawns)
                    {
                        if (AADB.DB_TowerDefProgs.TryGetValue(eventSpawn.tower_def_prog_id, out var eventProg))
                        {
                            if (AADB.DB_TowerDefs.TryGetValue(eventProg.tower_def_id, out var eventTowerDef))
                            {
                                eventNode.Nodes.Add($"{eventTowerDef.title_msgLocalized} ({eventTowerDef.id}) - {eventProg.msgLocalized} ({eventProg.id})");
                            }
                            else
                            {
                                eventNode.Nodes.Add($"Unknown TowerDef: {eventProg.tower_def_id}, Prog: {eventSpawn.tower_def_prog_id}");
                            }
                        }
                        else
                        {
                            eventNode.Nodes.Add($"Unknown TowerDefProg: {eventSpawn.tower_def_prog_id}");
                        }

                    }
                }

                var eventKills = AADB.DB_TowerDefProgKillTargets.Values.Where(x => x.kill_target_type == "Npc" && x.kill_target_id == npc.id);
                var eventMainKills = AADB.DB_TowerDefs.Values.Where(x => x.kill_npc_id == npc.id);
                if (eventKills.Any() || eventMainKills.Any())
                {
                    var killNode = tvNPCInfo.Nodes.Add("Event Kills");
                    foreach (var eventMainKill in eventMainKills)
                    {
                        killNode.Nodes.Add($"{eventMainKill.kill_npc_count} x {eventMainKill.title_msgLocalized}");
                    }

                    foreach (var eventKill in eventKills)
                    {
                        if (AADB.DB_TowerDefProgs.TryGetValue(eventKill.tower_def_prog_id, out var eventProg))
                        {
                            if (AADB.DB_TowerDefs.TryGetValue(eventProg.tower_def_id, out var eventTowerDef))
                            {
                                killNode.Nodes.Add($"{eventKill.kill_count} x {eventTowerDef.title_msgLocalized} ({eventTowerDef.id}) - {eventProg.msgLocalized} ({eventProg.id})");
                            }
                            else
                            {
                                killNode.Nodes.Add($"Unknown TowerDef: {eventProg.tower_def_id}, Prog: {eventKill.tower_def_prog_id}");
                            }
                        }
                        else
                        {
                            killNode.Nodes.Add($"Unknown TowerDefProg: {eventKill.tower_def_prog_id}");
                        }

                    }
                }
                #endregion

                ShowSelectedData("npcs", "(id = " + id.ToString() + ")", "id ASC");
                btnShowNPCsOnMap.Tag = npc.id;
            }
            else
            {
                lNPCTemplate.Text = "???";
                lNPCTags.Text = "???";
                tvNPCInfo.Nodes.Clear();
                btnShowNPCsOnMap.Tag = 0;
                btnShowNpcLoot.Tag = 0;
                btnShowNpcLoot.Enabled = false;
            }

            lGMNPCSpawn.Text = "/spawn npc " + lNPCTemplate.Text;
        }

        private void DgvNpcs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNPCs.SelectedRows.Count <= 0)
                return;
            var row = dgvNPCs.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;


            long id = -1;
            if (row.Cells[0].Value != null)
                if (long.TryParse(row.Cells[0].Value.ToString(), out var i))
                    id = i;
            if (id > 0)
                ShowDbNpcInfo(id);
        }

        private void TSearchDoodads_TextChanged(object sender, EventArgs e)
        {
            btnSearchDoodads.Enabled = (cbSearchDoodads.Text != string.Empty);
        }

        private void TSearchDoodads_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearchDoodads_Click(null, null);
        }

        private void BtnSearchDoodads_Click(object sender, EventArgs e)
        {
            string searchText = cbSearchDoodads.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            bool first = true;
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvDoodads.Rows.Clear();
            int c = 0;
            foreach (var t in AADB.DB_Doodad_Almighties)
            {
                var z = t.Value;
                if ((z.id == searchID) || (z.group_id == searchID) || (z.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvDoodads.Rows.Add();
                    var row = dgvDoodads.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.nameLocalized;
                    row.Cells[2].Value = z.mgmt_spawn.ToString();
                    if (AADB.DB_Doodad_Groups.TryGetValue(z.group_id, out var dGroup))
                    {
                        row.Cells[3].Value = dGroup.nameLocalized + " (" + z.group_id.ToString() + ")";
                    }
                    else
                    {
                        row.Cells[3].Value = z.group_id.ToString();
                    }

                    row.Cells[4].Value = z.percent.ToString();
                    if (z.faction_id != 0)
                        row.Cells[5].Value = AADB.GetFactionName(z.faction_id, true);
                    else
                        row.Cells[5].Value = string.Empty;
                    row.Cells[6].Value = z.model_kind_id.ToString();

                    if (first)
                    {
                        first = false;
                        ShowDbDoodad(z.id);
                    }

                    c++;
                    if (c >= 250)
                    {
                        MessageBox.Show(
                            "The results were cut off at " + c.ToString() + " doodads, please refine your search !",
                            "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }

            if (c > 0)
                AddToSearchHistory(cbSearchDoodads, searchText);

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void DgvDoodads_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoodads.SelectedRows.Count <= 0)
                return;
            var row = dgvDoodads.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var id = row.Cells[0].Value;
            if (id != null)
            {
                ShowDbDoodad(long.Parse(id.ToString()));
                ShowSelectedData("doodad_almighties", "(id = " + id.ToString() + ")", "id ASC");
            }

        }

        private void ShowDbDoodad(long id)
        {
            if (AADB.DB_Doodad_Almighties.TryGetValue(id, out var doodad))
            {
                lDoodadID.Text = doodad.id.ToString();
                lDoodadName.Text = doodad.nameLocalized;
                lDoodadModel.Text = doodad.model;
                lDoodadOnceOneMan.Text = doodad.once_one_man.ToString();
                lDoodadOnceOneInteraction.Text = doodad.once_one_interaction.ToString();
                lDoodadShowName.Text = doodad.show_name.ToString();
                lDoodadMgmtSpawn.Text = doodad.mgmt_spawn.ToString();
                lDoodadPercent.Text = doodad.percent.ToString();
                lDoodadMinTime.Text = MSToString(doodad.min_time);
                lDoodadMaxTime.Text = MSToString(doodad.max_time);
                lDoodadModelKindID.Text = doodad.model_kind_id.ToString();
                lDoodadUseCreatorFaction.Text = doodad.use_creator_faction.ToString();
                lDoodadForceToDTopPriority.Text = doodad.force_tod_top_priority.ToString();
                lDoodadMilestoneID.Text = doodad.milestone_id.ToString();
                lDoodadGroupID.Text = doodad.group_id.ToString();
                lDoodadShowName.Text = doodad.show_minimap.ToString();
                lDoodadUseTargetDecal.Text = doodad.use_target_decal.ToString();
                lDoodadUseTargetSilhouette.Text = doodad.use_target_silhouette.ToString();
                lDoodadUseTargetHighlight.Text = doodad.use_target_highlight.ToString();
                lDoodadTargetDecalSize.Text = doodad.target_decal_size.ToString();
                lDoodadSimRadius.Text = RangeToString(doodad.sim_radius);
                lDoodadCollideShip.Text = doodad.collide_ship.ToString();
                lDoodadCollideVehicle.Text = doodad.collide_vehicle.ToString();
                lDoodadClimateID.Text = doodad.climate_id.ToString();
                lDoodadSaveIndun.Text = doodad.save_indun.ToString();
                lDoodadMarkModel.Text = doodad.mark_model.ToString();
                lDoodadForceUpAction.Text = doodad.force_up_action.ToString();
                lDoodadLoadModelFromWorld.Text = doodad.load_model_from_world.ToString();
                lDoodadParentable.Text = doodad.parentable.ToString();
                lDoodadChildable.Text = doodad.childable.ToString();
                lDoodadFactionID.Text = AADB.GetFactionName(doodad.faction_id, true);
                lDoodadGrowthTime.Text = MSToString(doodad.growth_time);
                lDoodadDespawnOnCollision.Text = doodad.despawn_on_collision.ToString();
                lDoodadNoCollision.Text = doodad.no_collision.ToString();
                lDoodadRestrictZoneID.Text = doodad.restrict_zone_id.ToString();
                btnShowDoodadOnMap.Tag = doodad.id;

                if (AADB.DB_Doodad_Groups.TryGetValue(doodad.group_id, out var dGroup))
                {
                    lDoodadGroupName.Text = dGroup.nameLocalized + " (" + doodad.group_id.ToString() + ")";
                    lDoodadGroupIsExport.Text = dGroup.is_export.ToString();
                    lDoodadGroupGuardOnFieldTime.Text = SecondsToString(dGroup.guard_on_field_time);
                    lDoodadGroupRemovedByHouse.Text = dGroup.removed_by_house.ToString();
                }
                else
                {
                    lDoodadGroupName.Text = "<none>";
                    lDoodadGroupIsExport.Text = "";
                    lDoodadGroupGuardOnFieldTime.Text = "";
                    lDoodadGroupRemovedByHouse.Text = "";
                }

                bool firstFuncGroup = true;
                dgvDoodadFuncGroups.Rows.Clear();
                foreach (var f in AADB.DB_Doodad_Func_Groups)
                {
                    var dFuncGroup = f.Value;
                    if (dFuncGroup.doodad_almighty_id == doodad.id)
                    {
                        GameDoodadPhaseFunc dPhaseFunc = null;
                        foreach (var dpf in AADB.DB_Doodad_Phase_Funcs)
                            if (dpf.Value.doodad_func_group_id == dFuncGroup.id)
                            {
                                dPhaseFunc = dpf.Value;
                                break;
                            }


                        var line = dgvDoodadFuncGroups.Rows.Add();
                        var row = dgvDoodadFuncGroups.Rows[line];

                        row.Cells[0].Value = dFuncGroup.id.ToString();
                        row.Cells[1].Value = dFuncGroup.doodad_func_group_kind_id.ToString();
                        row.Cells[2].Value = dPhaseFunc?.actual_func_id.ToString() ?? "-";
                        row.Cells[3].Value = dPhaseFunc?.actual_func_type ?? "none";

                        if (firstFuncGroup)
                        {
                            firstFuncGroup = false;
                            ShowDbDoodadFuncGroup(dFuncGroup.id);
                        }
                    }
                }

                // Details Tab
                tvDoodadDetails.Nodes.Clear();
                var rootNode = tvDoodadDetails.Nodes.Add(doodad.nameLocalized + " ( " + doodad.id + " )");
                rootNode.ForeColor = Color.White;
                foreach (var f in AADB.DB_Doodad_Func_Groups)
                {
                    var dFuncGroup = f.Value;
                    if (dFuncGroup.doodad_almighty_id == doodad.id)
                    {
                        var doodadGroupName = "Group: " + dFuncGroup.id.ToString() + " - Kind: " + dFuncGroup.doodad_func_group_kind_id.ToString() + " - " + dFuncGroup.nameLocalized;
                        var groupNode = rootNode.Nodes.Add(doodadGroupName);
                        groupNode.ForeColor = Color.LightCyan;

                        // Phase Funcs
                        foreach (var dpf in AADB.DB_Doodad_Phase_Funcs)
                            if (dpf.Value.doodad_func_group_id == dFuncGroup.id)
                            {
                                var phaseNode = groupNode.Nodes.Add("PhaseFuncs: " + dpf.Value.actual_func_type +
                                                                    " ( " +
                                                                    dpf.Value.actual_func_id + " )");
                                phaseNode.ForeColor = Color.Yellow;
                                var tableName = FunctionTypeToTableName(dpf.Value.actual_func_type);
                                var fieldsList = GetCustomTableValues(tableName, "id",
                                    dpf.Value.actual_func_id.ToString());
                                foreach (var fields in fieldsList)
                                {
                                    foreach (var fl in fields)
                                    {
                                        AddCustomPropertyNode(fl.Key, fl.Value, cbDoodadWorkflowHideEmpty.Checked,
                                            phaseNode);
                                        /*
                                        if (cbDoodadWorkflowHideEmpty.Checked && (string.IsNullOrWhiteSpace(fl.Value) || (fl.Value == "0") || (fl.Value == "<null>") || (fl.Value == "f")))
                                        {
                                            // ignore empty values
                                        }
                                        else
                                        {
                                            phaseNode.Nodes.Add(AddCustomPropertyInfo(fl.Key, fl.Value));
                                        }
                                        */
                                    }
                                }

                                phaseNode.Collapse();
                            }

                        // doodad_funcs
                        var funcFieldsList =
                            GetCustomTableValues("doodad_funcs", "doodad_func_group_id", dFuncGroup.id.ToString());
                        foreach (var funcFields in funcFieldsList)
                        {
                            var funcsNode = groupNode.Nodes.Add(funcFields.Count > 0
                                ? "Funcs: " + funcFields["actual_func_type"] + " ( " + funcFields["actual_func_id"] +
                                  " )"
                                : "Funcs");
                            funcsNode.ForeColor = Color.LimeGreen;
                            var tableName = FunctionTypeToTableName(funcFields["actual_func_type"]);
                            var fieldsList = GetCustomTableValues(tableName, "id", funcFields["actual_func_id"]);
                            foreach (var fl in funcFields)
                            {
                                if ((fl.Key == "actual_func_type") || (fl.Key == "actual_func_id"))
                                    continue;

                                AddCustomPropertyNode(fl.Key, fl.Value, cbDoodadWorkflowHideEmpty.Checked, funcsNode);
                                /*
                                if (cbDoodadWorkflowHideEmpty.Checked && IsCustomPropertyEmpty(fl.Value))
                                {
                                    // ignore empty values
                                }
                                else
                                {
                                    funcsNode.Nodes.Add(AddCustomPropertyInfo(fl.Key, fl.Value));
                                }
                                */
                            }

                            funcsNode.Nodes.Add("<=== details ===>").ForeColor = Color.Gray;
                            foreach (var fields in fieldsList)
                            {
                                foreach (var fl in fields)
                                {
                                    AddCustomPropertyNode(fl.Key, fl.Value, cbDoodadWorkflowHideEmpty.Checked,
                                        funcsNode);
                                    /*
                                    if (cbDoodadWorkflowHideEmpty.Checked && IsCustomPropertyEmpty(fl.Value))
                                    {
                                        // ignore empty values
                                    }
                                    else
                                    {
                                        funcsNode.Nodes.Add(AddCustomPropertyInfo(fl.Key, fl.Value));
                                    }
                                    */
                                }
                            }

                            funcsNode.Collapse();
                            if (funcsNode.Nodes.Count <= 0)
                                groupNode.Nodes.Remove(funcsNode);
                        }

                        groupNode.Expand();
                    }
                }

                rootNode.Expand();

            }
            else
            {
                // blank
                lDoodadID.Text = "0";
                lDoodadName.Text = "<none>";
                lDoodadModel.Text = "<none>";
                lDoodadOnceOneMan.Text = "";
                lDoodadOnceOneInteraction.Text = "";
                lDoodadShowName.Text = "";
                lDoodadMgmtSpawn.Text = "";
                lDoodadPercent.Text = "";
                lDoodadMinTime.Text = "";
                lDoodadMaxTime.Text = "";
                lDoodadModelKindID.Text = "";
                lDoodadUseCreatorFaction.Text = "";
                lDoodadForceToDTopPriority.Text = "";
                lDoodadMilestoneID.Text = "";
                lDoodadGroupID.Text = "";
                lDoodadShowName.Text = "";
                lDoodadUseTargetDecal.Text = "";
                lDoodadUseTargetSilhouette.Text = "";
                lDoodadUseTargetHighlight.Text = "";
                lDoodadTargetDecalSize.Text = "";
                lDoodadSimRadius.Text = "";
                lDoodadCollideShip.Text = "";
                lDoodadCollideVehicle.Text = "";
                lDoodadClimateID.Text = "";
                lDoodadSaveIndun.Text = "";
                lDoodadMarkModel.Text = "";
                lDoodadForceUpAction.Text = "";
                lDoodadLoadModelFromWorld.Text = "";
                lDoodadParentable.Text = "";
                lDoodadChildable.Text = "";
                lDoodadFactionID.Text = "";
                lDoodadGrowthTime.Text = "";
                lDoodadDespawnOnCollision.Text = "";
                lDoodadNoCollision.Text = "";
                lDoodadRestrictZoneID.Text = "";

                lDoodadGroupName.Text = "<none>";
                lDoodadGroupIsExport.Text = "";
                lDoodadGroupGuardOnFieldTime.Text = "";
                lDoodadGroupRemovedByHouse.Text = "";

                btnShowDoodadOnMap.Tag = 0;

                dgvDoodadFuncGroups.Rows.Clear();

                tvDoodadDetails.Nodes.Clear();
            }
        }

        private void ShowDbDoodadFuncGroup(long id)
        {
            if (AADB.DB_Doodad_Func_Groups.TryGetValue(id, out var dfg))
            {
                // DoodadFuncGroup
                lDoodadFuncGroupID.Text = dfg.id.ToString();
                lDoodadFuncGroupModel.Text = dfg.model;
                lDoodadFuncGroupKindID.Text = dfg.doodad_func_group_kind_id.ToString();
                lDoodadFuncGroupPhaseMsg.Text = dfg.phase_msgLocalized;
                lDoodadFuncGroupSoundID.Text = dfg.sound_id.ToString();
                lDoodadFuncGroupName.Text = dfg.nameLocalized;
                lDoodadFuncGroupSoundTime.Text = MSToString(dfg.sound_time);
                lDoodadFuncGroupComment.Text = dfg.comment;
                lDoodadFuncGroupIsMsgToZone.Text = dfg.is_msg_to_zone.ToString();

                //lDoodadPhaseFuncsId.Text = "";
                lDoodadPhaseFuncsActualId.Text = "";
                lDoodadPhaseFuncsActualType.Text = "";
                foreach (var dpf in AADB.DB_Doodad_Phase_Funcs)
                    if (dpf.Value.doodad_func_group_id == dfg.id)
                    {
                        //lDoodadPhaseFuncsId.Text = dpf.Value.id.ToString();
                        lDoodadPhaseFuncsActualId.Text = dpf.Value.actual_func_id.ToString();
                        lDoodadPhaseFuncsActualType.Text = dpf.Value.actual_func_type;
                        break;
                    }
            }
            else
            {
                // blank
                lDoodadFuncGroupID.Text = "0";
                lDoodadFuncGroupModel.Text = "<none>";
                lDoodadFuncGroupKindID.Text = "";
                lDoodadFuncGroupPhaseMsg.Text = "";
                lDoodadFuncGroupSoundID.Text = "";
                lDoodadFuncGroupName.Text = "";
                lDoodadFuncGroupSoundTime.Text = "";
                lDoodadFuncGroupComment.Text = "";
                lDoodadFuncGroupIsMsgToZone.Text = "";

                //lDoodadPhaseFuncsId.Text = "";
                lDoodadPhaseFuncsActualId.Text = "";
                lDoodadPhaseFuncsActualType.Text = "";
            }
        }

        private void DgvDoodadFuncGroups_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoodadFuncGroups.SelectedRows.Count <= 0)
                return;
            var row = dgvDoodadFuncGroups.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var id = row.Cells[0].Value;
            if (id != null)
            {
                ShowDbDoodadFuncGroup(long.Parse(id.ToString()));
            }

        }

        private void TvDoodadDetails_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void CbDoodadWorkflowHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (long.TryParse(lDoodadID.Text, out var id))
                ShowDbDoodad(id);
        }

        private void BtnSearchSlave_Click(object sender, EventArgs e)
        {
            string searchText = tSearchSlave.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            dgvSlaves.Rows.Clear();
            int c = 0;
            foreach (var t in AADB.DB_Slaves)
            {
                var z = t.Value;
                var modelDetails = string.Empty;
                if (AADB.DB_Models.TryGetValue(z.model_id, out var model))
                    modelDetails = model.sub_type + " " + model.sub_id.ToString() + " - " + model.name;

                if (
                    (z.id == searchID) ||
                    (z.model_id == searchID) ||
                    (z.searchText.IndexOf(searchText) >= 0) ||
                    (modelDetails.IndexOf(searchText) >= 0)
                )
                {
                    var line = dgvSlaves.Rows.Add();
                    var row = dgvSlaves.Rows[line];

                    row.Cells[0].Value = z.id.ToString();
                    row.Cells[1].Value = z.nameLocalized;
                    row.Cells[2].Value = z.model_id.ToString();
                    row.Cells[3].Value = z.level.ToString();
                    row.Cells[4].Value = AADB.GetFactionName(z.faction_id, true);
                    row.Cells[5].Value = modelDetails;

                    if (c == 0)
                    {
                        ShowDbSlaveInfo(z.id);
                    }

                    c++;
                    if (c >= 250)
                    {
                        MessageBox.Show(
                            "The results were cut off at " + c.ToString() + " items, please refine your search !",
                            "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void TSearchSlave_TextChanged(object sender, EventArgs e)
        {
            btnSearchSlave.Enabled = !string.IsNullOrEmpty(tSearchSlave.Text);
        }

        private void TSearchSlave_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (btnSearchSlave.Enabled))
                BtnSearchSlave_Click(null, null);
        }

        private void ShowDbSlaveInfo(long id)
        {
            if (AADB.DB_Slaves.TryGetValue(id, out var slave))
            {
                lSlaveTemplate.Text = slave.id.ToString();
                lSlaveName.Text = slave.nameLocalized;
                ShowSelectedData("slaves", "(id = " + id.ToString() + ")", "id ASC");

                tvSlaveInfo.Nodes.Clear();

                // Passive Buffs
                var slavePassiveBuff = AADB.DB_Slave_Passive_Buffs.Values.FirstOrDefault(x => x.owner_type == "Slave" && x.owner_id == slave.id);
                if ((slavePassiveBuff != null) && AADB.DB_Passive_Buffs.TryGetValue(slavePassiveBuff.passive_buff_id, out var passiveBuff))
                {
                    var passiveNode = tvSlaveInfo.Nodes.Add("Passive Buffs");
                    AddCustomPropertyNode("buff_id", passiveBuff.buff_id.ToString(), false, passiveNode);
                }

                // Initial Buffs
                var slaveInitialBuffs = AADB.DB_Slave_Initial_Buffs.Values.Where(x => x.slave_id == slave.id).ToList();
                if (slaveInitialBuffs.Count > 0)
                {
                    var initialNode = tvSlaveInfo.Nodes.Add("Initial Buffs");
                    foreach (var initialBuff in slaveInitialBuffs)
                    {
                        AddCustomPropertyNode("buff_id", initialBuff.buff_id.ToString(), false, initialNode);
                    }
                }

                // Skills
                var slaveMountSkills = AADB.DB_Slave_Mount_Skills.Values.Where(x => x.slave_id == slave.id).ToList();
                if (slaveMountSkills.Count > 0)
                {
                    var skillNode = tvSlaveInfo.Nodes.Add("Skills");
                    foreach (var slaveMountSkill in slaveMountSkills)
                    {
                        if (!AADB.DB_Mount_Skills.TryGetValue(slaveMountSkill.mount_skill_id, out var mountSkill))
                            continue;
                        AddCustomPropertyNode("skill_id", mountSkill.skill_id.ToString(), false, skillNode);
                    }
                }

                // Bindings
                var slaveBindings = AADB.DB_Slave_Bindings.Values.Where(x => x.owner_type == "Slave" && x.owner_id == slave.id).ToList();
                var slaveDoodadBindings = AADB.DB_Slave_Doodad_Bindings.Values.Where(x => x.owner_type == "Slave" && x.owner_id == slave.id).ToList();
                if ((slaveBindings.Count > 0) || (slaveDoodadBindings.Count > 0))
                {
                    var bindingNode = tvSlaveInfo.Nodes.Add("Bindings");
                    foreach (var slaveBinding in slaveBindings)
                    {
                        var n = AddCustomPropertyNode("slave_id", slaveBinding.slave_id.ToString(), false, bindingNode);
                        n.Text = $" @{slaveBinding.attach_point_id}: {n.Text}";
                    }
                    foreach (var slaveDoodadBinding in slaveDoodadBindings)
                    {
                        var n = AddCustomPropertyNode("doodad_id", slaveDoodadBinding.doodad_id.ToString(), false, bindingNode);
                        n.Text = $" @{slaveDoodadBinding.attach_point_id}: {n.Text}";
                    }
                }

                tvSlaveInfo.ExpandAll();

            }
            else
            {
                lSlaveTemplate.Text = "???";
                lSlaveName.Text = "???";
                tvSlaveInfo.Nodes.Clear();
            }
        }

        private void TvNpcInfo_DoubleClick(object sender, EventArgs e)
        {
            // In properties the node tag is used internally, so only allow this node double-click if it's not set
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void DgvSlaves_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSlaves.SelectedRows.Count <= 0)
                return;
            var row = dgvSlaves.SelectedRows[0];
            if (row.Cells.Count <= 0)
                return;

            var val = row.Cells[0].Value;
            if (val == null)
                return;

            var qid = long.Parse(val.ToString());
            ShowDbSlaveInfo(qid);
        }

        private void TvSlaveInfo_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null) && (tv.SelectedNode.Tag == null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }
    }
}
