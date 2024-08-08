using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AAEmu.DBViewer.DbDefs;
using AAEmu.DBViewer.enums;
using AAEmu.Game.Utils.DB;

namespace AAEmu.DBViewer;

public partial class MainForm
{
    private void LoadSkills()
    {
        Application.UseWaitCursor = true;
        Cursor = Cursors.WaitCursor;

        using (var connection = SQLite.CreateConnection())
        {
            // Skills base
            if (AllTableNames.GetValueOrDefault("skills") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM skills ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var columnNames = reader.GetColumnNames();
                        var readWebDesc = (columnNames.IndexOf("web_desc") >= 0);

                        while (reader.Read())
                        {
                            var t = new GameSkills();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.Desc = GetString(reader, "desc");
                            // Added a check for this
                            if (readWebDesc)
                                t.WebDesc = GetString(reader, "web_desc");
                            else
                                t.WebDesc = string.Empty;
                            t.Cost = GetInt64(reader, "cost");
                            t.IconId = GetInt64(reader, "icon_id");
                            t.Show = GetBool(reader, "show");
                            t.CooldownTime = GetInt64(reader, "cooldown_time");
                            t.CastingTime = GetInt64(reader, "casting_time");
                            t.IgnoreGlobalCooldown = GetBool(reader, "ignore_global_cooldown");
                            t.EffectDelay = GetInt64(reader, "effect_delay");
                            t.AbilityId = GetInt64(reader, "ability_id");
                            t.ManaCost = GetInt64(reader, "mana_cost");
                            t.TimingId = GetInt64(reader, "timing_id");
                            t.ConsumeLp = GetInt64(reader, "consume_lp");
                            t.DefaultGcd = GetBool(reader, "default_gcd");
                            ;
                            t.CustomGcd = GetInt64(reader, "custom_gcd");
                            t.FirstReagentOnly = GetBool(reader, "first_reagent_only");
                            t.PlotId = GetInt64(reader, "plot_id");

                            t.OrUnitReqs = GetBool(reader, "or_unit_reqs");

                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "skills", "name", t.Name);
                            t.DescriptionLocalized = AaDb.GetTranslationById(t.Id, "skills", "desc", t.Desc);
                            if (readWebDesc)
                                t.WebDescriptionLocalized =
                                    AaDb.GetTranslationById(t.Id, "skills", "web_desc", t.WebDesc);
                            else
                                t.WebDescriptionLocalized = string.Empty;

                            t.SearchString = t.Name + " " + t.Desc + " " + t.NameLocalized + " " +
                                             t.DescriptionLocalized;
                            t.SearchString = t.SearchString.ToLower();

                            AaDb.DbSkills.Add(t.Id, t);
                        }

                    }
                }
            }

            // Skill Effects
            if (AllTableNames.GetValueOrDefault("skill_effects") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM skill_effects ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameSkillEffects();
                            t.Id = GetInt64(reader, "id");
                            t.SkillId = GetInt64(reader, "skill_id");
                            t.EffectId = GetInt64(reader, "effect_id");
                            t.Weight = GetInt64(reader, "weight");
                            t.StartLevel = GetInt64(reader, "start_level");
                            t.EndLevel = GetInt64(reader, "end_level");
                            t.Friendly = GetBool(reader, "friendly");
                            t.NonFriendly = GetBool(reader, "non_friendly");
                            t.TargetBuffTagId = GetInt64(reader, "target_buff_tag_id");
                            t.TargetNoBuffTagId = GetInt64(reader, "target_nobuff_tag_id");
                            t.SourceBuffTagId = GetInt64(reader, "source_buff_tag_id");
                            t.SourceNoBuffTagId = GetInt64(reader, "source_nobuff_tag_id");
                            t.Chance = GetInt64(reader, "chance");
                            t.Front = GetBool(reader, "front");
                            t.Back = GetBool(reader, "back");
                            t.TargetNpcTagId = GetInt64(reader, "target_npc_tag_id");
                            t.ApplicationMethodId = GetInt64(reader, "application_method_id");
                            t.SynergyText = GetBool(reader, "synergy_text");
                            t.ConsumeSourceItem = GetBool(reader, "consume_source_item");
                            t.ConsumeItemId = GetInt64(reader, "consume_item_id");
                            t.ConsumeItemCount = GetInt64(reader, "consume_item_count");
                            t.AlwaysHit = GetBool(reader, "always_hit");
                            t.ItemSetId = GetInt64(reader, "item_set_id");
                            t.InteractionSuccessHit = GetBool(reader, "interaction_success_hit");
                            AaDb.DbSkillEffects.Add(t.Id, t);
                        }

                    }
                }
            }

            // Effects
            if (AllTableNames.GetValueOrDefault("effects") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM effects ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameEffects();
                            t.Id = GetInt64(reader, "id");
                            t.ActualId = GetInt64(reader, "actual_id");
                            t.ActualType = GetString(reader, "actual_type");
                            AaDb.DbEffects.Add(t.Id, t);
                        }

                    }
                }
            }

            // Mount Skills
            if (AllTableNames.GetValueOrDefault("mount_skills") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM mount_skills ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        // name field is not present after in 3.0.3.0
                        var columnNames = reader.GetColumnNames();
                        var readNames = (columnNames.IndexOf("name") >= 0);

                        while (reader.Read())
                        {
                            var t = new GameMountSkill();
                            t.Id = GetInt64(reader, "id");
                            if (readNames)
                                t.Name = GetString(reader, "name");
                            t.SkillId = GetInt64(reader, "skill_id");
                            AaDb.DbMountSkills.Add(t.Id, t);
                        }
                    }
                }
            }

            // Slave Mount Skills
            if (AllTableNames.GetValueOrDefault("slave_mount_skills") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM slave_mount_skills ORDER BY slave_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var columnNames = reader.GetColumnNames();
                        // id field is not present after in 3.0.3.0
                        var readId = (columnNames.IndexOf("id") >= 0);
                        var indx = 1L;
                        while (reader.Read())
                        {
                            var t = new GameSlaveMountSkill();
                            t.Id = readId ? GetInt64(reader, "id") : indx;
                            t.SlaveId = GetInt64(reader, "slave_id");
                            t.MountSkillId = GetInt64(reader, "mount_skill_id");
                            AaDb.DbSlaveMountSkills.Add(t.Id, t);
                            indx++;
                        }
                    }
                }
            }

            // NpSkills
            if (AllTableNames.GetValueOrDefault("np_skills") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM np_skills ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            var t = new GameNpSkills();
                            t.Id = GetInt64(reader, "id");
                            t.OwnerId = GetInt64(reader, "owner_id");
                            t.OwnerType = GetString(reader, "owner_type"); // They are actually all "Npc"
                            t.SkillId = GetInt64(reader, "skill_id");
                            t.SkillUseConditionId = (SkillUseConditionKind)GetInt64(reader, "skill_use_condition_id");
                            t.SkillUseParam1 = GetFloat(reader, "skill_use_param1");
                            t.SkillUseParam2 = GetFloat(reader, "skill_use_param2");
                            AaDb.DbNpSkills.Add(t.Id, t);
                        }

                    }
                }
            }
        }

        Cursor = Cursors.Default;
        Application.UseWaitCursor = false;
    }

    private void LoadSkillReagents()
    {
        if (AllTableNames.GetValueOrDefault("skill_reagents") == SQLite.SQLiteFileName)
        {
            using var connection = SQLite.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM skill_reagents ORDER BY id ASC";
            command.Prepare();
            using var reader = new SQLiteWrapperReader(command.ExecuteReader());
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            while (reader.Read())
            {
                GameSkillItems t = new GameSkillItems();
                t.Id = GetInt64(reader, "id");
                t.SkillId = GetInt64(reader, "skill_id");
                t.ItemId = GetInt64(reader, "item_id");
                t.Amount = GetInt64(reader, "amount");

                AaDb.DbSkillReagents.Add(t.Id, t);
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }
    }

    private void LoadSkillProducts()
    {
        if (AllTableNames.GetValueOrDefault("skill_products") == SQLite.SQLiteFileName)
        {
            using var connection = SQLite.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM skill_products ORDER BY id ASC";
            command.Prepare();
            using var reader = new SQLiteWrapperReader(command.ExecuteReader());
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            while (reader.Read())
            {
                GameSkillItems t = new GameSkillItems();
                t.Id = GetInt64(reader, "id");
                t.SkillId = GetInt64(reader, "skill_id");
                t.ItemId = GetInt64(reader, "item_id");
                t.Amount = GetInt64(reader, "amount");

                AaDb.DbSkillProducts.Add(t.Id, t);
            }

            Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }
    }

    private void LoadBuffs()
    {
        using (var connection = SQLite.CreateConnection())
        {
            Application.UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            if (AllTableNames.GetValueOrDefault("buffs") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM buffs ORDER BY id ASC";
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
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.Desc = GetString(reader, "desc");
                            t.IconId = GetInt64(reader, "icon_id");
                            t.Duration = GetInt64(reader, "duration");

                            t.NameLocalized = AaDb.GetTranslationById(t.Id, "buffs", "name", t.Name);
                            t.DescLocalized = AaDb.GetTranslationById(t.Id, "buffs", "desc", t.Desc);

                            t.SearchString = t.Name + " " + t.NameLocalized + " " + t.Desc + " " + t.DescLocalized;
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
                                            t.Others.Add(c, v);
                                    }
                                    else if ((v != string.Empty) && (v != "0") && (v != "f") && (v != "NULL") &&
                                             (v != "--- :null"))
                                    {
                                        t.Others.Add(c, v);
                                    }
                                }
                            }

                            AaDb.DbBuffs.Add(t.Id, t);
                        }
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("buff_triggers") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM buff_triggers ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {

                        while (reader.Read())
                        {
                            var t = new GameBuffTrigger();
                            t.Id = GetInt64(reader, "id");
                            t.BuffId = GetInt64(reader, "buff_id");
                            t.EventId = GetInt64(reader, "event_id");
                            t.EffectId = GetInt64(reader, "effect_id");

                            AaDb.DbBuffTriggers.Add(t.Id, t);
                        }
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("buff_modifiers") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM buff_modifiers";
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
                            t.Id = useDbId ? GetInt64(reader, "id") : dbId;
                            t.OwnerId = GetInt64(reader, "owner_id");
                            t.OwnerType = GetString(reader, "owner_type");
                            t.BuffId = GetInt64(reader, "buff_id");
                            t.TagId = GetInt64(reader, "tag_id");
                            t.BuffAttributeId = (BuffAttribute)GetInt64(reader, "buff_attribute_id");
                            t.UnitModifierTypeId = GetInt64(reader, "unit_modifier_type_id");
                            t.Value = GetInt64(reader, "value");
                            t.Synergy = GetBool(reader, "synergy");

                            AaDb.DbBuffModifiers.Add(t.Id, t);
                        }
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("npc_initial_buffs") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM npc_initial_buffs ORDER BY npc_id ASC";
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
                            t.Id = readId ? GetInt64(reader, "id") : indx;
                            t.NpcId = GetInt64(reader, "npc_id");
                            t.BuffId = GetInt64(reader, "buff_id");

                            AaDb.DbNpcInitialBuffs.Add(t.Id, t);
                            indx++;
                        }
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("passive_buffs") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM passive_buffs ORDER BY id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {

                        while (reader.Read())
                        {
                            var t = new GamePassiveBuff();
                            t.Id = GetInt64(reader, "id");
                            t.AbilityId = GetInt64(reader, "ability_id");
                            t.Level = GetInt64(reader, "level");
                            t.BuffId = GetInt64(reader, "buff_id");
                            t.ReqPoints = GetInt64(reader, "req_points");
                            t.Active = GetBool(reader, "active");

                            AaDb.DbPassiveBuffs.Add(t.Id, t);
                        }
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("slave_passive_buffs") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM slave_passive_buffs ORDER BY owner_id ASC";
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
                            t.Id = readId ? GetInt64(reader, "id") : indx;
                            t.OwnerId = GetInt64(reader, "owner_id");
                            t.OwnerType = GetString(reader, "owner_type");
                            t.PassiveBuffId = GetInt64(reader, "passive_buff_id");

                            AaDb.DbSlavePassiveBuffs.Add(t.Id, t);
                            indx++;
                        }
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("np_passive_buffs") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM np_passive_buffs ORDER BY owner_id ASC";
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

                            var t = new GameNpPassiveBuff();
                            t.Id = readId ? GetInt64(reader, "id") : indx;
                            t.OwnerId = GetInt64(reader, "owner_id");
                            t.OwnerType = GetString(reader, "owner_type");
                            t.PassiveBuffId = GetInt64(reader, "passive_buff_id");

                            AaDb.DbNpPassiveBuffs.Add(t.Id, t);
                            indx++;
                        }
                    }
                }
            }

            if (AllTableNames.GetValueOrDefault("slave_initial_buffs") == SQLite.SQLiteFileName)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM slave_initial_buffs ORDER BY slave_id ASC";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        var indx = 1L;
                        while (reader.Read())
                        {
                            var t = new GameSlaveInitialBuff();
                            //t.id = GetInt64(reader, "id"); no in 3030
                            t.Id = indx;
                            t.SlaveId = GetInt64(reader, "slave_id");
                            t.BuffId = GetInt64(reader, "buff_id");

                            AaDb.DbSlaveInitialBuffs.Add(t.Id, t);
                            indx++;
                        }
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
        if (AllTableNames.GetValueOrDefault("plots") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM plots";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            var t = new GamePlot();
                            t.Id = GetInt64(reader, "id");
                            t.Name = GetString(reader, "name");
                            t.TargetTypeId = GetInt64(reader, "target_type_id");

                            AaDb.DbPlots.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        // events
        if (AllTableNames.GetValueOrDefault("plot_events") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM plot_events";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GamePlotEvent();
                            t.Id = GetInt64(reader, "id");
                            t.PlotId = GetInt64(reader, "plot_id");
                            t.Postion = GetInt64(reader, "position");
                            t.Name = GetString(reader, "name");
                            t.SourceUpdateMethodId = GetInt64(reader, "source_update_method_id");
                            t.TargetUpdateMethodId = GetInt64(reader, "target_update_method_id");
                            t.TargetUpdateMethodParam1 = GetInt64(reader, "target_update_method_param1");
                            t.TargetUpdateMethodParam2 = GetInt64(reader, "target_update_method_param2");
                            t.TargetUpdateMethodParam3 = GetInt64(reader, "target_update_method_param3");
                            t.TargetUpdateMethodParam4 = GetInt64(reader, "target_update_method_param4");
                            t.TargetUpdateMethodParam5 = GetInt64(reader, "target_update_method_param5");
                            t.TargetUpdateMethodParam6 = GetInt64(reader, "target_update_method_param6");
                            t.TargetUpdateMethodParam7 = GetInt64(reader, "target_update_method_param7");
                            t.TargetUpdateMethodParam8 = GetInt64(reader, "target_update_method_param8");
                            t.TargetUpdateMethodParam9 = GetInt64(reader, "target_update_method_param9");
                            t.Tickets = GetInt64(reader, "tickets");
                            t.AeoDiminishing = GetBool(reader, "aoe_diminishing");

                            AaDb.DbPlotEvents.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        // next events
        if (AllTableNames.GetValueOrDefault("plot_next_events") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM plot_next_events";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            var t = new GamePlotNextEvent();
                            t.Id = GetInt64(reader, "id");
                            t.EventId = GetInt64(reader, "event_id");
                            t.Postion = GetInt64(reader, "position");
                            t.NextEventId = GetInt64(reader, "next_event_id");
                            t.Delay = GetInt64(reader, "delay");
                            t.Speed = GetInt64(reader, "speed");

                            AaDb.DbPlotNextEvents.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        // plot events condition
        if (AllTableNames.GetValueOrDefault("plot_event_conditions") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM plot_event_conditions";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {

                            var t = new GamePlotEventCondition();
                            t.Id = GetInt64(reader, "id");
                            t.EventId = GetInt64(reader, "event_id");
                            t.Postion = GetInt64(reader, "position");
                            t.ConditionId = GetInt64(reader, "condition_id");
                            t.SourceId = GetInt64(reader, "source_id");
                            t.TargetId = GetInt64(reader, "target_id");
                            t.NotifyFailure = GetBool(reader, "notify_failure");

                            AaDb.DbPlotEventConditions.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        // plot condition
        if (AllTableNames.GetValueOrDefault("plot_conditions") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM plot_conditions";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GamePlotCondition();
                            t.Id = GetInt64(reader, "id");
                            t.NotCondition = GetBool(reader, "not_condition");
                            t.KindId = GetInt64(reader, "kind_id");
                            t.Param1 = GetInt64(reader, "param1");
                            t.Param2 = GetInt64(reader, "param2");
                            t.Param3 = GetInt64(reader, "param3");

                            AaDb.DbPlotConditions.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
                }
            }
        }

        // plot effects
        if (AllTableNames.GetValueOrDefault("plot_effects") == SQLite.SQLiteFileName)
        {
            using (var connection = SQLite.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM plot_effects";
                    command.Prepare();
                    using (var reader = new SQLiteWrapperReader(command.ExecuteReader()))
                    {
                        Application.UseWaitCursor = true;
                        Cursor = Cursors.WaitCursor;

                        while (reader.Read())
                        {
                            var t = new GamePlotEffect();
                            t.Id = GetInt64(reader, "id");
                            t.EventId = GetInt64(reader, "event_id");
                            t.Position = GetInt64(reader, "position");
                            t.SourceId = GetInt64(reader, "source_id");
                            t.TargetId = GetInt64(reader, "target_id");
                            t.ActualId = GetInt64(reader, "actual_id");
                            t.ActualType = GetString(reader, "actual_type");

                            AaDb.DbPlotEffects.Add(t.Id, t);
                        }

                        Cursor = Cursors.Default;
                        Application.UseWaitCursor = false;
                    }
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

        var nodeName = nodeNamePrefix + child.Id.ToString() + " - " + child.Name;
        if (child.Tickets > 1)
            nodeName = child.Tickets.ToString() + " x " + nodeName;
        var eventNode = new TreeNode(nodeName);
        eventNode.ImageIndex = 1;
        eventNode.SelectedImageIndex = 1;
        eventNode.Tag = child.Id;

        parent.Nodes.Add(eventNode);

        // Does it have conditions ?
        var plotEventConditions = AaDb.DbPlotEventConditions.Where(pec => pec.Value.EventId == child.Id);
        foreach (var plotEventCondition in plotEventConditions)
        {
            var eventConditionName = $"Event Condition ({plotEventCondition.Value.ConditionId})";
            var eventConditionNode = new TreeNode(eventConditionName);
            eventConditionNode.ImageIndex = 3;
            eventConditionNode.SelectedImageIndex = 3;
            eventConditionNode.Tag = 0;
            eventNode.Nodes.Add(eventConditionNode);
            eventConditionNode.Nodes.Add("Source: " + PlotUpdateMethod(plotEventCondition.Value.SourceId));
            eventConditionNode.Nodes.Add("Target: " + PlotUpdateMethod(plotEventCondition.Value.TargetId));
            eventConditionNode.Nodes.Add("Notify Failure: " + plotEventCondition.Value.NotifyFailure);
            if (AaDb.DbPlotConditions.TryGetValue(plotEventCondition.Value.ConditionId, out var condition))
            {
                eventConditionNode.Text += (condition.NotCondition ? " NOT " : " ") + ConditionTypeName(condition.KindId);
                // eventConditionNode.Nodes.Add("Condition: " + (condition.not_condition ? "NOT " : "") + ConditionTypeName(condition.kind_id));
                if (condition.Param1 != 0)
                    eventConditionNode.Nodes.Add("Param1: " + condition.Param1);
                if (condition.Param2 != 0)
                    eventConditionNode.Nodes.Add("Param2: " + condition.Param2);
                if (condition.Param3 != 0)
                    eventConditionNode.Nodes.Add("Param3: " + condition.Param3);
            }
        }

        // Plot Effects
        var plotEventEffects = AaDb.DbPlotEffects.Where(pec => pec.Value.EventId == child.Id).OrderBy(pec => pec.Value.Position);
        foreach (var plotEffect in plotEventEffects)
        {
            var eventConditionName = $"Plot Effect ({plotEffect.Value.Id})";
            var effectNode = new TreeNode(eventConditionName);
            effectNode.ImageIndex = 2;
            effectNode.SelectedImageIndex = 2;
            effectNode.Tag = 0;
            eventNode.Nodes.Add(effectNode);
            effectNode.Nodes.Add("Pos: " + plotEffect.Value.Position);
            effectNode.Nodes.Add("Source: " + PlotUpdateMethod(plotEffect.Value.SourceId));
            effectNode.Nodes.Add("Target: " + PlotUpdateMethod(plotEffect.Value.TargetId));
            // var actualEffectNode = effectNode.Nodes.Add("Actual Effect: " + plotEffect.Value.actual_type + " (" +plotEffect.Value.actual_id + ")");

            CreatePlotEffectNode(plotEffect.Value.ActualType, plotEffect.Value.ActualId, effectNode, true);
        }

        var nextEvents = AaDb.DbPlotNextEvents.Where(nextEvent => nextEvent.Value.EventId == child.Id).OrderBy(p => p.Value.Postion);
        foreach (var n in nextEvents)
        {
            if (n.Value.NextEventId == n.Value.EventId)
            {
                var rNode = eventNode.Nodes.Add("Repeats itself with Delay: " + n.Value.Delay + ", Speed: " + n.Value.Speed);
                rNode.ImageIndex = 3;
                rNode.SelectedImageIndex = rNode.ImageIndex;
            }
            else
            if (AaDb.DbPlotEvents.TryGetValue(n.Value.NextEventId, out var next))
            {
                //AddPlotEventNode(eventNode, next, depth, "Next Plot Event: ");

                var nextNode = new TreeNode("Next Event Node: " + next.Id.ToString() + " - " + next.Name);
                nextNode.Tag = next.Id;
                nextNode.ImageIndex = 2;
                nextNode.SelectedImageIndex = nextNode.ImageIndex;
                eventNode.Nodes.Add(nextNode);
            }
            else
            {
                var errorNode = new TreeNode("Unknown Next Event: " + n.Value.NextEventId.ToString());
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
        var imgId = ilIcons.Images.IndexOfKey(skill.IconId.ToString());
        var rootNode = new TreeNode(skill.NameLocalized + $" ( {skill.Id} )", imgId, imgId);
        rootNode.Tag = 0;
        tvSkill.Nodes.Add(rootNode);

        var requires = GetSkillRequirements(skill.Id);
        var reqNode = AddUnitRequirementNode(requires, skill.OrUnitReqs, tvSkill.Nodes);

        var skillsProperties = GetCustomTableValues("skills", "id", skill.Id.ToString());
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

        var skillEffectsList = from se in AaDb.DbSkillEffects
            where se.Value.SkillId == skill.Id
            select se.Value;

        TreeNode effectsRoot = null;

        if (skillEffectsList != null)
        {
            long totalWeight = 0;
            foreach (var skillEffect in skillEffectsList)
            {
                totalWeight += skillEffect.Weight;
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

                CreateSkillEffectNode(skillEffect.EffectId, effectsRoot, skillEffect.Weight, totalWeight, true);
            }
        }


        if (AaDb.DbPlots.TryGetValue(skill.PlotId, out var plot))
        {
            var firstPlotNode = new TreeNode(plot.Id.ToString() + " - " + plot.Name);
            firstPlotNode.ImageIndex = 2;
            firstPlotNode.SelectedImageIndex = 2;
            firstPlotNode.Tag = plot.Id;
            tvSkill.Nodes.Add(firstPlotNode);

            var events = AaDb.DbPlotEvents.Where(plotEvent => plotEvent.Value.PlotId == plot.Id)
                .OrderBy(plotEvent => plotEvent.Value.Postion);
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
        if (AaDb.DbEffects.TryGetValue(effectId, out var effect))
        {
            var rate = (totalWeight > 0 && thisWeight > 0) ? thisWeight * 100f / totalWeight : 100f;
            effectTypeText = effect.ActualType + " ( " + effect.ActualId.ToString() + " )" + (rate < 100 ? $" {rate:F0}%" : "");
        }

        var skillEffectNode = effectsRoot.Nodes.Add(effectTypeText);
        skillEffectNode.ImageIndex = 2;
        skillEffectNode.SelectedImageIndex = 2;
        skillEffectNode.Tag = 0;

        if (effect != null)
        {
            var effectsTableName = FunctionTypeToTableName(effect.ActualType);
            var effectValuesList = GetCustomTableValues(effectsTableName, "id", effect.ActualId.ToString());
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
        if (AaDb.DbSkills.TryGetValue(idx, out var skill))
        {
            lSkillID.Text = idx.ToString();
            lSkillName.Text = skill.NameLocalized;
            lSkillCost.Text = skill.Cost.ToString();
            lSkillMana.Text = skill.ManaCost.ToString();
            lSkillLabor.Text = skill.ConsumeLp.ToString();
            lSkillCooldown.Text = MSToString(skill.CooldownTime);
            if ((skill.DefaultGcd) && (!skill.IgnoreGlobalCooldown))
            {
                lSkillGCD.Text = "Default";
            }
            else if ((!skill.DefaultGcd) && (!skill.IgnoreGlobalCooldown))
            {
                lSkillGCD.Text = MSToString(skill.CustomGcd);
            }
            else if ((!skill.DefaultGcd) && (skill.IgnoreGlobalCooldown))
            {
                lSkillGCD.Text = "Ignored";
            }
            else
            {
                lSkillGCD.Text = "Default";
            }

            // lSkillGCD.Text = skill.ignore_global_cooldown ? "Ignore" : "Normal";
            FormattedTextToRichtEdit(skill.DescriptionLocalized, rtSkillDescription);
            IconIdToLabel(skill.IconId, skillIcon);
            lSkillTags.Text = TagsAsString(idx, AaDb.DbTaggedSkills);

            ShowSelectedData("skills", "(id = " + idx.ToString() + ")", "id ASC");

            if (skill.FirstReagentOnly)
            {
                labelSkillReagents.Text = "Requires either of these items to use";
            }
            else
            {
                labelSkillReagents.Text = "Required items to use this skill";
            }

            // Produces
            dgvSkillProducts.Rows.Clear();
            foreach (var p in AaDb.DbSkillProducts)
            {
                if (p.Value.SkillId == idx)
                {
                    var line = dgvSkillProducts.Rows.Add();
                    var row = dgvSkillProducts.Rows[line];
                    row.Cells[0].Value = p.Value.ItemId.ToString();
                    if (AaDb.DbItems.TryGetValue(p.Value.ItemId, out var item))
                    {
                        row.Cells[1].Value = item.NameLocalized;
                    }
                    else
                    {
                        row.Cells[1].Value = "???";
                    }

                    row.Cells[2].Value = p.Value.Amount.ToString();
                }
            }

            // Reagents
            dgvSkillReagents.Rows.Clear();
            foreach (var p in AaDb.DbSkillReagents)
            {
                if (p.Value.SkillId == idx)
                {
                    var line = dgvSkillReagents.Rows.Add();
                    var row = dgvSkillReagents.Rows[line];
                    row.Cells[0].Value = p.Value.ItemId.ToString();
                    if (AaDb.DbItems.TryGetValue(p.Value.ItemId, out var item))
                    {
                        row.Cells[1].Value = item.NameLocalized;
                    }
                    else
                    {
                        row.Cells[1].Value = "???";
                    }

                    row.Cells[2].Value = p.Value.Amount.ToString();
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
        if (!AaDb.DbBuffs.TryGetValue(buff_id, out var b))
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

        lBuffId.Text = b.Id.ToString();
        lBuffName.Text = b.NameLocalized;
        lBuffDuration.Text = MSToString(b.Duration);
        lBuffTags.Text = TagsAsString(buff_id, AaDb.DbTaggedBuffs);
        IconIdToLabel(b.IconId, buffIcon);
        FormattedTextToRichtEdit(b.DescLocalized, rtBuffDesc);
        ClearBuffTags();
        foreach (var c in b.Others)
        {
            AddBuffTag(c.Key + " = " + c.Value);
        }

        lBuffAddGMCommand.Text = "/addbuff " + lBuffId.Text;
        ShowSelectedData("buffs", "id = " + b.Id.ToString(), "id ASC");

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
        var stats = AaDb.DbUnitModifiers.Values.Where(x => x.OwnerType == "Buff" && x.OwnerId == buff_id).ToList();
        if (stats.Any())
        {
            var statsNode = tvBuffTriggers.Nodes.Add("Stat modifiers");
            foreach (var unitStat in stats)
            {
                var statNode = statsNode.Nodes.Add($"{unitStat.UnitAttributeId} {unitStat.Value}{(unitStat.UnitModifierTypeId != 0 ? "%" : "")}");
                if (unitStat.LinearLevelBonus > 0)
                    statNode.Text += $" +Linear Level Bonus: {unitStat.LinearLevelBonus}";

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
        var mods = AaDb.DbBuffModifiers.Values.Where(x => x.OwnerType == "Buff" && x.OwnerId == buff_id).ToList();
        if (mods.Any())
        {
            var modsNode = tvBuffTriggers.Nodes.Add("Modifiers");
            foreach (var mod in mods)
            {
                TreeNode modNode = null;
                if (mod.BuffId > 0)
                    modNode = AddCustomPropertyNode("buff_id", mod.BuffId.ToString(), false, modsNode);
                if (mod.TagId > 0)
                    modNode = AddCustomPropertyNode("tag_id", mod.TagId.ToString(), false, modsNode);
                if (modNode == null)
                    continue; // should never happen

                modNode.Text = @"With " + modNode.Text;

                modNode.Nodes.Add($"buff_attribute_id: {mod.BuffAttributeId}");
                modNode.Nodes.Add($"value: {mod.Value}{(mod.UnitModifierTypeId != 0 ? "%" : "")}");
                if (mod.Synergy)
                    modNode.Nodes.Add("synergy");
            }
        }


        // Buff Triggers
        var triggers = AaDb.DbBuffTriggers.Values.Where(bt => bt.BuffId == buff_id)
            .GroupBy(bt => bt.EventId, bt => bt).ToDictionary(bt => bt.Key, bt => bt.ToList());

        foreach (var triggerGrouping in triggers)
        {
            var groupingNode = tvBuffTriggers.Nodes.Add(EventTypeName(triggerGrouping.Key));
            groupingNode.ImageIndex = 1;
            groupingNode.SelectedImageIndex = 1;

            foreach (var trigger in triggerGrouping.Value)
            {
                if (AaDb.DbEffects.TryGetValue(trigger.EffectId, out var effect))
                {
                    // var triggerNode = new TreeNode($"{trigger.id} - Effect {trigger.effect_id} ({effect.actual_type} {effect.actual_id})");
                    // groupingNode.Nodes.Add(triggerNode);
                    CreateSkillEffectNode(effect.Id, groupingNode, 0, 0, cbBuffsHideEmpty.Checked);
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
        foreach (var skill in AaDb.DbSkills)
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
                long itemIdx = skill.Value.Id;
                if (firstResult < 0)
                    firstResult = itemIdx;
                row.Cells[0].Value = itemIdx.ToString();
                row.Cells[1].Value = skill.Value.NameLocalized;
                row.Cells[2].Value = skill.Value.DescriptionLocalized;
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
        if (AaDb.DbSkills.TryGetValue(skillIndex, out var skill))
        {
            int line = dgvSkills.Rows.Add();
            var row = dgvSkills.Rows[line];
            row.Cells[0].Value = skill.Id.ToString();
            row.Cells[1].Value = skill.NameLocalized;
            row.Cells[2].Value = skill.DescriptionLocalized;

            if (line == 0)
                ShowDbSkill(skill.Id);
        }
    }

    private void ShowDbSkillByItem(long id)
    {
        if (AaDb.DbItems.TryGetValue(id, out var item))
        {
            dgvSkills.Rows.Clear();
            dgvSkillReagents.Rows.Clear();
            dgvSkillProducts.Rows.Clear();
            if (item.UseSkillId > 0)
                AddSkillLine(item.UseSkillId);

            foreach (var p in AaDb.DbSkillReagents)
            {
                if (p.Value.ItemId == id)
                    AddSkillLine(p.Value.SkillId);
            }

            foreach (var p in AaDb.DbSkillProducts)
            {
                if (p.Value.ItemId == id)
                    AddSkillLine(p.Value.SkillId);
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
        foreach (var t in AaDb.DbBuffs)
        {
            var b = t.Value;
            if ((b.Id == searchID) || (b.SearchString.IndexOf(searchText) >= 0))
            {
                var line = dgvBuffs.Rows.Add();
                var row = dgvBuffs.Rows[line];

                row.Cells[0].Value = b.Id.ToString();
                row.Cells[1].Value = b.NameLocalized;
                row.Cells[2].Value = b.Duration > 0 ? MSToString(b.Duration, true) : "";

                if (first)
                {
                    first = false;
                    ShowDbBuff(b.Id);
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
        if (!AaDb.DbPlotEvents.TryGetValue(long.Parse(node.Tag.ToString() ?? "0"), out var plotEvent))
            return;

        lPlotEventSourceUpdate.Text =
            @"Source Update Method: " + PlotUpdateMethod(plotEvent.SourceUpdateMethodId);
        lPlotEventTargetUpdate.Text =
            @"Target Update Method: " + PlotUpdateMethod(plotEvent.TargetUpdateMethodId);
        lPlotEventP1.Text = @"1: " + plotEvent.TargetUpdateMethodParam1.ToString();
        lPlotEventP2.Text = @"2: " + plotEvent.TargetUpdateMethodParam2.ToString();
        lPlotEventP3.Text = @"3: " + plotEvent.TargetUpdateMethodParam3.ToString();
        lPlotEventP4.Text = @"4: " + plotEvent.TargetUpdateMethodParam4.ToString();
        lPlotEventP5.Text = @"5: " + plotEvent.TargetUpdateMethodParam5.ToString();
        lPlotEventP6.Text = @"6: " + plotEvent.TargetUpdateMethodParam6.ToString();
        lPlotEventP7.Text = @"7: " + plotEvent.TargetUpdateMethodParam7.ToString();
        lPlotEventP8.Text = @"8: " + plotEvent.TargetUpdateMethodParam8.ToString();
        lPlotEventP9.Text = @"9: " + plotEvent.TargetUpdateMethodParam9.ToString();
        lPlotEventTickets.Text = @"Tickets: " + plotEvent.Tickets.ToString();
        lPlotEventAoE.Text = @"AoE: " + (plotEvent.AeoDiminishing ? "Diminishing" : "Normal");

        ShowSelectedData("plot_events", "id == " + plotEvent.Id.ToString(), "id");
    }

    private void LoadUnitReqs()
    {
        if (AllTableNames.GetValueOrDefault("unit_reqs") != SQLite.SQLiteFileName)
            return;

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
            t.Id = useDbId ? reader.GetUInt32("id") : i;
            t.OwnerId = reader.GetUInt32("owner_id");
            t.OwnerType = reader.GetString("owner_type");
            t.KindId = (GameUnitReqsKind)reader.GetUInt32("kind_id");
            t.Value1 = reader.GetUInt32("value1");
            t.Value2 = reader.GetUInt32("value2");

            AaDb.DbUnitReqs.TryAdd(t.Id, t);
        }
    }

    private void LoadUnitMods()
    {
        if (AllTableNames.GetValueOrDefault("unit_modifiers") != SQLite.SQLiteFileName)
            return;

        using var connection = SQLite.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM unit_modifiers";
        command.Prepare();
        using var sqliteReader = command.ExecuteReader();
        using var reader = new SQLiteWrapperReader(sqliteReader);
        var columnNames = reader.GetColumnNames();
        var useDbId = (columnNames.IndexOf("id") >= 0);
        var i = 0u;

        while (reader.Read())
        {
            var t = new GameUnitModifiers();
            i++;
            t.Id = useDbId ? reader.GetInt64("id") : i;
            t.OwnerId = reader.GetInt64("owner_id");
            t.OwnerType = reader.GetString("owner_type");
            t.UnitAttributeId = (UnitAttribute)reader.GetInt64("unit_attribute_id");
            t.UnitModifierTypeId = reader.GetInt64("unit_modifier_type_id");
            t.Value = reader.GetInt64("value");
            t.LinearLevelBonus = reader.GetInt64("linear_level_bonus");

            AaDb.DbUnitModifiers.TryAdd(t.Id, t);
        }
    }

    private IEnumerable<GameUnitReqs> GetRequirement(string ownerType, long ownerId)
    {
        return AaDb.DbUnitReqs.Values.Where(x => x.OwnerId == ownerId && x.OwnerType == ownerType);
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