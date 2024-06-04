using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBDefs;
using AAEmu.DBViewer.enums;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer
{
    public partial class MainForm
    {
        private void LoadQuests()
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Categories.Clear();

                    command.CommandText = "SELECT * FROM quest_categories ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestCategory t = new GameQuestCategory();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");

                            t.nameLocalized = AADB.GetTranslationByID(t.id, "quest_categories", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Quest_Categories.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Contexts.Clear();

                    command.CommandText = "SELECT * FROM quest_contexts ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestContexts t = new GameQuestContexts();
                            t.id = GetInt64(reader, "id");
                            t.name = GetString(reader, "name");
                            t.category_id = GetInt64(reader, "category_id");
                            t.repeatable = GetBool(reader, "repeatable");
                            t.level = GetInt64(reader, "level");
                            t.selective = GetBool(reader, "selective");
                            t.successive = GetBool(reader, "successive");
                            t.restart_on_fail = GetBool(reader, "restart_on_fail");
                            t.chapter_idx = GetInt64(reader, "chapter_idx");
                            t.quest_idx = GetInt64(reader, "quest_idx");
                            // t.milestone_id = GetInt64(reader, "milestone_id");
                            t.let_it_done = GetBool(reader, "let_it_done");
                            t.detail_id = GetInt64(reader, "detail_id");
                            t.zone_id = GetInt64(reader, "zone_id");
                            t.degree = GetInt64(reader, "degree");
                            t.use_quest_camera = GetBool(reader, "use_quest_camera");
                            t.score = GetInt64(reader, "score");
                            t.use_accept_message = GetBool(reader, "use_accept_message");
                            t.use_complete_message = GetBool(reader, "use_complete_message");
                            t.grade_id = GetInt64(reader, "grade_id");


                            t.nameLocalized = AADB.GetTranslationByID(t.id, "quest_contexts", "name", t.name);

                            t.SearchString = t.name + " " + t.nameLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AADB.DB_Quest_Contexts.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Acts.Clear();

                    command.CommandText = "SELECT * FROM quest_acts ORDER BY quest_component_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestAct t = new GameQuestAct();
                            t.id = GetInt64(reader, "id");
                            t.quest_component_id = GetInt64(reader, "quest_component_id");
                            t.act_detail_id = GetInt64(reader, "act_detail_id");
                            t.act_detail_type = GetString(reader, "act_detail_type");

                            AADB.DB_Quest_Acts.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Components.Clear();

                    command.CommandText =
                        "SELECT * FROM quest_components ORDER BY quest_context_id ASC, component_kind_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestComponent t = new GameQuestComponent();
                            t.id = GetInt64(reader, "id");
                            t.quest_context_id = GetInt64(reader, "quest_context_id");
                            t.component_kind_id = GetInt64(reader, "component_kind_id");
                            t.next_component = GetInt64(reader, "next_component");
                            t.npc_ai_id = GetInt64(reader, "npc_ai_id");
                            t.npc_id = GetInt64(reader, "npc_id");
                            t.skill_id = GetInt64(reader, "skill_id");
                            t.skill_self = GetBool(reader, "skill_self");
                            t.ai_path_name = GetString(reader, "ai_path_name");
                            t.ai_path_type_id = GetInt64(reader, "ai_path_type_id");
                            t.sound_id = GetInt64(reader, "sound_id");
                            t.npc_spawner_id = GetInt64(reader, "npc_spawner_id");
                            t.play_cinema_before_bubble = GetBool(reader, "play_cinema_before_bubble");
                            t.ai_command_set_id = GetInt64(reader, "ai_command_set_id");
                            t.or_unit_reqs = GetBool(reader, "or_unit_reqs");
                            t.cinema_id = GetInt64(reader, "cinema_id");
                            t.summary_voice_id = GetInt64(reader, "summary_voice_id");
                            t.hide_quest_marker = GetBool(reader, "hide_quest_marker");
                            t.buff_id = GetInt64(reader, "buff_id");
                            AADB.DB_Quest_Components.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Component_Texts.Clear();

                    command.CommandText =
                        "SELECT * FROM quest_component_texts ORDER BY quest_component_id ASC, quest_component_text_kind_id ASC, id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestComponentText t = new GameQuestComponentText();
                            t.id = GetInt64(reader, "id");
                            t.quest_component_text_kind_id = GetInt64(reader, "quest_component_text_kind_id");
                            t.quest_component_id = GetInt64(reader, "quest_component_id");
                            t.text = GetString(reader, "text");

                            t.textLocalized = AADB.GetTranslationByID(t.id, "quest_component_texts", "text", t.text);

                            AADB.DB_Quest_Component_Texts.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }

                using (var command = connection.CreateCommand())
                {
                    AADB.DB_Quest_Context_Texts.Clear();

                    command.CommandText = "SELECT * FROM quest_context_texts ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            GameQuestContextText t = new GameQuestContextText();
                            t.id = GetInt64(reader, "id");
                            t.quest_context_text_kind_id = GetInt64(reader, "quest_context_text_kind_id");
                            t.quest_context_id = GetInt64(reader, "quest_context_id");
                            t.text = GetString(reader, "text");

                            t.textLocalized = AADB.GetTranslationByID(t.id, "quest_context_texts", "text", t.text);

                            AADB.DB_Quest_Context_Texts.Add(t.id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;

                    }
                }



            }

        }

        private void ShowDbQuest(long quest_id)
        {
            tvQuestWorkflow.Nodes.Clear();
            if (!AADB.DB_Quest_Contexts.TryGetValue(quest_id, out var q))
            {
                rtQuestText.Text = "";
                btnQuestFindRelatedOnMap.Tag = 0;
                return;
            }

            btnQuestFindRelatedOnMap.Tag = quest_id;
            var rootNode = tvQuestWorkflow.Nodes.Add(q.nameLocalized + " ( " + q.id + " )");
            rootNode.ForeColor = Color.White;

            var contextNode = rootNode.Nodes.Add("Context");
            contextNode.ForeColor = Color.White;
            var contextFieldsList = GetCustomTableValues("quest_contexts", "id", q.id.ToString());
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
                        if (AADB.DB_Quest_Categories.TryGetValue(q.category_id, out var questCat))
                        {
                            contextNode.Nodes.Add("Category: " + questCat.nameLocalized + " ( " + questCat.id + " )");
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
            var comps = from c in AADB.DB_Quest_Components
                where c.Value.quest_context_id == q.id
                orderby c.Value.component_kind_id
                select c.Value;

            var questText = "";

            var qTextQuery = from qt in AADB.DB_Quest_Context_Texts
                where qt.Value.quest_context_id == q.id
                orderby qt.Value.quest_context_text_kind_id
                select qt.Value.textLocalized;

            foreach (var t in qTextQuery)
                questText += t + "\r\r";

            foreach (var c in comps)
            {
                // Component Info
                var kindName = "";
                switch (c.component_kind_id)
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

                kindName += " ( " + c.component_kind_id + " )";

                // Quest Component header
                var componentNode = rootNode.Nodes.Add("Component " + c.id.ToString() + " - " + kindName);
                questText += "|nc;" + kindName + "|r\r\r";

                // Quest description text
                var compTextRaw = from ct in AADB.DB_Quest_Component_Texts
                    where ct.Value.quest_component_id == c.id
                    // && ct.Value.quest_component_text_kind_id == c.component_kind_id
                    select ct.Value;

                if (compTextRaw != null)
                {
                    foreach (var ct in compTextRaw)
                    {
                        var compText = AADB.GetTranslationByID(ct.id, "quest_component_texts", "text", ct.text);
                        questText += compText + "\r\r";
                    }
                }

                componentNode.ForeColor = Color.Yellow;
                var componentInfoNode = componentNode.Nodes.Add("Properties");
                componentInfoNode.ForeColor = Color.Yellow;
                var fieldsList = GetCustomTableValues("quest_components", "id", c.id.ToString());
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
                var acts = from a in AADB.DB_Quest_Acts
                    where a.Value.quest_component_id == c.id
                    select a.Value;

                foreach (var a in acts)
                {
                    var actsNode = componentNode.Nodes.Add("Act " + a.id + " - " + a.act_detail_type + " ( " +
                                                           a.act_detail_id + " )");
                    actsNode.ForeColor = Color.LimeGreen;
                    var actDetailTableName = FunctionTypeToTableName(a.act_detail_type);
                    var actsFieldsList = GetCustomTableValues(actDetailTableName, "id", a.act_detail_id.ToString());
                    foreach (var fields in actsFieldsList)
                    {
                        foreach (var field in fields)
                        {
                            if (field.Key == "quest_act_obj_alias_id")
                            {
                                if (long.TryParse(field.Value, out var aliasId) && (aliasId > 0))
                                {
                                    // no idea why this is the name field instead of text
                                    var objAlias = AADB.GetTranslationByID(aliasId, "quest_act_obj_aliases", "name",
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
            ShowSelectedData("quest_contexts", "id = " + q.id.ToString(), "id ASC");
        }

        private void LoadSchedules()
        {
            using (var connection = SQLite.CreateConnection())
            {
                // seasonal
                AADB.DB_ScheduleItems.Clear();
                if (allTableNames.Contains("schedule_items"))
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
                                t.id = GetInt64(reader, "id");
                                t.name = GetString(reader, "name");
                                t.kind_id = (int)GetInt64(reader, "kind_id");
                                t.st_year = (int)GetInt64(reader, "st_year");
                                t.st_month = (int)GetInt64(reader, "st_month");
                                t.st_day = (int)GetInt64(reader, "st_day");
                                t.st_hour = (int)GetInt64(reader, "st_hour");
                                t.st_min = (int)GetInt64(reader, "st_min");
                                t.ed_year = (int)GetInt64(reader, "ed_year");
                                t.ed_month = (int)GetInt64(reader, "ed_month");
                                t.ed_day = (int)GetInt64(reader, "ed_day");
                                t.ed_hour = (int)GetInt64(reader, "ed_hour");
                                t.ed_min = (int)GetInt64(reader, "ed_min");
                                t.give_term = GetInt64(reader, "give_term");
                                t.give_max = GetInt64(reader, "give_max");
                                t.item_id = GetInt64(reader, "item_id");
                                t.item_count = GetInt64(reader, "item_count");
                                t.premium_grade_id = hasPremiumGrade ? GetInt64(reader, "premium_grade_id") : 0;
                                t.active_take = GetBool(reader, "active_take");
                                t.on_air = GetBool(reader, "on_air");
                                t.show_wherever = GetBool(reader, "show_wherever");
                                t.show_whenever = GetBool(reader, "show_whenever");
                                t.tool_tip = GetString(reader, "tool_tip");
                                t.icon_path = GetString(reader, "icon_path");
                                t.enable_key_string = GetString(reader, "enable_key_string");
                                t.disable_key_string = GetString(reader, "disable_key_string");
                                t.label_key_string = GetString(reader, "label_key_string");

                                AADB.DB_ScheduleItems.Add(t.id, t);
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;
                        }
                    }
                }

                // in-game
                AADB.DB_GameSchedules.Clear();
                AADB.DB_ScheduleDoodads.Clear();
                AADB.DB_ScheduleSpawners.Clear();
                AADB.DB_ScheduleQuest.Clear();
                if (allTableNames.Contains("game_schedules") &&
                    allTableNames.Contains("game_schedule_doodads") &&
                    allTableNames.Contains("game_schedule_spawners") &&
                    allTableNames.Contains("game_schedule_quests"))
                {
                    using (var command = connection.CreateCommand())
                    {

                        command.CommandText = "SELECT * FROM game_schedules ORDER BY id ASC";
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            Application.UseWaitCursor = true;
                            Cursor = Cursors.WaitCursor;

                            while (reader.Read())
                            {
                                var t = new GameGameSchedules();
                                t.id = GetInt64(reader, "id");
                                t.name = GetString(reader, "name");
                                t.day_of_week_id = (AaDayOfWeek)GetInt64(reader, "day_of_week_id");
                                t.start_time = GetInt64(reader, "start_time");
                                t.start_time_min = GetInt64(reader, "start_time_min");
                                t.end_time = GetInt64(reader, "end_time");
                                t.end_time_min = GetInt64(reader, "end_time_min");
                                t.st_year = GetInt64(reader, "st_year");
                                t.st_month = GetInt64(reader, "st_month");
                                t.st_day = GetInt64(reader, "st_day");
                                t.st_hour = GetInt64(reader, "st_hour");
                                t.st_min = GetInt64(reader, "st_min");
                                t.ed_year = GetInt64(reader, "ed_year");
                                t.ed_month = GetInt64(reader, "ed_month");
                                t.ed_day = GetInt64(reader, "ed_day");
                                t.ed_hour = GetInt64(reader, "ed_hour");
                                t.ed_min = GetInt64(reader, "ed_min");

                                AADB.DB_GameSchedules.Add(t.id, t);
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
                                t.id = GetInt64(reader, "id");
                                t.game_schedule_id = GetInt64(reader, "game_schedule_id");
                                t.doodad_id = GetInt64(reader, "doodad_id");

                                AADB.DB_ScheduleDoodads.Add(t.id, t);
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
                                t.id = GetInt64(reader, "id");
                                t.game_schedule_id = GetInt64(reader, "game_schedule_id");
                                t.spawner_id = GetInt64(reader, "spawner_id");

                                AADB.DB_ScheduleSpawners.Add(t.id, t);
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;
                        }
                    }

                    // schedule quests
                    using (var command = connection.CreateCommand())
                    {
                        AADB.DB_ScheduleQuest.Clear();

                        command.CommandText = "SELECT * FROM game_schedule_quests ORDER BY id ASC";
                        command.Prepare();
                        using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                        {
                            Application.UseWaitCursor = true;
                            Cursor = Cursors.WaitCursor;

                            while (reader.Read())
                            {
                                var t = new GameScheduleQuest();
                                t.id = GetInt64(reader, "id");
                                t.game_schedule_id = GetInt64(reader, "game_schedule_id");
                                t.quest_id = GetInt64(reader, "quest_id");

                                AADB.DB_ScheduleQuest.Add(t.id, t);
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;
                        }
                    }
                }

                // tower defs
                AADB.DB_TowerDefs.Clear();
                AADB.DB_TowerDefProgs.Clear();
                AADB.DB_TowerDefProgSpawnTargets.Clear();
                AADB.DB_TowerDefProgKillTargets.Clear();
                if (allTableNames.Contains("tower_defs") &&
                    allTableNames.Contains("tower_def_progs") &&
                    allTableNames.Contains("tower_def_prog_spawn_targets") &&
                    allTableNames.Contains("tower_def_prog_kill_targets"))
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
                                t.id = GetInt64(reader, "id");
                                t.name = hasName ? GetString(reader, "name") : string.Empty;
                                t.start_msg = GetString(reader, "start_msg");
                                t.end_msg = GetString(reader, "end_msg");
                                t.tod = GetFloat(reader, "tod");
                                t.first_wave_after = GetFloat(reader, "first_wave_after");
                                t.target_npc_spawner_id = GetInt64(reader, "target_npc_spawner_id");
                                t.kill_npc_id = GetInt64(reader, "kill_npc_id");
                                t.kill_npc_count = GetInt64(reader, "kill_npc_count");
                                t.force_end_time = GetFloat(reader, "force_end_time");
                                t.tod_day_interval = GetInt64(reader, "tod_day_interval");
                                t.milestone_id = hasMilestone ? GetInt64(reader, "milestone_id") : 0;

                                // Helpers
                                t.nameLocalized = AADB.GetTranslationByID(t.id, "tower_defs", "name", t.name);
                                t.start_msgLocalized = AADB.GetTranslationByID(t.id, "tower_defs", "start_msg", t.name);
                                t.end_msgLocalized = AADB.GetTranslationByID(t.id, "tower_defs", "end_msg", t.name);
                                t.title_msgLocalized = AADB.GetTranslationByID(t.id, "tower_defs", "title_msg", t.name);

                                AADB.DB_TowerDefs.Add(t.id, t);
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
                                t.id = GetInt64(reader, "id");
                                t.tower_def_id = GetInt64(reader, "tower_def_id");
                                t.msg = GetString(reader, "msg");
                                t.cond_to_next_time = GetFloat(reader, "cond_to_next_time");
                                t.cond_comp_by_and = GetBool(reader, "cond_comp_by_and");

                                // Helpers
                                t.msgLocalized = AADB.GetTranslationByID(t.id, "tower_def_progs", "msg", t.msg);

                                AADB.DB_TowerDefProgs.Add(t.id, t);
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
                                t.id = GetInt64(reader, "id");
                                t.tower_def_prog_id = GetInt64(reader, "tower_def_prog_id");
                                t.spawn_target_id = GetInt64(reader, "spawn_target_id");
                                t.spawn_target_type = GetString(reader, "spawn_target_type");
                                t.despawn_on_next_step = GetBool(reader, "despawn_on_next_step");

                                AADB.DB_TowerDefProgSpawnTargets.Add(t.id, t);
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
                                t.id = GetInt64(reader, "id");
                                t.tower_def_prog_id = GetInt64(reader, "tower_def_prog_id");
                                t.kill_target_id = GetInt64(reader, "kill_target_id");
                                t.kill_target_type = GetString(reader, "kill_target_type");
                                t.kill_count = GetInt64(reader, "kill_count");

                                AADB.DB_TowerDefProgKillTargets.Add(t.id, t);
                            }

                            Cursor = Cursors.Default;
                            Application.UseWaitCursor = false;
                        }
                    }
                }
            }

            //lbSchedulesIRL.Sorted = true;
            lbSchedulesIRL.Items.Clear();
            foreach (var (key, val) in AADB.DB_ScheduleItems)
            {
                lbSchedulesIRL.Items.Add(val);
            }

            //lbSchedulesGame.Sorted = true;
            lbSchedulesGame.Items.Clear();
            foreach (var (key, val) in AADB.DB_GameSchedules)
            {
                lbSchedulesGame.Items.Add(val);
            }

            //lbTowerDefs.Sorted = true;
            lbTowerDefs.Items.Clear();
            foreach (var (key, val) in AADB.DB_TowerDefs)
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

        private void BtnQuestsSearch_Click(object sender, EventArgs e)
        {
            string searchText = cbQuestSearch.Text.ToLower();
            if (searchText == string.Empty)
                return;
            long searchID;
            if (!long.TryParse(searchText, out searchID))
                searchID = -1;

            bool first = true;
            dgvQuests.Rows.Clear();
            foreach (var t in AADB.DB_Quest_Contexts)
            {
                var q = t.Value;
                if ((q.id == searchID) || (q.SearchString.IndexOf(searchText) >= 0))
                {
                    var line = dgvQuests.Rows.Add();
                    var row = dgvQuests.Rows[line];

                    row.Cells[0].Value = q.id.ToString();
                    row.Cells[1].Value = q.nameLocalized;
                    row.Cells[2].Value = q.level.ToString();
                    if (AADB.DB_Zones.TryGetValue(q.zone_id, out var z))
                    {
                        if (AADB.DB_Zone_Groups.TryGetValue(z.group_id, out var zg))
                            row.Cells[3].Value = zg.display_textLocalized;
                        else
                            row.Cells[3].Value = z.display_textLocalized;
                    }
                    else
                        row.Cells[3].Value = q.zone_id.ToString();

                    if (AADB.DB_Quest_Categories.TryGetValue(q.category_id, out var qc))
                        row.Cells[4].Value = qc.nameLocalized;
                    else
                        row.Cells[4].Value = q.category_id.ToString();


                    if (first)
                    {
                        first = false;
                        ShowDbQuest(q.id);
                    }

                }
            }

            if (dgvQuests.Rows.Count > 0)
                AddToSearchHistory(cbQuestSearch, searchText);
        }

        private void TQuestSearch_TextChanged(object sender, EventArgs e)
        {
            btnQuestsSearch.Enabled = (cbQuestSearch.Text != string.Empty);
        }

        private void TQuestSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnQuestsSearch_Click(null, null);
            }
        }

        private void DgvQuests_SelectionChanged(object sender, EventArgs e)
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

        public void LoadQuestSpheres()
        {
            if ((pak == null) || (!pak.IsOpen))
                return;


            AADB.PAK_QuestSignSpheres = new List<QuestSphereEntry>();
            var sl = new List<string>();

            // Find all related files and concat them into a giant stringlist
            foreach (var pfi in pak.Files)
            {
                var lowerName = pfi.Name.ToLower();
                if (lowerName.EndsWith("quest_sign_sphere.g"))
                {
                    var nameSplit = lowerName.Split('/');
                    var thisStream = pak.ExportFileAsStream(pfi);
                    using (var rs = new StreamReader(thisStream))
                    {
                        sl.Clear();
                        while (!rs.EndOfStream)
                        {
                            sl.Add(rs.ReadLine().Trim(' ').Trim('\t').ToLower());
                        }
                    }

                    var worldname = "";
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
                            worldname = nameSplit[2];
                            zone = int.Parse(nameSplit[5]);
                        }
                    }

                    var zoneOffX = 0f;
                    var zoneOffY = 0f;

                    var zonexml = MapViewWorldXML.main_world.GetZoneByKey(zone);
                    if (zonexml != null)
                    {
                        zoneOffX = zonexml.originCellX * 1024f;
                        zoneOffY = zonexml.originCellY * 1024f;
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
                                qse.worldID = worldname;
                                qse.zoneID = zone;

                                qse.questID = int.Parse(l1.Substring(6));

                                qse.componentID = int.Parse(l2.Substring(6));

                                var subline = l3.Substring(4).Replace("(", "").Replace(")", "").Replace("x", "")
                                    .Replace("y", "").Replace("z", "").Replace(" ", "");
                                var posstring = subline.Split(',');
                                if (posstring.Length == 3)
                                {
                                    // Parse the floats with NumberStyles.Float and CultureInfo.InvariantCulture or we get all sorts of
                                    // weird stuff with the decimal points depending on the user's language settings
                                    qse.X = zoneOffX + float.Parse(posstring[0], NumberStyles.Float,
                                        CultureInfo.InvariantCulture);
                                    qse.Y = zoneOffY + float.Parse(posstring[1], NumberStyles.Float,
                                        CultureInfo.InvariantCulture);
                                    qse.Z = float.Parse(posstring[2], NumberStyles.Float, CultureInfo.InvariantCulture);
                                }

                                qse.radius = float.Parse(l4.Substring(7), NumberStyles.Float,
                                    CultureInfo.InvariantCulture);

                                AADB.PAK_QuestSignSpheres.Add(qse);
                                i += 5;
                            }
                            catch (Exception x)
                            {
                                MessageBox.Show("Exception: " + x.Message, "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    // System.Threading.Thread.Sleep(5);
                }
            }


        }

        private void BtnFindAllQuestSpheres_Click(object sender, EventArgs e)
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

            foreach (var p in AADB.PAK_QuestSignSpheres)
            {
                var name = string.Empty;
                if (AADB.DB_Quest_Contexts.TryGetValue(p.questID, out var qc))
                    name += qc.nameLocalized + " ";
                name += "q:" + p.questID.ToString() + " c:" + p.componentID.ToString();
                Color col = Color.LightCyan;

                var isFilteredVal = false;
                if ((eQuestSignSphereSearch.Text != string.Empty) &&
                    name.ToLower().Contains(eQuestSignSphereSearch.Text.ToLower()))
                    isFilteredVal = true;
                if (isFilteredVal)
                    col = Color.Red;

                if (cbQuestSignSphereSearchShowAll.Checked || (eQuestSignSphereSearch.Text == string.Empty))
                {
                    map.AddQuestSphere(p.X, p.Y, p.Z, name, col, p.radius, p.componentID);
                }
                else if (isFilteredVal)
                    map.AddQuestSphere(p.X, p.Y, p.Z, name, col, p.radius, p.componentID);
            }

            map.tsbShowQuestSphere.Checked = true;
            map.tsbNamesQuestSphere.Checked =
                (!cbQuestSignSphereSearchShowAll.Checked && (eQuestSignSphereSearch.Text != string.Empty));
            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void BtnQuestFindRelatedOnMap_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            if ((sender as Button).Tag == null)
                return;
            var searchId = (long)(sender as Button).Tag;
            if (searchId <= 0)
                return;

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
            foreach (var p in AADB.PAK_QuestSignSpheres)
            {
                var name = string.Empty;
                if (AADB.DB_Quest_Contexts.TryGetValue(p.questID, out var qc))
                    name += qc.nameLocalized + " ";
                else
                    continue;
                if (qc.id != searchId)
                    continue;
                name += "q:" + p.questID.ToString() + " c:" + p.componentID.ToString();
                map.AddQuestSphere(p.X, p.Y, p.Z, name, Color.Cyan, p.radius, p.componentID);
                sphereCount++;
            }

            var NPCsToShow = new List<long>();
            // TODO: NPCs (start/end/progress)
            // TODO: Monsters (single, group, zone)
            var comps = from c in AADB.DB_Quest_Components
                where c.Value.quest_context_id == searchId
                select c.Value;
            foreach (var c in comps)
            {
                if ((c.npc_id > 0) && (!NPCsToShow.Contains(c.npc_id)))
                    NPCsToShow.Add(c.npc_id);

                var acts = from a in AADB.DB_Quest_Acts
                    where a.Value.quest_component_id == c.id
                    select a.Value;

                foreach (var a in acts)
                {
                    if (a.act_detail_type == "QuestActObjMonsterHunt")
                    {
                        string sql = "SELECT * FROM quest_act_obj_monster_hunts WHERE id = " +
                                     a.act_detail_id.ToString();
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
                    loading.ShowInfo("Searching NPCs in zones: " + AADB.DB_Zone_Groups.Count.ToString());
                    loading.Show();

                    var zoneCount = 0;
                    foreach (var zgv in AADB.DB_Zone_Groups)
                    {
                        var zg = zgv.Value;
                        if (zg != null)
                        {
                            zoneCount++;
                            loading.ShowInfo("Searching in zones: " + zoneCount.ToString() + "/" +
                                             AADB.DB_Zone_Groups.Count.ToString());
                            npcList.AddRange(GetNpcSpawnsInZoneGroup(zg.id, false, NPCsToShow));
                        }
                    }

                    if (npcList.Count > 0)
                    {
                        // Add to NPC list
                        foreach (var npc in npcList)
                        {
                            //if (!NPCsToShow.Contains(npc.id))
                            //    continue;
                            if (AADB.DB_NPCs.TryGetValue(npc.id, out var z))
                            {
                                map.AddPoI(npc.x, npc.y, npc.z, z.nameLocalized + " (" + npc.id.ToString() + ")",
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

        private void TvQuestWorkflow_DoubleClick(object sender, EventArgs e)
        {
            if ((sender is TreeView tv) && (tv.SelectedNode != null))
                ProcessNodeInfoDoubleClick(tv.SelectedNode);
        }

        private void CbQuestWorkflowHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if ((dgvQuests.CurrentRow != null) && (dgvQuests.CurrentRow.Cells.Count > 0))
            {
                if (long.TryParse(dgvQuests.CurrentRow.Cells[0].Value.ToString(), out var id))
                    ShowDbQuest(id);
            }
        }

        private void LbSchedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (lbSchedulesIRL.SelectedItem as GameScheduleItem);
            if (selectedItem == null)
                return;
            tvSchedule.Nodes.Clear();
            var rootNode = tvSchedule.Nodes.Add(selectedItem.ToString());
            if ((selectedItem.st_year > 0) && (selectedItem.st_month > 0) && (selectedItem.st_day > 0))
            {
                var startTime = new DateTime(
                    selectedItem.st_year, selectedItem.st_month, selectedItem.st_day,
                    selectedItem.st_hour, selectedItem.st_min, selectedItem.st_min);
                rootNode.Nodes.Add($"Starts: {startTime}");
            }

            if ((selectedItem.ed_year > 0) && (selectedItem.ed_month > 0) && (selectedItem.ed_day > 0))
            {
                var startTime = new DateTime(
                    selectedItem.ed_year, selectedItem.ed_month, selectedItem.ed_day,
                    selectedItem.ed_hour, selectedItem.ed_min, selectedItem.ed_min);
                rootNode.Nodes.Add($"Ends: {startTime}");
            }

            rootNode.Nodes.Add($"give_term: {selectedItem.give_term}");
            rootNode.Nodes.Add($"give_max: {selectedItem.give_max}");

            if ((selectedItem.item_id > 0) || (selectedItem.item_count > 0))
            {
                AddCustomPropertyNode("item_id", selectedItem.item_id.ToString(), true, rootNode);
                rootNode.Nodes.Add($"item_count: {selectedItem.item_count}");
            }

            rootNode.Nodes.Add($"premium_grade_id: {selectedItem.premium_grade_id}");
            rootNode.Nodes.Add($"active_take: {selectedItem.active_take}");
            rootNode.Nodes.Add($"on_air: {selectedItem.on_air}");
            rootNode.Nodes.Add($"tool_tip: {AADB.GetTranslationByID(selectedItem.id, "schedule_items", "tool_tip", selectedItem.tool_tip)}");
            rootNode.Nodes.Add($"show_wherever: {selectedItem.show_wherever}");
            rootNode.Nodes.Add($"show_whenever: {selectedItem.show_whenever}");
            rootNode.Nodes.Add($"icon_path: {selectedItem.icon_path}");
            rootNode.Nodes.Add($"enable_key_string: {selectedItem.enable_key_string}");
            rootNode.Nodes.Add($"disable_key_string: {selectedItem.disable_key_string}");
            rootNode.Nodes.Add($"label_key_string: {selectedItem.label_key_string}");
            rootNode.ExpandAll();

            ShowSelectedData("schedule_items", "(id = " + selectedItem.id.ToString() + ")", "id ASC");
        }

        private void LbSchedulesGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (lbSchedulesGame.SelectedItem as GameGameSchedules);
            if (selectedItem == null)
                return;
            tvSchedule.Nodes.Clear();
            var rootNode = tvSchedule.Nodes.Add(selectedItem.ToString());

            rootNode.Nodes.Add($"Day of the Week: {selectedItem.day_of_week_id}");

            rootNode.Nodes.Add($"Starts: {selectedItem.start_time:00}:{selectedItem.start_time_min:00}");
            rootNode.Nodes.Add($"Ends: {selectedItem.end_time:00}:{selectedItem.end_time_min:00}");

            if ((selectedItem.st_year > 0) && (selectedItem.st_month > 0) && (selectedItem.st_day > 0))
            {
                var startTime = new DateTime(
                    (int)selectedItem.st_year, (int)selectedItem.st_month, (int)selectedItem.st_day,
                    (int)selectedItem.st_hour, (int)selectedItem.st_min, (int)selectedItem.st_min);
                rootNode.Nodes.Add($"IRL Starts: {startTime}");
            }

            if ((selectedItem.ed_year > 0) && (selectedItem.ed_month > 0) && (selectedItem.ed_day > 0))
            {
                var startTime = new DateTime(
                    (int)selectedItem.ed_year, (int)selectedItem.ed_month, (int)selectedItem.ed_day,
                    (int)selectedItem.ed_hour, (int)selectedItem.ed_min, (int)selectedItem.ed_min);
                rootNode.Nodes.Add($"IRL Ends: {startTime}");
            }

            var quests = AADB.DB_ScheduleQuest.Values.Where(x => x.game_schedule_id == selectedItem.id);
            if (quests.Any())
            {
                var questNode = tvSchedule.Nodes.Add("Quests");
                foreach (var gameScheduleQuest in quests)
                    AddCustomPropertyNode("quest_id", gameScheduleQuest.quest_id.ToString(), false, questNode);
            }

            var doodads = AADB.DB_ScheduleDoodads.Values.Where(x => x.game_schedule_id == selectedItem.id);
            if (doodads.Any())
            {
                var doodadNode = tvSchedule.Nodes.Add("Doodads");
                foreach (var gameScheduleQuest in doodads)
                    AddCustomPropertyNode("doodad_id", gameScheduleQuest.doodad_id.ToString(), false, doodadNode);
            }

            var spawners = AADB.DB_ScheduleSpawners.Values.Where(x => x.game_schedule_id == selectedItem.id);
            if (spawners.Any())
            {
                var spawnersNode = tvSchedule.Nodes.Add("Spawners");
                foreach (var gameScheduleQuest in spawners)
                    AddCustomPropertyNode("spawner_id", gameScheduleQuest.spawner_id.ToString(), false, spawnersNode);
            }

            tvSchedule.ExpandAll();
            ShowSelectedData("game_schedules", "(id = " + selectedItem.id.ToString() + ")", "id ASC");
        }

        private void TvSchedule_DoubleClick(object sender, EventArgs e)
        {
            ProcessNodeInfoDoubleClick(tvSchedule.SelectedNode);
        }

        private void LbTowerDefs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (lbTowerDefs.SelectedItem as GameTowerDefs);
            if (selectedItem == null)
                return;
            tvSchedule.Nodes.Clear();
            var hasName = !string.IsNullOrWhiteSpace(selectedItem.nameLocalized);
            var displayName = hasName ? selectedItem.nameLocalized : selectedItem.title_msgLocalized;
            var rootNode = tvSchedule.Nodes.Add($"{selectedItem.id} - {displayName}");
            rootNode.ImageIndex = 3;

            if (hasName)
                rootNode.Nodes.Add($"Title: {selectedItem.title_msgLocalized}");

            if (selectedItem.tod != 0f)
            {
                var startHours = (int)Math.Floor(selectedItem.tod);
                var startMinutes = (int)Math.Floor((selectedItem.tod % 1) * 60f);

                rootNode.Nodes.Add($"Starts at {startHours:00}:{startMinutes:00} game time");
            }
            else
            {
                var triggerCount = 0;
                var conflictZoneWarTriggers = AADB.DB_ConflictZones.Values.Where(x => x.war_tower_def_id == selectedItem.id);
                if (conflictZoneWarTriggers.Any())
                {
                    foreach (var conflictZoneWarTrigger in conflictZoneWarTriggers)
                    {
                        if (AADB.DB_Zone_Groups.TryGetValue(conflictZoneWarTrigger.zone_group_id, out var zoneGroup))
                            rootNode.Nodes.Add($"Starts when WAR in {zoneGroup.display_textLocalized} ({zoneGroup.id})");
                        else
                            rootNode.Nodes.Add($"Starts when WAR in zone_group {conflictZoneWarTrigger.zone_group_id})");
                        triggerCount++;
                    }
                }
                var conflictZonePeaceTriggers = AADB.DB_ConflictZones.Values.Where(x => x.peace_tower_def_id == selectedItem.id);
                if (conflictZonePeaceTriggers.Any())
                {
                    foreach (var conflictZonePeaceTrigger in conflictZonePeaceTriggers)
                    {
                        if (AADB.DB_Zone_Groups.TryGetValue(conflictZonePeaceTrigger.zone_group_id, out var zoneGroup))
                            rootNode.Nodes.Add($"Starts when PEACE in {zoneGroup.display_textLocalized} ({zoneGroup.id})");
                        else
                            rootNode.Nodes.Add($"Starts when PEACE in zone_group {conflictZonePeaceTrigger.zone_group_id})");
                        triggerCount++;
                    }
                }

                if (triggerCount <= 0)
                    rootNode.Nodes.Add($"Starts by manual trigger");
            }

            rootNode.Nodes.Add($"First wave after: {selectedItem.first_wave_after} - {MSToString((long)Math.Round(selectedItem.first_wave_after*1000f))}");
            rootNode.Nodes.Add($"Force end after: {selectedItem.force_end_time} - {MSToString((long)Math.Round(selectedItem.force_end_time * 1000f))}");
            rootNode.Nodes.Add($"ToD Day Interval: {selectedItem.tod_day_interval}");
            AddCustomPropertyNode("target_npc_spawner_id", selectedItem.target_npc_spawner_id.ToString(), false, rootNode);
            AddCustomPropertyNode("kill_npc_id", selectedItem.kill_npc_id.ToString(), false, rootNode);
            AddCustomPropertyNode("kill_npc_count", selectedItem.kill_npc_count.ToString(), false, rootNode);

            rootNode.Nodes.Add($"Start Message: {selectedItem.start_msgLocalized}");
            rootNode.Nodes.Add($"End Message: {selectedItem.end_msgLocalized}");

            var steps = AADB.DB_TowerDefProgs.Values.Where(x => x.tower_def_id == selectedItem.id)?.OrderBy(x => x.id);
            if (steps.Any())
            {
                foreach (var step in steps)
                {
                    var stepNode = tvSchedule.Nodes.Add($"{step.id} - {step.msgLocalized}");
                    stepNode.ImageIndex = 2;
                    // AddCustomPropertyNode("cond_to_next_time", ((long)Math.Round(step.cond_to_next_time * 1000f)).ToString(), true, stepNode);
                    stepNode.Nodes.Add($"cond_to_next_time: {step.cond_to_next_time} - {MSToString((long)Math.Round(step.cond_to_next_time * 1000f))}");
                    stepNode.Nodes.Add($"cond_comp_by_and: {step.cond_comp_by_and}");

                    var spawnsNode = stepNode.Nodes.Add("Spawns");
                    var spawnTargets = AADB.DB_TowerDefProgSpawnTargets.Values.Where(x => x.tower_def_prog_id == step.id);
                    if (spawnTargets.Any())
                    {
                        foreach (var spawnTarget in spawnTargets)
                        {
                            TreeNode spawnNode = null;
                            if (spawnTarget.spawn_target_type == "DoodadAlmighty")
                                spawnNode = AddCustomPropertyNode("doodad_id", spawnTarget.spawn_target_id.ToString(), false, spawnsNode);
                            else if (spawnTarget.spawn_target_type == "NpcSpawner")
                                spawnNode = AddCustomPropertyNode("npc_spawner_id", spawnTarget.spawn_target_id.ToString(), false, spawnsNode);
                            else
                                spawnNode = spawnsNode.Nodes.Add($"{spawnTarget.spawn_target_type} - {spawnTarget.spawn_target_id}");

                            if (spawnTarget.despawn_on_next_step)
                                spawnNode.Nodes.Add("Despawns on next step").ImageIndex = 3;
                        }
                    }

                    var killsNode = stepNode.Nodes.Add("Kills");
                    var killTargets = AADB.DB_TowerDefProgKillTargets.Values.Where(x => x.tower_def_prog_id == step.id);
                    if (killTargets.Any())
                    {
                        foreach (var killTarget in killTargets)
                        {
                            TreeNode spawnNode = null;
                            if (killTarget.kill_target_type == "DoodadAlmighty")
                                spawnNode = AddCustomPropertyNode("doodad_id", killTarget.kill_target_id.ToString(), false, killsNode);
                            else if (killTarget.kill_target_type == "Npc")
                                spawnNode = AddCustomPropertyNode("npc_id", killTarget.kill_target_id.ToString(), false, killsNode);
                            else
                                spawnNode = killsNode.Nodes.Add($"{killTarget.kill_target_type} - {killTarget.kill_target_id}");

                            if (killTarget.kill_count != 1)
                                spawnNode.Text += $" x {killTarget.kill_count}";
                        }
                    }
                }
            }

            rootNode.ExpandAll();

            ShowSelectedData("tower_defs", "(id = " + selectedItem.id.ToString() + ")", "id ASC");
        }
    }
}
