using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBViewer.DbDefs;
using AAEmu.DBViewer.enums;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer;

public partial class MainForm
{
    private void LoadQuests()
    {
        using (var connection = SQLite.CreateConnection())
        {
            if (AllTableNames.GetValueOrDefault("quest_categories") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_categories ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestCategory t = new GameQuestCategory();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");

                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "quest_categories", "name", t.Name);

                            t.SearchString = t.Name + " " + t.NameLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AaDb.DbQuestCategories.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("quest_contexts") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_contexts ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestContexts t = new GameQuestContexts();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.CategoryId = GetInt64(reader, "category_id");
                            t.Repeatable = GetBool(reader, "repeatable");
                            t.Level = GetInt64(reader, "level");
                            t.Selective = GetBool(reader, "selective");
                            t.Successive = GetBool(reader, "successive");
                            t.RestartOnFail = GetBool(reader, "restart_on_fail");
                            t.ChapterIdx = GetInt64(reader, "chapter_idx");
                            t.QuestIdx = GetInt64(reader, "quest_idx");
                            // t.milestone_id = GetInt64(reader, "milestone_id");
                            t.LetItDone = GetBool(reader, "let_it_done");
                            t.DetailId = GetInt64(reader, "detail_id");
                            t.ZoneId = GetInt64(reader, "zone_id");
                            t.Degree = GetInt64(reader, "degree");
                            t.UseQuestCamera = GetBool(reader, "use_quest_camera");
                            t.Score = GetInt64(reader, "score");
                            t.UseAcceptMessage = GetBool(reader, "use_accept_message");
                            t.UseCompleteMessage = GetBool(reader, "use_complete_message");
                            t.GradeId = GetInt64(reader, "grade_id");


                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "quest_contexts", "name", t.Name);

                            t.SearchString = t.Name + " " + t.NameLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AaDb.DbQuestContexts.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("quest_acts") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_acts ORDER BY quest_component_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestAct t = new GameQuestAct();
                            t.Id = GetInt64(reader, "id");
                            t.QuestComponentId = GetInt64(reader, "quest_component_id");
                            t.ActDetailId = GetInt64(reader, "act_detail_id");
                            t.ActDetailType = GetString(reader, "act_detail_type");

                            AaDb.DbQuestActs.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("quest_act_con_accept_npcs") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_act_con_accept_npcs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameQuestActConAcceptNpc();
                            t.Id = GetInt64(reader, "id");
                            t.NpcId = GetInt64(reader, "npc_id");

                            AaDb.DbQuestActConAcceptNpc.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("quest_components") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_components ORDER BY quest_context_id ASC, component_kind_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestComponent t = new GameQuestComponent();
                            t.Id = GetInt64(reader, "id");
                            t.QuestContextId = GetInt64(reader, "quest_context_id");
                            t.ComponentKindId = GetInt64(reader, "component_kind_id");
                            t.NextComponent = GetInt64(reader, "next_component");
                            t.NpcAiId = GetInt64(reader, "npc_ai_id");
                            t.NpcId = GetInt64(reader, "npc_id");
                            t.SkillId = GetInt64(reader, "skill_id");
                            t.SkillSelf = GetBool(reader, "skill_self");
                            t.AiPathName = GetString(reader, "ai_path_name");
                            t.AiPathTypeId = GetInt64(reader, "ai_path_type_id");
                            t.SoundId = GetInt64(reader, "sound_id");
                            t.NpcSpawnerId = GetInt64(reader, "npc_spawner_id");
                            t.PlayCinemaBeforeBubble = GetBool(reader, "play_cinema_before_bubble");
                            t.AiCommandSetId = GetInt64(reader, "ai_command_set_id");
                            t.OrUnitReqs = GetBool(reader, "or_unit_reqs");
                            t.CinemaId = GetInt64(reader, "cinema_id");
                            t.SummaryVoiceId = GetInt64(reader, "summary_voice_id");
                            t.HideQuestMarker = GetBool(reader, "hide_quest_marker");
                            t.BuffId = GetInt64(reader, "buff_id");
                            AaDb.DbQuestComponents.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("quest_component_texts") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_component_texts ORDER BY quest_component_id ASC, quest_component_text_kind_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestComponentText t = new GameQuestComponentText();
                            t.Id = GetInt64(reader, "id");
                            t.QuestComponentTextKindId = GetInt64(reader, "quest_component_text_kind_id");
                            t.QuestComponentId = GetInt64(reader, "quest_component_id");
                            t.Text = GetString(reader, "text");

                            t.TextLocalized = AaDb.GetTranslationById(t.Id, "quest_component_texts", "text", t.Text);

                            AaDb.DbQuestComponentTexts.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("quest_context_texts") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM quest_context_texts ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestContextText t = new GameQuestContextText();
                            t.Id = GetInt64(reader, "id");
                            t.QuestContextTextKindId = GetInt64(reader, "quest_context_text_kind_id");
                            t.QuestContextId = GetInt64(reader, "quest_context_id");
                            t.Text = GetString(reader, "text");

                            t.TextLocalized = AaDb.GetTranslationById(t.Id, "quest_context_texts", "text", t.Text);

                            AaDb.DbQuestContextTexts.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }
            }
        }

    }

    private void ShowDbQuest(long quest_id)
    {
        tvQuestWorkflow.Nodes.Clear();
        if (!AaDb.DbQuestContexts.TryGetValue(quest_id, out var q))
        {
            rtQuestText.Text = "";
            lQuestAddGMCommand.Text = "";
            btnQuestFindRelatedOnMap.Tag = 0;
            return;
        }

        btnQuestFindRelatedOnMap.Tag = quest_id;
        lQuestAddGMCommand.Text = $@"/quest add {q.Id}";

        var rootNode = tvQuestWorkflow.Nodes.Add(q.NameLocalized + " ( " + q.Id + " )");
        rootNode.ForeColor = Color.White;

        var contextNode = rootNode.Nodes.Add("Context");
        contextNode.ForeColor = Color.White;
        var contextFieldsList = GetCustomTableValues("quest_contexts", "id", q.Id.ToString());
        foreach (var contextFields in contextFieldsList)
        {
            foreach (var contextField in contextFields)
            {
                if (contextField.Key == "translate") // we can ignore this one
                    continue;
                else if
                    (contextField.Key ==
                     "category_id") // special hanlding for this one, as we can't use it's general name to autogenrate the name
                {
                    if (AaDb.DbQuestCategories.TryGetValue(q.CategoryId, out var questCat))
                    {
                        contextNode.Nodes.Add("Category: " + questCat.NameLocalized + " ( " + questCat.Id + " )");
                    }
                    else
                        AddCustomPropertyNode(contextField.Key, contextField.Value, false, contextNode);
                    //contextNode.Nodes.Add(AddCustomPropertyInfo(contextField.Key, contextField.Value));
                }
                else
                    AddCustomPropertyNode(contextField.Key, contextField.Value, cbQuestWorkflowHideEmpty.Checked,
                        contextNode);
                //if (!cbQuestWorkflowHideEmpty.Checked || !IsCustomPropertyEmpty(contextField.Value))
                //    contextNode.Nodes.Add(AddCustomPropertyInfo(contextField.Key, contextField.Value));
            }
        }

        contextNode.Expand();

        //dgvQuestComponents.Rows.Clear();
        var comps = from c in AaDb.DbQuestComponents
            where c.Value.QuestContextId == q.Id
            orderby c.Value.ComponentKindId
            select c.Value;

        var questText = "";

        var qTextQuery = from qt in AaDb.DbQuestContextTexts
            where qt.Value.QuestContextId == q.Id
            orderby qt.Value.QuestContextTextKindId
            select qt.Value.TextLocalized;

        foreach (var t in qTextQuery)
            questText += t + "\r\r";

        foreach (var c in comps)
        {
            // Component Info
            var kindName = "";
            switch (c.ComponentKindId)
            {
                case 1:
                    kindName = "None";
                    break;
                case 2:
                    kindName = "Start";
                    break;
                case 3:
                    kindName = "Supply";
                    break;
                case 4:
                    kindName = "Progress";
                    break;
                case 5:
                    kindName = "Fail";
                    break;
                case 6:
                    kindName = "Ready";
                    break;
                case 7:
                    kindName = "Drop";
                    break;
                case 8:
                    kindName = "Reward";
                    break;
            }

            kindName += " ( " + c.ComponentKindId + " )";

            // Quest Component header
            var componentNode = rootNode.Nodes.Add("Component " + c.Id.ToString() + " - " + kindName);
            questText += "|nc;" + kindName + "|r\r\r";

            // Quest description text
            var compTextRaw = from ct in AaDb.DbQuestComponentTexts
                where ct.Value.QuestComponentId == c.Id
                // && ct.Value.quest_component_text_kind_id == c.component_kind_id
                select ct.Value;

            if (compTextRaw != null)
            {
                foreach (var ct in compTextRaw)
                {
                    var compText = AaDb.GetTranslationById(ct.Id, "quest_component_texts", "text", ct.Text);
                    questText += compText + "\r\r";
                }
            }

            componentNode.ForeColor = Color.Yellow;

            var requires = GetQuestComponentRequirements(c.Id);
            var reqNode = AddUnitRequirementNode(requires, c.OrUnitReqs, componentNode.Nodes);

            var componentInfoNode = componentNode.Nodes.Add("Properties");
            componentInfoNode.ForeColor = Color.Yellow;
            var fieldsList = GetCustomTableValues("quest_components", "id", c.Id.ToString());
            foreach (var fields in fieldsList)
            {
                foreach (var field in fields)
                {
                    if (field.Key == "quest_context_id") // skip redundant info
                        continue;
                    if (field.Key == "component_kind_id") // skip redundant info
                        continue;
                    //if (!cbQuestWorkflowHideEmpty.Checked || !IsCustomPropertyEmpty(field.Value))
                    //    componentInfoNode.Nodes.Add(AddCustomPropertyInfo(field.Key, field.Value));
                    AddCustomPropertyNode(field.Key, field.Value, cbQuestWorkflowHideEmpty.Checked,
                        componentInfoNode);
                }
            }

            componentInfoNode.Expand();

            // Acts Info
            var acts = from a in AaDb.DbQuestActs
                where a.Value.QuestComponentId == c.Id
                select a.Value;

            foreach (var a in acts)
            {
                var actsNode = componentNode.Nodes.Add("Act " + a.Id + " - " + a.ActDetailType + " ( " +
                                                       a.ActDetailId + " )");
                actsNode.ForeColor = Color.LimeGreen;
                var actDetailTableName = FunctionTypeToTableName(a.ActDetailType);
                var actsFieldsList = GetCustomTableValues(actDetailTableName, "id", a.ActDetailId.ToString());
                foreach (var fields in actsFieldsList)
                {
                    foreach (var field in fields)
                    {
                        if (field.Key == "quest_act_obj_alias_id")
                        {
                            if (long.TryParse(field.Value, out var aliasId) && (aliasId > 0))
                            {
                                // no idea why this is the name field instead of text
                                var objAlias = AaDb.GetTranslationById(aliasId, "quest_act_obj_aliases", "name",
                                    field.Value);
                                questText += "|ni;QuestActObjAlias(" + field.Value + ")|r \r" + objAlias + "\r\r\r";
                            }
                        }

                        //if (!cbQuestWorkflowHideEmpty.Checked || !IsCustomPropertyEmpty(field.Value))
                        //    actsNode.Nodes.Add(AddCustomPropertyInfo(field.Key, field.Value));
                        AddCustomPropertyNode(field.Key, field.Value, cbQuestWorkflowHideEmpty.Checked, actsNode);
                    }
                }

                actsNode.Expand();
            }
        }

        rootNode.Expand();
        tvQuestWorkflow.SelectedNode = rootNode;
        FormattedTextToRichtEdit(questText, rtQuestText);
        ShowSelectedData("quest_contexts", "id = " + q.Id.ToString(), "id ASC");
    }

    private void LoadSchedules()
    {
        using (var connection = SQLite.CreateConnection())
        {
            // seasonal
            if (AllTableNames.GetValueOrDefault("schedule_items") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM schedule_items ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        var hasPremiumGrade = (reader.GetColumnNames()?.IndexOf("premium_grade_id") >= 0);

                        while (reader.Read())
                        {
                            var t = new GameScheduleItem();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.KindId = (int)GetInt64(reader, "kind_id");
                            t.StYear = (int)GetInt64(reader, "st_year");
                            t.StMonth = (int)GetInt64(reader, "st_month");
                            t.StDay = (int)GetInt64(reader, "st_day");
                            t.StHour = (int)GetInt64(reader, "st_hour");
                            t.StMin = (int)GetInt64(reader, "st_min");
                            t.EdYear = (int)GetInt64(reader, "ed_year");
                            t.EdMonth = (int)GetInt64(reader, "ed_month");
                            t.EdDay = (int)GetInt64(reader, "ed_day");
                            t.EdHour = (int)GetInt64(reader, "ed_hour");
                            t.EdMin = (int)GetInt64(reader, "ed_min");
                            t.GiveTerm = GetInt64(reader, "give_term");
                            t.GiveMax = GetInt64(reader, "give_max");
                            t.ItemId = GetInt64(reader, "item_id");
                            t.ItemCount = GetInt64(reader, "item_count");
                            t.PremiumGradeId = hasPremiumGrade ? GetInt64(reader, "premium_grade_id") : 0;
                            t.ActiveTake = GetBool(reader, "active_take");
                            t.OnAir = GetBool(reader, "on_air");
                            t.ShowWherever = GetBool(reader, "show_wherever");
                            t.ShowWhenever = GetBool(reader, "show_whenever");
                            t.ToolTip = GetString(reader, "tool_tip");
                            t.IconPath = GetString(reader, "icon_path");
                            t.EnableKeyString = GetString(reader, "enable_key_string");
                            t.DisableKeyString = GetString(reader, "disable_key_string");
                            t.LabelKeyString = GetString(reader, "label_key_string");

                            AaDb.DbScheduleItems.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // in-game
            if (AllTableNames.GetValueOrDefault("game_schedules") == SQLite.SQLiteFileName &&
                AllTableNames.GetValueOrDefault("game_schedule_doodads") == SQLite.SQLiteFileName &&
                AllTableNames.GetValueOrDefault("game_schedule_spawners") == SQLite.SQLiteFileName &&
                AllTableNames.GetValueOrDefault("game_schedule_quests") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {

                    command.CommandText = "SELECT * FROM game_schedules ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;
                        var hasName = (reader.GetColumnNames()?.IndexOf("name") >= 0);

                        while (reader.Read())
                        {
                            var t = new GameGameSchedules();
                            t.Id = GetInt64(reader, "id");
                            t.Name = hasName ? GetString(reader, "name") : $"ID{t.Id}";
                            t.DayOfWeekId = (AaDayOfWeek)GetInt64(reader, "day_of_week_id");
                            t.StartTime = GetInt64(reader, "start_time");
                            t.StartTimeMin = GetInt64(reader, "start_time_min");
                            t.EndTime = GetInt64(reader, "end_time");
                            t.EndTimeMin = GetInt64(reader, "end_time_min");
                            t.StYear = GetInt64(reader, "st_year");
                            t.StMonth = GetInt64(reader, "st_month");
                            t.StDay = GetInt64(reader, "st_day");
                            t.StHour = GetInt64(reader, "st_hour");
                            t.StMin = GetInt64(reader, "st_min");
                            t.EdYear = GetInt64(reader, "ed_year");
                            t.EdMonth = GetInt64(reader, "ed_month");
                            t.EdDay = GetInt64(reader, "ed_day");
                            t.EdHour = GetInt64(reader, "ed_hour");
                            t.EdMin = GetInt64(reader, "ed_min");

                            AaDb.DbGameSchedules.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // schedule doodads
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM game_schedule_doodads ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameScheduleDoodads();
                            t.Id = GetInt64(reader, "id");
                            t.GameScheduleId = GetInt64(reader, "game_schedule_id");
                            t.DoodadId = GetInt64(reader, "doodad_id");

                            AaDb.DbScheduleDoodads.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // schedule spawners
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM game_schedule_spawners ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameScheduleSpawners();
                            t.Id = GetInt64(reader, "id");
                            t.GameScheduleId = GetInt64(reader, "game_schedule_id");
                            t.SpawnerId = GetInt64(reader, "spawner_id");

                            AaDb.DbScheduleSpawners.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // schedule quests
                using (var command = connection.CreateCommand())
                {
                    AaDb.DbScheduleQuest.Clear();

                    command.CommandText = "SELECT * FROM game_schedule_quests ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameScheduleQuest();
                            t.Id = GetInt64(reader, "id");
                            t.GameScheduleId = GetInt64(reader, "game_schedule_id");
                            t.QuestId = GetInt64(reader, "quest_id");

                            AaDb.DbScheduleQuest.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }

            // tower defs
            if (AllTableNames.GetValueOrDefault("tower_defs") == SQLite.SQLiteFileName &&
                AllTableNames.GetValueOrDefault("tower_def_progs") == SQLite.SQLiteFileName &&
                AllTableNames.GetValueOrDefault("tower_def_prog_spawn_targets") == SQLite.SQLiteFileName &&
                AllTableNames.GetValueOrDefault("tower_def_prog_kill_targets") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM tower_defs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        var hasName = (reader.GetColumnNames()?.IndexOf("name") >= 0);
                        var hasMilestone = (reader.GetColumnNames()?.IndexOf("milestone_id") >= 0);

                        while (reader.Read())
                        {
                            var t = new GameTowerDefs();
                            t.Id = GetInt64(reader, "id");
                            t.Name = hasName ? GetString(reader, "name") : string.Empty;
                            t.StartMsg = GetString(reader, "start_msg");
                            t.EndMsg = GetString(reader, "end_msg");
                            t.Tod = GetFloat(reader, "tod");
                            t.FirstWaveAfter = GetFloat(reader, "first_wave_after");
                            t.TargetNpcSpawnerId = GetInt64(reader, "target_npc_spawner_id");
                            t.KillNpcId = GetInt64(reader, "kill_npc_id");
                            t.KillNpcCount = GetInt64(reader, "kill_npc_count");
                            t.ForceEndTime = GetFloat(reader, "force_end_time");
                            t.TodDayInterval = GetInt64(reader, "tod_day_interval");
                            t.MilestoneId = hasMilestone ? GetInt64(reader, "milestone_id") : 0;

                            // Helpers
                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "tower_defs", "name", t.Name);
                            t.StartMsgLocalized = AaDb.GetTranslationById(t.Id, "tower_defs", "start_msg", t.Name);
                            t.EndMsgLocalized = AaDb.GetTranslationById(t.Id, "tower_defs", "end_msg", t.Name);
                            t.TitleMsgLocalized = AaDb.GetTranslationById(t.Id, "tower_defs", "title_msg", t.Name);

                            AaDb.DbTowerDefs.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // tower def progs
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM tower_def_progs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameTowerDefProgs();
                            t.Id = GetInt64(reader, "id");
                            t.TowerDefId = GetInt64(reader, "tower_def_id");
                            t.Msg = GetString(reader, "msg");
                            t.CondToNextTime = GetFloat(reader, "cond_to_next_time");
                            t.CondCompByAnd = GetBool(reader, "cond_comp_by_and");

                            // Helpers
                            t.MsgLocalized = AaDb.GetTranslationById(t.Id, "tower_def_progs", "msg", t.Msg);

                            AaDb.DbTowerDefProgs.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // tower def prog spawn targets
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM tower_def_prog_spawn_targets ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameTowerDefProgSpawnTargets();
                            t.Id = GetInt64(reader, "id");
                            t.TowerDefProgId = GetInt64(reader, "tower_def_prog_id");
                            t.SpawnTargetId = GetInt64(reader, "spawn_target_id");
                            t.SpawnTargetType = GetString(reader, "spawn_target_type");
                            t.DespawnOnNextStep = GetBool(reader, "despawn_on_next_step");

                            AaDb.DbTowerDefProgSpawnTargets.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }

                // tower def kill targets
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM tower_def_prog_kill_targets ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GameTowerDefProgKillTargets();
                            t.Id = GetInt64(reader, "id");
                            t.TowerDefProgId = GetInt64(reader, "tower_def_prog_id");
                            t.KillTargetId = GetInt64(reader, "kill_target_id");
                            t.KillTargetType = GetString(reader, "kill_target_type");
                            t.KillCount = GetInt64(reader, "kill_count");

                            AaDb.DbTowerDefProgKillTargets.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        //lbSchedulesIRL.Sorted = true;
        lbSchedulesIRL.Items.Clear();
        foreach (var (key, val) in AaDb.DbScheduleItems)
        {
            lbSchedulesIRL.Items.Add(val);
        }

        //lbSchedulesGame.Sorted = true;
        lbSchedulesGame.Items.Clear();
        foreach (var (key, val) in AaDb.DbGameSchedules)
        {
            lbSchedulesGame.Items.Add(val);
        }

        //lbTowerDefs.Sorted = true;
        lbTowerDefs.Items.Clear();
        foreach (var (key, val) in AaDb.DbTowerDefs)
        {
            lbTowerDefs.Items.Add(val);
        }

        tpSchedules.Enabled = (lbSchedulesIRL.Items.Count > 0) || 
                              (lbSchedulesGame.Items.Count > 0) ||
                              (lbTowerDefs.Items.Count > 0);

        if (lbSchedulesIRL.Items.Count <= 0)
            lbSchedulesIRL.Items.Add("Nothing loaded or incomplete data");
        if (lbSchedulesGame.Items.Count <= 0)
            lbSchedulesGame.Items.Add("Nothing loaded or incomplete data");
        if (lbTowerDefs.Items.Count <= 0)
            lbTowerDefs.Items.Add("Nothing loaded or incomplete data");

        tcScheduleTypes.SelectedTab = tpTowerDefs;
    }

    private void DoQuestsSearch()
    {
        string searchText = cbQuestSearch.Text.ToLower();
        if (searchText == string.Empty)
            return;
        long searchID;
        if (!long.TryParse(searchText, out searchID))
            searchID = -1;

        bool first = true;
        dgvQuests.Rows.Clear();
        foreach (var t in AaDb.DbQuestContexts)
        {
            var q = t.Value;
            if ((q.Id == searchID) || (q.SearchString.IndexOf(searchText) >= 0))
            {
                var line = dgvQuests.Rows.Add();
                var row = dgvQuests.Rows[line];

                row.Cells[0].Value = q.Id.ToString();
                row.Cells[1].Value = q.NameLocalized;
                row.Cells[2].Value = q.Level.ToString();
                if (AaDb.DbZones.TryGetValue(q.ZoneId, out var z))
                {
                    if (AaDb.DbZoneGroups.TryGetValue(z.GroupId, out var zg))
                        row.Cells[3].Value = zg.DisplayTextLocalized;
                    else
                        row.Cells[3].Value = z.DisplayTextLocalized;
                }
                else
                    row.Cells[3].Value = q.ZoneId.ToString();

                if (AaDb.DbQuestCategories.TryGetValue(q.CategoryId, out var qc))
                    row.Cells[4].Value = qc.NameLocalized;
                else
                    row.Cells[4].Value = q.CategoryId.ToString();


                if (first)
                {
                    first = false;
                    ShowDbQuest(q.Id);
                }

            }
        }

        if (dgvQuests.Rows.Count > 0)
            AddToSearchHistory(cbQuestSearch, searchText);
    }

    private void DoQuestsSelectionChanged()
    {
        if (dgvQuests.SelectedRows.Count <= 0)
            return;
        var row = dgvQuests.SelectedRows[0];
        if (row.Cells.Count <= 0)
            return;

        var val = row.Cells[0].Value;
        if (val == null)
            return;

        var qid = long.Parse(val.ToString());
        ShowDbQuest(qid);
        ShowSelectedData("quest_contexts", "id = " + qid.ToString(), "id ASC");
    }

    public void LoadQuestSpheresFromPak()
    {
        if ((Pak == null) || (!Pak.IsOpen))
            return;


        AaDb.PakQuestSignSpheres = new List<QuestSphereEntry>();
        var sl = new List<string>();

        // Find all related files and concat them into a giant stringlist
        foreach (var pfi in Pak.Files)
        {
            var lowerName = pfi.Name.ToLower();
            if (lowerName.EndsWith("quest_sign_sphere.g"))
            {
                var nameSplit = lowerName.Split('/');
                var thisStream = Pak.ExportFileAsStream(pfi);
                using (var rs = new StreamReader(thisStream))
                {
                    sl.Clear();
                    while (!rs.EndOfStream)
                    {
                        sl.Add(rs.ReadLine().Trim().ToLower());
                    }
                }

                var worldName = "";
                var zone = 0;

                if (nameSplit.Length > 6)
                {
                    if ((nameSplit[0] == "game") &&
                        (nameSplit[1] == "worlds") &&
                        (nameSplit[3] == "level_design") &&
                        (nameSplit[4] == "zone") &&
                        (nameSplit[6] == "client")
                       )
                    {
                        worldName = nameSplit[2];
                        zone = int.Parse(nameSplit[5]);
                    }
                }

                var zoneOffX = 0f;
                var zoneOffY = 0f;

                var zoneXml = MapViewWorldXML.main_world.GetZoneByKey(zone);
                if (zoneXml != null)
                {
                    zoneOffX = zoneXml.originCellX * 1024f;
                    zoneOffY = zoneXml.originCellY * 1024f;
                }

                /*
                GameZone gameZone = AADB.GetZoneByKey(zone);
                if (gameZone != null)
                {
                    if (AADB.DB_Zone_Groups.TryGetValue(gameZone.group_id, out var zg))
                    {
                        zoneOffX = zg.PosAndSize.X;
                        zoneOffY = zg.PosAndSize.Y;
                    }
                    else
                    {
                        // no zone group ?
                    }
                }
                else
                {
                    // zone not found in DB
                }
                */

                for (int i = 0; i < sl.Count - 4; i++)
                {
                    var l0 = sl[i + 0]; // area
                    var l1 = sl[i + 1]; // qtype
                    var l2 = sl[i + 2]; // ctype
                    var l3 = sl[i + 3]; // pos
                    var l4 = sl[i + 4]; // radius


                    if (l0.StartsWith("area") && l1.StartsWith("qtype") && l2.StartsWith("ctype") &&
                        l3.StartsWith("pos") && l4.StartsWith("radius"))
                    {
                        try
                        {
                            var qse = new QuestSphereEntry();
                            qse.WorldId = worldName;
                            qse.ZoneKey = zone;

                            qse.QuestId = int.Parse(l1.Substring(6));

                            qse.ComponentId = int.Parse(l2.Substring(6));

                            var subLine = l3.Substring(4).Replace("(", "").Replace(")", "").Replace("x", "")
                                .Replace("y", "").Replace("z", "").Replace(" ", "");
                            var posString = subLine.Split(',');
                            if (posString.Length == 3)
                            {
                                // Parse the floats with NumberStyles.Float and CultureInfo.InvariantCulture or we get all sorts of
                                // weird stuff with the decimal points depending on the user's language settings
                                qse.X = zoneOffX + float.Parse(posString[0], NumberStyles.Float,
                                    CultureInfo.InvariantCulture);
                                qse.Y = zoneOffY + float.Parse(posString[1], NumberStyles.Float,
                                    CultureInfo.InvariantCulture);
                                qse.Z = float.Parse(posString[2], NumberStyles.Float, CultureInfo.InvariantCulture);
                            }

                            qse.Radius = float.Parse(l4.Substring(7), NumberStyles.Float,
                                CultureInfo.InvariantCulture);

                            AaDb.PakQuestSignSpheres.Add(qse);
                            i += 4;
                        }
                        catch (Exception x)
                        {
                            MessageBox.Show("Exception: " + x.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Invalid data at line {i} in {pfi.Name}");
                    }
                }
                // System.Threading.Thread.Sleep(5);
            }
        }


    }

    private void DoFindAllQuestSpheres()
    {
        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        PrepareWorldXml(false);
        var map = MapViewForm.GetMap();
        map.Show();

        if (map.GetQuestSphereCount() > 0)
            if (MessageBox.Show("Keep current Quest Spheres ?", "Add Spheres", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                map.ClearQuestSpheres();

        foreach (var p in AaDb.PakQuestSignSpheres)
        {
            var name = string.Empty;
            if (AaDb.DbQuestContexts.TryGetValue(p.QuestId, out var qc))
                name += qc.NameLocalized + " ";
            name += "q:" + p.QuestId.ToString() + " c:" + p.ComponentId.ToString();
            Color col = Color.LightCyan;

            var isFilteredVal = false;
            if ((eQuestSignSphereSearch.Text != string.Empty) &&
                name.ToLower().Contains(eQuestSignSphereSearch.Text.ToLower()))
                isFilteredVal = true;
            if (isFilteredVal)
                col = Color.Red;

            if (cbQuestSignSphereSearchShowAll.Checked || (eQuestSignSphereSearch.Text == string.Empty))
            {
                map.AddQuestSphere(p.X, p.Y, p.Z, name, col, p.Radius, p.ComponentId);
            }
            else if (isFilteredVal)
                map.AddQuestSphere(p.X, p.Y, p.Z, name, col, p.Radius, p.ComponentId);
        }

        map.tsbShowQuestSphere.Checked = true;
        map.tsbNamesQuestSphere.Checked =
            (!cbQuestSignSphereSearchShowAll.Checked && (eQuestSignSphereSearch.Text != string.Empty));
        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void DoQuestFindRelatedOnMap(long searchId)
    {
        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        PrepareWorldXml(false);
        var map = MapViewForm.GetMap();
        map.Show();

        var foundCount = 0;

        if ((map.GetQuestSphereCount() > 0) || (map.GetPoICount() > 0))
            if (MessageBox.Show("Keep current NPC and Quest Spheres ?", "Add Quest Info", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
            {
                map.ClearPoI();
                map.ClearQuestSpheres();
            }

        var sphereCount = 0;
        // Quest Spheres
        foreach (var p in AaDb.PakQuestSignSpheres)
        {
            var name = string.Empty;
            if (AaDb.DbQuestContexts.TryGetValue(p.QuestId, out var qc))
                name += qc.NameLocalized + " ";
            else
                continue;
            if (qc.Id != searchId)
                continue;
            name += "q:" + p.QuestId.ToString() + " c:" + p.ComponentId.ToString();
            map.AddQuestSphere(p.X, p.Y, p.Z, name, Color.Cyan, p.Radius, p.ComponentId);
            sphereCount++;
        }

        var NPCsToShow = new List<long>();
        // TODO: NPCs (start/end/progress)
        // TODO: Monsters (single, group, zone)
        var comps = from c in AaDb.DbQuestComponents
            where c.Value.QuestContextId == searchId
            select c.Value;
        foreach (var c in comps)
        {
            if ((c.NpcId > 0) && (!NPCsToShow.Contains(c.NpcId)))
                NPCsToShow.Add(c.NpcId);

            var acts = from a in AaDb.DbQuestActs
                where a.Value.QuestComponentId == c.Id
                select a.Value;

            foreach (var a in acts)
            {
                if (a.ActDetailType == "QuestActObjMonsterHunt")
                {
                    string sql = "SELECT * FROM quest_act_obj_monster_hunts WHERE id = " +
                                 a.ActDetailId.ToString();
                    using (var connection = SQLite.CreateConnection())
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.Prepare();
                            using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                            {
                                while (reader.Read())
                                {
                                    var huntNPC = GetInt64(reader, "npc_id");
                                    if (!NPCsToShow.Contains(huntNPC))
                                    {
                                        NPCsToShow.Add(huntNPC);
                                        foundCount++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (NPCsToShow.Count > 0)
        {
            List<MapSpawnLocation> npcList = new List<MapSpawnLocation>();

            using (var loading = new LoadingForm())
            {
                loading.ShowInfo("Searching NPCs in zones: " + AaDb.DbZoneGroups.Count.ToString());
                loading.Show();

                var zoneCount = 0;
                foreach (var zgv in AaDb.DbZoneGroups)
                {
                    var zg = zgv.Value;
                    if (zg != null)
                    {
                        zoneCount++;
                        loading.ShowInfo("Searching in zones: " + zoneCount.ToString() + "/" +
                                         AaDb.DbZoneGroups.Count.ToString());
                        npcList.AddRange(GetNpcSpawnsInZoneGroup(zg.Id, false, NPCsToShow));
                    }
                }

                if (npcList.Count > 0)
                {
                    // Add to NPC list
                    foreach (var npc in npcList)
                    {
                        //if (!NPCsToShow.Contains(npc.id))
                        //    continue;
                        if (AaDb.DbNpCs.TryGetValue(npc.id, out var z))
                        {
                            map.AddPoI(npc.x, npc.y, npc.z, z.NameLocalized + " (" + npc.id.ToString() + ")",
                                Color.Yellow, 0f, "npc", npc.id, z);
                            foundCount++;
                        }
                    }
                }

            }

        }

        if (foundCount <= 0)
        {
            if (NPCsToShow.Count > 0)
                MessageBox.Show("The Quest listed NPCs, but no valid match was found in the dat files.");
        }

        if ((foundCount <= 0) && (sphereCount <= 0))
            MessageBox.Show("Nothing to display.");

        map.tsbShowQuestSphere.Checked = true;
        map.tsbNamesQuestSphere.Checked = true;
        map.FocusAll(true, false, true);
        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void DoSchedulesSelectedIndexChanged()
    {
        var selectedItem = (lbSchedulesIRL.SelectedItem as GameScheduleItem);
        if (selectedItem == null)
            return;
        tvSchedule.Nodes.Clear();
        var rootNode = tvSchedule.Nodes.Add(selectedItem.ToString());
        if ((selectedItem.StYear > 0) && (selectedItem.StMonth > 0) && (selectedItem.StDay > 0))
        {
            var startTime = new DateTime(
                selectedItem.StYear, selectedItem.StMonth, selectedItem.StDay,
                selectedItem.StHour, selectedItem.StMin, selectedItem.StMin);
            rootNode.Nodes.Add($"Starts: {startTime}");
        }

        if ((selectedItem.EdYear > 0) && (selectedItem.EdMonth > 0) && (selectedItem.EdDay > 0))
        {
            var startTime = new DateTime(
                selectedItem.EdYear, selectedItem.EdMonth, selectedItem.EdDay,
                selectedItem.EdHour, selectedItem.EdMin, selectedItem.EdMin);
            rootNode.Nodes.Add($"Ends: {startTime}");
        }

        rootNode.Nodes.Add($"give_term: {selectedItem.GiveTerm}");
        rootNode.Nodes.Add($"give_max: {selectedItem.GiveMax}");

        if ((selectedItem.ItemId > 0) || (selectedItem.ItemCount > 0))
        {
            AddCustomPropertyNode("item_id", selectedItem.ItemId.ToString(), true, rootNode);
            rootNode.Nodes.Add($"item_count: {selectedItem.ItemCount}");
        }

        rootNode.Nodes.Add($"premium_grade_id: {selectedItem.PremiumGradeId}");
        rootNode.Nodes.Add($"active_take: {selectedItem.ActiveTake}");
        rootNode.Nodes.Add($"on_air: {selectedItem.OnAir}");
        rootNode.Nodes.Add($"tool_tip: {AaDb.GetTranslationById(selectedItem.Id, "schedule_items", "tool_tip", selectedItem.ToolTip)}");
        rootNode.Nodes.Add($"show_wherever: {selectedItem.ShowWherever}");
        rootNode.Nodes.Add($"show_whenever: {selectedItem.ShowWhenever}");
        rootNode.Nodes.Add($"icon_path: {selectedItem.IconPath}");
        rootNode.Nodes.Add($"enable_key_string: {selectedItem.EnableKeyString}");
        rootNode.Nodes.Add($"disable_key_string: {selectedItem.DisableKeyString}");
        rootNode.Nodes.Add($"label_key_string: {selectedItem.LabelKeyString}");
        rootNode.ExpandAll();

        ShowSelectedData("schedule_items", "(id = " + selectedItem.Id.ToString() + ")", "id ASC");
    }

    private void DoSchedulesGameSelectedIndexChanged()
    {
        var selectedItem = (lbSchedulesGame.SelectedItem as GameGameSchedules);
        if (selectedItem == null)
            return;
        tvSchedule.Nodes.Clear();
        var rootNode = tvSchedule.Nodes.Add(selectedItem.ToString());

        rootNode.Nodes.Add($"Day of the Week: {selectedItem.DayOfWeekId}");

        rootNode.Nodes.Add($"Starts: {selectedItem.StartTime:00}:{selectedItem.StartTimeMin:00}");
        rootNode.Nodes.Add($"Ends: {selectedItem.EndTime:00}:{selectedItem.EndTimeMin:00}");

        if ((selectedItem.StYear > 0) && (selectedItem.StMonth > 0) && (selectedItem.StDay > 0))
        {
            var startTime = new DateTime(
                (int)selectedItem.StYear, (int)selectedItem.StMonth, (int)selectedItem.StDay,
                (int)selectedItem.StHour, (int)selectedItem.StMin, (int)selectedItem.StMin);
            rootNode.Nodes.Add($"IRL Starts: {startTime}");
        }

        if ((selectedItem.EdYear > 0) && (selectedItem.EdMonth > 0) && (selectedItem.EdDay > 0))
        {
            var startTime = new DateTime(
                (int)selectedItem.EdYear, (int)selectedItem.EdMonth, (int)selectedItem.EdDay,
                (int)selectedItem.EdHour, (int)selectedItem.EdMin, (int)selectedItem.EdMin);
            rootNode.Nodes.Add($"IRL Ends: {startTime}");
        }

        var quests = AaDb.DbScheduleQuest.Values.Where(x => x.GameScheduleId == selectedItem.Id);
        if (quests.Any())
        {
            var questNode = tvSchedule.Nodes.Add("Quests");
            foreach (var gameScheduleQuest in quests)
                AddCustomPropertyNode("quest_id", gameScheduleQuest.QuestId.ToString(), false, questNode);
        }

        var doodads = AaDb.DbScheduleDoodads.Values.Where(x => x.GameScheduleId == selectedItem.Id);
        if (doodads.Any())
        {
            var doodadNode = tvSchedule.Nodes.Add("Doodads");
            foreach (var gameScheduleQuest in doodads)
                AddCustomPropertyNode("doodad_id", gameScheduleQuest.DoodadId.ToString(), false, doodadNode);
        }

        var spawners = AaDb.DbScheduleSpawners.Values.Where(x => x.GameScheduleId == selectedItem.Id);
        if (spawners.Any())
        {
            var spawnersNode = tvSchedule.Nodes.Add("Spawners");
            foreach (var gameScheduleQuest in spawners)
                AddCustomPropertyNode("spawner_id", gameScheduleQuest.SpawnerId.ToString(), false, spawnersNode);
        }

        tvSchedule.ExpandAll();
        ShowSelectedData("game_schedules", "(id = " + selectedItem.Id.ToString() + ")", "id ASC");
    }

    private void DoTowerDefsSelectedIndexChanged()
    {
        var selectedItem = (lbTowerDefs.SelectedItem as GameTowerDefs);
        if (selectedItem == null)
            return;
        tvSchedule.Nodes.Clear();
        var hasName = !string.IsNullOrWhiteSpace(selectedItem.NameLocalized);
        var displayName = hasName ? selectedItem.NameLocalized : selectedItem.TitleMsgLocalized;
        var rootNode = tvSchedule.Nodes.Add($"{selectedItem.Id} - {displayName}");
        rootNode.ImageIndex = 3;

        if (hasName)
            rootNode.Nodes.Add($"Title: {selectedItem.TitleMsgLocalized}");

        if (selectedItem.Tod != 0f)
        {
            var startHours = (int)Math.Floor(selectedItem.Tod);
            var startMinutes = (int)Math.Floor((selectedItem.Tod % 1) * 60f);

            rootNode.Nodes.Add($"Starts at {startHours:00}:{startMinutes:00} game time");
        }
        else
        {
            var triggerCount = 0;
            var conflictZoneWarTriggers = AaDb.DbConflictZones.Values.Where(x => x.WarTowerDefId == selectedItem.Id);
            if (conflictZoneWarTriggers.Any())
            {
                foreach (var conflictZoneWarTrigger in conflictZoneWarTriggers)
                {
                    if (AaDb.DbZoneGroups.TryGetValue(conflictZoneWarTrigger.ZoneGroupId, out var zoneGroup))
                        rootNode.Nodes.Add($"Starts when WAR in {zoneGroup.DisplayTextLocalized} ({zoneGroup.Id})");
                    else
                        rootNode.Nodes.Add($"Starts when WAR in zone_group {conflictZoneWarTrigger.ZoneGroupId})");
                    triggerCount++;
                }
            }
            var conflictZonePeaceTriggers = AaDb.DbConflictZones.Values.Where(x => x.PeaceTowerDefId == selectedItem.Id);
            if (conflictZonePeaceTriggers.Any())
            {
                foreach (var conflictZonePeaceTrigger in conflictZonePeaceTriggers)
                {
                    if (AaDb.DbZoneGroups.TryGetValue(conflictZonePeaceTrigger.ZoneGroupId, out var zoneGroup))
                        rootNode.Nodes.Add($"Starts when PEACE in {zoneGroup.DisplayTextLocalized} ({zoneGroup.Id})");
                    else
                        rootNode.Nodes.Add($"Starts when PEACE in zone_group {conflictZonePeaceTrigger.ZoneGroupId})");
                    triggerCount++;
                }
            }

            if (triggerCount <= 0)
                rootNode.Nodes.Add($"Starts by manual trigger");
        }

        rootNode.Nodes.Add($"First wave after: {selectedItem.FirstWaveAfter} - {MSToString((long)Math.Round(selectedItem.FirstWaveAfter*1000f))}");
        rootNode.Nodes.Add($"Force end after: {selectedItem.ForceEndTime} - {MSToString((long)Math.Round(selectedItem.ForceEndTime * 1000f))}");
        rootNode.Nodes.Add($"ToD Day Interval: {selectedItem.TodDayInterval}");
        AddCustomPropertyNode("target_npc_spawner_id", selectedItem.TargetNpcSpawnerId.ToString(), false, rootNode);
        AddCustomPropertyNode("kill_npc_id", selectedItem.KillNpcId.ToString(), false, rootNode);
        AddCustomPropertyNode("kill_npc_count", selectedItem.KillNpcCount.ToString(), false, rootNode);

        rootNode.Nodes.Add($"Start Message: {selectedItem.StartMsgLocalized}");
        rootNode.Nodes.Add($"End Message: {selectedItem.EndMsgLocalized}");

        var steps = AaDb.DbTowerDefProgs.Values.Where(x => x.TowerDefId == selectedItem.Id)?.OrderBy(x => x.Id);
        if (steps.Any())
        {
            foreach (var step in steps)
            {
                var stepNode = tvSchedule.Nodes.Add($"{step.Id} - {step.MsgLocalized}");
                stepNode.ImageIndex = 2;
                // AddCustomPropertyNode("cond_to_next_time", ((long)Math.Round(step.cond_to_next_time * 1000f)).ToString(), true, stepNode);
                stepNode.Nodes.Add($"cond_to_next_time: {step.CondToNextTime} - {MSToString((long)Math.Round(step.CondToNextTime * 1000f))}");
                stepNode.Nodes.Add($"cond_comp_by_and: {step.CondCompByAnd}");

                var spawnsNode = stepNode.Nodes.Add("Spawns");
                var spawnTargets = AaDb.DbTowerDefProgSpawnTargets.Values.Where(x => x.TowerDefProgId == step.Id);
                if (spawnTargets.Any())
                {
                    foreach (var spawnTarget in spawnTargets)
                    {
                        TreeNode spawnNode = null;
                        if (spawnTarget.SpawnTargetType == "DoodadAlmighty")
                            spawnNode = AddCustomPropertyNode("doodad_id", spawnTarget.SpawnTargetId.ToString(), false, spawnsNode);
                        else if (spawnTarget.SpawnTargetType == "NpcSpawner")
                            spawnNode = AddCustomPropertyNode("npc_spawner_id", spawnTarget.SpawnTargetId.ToString(), false, spawnsNode);
                        else
                            spawnNode = spawnsNode.Nodes.Add($"{spawnTarget.SpawnTargetType} - {spawnTarget.SpawnTargetId}");

                        if (spawnTarget.DespawnOnNextStep)
                            spawnNode.Nodes.Add("Despawns on next step").ImageIndex = 3;
                    }
                }

                var killsNode = stepNode.Nodes.Add("Kills");
                var killTargets = AaDb.DbTowerDefProgKillTargets.Values.Where(x => x.TowerDefProgId == step.Id);
                if (killTargets.Any())
                {
                    foreach (var killTarget in killTargets)
                    {
                        TreeNode spawnNode = null;
                        if (killTarget.KillTargetType == "DoodadAlmighty")
                            spawnNode = AddCustomPropertyNode("doodad_id", killTarget.KillTargetId.ToString(), false, killsNode);
                        else if (killTarget.KillTargetType == "Npc")
                            spawnNode = AddCustomPropertyNode("npc_id", killTarget.KillTargetId.ToString(), false, killsNode);
                        else
                            spawnNode = killsNode.Nodes.Add($"{killTarget.KillTargetType} - {killTarget.KillTargetId}");

                        if (killTarget.KillCount != 1)
                            spawnNode.Text += $" x {killTarget.KillCount}";
                    }
                }
            }
        }

        rootNode.ExpandAll();

        ShowSelectedData("tower_defs", "(id = " + selectedItem.Id.ToString() + ")", "id ASC");
    }

    private void LoadSpheresFromCompact()
    {
        using var connection = SQLite.CreateConnection();

        if (AllTableNames.GetValueOrDefault("schedule_items") == SQLite.SQLiteFileName)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM spheres ORDER BY id ASC";
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {
                        var t = new GameSpheres();
                        t.Id = GetInt64(reader, "id");
                        t.Name = GetString(reader, "name");
                        t.EnterOrLeave = GetBool(reader, "enter_or_leave");
                        t.SphereDetailId = GetInt64(reader, "sphere_detail_id");
                        t.SphereDetailType = GetString(reader, "sphere_detail_type");
                        t.TriggerConditionId = GetInt64(reader, "trigger_condition_id");
                        t.TriggerConditionTime = GetInt64(reader, "trigger_condition_time");
                        t.TeamMsg = GetString(reader, "team_msg");
                        t.CategoryId = GetInt64(reader, "category_id");
                        t.OrUnitReqs = GetBool(reader, "or_unit_reqs");
                        t.IsPersonalMsg = GetBool(reader, "is_personal_msg");

                        AaDb.DbSpheres.Add(t.Id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;

                }
            }
        }
    }
}