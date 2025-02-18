using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBViewer.DbDefs;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer;

public partial class MainForm
{
    private void LoadNpcs()
    {
        if (AllTableNames.GetValueOrDefault("npcs") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM npcs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var columnNames = reader.GetColumnNames();
                        var hasEquipBodiesId = (columnNames.IndexOf("equip_bodies_id") > 0);
                        var hasMileStoneId = (columnNames.IndexOf("milestone_id") > 0);
                        var hasComments = ((columnNames.IndexOf("comment1") > 0) &&
                                           (columnNames.IndexOf("comment2") > 0) &&
                                           (columnNames.IndexOf("comment3") > 0) &&
                                           (columnNames.IndexOf("comment_wear") > 0));
                        var hasNpcTendencyId = (columnNames.IndexOf("npc_tendency_id") > 0);
                        var hasRecruitingBattleFieldId = (columnNames.IndexOf("recruiting_battle_field_id") > 0);
                        var hasFxScale = (columnNames.IndexOf("fx_scale") > 0);
                        var hasTranslate = (columnNames.IndexOf("translate") > 0);

                        while (reader.Read())
                        {
                            var t = new GameNpc();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.CharRaceId = GetInt64(reader, "char_race_id");
                            t.NpcGradeId = GetInt64(reader, "npc_grade_id");
                            t.NpcKindId = GetInt64(reader, "npc_kind_id");
                            t.Level = GetInt64(reader, "level");
                            t.FactionId = GetInt64(reader, "faction_id");
                            t.ModelId = GetInt64(reader, "model_id");
                            t.NpcTemplateId = GetInt64(reader, "npc_template_id");
                            if (hasEquipBodiesId)
                                t.EquipBodiesId = GetInt64(reader, "equip_bodies_id");
                            else
                                t.EquipBodiesId = -1;
                            t.EquipClothsId = GetInt64(reader, "equip_cloths_id");
                            t.EquipWeaponsId = GetInt64(reader, "equip_weapons_id");
                            t.SkillTrainer = GetBool(reader, "skill_trainer");
                            t.AiFileId = GetInt64(reader, "ai_file_id");
                            t.Merchant = GetBool(reader, "merchant");
                            t.NpcNicknameId = GetInt64(reader, "npc_nickname_id");
                            t.Auctioneer = GetBool(reader, "auctioneer");
                            t.ShowNameTag = GetBool(reader, "show_name_tag");
                            t.VisibleToCreatorOnly = GetBool(reader, "visible_to_creator_only");
                            t.NoExp = GetBool(reader, "no_exp");
                            t.PetItemId = GetInt64(reader, "pet_item_id");
                            t.BaseSkillId = GetInt64(reader, "base_skill_id");
                            t.TrackFriendship = GetBool(reader, "track_friendship");
                            t.Priest = GetBool(reader, "priest");
                            if (hasNpcTendencyId)
                                t.NpcTendencyId = GetInt64(reader, "npc_tendency_id");
                            else
                                t.NpcTendencyId = -1;
                            t.Blacksmith = GetBool(reader, "blacksmith");
                            t.Teleporter = GetBool(reader, "teleporter");
                            t.Opacity = GetFloat(reader, "opacity");
                            t.AbilityChanger = GetBool(reader, "ability_changer");
                            t.Scale = GetFloat(reader, "scale");
                            if (hasComments)
                            {
                                t.Comment1 = GetString(reader, "comment1");
                                t.Comment2 = GetString(reader, "comment2");
                                t.Comment3 = GetString(reader, "comment3");
                                t.CommentWear = GetString(reader, "comment_wear");
                            }
                            else
                            {
                                t.Comment1 = string.Empty;
                                t.Comment2 = string.Empty;
                                t.Comment3 = string.Empty;
                                t.CommentWear = string.Empty;
                            }

                            t.SightRangeScale = GetFloat(reader, "sight_range_scale");
                            t.SightFovScale = GetFloat(reader, "sight_fov_scale");
                            if (hasMileStoneId)
                                t.MilestoneId = GetInt64(reader, "milestone_id");
                            else
                                t.MilestoneId = -1;
                            t.AttackStartRangeScale = GetFloat(reader, "attack_start_range_scale");
                            t.Aggression = GetBool(reader, "aggression");
                            t.ExpMultiplier = GetFloat(reader, "exp_multiplier");
                            t.ExpAdder = GetInt64(reader, "exp_adder");
                            t.Stabler = GetBool(reader, "stabler");
                            t.AcceptAggroLink = GetBool(reader, "accept_aggro_link");
                            if (hasRecruitingBattleFieldId)
                                t.RecruitingBattleFieldId = GetInt64(reader, "recruiting_battle_field_id");
                            else
                                t.RecruitingBattleFieldId = -1;
                            t.ReturnDistance = GetInt64(reader, "return_distance");
                            t.NpcAiParamId = GetInt64(reader, "npc_ai_param_id");
                            t.NonPushableByActor = GetBool(reader, "non_pushable_by_actor");
                            t.Banker = GetBool(reader, "banker");
                            t.AggroLinkSpecialRuleId = GetInt64(reader, "aggro_link_special_rule_id");
                            t.AggroLinkHelpDist = GetFloat(reader, "aggro_link_help_dist");
                            t.AggroLinkSightCheck = GetBool(reader, "aggro_link_sight_check");
                            t.Expedition = GetBool(reader, "expedition");
                            t.HonorPoint = GetInt64(reader, "honor_point");
                            t.Trader = GetBool(reader, "trader");
                            t.AggroLinkSpecialGuard = GetBool(reader, "aggro_link_special_guard");
                            t.AggroLinkSpecialIgnoreNpcAttacker =
                                GetBool(reader, "aggro_link_special_ignore_npc_attacker");
                            t.AbsoluteReturnDistance = GetFloat(reader, "absolute_return_distance");
                            t.Repairman = GetBool(reader, "repairman");
                            t.ActivateAiAlways = GetBool(reader, "activate_ai_always");
                            t.SoState = GetString(reader, "so_state");
                            t.Specialty = GetBool(reader, "specialty");
                            t.SoundPackId = GetInt64(reader, "sound_pack_id");
                            t.SpecialtyCoinId = GetInt64(reader, "specialty_coin_id");
                            t.UseRangeMod = GetBool(reader, "use_range_mod");
                            t.NpcPostureSetId = GetInt64(reader, "npc_posture_set_id");
                            t.MateEquipSlotPackId = GetInt64(reader, "mate_equip_slot_pack_id");
                            t.MateKindId = GetInt64(reader, "mate_kind_id");
                            t.EngageCombatGiveQuestId = GetInt64(reader, "engage_combat_give_quest_id");
                            t.TotalCustomId = GetInt64(reader, "total_custom_id");
                            t.NoApplyTotalCustom = GetBool(reader, "no_apply_total_custom");
                            t.BaseSkillStrafe = GetBool(reader, "base_skill_strafe");
                            t.BaseSkillDelay = GetFloat(reader, "base_skill_delay");
                            t.NpcInteractionSetId = GetInt64(reader, "npc_interaction_set_id");
                            t.UseAbuserList = GetBool(reader, "use_abuser_list");
                            t.ReturnWhenEnterHousingArea = GetBool(reader, "return_when_enter_housing_area");
                            t.LookConverter = GetBool(reader, "look_converter");
                            t.UseDdcmsMountSkill = GetBool(reader, "use_ddcms_mount_skill");
                            t.CrowdEffect = GetBool(reader, "crowd_effect");
                            if (hasFxScale)
                                t.FxScale = GetFloat(reader, "fx_scale");
                            else
                                t.FxScale = 1.0f;
                            if (hasTranslate)
                                t.Translate = GetBool(reader, "translate");
                            else
                                t.Translate = false;
                            t.NoPenalty = GetBool(reader, "no_penalty");
                            t.ShowFactionTag = GetBool(reader, "show_faction_tag");


                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "npcs", "name", t.Name);

                            t.SearchString = t.Name + " " + t.NameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AaDb.DbNpCs.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("npc_spawner_npcs") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM npc_spawner_npcs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameNpcSpawnerNpc();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.NpcSpawnerId = GetInt64(reader, "npc_spawner_id");
                            t.MemberId = GetInt64(reader, "member_id");
                            t.MemberType = GetString(reader, "member_type");
                            t.Weight = GetFloat(reader, "weight");
                            AaDb.DbNpcSpawnerNpcs.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("npc_spawners") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM npc_spawners ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameNpcSpawner();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.NpcSpawnerCategoryId = GetInt64(reader, "npc_spawner_category_id");
                            t.Name = GetString(reader, "name");
                            t.Comment = GetString(reader, "comment");
                            t.MaxPopulation = GetInt64(reader, "maxPopulation");
                            t.StartTime = GetFloat(reader, "startTime");
                            t.EndTime = GetFloat(reader, "endTime");
                            t.DestroyTime = GetFloat(reader, "destroyTime");
                            t.SpawnDelayMin = GetFloat(reader, "spawn_delay_min");
                            t.ActivationState = GetBool(reader, "activation_state");
                            t.SaveIndun = GetBool(reader, "save_indun");
                            t.MinPopulation = GetInt64(reader, "min_population");
                            t.TestRadiusNpc = GetFloat(reader, "test_radius_npc");
                            t.TestRadiusPc = GetFloat(reader, "test_radius_pc");
                            t.SuspendSpawnCount = GetInt64(reader, "suspend_spawn_count");
                            t.SpawnDelayMax = GetFloat(reader, "spawn_delay_max");

                            AaDb.DbNpcSpawners.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("quest_monster_groups") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_monster_groups ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        // category_id field is not present after in 3.0.3.0
                        var columnNames = reader.GetColumnNames();
                        var readCatId = (columnNames.IndexOf("category_id") >= 0);

                        while (reader.Read())
                        {
                            var t = new GameQuestMonsterGroups();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.CategoryId = readCatId ? GetInt64(reader, "category_id") : 0;
                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "quest_monster_groups", "name", t.Name);

                            AaDb.DbQuestMonsterGroups.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("quest_monster_npcs") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_monster_npcs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameQuestMonsterNpcs();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.QuestMonsterGroupId = GetInt64(reader, "quest_monster_group_id");
                            t.NpcId = GetInt64(reader, "npc_id");

                            AaDb.DbQuestMonsterNpcs.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("npc_interactions") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM npc_interactions ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameNpcInteractions();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.NpcInteractionSetId = GetInt64(reader, "npc_interaction_set_id");
                            t.SkillId = GetInt64(reader, "skill_id");

                            AaDb.DbNpcInteractions.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("ai_files") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ai_files ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameAiFiles();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.ParamTemplate = GetString(reader, "param_template");

                            AaDb.DbAiFiles.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        if (AllTableNames.GetValueOrDefault("ai_commands") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ai_commands";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var columns = reader.GetColumnNames();
                        var useId = columns.Contains("id");
                        var id = 0u;

                        while (reader.Read())
                        {
                            var t = new GameAiCommands();
                            t.Id = useId ? GetInt64(reader, "id") : id;
                            t.CmdSetId = GetInt64(reader, "cmd_set_id");
                            t.CmdId = GetInt64(reader, "cmd_id");
                            t.Param1 = GetInt64(reader, "param1");
                            t.Param2 = GetString(reader, "param2");

                            id++;
                            _ = AaDb.DbAiCommands.TryAdd(t.Id, t);
                        }
                    }
                }
            }
        }
    }

    private void LoadDoodads()
    {
        // doodad_almighties
        if (AllTableNames.GetValueOrDefault("doodad_almighties") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM doodad_almighties ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var columnNames = reader.GetColumnNames();
                        var hasMileStoneId = (columnNames.IndexOf("milestone_id") > 0);
                        var hasTranslate = (columnNames.IndexOf("translate") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodad();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.Model = GetString(reader, "model");
                            t.OnceOneMan = GetBool(reader, "once_one_man");
                            t.OnceOneInteraction = GetBool(reader, "once_one_interaction");
                            t.ShowName = GetBool(reader, "show_name");
                            t.MgmtSpawn = GetBool(reader, "mgmt_spawn");
                            t.Percent = GetInt64(reader, "percent");
                            t.MinTime = GetInt64(reader, "min_time");
                            t.MaxTime = GetInt64(reader, "max_time");
                            t.ModelKindId = GetInt64(reader, "model_kind_id");
                            t.UseCreatorFaction = GetBool(reader, "use_creator_faction");
                            t.ForceTodTopPriority = GetBool(reader, "force_tod_top_priority");
                            if (hasMileStoneId)
                                t.MilestoneId = GetInt64(reader, "milestone_id");
                            else
                                t.MilestoneId = -1;
                            t.GroupId = GetInt64(reader, "group_id");
                            t.ShowMinimap = GetBool(reader, "show_minimap");
                            t.UseTargetDecal = GetBool(reader, "use_target_decal");
                            t.UseTargetSilhouette = GetBool(reader, "use_target_silhouette");
                            t.UseTargetHighlight = GetBool(reader, "use_target_highlight");
                            t.TargetDecalSize = GetFloat(reader, "target_decal_size");
                            t.SimRadius = GetInt64(reader, "sim_radius");
                            t.CollideShip = GetBool(reader, "collide_ship");
                            t.CollideVehicle = GetBool(reader, "collide_vehicle");
                            t.ClimateId = GetInt64(reader, "climate_id");
                            t.SaveIndun = GetBool(reader, "save_indun");
                            t.MarkModel = GetString(reader, "mark_model");
                            t.ForceUpAction = GetBool(reader, "force_up_action");
                            t.LoadModelFromWorld = GetBool(reader, "load_model_from_world");
                            t.Parentable = GetBool(reader, "parentable");
                            t.Childable = GetBool(reader, "childable");
                            t.FactionId = GetInt64(reader, "faction_id");
                            t.GrowthTime = GetInt64(reader, "growth_time");
                            t.DespawnOnCollision = GetBool(reader, "despawn_on_collision");
                            t.NoCollision = GetBool(reader, "no_collision");
                            t.RestrictZoneId = GetInt64(reader, "restrict_zone_id");
                            if (hasTranslate)
                                t.Translate = GetBool(reader, "translate");
                            else
                                t.Translate = false;

                            // Helpers
                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "doodad_almighties", "name", t.Name);
                            t.SearchString = t.Name + " " + t.NameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AaDb.DbDoodadAlmighties.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        // doodad_groups
        if (AllTableNames.GetValueOrDefault("doodad_groups") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM doodad_groups ORDER BY id ASC";
                    command.Prepare();

                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var columnNames = reader.GetColumnNames();
                        bool hasName = (columnNames.IndexOf("name") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodadGroup();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            if (hasName)
                                t.Name = GetString(reader, "name");
                            else
                                t.Name = string.Empty;
                            t.IsExport = GetBool(reader, "is_export");
                            t.GuardOnFieldTime = GetInt64(reader, "guard_on_field_time");
                            t.RemovedByHouse = GetBool(reader, "removed_by_house");

                            // Helpers
                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "doodad_groups", "name", t.Name);
                            t.SearchString = t.Name + " " + t.NameLocalized;
                            t.SearchString = t.SearchString.ToLower();
                            AaDb.DbDoodadGroups.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        // doodad_funcs
        if (AllTableNames.GetValueOrDefault("doodad_funcs") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM doodad_funcs ORDER BY doodad_func_group_id ASC, actual_func_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameDoodadFunc();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.DoodadFuncGroupId = GetInt64(reader, "doodad_func_group_id");
                            t.ActualFuncId = GetInt64(reader, "actual_func_id");
                            t.ActualFuncType = GetString(reader, "actual_func_type");
                            t.NextPhase = GetInt64(reader, "next_phase");
                            t.SoundId = GetInt64(reader, "sound_id");
                            t.FuncSkillId = GetInt64(reader, "func_skill_id");
                            t.PermId = GetInt64(reader, "perm_id");
                            t.ActCount = GetInt64(reader, "act_count");
                            t.PopupWarn = GetBool(reader, "popup_warn");
                            t.ForbidOnClimb = GetBool(reader, "forbid_on_climb");

                            AaDb.DbDoodadFuncs.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        // doodad_func_groups
        if (AllTableNames.GetValueOrDefault("doodad_func_groups") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM doodad_func_groups ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var columnNames = reader.GetColumnNames();
                        bool hasComment = (columnNames.IndexOf("comment") > 0);

                        while (reader.Read())
                        {
                            var t = new GameDoodadFuncGroup();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.Model = GetString(reader, "model");
                            t.DoodadAlmightyId = GetInt64(reader, "doodad_almighty_id");
                            t.DoodadFuncGroupKindId = GetInt64(reader, "doodad_func_group_kind_id");
                            t.PhaseMsg = GetString(reader, "phase_msg");
                            t.SoundId = GetInt64(reader, "sound_id");
                            t.Name = GetString(reader, "name");
                            t.SoundTime = GetInt64(reader, "sound_time");
                            if (hasComment)
                                t.Comment = GetString(reader, "comment");
                            else
                                t.Comment = string.Empty;
                            t.IsMsgToZone = GetBool(reader, "is_msg_to_zone");

                            // Helpers
                            if (t.Name != string.Empty)
                                t.NameLocalized = AaDb.GetTranslationById(t.Id, "doodad_func_groups", "name");
                            else
                                t.NameLocalized = string.Empty;
                            if (t.PhaseMsgLocalized != string.Empty)
                                t.PhaseMsgLocalized = AaDb.GetTranslationById(t.Id, "doodad_func_groups", "phase_msg");
                            else
                                t.PhaseMsgLocalized = string.Empty;
                            t.SearchString = t.Name + " " + t.PhaseMsg + " " + t.NameLocalized + " " +
                                             t.PhaseMsgLocalized + " " + t.Comment;
                            t.SearchString = t.SearchString.ToLower();

                            AaDb.DbDoodadFuncGroups.Add(t.Id, t);
                        }
                    }
                }
            }
        }

        // doodad_phase_func
        if (AllTableNames.GetValueOrDefault("doodad_phase_funcs") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM doodad_phase_funcs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameDoodadPhaseFunc();
                            // Actual DB entries
                            t.Id = GetInt64(reader, "id");
                            t.DoodadFuncGroupId = GetInt64(reader, "doodad_func_group_id");
                            t.ActualFuncId = GetInt64(reader, "actual_func_id");
                            t.ActualFuncType = GetString(reader, "actual_func_type");

                            AaDb.DbDoodadPhaseFuncs.Add(t.Id, t);
                        }
                    }
                }
            }
        }
    }

    private void LoadTransfers()
    {
        if (AllTableNames.GetValueOrDefault("transfers") == SQLite.SQLiteFileName)
        {
            using var connection = SQLite.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM transfers ORDER BY id ASC";
            command.Prepare();
            using var reader = new SQLiteWrapperReader(command.ExecuteReader());
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            while (reader.Read())
            {

                GameTransfers t = new GameTransfers();
                t.Id = GetInt64(reader, "id");
                t.ModelId = GetInt64(reader, "model_id");
                t.PathSmoothing = GetFloat(reader, "path_smoothing");

                AaDb.DbTransfers.Add(t.Id, t);
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }
    }

    private void LoadTransferPaths()
    {
        if (AllTableNames.GetValueOrDefault("transfer_paths") == SQLite.SQLiteFileName)
        {
            using var connection = SQLite.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM transfer_paths ORDER BY owner_id ASC";
            command.Prepare();
            using var reader = new SQLiteWrapperReader(command.ExecuteReader());
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            while (reader.Read())
            {
                GameTransferPaths t = new GameTransferPaths();
                //t.id = GetInt64(reader, "id");
                t.OwnerId = GetInt64(reader, "owner_id");
                t.OwnerType = GetString(reader, "owner_type");
                t.PathName = GetString(reader, "path_name");
                t.WaitTimeStart = GetFloat(reader, "wait_time_start");
                t.WaitTimeEnd = GetFloat(reader, "wait_time_end");

                AaDb.DbTransferPaths.Add(t);
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }
    }

    private void LoadSlaves()
    {
        using (var connection = SQLite.CreateConnection())
        {
            // Slaves
            if (AllTableNames.GetValueOrDefault("slaves") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM slaves ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameSlaves();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.ModelId = GetInt64(reader, "model_id");
                            t.Mountable = GetBool(reader, "mountable");
                            t.OffsetX = GetFloat(reader, "offset_x");
                            t.OffsetY = GetFloat(reader, "offset_y");
                            t.OffsetZ = GetFloat(reader, "offset_z");
                            t.ObbPosX = GetFloat(reader, "obb_pos_x");
                            t.ObbPosY = GetFloat(reader, "obb_pos_y");
                            t.ObbPosZ = GetFloat(reader, "obb_pos_z");
                            t.ObbSizeX = GetFloat(reader, "obb_size_x");
                            t.ObbSizeY = GetFloat(reader, "obb_size_y");
                            t.ObbSizeZ = GetFloat(reader, "obb_size_z");
                            t.PortalSpawnFxId = GetInt64(reader, "portal_spawn_fx_id");
                            t.PortalScale = GetFloat(reader, "portal_scale");
                            t.PortalTime = GetFloat(reader, "portal_time");
                            t.PortalDespawnFxId = GetInt64(reader, "portal_despawn_fx_id");
                            t.Hp25DoodadCount = GetInt64(reader, "hp25_doodad_count");
                            t.Hp50DoodadCount = GetInt64(reader, "hp50_doodad_count");
                            t.Hp75DoodadCount = GetInt64(reader, "hp75_doodad_count");
                            t.SpawnXOffset = GetFloat(reader, "spawn_x_offset");
                            t.SpawnYOffset = GetFloat(reader, "spawn_y_offset");
                            t.FactionId = GetInt64(reader, "faction_id");
                            t.Level = GetInt64(reader, "level");
                            t.Cost = GetInt64(reader, "cost");
                            t.SlaveKindId = GetInt64(reader, "slave_kind_id");
                            t.SpawnValidAreaRange = GetInt64(reader, "spawn_valid_area_range");
                            t.SlaveInitialItemPackId = GetInt64(reader, "slave_initial_item_pack_id");
                            t.SlaveCustomizingId = GetInt64(reader, "slave_customizing_id");
                            t.Customizable = GetBool(reader, "customizable");

                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "slaves", "name", t.Name);
                            t.SearchText = t.Name.ToLower() + " " + t.NameLocalized.ToLower();

                            AaDb.DbSlaves.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // Slave Bindings
            if (AllTableNames.GetValueOrDefault("slave_bindings") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM slave_bindings ORDER BY owner_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        // id field is not present after in 3.0.3.0
                        var columnNames = reader.GetColumnNames();
                        var readId = (columnNames.IndexOf("id") >= 0);
                        var indx = 1L;
                        while (reader.Read())
                        {
                            var t = new GameSlaveBinding();
                            t.Id = readId ? GetInt64(reader, "id") : indx;
                            t.OwnerId = GetInt64(reader, "owner_id");
                            t.OwnerType = GetString(reader, "owner_type");
                            t.SlaveId = GetInt64(reader, "slave_id");
                            t.AttachPointId = GetInt64(reader, "attach_point_id");

                            AaDb.DbSlaveBindings.Add(t.Id, t);
                            indx++;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // Slave Doodad Bindings
            if (AllTableNames.GetValueOrDefault("slave_doodad_bindings") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM slave_doodad_bindings ORDER BY owner_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        // id field is not present after in 3.0.3.0
                        var columnNames = reader.GetColumnNames();
                        var readId = (columnNames.IndexOf("id") >= 0);
                        var indx = 1L;
                        while (reader.Read())
                        {
                            var t = new GameSlaveDoodadBinding();
                            t.Id = readId ? GetInt64(reader, "id") : indx;
                            t.OwnerId = GetInt64(reader, "owner_id");
                            t.OwnerType = GetString(reader, "owner_type");
                            t.AttachPointId = GetInt64(reader, "attach_point_id");
                            t.DoodadId = GetInt64(reader, "doodad_id");
                            t.Persist = GetBool(reader, "persist");
                            t.Scale = GetFloat(reader, "scale");

                            AaDb.DbSlaveDoodadBindings.Add(t.Id, t);
                            indx++;
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

    }

    private void LoadModels()
    {
        if (AllTableNames.GetValueOrDefault("models") != SQLite.SQLiteFileName)
            return;

        using var connection = SQLite.CreateConnection();
        using var command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM models ORDER BY id ASC";
        command.Prepare();
        using var reader = new SQLiteWrapperReader(command.ExecuteReader());
        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        while (reader.Read())
        {
            // comment field is not present after in 3.0.3.0
            var columnNames = reader.GetColumnNames();
            var readComment = (columnNames.IndexOf("comment") >= 0);
            var readCameraAngle = (columnNames.IndexOf("camera_distance_for_wide_angle") >= 0);

            var t = new GameModel();

            t.Id = GetInt64(reader, "id");
            if (readComment)
                t.Comment = GetString(reader, "comment");
            t.SubId = GetInt64(reader, "sub_id");
            t.SubType = GetString(reader, "sub_type");
            t.DyingTime = GetFloat(reader, "dying_time");
            t.SoundMaterialId = GetInt64(reader, "sound_material_id");
            t.Big = GetBool(reader, "big");
            t.TargetDecalSize = GetFloat(reader, "target_decal_size");
            t.UseTargetDecal = GetBool(reader, "use_target_decal");
            t.UseTargetSilhouette = GetBool(reader, "use_target_silhouette");
            t.UseTargetHighlight = GetBool(reader, "use_target_highlight");
            t.Name = GetString(reader, "name");
            t.CameraDistance = GetFloat(reader, "camera_distance");
            t.ShowNameTag = GetBool(reader, "show_name_tag");
            t.NameTagOffset = GetFloat(reader, "name_tag_offset");
            t.SoundPackId = GetInt64(reader, "sound_pack_id");
            t.DespawnDoodadOnCollision = GetBool(reader, "despawn_doodad_on_collision");
            t.PlayMountAnimation = GetBool(reader, "play_mount_animation");
            t.Selectable = GetBool(reader, "selectable");
            t.MountPoseId = GetInt64(reader, "mount_pose_id");
            t.CameraDistanceForWideAngle = readCameraAngle ? GetFloat(reader, "camera_distance_for_wide_angle") : 0f;
            AaDb.DbModels.Add(t.Id, t);
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void DoSearchNpc()
    {
        var searchText = cbSearchNPC.Text.ToLower();
        if (searchText == string.Empty)
            return;
        if (!long.TryParse(searchText, out var searchId))
            searchId = -1;

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;
        dgvNPCs.Rows.Clear();
        var c = 0;
        foreach (var t in AaDb.DbNpCs)
        {
            var z = t.Value;
            if ((z.Id == searchId) || (z.ModelId == searchId) || z.SearchString.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            {
                var line = dgvNPCs.Rows.Add();
                var row = dgvNPCs.Rows[line];

                row.Cells[0].Value = z.Id.ToString();
                row.Cells[1].Value = z.NameLocalized;
                row.Cells[2].Value = z.Level.ToString();
                row.Cells[3].Value = z.NpcKindId.ToString();
                row.Cells[4].Value = z.NpcGradeId.ToString();
                row.Cells[5].Value = AaDb.GetFactionName(z.FactionId, true);
                row.Cells[6].Value = "???";

                if (c == 0)
                {
                    ShowDbNpcInfo(z.Id);
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

    private void ShowDbNpcInfo(long id)
    {
        if (AaDb.DbNpCs.TryGetValue(id, out var npc))
        {
            lNPCTemplate.Text = npc.Id.ToString();
            lNPCTags.Text = TagsAsString(id, AaDb.DbTaggedNpCs);
            tvNPCInfo.Nodes.Clear();

            if (npc.NpcNicknameId > 0)
            {
                var nickNode = tvNPCInfo.Nodes.Add("[" + AaDb.GetTranslationById(npc.NpcNicknameId, "npc_nicknames", "name") + "]");
                nickNode.ForeColor = Color.Yellow;
            }

            if (npc.AiFileId > 0)
            {
                if (AaDb.DbAiFiles.TryGetValue(npc.AiFileId, out var aiFile))
                {
                    var aiNode = tvNPCInfo.Nodes.Add("AI: " + aiFile.Name);
                    aiNode.ForeColor = Color.White;
                    // aiNode.ImageIndex = 3;
                    // aiNode.SelectedImageIndex = aiNode.ImageIndex;
                }
                else
                {
                    tvNPCInfo.Nodes.Add("AI Unknown FileId: " + npc.AiFileId);
                }
            }

            // var aiCommands = AADB.DB_AiCommands.Values.Where(x => x.)


            #region spawners
            // Spawners
            var spawnersNode = tvNPCInfo.Nodes.Add("Spawners");
            spawnersNode.ImageIndex = 1;
            spawnersNode.SelectedImageIndex = 1;
            var spawners = AaDb.DbNpcSpawnerNpcs.Values.Where(x => x.MemberType == "Npc" && x.MemberId == npc.Id).ToList();
            foreach (var npcSpawner in spawners)
            {
                if (AaDb.DbNpcSpawners.TryGetValue(npcSpawner.NpcSpawnerId, out var spawner))
                {
                    var spawnerNode = spawnersNode.Nodes.Add($"ID: {npcSpawner.NpcSpawnerId} - {(spawner.NpcSpawnerCategoryId == 0 ? "Normal" : "AutoCreated")} {(spawner.ActivationState ? " (ActivationState)" : string.Empty)}");
                    
                    var npcsToSpawn = AaDb.DbNpcSpawnerNpcs.Values.Where(x => x.NpcSpawnerId == spawner.Id).ToList();
                    if (npcsToSpawn.Any())
                    {
                        var npcSpawnNode = spawnerNode.Nodes.Add("Spawns");
                        foreach (var npcSpawnerNpc in npcsToSpawn)
                        {
                            AddCustomPropertyNode("npc_id", npcSpawnerNpc.MemberId.ToString(), false, npcSpawnNode);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(spawner.Name))
                        spawnerNode.Nodes.Add($"Name: {spawner.Name}");
                    if (!string.IsNullOrWhiteSpace(spawner.Comment))
                        spawnerNode.Nodes.Add($"Comment: {spawner.Comment}");

                    // spawnerNode.Nodes.Add($"Activation State: {spawner.activation_state}");
                    spawnerNode.Nodes.Add($"Save Indun: {spawner.SaveIndun}");

                    if (Math.Abs(spawner.SpawnDelayMin - spawner.SpawnDelayMax) < float.Epsilon)
                    {
                        spawnerNode.Nodes.Add($"Spawn Delay: {MSToString((long)spawner.SpawnDelayMin * 1000, true)}");
                    }
                    else
                    {
                        spawnerNode.Nodes.Add($"Spawn Delay Min: {MSToString((long)spawner.SpawnDelayMin * 1000, true)}");
                        spawnerNode.Nodes.Add($"Spawn Delay Max: {MSToString((long)spawner.SpawnDelayMax * 1000, true)}");
                    }

                    if (spawner.MinPopulation != 0)
                        spawnerNode.Nodes.Add($"Min Population: {spawner.MinPopulation}");
                    if (spawner.MaxPopulation != 1)
                        spawnerNode.Nodes.Add($"Max Population: {spawner.MaxPopulation}");
                    if (spawner.StartTime > 0)
                        spawnerNode.Nodes.Add($"Start Time: {spawner.StartTime}");
                    if (spawner.EndTime > 0)
                        spawnerNode.Nodes.Add($"End Time: {spawner.EndTime}");
                    if (spawner.TestRadiusNpc > 0)
                        spawnerNode.Nodes.Add($"Test Radius NPC: {spawner.TestRadiusNpc}");
                    if (spawner.TestRadiusPc > 0)
                        spawnerNode.Nodes.Add($"Test Radius PC: {spawner.TestRadiusPc}");
                    if (spawner.SuspendSpawnCount > 0)
                        spawnerNode.Nodes.Add($"Suspend Spawn Count: {spawner.SuspendSpawnCount}");
                }
                else
                {
                    spawnersNode.Nodes.Add("NOT found!:" + npcSpawner.NpcSpawnerId);
                }
                spawnersNode.Expand();
            }
            #endregion

            #region interactions
            var interactionNode = tvNPCInfo.Nodes.Add("Interaction");
            interactionNode.ImageIndex = 2;
            interactionNode.SelectedImageIndex = 2;
            if (npc.NpcInteractionSetId > 0)
            {
                var interactions = AaDb.DbNpcInteractions.Values.Where(x => x.NpcInteractionSetId == npc.NpcInteractionSetId).ToList();
                foreach (var interaction in interactions)
                {
                    AddCustomPropertyNode("skill_id", interaction.SkillId.ToString(), false, interactionNode);
                }
            }
            if (npc.Auctioneer)
                interactionNode.Nodes.Add("Auction");
            if (npc.Banker)
                interactionNode.Nodes.Add("Warehouse");
            if (npc.Blacksmith)
                interactionNode.Nodes.Add("Blacksmith");
            if (npc.Expedition)
                interactionNode.Nodes.Add("Guild Manager");
            if (npc.Merchant)
                interactionNode.Nodes.Add("Merchant");
            if (npc.Priest)
                interactionNode.Nodes.Add("Priest");
            if (npc.Repairman)
                interactionNode.Nodes.Add("Repairs");
            if (npc.SkillTrainer)
                interactionNode.Nodes.Add("Skillmanager");
            if (npc.Specialty)
                interactionNode.Nodes.Add("Speciality");
            if (npc.Stabler)
                interactionNode.Nodes.Add("Stablemaster");
            if (npc.Teleporter)
                interactionNode.Nodes.Add("Teleporter");
            if (npc.Trader)
                interactionNode.Nodes.Add("Trader");

            if (interactionNode.Nodes.Count > 0)
                interactionNode.Expand();
            else
                tvNPCInfo.Nodes.Remove(interactionNode);

            #endregion

            #region skills
            // Base Skill
            if (npc.BaseSkillId > 0)
            {
                var baseSkillNode = tvNPCInfo.Nodes.Add("Base Skill");
                baseSkillNode.ImageIndex = 2;
                baseSkillNode.SelectedImageIndex = 2;
                AddCustomPropertyNode("skill_id", npc.BaseSkillId.ToString(), false, baseSkillNode);
                AddCustomPropertyNode("base_skill_delay", npc.BaseSkillDelay.ToString(CultureInfo.InvariantCulture), true, baseSkillNode);
                AddCustomPropertyNode("base_skill_strafe", npc.BaseSkillStrafe.ToString(), false, baseSkillNode);
                baseSkillNode.Expand();
            }

            // NP Skills
            var npSkillsNode = tvNPCInfo.Nodes.Add("NP Skills");
            npSkillsNode.ImageIndex = 2;
            npSkillsNode.SelectedImageIndex = 2;
            var npSkills = AaDb.DbNpSkills.Values.Where(x => x.OwnerId == npc.Id && x.OwnerType == "Npc").ToList();
            foreach (var npSkill in npSkills)
            {
                var npSkillNode = AddCustomPropertyNode("skill_id", npSkill.SkillId.ToString(), false, npSkillsNode);
                npSkillNode.Text += $@", {npSkill.SkillUseConditionId}( {npSkill.SkillUseParam1:F1} | {npSkill.SkillUseParam2:F1} )";
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
            var initialBuffs = AaDb.DbNpcInitialBuffs.Values.Where(x => x.NpcId == npc.Id).ToList();
            foreach (var initialBuff in initialBuffs)
            {
                AddCustomPropertyNode("buff_id", initialBuff.BuffId.ToString(), false, initialBuffsNode);
            }
            if (initialBuffsNode.Nodes.Count > 0)
                initialBuffsNode.Expand();
            else
                tvNPCInfo.Nodes.Remove(initialBuffsNode);
            #endregion

            #region passive_buffs
            var passiveBuffsNode = tvNPCInfo.Nodes.Add("Passive Buffs");
            passiveBuffsNode.ImageIndex = 2;
            passiveBuffsNode.SelectedImageIndex = 2;

            var npPassiveBuffs = AaDb.DbNpPassiveBuffs.Values.Where(x => x.OwnerId == npc.Id && x.OwnerType == "Npc").ToList();
            
            foreach (var npPassiveBuff in npPassiveBuffs)
            {
                var passiveBuffs = AaDb.DbPassiveBuffs.Values.Where(x => x.Id == npPassiveBuff.PassiveBuffId).ToList();
                foreach (var passiveBuff in passiveBuffs)
                {
                    AddCustomPropertyNode("buff_id", passiveBuff.BuffId.ToString(), false, passiveBuffsNode);
                }
            }

            if (passiveBuffsNode.Nodes.Count > 0)
                passiveBuffsNode.Expand();
            else
                tvNPCInfo.Nodes.Remove(passiveBuffsNode);
            #endregion

            #region npc_quests
            var allQuestsStartersActsForNpcs = AaDb.DbQuestActConAcceptNpc.Values.Where(x => x.NpcId == npc.Id).ToList();
            if (allQuestsStartersActsForNpcs.Any())
            {
                var questNode = tvNPCInfo.Nodes.Add("Quests");
                questNode.ImageIndex = 2;
                questNode.SelectedImageIndex = 2;
                foreach (var actConAcceptNpc in allQuestsStartersActsForNpcs)
                {
                    var allQuestsStartersForNpcs = AaDb.DbQuestActs.Values.Where(x =>
                        x.ActDetailType == "QuestActConAcceptNpc" && x.ActDetailId == actConAcceptNpc.Id).ToList();
                    if (allQuestsStartersForNpcs != null && allQuestsStartersForNpcs.Any())
                    {
                        foreach (var gameQuestAct in allQuestsStartersForNpcs)
                        {
                            var allQuestsForNpcs = AaDb.DbQuestComponents.Values
                                .Where(x => x.ComponentKindId == 2 && x.Id == gameQuestAct.QuestComponentId).ToList();
                            if (allQuestsForNpcs != null && allQuestsForNpcs.Any())
                            {
                                foreach (var gameQuestComponent in allQuestsForNpcs)
                                {
                                    AddCustomPropertyNode("quest_id", gameQuestComponent.QuestContextId.ToString(),
                                        true,
                                        questNode);
                                }
                            }
                        }
                    }
                }
                if (questNode.Nodes.Count <= 0)
                    tvNPCInfo.Nodes.Remove(questNode);
            }

            #endregion

            #region loot_drops
            btnShowNpcLoot.Tag = npc.Id;
            btnShowNpcLoot.Enabled = AaDb.DbLootPackDroppingNpc.Any(pl => pl.Value.NpcId == npc.Id);
            if (btnShowNpcLoot.Enabled)
            {
                var lootNode = tvNPCInfo.Nodes.Add("Loot");
                lootNode.ImageIndex = 2;
                lootNode.SelectedImageIndex = 2;

                // Get all packs for this NPC
                var allPacksForNpc = AaDb.DbLootPackDroppingNpc.Values.Where(lp => lp.NpcId == npc.Id).ToList();
                //var nonDefaultPackCount = allPacksForNpc.Count(p => p.Value.default_pack == false);

                foreach (var gameLootPackDroppingNpc in allPacksForNpc)
                {
                    AddCustomPropertyNodeForLootPack(gameLootPackDroppingNpc.LootPackId, lootNode);
                }
                /*
                // Extract just the IDs
                var usedLootPacks = allPacksForNpc.Select(x => x.LootPackId).ToList();

                //
                var allLootPackGroups = AaDb.DbLootGroups.Values.Where(x => usedLootPacks.Contains(x.PackId)).ToList();

                // GroupId, (List<GameLoot>, TotalValue)
                var resultLootGroups = new Dictionary<long, List<GameLoot>>();

                // Check all loot connected to this NPC and place them according to their group
                foreach (var pack in allPacksForNpc)
                {
                    var lootPacks = AaDb.DbLoots.Values.Where(x => x.LootPackId == pack.LootPackId).ToList();
                    if (!lootPacks.Any())
                    {
                        // Pack does not exist
                        lootNode.Nodes.Add($"Missing loot_pack_id {pack.LootPackId}");
                        continue;
                    }

                    foreach (var lootPack in lootPacks)
                    {
                        if (!resultLootGroups.TryGetValue(lootPack.Group, out var lootGroup))
                        {
                            lootGroup = new List<GameLoot>();
                            resultLootGroups.Add(lootPack.Group, lootGroup);
                        }
                        lootGroup.Add(lootPack);
                    }
                }

                // Get a sorted list of Group IDs
                var groupKeys = resultLootGroups.Keys.ToList();
                groupKeys.Sort();

                // Create group nodes
                var groupNodes = new Dictionary<long, TreeNode>();
                foreach (var groupId in groupKeys)
                {
                    var groupName = $"Group {groupId}";
                    //if (groupId == 0)
                    //    groupName = "Quest Items";
                    //if (groupId == 1)
                    //    groupName = "Always Drop";
                    groupNodes.Add(groupId, lootNode.Nodes.Add(groupName));
                }

                // List each item and sort by group
                foreach (var lootGroup in resultLootGroups)
                {
                    if (!groupNodes.TryGetValue(lootGroup.Key, out var groupNode))
                        continue;

                    var totalWeight = lootGroup.Value.Sum(x => x.DropRate);
                    if (totalWeight <= 0)
                        totalWeight = 1;

                    groupNode.Text += $@" (Weight {totalWeight})";

                    foreach (var loot in lootGroup.Value)
                    {
                        var lootGroupData = allLootPackGroups.FirstOrDefault(x => x.PackId == loot.LootPackId && x.GroupNo == loot.Group);

                        var baseDropRate = loot is { DropRate: > 1 }
                            ? loot.DropRate / 10_000_000f
                            : 1f;

                        var groupDropRate = lootGroupData is { DropRate: > 1 }
                            ? lootGroupData.DropRate / 100_000f
                            : 1f;

                        if (groupDropRate > 1f)
                            groupDropRate = 1f;

                        var dropRate = baseDropRate * groupDropRate;

                        var visibleRate = dropRate * 100f;

                        var itemNode = AddCustomPropertyNode("item_id", loot.ItemId.ToString(), false, groupNode);

                        if (visibleRate > 50f)
                            itemNode.Text = $@"{visibleRate:F0}% {itemNode.Text}";
                        else
                        if (visibleRate > 5f)
                            itemNode.Text = $@"{visibleRate:F1}% {itemNode.Text}";
                        else
                            itemNode.Text = $@"{visibleRate:F2}% {itemNode.Text}";
                    }

                }
                */

                lootNode.ExpandAll();
                if (cbNpcCollapseLoot.Checked)
                    lootNode.Collapse();
            }
            #endregion

            #region events
            var eventSpawns = AaDb.DbTowerDefProgSpawnTargets.Values.Where(x => x.SpawnTargetType == "NpcSpawner" && spawners.Select(s => s.NpcSpawnerId).Contains(x.SpawnTargetId)).ToList();
            var eventMainSpawns = AaDb.DbTowerDefs.Values.Where(x => spawners.Select(s => s.NpcSpawnerId).Contains(x.TargetNpcSpawnerId)).ToList();
            if (eventSpawns.Any() || eventMainSpawns.Any())
            {
                var eventNode = tvNPCInfo.Nodes.Add("Event Spawns");
                foreach (var eventMainSpawn in eventMainSpawns)
                {
                    eventNode.Nodes.Add($"Spawner: {eventMainSpawn.TargetNpcSpawnerId}, Event: {eventMainSpawn.TitleMsgLocalized}");
                }

                foreach (var eventSpawn in eventSpawns)
                {
                    if (AaDb.DbTowerDefProgs.TryGetValue(eventSpawn.TowerDefProgId, out var eventProg))
                    {
                        if (AaDb.DbTowerDefs.TryGetValue(eventProg.TowerDefId, out var eventTowerDef))
                        {
                            eventNode.Nodes.Add($"{eventTowerDef.TitleMsgLocalized} ({eventTowerDef.Id}) - {eventProg.MsgLocalized} ({eventProg.Id})");
                        }
                        else
                        {
                            eventNode.Nodes.Add($"Unknown TowerDef: {eventProg.TowerDefId}, Prog: {eventSpawn.TowerDefProgId}");
                        }
                    }
                    else
                    {
                        eventNode.Nodes.Add($"Unknown TowerDefProg: {eventSpawn.TowerDefProgId}");
                    }

                }
            }

            var eventKills = AaDb.DbTowerDefProgKillTargets.Values.Where(x => x.KillTargetType == "Npc" && x.KillTargetId == npc.Id).ToList();
            var eventMainKills = AaDb.DbTowerDefs.Values.Where(x => x.KillNpcId == npc.Id).ToList();
            if (eventKills.Any() || eventMainKills.Any())
            {
                var killNode = tvNPCInfo.Nodes.Add("Event Kills");
                foreach (var eventMainKill in eventMainKills)
                {
                    killNode.Nodes.Add($"{eventMainKill.KillNpcCount} x {eventMainKill.TitleMsgLocalized}");
                }

                foreach (var eventKill in eventKills)
                {
                    if (AaDb.DbTowerDefProgs.TryGetValue(eventKill.TowerDefProgId, out var eventProg))
                    {
                        if (AaDb.DbTowerDefs.TryGetValue(eventProg.TowerDefId, out var eventTowerDef))
                        {
                            killNode.Nodes.Add($"{eventKill.KillCount} x {eventTowerDef.TitleMsgLocalized} ({eventTowerDef.Id}) - {eventProg.MsgLocalized} ({eventProg.Id})");
                        }
                        else
                        {
                            killNode.Nodes.Add($"Unknown TowerDef: {eventProg.TowerDefId}, Prog: {eventKill.TowerDefProgId}");
                        }
                    }
                    else
                    {
                        killNode.Nodes.Add($"Unknown TowerDefProg: {eventKill.TowerDefProgId}");
                    }

                }
            }
            #endregion

            ShowSelectedData("npcs", "(id = " + id.ToString() + ")", "id ASC");
            btnShowNPCsOnMap.Tag = npc.Id;
        }
        else
        {
            lNPCTemplate.Text = @"???";
            lNPCTags.Text = @"???";
            tvNPCInfo.Nodes.Clear();
            btnShowNPCsOnMap.Tag = 0;
            btnShowNpcLoot.Tag = 0;
            btnShowNpcLoot.Enabled = false;
        }

        lGMNPCSpawn.Text = $@"/spawn npc {lNPCTemplate.Text}";
    }

    private void DoNpcsSelectionChanged()
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

    private void DoSearchDoodads()
    {
        var searchText = cbSearchDoodads.Text.ToLower();
        if (searchText == string.Empty)
            return;
        long searchId;
        if (!long.TryParse(searchText, out searchId))
            searchId = -1;

        var first = true;
        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;
        dgvDoodads.Rows.Clear();
        var c = 0;
        foreach (var t in AaDb.DbDoodadAlmighties)
        {
            var z = t.Value;
            if ((z.Id == searchId) || (z.GroupId == searchId) || z.SearchString.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            {
                var line = dgvDoodads.Rows.Add();
                var row = dgvDoodads.Rows[line];

                row.Cells[0].Value = z.Id.ToString();
                row.Cells[1].Value = z.NameLocalized;
                row.Cells[2].Value = z.MgmtSpawn.ToString();
                if (AaDb.DbDoodadGroups.TryGetValue(z.GroupId, out var dGroup))
                {
                    row.Cells[3].Value = dGroup.NameLocalized + " (" + z.GroupId.ToString() + ")";
                }
                else
                {
                    row.Cells[3].Value = z.GroupId.ToString();
                }

                row.Cells[4].Value = z.Percent.ToString();
                if (z.FactionId != 0)
                    row.Cells[5].Value = AaDb.GetFactionName(z.FactionId, true);
                else
                    row.Cells[5].Value = string.Empty;
                row.Cells[6].Value = z.ModelKindId.ToString();

                if (first)
                {
                    first = false;
                    ShowDbDoodad(z.Id);
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

    private void DoDoodadsSelectionChanged()
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
            ShowSelectedData(@"doodad_almighties", "(id = " + id + ")", "id ASC");
        }

    }

    private void ShowDbDoodad(long id)
    {
        if (AaDb.DbDoodadAlmighties.TryGetValue(id, out var doodad))
        {
            lDoodadID.Text = doodad.Id.ToString();
            lDoodadName.Text = doodad.NameLocalized;
            lDoodadModel.Text = doodad.Model;
            lDoodadOnceOneMan.Text = doodad.OnceOneMan.ToString();
            lDoodadOnceOneInteraction.Text = doodad.OnceOneInteraction.ToString();
            lDoodadShowName.Text = doodad.ShowName.ToString();
            lDoodadMgmtSpawn.Text = doodad.MgmtSpawn.ToString();
            lDoodadPercent.Text = doodad.Percent.ToString();
            lDoodadMinTime.Text = MSToString(doodad.MinTime);
            lDoodadMaxTime.Text = MSToString(doodad.MaxTime);
            lDoodadModelKindID.Text = doodad.ModelKindId.ToString();
            lDoodadUseCreatorFaction.Text = doodad.UseCreatorFaction.ToString();
            lDoodadForceToDTopPriority.Text = doodad.ForceTodTopPriority.ToString();
            lDoodadMilestoneID.Text = doodad.MilestoneId.ToString();
            lDoodadGroupID.Text = doodad.GroupId.ToString();
            lDoodadShowName.Text = doodad.ShowMinimap.ToString();
            lDoodadUseTargetDecal.Text = doodad.UseTargetDecal.ToString();
            lDoodadUseTargetSilhouette.Text = doodad.UseTargetSilhouette.ToString();
            lDoodadUseTargetHighlight.Text = doodad.UseTargetHighlight.ToString();
            lDoodadTargetDecalSize.Text = doodad.TargetDecalSize.ToString(CultureInfo.InvariantCulture);
            lDoodadSimRadius.Text = RangeToString(doodad.SimRadius);
            lDoodadCollideShip.Text = doodad.CollideShip.ToString();
            lDoodadCollideVehicle.Text = doodad.CollideVehicle.ToString();
            lDoodadClimateID.Text = doodad.ClimateId.ToString();
            lDoodadSaveIndun.Text = doodad.SaveIndun.ToString();
            lDoodadMarkModel.Text = doodad.MarkModel;
            lDoodadForceUpAction.Text = doodad.ForceUpAction.ToString();
            lDoodadLoadModelFromWorld.Text = doodad.LoadModelFromWorld.ToString();
            lDoodadParentable.Text = doodad.Parentable.ToString();
            lDoodadChildable.Text = doodad.Childable.ToString();
            lDoodadFactionID.Text = AaDb.GetFactionName(doodad.FactionId, true);
            lDoodadGrowthTime.Text = MSToString(doodad.GrowthTime);
            lDoodadDespawnOnCollision.Text = doodad.DespawnOnCollision.ToString();
            lDoodadNoCollision.Text = doodad.NoCollision.ToString();
            lDoodadRestrictZoneID.Text = doodad.RestrictZoneId.ToString();
            btnShowDoodadOnMap.Tag = doodad.Id;

            lDoodadAddGMCommand.Text = $@"/doodad spawn {doodad.Id}";
            lDoodadRemoveGMCommand.Text = $@"/despawn doodad {doodad.Id} 5";

            if (AaDb.DbDoodadGroups.TryGetValue(doodad.GroupId, out var dGroup))
            {
                lDoodadGroupName.Text = $@"{dGroup.NameLocalized} ({doodad.GroupId})";
                lDoodadGroupIsExport.Text = dGroup.IsExport.ToString();
                lDoodadGroupGuardOnFieldTime.Text = SecondsToString(dGroup.GuardOnFieldTime);
                lDoodadGroupRemovedByHouse.Text = dGroup.RemovedByHouse.ToString();
            }
            else
            {
                lDoodadGroupName.Text = @"<none>";
                lDoodadGroupIsExport.Text = string.Empty;
                lDoodadGroupGuardOnFieldTime.Text = string.Empty;
                lDoodadGroupRemovedByHouse.Text = string.Empty;
            }

            bool firstFuncGroup = true;
            dgvDoodadFuncGroups.Rows.Clear();
            foreach (var f in AaDb.DbDoodadFuncGroups)
            {
                var dFuncGroup = f.Value;
                if (dFuncGroup.DoodadAlmightyId == doodad.Id)
                {
                    GameDoodadPhaseFunc dPhaseFunc = null;
                    foreach (var dpf in AaDb.DbDoodadPhaseFuncs)
                        if (dpf.Value.DoodadFuncGroupId == dFuncGroup.Id)
                        {
                            dPhaseFunc = dpf.Value;
                            break;
                        }


                    var line = dgvDoodadFuncGroups.Rows.Add();
                    var row = dgvDoodadFuncGroups.Rows[line];

                    row.Cells[0].Value = dFuncGroup.Id.ToString();
                    row.Cells[1].Value = dFuncGroup.DoodadFuncGroupKindId.ToString();
                    row.Cells[2].Value = dPhaseFunc?.ActualFuncId.ToString() ?? "-";
                    row.Cells[3].Value = dPhaseFunc?.ActualFuncType ?? "none";

                    if (firstFuncGroup)
                    {
                        firstFuncGroup = false;
                        ShowDbDoodadFuncGroup(dFuncGroup.Id);
                    }
                }
            }

            // Details Tab
            tvDoodadDetails.Nodes.Clear();
            var rootNode = tvDoodadDetails.Nodes.Add(doodad.NameLocalized + " ( " + doodad.Id + " )");
            rootNode.ForeColor = Color.White;
            foreach (var f in AaDb.DbDoodadFuncGroups)
            {
                var dFuncGroup = f.Value;
                if (dFuncGroup.DoodadAlmightyId == doodad.Id)
                {
                    var doodadGroupName = "Group: " + dFuncGroup.Id.ToString() + " - Kind: " + dFuncGroup.DoodadFuncGroupKindId.ToString() + " - " + dFuncGroup.NameLocalized;
                    var groupNode = rootNode.Nodes.Add(doodadGroupName);
                    groupNode.ForeColor = Color.LightCyan;

                    // Phase Funcs
                    foreach (var dpf in AaDb.DbDoodadPhaseFuncs)
                        if (dpf.Value.DoodadFuncGroupId == dFuncGroup.Id)
                        {
                            var phaseNode = groupNode.Nodes.Add("PhaseFuncs: " + dpf.Value.ActualFuncType +
                                                                " ( " +
                                                                dpf.Value.ActualFuncId + " )");
                            phaseNode.ForeColor = Color.Yellow;
                            var tableName = FunctionTypeToTableName(dpf.Value.ActualFuncType);
                            var fieldsList = GetCustomTableValues(tableName, "id",
                                dpf.Value.ActualFuncId.ToString());
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
                        GetCustomTableValues("doodad_funcs", "doodad_func_group_id", dFuncGroup.Id.ToString());
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
            lDoodadID.Text = @"0";
            lDoodadName.Text = @"<none>";
            lDoodadModel.Text = @"<none>";
            lDoodadOnceOneMan.Text = string.Empty;
            lDoodadOnceOneInteraction.Text = string.Empty;
            lDoodadShowName.Text = string.Empty;
            lDoodadMgmtSpawn.Text = string.Empty;
            lDoodadPercent.Text = string.Empty;
            lDoodadMinTime.Text = string.Empty;
            lDoodadMaxTime.Text = string.Empty;
            lDoodadModelKindID.Text = string.Empty;
            lDoodadUseCreatorFaction.Text = string.Empty;
            lDoodadForceToDTopPriority.Text = string.Empty;
            lDoodadMilestoneID.Text = string.Empty;
            lDoodadGroupID.Text = string.Empty;
            lDoodadShowName.Text = string.Empty;
            lDoodadUseTargetDecal.Text = string.Empty;
            lDoodadUseTargetSilhouette.Text = string.Empty;
            lDoodadUseTargetHighlight.Text = string.Empty;
            lDoodadTargetDecalSize.Text = string.Empty;
            lDoodadSimRadius.Text = string.Empty;
            lDoodadCollideShip.Text = string.Empty;
            lDoodadCollideVehicle.Text = string.Empty;
            lDoodadClimateID.Text = string.Empty;
            lDoodadSaveIndun.Text = string.Empty;
            lDoodadMarkModel.Text = string.Empty;
            lDoodadForceUpAction.Text = string.Empty;
            lDoodadLoadModelFromWorld.Text = string.Empty;
            lDoodadParentable.Text = string.Empty;
            lDoodadChildable.Text = string.Empty;
            lDoodadFactionID.Text = string.Empty;
            lDoodadGrowthTime.Text = string.Empty;
            lDoodadDespawnOnCollision.Text = string.Empty;
            lDoodadNoCollision.Text = string.Empty;
            lDoodadRestrictZoneID.Text = string.Empty;

            lDoodadGroupName.Text = @"<none>";
            lDoodadGroupIsExport.Text = string.Empty;
            lDoodadGroupGuardOnFieldTime.Text = string.Empty;
            lDoodadGroupRemovedByHouse.Text = string.Empty;

            btnShowDoodadOnMap.Tag = 0;

            dgvDoodadFuncGroups.Rows.Clear();

            tvDoodadDetails.Nodes.Clear();

            lDoodadAddGMCommand.Text = string.Empty;
            lDoodadRemoveGMCommand.Text = string.Empty;
        }
    }

    private void ShowDbDoodadFuncGroup(long id)
    {
        if (AaDb.DbDoodadFuncGroups.TryGetValue(id, out var dfg))
        {
            // DoodadFuncGroup
            lDoodadFuncGroupID.Text = dfg.Id.ToString();
            lDoodadFuncGroupModel.Text = dfg.Model;
            lDoodadFuncGroupKindID.Text = dfg.DoodadFuncGroupKindId.ToString();
            lDoodadFuncGroupPhaseMsg.Text = dfg.PhaseMsgLocalized;
            lDoodadFuncGroupSoundID.Text = dfg.SoundId.ToString();
            lDoodadFuncGroupName.Text = dfg.NameLocalized;
            lDoodadFuncGroupSoundTime.Text = MSToString(dfg.SoundTime);
            lDoodadFuncGroupComment.Text = dfg.Comment;
            lDoodadFuncGroupIsMsgToZone.Text = dfg.IsMsgToZone.ToString();

            //lDoodadPhaseFuncsId.Text = string.Empty;
            lDoodadPhaseFuncsActualId.Text = string.Empty;
            lDoodadPhaseFuncsActualType.Text = string.Empty;
            foreach (var dpf in AaDb.DbDoodadPhaseFuncs)
                if (dpf.Value.DoodadFuncGroupId == dfg.Id)
                {
                    //lDoodadPhaseFuncsId.Text = dpf.Value.id.ToString();
                    lDoodadPhaseFuncsActualId.Text = dpf.Value.ActualFuncId.ToString();
                    lDoodadPhaseFuncsActualType.Text = dpf.Value.ActualFuncType;
                    break;
                }
        }
        else
        {
            // blank
            lDoodadFuncGroupID.Text = @"0";
            lDoodadFuncGroupModel.Text = @"<none>";
            lDoodadFuncGroupKindID.Text = string.Empty;
            lDoodadFuncGroupPhaseMsg.Text = string.Empty;
            lDoodadFuncGroupSoundID.Text = string.Empty;
            lDoodadFuncGroupName.Text = string.Empty;
            lDoodadFuncGroupSoundTime.Text = string.Empty;
            lDoodadFuncGroupComment.Text = string.Empty;
            lDoodadFuncGroupIsMsgToZone.Text = string.Empty;

            //lDoodadPhaseFuncsId.Text = string.Empty;
            lDoodadPhaseFuncsActualId.Text = string.Empty;
            lDoodadPhaseFuncsActualType.Text = string.Empty;
        }
    }

    private void DoDoodadFuncGroupsSelectionChanged()
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

    private void DoSearchSlave()
    {
        string searchText = tSearchSlave.Text.ToLower();
        if (searchText == string.Empty)
            return;
        long searchId;
        if (!long.TryParse(searchText, out searchId))
            searchId = -1;

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;
        dgvSlaves.Rows.Clear();
        int c = 0;
        foreach (var t in AaDb.DbSlaves)
        {
            var z = t.Value;
            var modelDetails = string.Empty;
            if (AaDb.DbModels.TryGetValue(z.ModelId, out var model))
                modelDetails = model.SubType + " " + model.SubId.ToString() + " - " + model.Name;

            if (
                (z.Id == searchId) ||
                (z.ModelId == searchId) ||
                (z.SearchText.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)) ||
                (modelDetails.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
            )
            {
                var line = dgvSlaves.Rows.Add();
                var row = dgvSlaves.Rows[line];

                row.Cells[0].Value = z.Id.ToString();
                row.Cells[1].Value = z.NameLocalized;
                row.Cells[2].Value = z.ModelId.ToString();
                row.Cells[3].Value = z.Level.ToString();
                row.Cells[4].Value = AaDb.GetFactionName(z.FactionId, true);
                row.Cells[5].Value = modelDetails;

                if (c == 0)
                {
                    ShowDbSlaveInfo(z.Id);
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

    private void ShowDbSlaveInfo(long id)
    {
        if (AaDb.DbSlaves.TryGetValue(id, out var slave))
        {
            lSlaveTemplate.Text = slave.Id.ToString();
            lSlaveName.Text = slave.NameLocalized;
            ShowSelectedData("slaves", "(id = " + id.ToString() + ")", "id ASC");

            tvSlaveInfo.Nodes.Clear();

            // Passive Buffs
            var slavePassiveBuff = AaDb.DbSlavePassiveBuffs.Values.FirstOrDefault(x => x.OwnerType == "Slave" && x.OwnerId == slave.Id);
            if ((slavePassiveBuff != null) && AaDb.DbPassiveBuffs.TryGetValue(slavePassiveBuff.PassiveBuffId, out var passiveBuff))
            {
                var passiveNode = tvSlaveInfo.Nodes.Add("Passive Buffs");
                AddCustomPropertyNode("buff_id", passiveBuff.BuffId.ToString(), false, passiveNode);
            }

            // Initial Buffs
            var slaveInitialBuffs = AaDb.DbSlaveInitialBuffs.Values.Where(x => x.SlaveId == slave.Id).ToList();
            if (slaveInitialBuffs.Count > 0)
            {
                var initialNode = tvSlaveInfo.Nodes.Add("Initial Buffs");
                foreach (var initialBuff in slaveInitialBuffs)
                {
                    AddCustomPropertyNode("buff_id", initialBuff.BuffId.ToString(), false, initialNode);
                }
            }

            // Skills
            var slaveMountSkills = AaDb.DbSlaveMountSkills.Values.Where(x => x.SlaveId == slave.Id).ToList();
            if (slaveMountSkills.Count > 0)
            {
                var skillNode = tvSlaveInfo.Nodes.Add("Skills");
                foreach (var slaveMountSkill in slaveMountSkills)
                {
                    if (!AaDb.DbMountSkills.TryGetValue(slaveMountSkill.MountSkillId, out var mountSkill))
                        continue;
                    AddCustomPropertyNode("skill_id", mountSkill.SkillId.ToString(), false, skillNode);
                }
            }

            // Bindings
            var slaveBindings = AaDb.DbSlaveBindings.Values.Where(x => x.OwnerType == "Slave" && x.OwnerId == slave.Id).ToList();
            var slaveDoodadBindings = AaDb.DbSlaveDoodadBindings.Values.Where(x => x.OwnerType == "Slave" && x.OwnerId == slave.Id).ToList();
            if ((slaveBindings.Count > 0) || (slaveDoodadBindings.Count > 0))
            {
                var bindingNode = tvSlaveInfo.Nodes.Add("Bindings");
                foreach (var slaveBinding in slaveBindings)
                {
                    var n = AddCustomPropertyNode("slave_id", slaveBinding.SlaveId.ToString(), false, bindingNode);
                    n.Text = $@" @{slaveBinding.AttachPointId}: {n.Text}";
                }
                foreach (var slaveDoodadBinding in slaveDoodadBindings)
                {
                    var n = AddCustomPropertyNode("doodad_id", slaveDoodadBinding.DoodadId.ToString(), false, bindingNode);
                    n.Text = $@" @{slaveDoodadBinding.AttachPointId}: {n.Text}";
                }
            }

            tvSlaveInfo.ExpandAll();

        }
        else
        {
            lSlaveTemplate.Text = @"???";
            lSlaveName.Text = @"???";
            tvSlaveInfo.Nodes.Clear();
        }
    }

    private void DoSlavesSelectionChanged()
    {
        if (dgvSlaves.SelectedRows.Count <= 0)
            return;
        var row = dgvSlaves.SelectedRows[0];
        if (row.Cells.Count <= 0)
            return;

        var val = row.Cells[0].Value;
        if (val == null)
            return;

        var qid = long.Parse(val.ToString() ?? "0");
        ShowDbSlaveInfo(qid);
    }
}