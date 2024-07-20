using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBDefs;
using AAEmu.DBViewer.enums;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer;

public partial class MainForm
{
    private void LoadSkills()
    {
        List<string> columnNames = null;
        bool readWebDesc = false;

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        using (var connection = SQLite.CreateConnection())
        {
            // Skills base
            string sql = "SELECT * FROM skills ORDER BY id ASC";
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Skills.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {

                    if (columnNames == null)
                    {
                        columnNames = reader.GetColumnNames();
                        if (columnNames.IndexOf("web_desc") >= 0)
                            readWebDesc = true;
                    }

                    while (reader.Read())
                    {
                        var t = new GameSkills();
                        t.id = GetInt64(reader, "id");
                        t.name = GetString(reader, "name");
                        t.desc = GetString(reader, "desc");
                        // Added a check for this
                        if (readWebDesc)
                            t.web_desc = GetString(reader, "web_desc");
                        else
                            t.web_desc = string.Empty;
                        t.cost = GetInt64(reader, "cost");
                        t.icon_id = GetInt64(reader, "icon_id");
                        t.show = GetBool(reader, "show");
                        t.cooldown_time = GetInt64(reader, "cooldown_time");
                        t.casting_time = GetInt64(reader, "casting_time");
                        t.ignore_global_cooldown = GetBool(reader, "ignore_global_cooldown");
                        t.effect_delay = GetInt64(reader, "effect_delay");
                        t.ability_id = GetInt64(reader, "ability_id");
                        t.mana_cost = GetInt64(reader, "mana_cost");
                        t.timing_id = GetInt64(reader, "timing_id");
                        t.consume_lp = GetInt64(reader, "consume_lp");
                        t.default_gcd = GetBool(reader, "default_gcd");
                        ;
                        t.custom_gcd = GetInt64(reader, "custom_gcd");
                        t.first_reagent_only = GetBool(reader, "first_reagent_only");
                        t.plot_id = GetInt64(reader, "plot_id");

                        t.or_unit_reqs = GetBool(reader, "or_unit_reqs");

                        t.nameLocalized = AADB.GetTranslationByID(t.id, "skills", "name", t.name);
                        t.descriptionLocalized = AADB.GetTranslationByID(t.id, "skills", "desc", t.desc);
                        if (readWebDesc)
                            t.webDescriptionLocalized =
                                AADB.GetTranslationByID(t.id, "skills", "web_desc", t.web_desc);
                        else
                            t.webDescriptionLocalized = string.Empty;

                        t.SearchString = t.name + " " + t.desc + " " + t.nameLocalized + " " +
                                         t.descriptionLocalized;
                        t.SearchString = t.SearchString.ToLower();

                        AADB.DB_Skills.Add(t.id, t);
                    }

                }
            }

            // Skill Effects
            sql = "SELECT * FROM skill_effects ORDER BY id ASC";
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Skill_Effects.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    while (reader.Read())
                    {
                        var t = new GameSkillEffects();
                        t.id = GetInt64(reader, "id");
                        t.skill_id = GetInt64(reader, "skill_id");
                        t.effect_id = GetInt64(reader, "effect_id");
                        t.weight = GetInt64(reader, "weight");
                        t.start_level = GetInt64(reader, "start_level");
                        t.end_level = GetInt64(reader, "end_level");
                        t.friendly = GetBool(reader, "friendly");
                        t.non_friendly = GetBool(reader, "non_friendly");
                        t.target_buff_tag_id = GetInt64(reader, "target_buff_tag_id");
                        t.target_nobuff_tag_id = GetInt64(reader, "target_nobuff_tag_id");
                        t.source_buff_tag_id = GetInt64(reader, "source_buff_tag_id");
                        t.source_nobuff_tag_id = GetInt64(reader, "source_nobuff_tag_id");
                        t.chance = GetInt64(reader, "chance");
                        t.front = GetBool(reader, "front");
                        t.back = GetBool(reader, "back");
                        t.target_npc_tag_id = GetInt64(reader, "target_npc_tag_id");
                        t.application_method_id = GetInt64(reader, "application_method_id");
                        t.synergy_text = GetBool(reader, "synergy_text");
                        t.consume_source_item = GetBool(reader, "consume_source_item");
                        t.consume_item_id = GetInt64(reader, "consume_item_id");
                        t.consume_item_count = GetInt64(reader, "consume_item_count");
                        t.always_hit = GetBool(reader, "always_hit");
                        t.item_set_id = GetInt64(reader, "item_set_id");
                        t.interaction_success_hit = GetBool(reader, "interaction_success_hit");
                        AADB.DB_Skill_Effects.Add(t.id, t);
                    }

                }
            }

            // Effects
            sql = "SELECT * FROM effects ORDER BY id ASC";
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Effects.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    while (reader.Read())
                    {
                        var t = new GameEffects();
                        t.id = GetInt64(reader, "id");
                        t.actual_id = GetInt64(reader, "actual_id");
                        t.actual_type = GetString(reader, "actual_type");
                        AADB.DB_Effects.Add(t.id, t);
                    }

                }
            }

            // Mount Skills
            sql = "SELECT * FROM mount_skills ORDER BY id ASC";
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Mount_Skills.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    columnNames = null;
                    var readNames = false;
                    while (reader.Read())
                    {
                        // name field is not present after in 3.0.3.0
                        if (columnNames == null)
                        {
                            columnNames = reader.GetColumnNames();
                            readNames = (columnNames.IndexOf("name") >= 0);
                        }

                        var t = new GameMountSkill();
                        t.id = GetInt64(reader, "id");
                        if (readNames)
                            t.name = GetString(reader, "name");
                        t.skill_id = GetInt64(reader, "skill_id");
                        AADB.DB_Mount_Skills.Add(t.id, t);
                    }
                }
            }

            // Slave Mount Skills
            sql = "SELECT * FROM slave_mount_skills ORDER BY slave_id ASC";
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Slave_Mount_Skills.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    columnNames = null;
                    var indx = 1L;
                    var readId = false;
                    while (reader.Read())
                    {
                        // id field is not present after in 3.0.3.0
                        if (columnNames == null)
                        {
                            columnNames = reader.GetColumnNames();
                            readId = (columnNames.IndexOf("id") >= 0);
                        }

                        var t = new GameSlaveMountSkill();
                        t.id = readId ? GetInt64(reader, "id") : indx;
                        t.slave_id = GetInt64(reader, "slave_id");
                        t.mount_skill_id = GetInt64(reader, "mount_skill_id");
                        AADB.DB_Slave_Mount_Skills.Add(t.id, t);
                        indx++;
                    }
                }
            }

            // NpSkills
            if (allTableNames.Contains("np_skills"))
            {
                sql = "SELECT * FROM np_skills ORDER BY id ASC";
                using (var command = connection.CreateCommand())
                {
                    AADB.DB_NpSkills.Clear();

                    command.CommandText = sql;
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameNpSkills();
                            t.id = GetInt64(reader, "id");
                            t.owner_id = GetInt64(reader, "owner_id");
                            t.owner_type = GetString(reader, "owner_type"); // They are actually all "Npc"
                            t.skill_id = GetInt64(reader, "skill_id");
                            t.skill_use_condition_id = (SkillUseConditionKind)GetInt64(reader, "skill_use_condition_id");
                            t.skill_use_param1 = GetFloat(reader, "skill_use_param1");
                            t.skill_use_param2 = GetFloat(reader, "skill_use_param2");
                            AADB.DB_NpSkills.Add(t.id, t);
                        }

                    }
                }
            }
            else
            {
                AADB.DB_NpSkills.Clear();
            }
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void LoadSkillReagents()
    {
        string sql = "SELECT * FROM skill_reagents ORDER BY id ASC";

        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Skill_Reagents.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {
                        GameSkillItems t = new GameSkillItems();
                        t.id = GetInt64(reader, "id");
                        t.skill_id = GetInt64(reader, "skill_id");
                        t.item_id = GetInt64(reader, "item_id");
                        t.amount = GetInt64(reader, "amount");

                        AADB.DB_Skill_Reagents.Add(t.id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

    }

    private void LoadSkillProducts()
    {
        string sql = "SELECT * FROM skill_products ORDER BY id ASC";

        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Skill_Products.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {
                        GameSkillItems t = new GameSkillItems();
                        t.id = GetInt64(reader, "id");
                        t.skill_id = GetInt64(reader, "skill_id");
                        t.item_id = GetInt64(reader, "item_id");
                        t.amount = GetInt64(reader, "amount");

                        AADB.DB_Skill_Products.Add(t.id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

    }

    private void LoadBuffs()
    {
        var sql = "SELECT * FROM buffs ORDER BY id ASC";
        var triggerSql = "SELECT * FROM buff_triggers ORDER BY id ASC";
        var modifiersSql = "SELECT * FROM buff_modifiers";
        var npcInitialSql = "SELECT * FROM npc_initial_buffs ORDER BY npc_id ASC";
        var passiveSql = "SELECT * FROM passive_buffs ORDER BY id ASC";
        var slavePassiveSql = "SELECT * FROM slave_passive_buffs ORDER BY owner_id ASC";
        var slaveInitialSql = "SELECT * FROM slave_initial_buffs ORDER BY slave_id ASC";

        using (var connection = SQLite.CreateConnection())
        {
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            using (var command = connection.CreateCommand())
            {
                AADB.DB_Buffs.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    // Get a list of all fields
                    var cols = reader.GetColumnNames();
                    // Remove those that we'll specifically load
                    cols.Remove("id");
                    cols.Remove("name");
                    cols.Remove("desc");
                    cols.Remove("icon_id");
                    cols.Remove("duration");
                    cols.Remove("name_tr");
                    cols.Remove("desc_tr");

                    while (reader.Read())
                    {

                        GameBuff t = new GameBuff();
                        t.id = GetInt64(reader, "id");
                        t.name = GetString(reader, "name");
                        t.desc = GetString(reader, "desc");
                        t.icon_id = GetInt64(reader, "icon_id");
                        t.duration = GetInt64(reader, "duration");

                        t.nameLocalized = AADB.GetTranslationByID(t.id, "buffs", "name", t.name);
                        t.descLocalized = AADB.GetTranslationByID(t.id, "buffs", "desc", t.desc);

                        t.SearchString = t.name + " " + t.nameLocalized + " " + t.desc + " " + t.descLocalized;
                        t.SearchString = t.SearchString.ToLower();

                        // Read remaining data
                        foreach (var c in cols)
                        {
                            if (!reader.IsDBNull(c))
                            {
                                var v = reader.GetString(c, string.Empty);
                                var isnumber = double.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture,
                                    out var dVal);
                                if (isnumber)
                                {
                                    if (dVal != 0f)
                                        t._others.Add(c, v);
                                }
                                else if ((v != string.Empty) && (v != "0") && (v != "f") && (v != "NULL") &&
                                         (v != "--- :null"))
                                {
                                    t._others.Add(c, v);
                                }
                            }
                        }

                        AADB.DB_Buffs.Add(t.id, t);
                    }
                }
            }

            using (var command = connection.CreateCommand())
            {
                AADB.DB_BuffTriggers.Clear();
                command.CommandText = triggerSql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {

                    while (reader.Read())
                    {
                        var t = new GameBuffTrigger();
                        t.id = GetInt64(reader, "id");
                        t.buff_id = GetInt64(reader, "buff_id");
                        t.event_id = GetInt64(reader, "event_id");
                        t.effect_id = GetInt64(reader, "effect_id");

                        AADB.DB_BuffTriggers.Add(t.id, t);
                    }
                }
            }

            using (var command = connection.CreateCommand())
            {
                AADB.DB_BuffModifiers.Clear();
                command.CommandText = modifiersSql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    var useDbId = false;
                    var columnNames = reader.GetColumnNames();
                    if (columnNames.IndexOf("id") >= 0)
                        useDbId = true;
                    var dbId = 0;

                    while (reader.Read())
                    {
                        dbId++;
                        var t = new GameBuffModifier();
                        t.id = useDbId ? GetInt64(reader, "id") : dbId;
                        t.owner_id = GetInt64(reader, "owner_id");
                        t.owner_type = GetString(reader, "owner_type");
                        t.buff_id = GetInt64(reader, "buff_id");
                        t.tag_id = GetInt64(reader, "tag_id");
                        t.buff_attribute_id = (BuffAttribute)GetInt64(reader, "buff_attribute_id");
                        t.unit_modifier_type_id = GetInt64(reader, "unit_modifier_type_id");
                        t.value = GetInt64(reader, "value");
                        t.synergy = GetBool(reader, "synergy");

                        AADB.DB_BuffModifiers.Add(t.id, t);
                    }
                }
            }

            using (var command = connection.CreateCommand())
            {
                AADB.DB_NpcInitialBuffs.Clear();
                command.CommandText = npcInitialSql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    List<string> columnNames = null;
                    var indx = 1L;
                    var readId = false;
                    while (reader.Read())
                    {
                        // id field is not present after in 3.0.3.0
                        if (columnNames == null)
                        {
                            columnNames = reader.GetColumnNames();
                            readId = (columnNames.IndexOf("id") >= 0);
                        }

                        var t = new GameNpcInitialBuffs();
                        t.id = readId ? GetInt64(reader, "id") : indx;
                        t.npc_id = GetInt64(reader, "npc_id");
                        t.buff_id = GetInt64(reader, "buff_id");

                        AADB.DB_NpcInitialBuffs.Add(t.id, t);
                        indx++;
                    }
                }
            }

            using (var command = connection.CreateCommand())
            {
                AADB.DB_Passive_Buffs.Clear();
                command.CommandText = passiveSql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {

                    while (reader.Read())
                    {
                        var t = new GamePassiveBuff();
                        t.id = GetInt64(reader, "id");
                        t.ability_id = GetInt64(reader, "ability_id");
                        t.level = GetInt64(reader, "level");
                        t.buff_id = GetInt64(reader, "buff_id");
                        t.req_points = GetInt64(reader, "req_points");
                        t.active = GetBool(reader, "active");

                        AADB.DB_Passive_Buffs.Add(t.id, t);
                    }
                }
            }

            using (var command = connection.CreateCommand())
            {
                AADB.DB_Slave_Passive_Buffs.Clear();
                command.CommandText = slavePassiveSql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
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

                        var t = new GameSlavePassiveBuff();
                        t.id = readId ? GetInt64(reader, "id") : indx;
                        t.owner_id = GetInt64(reader, "owner_id");
                        t.owner_type = GetString(reader, "owner_type");
                        t.passive_buff_id = GetInt64(reader, "passive_buff_id");

                        AADB.DB_Slave_Passive_Buffs.Add(t.id, t);
                        indx++;
                    }
                }
            }

            using (var command = connection.CreateCommand())
            {
                AADB.DB_Slave_Initial_Buffs.Clear();
                command.CommandText = slaveInitialSql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    var indx = 1L;
                    while (reader.Read())
                    {
                        var t = new GameSlaveInitialBuff();
                        //t.id = GetInt64(reader, "id"); no in 3030
                        t.id = indx;
                        t.slave_id = GetInt64(reader, "slave_id");
                        t.buff_id = GetInt64(reader, "buff_id");

                        AADB.DB_Slave_Initial_Buffs.Add(t.id, t);
                        indx++;
                    }
                }
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

    }

    private void LoadPlots()
    {
        // base plots
        string sql = "SELECT * FROM plots";

        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Plots.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {

                        var t = new GamePlot();
                        t.id = GetInt64(reader, "id");
                        t.name = GetString(reader, "name");
                        t.target_type_id = GetInt64(reader, "target_type_id");

                        AADB.DB_Plots.Add(t.id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // events
        sql = "SELECT * FROM plot_events";

        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Plot_Events.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {

                        var t = new GamePlotEvent();
                        t.id = GetInt64(reader, "id");
                        t.plot_id = GetInt64(reader, "plot_id");
                        t.postion = GetInt64(reader, "position");
                        t.name = GetString(reader, "name");
                        t.source_update_method_id = GetInt64(reader, "source_update_method_id");
                        t.target_update_method_id = GetInt64(reader, "target_update_method_id");
                        t.target_update_method_param1 = GetInt64(reader, "target_update_method_param1");
                        t.target_update_method_param2 = GetInt64(reader, "target_update_method_param2");
                        t.target_update_method_param3 = GetInt64(reader, "target_update_method_param3");
                        t.target_update_method_param4 = GetInt64(reader, "target_update_method_param4");
                        t.target_update_method_param5 = GetInt64(reader, "target_update_method_param5");
                        t.target_update_method_param6 = GetInt64(reader, "target_update_method_param6");
                        t.target_update_method_param7 = GetInt64(reader, "target_update_method_param7");
                        t.target_update_method_param8 = GetInt64(reader, "target_update_method_param8");
                        t.target_update_method_param9 = GetInt64(reader, "target_update_method_param9");
                        t.tickets = GetInt64(reader, "tickets");
                        t.aeo_diminishing = GetBool(reader, "aoe_diminishing");

                        AADB.DB_Plot_Events.Add(t.id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // next events
        sql = "SELECT * FROM plot_next_events";
        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Plot_Next_Events.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {

                        var t = new GamePlotNextEvent();
                        t.id = GetInt64(reader, "id");
                        t.event_id = GetInt64(reader, "event_id");
                        t.postion = GetInt64(reader, "position");
                        t.next_event_id = GetInt64(reader, "next_event_id");
                        t.delay = GetInt64(reader, "delay");
                        t.speed = GetInt64(reader, "speed");

                        AADB.DB_Plot_Next_Events.Add(t.id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // plot events condition
        sql = "SELECT * FROM plot_event_conditions";
        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Plot_Event_Conditions.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {

                        var t = new GamePlotEventCondition();
                        t.id = GetInt64(reader, "id");
                        t.event_id = GetInt64(reader, "event_id");
                        t.postion = GetInt64(reader, "position");
                        t.condition_id = GetInt64(reader, "condition_id");
                        t.source_id = GetInt64(reader, "source_id");
                        t.target_id = GetInt64(reader, "target_id");
                        t.notify_failure = GetBool(reader, "notify_failure");

                        AADB.DB_Plot_Event_Conditions.Add(t.id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // plot condition
        sql = "SELECT * FROM plot_conditions";
        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Plot_Conditions.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {
                        var t = new GamePlotCondition();
                        t.id = GetInt64(reader, "id");
                        t.not_condition = GetBool(reader, "not_condition");
                        t.kind_id = GetInt64(reader, "kind_id");
                        t.param1 = GetInt64(reader, "param1");
                        t.param2 = GetInt64(reader, "param2");
                        t.param3 = GetInt64(reader, "param3");

                        AADB.DB_Plot_Conditions.Add(t.id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

        // plot effects
        sql = "SELECT * FROM plot_effects";
        using (var connection = SQLite.CreateConnection())
        {
            using (var command = connection.CreateCommand())
            {
                AADB.DB_Plot_Effects.Clear();

                command.CommandText = sql;
                command.Prepare();
                using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                {
                    Application.UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;

                    while (reader.Read())
                    {
                        var t = new GamePlotEffect();
                        t.id = GetInt64(reader, "id");
                        t.event_id = GetInt64(reader, "event_id");
                        t.position = GetInt64(reader, "position");
                        t.source_id = GetInt64(reader, "source_id");
                        t.target_id = GetInt64(reader, "target_id");
                        t.actual_id = GetInt64(reader, "actual_id");
                        t.actual_type = GetString(reader, "actual_type");

                        AADB.DB_Plot_Effects.Add(t.id, t);
                    }

                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                }
            }
        }

    }

    private static string ConditionTypeName(long id)
    {
        switch (id)
        {
            case 1: return "Level (1)";
            case 2: return "Relation (2)";
            case 3: return "Direction (3)";
            case 4: return "Unk4 (4)";
            case 5: return "BuffTag (5)";
            case 6: return "WeaponEquipStatus (6)";
            case 7: return "Chance (7)";
            case 8: return "Dead (8)";
            case 9: return "CombatDiceResult (9)";
            case 10: return "InstrumentType (10)";
            case 11: return "Range (11)";
            case 12: return "Variable (12)";
            case 13: return "UnitAttrib (13)";
            case 14: return "Actability (14)";
            case 15: return "Stealth (15)";
            case 16: return "Visible (16)";
            case 17: return "ABLevel (17)";
            default: return id.ToString();
        }
    }

    private void AddPlotEventNode(TreeNode parent, GamePlotEvent child, int depth = 0, string nodeNamePrefix = "")
    {
        depth++;
        if (depth > 16)
        {
            var overflowNode = new TreeNode("Too many recursions !");
            overflowNode.ImageIndex = 4;
            overflowNode.SelectedImageIndex = 4;
            overflowNode.Tag = 0;
            parent.Nodes.Add(overflowNode);
            return;
        }

        var nodeName = nodeNamePrefix + child.id.ToString() + " - " + child.name;
        if (child.tickets > 1)
            nodeName = child.tickets.ToString() + " x " + nodeName;
        var eventNode = new TreeNode(nodeName);
        eventNode.ImageIndex = 1;
        eventNode.SelectedImageIndex = 1;
        eventNode.Tag = child.id;

        parent.Nodes.Add(eventNode);

        // Does it have conditions ?
        var plotEventConditions = AADB.DB_Plot_Event_Conditions.Where(pec => pec.Value.event_id == child.id);
        foreach (var plotEventCondition in plotEventConditions)
        {
            var eventConditionName = $"Event Condition ({plotEventCondition.Value.condition_id})";
            var eventConditionNode = new TreeNode(eventConditionName);
            eventConditionNode.ImageIndex = 3;
            eventConditionNode.SelectedImageIndex = 3;
            eventConditionNode.Tag = 0;
            eventNode.Nodes.Add(eventConditionNode);
            eventConditionNode.Nodes.Add("Source: " + PlotUpdateMethod(plotEventCondition.Value.source_id));
            eventConditionNode.Nodes.Add("Target: " + PlotUpdateMethod(plotEventCondition.Value.target_id));
            eventConditionNode.Nodes.Add("Notify Failure: " + plotEventCondition.Value.notify_failure);
            if (AADB.DB_Plot_Conditions.TryGetValue(plotEventCondition.Value.condition_id, out var condition))
            {
                eventConditionNode.Text += (condition.not_condition ? " NOT " : " ") + ConditionTypeName(condition.kind_id);
                // eventConditionNode.Nodes.Add("Condition: " + (condition.not_condition ? "NOT " : "") + ConditionTypeName(condition.kind_id));
                if (condition.param1 != 0)
                    eventConditionNode.Nodes.Add("Param1: " + condition.param1);
                if (condition.param2 != 0)
                    eventConditionNode.Nodes.Add("Param2: " + condition.param2);
                if (condition.param3 != 0)
                    eventConditionNode.Nodes.Add("Param3: " + condition.param3);
            }
        }

        // Plot Effects
        var plotEventEffects = AADB.DB_Plot_Effects.Where(pec => pec.Value.event_id == child.id).OrderBy(pec => pec.Value.position);
        foreach (var plotEffect in plotEventEffects)
        {
            var eventConditionName = $"Plot Effect ({plotEffect.Value.id})";
            var effectNode = new TreeNode(eventConditionName);
            effectNode.ImageIndex = 2;
            effectNode.SelectedImageIndex = 2;
            effectNode.Tag = 0;
            eventNode.Nodes.Add(effectNode);
            effectNode.Nodes.Add("Pos: " + plotEffect.Value.position);
            effectNode.Nodes.Add("Source: " + PlotUpdateMethod(plotEffect.Value.source_id));
            effectNode.Nodes.Add("Target: " + PlotUpdateMethod(plotEffect.Value.target_id));
            // var actualEffectNode = effectNode.Nodes.Add("Actual Effect: " + plotEffect.Value.actual_type + " (" +plotEffect.Value.actual_id + ")");

            CreatePlotEffectNode(plotEffect.Value.actual_type, plotEffect.Value.actual_id, effectNode, true);
        }

        var nextEvents = AADB.DB_Plot_Next_Events.Where(nextEvent => nextEvent.Value.event_id == child.id).OrderBy(p => p.Value.postion);
        foreach (var n in nextEvents)
        {
            if (n.Value.next_event_id == n.Value.event_id)
            {
                var rNode = eventNode.Nodes.Add("Repeats itself with Delay: " + n.Value.delay + ", Speed: " + n.Value.speed);
                rNode.ImageIndex = 3;
                rNode.SelectedImageIndex = rNode.ImageIndex;
            }
            else
            if (AADB.DB_Plot_Events.TryGetValue(n.Value.next_event_id, out var next))
            {
                //AddPlotEventNode(eventNode, next, depth, "Next Plot Event: ");

                var nextNode = new TreeNode("Next Event Node: " + next.id.ToString() + " - " + next.name);
                nextNode.Tag = next.id;
                nextNode.ImageIndex = 2;
                nextNode.SelectedImageIndex = nextNode.ImageIndex;
                eventNode.Nodes.Add(nextNode);
            }
            else
            {
                var errorNode = new TreeNode("Unknown Next Event: " + n.Value.next_event_id.ToString());
                errorNode.ImageIndex = 4;
                errorNode.SelectedImageIndex = 4;
                errorNode.Tag = 0;
                eventNode.Nodes.Add(errorNode);
            }

        }
    }

    private void ShowSkillPlotTree(GameSkills skill)
    {
        tvSkill.Nodes.Clear();
        var imgId = ilIcons.Images.IndexOfKey(skill.icon_id.ToString());
        var rootNode = new TreeNode(skill.nameLocalized + $" ( {skill.id} )", imgId, imgId);
        rootNode.Tag = 0;
        tvSkill.Nodes.Add(rootNode);

        var requires = GetSkillRequirements(skill.id);
        var reqNode = AddUnitRequirementNode(requires, skill.or_unit_reqs, tvSkill.Nodes);

        var skillsProperties = GetCustomTableValues("skills", "id", skill.id.ToString());
        foreach (var skillsProperty in skillsProperties)
        foreach (var skillsPropertyValue in skillsProperty)
        {
            if ((skillsPropertyValue.Key == "name") ||
                (skillsPropertyValue.Key == "desc") ||
                (skillsPropertyValue.Key == "web_desc") ||
                (skillsPropertyValue.Key == "name_tr") ||
                (skillsPropertyValue.Key == "desc_tr") ||
                (skillsPropertyValue.Key == "web_desc_tr")
               )
                continue; // ignore name/description fields

            var thisNode = AddCustomPropertyNode(skillsPropertyValue.Key, skillsPropertyValue.Value, true, rootNode);
            if (thisNode == null)
                continue;

            if (thisNode.ImageIndex <= 0) // override default blank icon with blue !
            {
                thisNode.ImageIndex = 4;
                thisNode.SelectedImageIndex = 4;
            }
        }

        /*
        if (skill.casting_time > 0)
            AddCustomPropertyNode("casting_time", skill.casting_time.ToString(), true, rootNode);

        if (skill.cooldown_time > 0)
            AddCustomPropertyNode("cooldown_time", skill.cooldown_time.ToString(), true, rootNode);

        if (skill.effect_delay > 0)
            AddCustomPropertyNode("effect_delay", skill.effect_delay.ToString(), true, rootNode);

        if (skill.ability_id > 0)
            AddCustomPropertyNode("ability_id", ((AbilityType)skill.ability_id).ToString(), true, rootNode);
        */

        var skillEffectsList = from se in AADB.DB_Skill_Effects
            where se.Value.skill_id == skill.id
            select se.Value;

        TreeNode effectsRoot = null;

        if (skillEffectsList != null)
        {
            long totalWeight = 0;
            foreach (var skillEffect in skillEffectsList)
            {
                totalWeight += skillEffect.weight;
            }
            foreach (var skillEffect in skillEffectsList)
            {
                if (effectsRoot == null)
                {
                    effectsRoot = tvSkill.Nodes.Add("Effects");
                    effectsRoot.ImageIndex = 2;
                    effectsRoot.SelectedImageIndex = 2;
                    effectsRoot.Tag = 0;
                }

                CreateSkillEffectNode(skillEffect.effect_id, effectsRoot, skillEffect.weight, totalWeight, true);
            }
        }


        if (AADB.DB_Plots.TryGetValue(skill.plot_id, out var plot))
        {
            var firstPlotNode = new TreeNode(plot.id.ToString() + " - " + plot.name);
            firstPlotNode.ImageIndex = 2;
            firstPlotNode.SelectedImageIndex = 2;
            firstPlotNode.Tag = plot.id;
            tvSkill.Nodes.Add(firstPlotNode);

            var events = AADB.DB_Plot_Events.Where(plotEvent => plotEvent.Value.plot_id == plot.id)
                .OrderBy(plotEvent => plotEvent.Value.postion);
            foreach (var e in events)
            {
                AddPlotEventNode(firstPlotNode, e.Value);
            }
        }

        tvSkill.ExpandAll();
        if (tvSkill.Nodes.Count > 1)
            rootNode.Collapse();
        tvSkill.SelectedNode = rootNode;
    }

    private void CreateSkillEffectNode(long effectId, TreeNode effectsRoot, long thisWeight, long totalWeight, bool hideEmptyProperties)
    {
        var effectTypeText = "???";
        if (AADB.DB_Effects.TryGetValue(effectId, out var effect))
        {
            var rate = (totalWeight > 0 && thisWeight > 0) ? thisWeight * 100f / totalWeight : 100f;
            effectTypeText = effect.actual_type + " ( " + effect.actual_id.ToString() + " )" + (rate < 100 ? $" {rate:F0}%" : "");
        }

        var skillEffectNode = effectsRoot.Nodes.Add(effectTypeText);
        skillEffectNode.ImageIndex = 2;
        skillEffectNode.SelectedImageIndex = 2;
        skillEffectNode.Tag = 0;

        if (effect != null)
        {
            var effectsTableName = FunctionTypeToTableName(effect.actual_type);
            var effectValuesList = GetCustomTableValues(effectsTableName, "id", effect.actual_id.ToString());
            foreach (var effectValues in effectValuesList)
            foreach (var effectValue in effectValues)
            {
                var thisNode = AddCustomPropertyNode(effectValue.Key, effectValue.Value, hideEmptyProperties,
                    skillEffectNode);
                if (thisNode == null)
                    continue;

                if (thisNode.ImageIndex <= 0) // override default blank icon with blue !
                {
                    thisNode.ImageIndex = 4;
                    thisNode.SelectedImageIndex = 4;
                }
            }
        }
    }

    private void CreatePlotEffectNode(string actualType, long id, TreeNode effectsRoot, bool hideEmptyProperties)
    {
        var effectTypeText = actualType + " ( " + id.ToString() + " )";

        var skillEffectNode = effectsRoot.Nodes.Add(effectTypeText);
        skillEffectNode.ImageIndex = 3;
        skillEffectNode.SelectedImageIndex = 3;
        skillEffectNode.Tag = 0;

        var effectsTableName = FunctionTypeToTableName(actualType);
        var effectValuesList = GetCustomTableValues(effectsTableName, "id", id.ToString());
        foreach (var effectValues in effectValuesList)
        foreach (var effectValue in effectValues)
        {
            var thisNode = AddCustomPropertyNode(effectValue.Key, effectValue.Value, hideEmptyProperties,
                skillEffectNode);
            if (thisNode == null)
                continue;

            if (thisNode.ImageIndex <= 0) // override default blank icon with blue !
            {
                thisNode.ImageIndex = 4;
                thisNode.SelectedImageIndex = 4;
            }
        }
    }

    private void ShowDbSkill(long idx)
    {
        if (AADB.DB_Skills.TryGetValue(idx, out var skill))
        {
            lSkillID.Text = idx.ToString();
            lSkillName.Text = skill.nameLocalized;
            lSkillCost.Text = skill.cost.ToString();
            lSkillMana.Text = skill.mana_cost.ToString();
            lSkillLabor.Text = skill.consume_lp.ToString();
            lSkillCooldown.Text = MSToString(skill.cooldown_time);
            if ((skill.default_gcd) && (!skill.ignore_global_cooldown))
            {
                lSkillGCD.Text = "Default";
            }
            else if ((!skill.default_gcd) && (!skill.ignore_global_cooldown))
            {
                lSkillGCD.Text = MSToString(skill.custom_gcd);
            }
            else if ((!skill.default_gcd) && (skill.ignore_global_cooldown))
            {
                lSkillGCD.Text = "Ignored";
            }
            else
            {
                lSkillGCD.Text = "Default";
            }

            // lSkillGCD.Text = skill.ignore_global_cooldown ? "Ignore" : "Normal";
            FormattedTextToRichtEdit(skill.descriptionLocalized, rtSkillDescription);
            IconIdToLabel(skill.icon_id, skillIcon);
            lSkillTags.Text = TagsAsString(idx, AADB.DB_Tagged_Skills);

            ShowSelectedData("skills", "(id = " + idx.ToString() + ")", "id ASC");

            if (skill.first_reagent_only)
            {
                labelSkillReagents.Text = "Requires either of these items to use";
            }
            else
            {
                labelSkillReagents.Text = "Required items to use this skill";
            }

            // Produces
            dgvSkillProducts.Rows.Clear();
            foreach (var p in AADB.DB_Skill_Products)
            {
                if (p.Value.skill_id == idx)
                {
                    var line = dgvSkillProducts.Rows.Add();
                    var row = dgvSkillProducts.Rows[line];
                    row.Cells[0].Value = p.Value.item_id.ToString();
                    if (AADB.DB_Items.TryGetValue(p.Value.item_id, out var item))
                    {
                        row.Cells[1].Value = item.nameLocalized;
                    }
                    else
                    {
                        row.Cells[1].Value = "???";
                    }

                    row.Cells[2].Value = p.Value.amount.ToString();
                }
            }

            // Reagents
            dgvSkillReagents.Rows.Clear();
            foreach (var p in AADB.DB_Skill_Reagents)
            {
                if (p.Value.skill_id == idx)
                {
                    var line = dgvSkillReagents.Rows.Add();
                    var row = dgvSkillReagents.Rows[line];
                    row.Cells[0].Value = p.Value.item_id.ToString();
                    if (AADB.DB_Items.TryGetValue(p.Value.item_id, out var item))
                    {
                        row.Cells[1].Value = item.nameLocalized;
                    }
                    else
                    {
                        row.Cells[1].Value = "???";
                    }

                    row.Cells[2].Value = p.Value.amount.ToString();
                }
            }

            ShowSkillPlotTree(skill);
        }
        else
        {
            lSkillID.Text = idx.ToString();
            lSkillName.Text = "<not found>";
            lSkillCost.Text = "";
            lSkillMana.Text = "";
            lSkillLabor.Text = "";
            lSkillCooldown.Text = "";
            lSkillGCD.Text = "";
            rtSkillDescription.Clear();
            skillIcon.Image = null;
            skillIcon.Text = "???";
            lSkillTags.Text = "???";
            tvSkill.Nodes.Clear();
        }
    }

    private void AddBuffTag(string name)
    {
        if (name.Trim(' ') == string.Empty)
            return;
        var lastTagID = flpBuff.Controls.Count + 10;
        var L = new Label();
        L.Tag = lastTagID;
        // L.BorderStyle = BorderStyle.FixedSingle;
        L.Text = name;
        L.AutoSize = true;
        // L.BackColor = Color.White;
        if ((lastTagID % 2) == 0)
            L.ForeColor = Color.Gray;
        else
            L.ForeColor = Color.LightGray;
        flpBuff.Controls.Add(L);
        L.Cursor = Cursors.Hand;
        L.Click += new EventHandler(LabelToCopy_Click);
    }

    private void ClearBuffTags()
    {
        for (var i = flpBuff.Controls.Count - 1; i >= 0; i--)
        {
            var c = (flpBuff.Controls[i] is Label) ? (flpBuff.Controls[i] as Label) : null;
            if ((c is not null) && (c.Tag != null) && ((int)c.Tag > 0))
            {
                flpBuff.Controls.RemoveAt(i);
            }
        }
    }

    private void ShowDbBuff(long buff_id)
    {
        if (!AADB.DB_Buffs.TryGetValue(buff_id, out var b))
        {
            lBuffId.Text = "???";
            lBuffName.Text = "???";
            lBuffDuration.Text = "???";
            buffIcon.Text = "???";
            lBuffTags.Text = "???";
            ClearBuffTags();
            rtBuffDesc.Clear();
            tvBuffTriggers.Nodes.Clear();
            return;
        }

        lBuffId.Text = b.id.ToString();
        lBuffName.Text = b.nameLocalized;
        lBuffDuration.Text = MSToString(b.duration);
        lBuffTags.Text = TagsAsString(buff_id, AADB.DB_Tagged_Buffs);
        IconIdToLabel(b.icon_id, buffIcon);
        FormattedTextToRichtEdit(b.descLocalized, rtBuffDesc);
        ClearBuffTags();
        foreach (var c in b._others)
        {
            AddBuffTag(c.Key + " = " + c.Value);
        }

        lBuffAddGMCommand.Text = "/addbuff " + lBuffId.Text;
        ShowSelectedData("buffs", "id = " + b.id.ToString(), "id ASC");

        ShowDbBuffTriggers(buff_id);
    }

    private void ShowDbBuffTriggers(long buff_id)
    {
        string EventTypeName(long id)
        {
            switch (id)
            {
                case 1: return "Attack (1)";
                case 2: return "Attacked (2)";
                case 3: return "Damage (3)";
                case 4: return "Damaged (4)";
                case 5: return "Dispelled (5)";
                case 6: return "Timeout (6)";
                case 7: return "DamagedMelee (7)";
                case 8: return "DamagedRanged (8)";
                case 9: return "DamagedSpell (9)";
                case 10: return "DamagedSiege (10)";
                case 11: return "Landing (11)";
                case 12: return "Started (12)";
                case 13: return "RemoveOnMove (13)";
                case 14: return "ChannelingCancel (14)";
                case 15: return "RemoveOnDamage (15)";
                case 16: return "Death (16)";
                case 17: return "Unmount (17)";
                case 18: return "Kill (18)";
                case 19: return "DamagedCollision (19)";
                case 20: return "Immortality (20)";
                case 21: return "Time (21)";
                case 22: return "KillAny (22)";
                default: return id.ToString();
            }
        }

        tvBuffTriggers.Nodes.Clear();
        // Stat Modifiers
        var stats = AADB.DB_UnitModifiers.Values.Where(x => x.owner_type == "Buff" && x.owner_id == buff_id).ToList();
        if (stats.Any())
        {
            var statsNode = tvBuffTriggers.Nodes.Add("Stat modifiers");
            foreach (var unitStat in stats)
            {
                var statNode = statsNode.Nodes.Add($"{unitStat.unit_attribute_id} {unitStat.value}{(unitStat.unit_modifier_type_id != 0 ? "%" : "")}");
                if (unitStat.linear_level_bonus > 0)
                    statNode.Text += $" +Linear Level Bonus: {unitStat.linear_level_bonus}";

                if (statNode.Text.Contains("health", StringComparison.InvariantCultureIgnoreCase))
                    statNode.ForeColor = Color.Red;
                else
                if (statNode.Text.Contains("mana", StringComparison.InvariantCultureIgnoreCase))
                    statNode.ForeColor = Color.DeepSkyBlue;
                else
                if (statNode.Text.Contains("armor", StringComparison.InvariantCultureIgnoreCase))
                    statNode.ForeColor = Color.Yellow;
                else
                if (statNode.Text.Contains("resist", StringComparison.InvariantCultureIgnoreCase))
                    statNode.ForeColor = Color.MediumPurple;
                else
                if (statNode.Text.Contains("speed", StringComparison.InvariantCultureIgnoreCase))
                    statNode.ForeColor = Color.LawnGreen;
                else
                if (statNode.Text.Contains("exp", StringComparison.InvariantCultureIgnoreCase))
                    statNode.ForeColor = Color.WhiteSmoke;
            }
        }


        // Buff Modifiers
        var mods = AADB.DB_BuffModifiers.Values.Where(x => x.owner_type == "Buff" && x.owner_id == buff_id).ToList();
        if (mods.Any())
        {
            var modsNode = tvBuffTriggers.Nodes.Add("Modifiers");
            foreach (var mod in mods)
            {
                TreeNode modNode = null;
                if (mod.buff_id > 0)
                    modNode = AddCustomPropertyNode("buff_id", mod.buff_id.ToString(), false, modsNode);
                if (mod.tag_id > 0)
                    modNode = AddCustomPropertyNode("tag_id", mod.tag_id.ToString(), false, modsNode);
                if (modNode == null)
                    continue; // should never happen

                modNode.Text = @"With " + modNode.Text;

                modNode.Nodes.Add($"buff_attribute_id: {mod.buff_attribute_id}");
                modNode.Nodes.Add($"value: {mod.value}{(mod.unit_modifier_type_id != 0 ? "%" : "")}");
                if (mod.synergy)
                    modNode.Nodes.Add("synergy");
            }
        }


        // Buff Triggers
        var triggers = AADB.DB_BuffTriggers.Values.Where(bt => bt.buff_id == buff_id)
            .GroupBy(bt => bt.event_id, bt => bt).ToDictionary(bt => bt.Key, bt => bt.ToList());

        foreach (var triggerGrouping in triggers)
        {
            var groupingNode = tvBuffTriggers.Nodes.Add(EventTypeName(triggerGrouping.Key));
            groupingNode.ImageIndex = 1;
            groupingNode.SelectedImageIndex = 1;

            foreach (var trigger in triggerGrouping.Value)
            {
                if (AADB.DB_Effects.TryGetValue(trigger.effect_id, out var effect))
                {
                    // var triggerNode = new TreeNode($"{trigger.id} - Effect {trigger.effect_id} ({effect.actual_type} {effect.actual_id})");
                    // groupingNode.Nodes.Add(triggerNode);
                    CreateSkillEffectNode(effect.id, groupingNode, 0, 0, cbBuffsHideEmpty.Checked);
                }
            }
        }

        tvBuffTriggers.ExpandAll();
        if (tvBuffTriggers.Nodes.Count > 0)
            tvBuffTriggers.SelectedNode = tvBuffTriggers.Nodes[0];
    }

    private void DoSkillSearch()
    {
        dgvSkills.Rows.Clear();
        dgvSkillReagents.Rows.Clear();
        dgvSkillProducts.Rows.Clear();
        string searchText = cbSkillSearch.Text;
        if (searchText == string.Empty)
            return;
        string lng = cbItemSearchLanguage.Text;
        string sql = string.Empty;
        string sqlWhere = string.Empty;
        long searchID;
        bool SearchByID = false;
        if (long.TryParse(searchText, out searchID))
            SearchByID = true;
        bool showFirst = true;
        long firstResult = -1;
        string searchTextLower = searchText.ToLower();

        // More Complex syntax with category names
        // SELECT t1.idx, t1.ru, t1.ru_ver, t2.ID, t2.category_id, t3.name, t4.en_us FROM localized_texts as t1 LEFT JOIN items as t2 ON (t1.idx = t2.ID) LEFT JOIN item_categories as t3 ON (t2.category_id = t3.ID) LEFT JOIN localized_texts as t4 ON ((t4.idx = t3.ID) AND (t4.tbl_name = 'item_categories') AND (t4.tbl_column_name = 'name') ) WHERE (t1.tbl_name = 'items') AND (t1.tbl_column_name = 'name') AND (t1.ru LIKE '%Камень%') ORDER BY t1.ru ASC

        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;
        dgvSkills.Visible = false;
        foreach (var skill in AADB.DB_Skills)
        {
            bool addThis = false;
            if (SearchByID)
            {
                if (skill.Key == searchID)
                {
                    addThis = true;
                }
            }
            else if (skill.Value.SearchString.IndexOf(searchTextLower) >= 0)
                addThis = true;

            if (addThis)
            {
                int line = dgvSkills.Rows.Add();
                var row = dgvSkills.Rows[line];
                long itemIdx = skill.Value.id;
                if (firstResult < 0)
                    firstResult = itemIdx;
                row.Cells[0].Value = itemIdx.ToString();
                row.Cells[1].Value = skill.Value.nameLocalized;
                row.Cells[2].Value = skill.Value.descriptionLocalized;
                //row.Cells[3].Value = skill.Value.webDescriptionLocalized;

                if (showFirst)
                {
                    showFirst = false;
                    ShowDbSkill(itemIdx);
                }
            }
        }

        dgvSkills.Visible = true;

        if (dgvSkills.Rows.Count > 0)
            AddToSearchHistory(cbSkillSearch, searchText);

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;

    }

    private void DoSkillsSelectionChanged()
    {
        if (dgvSkills.SelectedRows.Count <= 0)
            return;
        var row = dgvSkills.SelectedRows[0];
        if (row.Cells.Count <= 0)
            return;

        var val = row.Cells[0].Value;
        if (val == null)
            return;
        ShowDbSkill(long.Parse(val.ToString()));
    }

    private void AddSkillLine(long skillIndex)
    {
        if (AADB.DB_Skills.TryGetValue(skillIndex, out var skill))
        {
            int line = dgvSkills.Rows.Add();
            var row = dgvSkills.Rows[line];
            row.Cells[0].Value = skill.id.ToString();
            row.Cells[1].Value = skill.nameLocalized;
            row.Cells[2].Value = skill.descriptionLocalized;

            if (line == 0)
                ShowDbSkill(skill.id);
        }
    }

    private void ShowDbSkillByItem(long id)
    {
        if (AADB.DB_Items.TryGetValue(id, out var item))
        {
            dgvSkills.Rows.Clear();
            dgvSkillReagents.Rows.Clear();
            dgvSkillProducts.Rows.Clear();
            if (item.use_skill_id > 0)
                AddSkillLine(item.use_skill_id);

            foreach (var p in AADB.DB_Skill_Reagents)
            {
                if (p.Value.item_id == id)
                    AddSkillLine(p.Value.skill_id);
            }

            foreach (var p in AADB.DB_Skill_Products)
            {
                if (p.Value.item_id == id)
                    AddSkillLine(p.Value.skill_id);
            }

            cbSkillSearch.Text = string.Empty;
            tcViewer.SelectedTab = tpSkills;
            //cbSkillSearch.Text = item.use_skill_id.ToString();
            //BtnSkillSearch_Click(null, null);
        }
    }

    private void DoFindItemSkill()
    {
        if (dgvItem.SelectedRows.Count <= 0)
            return;
        var row = dgvItem.SelectedRows[0];
        if (row.Cells.Count <= 0)
            return;

        var val = row.Cells[0].Value;
        if (val == null)
            return;

        ShowDbSkillByItem(long.Parse(val.ToString()));
        tcViewer.SelectedTab = tpSkills;
    }

    private void DoSearchBuffs()
    {
        string searchText = cbSearchBuffs.Text.ToLower();
        if (searchText == string.Empty)
            return;
        long searchID;
        if (!long.TryParse(searchText, out searchID))
            searchID = -1;

        bool first = true;
        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;
        dgvBuffs.Rows.Clear();
        int c = 0;
        foreach (var t in AADB.DB_Buffs)
        {
            var b = t.Value;
            if ((b.id == searchID) || (b.SearchString.IndexOf(searchText) >= 0))
            {
                var line = dgvBuffs.Rows.Add();
                var row = dgvBuffs.Rows[line];

                row.Cells[0].Value = b.id.ToString();
                row.Cells[1].Value = b.nameLocalized;
                row.Cells[2].Value = b.duration > 0 ? MSToString(b.duration, true) : "";

                if (first)
                {
                    first = false;
                    ShowDbBuff(b.id);
                }

                c++;
                if (c >= 250)
                {
                    MessageBox.Show(
                        "The results were cut off at " + c.ToString() + " buffs, please refine your search !",
                        "Too many entries", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }

        }

        if (c > 0)
            AddToSearchHistory(cbSearchBuffs, searchText);

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void DoBuffsSelectionChanged()
    {
        if (dgvBuffs.SelectedRows.Count <= 0)
            return;
        var row = dgvBuffs.SelectedRows[0];
        if (row.Cells.Count <= 0)
            return;

        var id = row.Cells[0].Value;
        if (id != null)
        {
            ShowDbBuff(long.Parse(id.ToString()));
            ShowSelectedData("buffs", "id = " + id.ToString(), "id ASC");
        }

    }

    private static string PlotUpdateMethod(long id)
    {
        switch (id)
        {
            case 1: return "OriginalSource (1)";
            case 2: return "OriginalTarget (2)";
            case 3: return "PreviousSource (3)";
            case 4: return "PreviousTarget (4)";
            case 5: return "Area (5)";
            case 6: return "RandomUnit (6)";
            case 7: return "RandomArea (7)";
            default: return id.ToString();
        }
    }

    private void DoSkillAfterSelect(TreeNode node)
    {

        lPlotEventSourceUpdate.Text = @"Source Update: ?";
        lPlotEventTargetUpdate.Text = @"Target Update: ?";
        lPlotEventP1.Text = @"1: ?";
        lPlotEventP2.Text = @"2: ?";
        lPlotEventP3.Text = @"3: ?";
        lPlotEventP4.Text = @"4: ?";
        lPlotEventP5.Text = @"5: ?";
        lPlotEventP6.Text = @"6: ?";
        lPlotEventP7.Text = @"7: ?";
        lPlotEventP8.Text = @"8: ?";
        lPlotEventP9.Text = @"9: ?";
        lPlotEventTickets.Text = @"Tickets: ?";
        lPlotEventAoE.Text = @"AoE: ?";


        if ((node == null) || (node.Tag == null))
            return;
        if (!AADB.DB_Plot_Events.TryGetValue(long.Parse(node.Tag.ToString() ?? "0"), out var plotEvent))
            return;

        lPlotEventSourceUpdate.Text =
            @"Source Update Method: " + PlotUpdateMethod(plotEvent.source_update_method_id);
        lPlotEventTargetUpdate.Text =
            @"Target Update Method: " + PlotUpdateMethod(plotEvent.target_update_method_id);
        lPlotEventP1.Text = @"1: " + plotEvent.target_update_method_param1.ToString();
        lPlotEventP2.Text = @"2: " + plotEvent.target_update_method_param2.ToString();
        lPlotEventP3.Text = @"3: " + plotEvent.target_update_method_param3.ToString();
        lPlotEventP4.Text = @"4: " + plotEvent.target_update_method_param4.ToString();
        lPlotEventP5.Text = @"5: " + plotEvent.target_update_method_param5.ToString();
        lPlotEventP6.Text = @"6: " + plotEvent.target_update_method_param6.ToString();
        lPlotEventP7.Text = @"7: " + plotEvent.target_update_method_param7.ToString();
        lPlotEventP8.Text = @"8: " + plotEvent.target_update_method_param8.ToString();
        lPlotEventP9.Text = @"9: " + plotEvent.target_update_method_param9.ToString();
        lPlotEventTickets.Text = @"Tickets: " + plotEvent.tickets.ToString();
        lPlotEventAoE.Text = @"AoE: " + (plotEvent.aeo_diminishing ? "Diminishing" : "Normal");

        ShowSelectedData("plot_events", "id == " + plotEvent.id.ToString(), "id");
    }

    private void LoadUnitReqs()
    {
        AADB.DB_UnitReqs.Clear();
        using var connection = SQLite.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM unit_reqs";
        command.Prepare();
        using var sqliteReader = command.ExecuteReader();
        using var reader = new SQLiteWrapperReader(sqliteReader);
        var useDbId = false;
        var columnNames = reader.GetColumnNames();
        if (columnNames.IndexOf("id") >= 0)
            useDbId = true;
        var i = 0u;
        while (reader.Read())
        {
            var t = new GameUnitReqs();
            i++;
            t.id = useDbId ? reader.GetUInt32("id") : i;
            t.owner_id = reader.GetUInt32("owner_id");
            t.owner_type = reader.GetString("owner_type");
            t.kind_id = (GameUnitReqsKind)reader.GetUInt32("kind_id");
            t.value1 = reader.GetUInt32("value1");
            t.value2 = reader.GetUInt32("value2");

            AADB.DB_UnitReqs.TryAdd(t.id, t);
        }
    }

    private void LoadUnitMods()
    {
        AADB.DB_UnitReqs.Clear();
        using var connection = SQLite.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM unit_modifiers";
        command.Prepare();
        using var sqliteReader = command.ExecuteReader();
        using var reader = new SQLiteWrapperReader(sqliteReader);
        var useDbId = false;
        var columnNames = reader.GetColumnNames();
        if (columnNames.IndexOf("id") >= 0)
            useDbId = true;
        var i = 0u;
        while (reader.Read())
        {
            var t = new GameUnitModifiers();
            i++;
            t.id = useDbId ? reader.GetInt64("id") : i;
            t.owner_id = reader.GetInt64("owner_id");
            t.owner_type = reader.GetString("owner_type");
            t.unit_attribute_id = (UnitAttribute)reader.GetInt64("unit_attribute_id");
            t.unit_modifier_type_id = reader.GetInt64("unit_modifier_type_id");
            t.value = reader.GetInt64("value");
            t.linear_level_bonus = reader.GetInt64("linear_level_bonus");

            AADB.DB_UnitModifiers.TryAdd(t.id, t);
        }
    }

    private IEnumerable<GameUnitReqs> GetRequirement(string ownerType, long ownerId)
    {
        return AADB.DB_UnitReqs.Values.Where(x => x.owner_id == ownerId && x.owner_type == ownerType);
    }

    public List<GameUnitReqs> GetSkillRequirements(long skillId)
    {
        return GetRequirement("Skill", skillId).ToList();
    }

    public List<GameUnitReqs> GetAchievementObjectiveRequirements(long achievementObjectiveId)
    {
        return GetRequirement("AchievementObjective", achievementObjectiveId).ToList();
    }

    public List<GameUnitReqs> GetAiEventRequirements(long aiEvent)
    {
        return GetRequirement("AiEvent", aiEvent).ToList();
    }

    public List<GameUnitReqs> GetItemArmorRequirements(long armorId)
    {
        return GetRequirement("ItemArmor", armorId).ToList();
    }

    public List<GameUnitReqs> GetItemWeaponRequirements(long weaponId)
    {
        return GetRequirement("ItemWeapon", weaponId).ToList();
    }

    public List<GameUnitReqs> GetQuestComponentRequirements(long componentId)
    {
        return GetRequirement("QuestComponent", componentId).ToList();
    }

    public List<GameUnitReqs> GetSphereRequirements(long sphereId)
    {
        return GetRequirement("Sphere", sphereId).ToList();
    }
}