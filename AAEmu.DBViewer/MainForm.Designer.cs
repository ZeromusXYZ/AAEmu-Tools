namespace AAEmu.DBViewer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Skill");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbTableNames = new System.Windows.Forms.ListBox();
            this.tcViewer = new System.Windows.Forms.TabControl();
            this.tbTables = new System.Windows.Forms.TabPage();
            this.label29 = new System.Windows.Forms.Label();
            this.tFilterTables = new System.Windows.Forms.TextBox();
            this.lCurrentPakFile = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.btnSimpleSQL = new System.Windows.Forms.Button();
            this.tSimpleSQL = new System.Windows.Forms.TextBox();
            this.dgvSimple = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFindGameClient = new System.Windows.Forms.Button();
            this.btnOpenServerDB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbItemSearchLanguage = new System.Windows.Forms.ComboBox();
            this.tpCurrentRecord = new System.Windows.Forms.TabPage();
            this.labelCurrentDataInfo = new System.Windows.Forms.Label();
            this.dgvCurrentData = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbLocalizer = new System.Windows.Forms.TabPage();
            this.label93 = new System.Windows.Forms.Label();
            this.dgvLocalized = new System.Windows.Forms.DataGridView();
            this.Column48 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column49 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column51 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tSearchLocalized = new System.Windows.Forms.TextBox();
            this.tpMap = new System.Windows.Forms.TabPage();
            this.btnShowEntityAreaShape = new System.Windows.Forms.Button();
            this.btnLoadCustomAAEmuJson = new System.Windows.Forms.Button();
            this.btnLoadCustomPaths = new System.Windows.Forms.Button();
            this.label130 = new System.Windows.Forms.Label();
            this.cbQuestSignSphereSearchShowAll = new System.Windows.Forms.CheckBox();
            this.eQuestSignSphereSearch = new System.Windows.Forms.TextBox();
            this.label129 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.btnFindAllHousing = new System.Windows.Forms.Button();
            this.btnFindAllSubzone = new System.Windows.Forms.Button();
            this.label128 = new System.Windows.Forms.Label();
            this.btnFindAllQuestSpheres = new System.Windows.Forms.Button();
            this.btnFindAllTransferPaths = new System.Windows.Forms.Button();
            this.btnMap = new System.Windows.Forms.Button();
            this.tpV1 = new System.Windows.Forms.TabPage();
            this.btnExportDoodadSpawnData = new System.Windows.Forms.Button();
            this.btnExportNPCSpawnData = new System.Windows.Forms.Button();
            this.btnExportDataForVieweD = new System.Windows.Forms.Button();
            this.lSpace = new System.Windows.Forms.Label();
            this.tpBuffs = new System.Windows.Forms.TabPage();
            this.tcBuffs = new System.Windows.Forms.TabControl();
            this.tcBuffs_Buffs = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.lBuffAddGMCommand = new System.Windows.Forms.Label();
            this.lBuffTags = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.lBuffDuration = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.rtBuffDesc = new System.Windows.Forms.RichTextBox();
            this.buffIcon = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.lBuffName = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.flpBuff = new System.Windows.Forms.FlowLayoutPanel();
            this.lBuffId = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.tcBuffs_Triggers = new System.Windows.Forms.TabPage();
            this.tvBuffTriggers = new System.Windows.Forms.TreeView();
            this.btnSearchBuffs = new System.Windows.Forms.Button();
            this.label115 = new System.Windows.Forms.Label();
            this.tSearchBuffs = new System.Windows.Forms.TextBox();
            this.dgvBuffs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column52 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpDoodads = new System.Windows.Forms.TabPage();
            this.tcDoodads = new System.Windows.Forms.TabControl();
            this.tpDoodadInfo = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.lDoodadGroupRemovedByHouse = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.lDoodadGroupGuardOnFieldTime = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.lDoodadGroupIsExport = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.lDoodadGroupName = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lDoodadSaveIndun = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.lDoodadRestrictZoneID = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.lDoodadNoCollision = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.lDoodadDespawnOnCollision = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.lDoodadGrowthTime = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.lDoodadFactionID = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.lDoodadChildable = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.lDoodadParentable = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.lDoodadLoadModelFromWorld = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.lDoodadForceUpAction = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.lDoodadMarkModel = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.lDoodadClimateID = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.lDoodadCollideVehicle = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.lDoodadCollideShip = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.lDoodadSimRadius = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.lDoodadTargetDecalSize = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.lDoodadUseTargetHighlight = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.lDoodadUseTargetSilhouette = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.lDoodadUseTargetDecal = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.lDoodadShowMinimap = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.lDoodadGroupID = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.lDoodadMilestoneID = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.lDoodadForceToDTopPriority = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.lDoodadUseCreatorFaction = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.lDoodadModelKindID = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.lDoodadMaxTime = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.lDoodadMinTime = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.lDoodadPercent = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.lDoodadMgmtSpawn = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.lDoodadShowName = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.lDoodadOnceOneInteraction = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.lDoodadModel = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.lDoodadID = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.lDoodadOnceOneMan = new System.Windows.Forms.Label();
            this.lDoodadName = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.tpDoodadFunctions = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.lDoodadPhaseFuncsActualType = new System.Windows.Forms.Label();
            this.lDoodadPhaseFuncsActualId = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupIsMsgToZone = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupSoundID = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupSoundTime = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupModel = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupComment = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupPhaseMsg = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupKindID = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupID = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.lDoodadFuncGroupName = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.dgvDoodadFuncGroups = new System.Windows.Forms.DataGridView();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column54 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpDoodadTools = new System.Windows.Forms.TabPage();
            this.btnShowDoodadOnMap = new System.Windows.Forms.Button();
            this.tpDoodadWorkflow = new System.Windows.Forms.TabPage();
            this.cbDoodadWorkflowHideEmpty = new System.Windows.Forms.CheckBox();
            this.tvDoodadDetails = new System.Windows.Forms.TreeView();
            this.btnSearchDoodads = new System.Windows.Forms.Button();
            this.label46 = new System.Windows.Forms.Label();
            this.tSearchDoodads = new System.Windows.Forms.TextBox();
            this.dgvDoodads = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column53 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbFactions = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lFactionHostilePirate = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.lFactionHostileHaranya = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lFactionHostileNuia = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lFactionMotherID = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.lFactionDiplomacyLinkID = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.lFactionIsDiplomacyTarget = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.lFactionGuardLink = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.lFactionAggroLink = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.LFactionPoliticalSystemID = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.lFactionOwnerTypeID = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lFactionOwnerName = new System.Windows.Forms.Label();
            this.lFactionOwnerID = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lFactionName = new System.Windows.Forms.Label();
            this.lFactionID = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.btnFactionsAll = new System.Windows.Forms.Button();
            this.btnSearchFaction = new System.Windows.Forms.Button();
            this.label42 = new System.Windows.Forms.Label();
            this.tSearchFaction = new System.Windows.Forms.TextBox();
            this.dgvFactions = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpItems = new System.Windows.Forms.TabPage();
            this.cbItemSearchRange = new System.Windows.Forms.ComboBox();
            this.label135 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.cbItemSearchItemCategoryTypeList = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.cbItemSearchItemArmorSlotTypeList = new System.Windows.Forms.ComboBox();
            this.btnFindItemSkill = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lItemTags = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.lItemAddGMCommand = new System.Windows.Forms.Label();
            this.itemIcon = new System.Windows.Forms.Label();
            this.rtItemDesc = new System.Windows.Forms.RichTextBox();
            this.lItemLevel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lItemCategory = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lItemName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lItemID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnItemSearch = new System.Windows.Forms.Button();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.Item_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_Name_EN_US = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFindItemInLoot = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tItemSearch = new System.Windows.Forms.TextBox();
            this.tpLoot = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.LLootPackGroupNumber = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.LLootGroupPackID = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.btnLootSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tLootSearch = new System.Windows.Forms.TextBox();
            this.dgvLoot = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpNPCs = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnShowNPCsOnMap = new System.Windows.Forms.Button();
            this.lNPCTags = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.lGMNPCSpawn = new System.Windows.Forms.Label();
            this.lNPCTemplate = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.btnSearchNPC = new System.Windows.Forms.Button();
            this.label39 = new System.Windows.Forms.Label();
            this.tSearchNPC = new System.Windows.Forms.TextBox();
            this.dgvNPCs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpQuests = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label89 = new System.Windows.Forms.Label();
            this.tQuestSearch = new System.Windows.Forms.TextBox();
            this.btnQuestsSearch = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.rtQuestText = new System.Windows.Forms.RichTextBox();
            this.dgvQuests = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnQuestFindRelatedOnMap = new System.Windows.Forms.Button();
            this.cbQuestWorkflowHideEmpty = new System.Windows.Forms.CheckBox();
            this.tvQuestWorkflow = new System.Windows.Forms.TreeView();
            this.tpSkills = new System.Windows.Forms.TabPage();
            this.tcSkillInfo = new System.Windows.Forms.TabControl();
            this.tpSkillInfo = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lSkillTags = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.rtSkillDescription = new System.Windows.Forms.RichTextBox();
            this.lSkillGCD = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lSkillCooldown = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lSkillLabor = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lSkillMana = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lSkillCost = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.skillIcon = new System.Windows.Forms.Label();
            this.lSkillName = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lSkillID = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tpSkillItems = new System.Windows.Forms.TabPage();
            this.dgvSkillProducts = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSkillReagents = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label13 = new System.Windows.Forms.Label();
            this.labelSkillReagents = new System.Windows.Forms.Label();
            this.tpSkillExecution = new System.Windows.Forms.TabPage();
            this.gbSkillPlotEventInfo = new System.Windows.Forms.GroupBox();
            this.lPlotEventTargetUpdate = new System.Windows.Forms.Label();
            this.lPlotEventSourceUpdate = new System.Windows.Forms.Label();
            this.lPlotEventAoE = new System.Windows.Forms.Label();
            this.lPlotEventTickets = new System.Windows.Forms.Label();
            this.lPlotEventP9 = new System.Windows.Forms.Label();
            this.lPlotEventP8 = new System.Windows.Forms.Label();
            this.lPlotEventP7 = new System.Windows.Forms.Label();
            this.lPlotEventP6 = new System.Windows.Forms.Label();
            this.lPlotEventP5 = new System.Windows.Forms.Label();
            this.lPlotEventP4 = new System.Windows.Forms.Label();
            this.lPlotEventP3 = new System.Windows.Forms.Label();
            this.lPlotEventP2 = new System.Windows.Forms.Label();
            this.lPlotEventP1 = new System.Windows.Forms.Label();
            this.tvSkill = new System.Windows.Forms.TreeView();
            this.ilMiniIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnSkillSearch = new System.Windows.Forms.Button();
            this.dgvSkills = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.tSkillSearch = new System.Windows.Forms.TextBox();
            this.tpZones = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.lWorldGroupImageSizeAndPos = new System.Windows.Forms.Label();
            this.lWorldGroupTargetID = new System.Windows.Forms.Label();
            this.lWorldGroupName = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lWorldGroupImageMap = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lWorldGroupSizeAndPos = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnFindDoodadsInZone = new System.Windows.Forms.Button();
            this.labelZoneGroupRestrictions = new System.Windows.Forms.Label();
            this.btnFindQuestsInZone = new System.Windows.Forms.Button();
            this.btnFindNPCsInZone = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.lZoneGroupsSoundPackID = new System.Windows.Forms.Label();
            this.lZoneGroupsDisplayName = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.btnZoneGroupsFreshWaterFish = new System.Windows.Forms.Button();
            this.btnZoneGroupsSaltWaterFish = new System.Windows.Forms.Button();
            this.lZoneGroupsName = new System.Windows.Forms.Label();
            this.lZoneGroupsBuffID = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label01 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lZoneGroupsSizePos = new System.Windows.Forms.Label();
            this.lZoneGroupsPirateDesperado = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lZoneGroupsImageMap = new System.Windows.Forms.Label();
            this.lZoneGroupsFactionChatID = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lZoneGroupsSoundID = new System.Windows.Forms.Label();
            this.lZoneGroupsTargetID = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lZoneInstance = new System.Windows.Forms.Label();
            this.btnFindTransferPathsInZone = new System.Windows.Forms.Button();
            this.lZoneDisplayName = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lZoneFactionID = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lZoneGroupID = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lZoneKey = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lZoneName = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lZoneID = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.btnZonesShowAll = new System.Windows.Forms.Button();
            this.btnSearchZones = new System.Windows.Forms.Button();
            this.dgvZones = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.tZonesSearch = new System.Windows.Forms.TextBox();
            this.tpTrade = new System.Windows.Forms.TabPage();
            this.lTradeRoute = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.lTradeRatio = new System.Windows.Forms.Label();
            this.lTradeProfit = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.lbTradeDestination = new System.Windows.Forms.ListBox();
            this.lbTradeSource = new System.Windows.Forms.ListBox();
            this.openDBDlg = new System.Windows.Forms.OpenFileDialog();
            this.openGamePakFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainFormToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ofdCustomPaths = new System.Windows.Forms.OpenFileDialog();
            this.ofdJsonData = new System.Windows.Forms.OpenFileDialog();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.tcViewer.SuspendLayout();
            this.tbTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimple)).BeginInit();
            this.tpCurrentRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentData)).BeginInit();
            this.tbLocalizer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalized)).BeginInit();
            this.tpMap.SuspendLayout();
            this.tpV1.SuspendLayout();
            this.tpBuffs.SuspendLayout();
            this.tcBuffs.SuspendLayout();
            this.tcBuffs_Buffs.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tcBuffs_Triggers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuffs)).BeginInit();
            this.tpDoodads.SuspendLayout();
            this.tcDoodads.SuspendLayout();
            this.tpDoodadInfo.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tpDoodadFunctions.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoodadFuncGroups)).BeginInit();
            this.tpDoodadTools.SuspendLayout();
            this.tpDoodadWorkflow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoodads)).BeginInit();
            this.tbFactions.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactions)).BeginInit();
            this.tpItems.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            this.tpLoot.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoot)).BeginInit();
            this.tpNPCs.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNPCs)).BeginInit();
            this.tpQuests.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuests)).BeginInit();
            this.tpSkills.SuspendLayout();
            this.tcSkillInfo.SuspendLayout();
            this.tpSkillInfo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tpSkillItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkillProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkillReagents)).BeginInit();
            this.tpSkillExecution.SuspendLayout();
            this.gbSkillPlotEventInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkills)).BeginInit();
            this.tpZones.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZones)).BeginInit();
            this.tpTrade.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTableNames
            // 
            this.lbTableNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTableNames.FormattingEnabled = true;
            this.lbTableNames.Location = new System.Drawing.Point(6, 32);
            this.lbTableNames.Name = "lbTableNames";
            this.lbTableNames.Size = new System.Drawing.Size(270, 433);
            this.lbTableNames.TabIndex = 1;
            this.lbTableNames.SelectedIndexChanged += new System.EventHandler(this.LbTableNames_SelectedIndexChanged);
            // 
            // tcViewer
            // 
            this.tcViewer.Controls.Add(this.tbTables);
            this.tcViewer.Controls.Add(this.tpCurrentRecord);
            this.tcViewer.Controls.Add(this.tbLocalizer);
            this.tcViewer.Controls.Add(this.tpMap);
            this.tcViewer.Controls.Add(this.tpV1);
            this.tcViewer.Controls.Add(this.tpBuffs);
            this.tcViewer.Controls.Add(this.tpDoodads);
            this.tcViewer.Controls.Add(this.tbFactions);
            this.tcViewer.Controls.Add(this.tpItems);
            this.tcViewer.Controls.Add(this.tpLoot);
            this.tcViewer.Controls.Add(this.tpNPCs);
            this.tcViewer.Controls.Add(this.tpQuests);
            this.tcViewer.Controls.Add(this.tpSkills);
            this.tcViewer.Controls.Add(this.tpZones);
            this.tcViewer.Controls.Add(this.tpTrade);
            this.tcViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcViewer.Location = new System.Drawing.Point(0, 0);
            this.tcViewer.Multiline = true;
            this.tcViewer.Name = "tcViewer";
            this.tcViewer.SelectedIndex = 0;
            this.tcViewer.Size = new System.Drawing.Size(934, 511);
            this.tcViewer.TabIndex = 3;
            // 
            // tbTables
            // 
            this.tbTables.BackColor = System.Drawing.Color.Transparent;
            this.tbTables.Controls.Add(this.label29);
            this.tbTables.Controls.Add(this.tFilterTables);
            this.tbTables.Controls.Add(this.lCurrentPakFile);
            this.tbTables.Controls.Add(this.label41);
            this.tbTables.Controls.Add(this.btnSimpleSQL);
            this.tbTables.Controls.Add(this.tSimpleSQL);
            this.tbTables.Controls.Add(this.dgvSimple);
            this.tbTables.Controls.Add(this.label8);
            this.tbTables.Controls.Add(this.btnFindGameClient);
            this.tbTables.Controls.Add(this.btnOpenServerDB);
            this.tbTables.Controls.Add(this.lbTableNames);
            this.tbTables.Controls.Add(this.label2);
            this.tbTables.Controls.Add(this.cbItemSearchLanguage);
            this.tbTables.Location = new System.Drawing.Point(4, 22);
            this.tbTables.Name = "tbTables";
            this.tbTables.Padding = new System.Windows.Forms.Padding(3);
            this.tbTables.Size = new System.Drawing.Size(926, 485);
            this.tbTables.TabIndex = 0;
            this.tbTables.Text = "Tables and Settings";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(8, 9);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(29, 13);
            this.label29.TabIndex = 15;
            this.label29.Text = "Filter";
            // 
            // tFilterTables
            // 
            this.tFilterTables.Location = new System.Drawing.Point(55, 6);
            this.tFilterTables.Name = "tFilterTables";
            this.tFilterTables.Size = new System.Drawing.Size(221, 20);
            this.tFilterTables.TabIndex = 14;
            this.tFilterTables.TextChanged += new System.EventHandler(this.tFilterTables_TextChanged);
            // 
            // lCurrentPakFile
            // 
            this.lCurrentPakFile.AutoSize = true;
            this.lCurrentPakFile.Location = new System.Drawing.Point(282, 61);
            this.lCurrentPakFile.Name = "lCurrentPakFile";
            this.lCurrentPakFile.Size = new System.Drawing.Size(103, 13);
            this.lCurrentPakFile.TabIndex = 13;
            this.lCurrentPakFile.Text = "<no pak file loaded>";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label41.Location = new System.Drawing.Point(282, 89);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(493, 13);
            this.label41.TabIndex = 12;
            this.label41.Text = "You can click a table to preview, or type a simple SQLite statement here. It is p" + "retty slow on large tables";
            // 
            // btnSimpleSQL
            // 
            this.btnSimpleSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSimpleSQL.Location = new System.Drawing.Point(842, 103);
            this.btnSimpleSQL.Name = "btnSimpleSQL";
            this.btnSimpleSQL.Size = new System.Drawing.Size(75, 23);
            this.btnSimpleSQL.TabIndex = 11;
            this.btnSimpleSQL.Text = "Run SQL";
            this.btnSimpleSQL.UseVisualStyleBackColor = true;
            this.btnSimpleSQL.Click += new System.EventHandler(this.BtnSimpleSQL_Click);
            // 
            // tSimpleSQL
            // 
            this.tSimpleSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tSimpleSQL.Location = new System.Drawing.Point(282, 105);
            this.tSimpleSQL.Name = "tSimpleSQL";
            this.tSimpleSQL.Size = new System.Drawing.Size(554, 20);
            this.tSimpleSQL.TabIndex = 10;
            this.tSimpleSQL.TextChanged += new System.EventHandler(this.TSimpleSQL_TextChanged);
            this.tSimpleSQL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TSimpleSQL_KeyDown);
            // 
            // dgvSimple
            // 
            this.dgvSimple.AllowUserToAddRows = false;
            this.dgvSimple.AllowUserToDeleteRows = false;
            this.dgvSimple.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSimple.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSimple.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSimple.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSimple.Location = new System.Drawing.Point(282, 131);
            this.dgvSimple.MultiSelect = false;
            this.dgvSimple.Name = "dgvSimple";
            this.dgvSimple.RowHeadersVisible = false;
            this.dgvSimple.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSimple.Size = new System.Drawing.Size(635, 338);
            this.dgvSimple.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(474, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(208, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Used for loading icons and map/zone data";
            // 
            // btnFindGameClient
            // 
            this.btnFindGameClient.Location = new System.Drawing.Point(282, 35);
            this.btnFindGameClient.Name = "btnFindGameClient";
            this.btnFindGameClient.Size = new System.Drawing.Size(186, 23);
            this.btnFindGameClient.TabIndex = 7;
            this.btnFindGameClient.Text = "Locate Client game_pak";
            this.btnFindGameClient.UseVisualStyleBackColor = true;
            this.btnFindGameClient.Click += new System.EventHandler(this.BtnFindGameClient_Click);
            // 
            // btnOpenServerDB
            // 
            this.btnOpenServerDB.Location = new System.Drawing.Point(282, 6);
            this.btnOpenServerDB.Name = "btnOpenServerDB";
            this.btnOpenServerDB.Size = new System.Drawing.Size(186, 23);
            this.btnOpenServerDB.TabIndex = 6;
            this.btnOpenServerDB.Text = "Open (server) DB";
            this.btnOpenServerDB.UseVisualStyleBackColor = true;
            this.btnOpenServerDB.Click += new System.EventHandler(this.BtnOpenServerDB_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(781, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Language";
            // 
            // cbItemSearchLanguage
            // 
            this.cbItemSearchLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbItemSearchLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbItemSearchLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbItemSearchLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemSearchLanguage.FormattingEnabled = true;
            this.cbItemSearchLanguage.Items.AddRange(new object[] { "en_us", "ru", "ko", "zh_cn", "zh_tw", "de", "fr", "ja" });
            this.cbItemSearchLanguage.Location = new System.Drawing.Point(842, 6);
            this.cbItemSearchLanguage.Name = "cbItemSearchLanguage";
            this.cbItemSearchLanguage.Size = new System.Drawing.Size(75, 21);
            this.cbItemSearchLanguage.TabIndex = 5;
            this.cbItemSearchLanguage.SelectedIndexChanged += new System.EventHandler(this.CbItemSearchLanguage_SelectedIndexChanged);
            // 
            // tpCurrentRecord
            // 
            this.tpCurrentRecord.Controls.Add(this.labelCurrentDataInfo);
            this.tpCurrentRecord.Controls.Add(this.dgvCurrentData);
            this.tpCurrentRecord.Location = new System.Drawing.Point(4, 22);
            this.tpCurrentRecord.Name = "tpCurrentRecord";
            this.tpCurrentRecord.Size = new System.Drawing.Size(926, 485);
            this.tpCurrentRecord.TabIndex = 4;
            this.tpCurrentRecord.Text = "Selected Data";
            this.tpCurrentRecord.UseVisualStyleBackColor = true;
            // 
            // labelCurrentDataInfo
            // 
            this.labelCurrentDataInfo.AutoSize = true;
            this.labelCurrentDataInfo.Location = new System.Drawing.Point(8, 10);
            this.labelCurrentDataInfo.Name = "labelCurrentDataInfo";
            this.labelCurrentDataInfo.Size = new System.Drawing.Size(87, 13);
            this.labelCurrentDataInfo.TabIndex = 4;
            this.labelCurrentDataInfo.Text = "Nothing selected";
            // 
            // dgvCurrentData
            // 
            this.dgvCurrentData.AllowUserToAddRows = false;
            this.dgvCurrentData.AllowUserToDeleteRows = false;
            this.dgvCurrentData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCurrentData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCurrentData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCurrentData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn5, this.Column12 });
            this.dgvCurrentData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCurrentData.Location = new System.Drawing.Point(8, 26);
            this.dgvCurrentData.MultiSelect = false;
            this.dgvCurrentData.Name = "dgvCurrentData";
            this.dgvCurrentData.RowHeadersVisible = false;
            this.dgvCurrentData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCurrentData.Size = new System.Drawing.Size(909, 453);
            this.dgvCurrentData.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.FillWeight = 75F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Field Name";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 85;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Value";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Localized";
            this.Column12.Name = "Column12";
            // 
            // tbLocalizer
            // 
            this.tbLocalizer.Controls.Add(this.label93);
            this.tbLocalizer.Controls.Add(this.dgvLocalized);
            this.tbLocalizer.Controls.Add(this.tSearchLocalized);
            this.tbLocalizer.Location = new System.Drawing.Point(4, 22);
            this.tbLocalizer.Name = "tbLocalizer";
            this.tbLocalizer.Padding = new System.Windows.Forms.Padding(3);
            this.tbLocalizer.Size = new System.Drawing.Size(926, 485);
            this.tbLocalizer.TabIndex = 10;
            this.tbLocalizer.Text = "Localizer";
            this.tbLocalizer.UseVisualStyleBackColor = true;
            this.tbLocalizer.Enter += new System.EventHandler(this.tbLocalizer_Enter);
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(5, 13);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(501, 13);
            this.label93.TabIndex = 12;
            this.label93.Text = "Enter (partial) text here to search inside the localized texts. Enclose in =\'s fo" + "r exact search. Max 50 results";
            // 
            // dgvLocalized
            // 
            this.dgvLocalized.AllowUserToAddRows = false;
            this.dgvLocalized.AllowUserToDeleteRows = false;
            this.dgvLocalized.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLocalized.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLocalized.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLocalized.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column48, this.Column49, this.Column51, this.Column50 });
            this.dgvLocalized.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvLocalized.Location = new System.Drawing.Point(8, 55);
            this.dgvLocalized.MultiSelect = false;
            this.dgvLocalized.Name = "dgvLocalized";
            this.dgvLocalized.RowHeadersVisible = false;
            this.dgvLocalized.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvLocalized.Size = new System.Drawing.Size(909, 424);
            this.dgvLocalized.TabIndex = 11;
            // 
            // Column48
            // 
            this.Column48.FillWeight = 15F;
            this.Column48.HeaderText = "Table";
            this.Column48.Name = "Column48";
            // 
            // Column49
            // 
            this.Column49.FillWeight = 15F;
            this.Column49.HeaderText = "Field";
            this.Column49.Name = "Column49";
            // 
            // Column51
            // 
            this.Column51.FillWeight = 10F;
            this.Column51.HeaderText = "Index";
            this.Column51.Name = "Column51";
            // 
            // Column50
            // 
            this.Column50.FillWeight = 60F;
            this.Column50.HeaderText = "Value";
            this.Column50.Name = "Column50";
            // 
            // tSearchLocalized
            // 
            this.tSearchLocalized.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tSearchLocalized.Location = new System.Drawing.Point(8, 29);
            this.tSearchLocalized.Name = "tSearchLocalized";
            this.tSearchLocalized.Size = new System.Drawing.Size(364, 20);
            this.tSearchLocalized.TabIndex = 10;
            this.tSearchLocalized.TextChanged += new System.EventHandler(this.tSearchLocalized_TextChanged);
            // 
            // tpMap
            // 
            this.tpMap.Controls.Add(this.btnShowEntityAreaShape);
            this.tpMap.Controls.Add(this.btnLoadCustomAAEmuJson);
            this.tpMap.Controls.Add(this.btnLoadCustomPaths);
            this.tpMap.Controls.Add(this.label130);
            this.tpMap.Controls.Add(this.cbQuestSignSphereSearchShowAll);
            this.tpMap.Controls.Add(this.eQuestSignSphereSearch);
            this.tpMap.Controls.Add(this.label129);
            this.tpMap.Controls.Add(this.label131);
            this.tpMap.Controls.Add(this.btnFindAllHousing);
            this.tpMap.Controls.Add(this.btnFindAllSubzone);
            this.tpMap.Controls.Add(this.label128);
            this.tpMap.Controls.Add(this.btnFindAllQuestSpheres);
            this.tpMap.Controls.Add(this.btnFindAllTransferPaths);
            this.tpMap.Controls.Add(this.btnMap);
            this.tpMap.Location = new System.Drawing.Point(4, 22);
            this.tpMap.Name = "tpMap";
            this.tpMap.Padding = new System.Windows.Forms.Padding(3);
            this.tpMap.Size = new System.Drawing.Size(926, 485);
            this.tpMap.TabIndex = 13;
            this.tpMap.Text = "Map";
            this.tpMap.UseVisualStyleBackColor = true;
            // 
            // btnShowEntityAreaShape
            // 
            this.btnShowEntityAreaShape.Location = new System.Drawing.Point(8, 345);
            this.btnShowEntityAreaShape.Name = "btnShowEntityAreaShape";
            this.btnShowEntityAreaShape.Size = new System.Drawing.Size(230, 23);
            this.btnShowEntityAreaShape.TabIndex = 50;
            this.btnShowEntityAreaShape.Text = "Show All Entity AreaShape";
            this.btnShowEntityAreaShape.UseVisualStyleBackColor = true;
            this.btnShowEntityAreaShape.Click += new System.EventHandler(this.btnShowEntityAreaShape_Click);
            // 
            // btnLoadCustomAAEmuJson
            // 
            this.btnLoadCustomAAEmuJson.Location = new System.Drawing.Point(8, 297);
            this.btnLoadCustomAAEmuJson.Name = "btnLoadCustomAAEmuJson";
            this.btnLoadCustomAAEmuJson.Size = new System.Drawing.Size(230, 23);
            this.btnLoadCustomAAEmuJson.TabIndex = 49;
            this.btnLoadCustomAAEmuJson.Text = "Load custom AAEmu json entity data";
            this.btnLoadCustomAAEmuJson.UseVisualStyleBackColor = true;
            this.btnLoadCustomAAEmuJson.Click += new System.EventHandler(this.btnLoadCustomAAEmuJson_Click);
            // 
            // btnLoadCustomPaths
            // 
            this.btnLoadCustomPaths.Location = new System.Drawing.Point(8, 234);
            this.btnLoadCustomPaths.Name = "btnLoadCustomPaths";
            this.btnLoadCustomPaths.Size = new System.Drawing.Size(230, 23);
            this.btnLoadCustomPaths.TabIndex = 48;
            this.btnLoadCustomPaths.Text = "Load custom entity path";
            this.btnLoadCustomPaths.UseVisualStyleBackColor = true;
            this.btnLoadCustomPaths.Click += new System.EventHandler(this.btnLoadCustomPaths_Click);
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(242, 105);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(46, 13);
            this.label130.TabIndex = 47;
            this.label130.Text = "Filter by:";
            // 
            // cbQuestSignSphereSearchShowAll
            // 
            this.cbQuestSignSphereSearchShowAll.AutoSize = true;
            this.cbQuestSignSphereSearchShowAll.Location = new System.Drawing.Point(471, 104);
            this.cbQuestSignSphereSearchShowAll.Name = "cbQuestSignSphereSearchShowAll";
            this.cbQuestSignSphereSearchShowAll.Size = new System.Drawing.Size(106, 17);
            this.cbQuestSignSphereSearchShowAll.TabIndex = 46;
            this.cbQuestSignSphereSearchShowAll.Text = "Also show others";
            this.cbQuestSignSphereSearchShowAll.UseVisualStyleBackColor = true;
            // 
            // eQuestSignSphereSearch
            // 
            this.eQuestSignSphereSearch.Location = new System.Drawing.Point(294, 102);
            this.eQuestSignSphereSearch.Name = "eQuestSignSphereSearch";
            this.eQuestSignSphereSearch.Size = new System.Drawing.Size(171, 20);
            this.eQuestSignSphereSearch.TabIndex = 45;
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(242, 170);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(108, 13);
            this.label129.TabIndex = 44;
            this.label129.Text = "Shows housing areas";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(242, 170);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(111, 13);
            this.label131.TabIndex = 44;
            this.label131.Text = "Shows subzone areas";
            // 
            // btnFindAllHousing
            // 
            this.btnFindAllHousing.Location = new System.Drawing.Point(6, 165);
            this.btnFindAllHousing.Name = "btnFindAllHousing";
            this.btnFindAllHousing.Size = new System.Drawing.Size(230, 23);
            this.btnFindAllHousing.TabIndex = 43;
            this.btnFindAllHousing.Text = "Show All Housing";
            this.btnFindAllHousing.UseVisualStyleBackColor = true;
            this.btnFindAllHousing.Click += new System.EventHandler(this.btnFindAllHousing_Click);
            // 
            // btnFindAllSubzone
            // 
            this.btnFindAllSubzone.Location = new System.Drawing.Point(6, 185);
            this.btnFindAllSubzone.Name = "btnFindAllSubzone";
            this.btnFindAllSubzone.Size = new System.Drawing.Size(230, 23);
            this.btnFindAllSubzone.TabIndex = 50;
            this.btnFindAllSubzone.Text = "Show All Subzone";
            this.btnFindAllSubzone.UseVisualStyleBackColor = true;
            this.btnFindAllSubzone.Click += new System.EventHandler(this.btnFindAllSubZone_Click);
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(244, 50);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(253, 13);
            this.label128.TabIndex = 42;
            this.label128.Text = "Routes of Airships, Carriage and other fixed pathings";
            // 
            // btnFindAllQuestSpheres
            // 
            this.btnFindAllQuestSpheres.Location = new System.Drawing.Point(6, 100);
            this.btnFindAllQuestSpheres.Name = "btnFindAllQuestSpheres";
            this.btnFindAllQuestSpheres.Size = new System.Drawing.Size(230, 23);
            this.btnFindAllQuestSpheres.TabIndex = 40;
            this.btnFindAllQuestSpheres.Text = "Show All Quest Spheres";
            this.btnFindAllQuestSpheres.UseVisualStyleBackColor = true;
            this.btnFindAllQuestSpheres.Click += new System.EventHandler(this.btnFindAllQuestSpheres_Click);
            // 
            // btnFindAllTransferPaths
            // 
            this.btnFindAllTransferPaths.Location = new System.Drawing.Point(8, 45);
            this.btnFindAllTransferPaths.Name = "btnFindAllTransferPaths";
            this.btnFindAllTransferPaths.Size = new System.Drawing.Size(230, 23);
            this.btnFindAllTransferPaths.TabIndex = 39;
            this.btnFindAllTransferPaths.Text = "Show All Transfer Paths";
            this.btnFindAllTransferPaths.UseVisualStyleBackColor = true;
            this.btnFindAllTransferPaths.Click += new System.EventHandler(this.btnFindAllTransferPaths_Click);
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(8, 6);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(109, 23);
            this.btnMap.TabIndex = 3;
            this.btnMap.Text = "Open Map";
            this.btnMap.UseVisualStyleBackColor = true;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // tpV1
            // 
            this.tpV1.Controls.Add(this.btnExportDoodadSpawnData);
            this.tpV1.Controls.Add(this.btnExportNPCSpawnData);
            this.tpV1.Controls.Add(this.btnExportDataForVieweD);
            this.tpV1.Controls.Add(this.lSpace);
            this.tpV1.Location = new System.Drawing.Point(4, 22);
            this.tpV1.Name = "tpV1";
            this.tpV1.Padding = new System.Windows.Forms.Padding(3);
            this.tpV1.Size = new System.Drawing.Size(926, 485);
            this.tpV1.TabIndex = 11;
            this.tpV1.Text = "|";
            this.tpV1.UseVisualStyleBackColor = true;
            // 
            // btnExportDoodadSpawnData
            // 
            this.btnExportDoodadSpawnData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportDoodadSpawnData.Location = new System.Drawing.Point(8, 398);
            this.btnExportDoodadSpawnData.Name = "btnExportDoodadSpawnData";
            this.btnExportDoodadSpawnData.Size = new System.Drawing.Size(166, 23);
            this.btnExportDoodadSpawnData.TabIndex = 4;
            this.btnExportDoodadSpawnData.Text = "Export Doodad Spawn Data";
            this.btnExportDoodadSpawnData.UseVisualStyleBackColor = true;
            this.btnExportDoodadSpawnData.Click += new System.EventHandler(this.btnExportDoodadSpawnData_Click);
            // 
            // btnExportNPCSpawnData
            // 
            this.btnExportNPCSpawnData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportNPCSpawnData.Location = new System.Drawing.Point(8, 427);
            this.btnExportNPCSpawnData.Name = "btnExportNPCSpawnData";
            this.btnExportNPCSpawnData.Size = new System.Drawing.Size(166, 23);
            this.btnExportNPCSpawnData.TabIndex = 3;
            this.btnExportNPCSpawnData.Text = "Export NPC Spawn Data";
            this.btnExportNPCSpawnData.UseVisualStyleBackColor = true;
            this.btnExportNPCSpawnData.Click += new System.EventHandler(this.btnExportNPCSpawnData_Click);
            // 
            // btnExportDataForVieweD
            // 
            this.btnExportDataForVieweD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportDataForVieweD.Location = new System.Drawing.Point(8, 456);
            this.btnExportDataForVieweD.Name = "btnExportDataForVieweD";
            this.btnExportDataForVieweD.Size = new System.Drawing.Size(166, 23);
            this.btnExportDataForVieweD.TabIndex = 1;
            this.btnExportDataForVieweD.Text = "Export Data for VieweD";
            this.btnExportDataForVieweD.UseVisualStyleBackColor = true;
            this.btnExportDataForVieweD.Click += new System.EventHandler(this.btnExportDataForVieweD_Click);
            // 
            // lSpace
            // 
            this.lSpace.AutoSize = true;
            this.lSpace.Location = new System.Drawing.Point(69, 33);
            this.lSpace.Name = "lSpace";
            this.lSpace.Size = new System.Drawing.Size(127, 13);
            this.lSpace.TabIndex = 0;
            this.lSpace.Text = "Space, the final divider ...";
            // 
            // tpBuffs
            // 
            this.tpBuffs.Controls.Add(this.tcBuffs);
            this.tpBuffs.Controls.Add(this.btnSearchBuffs);
            this.tpBuffs.Controls.Add(this.label115);
            this.tpBuffs.Controls.Add(this.tSearchBuffs);
            this.tpBuffs.Controls.Add(this.dgvBuffs);
            this.tpBuffs.Location = new System.Drawing.Point(4, 22);
            this.tpBuffs.Name = "tpBuffs";
            this.tpBuffs.Size = new System.Drawing.Size(926, 485);
            this.tpBuffs.TabIndex = 12;
            this.tpBuffs.Text = "Buffs";
            this.tpBuffs.UseVisualStyleBackColor = true;
            // 
            // tcBuffs
            // 
            this.tcBuffs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcBuffs.Controls.Add(this.tcBuffs_Buffs);
            this.tcBuffs.Controls.Add(this.tcBuffs_Triggers);
            this.tcBuffs.Location = new System.Drawing.Point(382, 8);
            this.tcBuffs.Name = "tcBuffs";
            this.tcBuffs.SelectedIndex = 0;
            this.tcBuffs.Size = new System.Drawing.Size(543, 468);
            this.tcBuffs.TabIndex = 13;
            // 
            // tcBuffs_Buffs
            // 
            this.tcBuffs_Buffs.Controls.Add(this.groupBox14);
            this.tcBuffs_Buffs.Location = new System.Drawing.Point(4, 4);
            this.tcBuffs_Buffs.Name = "tcBuffs_Buffs";
            this.tcBuffs_Buffs.Padding = new System.Windows.Forms.Padding(3);
            this.tcBuffs_Buffs.Size = new System.Drawing.Size(535, 442);
            this.tcBuffs_Buffs.TabIndex = 0;
            this.tcBuffs_Buffs.Text = "Buff";
            this.tcBuffs_Buffs.UseVisualStyleBackColor = true;
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.groupBox14.Controls.Add(this.lBuffAddGMCommand);
            this.groupBox14.Controls.Add(this.lBuffTags);
            this.groupBox14.Controls.Add(this.label117);
            this.groupBox14.Controls.Add(this.lBuffDuration);
            this.groupBox14.Controls.Add(this.label119);
            this.groupBox14.Controls.Add(this.label109);
            this.groupBox14.Controls.Add(this.rtBuffDesc);
            this.groupBox14.Controls.Add(this.buffIcon);
            this.groupBox14.Controls.Add(this.label97);
            this.groupBox14.Controls.Add(this.lBuffName);
            this.groupBox14.Controls.Add(this.label105);
            this.groupBox14.Controls.Add(this.flpBuff);
            this.groupBox14.Controls.Add(this.lBuffId);
            this.groupBox14.Controls.Add(this.label113);
            this.groupBox14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.groupBox14.Location = new System.Drawing.Point(0, 0);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(533, 467);
            this.groupBox14.TabIndex = 12;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Buff Info";
            // 
            // lBuffAddGMCommand
            // 
            this.lBuffAddGMCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lBuffAddGMCommand.AutoSize = true;
            this.lBuffAddGMCommand.Location = new System.Drawing.Point(6, 447);
            this.lBuffAddGMCommand.Name = "lBuffAddGMCommand";
            this.lBuffAddGMCommand.Size = new System.Drawing.Size(71, 13);
            this.lBuffAddGMCommand.TabIndex = 19;
            this.lBuffAddGMCommand.Text = "/addbuff <id>";
            // 
            // lBuffTags
            // 
            this.lBuffTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lBuffTags.AutoSize = true;
            this.lBuffTags.Location = new System.Drawing.Point(54, 422);
            this.lBuffTags.Name = "lBuffTags";
            this.lBuffTags.Size = new System.Drawing.Size(25, 13);
            this.lBuffTags.TabIndex = 18;
            this.lBuffTags.Text = "???";
            // 
            // label117
            // 
            this.label117.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(6, 422);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(31, 13);
            this.label117.TabIndex = 17;
            this.label117.Text = "Tags";
            // 
            // lBuffDuration
            // 
            this.lBuffDuration.AutoSize = true;
            this.lBuffDuration.Location = new System.Drawing.Point(58, 49);
            this.lBuffDuration.Name = "lBuffDuration";
            this.lBuffDuration.Size = new System.Drawing.Size(25, 13);
            this.lBuffDuration.TabIndex = 16;
            this.lBuffDuration.Text = "???";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(6, 49);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(47, 13);
            this.label119.TabIndex = 15;
            this.label119.Text = "Duration";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(6, 69);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(60, 13);
            this.label109.TabIndex = 14;
            this.label109.Text = "Description";
            // 
            // rtBuffDesc
            // 
            this.rtBuffDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rtBuffDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.rtBuffDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.rtBuffDesc.Location = new System.Drawing.Point(9, 85);
            this.rtBuffDesc.Name = "rtBuffDesc";
            this.rtBuffDesc.Size = new System.Drawing.Size(448, 64);
            this.rtBuffDesc.TabIndex = 13;
            this.rtBuffDesc.Text = "";
            // 
            // buffIcon
            // 
            this.buffIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buffIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.buffIcon.Location = new System.Drawing.Point(463, 85);
            this.buffIcon.Name = "buffIcon";
            this.buffIcon.Size = new System.Drawing.Size(64, 64);
            this.buffIcon.TabIndex = 12;
            this.buffIcon.Text = "???";
            this.buffIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(6, 152);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(68, 13);
            this.label97.TabIndex = 5;
            this.label97.Text = "Other Values";
            // 
            // lBuffName
            // 
            this.lBuffName.AutoSize = true;
            this.lBuffName.Location = new System.Drawing.Point(58, 31);
            this.lBuffName.Name = "lBuffName";
            this.lBuffName.Size = new System.Drawing.Size(31, 13);
            this.lBuffName.TabIndex = 4;
            this.lBuffName.Text = "none";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(6, 31);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(35, 13);
            this.label105.TabIndex = 3;
            this.label105.Text = "Name";
            // 
            // flpBuff
            // 
            this.flpBuff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.flpBuff.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpBuff.Location = new System.Drawing.Point(9, 168);
            this.flpBuff.Name = "flpBuff";
            this.flpBuff.Size = new System.Drawing.Size(518, 251);
            this.flpBuff.TabIndex = 2;
            // 
            // lBuffId
            // 
            this.lBuffId.AutoSize = true;
            this.lBuffId.Location = new System.Drawing.Point(58, 16);
            this.lBuffId.Name = "lBuffId";
            this.lBuffId.Size = new System.Drawing.Size(13, 13);
            this.lBuffId.TabIndex = 1;
            this.lBuffId.Text = "0";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(6, 16);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(33, 13);
            this.label113.TabIndex = 0;
            this.label113.Text = "Index";
            // 
            // tcBuffs_Triggers
            // 
            this.tcBuffs_Triggers.Controls.Add(this.tvBuffTriggers);
            this.tcBuffs_Triggers.Location = new System.Drawing.Point(4, 4);
            this.tcBuffs_Triggers.Name = "tcBuffs_Triggers";
            this.tcBuffs_Triggers.Padding = new System.Windows.Forms.Padding(3);
            this.tcBuffs_Triggers.Size = new System.Drawing.Size(535, 442);
            this.tcBuffs_Triggers.TabIndex = 1;
            this.tcBuffs_Triggers.Text = "Triggers";
            this.tcBuffs_Triggers.UseVisualStyleBackColor = true;
            // 
            // tvBuffTriggers
            // 
            this.tvBuffTriggers.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tvBuffTriggers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvBuffTriggers.ItemHeight = 24;
            this.tvBuffTriggers.Location = new System.Drawing.Point(0, 0);
            this.tvBuffTriggers.Name = "tvBuffTriggers";
            this.tvBuffTriggers.Size = new System.Drawing.Size(534, 441);
            this.tvBuffTriggers.TabIndex = 0;
            // 
            // btnSearchBuffs
            // 
            this.btnSearchBuffs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchBuffs.Enabled = false;
            this.btnSearchBuffs.Location = new System.Drawing.Point(298, 7);
            this.btnSearchBuffs.Name = "btnSearchBuffs";
            this.btnSearchBuffs.Size = new System.Drawing.Size(79, 23);
            this.btnSearchBuffs.TabIndex = 11;
            this.btnSearchBuffs.Text = "Search";
            this.btnSearchBuffs.UseVisualStyleBackColor = true;
            this.btnSearchBuffs.Click += new System.EventHandler(this.btnSearchBuffs_Click);
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(8, 12);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(120, 13);
            this.label115.TabIndex = 10;
            this.label115.Text = "Search Buff Name or ID";
            // 
            // tSearchBuffs
            // 
            this.tSearchBuffs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tSearchBuffs.Location = new System.Drawing.Point(134, 9);
            this.tSearchBuffs.Name = "tSearchBuffs";
            this.tSearchBuffs.Size = new System.Drawing.Size(158, 20);
            this.tSearchBuffs.TabIndex = 9;
            this.tSearchBuffs.TextChanged += new System.EventHandler(this.tSearchBuffs_TextChanged);
            this.tSearchBuffs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tSearchBuffs_KeyDown);
            // 
            // dgvBuffs
            // 
            this.dgvBuffs.AllowUserToAddRows = false;
            this.dgvBuffs.AllowUserToDeleteRows = false;
            this.dgvBuffs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvBuffs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBuffs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvBuffs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuffs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn21, this.dataGridViewTextBoxColumn24, this.Column52 });
            this.dgvBuffs.Location = new System.Drawing.Point(8, 40);
            this.dgvBuffs.Name = "dgvBuffs";
            this.dgvBuffs.ReadOnly = true;
            this.dgvBuffs.RowHeadersVisible = false;
            this.dgvBuffs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBuffs.Size = new System.Drawing.Size(370, 437);
            this.dgvBuffs.TabIndex = 8;
            this.dgvBuffs.SelectionChanged += new System.EventHandler(this.dgvBuffs_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.dataGridViewTextBoxColumn21.HeaderText = "ID";
            this.dataGridViewTextBoxColumn21.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Width = 50;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn24.HeaderText = "Name";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            // 
            // Column52
            // 
            this.Column52.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Column52.HeaderText = "Duration";
            this.Column52.MinimumWidth = 75;
            this.Column52.Name = "Column52";
            this.Column52.ReadOnly = true;
            this.Column52.Width = 75;
            // 
            // tpDoodads
            // 
            this.tpDoodads.Controls.Add(this.tcDoodads);
            this.tpDoodads.Controls.Add(this.btnSearchDoodads);
            this.tpDoodads.Controls.Add(this.label46);
            this.tpDoodads.Controls.Add(this.tSearchDoodads);
            this.tpDoodads.Controls.Add(this.dgvDoodads);
            this.tpDoodads.Location = new System.Drawing.Point(4, 22);
            this.tpDoodads.Name = "tpDoodads";
            this.tpDoodads.Padding = new System.Windows.Forms.Padding(3);
            this.tpDoodads.Size = new System.Drawing.Size(926, 485);
            this.tpDoodads.TabIndex = 8;
            this.tpDoodads.Text = "Doodads";
            this.tpDoodads.UseVisualStyleBackColor = true;
            // 
            // tcDoodads
            // 
            this.tcDoodads.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcDoodads.Controls.Add(this.tpDoodadInfo);
            this.tcDoodads.Controls.Add(this.tpDoodadFunctions);
            this.tcDoodads.Controls.Add(this.tpDoodadTools);
            this.tcDoodads.Controls.Add(this.tpDoodadWorkflow);
            this.tcDoodads.Dock = System.Windows.Forms.DockStyle.Right;
            this.tcDoodads.Location = new System.Drawing.Point(581, 3);
            this.tcDoodads.Multiline = true;
            this.tcDoodads.Name = "tcDoodads";
            this.tcDoodads.SelectedIndex = 0;
            this.tcDoodads.Size = new System.Drawing.Size(342, 479);
            this.tcDoodads.TabIndex = 15;
            // 
            // tpDoodadInfo
            // 
            this.tpDoodadInfo.Controls.Add(this.groupBox9);
            this.tpDoodadInfo.Controls.Add(this.groupBox8);
            this.tpDoodadInfo.Location = new System.Drawing.Point(4, 4);
            this.tpDoodadInfo.Name = "tpDoodadInfo";
            this.tpDoodadInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpDoodadInfo.Size = new System.Drawing.Size(334, 453);
            this.tpDoodadInfo.TabIndex = 1;
            this.tpDoodadInfo.Text = "Doodad";
            this.tpDoodadInfo.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.lDoodadGroupRemovedByHouse);
            this.groupBox9.Controls.Add(this.label77);
            this.groupBox9.Controls.Add(this.lDoodadGroupGuardOnFieldTime);
            this.groupBox9.Controls.Add(this.label72);
            this.groupBox9.Controls.Add(this.lDoodadGroupIsExport);
            this.groupBox9.Controls.Add(this.label56);
            this.groupBox9.Controls.Add(this.label62);
            this.groupBox9.Controls.Add(this.lDoodadGroupName);
            this.groupBox9.Controls.Add(this.label68);
            this.groupBox9.Controls.Add(this.label70);
            this.groupBox9.Location = new System.Drawing.Point(3, 365);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(328, 80);
            this.groupBox9.TabIndex = 13;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Doodad Group Info";
            // 
            // lDoodadGroupRemovedByHouse
            // 
            this.lDoodadGroupRemovedByHouse.AutoSize = true;
            this.lDoodadGroupRemovedByHouse.Location = new System.Drawing.Point(116, 55);
            this.lDoodadGroupRemovedByHouse.Name = "lDoodadGroupRemovedByHouse";
            this.lDoodadGroupRemovedByHouse.Size = new System.Drawing.Size(13, 13);
            this.lDoodadGroupRemovedByHouse.TabIndex = 42;
            this.lDoodadGroupRemovedByHouse.Text = "0";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(6, 55);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(102, 13);
            this.label77.TabIndex = 41;
            this.label77.Text = "Removed By House";
            // 
            // lDoodadGroupGuardOnFieldTime
            // 
            this.lDoodadGroupGuardOnFieldTime.AutoSize = true;
            this.lDoodadGroupGuardOnFieldTime.Location = new System.Drawing.Point(116, 42);
            this.lDoodadGroupGuardOnFieldTime.Name = "lDoodadGroupGuardOnFieldTime";
            this.lDoodadGroupGuardOnFieldTime.Size = new System.Drawing.Size(13, 13);
            this.lDoodadGroupGuardOnFieldTime.TabIndex = 40;
            this.lDoodadGroupGuardOnFieldTime.Text = "0";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(6, 42);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(104, 13);
            this.label72.TabIndex = 39;
            this.label72.Text = "Guard On Field Time";
            // 
            // lDoodadGroupIsExport
            // 
            this.lDoodadGroupIsExport.AutoSize = true;
            this.lDoodadGroupIsExport.Location = new System.Drawing.Point(116, 29);
            this.lDoodadGroupIsExport.Name = "lDoodadGroupIsExport";
            this.lDoodadGroupIsExport.Size = new System.Drawing.Size(13, 13);
            this.lDoodadGroupIsExport.TabIndex = 38;
            this.lDoodadGroupIsExport.Text = "0";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(6, 29);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(48, 13);
            this.label56.TabIndex = 37;
            this.label56.Text = "Is Export";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(92, 82);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(13, 13);
            this.label62.TabIndex = 35;
            this.label62.Text = "0";
            // 
            // lDoodadGroupName
            // 
            this.lDoodadGroupName.AutoSize = true;
            this.lDoodadGroupName.Location = new System.Drawing.Point(116, 16);
            this.lDoodadGroupName.Name = "lDoodadGroupName";
            this.lDoodadGroupName.Size = new System.Drawing.Size(43, 13);
            this.lDoodadGroupName.TabIndex = 36;
            this.lDoodadGroupName.Text = "<none>";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(6, 82);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(52, 13);
            this.label68.TabIndex = 34;
            this.label68.Text = "Target ID";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(6, 16);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(35, 13);
            this.label70.TabIndex = 34;
            this.label70.Text = "Name";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lDoodadSaveIndun);
            this.groupBox8.Controls.Add(this.label55);
            this.groupBox8.Controls.Add(this.lDoodadRestrictZoneID);
            this.groupBox8.Controls.Add(this.label126);
            this.groupBox8.Controls.Add(this.lDoodadNoCollision);
            this.groupBox8.Controls.Add(this.label124);
            this.groupBox8.Controls.Add(this.lDoodadDespawnOnCollision);
            this.groupBox8.Controls.Add(this.label122);
            this.groupBox8.Controls.Add(this.lDoodadGrowthTime);
            this.groupBox8.Controls.Add(this.label120);
            this.groupBox8.Controls.Add(this.lDoodadFactionID);
            this.groupBox8.Controls.Add(this.label118);
            this.groupBox8.Controls.Add(this.lDoodadChildable);
            this.groupBox8.Controls.Add(this.label116);
            this.groupBox8.Controls.Add(this.lDoodadParentable);
            this.groupBox8.Controls.Add(this.label114);
            this.groupBox8.Controls.Add(this.lDoodadLoadModelFromWorld);
            this.groupBox8.Controls.Add(this.label112);
            this.groupBox8.Controls.Add(this.lDoodadForceUpAction);
            this.groupBox8.Controls.Add(this.label110);
            this.groupBox8.Controls.Add(this.lDoodadMarkModel);
            this.groupBox8.Controls.Add(this.label108);
            this.groupBox8.Controls.Add(this.lDoodadClimateID);
            this.groupBox8.Controls.Add(this.label106);
            this.groupBox8.Controls.Add(this.lDoodadCollideVehicle);
            this.groupBox8.Controls.Add(this.label104);
            this.groupBox8.Controls.Add(this.lDoodadCollideShip);
            this.groupBox8.Controls.Add(this.label102);
            this.groupBox8.Controls.Add(this.lDoodadSimRadius);
            this.groupBox8.Controls.Add(this.label100);
            this.groupBox8.Controls.Add(this.lDoodadTargetDecalSize);
            this.groupBox8.Controls.Add(this.label98);
            this.groupBox8.Controls.Add(this.lDoodadUseTargetHighlight);
            this.groupBox8.Controls.Add(this.label96);
            this.groupBox8.Controls.Add(this.lDoodadUseTargetSilhouette);
            this.groupBox8.Controls.Add(this.label94);
            this.groupBox8.Controls.Add(this.lDoodadUseTargetDecal);
            this.groupBox8.Controls.Add(this.label92);
            this.groupBox8.Controls.Add(this.lDoodadShowMinimap);
            this.groupBox8.Controls.Add(this.label90);
            this.groupBox8.Controls.Add(this.lDoodadGroupID);
            this.groupBox8.Controls.Add(this.label88);
            this.groupBox8.Controls.Add(this.lDoodadMilestoneID);
            this.groupBox8.Controls.Add(this.label86);
            this.groupBox8.Controls.Add(this.lDoodadForceToDTopPriority);
            this.groupBox8.Controls.Add(this.label84);
            this.groupBox8.Controls.Add(this.lDoodadUseCreatorFaction);
            this.groupBox8.Controls.Add(this.label82);
            this.groupBox8.Controls.Add(this.lDoodadModelKindID);
            this.groupBox8.Controls.Add(this.label80);
            this.groupBox8.Controls.Add(this.lDoodadMaxTime);
            this.groupBox8.Controls.Add(this.label78);
            this.groupBox8.Controls.Add(this.lDoodadMinTime);
            this.groupBox8.Controls.Add(this.label75);
            this.groupBox8.Controls.Add(this.lDoodadPercent);
            this.groupBox8.Controls.Add(this.label73);
            this.groupBox8.Controls.Add(this.lDoodadMgmtSpawn);
            this.groupBox8.Controls.Add(this.label67);
            this.groupBox8.Controls.Add(this.lDoodadShowName);
            this.groupBox8.Controls.Add(this.label64);
            this.groupBox8.Controls.Add(this.lDoodadOnceOneInteraction);
            this.groupBox8.Controls.Add(this.label53);
            this.groupBox8.Controls.Add(this.lDoodadModel);
            this.groupBox8.Controls.Add(this.label71);
            this.groupBox8.Controls.Add(this.lDoodadID);
            this.groupBox8.Controls.Add(this.label69);
            this.groupBox8.Controls.Add(this.lDoodadOnceOneMan);
            this.groupBox8.Controls.Add(this.lDoodadName);
            this.groupBox8.Controls.Add(this.label58);
            this.groupBox8.Controls.Add(this.label60);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(328, 357);
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Doodad Info";
            // 
            // lDoodadSaveIndun
            // 
            this.lDoodadSaveIndun.AutoSize = true;
            this.lDoodadSaveIndun.Location = new System.Drawing.Point(282, 238);
            this.lDoodadSaveIndun.Name = "lDoodadSaveIndun";
            this.lDoodadSaveIndun.Size = new System.Drawing.Size(13, 13);
            this.lDoodadSaveIndun.TabIndex = 107;
            this.lDoodadSaveIndun.Text = "0";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(167, 238);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(62, 13);
            this.label55.TabIndex = 106;
            this.label55.Text = "Save Indun";
            // 
            // lDoodadRestrictZoneID
            // 
            this.lDoodadRestrictZoneID.AutoSize = true;
            this.lDoodadRestrictZoneID.Location = new System.Drawing.Point(112, 338);
            this.lDoodadRestrictZoneID.Name = "lDoodadRestrictZoneID";
            this.lDoodadRestrictZoneID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadRestrictZoneID.TabIndex = 105;
            this.lDoodadRestrictZoneID.Text = "0";
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(6, 338);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(85, 13);
            this.label126.TabIndex = 104;
            this.label126.Text = "Restrict Zone ID";
            // 
            // lDoodadNoCollision
            // 
            this.lDoodadNoCollision.AutoSize = true;
            this.lDoodadNoCollision.Location = new System.Drawing.Point(282, 325);
            this.lDoodadNoCollision.Name = "lDoodadNoCollision";
            this.lDoodadNoCollision.Size = new System.Drawing.Size(13, 13);
            this.lDoodadNoCollision.TabIndex = 103;
            this.lDoodadNoCollision.Text = "0";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(167, 325);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(61, 13);
            this.label124.TabIndex = 102;
            this.label124.Text = "No collision";
            // 
            // lDoodadDespawnOnCollision
            // 
            this.lDoodadDespawnOnCollision.AutoSize = true;
            this.lDoodadDespawnOnCollision.Location = new System.Drawing.Point(112, 325);
            this.lDoodadDespawnOnCollision.Name = "lDoodadDespawnOnCollision";
            this.lDoodadDespawnOnCollision.Size = new System.Drawing.Size(13, 13);
            this.lDoodadDespawnOnCollision.TabIndex = 101;
            this.lDoodadDespawnOnCollision.Text = "0";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(6, 325);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(107, 13);
            this.label122.TabIndex = 100;
            this.label122.Text = "Despawn on collision";
            // 
            // lDoodadGrowthTime
            // 
            this.lDoodadGrowthTime.AutoSize = true;
            this.lDoodadGrowthTime.Location = new System.Drawing.Point(84, 101);
            this.lDoodadGrowthTime.Name = "lDoodadGrowthTime";
            this.lDoodadGrowthTime.Size = new System.Drawing.Size(13, 13);
            this.lDoodadGrowthTime.TabIndex = 99;
            this.lDoodadGrowthTime.Text = "0";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(6, 101);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(67, 13);
            this.label120.TabIndex = 98;
            this.label120.Text = "Growth Time";
            // 
            // lDoodadFactionID
            // 
            this.lDoodadFactionID.AutoSize = true;
            this.lDoodadFactionID.Location = new System.Drawing.Point(112, 299);
            this.lDoodadFactionID.Name = "lDoodadFactionID";
            this.lDoodadFactionID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadFactionID.TabIndex = 97;
            this.lDoodadFactionID.Text = "0";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(6, 299);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(56, 13);
            this.label118.TabIndex = 96;
            this.label118.Text = "Faction ID";
            // 
            // lDoodadChildable
            // 
            this.lDoodadChildable.AutoSize = true;
            this.lDoodadChildable.Location = new System.Drawing.Point(282, 286);
            this.lDoodadChildable.Name = "lDoodadChildable";
            this.lDoodadChildable.Size = new System.Drawing.Size(13, 13);
            this.lDoodadChildable.TabIndex = 95;
            this.lDoodadChildable.Text = "0";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(167, 286);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(50, 13);
            this.label116.TabIndex = 94;
            this.label116.Text = "Childable";
            // 
            // lDoodadParentable
            // 
            this.lDoodadParentable.AutoSize = true;
            this.lDoodadParentable.Location = new System.Drawing.Point(112, 286);
            this.lDoodadParentable.Name = "lDoodadParentable";
            this.lDoodadParentable.Size = new System.Drawing.Size(13, 13);
            this.lDoodadParentable.TabIndex = 93;
            this.lDoodadParentable.Text = "0";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(6, 286);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(58, 13);
            this.label114.TabIndex = 92;
            this.label114.Text = "Parentable";
            // 
            // lDoodadLoadModelFromWorld
            // 
            this.lDoodadLoadModelFromWorld.AutoSize = true;
            this.lDoodadLoadModelFromWorld.Location = new System.Drawing.Point(282, 264);
            this.lDoodadLoadModelFromWorld.Name = "lDoodadLoadModelFromWorld";
            this.lDoodadLoadModelFromWorld.Size = new System.Drawing.Size(13, 13);
            this.lDoodadLoadModelFromWorld.TabIndex = 91;
            this.lDoodadLoadModelFromWorld.Text = "0";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(161, 264);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(120, 13);
            this.label112.TabIndex = 90;
            this.label112.Text = "Load Model From World";
            // 
            // lDoodadForceUpAction
            // 
            this.lDoodadForceUpAction.AutoSize = true;
            this.lDoodadForceUpAction.Location = new System.Drawing.Point(112, 264);
            this.lDoodadForceUpAction.Name = "lDoodadForceUpAction";
            this.lDoodadForceUpAction.Size = new System.Drawing.Size(13, 13);
            this.lDoodadForceUpAction.TabIndex = 89;
            this.lDoodadForceUpAction.Text = "0";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(6, 264);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(84, 13);
            this.label110.TabIndex = 88;
            this.label110.Text = "Force Up Action";
            // 
            // lDoodadMarkModel
            // 
            this.lDoodadMarkModel.AutoSize = true;
            this.lDoodadMarkModel.Location = new System.Drawing.Point(112, 251);
            this.lDoodadMarkModel.Name = "lDoodadMarkModel";
            this.lDoodadMarkModel.Size = new System.Drawing.Size(43, 13);
            this.lDoodadMarkModel.TabIndex = 87;
            this.lDoodadMarkModel.Text = "<none>";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(6, 251);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(63, 13);
            this.label108.TabIndex = 86;
            this.label108.Text = "Mark Model";
            // 
            // lDoodadClimateID
            // 
            this.lDoodadClimateID.AutoSize = true;
            this.lDoodadClimateID.Location = new System.Drawing.Point(248, 127);
            this.lDoodadClimateID.Name = "lDoodadClimateID";
            this.lDoodadClimateID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadClimateID.TabIndex = 85;
            this.lDoodadClimateID.Text = "0";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(167, 127);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(55, 13);
            this.label106.TabIndex = 84;
            this.label106.Text = "Climate ID";
            // 
            // lDoodadCollideVehicle
            // 
            this.lDoodadCollideVehicle.AutoSize = true;
            this.lDoodadCollideVehicle.Location = new System.Drawing.Point(282, 225);
            this.lDoodadCollideVehicle.Name = "lDoodadCollideVehicle";
            this.lDoodadCollideVehicle.Size = new System.Drawing.Size(13, 13);
            this.lDoodadCollideVehicle.TabIndex = 83;
            this.lDoodadCollideVehicle.Text = "0";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(167, 225);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(76, 13);
            this.label104.TabIndex = 82;
            this.label104.Text = "Collide Vehicle";
            // 
            // lDoodadCollideShip
            // 
            this.lDoodadCollideShip.AutoSize = true;
            this.lDoodadCollideShip.Location = new System.Drawing.Point(112, 225);
            this.lDoodadCollideShip.Name = "lDoodadCollideShip";
            this.lDoodadCollideShip.Size = new System.Drawing.Size(13, 13);
            this.lDoodadCollideShip.TabIndex = 81;
            this.lDoodadCollideShip.Text = "0";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(6, 225);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(62, 13);
            this.label102.TabIndex = 80;
            this.label102.Text = "Collide Ship";
            // 
            // lDoodadSimRadius
            // 
            this.lDoodadSimRadius.AutoSize = true;
            this.lDoodadSimRadius.Location = new System.Drawing.Point(84, 127);
            this.lDoodadSimRadius.Name = "lDoodadSimRadius";
            this.lDoodadSimRadius.Size = new System.Drawing.Size(13, 13);
            this.lDoodadSimRadius.TabIndex = 79;
            this.lDoodadSimRadius.Text = "0";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(6, 127);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(60, 13);
            this.label100.TabIndex = 78;
            this.label100.Text = "Sim Radius";
            // 
            // lDoodadTargetDecalSize
            // 
            this.lDoodadTargetDecalSize.AutoSize = true;
            this.lDoodadTargetDecalSize.Location = new System.Drawing.Point(112, 199);
            this.lDoodadTargetDecalSize.Name = "lDoodadTargetDecalSize";
            this.lDoodadTargetDecalSize.Size = new System.Drawing.Size(13, 13);
            this.lDoodadTargetDecalSize.TabIndex = 77;
            this.lDoodadTargetDecalSize.Text = "0";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(6, 199);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(92, 13);
            this.label98.TabIndex = 76;
            this.label98.Text = "Target Decal Size";
            // 
            // lDoodadUseTargetHighlight
            // 
            this.lDoodadUseTargetHighlight.AutoSize = true;
            this.lDoodadUseTargetHighlight.Location = new System.Drawing.Point(282, 186);
            this.lDoodadUseTargetHighlight.Name = "lDoodadUseTargetHighlight";
            this.lDoodadUseTargetHighlight.Size = new System.Drawing.Size(13, 13);
            this.lDoodadUseTargetHighlight.TabIndex = 75;
            this.lDoodadUseTargetHighlight.Text = "0";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(167, 186);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(104, 13);
            this.label96.TabIndex = 74;
            this.label96.Text = "Use Target Highlight";
            // 
            // lDoodadUseTargetSilhouette
            // 
            this.lDoodadUseTargetSilhouette.AutoSize = true;
            this.lDoodadUseTargetSilhouette.Location = new System.Drawing.Point(112, 186);
            this.lDoodadUseTargetSilhouette.Name = "lDoodadUseTargetSilhouette";
            this.lDoodadUseTargetSilhouette.Size = new System.Drawing.Size(13, 13);
            this.lDoodadUseTargetSilhouette.TabIndex = 73;
            this.lDoodadUseTargetSilhouette.Text = "0";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(6, 186);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(110, 13);
            this.label94.TabIndex = 72;
            this.label94.Text = "Use Target Silhouette";
            // 
            // lDoodadUseTargetDecal
            // 
            this.lDoodadUseTargetDecal.AutoSize = true;
            this.lDoodadUseTargetDecal.Location = new System.Drawing.Point(112, 173);
            this.lDoodadUseTargetDecal.Name = "lDoodadUseTargetDecal";
            this.lDoodadUseTargetDecal.Size = new System.Drawing.Size(13, 13);
            this.lDoodadUseTargetDecal.TabIndex = 71;
            this.lDoodadUseTargetDecal.Text = "0";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(6, 173);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(91, 13);
            this.label92.TabIndex = 70;
            this.label92.Text = "Use Target Decal";
            // 
            // lDoodadShowMinimap
            // 
            this.lDoodadShowMinimap.AutoSize = true;
            this.lDoodadShowMinimap.Location = new System.Drawing.Point(112, 160);
            this.lDoodadShowMinimap.Name = "lDoodadShowMinimap";
            this.lDoodadShowMinimap.Size = new System.Drawing.Size(13, 13);
            this.lDoodadShowMinimap.TabIndex = 69;
            this.lDoodadShowMinimap.Text = "0";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(6, 160);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(76, 13);
            this.label90.TabIndex = 68;
            this.label90.Text = "Show Minimap";
            // 
            // lDoodadGroupID
            // 
            this.lDoodadGroupID.AutoSize = true;
            this.lDoodadGroupID.Location = new System.Drawing.Point(282, 53);
            this.lDoodadGroupID.Name = "lDoodadGroupID";
            this.lDoodadGroupID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadGroupID.TabIndex = 67;
            this.lDoodadGroupID.Text = "0";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(167, 53);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(50, 13);
            this.label88.TabIndex = 66;
            this.label88.Text = "Group ID";
            // 
            // lDoodadMilestoneID
            // 
            this.lDoodadMilestoneID.AutoSize = true;
            this.lDoodadMilestoneID.Location = new System.Drawing.Point(282, 160);
            this.lDoodadMilestoneID.Name = "lDoodadMilestoneID";
            this.lDoodadMilestoneID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadMilestoneID.TabIndex = 65;
            this.lDoodadMilestoneID.Text = "0";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(167, 160);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(66, 13);
            this.label86.TabIndex = 64;
            this.label86.Text = "Milestone ID";
            // 
            // lDoodadForceToDTopPriority
            // 
            this.lDoodadForceToDTopPriority.AutoSize = true;
            this.lDoodadForceToDTopPriority.Location = new System.Drawing.Point(282, 147);
            this.lDoodadForceToDTopPriority.Name = "lDoodadForceToDTopPriority";
            this.lDoodadForceToDTopPriority.Size = new System.Drawing.Size(13, 13);
            this.lDoodadForceToDTopPriority.TabIndex = 63;
            this.lDoodadForceToDTopPriority.Text = "0";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(167, 147);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(114, 13);
            this.label84.TabIndex = 62;
            this.label84.Text = "Force ToD Top Priority";
            // 
            // lDoodadUseCreatorFaction
            // 
            this.lDoodadUseCreatorFaction.AutoSize = true;
            this.lDoodadUseCreatorFaction.Location = new System.Drawing.Point(112, 147);
            this.lDoodadUseCreatorFaction.Name = "lDoodadUseCreatorFaction";
            this.lDoodadUseCreatorFaction.Size = new System.Drawing.Size(13, 13);
            this.lDoodadUseCreatorFaction.TabIndex = 61;
            this.lDoodadUseCreatorFaction.Text = "0";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(6, 147);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(101, 13);
            this.label82.TabIndex = 60;
            this.label82.Text = "Use Creator Faction";
            // 
            // lDoodadModelKindID
            // 
            this.lDoodadModelKindID.AutoSize = true;
            this.lDoodadModelKindID.Location = new System.Drawing.Point(112, 53);
            this.lDoodadModelKindID.Name = "lDoodadModelKindID";
            this.lDoodadModelKindID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadModelKindID.TabIndex = 59;
            this.lDoodadModelKindID.Text = "0";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(6, 53);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(74, 13);
            this.label80.TabIndex = 58;
            this.label80.Text = "Model Kind ID";
            // 
            // lDoodadMaxTime
            // 
            this.lDoodadMaxTime.AutoSize = true;
            this.lDoodadMaxTime.Location = new System.Drawing.Point(248, 114);
            this.lDoodadMaxTime.Name = "lDoodadMaxTime";
            this.lDoodadMaxTime.Size = new System.Drawing.Size(13, 13);
            this.lDoodadMaxTime.TabIndex = 57;
            this.lDoodadMaxTime.Text = "0";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(167, 114);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(77, 13);
            this.label78.TabIndex = 56;
            this.label78.Text = "Maximum Time";
            // 
            // lDoodadMinTime
            // 
            this.lDoodadMinTime.AutoSize = true;
            this.lDoodadMinTime.Location = new System.Drawing.Point(84, 114);
            this.lDoodadMinTime.Name = "lDoodadMinTime";
            this.lDoodadMinTime.Size = new System.Drawing.Size(13, 13);
            this.lDoodadMinTime.TabIndex = 55;
            this.lDoodadMinTime.Text = "0";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(6, 114);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(74, 13);
            this.label75.TabIndex = 54;
            this.label75.Text = "Minimum Time";
            // 
            // lDoodadPercent
            // 
            this.lDoodadPercent.AutoSize = true;
            this.lDoodadPercent.Location = new System.Drawing.Point(248, 101);
            this.lDoodadPercent.Name = "lDoodadPercent";
            this.lDoodadPercent.Size = new System.Drawing.Size(13, 13);
            this.lDoodadPercent.TabIndex = 53;
            this.lDoodadPercent.Text = "0";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(167, 101);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(44, 13);
            this.label73.TabIndex = 52;
            this.label73.Text = "Percent";
            // 
            // lDoodadMgmtSpawn
            // 
            this.lDoodadMgmtSpawn.AutoSize = true;
            this.lDoodadMgmtSpawn.Location = new System.Drawing.Point(282, 79);
            this.lDoodadMgmtSpawn.Name = "lDoodadMgmtSpawn";
            this.lDoodadMgmtSpawn.Size = new System.Drawing.Size(13, 13);
            this.lDoodadMgmtSpawn.TabIndex = 51;
            this.lDoodadMgmtSpawn.Text = "0";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(167, 79);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(69, 13);
            this.label67.TabIndex = 50;
            this.label67.Text = "Mgmt Spawn";
            // 
            // lDoodadShowName
            // 
            this.lDoodadShowName.AutoSize = true;
            this.lDoodadShowName.Location = new System.Drawing.Point(112, 79);
            this.lDoodadShowName.Name = "lDoodadShowName";
            this.lDoodadShowName.Size = new System.Drawing.Size(13, 13);
            this.lDoodadShowName.TabIndex = 49;
            this.lDoodadShowName.Text = "0";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(6, 79);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(65, 13);
            this.label64.TabIndex = 48;
            this.label64.Text = "Show Name";
            // 
            // lDoodadOnceOneInteraction
            // 
            this.lDoodadOnceOneInteraction.AutoSize = true;
            this.lDoodadOnceOneInteraction.Location = new System.Drawing.Point(282, 66);
            this.lDoodadOnceOneInteraction.Name = "lDoodadOnceOneInteraction";
            this.lDoodadOnceOneInteraction.Size = new System.Drawing.Size(13, 13);
            this.lDoodadOnceOneInteraction.TabIndex = 47;
            this.lDoodadOnceOneInteraction.Text = "0";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(167, 66);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(109, 13);
            this.label53.TabIndex = 46;
            this.label53.Text = "Once One Interaction";
            // 
            // lDoodadModel
            // 
            this.lDoodadModel.AutoSize = true;
            this.lDoodadModel.Location = new System.Drawing.Point(54, 40);
            this.lDoodadModel.Name = "lDoodadModel";
            this.lDoodadModel.Size = new System.Drawing.Size(43, 13);
            this.lDoodadModel.TabIndex = 45;
            this.lDoodadModel.Text = "<none>";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(6, 40);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(36, 13);
            this.label71.TabIndex = 44;
            this.label71.Text = "Model";
            // 
            // lDoodadID
            // 
            this.lDoodadID.AutoSize = true;
            this.lDoodadID.Location = new System.Drawing.Point(30, 16);
            this.lDoodadID.Name = "lDoodadID";
            this.lDoodadID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadID.TabIndex = 43;
            this.lDoodadID.Text = "0";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(6, 16);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(18, 13);
            this.label69.TabIndex = 42;
            this.label69.Text = "ID";
            // 
            // lDoodadOnceOneMan
            // 
            this.lDoodadOnceOneMan.AutoSize = true;
            this.lDoodadOnceOneMan.Location = new System.Drawing.Point(112, 66);
            this.lDoodadOnceOneMan.Name = "lDoodadOnceOneMan";
            this.lDoodadOnceOneMan.Size = new System.Drawing.Size(13, 13);
            this.lDoodadOnceOneMan.TabIndex = 35;
            this.lDoodadOnceOneMan.Text = "0";
            // 
            // lDoodadName
            // 
            this.lDoodadName.AutoSize = true;
            this.lDoodadName.Location = new System.Drawing.Point(131, 16);
            this.lDoodadName.Name = "lDoodadName";
            this.lDoodadName.Size = new System.Drawing.Size(43, 13);
            this.lDoodadName.TabIndex = 36;
            this.lDoodadName.Text = "<none>";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(6, 66);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(80, 13);
            this.label58.TabIndex = 34;
            this.label58.Text = "Once One Man";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(90, 16);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(35, 13);
            this.label60.TabIndex = 34;
            this.label60.Text = "Name";
            // 
            // tpDoodadFunctions
            // 
            this.tpDoodadFunctions.Controls.Add(this.groupBox10);
            this.tpDoodadFunctions.Location = new System.Drawing.Point(4, 4);
            this.tpDoodadFunctions.Name = "tpDoodadFunctions";
            this.tpDoodadFunctions.Padding = new System.Windows.Forms.Padding(3);
            this.tpDoodadFunctions.Size = new System.Drawing.Size(334, 453);
            this.tpDoodadFunctions.TabIndex = 2;
            this.tpDoodadFunctions.Text = "Functions";
            this.tpDoodadFunctions.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.lDoodadPhaseFuncsActualType);
            this.groupBox10.Controls.Add(this.lDoodadPhaseFuncsActualId);
            this.groupBox10.Controls.Add(this.label136);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupIsMsgToZone);
            this.groupBox10.Controls.Add(this.label111);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupSoundID);
            this.groupBox10.Controls.Add(this.label107);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupSoundTime);
            this.groupBox10.Controls.Add(this.label103);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupModel);
            this.groupBox10.Controls.Add(this.label99);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupComment);
            this.groupBox10.Controls.Add(this.label95);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupPhaseMsg);
            this.groupBox10.Controls.Add(this.label91);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupKindID);
            this.groupBox10.Controls.Add(this.label87);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupID);
            this.groupBox10.Controls.Add(this.label79);
            this.groupBox10.Controls.Add(this.lDoodadFuncGroupName);
            this.groupBox10.Controls.Add(this.label66);
            this.groupBox10.Controls.Add(this.dgvDoodadFuncGroups);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox10.Location = new System.Drawing.Point(3, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(328, 343);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Doodad Function Groups";
            // 
            // lDoodadPhaseFuncsActualType
            // 
            this.lDoodadPhaseFuncsActualType.AutoSize = true;
            this.lDoodadPhaseFuncsActualType.Location = new System.Drawing.Point(155, 315);
            this.lDoodadPhaseFuncsActualType.Name = "lDoodadPhaseFuncsActualType";
            this.lDoodadPhaseFuncsActualType.Size = new System.Drawing.Size(31, 13);
            this.lDoodadPhaseFuncsActualType.TabIndex = 59;
            this.lDoodadPhaseFuncsActualType.Text = "none";
            // 
            // lDoodadPhaseFuncsActualId
            // 
            this.lDoodadPhaseFuncsActualId.AutoSize = true;
            this.lDoodadPhaseFuncsActualId.Location = new System.Drawing.Point(94, 315);
            this.lDoodadPhaseFuncsActualId.Name = "lDoodadPhaseFuncsActualId";
            this.lDoodadPhaseFuncsActualId.Size = new System.Drawing.Size(13, 13);
            this.lDoodadPhaseFuncsActualId.TabIndex = 58;
            this.lDoodadPhaseFuncsActualId.Text = "0";
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(6, 315);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(49, 13);
            this.label136.TabIndex = 57;
            this.label136.Text = "Actual Id";
            // 
            // lDoodadFuncGroupIsMsgToZone
            // 
            this.lDoodadFuncGroupIsMsgToZone.AutoSize = true;
            this.lDoodadFuncGroupIsMsgToZone.Location = new System.Drawing.Point(94, 286);
            this.lDoodadFuncGroupIsMsgToZone.Name = "lDoodadFuncGroupIsMsgToZone";
            this.lDoodadFuncGroupIsMsgToZone.Size = new System.Drawing.Size(13, 13);
            this.lDoodadFuncGroupIsMsgToZone.TabIndex = 56;
            this.lDoodadFuncGroupIsMsgToZone.Text = "0";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(6, 286);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(83, 13);
            this.label111.TabIndex = 55;
            this.label111.Text = "is_msg_to_zone";
            // 
            // lDoodadFuncGroupSoundID
            // 
            this.lDoodadFuncGroupSoundID.AutoSize = true;
            this.lDoodadFuncGroupSoundID.Location = new System.Drawing.Point(94, 260);
            this.lDoodadFuncGroupSoundID.Name = "lDoodadFuncGroupSoundID";
            this.lDoodadFuncGroupSoundID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadFuncGroupSoundID.TabIndex = 54;
            this.lDoodadFuncGroupSoundID.Text = "0";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(6, 211);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(50, 13);
            this.label107.TabIndex = 53;
            this.label107.Text = "comment";
            // 
            // lDoodadFuncGroupSoundTime
            // 
            this.lDoodadFuncGroupSoundTime.AutoSize = true;
            this.lDoodadFuncGroupSoundTime.Location = new System.Drawing.Point(94, 273);
            this.lDoodadFuncGroupSoundTime.Name = "lDoodadFuncGroupSoundTime";
            this.lDoodadFuncGroupSoundTime.Size = new System.Drawing.Size(13, 13);
            this.lDoodadFuncGroupSoundTime.TabIndex = 52;
            this.lDoodadFuncGroupSoundTime.Text = "0";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(6, 273);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(61, 13);
            this.label103.TabIndex = 51;
            this.label103.Text = "sound_time";
            // 
            // lDoodadFuncGroupModel
            // 
            this.lDoodadFuncGroupModel.AutoSize = true;
            this.lDoodadFuncGroupModel.Location = new System.Drawing.Point(64, 224);
            this.lDoodadFuncGroupModel.Name = "lDoodadFuncGroupModel";
            this.lDoodadFuncGroupModel.Size = new System.Drawing.Size(43, 13);
            this.lDoodadFuncGroupModel.TabIndex = 50;
            this.lDoodadFuncGroupModel.Text = "<none>";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(6, 224);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(35, 13);
            this.label99.TabIndex = 49;
            this.label99.Text = "model";
            // 
            // lDoodadFuncGroupComment
            // 
            this.lDoodadFuncGroupComment.AutoSize = true;
            this.lDoodadFuncGroupComment.Location = new System.Drawing.Point(64, 211);
            this.lDoodadFuncGroupComment.Name = "lDoodadFuncGroupComment";
            this.lDoodadFuncGroupComment.Size = new System.Drawing.Size(43, 13);
            this.lDoodadFuncGroupComment.TabIndex = 48;
            this.lDoodadFuncGroupComment.Text = "<none>";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(6, 260);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(50, 13);
            this.label95.TabIndex = 47;
            this.label95.Text = "sound_id";
            // 
            // lDoodadFuncGroupPhaseMsg
            // 
            this.lDoodadFuncGroupPhaseMsg.AutoSize = true;
            this.lDoodadFuncGroupPhaseMsg.Location = new System.Drawing.Point(64, 198);
            this.lDoodadFuncGroupPhaseMsg.Name = "lDoodadFuncGroupPhaseMsg";
            this.lDoodadFuncGroupPhaseMsg.Size = new System.Drawing.Size(43, 13);
            this.lDoodadFuncGroupPhaseMsg.TabIndex = 46;
            this.lDoodadFuncGroupPhaseMsg.Text = "<none>";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(6, 198);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(61, 13);
            this.label91.TabIndex = 45;
            this.label91.Text = "phase_msg";
            // 
            // lDoodadFuncGroupKindID
            // 
            this.lDoodadFuncGroupKindID.AutoSize = true;
            this.lDoodadFuncGroupKindID.Location = new System.Drawing.Point(155, 237);
            this.lDoodadFuncGroupKindID.Name = "lDoodadFuncGroupKindID";
            this.lDoodadFuncGroupKindID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadFuncGroupKindID.TabIndex = 44;
            this.lDoodadFuncGroupKindID.Text = "0";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(6, 237);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(143, 13);
            this.label87.TabIndex = 43;
            this.label87.Text = "doodad_func_group_kind_id";
            // 
            // lDoodadFuncGroupID
            // 
            this.lDoodadFuncGroupID.AutoSize = true;
            this.lDoodadFuncGroupID.Location = new System.Drawing.Point(64, 172);
            this.lDoodadFuncGroupID.Name = "lDoodadFuncGroupID";
            this.lDoodadFuncGroupID.Size = new System.Drawing.Size(13, 13);
            this.lDoodadFuncGroupID.TabIndex = 40;
            this.lDoodadFuncGroupID.Text = "0";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(6, 172);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(18, 13);
            this.label79.TabIndex = 39;
            this.label79.Text = "ID";
            // 
            // lDoodadFuncGroupName
            // 
            this.lDoodadFuncGroupName.AutoSize = true;
            this.lDoodadFuncGroupName.Location = new System.Drawing.Point(64, 185);
            this.lDoodadFuncGroupName.Name = "lDoodadFuncGroupName";
            this.lDoodadFuncGroupName.Size = new System.Drawing.Size(43, 13);
            this.lDoodadFuncGroupName.TabIndex = 38;
            this.lDoodadFuncGroupName.Text = "<none>";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 185);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(35, 13);
            this.label66.TabIndex = 37;
            this.label66.Text = "Name";
            // 
            // dgvDoodadFuncGroups
            // 
            this.dgvDoodadFuncGroups.AllowUserToAddRows = false;
            this.dgvDoodadFuncGroups.AllowUserToDeleteRows = false;
            this.dgvDoodadFuncGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDoodadFuncGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoodadFuncGroups.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDoodadFuncGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoodadFuncGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Column27, this.Column32, this.Column33, this.Column54 });
            this.dgvDoodadFuncGroups.Location = new System.Drawing.Point(6, 19);
            this.dgvDoodadFuncGroups.Name = "dgvDoodadFuncGroups";
            this.dgvDoodadFuncGroups.ReadOnly = true;
            this.dgvDoodadFuncGroups.RowHeadersVisible = false;
            this.dgvDoodadFuncGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoodadFuncGroups.Size = new System.Drawing.Size(316, 143);
            this.dgvDoodadFuncGroups.TabIndex = 8;
            this.dgvDoodadFuncGroups.SelectionChanged += new System.EventHandler(this.DgvDoodadFuncGroups_SelectionChanged);
            // 
            // Column27
            // 
            this.Column27.FillWeight = 25F;
            this.Column27.HeaderText = "ID";
            this.Column27.Name = "Column27";
            this.Column27.ReadOnly = true;
            // 
            // Column32
            // 
            this.Column32.FillWeight = 15F;
            this.Column32.HeaderText = "Kind";
            this.Column32.Name = "Column32";
            this.Column32.ReadOnly = true;
            // 
            // Column33
            // 
            this.Column33.FillWeight = 25F;
            this.Column33.HeaderText = "ActualID";
            this.Column33.Name = "Column33";
            this.Column33.ReadOnly = true;
            // 
            // Column54
            // 
            this.Column54.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column54.HeaderText = "ActualType";
            this.Column54.Name = "Column54";
            this.Column54.ReadOnly = true;
            // 
            // tpDoodadTools
            // 
            this.tpDoodadTools.Controls.Add(this.btnShowDoodadOnMap);
            this.tpDoodadTools.Location = new System.Drawing.Point(4, 4);
            this.tpDoodadTools.Name = "tpDoodadTools";
            this.tpDoodadTools.Padding = new System.Windows.Forms.Padding(3);
            this.tpDoodadTools.Size = new System.Drawing.Size(334, 453);
            this.tpDoodadTools.TabIndex = 3;
            this.tpDoodadTools.Text = "Tools";
            this.tpDoodadTools.UseVisualStyleBackColor = true;
            // 
            // btnShowDoodadOnMap
            // 
            this.btnShowDoodadOnMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowDoodadOnMap.ForeColor = System.Drawing.Color.Black;
            this.btnShowDoodadOnMap.Location = new System.Drawing.Point(6, 425);
            this.btnShowDoodadOnMap.Name = "btnShowDoodadOnMap";
            this.btnShowDoodadOnMap.Size = new System.Drawing.Size(322, 22);
            this.btnShowDoodadOnMap.TabIndex = 27;
            this.btnShowDoodadOnMap.Text = "Find Selected";
            this.btnShowDoodadOnMap.UseVisualStyleBackColor = true;
            this.btnShowDoodadOnMap.Click += new System.EventHandler(this.btnShowDoodadOnMap_Click);
            // 
            // tpDoodadWorkflow
            // 
            this.tpDoodadWorkflow.Controls.Add(this.cbDoodadWorkflowHideEmpty);
            this.tpDoodadWorkflow.Controls.Add(this.tvDoodadDetails);
            this.tpDoodadWorkflow.Location = new System.Drawing.Point(4, 4);
            this.tpDoodadWorkflow.Name = "tpDoodadWorkflow";
            this.tpDoodadWorkflow.Padding = new System.Windows.Forms.Padding(3);
            this.tpDoodadWorkflow.Size = new System.Drawing.Size(334, 453);
            this.tpDoodadWorkflow.TabIndex = 4;
            this.tpDoodadWorkflow.Text = "Workflow";
            this.tpDoodadWorkflow.UseVisualStyleBackColor = true;
            // 
            // cbDoodadWorkflowHideEmpty
            // 
            this.cbDoodadWorkflowHideEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDoodadWorkflowHideEmpty.AutoSize = true;
            this.cbDoodadWorkflowHideEmpty.Location = new System.Drawing.Point(6, 430);
            this.cbDoodadWorkflowHideEmpty.Name = "cbDoodadWorkflowHideEmpty";
            this.cbDoodadWorkflowHideEmpty.Size = new System.Drawing.Size(113, 17);
            this.cbDoodadWorkflowHideEmpty.TabIndex = 1;
            this.cbDoodadWorkflowHideEmpty.Text = "Hide empty values";
            this.cbDoodadWorkflowHideEmpty.UseVisualStyleBackColor = true;
            // 
            // tvDoodadDetails
            // 
            this.tvDoodadDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tvDoodadDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.tvDoodadDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.tvDoodadDetails.LineColor = System.Drawing.Color.LightGray;
            this.tvDoodadDetails.Location = new System.Drawing.Point(6, 6);
            this.tvDoodadDetails.Name = "tvDoodadDetails";
            this.tvDoodadDetails.Size = new System.Drawing.Size(322, 420);
            this.tvDoodadDetails.TabIndex = 0;
            this.tvDoodadDetails.DoubleClick += new System.EventHandler(this.tvDoodadDetails_DoubleClick);
            // 
            // btnSearchDoodads
            // 
            this.btnSearchDoodads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchDoodads.Enabled = false;
            this.btnSearchDoodads.Location = new System.Drawing.Point(496, 8);
            this.btnSearchDoodads.Name = "btnSearchDoodads";
            this.btnSearchDoodads.Size = new System.Drawing.Size(79, 23);
            this.btnSearchDoodads.TabIndex = 14;
            this.btnSearchDoodads.Text = "Search";
            this.btnSearchDoodads.UseVisualStyleBackColor = true;
            this.btnSearchDoodads.Click += new System.EventHandler(this.BtnSearchDoodads_Click);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(8, 13);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(223, 13);
            this.label46.TabIndex = 13;
            this.label46.Text = "Search Doodad ID, Group ID, Name or Model";
            // 
            // tSearchDoodads
            // 
            this.tSearchDoodads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tSearchDoodads.Location = new System.Drawing.Point(237, 10);
            this.tSearchDoodads.Name = "tSearchDoodads";
            this.tSearchDoodads.Size = new System.Drawing.Size(253, 20);
            this.tSearchDoodads.TabIndex = 12;
            this.tSearchDoodads.TextChanged += new System.EventHandler(this.TSearchDoodads_TextChanged);
            this.tSearchDoodads.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TSearchDoodads_KeyDown);
            // 
            // dgvDoodads
            // 
            this.dgvDoodads.AllowUserToAddRows = false;
            this.dgvDoodads.AllowUserToDeleteRows = false;
            this.dgvDoodads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDoodads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDoodads.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDoodads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoodads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn18, this.dataGridViewTextBoxColumn19, this.Column23, this.Column28, this.Column25, this.Column26, this.Column31, this.Column22, this.Column53 });
            this.dgvDoodads.Location = new System.Drawing.Point(8, 41);
            this.dgvDoodads.Name = "dgvDoodads";
            this.dgvDoodads.ReadOnly = true;
            this.dgvDoodads.RowHeadersVisible = false;
            this.dgvDoodads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoodads.Size = new System.Drawing.Size(567, 437);
            this.dgvDoodads.TabIndex = 11;
            this.dgvDoodads.SelectionChanged += new System.EventHandler(this.DgvDoodads_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.HeaderText = "ID";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Width = 43;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "Name";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 60;
            // 
            // Column23
            // 
            this.Column23.HeaderText = "Managed";
            this.Column23.Name = "Column23";
            this.Column23.ReadOnly = true;
            this.Column23.Width = 77;
            // 
            // Column28
            // 
            this.Column28.HeaderText = "Group";
            this.Column28.Name = "Column28";
            this.Column28.ReadOnly = true;
            this.Column28.Width = 61;
            // 
            // Column25
            // 
            this.Column25.HeaderText = "Percent";
            this.Column25.Name = "Column25";
            this.Column25.ReadOnly = true;
            this.Column25.Width = 69;
            // 
            // Column26
            // 
            this.Column26.HeaderText = "Faction";
            this.Column26.Name = "Column26";
            this.Column26.ReadOnly = true;
            this.Column26.Width = 67;
            // 
            // Column31
            // 
            this.Column31.HeaderText = "Model Type";
            this.Column31.Name = "Column31";
            this.Column31.ReadOnly = true;
            this.Column31.Width = 88;
            // 
            // Column22
            // 
            this.Column22.HeaderText = "Model";
            this.Column22.Name = "Column22";
            this.Column22.ReadOnly = true;
            this.Column22.Width = 61;
            // 
            // Column53
            // 
            this.Column53.HeaderText = "Spawns";
            this.Column53.Name = "Column53";
            this.Column53.ReadOnly = true;
            this.Column53.Width = 70;
            // 
            // tbFactions
            // 
            this.tbFactions.Controls.Add(this.groupBox7);
            this.tbFactions.Controls.Add(this.groupBox6);
            this.tbFactions.Controls.Add(this.btnFactionsAll);
            this.tbFactions.Controls.Add(this.btnSearchFaction);
            this.tbFactions.Controls.Add(this.label42);
            this.tbFactions.Controls.Add(this.tSearchFaction);
            this.tbFactions.Controls.Add(this.dgvFactions);
            this.tbFactions.Location = new System.Drawing.Point(4, 22);
            this.tbFactions.Name = "tbFactions";
            this.tbFactions.Padding = new System.Windows.Forms.Padding(3);
            this.tbFactions.Size = new System.Drawing.Size(926, 485);
            this.tbFactions.TabIndex = 7;
            this.tbFactions.Text = "Factions";
            this.tbFactions.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.BackColor = System.Drawing.Color.Transparent;
            this.groupBox7.Controls.Add(this.lFactionHostilePirate);
            this.groupBox7.Controls.Add(this.label49);
            this.groupBox7.Controls.Add(this.lFactionHostileHaranya);
            this.groupBox7.Controls.Add(this.label47);
            this.groupBox7.Controls.Add(this.lFactionHostileNuia);
            this.groupBox7.Controls.Add(this.label76);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox7.Location = new System.Drawing.Point(615, 242);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(302, 88);
            this.groupBox7.TabIndex = 33;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Hostile";
            // 
            // lFactionHostilePirate
            // 
            this.lFactionHostilePirate.AutoSize = true;
            this.lFactionHostilePirate.Location = new System.Drawing.Point(106, 62);
            this.lFactionHostilePirate.Name = "lFactionHostilePirate";
            this.lFactionHostilePirate.Size = new System.Drawing.Size(13, 13);
            this.lFactionHostilePirate.TabIndex = 5;
            this.lFactionHostilePirate.Text = "0";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(6, 62);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(51, 13);
            this.label49.TabIndex = 4;
            this.label49.Text = "vs. Pirate";
            // 
            // lFactionHostileHaranya
            // 
            this.lFactionHostileHaranya.AutoSize = true;
            this.lFactionHostileHaranya.Location = new System.Drawing.Point(106, 38);
            this.lFactionHostileHaranya.Name = "lFactionHostileHaranya";
            this.lFactionHostileHaranya.Size = new System.Drawing.Size(13, 13);
            this.lFactionHostileHaranya.TabIndex = 3;
            this.lFactionHostileHaranya.Text = "0";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(6, 38);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(64, 13);
            this.label47.TabIndex = 2;
            this.label47.Text = "vs. Haranya";
            // 
            // lFactionHostileNuia
            // 
            this.lFactionHostileNuia.AutoSize = true;
            this.lFactionHostileNuia.Location = new System.Drawing.Point(106, 16);
            this.lFactionHostileNuia.Name = "lFactionHostileNuia";
            this.lFactionHostileNuia.Size = new System.Drawing.Size(13, 13);
            this.lFactionHostileNuia.TabIndex = 1;
            this.lFactionHostileNuia.Text = "0";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(6, 16);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(46, 13);
            this.label76.TabIndex = 0;
            this.label76.Text = "vs. Nuia";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.lFactionMotherID);
            this.groupBox6.Controls.Add(this.label65);
            this.groupBox6.Controls.Add(this.lFactionDiplomacyLinkID);
            this.groupBox6.Controls.Add(this.label63);
            this.groupBox6.Controls.Add(this.lFactionIsDiplomacyTarget);
            this.groupBox6.Controls.Add(this.label61);
            this.groupBox6.Controls.Add(this.lFactionGuardLink);
            this.groupBox6.Controls.Add(this.label57);
            this.groupBox6.Controls.Add(this.lFactionAggroLink);
            this.groupBox6.Controls.Add(this.label54);
            this.groupBox6.Controls.Add(this.LFactionPoliticalSystemID);
            this.groupBox6.Controls.Add(this.label52);
            this.groupBox6.Controls.Add(this.lFactionOwnerTypeID);
            this.groupBox6.Controls.Add(this.label50);
            this.groupBox6.Controls.Add(this.label44);
            this.groupBox6.Controls.Add(this.lFactionOwnerName);
            this.groupBox6.Controls.Add(this.lFactionOwnerID);
            this.groupBox6.Controls.Add(this.label48);
            this.groupBox6.Controls.Add(this.label45);
            this.groupBox6.Controls.Add(this.lFactionName);
            this.groupBox6.Controls.Add(this.lFactionID);
            this.groupBox6.Controls.Add(this.label59);
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox6.Location = new System.Drawing.Point(615, 10);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(302, 226);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Faction Info";
            // 
            // lFactionMotherID
            // 
            this.lFactionMotherID.AutoSize = true;
            this.lFactionMotherID.Location = new System.Drawing.Point(106, 126);
            this.lFactionMotherID.Name = "lFactionMotherID";
            this.lFactionMotherID.Size = new System.Drawing.Size(13, 13);
            this.lFactionMotherID.TabIndex = 32;
            this.lFactionMotherID.Text = "0";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 126);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(54, 13);
            this.label65.TabIndex = 31;
            this.label65.Text = "Mother ID";
            // 
            // lFactionDiplomacyLinkID
            // 
            this.lFactionDiplomacyLinkID.AutoSize = true;
            this.lFactionDiplomacyLinkID.Location = new System.Drawing.Point(106, 201);
            this.lFactionDiplomacyLinkID.Name = "lFactionDiplomacyLinkID";
            this.lFactionDiplomacyLinkID.Size = new System.Drawing.Size(13, 13);
            this.lFactionDiplomacyLinkID.TabIndex = 30;
            this.lFactionDiplomacyLinkID.Text = "0";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(6, 201);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(98, 13);
            this.label63.TabIndex = 29;
            this.label63.Text = "Displomacy Link ID";
            // 
            // lFactionIsDiplomacyTarget
            // 
            this.lFactionIsDiplomacyTarget.AutoSize = true;
            this.lFactionIsDiplomacyTarget.Location = new System.Drawing.Point(106, 188);
            this.lFactionIsDiplomacyTarget.Name = "lFactionIsDiplomacyTarget";
            this.lFactionIsDiplomacyTarget.Size = new System.Drawing.Size(32, 13);
            this.lFactionIsDiplomacyTarget.TabIndex = 28;
            this.lFactionIsDiplomacyTarget.Text = "False";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(6, 188);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(101, 13);
            this.label61.TabIndex = 27;
            this.label61.Text = "Is Diplomacy Target";
            // 
            // lFactionGuardLink
            // 
            this.lFactionGuardLink.AutoSize = true;
            this.lFactionGuardLink.Location = new System.Drawing.Point(106, 163);
            this.lFactionGuardLink.Name = "lFactionGuardLink";
            this.lFactionGuardLink.Size = new System.Drawing.Size(32, 13);
            this.lFactionGuardLink.TabIndex = 26;
            this.lFactionGuardLink.Text = "False";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(6, 163);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(59, 13);
            this.label57.TabIndex = 25;
            this.label57.Text = "Guard Link";
            // 
            // lFactionAggroLink
            // 
            this.lFactionAggroLink.AutoSize = true;
            this.lFactionAggroLink.Location = new System.Drawing.Point(106, 150);
            this.lFactionAggroLink.Name = "lFactionAggroLink";
            this.lFactionAggroLink.Size = new System.Drawing.Size(32, 13);
            this.lFactionAggroLink.TabIndex = 24;
            this.lFactionAggroLink.Text = "False";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(6, 150);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(58, 13);
            this.label54.TabIndex = 23;
            this.label54.Text = "Aggro Link";
            // 
            // LFactionPoliticalSystemID
            // 
            this.LFactionPoliticalSystemID.AutoSize = true;
            this.LFactionPoliticalSystemID.Location = new System.Drawing.Point(106, 104);
            this.LFactionPoliticalSystemID.Name = "LFactionPoliticalSystemID";
            this.LFactionPoliticalSystemID.Size = new System.Drawing.Size(13, 13);
            this.LFactionPoliticalSystemID.TabIndex = 22;
            this.LFactionPoliticalSystemID.Text = "0";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(6, 104);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(94, 13);
            this.label52.TabIndex = 21;
            this.label52.Text = "Political System ID";
            // 
            // lFactionOwnerTypeID
            // 
            this.lFactionOwnerTypeID.AutoSize = true;
            this.lFactionOwnerTypeID.Location = new System.Drawing.Point(106, 67);
            this.lFactionOwnerTypeID.Name = "lFactionOwnerTypeID";
            this.lFactionOwnerTypeID.Size = new System.Drawing.Size(13, 13);
            this.lFactionOwnerTypeID.TabIndex = 20;
            this.lFactionOwnerTypeID.Text = "0";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 67);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(79, 13);
            this.label50.TabIndex = 19;
            this.label50.Text = "Owner Type ID";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 54);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(69, 13);
            this.label44.TabIndex = 18;
            this.label44.Text = "Owner Name";
            // 
            // lFactionOwnerName
            // 
            this.lFactionOwnerName.AutoSize = true;
            this.lFactionOwnerName.Location = new System.Drawing.Point(106, 54);
            this.lFactionOwnerName.Name = "lFactionOwnerName";
            this.lFactionOwnerName.Size = new System.Drawing.Size(43, 13);
            this.lFactionOwnerName.TabIndex = 17;
            this.lFactionOwnerName.Text = "<none>";
            // 
            // lFactionOwnerID
            // 
            this.lFactionOwnerID.AutoSize = true;
            this.lFactionOwnerID.Location = new System.Drawing.Point(106, 80);
            this.lFactionOwnerID.Name = "lFactionOwnerID";
            this.lFactionOwnerID.Size = new System.Drawing.Size(13, 13);
            this.lFactionOwnerID.TabIndex = 16;
            this.lFactionOwnerID.Text = "0";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(6, 80);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(52, 13);
            this.label48.TabIndex = 15;
            this.label48.Text = "Owner ID";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 29);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(35, 13);
            this.label45.TabIndex = 14;
            this.label45.Text = "Name";
            // 
            // lFactionName
            // 
            this.lFactionName.AutoSize = true;
            this.lFactionName.Location = new System.Drawing.Point(106, 29);
            this.lFactionName.Name = "lFactionName";
            this.lFactionName.Size = new System.Drawing.Size(43, 13);
            this.lFactionName.TabIndex = 3;
            this.lFactionName.Text = "<none>";
            // 
            // lFactionID
            // 
            this.lFactionID.AutoSize = true;
            this.lFactionID.Location = new System.Drawing.Point(106, 16);
            this.lFactionID.Name = "lFactionID";
            this.lFactionID.Size = new System.Drawing.Size(13, 13);
            this.lFactionID.TabIndex = 1;
            this.lFactionID.Text = "0";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 16);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(33, 13);
            this.label59.TabIndex = 0;
            this.label59.Text = "Index";
            // 
            // btnFactionsAll
            // 
            this.btnFactionsAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFactionsAll.Location = new System.Drawing.Point(530, 8);
            this.btnFactionsAll.Name = "btnFactionsAll";
            this.btnFactionsAll.Size = new System.Drawing.Size(79, 23);
            this.btnFactionsAll.TabIndex = 15;
            this.btnFactionsAll.Text = "Show All";
            this.btnFactionsAll.UseVisualStyleBackColor = true;
            this.btnFactionsAll.Click += new System.EventHandler(this.BtnFactionsAll_Click);
            // 
            // btnSearchFaction
            // 
            this.btnSearchFaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchFaction.Enabled = false;
            this.btnSearchFaction.Location = new System.Drawing.Point(445, 8);
            this.btnSearchFaction.Name = "btnSearchFaction";
            this.btnSearchFaction.Size = new System.Drawing.Size(79, 23);
            this.btnSearchFaction.TabIndex = 14;
            this.btnSearchFaction.Text = "Search";
            this.btnSearchFaction.UseVisualStyleBackColor = true;
            this.btnSearchFaction.Click += new System.EventHandler(this.BtnSearchFaction_Click);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(8, 13);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(207, 13);
            this.label42.TabIndex = 13;
            this.label42.Text = "Search Faction ID, Name (includes owner)";
            // 
            // tSearchFaction
            // 
            this.tSearchFaction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tSearchFaction.Location = new System.Drawing.Point(221, 10);
            this.tSearchFaction.Name = "tSearchFaction";
            this.tSearchFaction.Size = new System.Drawing.Size(218, 20);
            this.tSearchFaction.TabIndex = 12;
            this.tSearchFaction.TextChanged += new System.EventHandler(this.TSearchFaction_TextChanged);
            this.tSearchFaction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TSearchFaction_KeyDown);
            // 
            // dgvFactions
            // 
            this.dgvFactions.AllowUserToAddRows = false;
            this.dgvFactions.AllowUserToDeleteRows = false;
            this.dgvFactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFactions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvFactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn15, this.dataGridViewTextBoxColumn16, this.Column30, this.Column24, this.Column29 });
            this.dgvFactions.Location = new System.Drawing.Point(8, 41);
            this.dgvFactions.Name = "dgvFactions";
            this.dgvFactions.ReadOnly = true;
            this.dgvFactions.RowHeadersVisible = false;
            this.dgvFactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFactions.Size = new System.Drawing.Size(601, 437);
            this.dgvFactions.TabIndex = 11;
            this.dgvFactions.SelectionChanged += new System.EventHandler(this.DgvFactions_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "ID";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 43;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "Name";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 60;
            // 
            // Column30
            // 
            this.Column30.HeaderText = "Mother";
            this.Column30.Name = "Column30";
            this.Column30.ReadOnly = true;
            this.Column30.Width = 65;
            // 
            // Column24
            // 
            this.Column24.HeaderText = "Owner";
            this.Column24.Name = "Column24";
            this.Column24.ReadOnly = true;
            this.Column24.Width = 63;
            // 
            // Column29
            // 
            this.Column29.HeaderText = "Diplomacy";
            this.Column29.Name = "Column29";
            this.Column29.ReadOnly = true;
            this.Column29.Width = 81;
            // 
            // tpItems
            // 
            this.tpItems.Controls.Add(this.cbItemSearchRange);
            this.tpItems.Controls.Add(this.label135);
            this.tpItems.Controls.Add(this.label83);
            this.tpItems.Controls.Add(this.cbItemSearchItemCategoryTypeList);
            this.tpItems.Controls.Add(this.label51);
            this.tpItems.Controls.Add(this.cbItemSearchItemArmorSlotTypeList);
            this.tpItems.Controls.Add(this.btnFindItemSkill);
            this.tpItems.Controls.Add(this.groupBox1);
            this.tpItems.Controls.Add(this.btnItemSearch);
            this.tpItems.Controls.Add(this.dgvItem);
            this.tpItems.Controls.Add(this.btnFindItemInLoot);
            this.tpItems.Controls.Add(this.label1);
            this.tpItems.Controls.Add(this.tItemSearch);
            this.tpItems.Location = new System.Drawing.Point(4, 22);
            this.tpItems.Name = "tpItems";
            this.tpItems.Padding = new System.Windows.Forms.Padding(3);
            this.tpItems.Size = new System.Drawing.Size(926, 485);
            this.tpItems.TabIndex = 1;
            this.tpItems.Text = "Items";
            this.tpItems.UseVisualStyleBackColor = true;
            // 
            // cbItemSearchRange
            // 
            this.cbItemSearchRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbItemSearchRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemSearchRange.FormattingEnabled = true;
            this.cbItemSearchRange.Items.AddRange(new object[] { "All", "Region Specific" });
            this.cbItemSearchRange.Location = new System.Drawing.Point(475, 30);
            this.cbItemSearchRange.Name = "cbItemSearchRange";
            this.cbItemSearchRange.Size = new System.Drawing.Size(137, 21);
            this.cbItemSearchRange.TabIndex = 19;
            this.cbItemSearchRange.SelectedIndexChanged += new System.EventHandler(this.cbItemSearchRange_SelectedIndexChanged);
            // 
            // label135
            // 
            this.label135.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(430, 33);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(39, 13);
            this.label135.TabIndex = 18;
            this.label135.Text = "Range";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(199, 33);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(49, 13);
            this.label83.TabIndex = 17;
            this.label83.Text = "Category";
            // 
            // cbItemSearchItemCategoryTypeList
            // 
            this.cbItemSearchItemCategoryTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemSearchItemCategoryTypeList.FormattingEnabled = true;
            this.cbItemSearchItemCategoryTypeList.Items.AddRange(new object[] { "---" });
            this.cbItemSearchItemCategoryTypeList.Location = new System.Drawing.Point(260, 30);
            this.cbItemSearchItemCategoryTypeList.Name = "cbItemSearchItemCategoryTypeList";
            this.cbItemSearchItemCategoryTypeList.Size = new System.Drawing.Size(137, 21);
            this.cbItemSearchItemCategoryTypeList.TabIndex = 16;
            this.cbItemSearchItemCategoryTypeList.SelectedIndexChanged += new System.EventHandler(this.cbItemSearchItemArmorSlotTypeList_SelectedIndexChanged);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(8, 33);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(55, 13);
            this.label51.TabIndex = 15;
            this.label51.Text = "Armor Slot";
            // 
            // cbItemSearchItemArmorSlotTypeList
            // 
            this.cbItemSearchItemArmorSlotTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemSearchItemArmorSlotTypeList.FormattingEnabled = true;
            this.cbItemSearchItemArmorSlotTypeList.Items.AddRange(new object[] { "Any", "Head (1)", "Neck (2)", "Chest (3)", "Waist (4)", "Legs (5)", "Hands (6)", "Feet (7)", "Arms (8)", "Back (9)", "Ear (10)", "Finger (11)", "Undershirt (12)", "Underpants (13)", "Mainhand (14)", "Offhand (15)", "TwoHanded (16)", "OneHanded (17)", "Ranged (18)", "Ammunition (19)", "Shield (20)", "Instrument (21)", "Bag (22)", "Face (23)", "Hair (24)", "Glasses (25)", "Reserved (26)", "Tail (27)", "Body (28)", "Beard (29)", "Backpack (30)", "Cosplay (31)" });
            this.cbItemSearchItemArmorSlotTypeList.Location = new System.Drawing.Point(69, 30);
            this.cbItemSearchItemArmorSlotTypeList.Name = "cbItemSearchItemArmorSlotTypeList";
            this.cbItemSearchItemArmorSlotTypeList.Size = new System.Drawing.Size(124, 21);
            this.cbItemSearchItemArmorSlotTypeList.TabIndex = 14;
            this.cbItemSearchItemArmorSlotTypeList.SelectedIndexChanged += new System.EventHandler(this.cbItemSearchItemArmorSlotTypeList_SelectedIndexChanged);
            // 
            // btnFindItemSkill
            // 
            this.btnFindItemSkill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFindItemSkill.BackColor = System.Drawing.SystemColors.Control;
            this.btnFindItemSkill.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFindItemSkill.Location = new System.Drawing.Point(97, 448);
            this.btnFindItemSkill.Name = "btnFindItemSkill";
            this.btnFindItemSkill.Size = new System.Drawing.Size(111, 23);
            this.btnFindItemSkill.TabIndex = 12;
            this.btnFindItemSkill.Text = "Find related Skills";
            this.btnFindItemSkill.UseVisualStyleBackColor = false;
            this.btnFindItemSkill.Click += new System.EventHandler(this.BtnFindItemSkill_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.groupBox1.Controls.Add(this.lItemTags);
            this.groupBox1.Controls.Add(this.label127);
            this.groupBox1.Controls.Add(this.lItemAddGMCommand);
            this.groupBox1.Controls.Add(this.itemIcon);
            this.groupBox1.Controls.Add(this.rtItemDesc);
            this.groupBox1.Controls.Add(this.lItemLevel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lItemCategory);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lItemName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lItemID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.groupBox1.Location = new System.Drawing.Point(618, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 470);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item Info";
            // 
            // lItemTags
            // 
            this.lItemTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lItemTags.AutoSize = true;
            this.lItemTags.Location = new System.Drawing.Point(6, 431);
            this.lItemTags.Name = "lItemTags";
            this.lItemTags.Size = new System.Drawing.Size(25, 13);
            this.lItemTags.TabIndex = 25;
            this.lItemTags.Text = "???";
            // 
            // label127
            // 
            this.label127.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(6, 418);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(31, 13);
            this.label127.TabIndex = 24;
            this.label127.Text = "Tags";
            // 
            // lItemAddGMCommand
            // 
            this.lItemAddGMCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lItemAddGMCommand.AutoSize = true;
            this.lItemAddGMCommand.Location = new System.Drawing.Point(6, 449);
            this.lItemAddGMCommand.Name = "lItemAddGMCommand";
            this.lItemAddGMCommand.Size = new System.Drawing.Size(49, 13);
            this.lItemAddGMCommand.TabIndex = 12;
            this.lItemAddGMCommand.Text = "/additem";
            // 
            // itemIcon
            // 
            this.itemIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.itemIcon.Location = new System.Drawing.Point(232, 14);
            this.itemIcon.Name = "itemIcon";
            this.itemIcon.Size = new System.Drawing.Size(64, 64);
            this.itemIcon.TabIndex = 11;
            this.itemIcon.Text = "???";
            this.itemIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtItemDesc
            // 
            this.rtItemDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rtItemDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.rtItemDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.rtItemDesc.Location = new System.Drawing.Point(9, 81);
            this.rtItemDesc.Name = "rtItemDesc";
            this.rtItemDesc.Size = new System.Drawing.Size(287, 334);
            this.rtItemDesc.TabIndex = 10;
            this.rtItemDesc.Text = "";
            // 
            // lItemLevel
            // 
            this.lItemLevel.AutoSize = true;
            this.lItemLevel.Location = new System.Drawing.Point(58, 65);
            this.lItemLevel.Name = "lItemLevel";
            this.lItemLevel.Size = new System.Drawing.Size(31, 13);
            this.lItemLevel.TabIndex = 8;
            this.lItemLevel.Text = "none";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Level";
            // 
            // lItemCategory
            // 
            this.lItemCategory.AutoSize = true;
            this.lItemCategory.Location = new System.Drawing.Point(58, 42);
            this.lItemCategory.Name = "lItemCategory";
            this.lItemCategory.Size = new System.Drawing.Size(13, 13);
            this.lItemCategory.TabIndex = 5;
            this.lItemCategory.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Category";
            // 
            // lItemName
            // 
            this.lItemName.AutoSize = true;
            this.lItemName.Location = new System.Drawing.Point(58, 29);
            this.lItemName.Name = "lItemName";
            this.lItemName.Size = new System.Drawing.Size(43, 13);
            this.lItemName.TabIndex = 3;
            this.lItemName.Text = "<none>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name";
            // 
            // lItemID
            // 
            this.lItemID.AutoSize = true;
            this.lItemID.Location = new System.Drawing.Point(58, 16);
            this.lItemID.Name = "lItemID";
            this.lItemID.Size = new System.Drawing.Size(13, 13);
            this.lItemID.TabIndex = 1;
            this.lItemID.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Index";
            // 
            // btnItemSearch
            // 
            this.btnItemSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemSearch.Enabled = false;
            this.btnItemSearch.Location = new System.Drawing.Point(533, 4);
            this.btnItemSearch.Name = "btnItemSearch";
            this.btnItemSearch.Size = new System.Drawing.Size(79, 23);
            this.btnItemSearch.TabIndex = 3;
            this.btnItemSearch.Text = "Search";
            this.btnItemSearch.UseVisualStyleBackColor = true;
            this.btnItemSearch.Click += new System.EventHandler(this.BtnItemSearch_Click);
            // 
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToDeleteRows = false;
            this.dgvItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvItem.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Item_ID, this.Item_Name_EN_US });
            this.dgvItem.Location = new System.Drawing.Point(11, 57);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.ReadOnly = true;
            this.dgvItem.RowHeadersVisible = false;
            this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItem.Size = new System.Drawing.Size(601, 385);
            this.dgvItem.TabIndex = 2;
            this.dgvItem.SelectionChanged += new System.EventHandler(this.DgvItemSearch_SelectionChanged);
            // 
            // Item_ID
            // 
            this.Item_ID.HeaderText = "ID";
            this.Item_ID.Name = "Item_ID";
            this.Item_ID.ReadOnly = true;
            this.Item_ID.Width = 43;
            // 
            // Item_Name_EN_US
            // 
            this.Item_Name_EN_US.FillWeight = 200F;
            this.Item_Name_EN_US.HeaderText = "Name";
            this.Item_Name_EN_US.Name = "Item_Name_EN_US";
            this.Item_Name_EN_US.ReadOnly = true;
            this.Item_Name_EN_US.Width = 60;
            // 
            // btnFindItemInLoot
            // 
            this.btnFindItemInLoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFindItemInLoot.BackColor = System.Drawing.SystemColors.Control;
            this.btnFindItemInLoot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFindItemInLoot.Location = new System.Drawing.Point(11, 448);
            this.btnFindItemInLoot.Name = "btnFindItemInLoot";
            this.btnFindItemInLoot.Size = new System.Drawing.Size(80, 23);
            this.btnFindItemInLoot.TabIndex = 9;
            this.btnFindItemInLoot.Text = "Find in Loot";
            this.btnFindItemInLoot.UseVisualStyleBackColor = false;
            this.btnFindItemInLoot.Click += new System.EventHandler(this.BtnFindItemInLoot_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search in Item ID, Name or description";
            // 
            // tItemSearch
            // 
            this.tItemSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tItemSearch.Location = new System.Drawing.Point(197, 6);
            this.tItemSearch.Name = "tItemSearch";
            this.tItemSearch.Size = new System.Drawing.Size(330, 20);
            this.tItemSearch.TabIndex = 0;
            this.tItemSearch.TextChanged += new System.EventHandler(this.TItemSearch_TextChanged);
            this.tItemSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TItemSearch_KeyDown);
            // 
            // tpLoot
            // 
            this.tpLoot.Controls.Add(this.groupBox11);
            this.tpLoot.Controls.Add(this.btnLootSearch);
            this.tpLoot.Controls.Add(this.label7);
            this.tpLoot.Controls.Add(this.tLootSearch);
            this.tpLoot.Controls.Add(this.dgvLoot);
            this.tpLoot.Location = new System.Drawing.Point(4, 22);
            this.tpLoot.Name = "tpLoot";
            this.tpLoot.Padding = new System.Windows.Forms.Padding(3);
            this.tpLoot.Size = new System.Drawing.Size(926, 485);
            this.tpLoot.TabIndex = 2;
            this.tpLoot.Text = "Loot";
            this.tpLoot.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.groupBox11.Controls.Add(this.LLootPackGroupNumber);
            this.groupBox11.Controls.Add(this.label74);
            this.groupBox11.Controls.Add(this.LLootGroupPackID);
            this.groupBox11.Controls.Add(this.label101);
            this.groupBox11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.groupBox11.Location = new System.Drawing.Point(696, 11);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(221, 467);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Loot Pack Info";
            // 
            // LLootPackGroupNumber
            // 
            this.LLootPackGroupNumber.AutoSize = true;
            this.LLootPackGroupNumber.Location = new System.Drawing.Point(58, 31);
            this.LLootPackGroupNumber.Name = "LLootPackGroupNumber";
            this.LLootPackGroupNumber.Size = new System.Drawing.Size(13, 13);
            this.LLootPackGroupNumber.TabIndex = 3;
            this.LLootPackGroupNumber.Text = "0";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(6, 31);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(36, 13);
            this.label74.TabIndex = 2;
            this.label74.Text = "Group";
            // 
            // LLootGroupPackID
            // 
            this.LLootGroupPackID.AutoSize = true;
            this.LLootGroupPackID.Location = new System.Drawing.Point(58, 16);
            this.LLootGroupPackID.Name = "LLootGroupPackID";
            this.LLootGroupPackID.Size = new System.Drawing.Size(13, 13);
            this.LLootGroupPackID.TabIndex = 1;
            this.LLootGroupPackID.Text = "0";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(6, 16);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(33, 13);
            this.label101.TabIndex = 0;
            this.label101.Text = "Index";
            // 
            // btnLootSearch
            // 
            this.btnLootSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLootSearch.Enabled = false;
            this.btnLootSearch.Location = new System.Drawing.Point(611, 11);
            this.btnLootSearch.Name = "btnLootSearch";
            this.btnLootSearch.Size = new System.Drawing.Size(79, 23);
            this.btnLootSearch.TabIndex = 6;
            this.btnLootSearch.Text = "Search";
            this.btnLootSearch.UseVisualStyleBackColor = true;
            this.btnLootSearch.Click += new System.EventHandler(this.BtnLootSearch_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Search Loot Pack ID";
            // 
            // tLootSearch
            // 
            this.tLootSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tLootSearch.Location = new System.Drawing.Point(121, 11);
            this.tLootSearch.Name = "tLootSearch";
            this.tLootSearch.Size = new System.Drawing.Size(484, 20);
            this.tLootSearch.TabIndex = 4;
            this.tLootSearch.TextChanged += new System.EventHandler(this.TLootSearch_TextChanged);
            this.tLootSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TLootSearch_KeyDown);
            // 
            // dgvLoot
            // 
            this.dgvLoot.AllowUserToAddRows = false;
            this.dgvLoot.AllowUserToDeleteRows = false;
            this.dgvLoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoot.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLoot.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLoot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.Column2, this.Column1, this.Column9, this.Column3, this.Column4, this.Column5, this.Column6, this.Column7, this.Column8 });
            this.dgvLoot.Location = new System.Drawing.Point(8, 42);
            this.dgvLoot.Name = "dgvLoot";
            this.dgvLoot.ReadOnly = true;
            this.dgvLoot.RowHeadersVisible = false;
            this.dgvLoot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoot.Size = new System.Drawing.Size(682, 437);
            this.dgvLoot.TabIndex = 3;
            this.dgvLoot.SelectionChanged += new System.EventHandler(this.DgvLoot_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 43;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Loot Pack ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 95;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Item ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 66;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Name";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Drop Rate";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 81;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Min";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 49;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Max";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 52;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Grade ID";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 75;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Always Drop";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 91;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Group";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 61;
            // 
            // tpNPCs
            // 
            this.tpNPCs.Controls.Add(this.groupBox13);
            this.tpNPCs.Controls.Add(this.btnSearchNPC);
            this.tpNPCs.Controls.Add(this.label39);
            this.tpNPCs.Controls.Add(this.tSearchNPC);
            this.tpNPCs.Controls.Add(this.dgvNPCs);
            this.tpNPCs.Location = new System.Drawing.Point(4, 22);
            this.tpNPCs.Name = "tpNPCs";
            this.tpNPCs.Padding = new System.Windows.Forms.Padding(3);
            this.tpNPCs.Size = new System.Drawing.Size(926, 485);
            this.tpNPCs.TabIndex = 6;
            this.tpNPCs.Text = "NPCs";
            this.tpNPCs.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.groupBox13.Controls.Add(this.btnShowNPCsOnMap);
            this.groupBox13.Controls.Add(this.lNPCTags);
            this.groupBox13.Controls.Add(this.label125);
            this.groupBox13.Controls.Add(this.lGMNPCSpawn);
            this.groupBox13.Controls.Add(this.lNPCTemplate);
            this.groupBox13.Controls.Add(this.label81);
            this.groupBox13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.groupBox13.Location = new System.Drawing.Point(696, 10);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(221, 467);
            this.groupBox13.TabIndex = 13;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "NPC Info";
            // 
            // btnShowNPCsOnMap
            // 
            this.btnShowNPCsOnMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowNPCsOnMap.ForeColor = System.Drawing.Color.Black;
            this.btnShowNPCsOnMap.Location = new System.Drawing.Point(9, 439);
            this.btnShowNPCsOnMap.Name = "btnShowNPCsOnMap";
            this.btnShowNPCsOnMap.Size = new System.Drawing.Size(206, 22);
            this.btnShowNPCsOnMap.TabIndex = 26;
            this.btnShowNPCsOnMap.Text = "Find Selected";
            this.btnShowNPCsOnMap.UseVisualStyleBackColor = true;
            this.btnShowNPCsOnMap.Click += new System.EventHandler(this.btnShowNPCsOnMap_Click);
            // 
            // lNPCTags
            // 
            this.lNPCTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lNPCTags.AutoSize = true;
            this.lNPCTags.Location = new System.Drawing.Point(6, 356);
            this.lNPCTags.Name = "lNPCTags";
            this.lNPCTags.Size = new System.Drawing.Size(25, 13);
            this.lNPCTags.TabIndex = 25;
            this.lNPCTags.Text = "???";
            // 
            // label125
            // 
            this.label125.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(6, 333);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(31, 13);
            this.label125.TabIndex = 24;
            this.label125.Text = "Tags";
            // 
            // lGMNPCSpawn
            // 
            this.lGMNPCSpawn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lGMNPCSpawn.AutoSize = true;
            this.lGMNPCSpawn.Location = new System.Drawing.Point(6, 407);
            this.lGMNPCSpawn.Name = "lGMNPCSpawn";
            this.lGMNPCSpawn.Size = new System.Drawing.Size(43, 13);
            this.lGMNPCSpawn.TabIndex = 13;
            this.lGMNPCSpawn.Text = "/spawn";
            // 
            // lNPCTemplate
            // 
            this.lNPCTemplate.AutoSize = true;
            this.lNPCTemplate.Location = new System.Drawing.Point(58, 16);
            this.lNPCTemplate.Name = "lNPCTemplate";
            this.lNPCTemplate.Size = new System.Drawing.Size(13, 13);
            this.lNPCTemplate.TabIndex = 1;
            this.lNPCTemplate.Text = "0";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(6, 16);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(51, 13);
            this.label81.TabIndex = 0;
            this.label81.Text = "Template";
            // 
            // btnSearchNPC
            // 
            this.btnSearchNPC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchNPC.Enabled = false;
            this.btnSearchNPC.Location = new System.Drawing.Point(611, 8);
            this.btnSearchNPC.Name = "btnSearchNPC";
            this.btnSearchNPC.Size = new System.Drawing.Size(79, 23);
            this.btnSearchNPC.TabIndex = 10;
            this.btnSearchNPC.Text = "Search";
            this.btnSearchNPC.UseVisualStyleBackColor = true;
            this.btnSearchNPC.Click += new System.EventHandler(this.BtnSearchNPC_Click);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(8, 13);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(172, 13);
            this.label39.TabIndex = 9;
            this.label39.Text = "Search NPC ID, Name or Model ID";
            // 
            // tSearchNPC
            // 
            this.tSearchNPC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tSearchNPC.Location = new System.Drawing.Point(186, 10);
            this.tSearchNPC.Name = "tSearchNPC";
            this.tSearchNPC.Size = new System.Drawing.Size(419, 20);
            this.tSearchNPC.TabIndex = 8;
            this.tSearchNPC.TextChanged += new System.EventHandler(this.TSearchNPC_TextChanged);
            this.tSearchNPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TSearchNPC_KeyDown);
            // 
            // dgvNPCs
            // 
            this.dgvNPCs.AllowUserToAddRows = false;
            this.dgvNPCs.AllowUserToDeleteRows = false;
            this.dgvNPCs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNPCs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNPCs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvNPCs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNPCs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn14, this.dataGridViewTextBoxColumn17, this.Column18, this.Column17, this.Column16, this.Column21, this.Column34 });
            this.dgvNPCs.Location = new System.Drawing.Point(8, 41);
            this.dgvNPCs.Name = "dgvNPCs";
            this.dgvNPCs.ReadOnly = true;
            this.dgvNPCs.RowHeadersVisible = false;
            this.dgvNPCs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNPCs.Size = new System.Drawing.Size(682, 437);
            this.dgvNPCs.TabIndex = 7;
            this.dgvNPCs.SelectionChanged += new System.EventHandler(this.DgvNPCs_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "ID";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 43;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "Name";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.Width = 60;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Level";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Width = 58;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Kind ID";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 67;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Grade ID";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Width = 75;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "Faction ID";
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            this.Column21.Width = 81;
            // 
            // Column34
            // 
            this.Column34.HeaderText = "Spawns";
            this.Column34.Name = "Column34";
            this.Column34.ReadOnly = true;
            this.Column34.Width = 70;
            // 
            // tpQuests
            // 
            this.tpQuests.Controls.Add(this.panel1);
            this.tpQuests.Controls.Add(this.splitContainer1);
            this.tpQuests.Location = new System.Drawing.Point(4, 22);
            this.tpQuests.Name = "tpQuests";
            this.tpQuests.Padding = new System.Windows.Forms.Padding(3);
            this.tpQuests.Size = new System.Drawing.Size(926, 485);
            this.tpQuests.TabIndex = 9;
            this.tpQuests.Text = "Quests";
            this.tpQuests.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label89);
            this.panel1.Controls.Add(this.tQuestSearch);
            this.panel1.Controls.Add(this.btnQuestsSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(920, 32);
            this.panel1.TabIndex = 14;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(5, 9);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(129, 13);
            this.label89.TabIndex = 10;
            this.label89.Text = "Search Quest ID or Name";
            // 
            // tQuestSearch
            // 
            this.tQuestSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tQuestSearch.Location = new System.Drawing.Point(140, 6);
            this.tQuestSearch.Name = "tQuestSearch";
            this.tQuestSearch.Size = new System.Drawing.Size(690, 20);
            this.tQuestSearch.TabIndex = 9;
            this.tQuestSearch.TextChanged += new System.EventHandler(this.tQuestSearch_TextChanged);
            this.tQuestSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tQuestSearch_KeyDown);
            // 
            // btnQuestsSearch
            // 
            this.btnQuestsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuestsSearch.Enabled = false;
            this.btnQuestsSearch.Location = new System.Drawing.Point(836, 4);
            this.btnQuestsSearch.Name = "btnQuestsSearch";
            this.btnQuestsSearch.Size = new System.Drawing.Size(79, 23);
            this.btnQuestsSearch.TabIndex = 11;
            this.btnQuestsSearch.Text = "Search";
            this.btnQuestsSearch.UseVisualStyleBackColor = true;
            this.btnQuestsSearch.Click += new System.EventHandler(this.btnQuestsSearch_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox12);
            this.splitContainer1.Panel1.Controls.Add(this.dgvQuests);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnQuestFindRelatedOnMap);
            this.splitContainer1.Panel2.Controls.Add(this.cbQuestWorkflowHideEmpty);
            this.splitContainer1.Panel2.Controls.Add(this.tvQuestWorkflow);
            this.splitContainer1.Size = new System.Drawing.Size(920, 446);
            this.splitContainer1.SplitterDistance = 330;
            this.splitContainer1.TabIndex = 13;
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.rtQuestText);
            this.groupBox12.Location = new System.Drawing.Point(3, 187);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(324, 256);
            this.groupBox12.TabIndex = 9;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Quest Text";
            // 
            // rtQuestText
            // 
            this.rtQuestText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.rtQuestText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtQuestText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.rtQuestText.Location = new System.Drawing.Point(3, 16);
            this.rtQuestText.Name = "rtQuestText";
            this.rtQuestText.Size = new System.Drawing.Size(318, 237);
            this.rtQuestText.TabIndex = 11;
            this.rtQuestText.Text = "";
            // 
            // dgvQuests
            // 
            this.dgvQuests.AllowUserToAddRows = false;
            this.dgvQuests.AllowUserToDeleteRows = false;
            this.dgvQuests.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQuests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvQuests.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvQuests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn20, this.Column35, this.Column19, this.Column20, this.Column36 });
            this.dgvQuests.Location = new System.Drawing.Point(3, 5);
            this.dgvQuests.Name = "dgvQuests";
            this.dgvQuests.ReadOnly = true;
            this.dgvQuests.RowHeadersVisible = false;
            this.dgvQuests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuests.Size = new System.Drawing.Size(324, 176);
            this.dgvQuests.TabIndex = 8;
            this.dgvQuests.SelectionChanged += new System.EventHandler(this.dgvQuests_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.HeaderText = "ID";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Width = 43;
            // 
            // Column35
            // 
            this.Column35.HeaderText = "Name";
            this.Column35.Name = "Column35";
            this.Column35.ReadOnly = true;
            this.Column35.Width = 60;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "Level";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Width = 58;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "ZoneId";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.Width = 66;
            // 
            // Column36
            // 
            this.Column36.HeaderText = "Category";
            this.Column36.Name = "Column36";
            this.Column36.ReadOnly = true;
            this.Column36.Width = 74;
            // 
            // btnQuestFindRelatedOnMap
            // 
            this.btnQuestFindRelatedOnMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuestFindRelatedOnMap.ForeColor = System.Drawing.Color.Black;
            this.btnQuestFindRelatedOnMap.Location = new System.Drawing.Point(383, 420);
            this.btnQuestFindRelatedOnMap.Name = "btnQuestFindRelatedOnMap";
            this.btnQuestFindRelatedOnMap.Size = new System.Drawing.Size(198, 22);
            this.btnQuestFindRelatedOnMap.TabIndex = 27;
            this.btnQuestFindRelatedOnMap.Text = "Show related map points";
            this.btnQuestFindRelatedOnMap.UseVisualStyleBackColor = true;
            this.btnQuestFindRelatedOnMap.Click += new System.EventHandler(this.btnQuestFindRelatedOnMap_Click);
            // 
            // cbQuestWorkflowHideEmpty
            // 
            this.cbQuestWorkflowHideEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbQuestWorkflowHideEmpty.AutoSize = true;
            this.cbQuestWorkflowHideEmpty.Checked = true;
            this.cbQuestWorkflowHideEmpty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbQuestWorkflowHideEmpty.Location = new System.Drawing.Point(2, 424);
            this.cbQuestWorkflowHideEmpty.Name = "cbQuestWorkflowHideEmpty";
            this.cbQuestWorkflowHideEmpty.Size = new System.Drawing.Size(113, 17);
            this.cbQuestWorkflowHideEmpty.TabIndex = 1;
            this.cbQuestWorkflowHideEmpty.Text = "Hide empty values";
            this.cbQuestWorkflowHideEmpty.UseVisualStyleBackColor = true;
            // 
            // tvQuestWorkflow
            // 
            this.tvQuestWorkflow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.tvQuestWorkflow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.tvQuestWorkflow.Location = new System.Drawing.Point(3, 5);
            this.tvQuestWorkflow.Name = "tvQuestWorkflow";
            this.tvQuestWorkflow.Size = new System.Drawing.Size(578, 413);
            this.tvQuestWorkflow.TabIndex = 0;
            this.tvQuestWorkflow.DoubleClick += new System.EventHandler(this.tvQuestWorkflow_DoubleClick);
            // 
            // tpSkills
            // 
            this.tpSkills.Controls.Add(this.tcSkillInfo);
            this.tpSkills.Controls.Add(this.btnSkillSearch);
            this.tpSkills.Controls.Add(this.dgvSkills);
            this.tpSkills.Controls.Add(this.label9);
            this.tpSkills.Controls.Add(this.tSkillSearch);
            this.tpSkills.Location = new System.Drawing.Point(4, 22);
            this.tpSkills.Name = "tpSkills";
            this.tpSkills.Padding = new System.Windows.Forms.Padding(3);
            this.tpSkills.Size = new System.Drawing.Size(926, 485);
            this.tpSkills.TabIndex = 3;
            this.tpSkills.Text = "Skill";
            this.tpSkills.UseVisualStyleBackColor = true;
            this.tpSkills.Click += new System.EventHandler(this.tpSkills_Click);
            // 
            // tcSkillInfo
            // 
            this.tcSkillInfo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcSkillInfo.Controls.Add(this.tpSkillInfo);
            this.tcSkillInfo.Controls.Add(this.tpSkillItems);
            this.tcSkillInfo.Controls.Add(this.tpSkillExecution);
            this.tcSkillInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.tcSkillInfo.Location = new System.Drawing.Point(562, 3);
            this.tcSkillInfo.Multiline = true;
            this.tcSkillInfo.Name = "tcSkillInfo";
            this.tcSkillInfo.SelectedIndex = 0;
            this.tcSkillInfo.Size = new System.Drawing.Size(361, 479);
            this.tcSkillInfo.TabIndex = 9;
            // 
            // tpSkillInfo
            // 
            this.tpSkillInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.tpSkillInfo.Controls.Add(this.groupBox2);
            this.tpSkillInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.tpSkillInfo.Location = new System.Drawing.Point(4, 4);
            this.tpSkillInfo.Name = "tpSkillInfo";
            this.tpSkillInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpSkillInfo.Size = new System.Drawing.Size(353, 453);
            this.tpSkillInfo.TabIndex = 0;
            this.tpSkillInfo.Text = "Skill";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.groupBox2.Controls.Add(this.lSkillTags);
            this.groupBox2.Controls.Add(this.label123);
            this.groupBox2.Controls.Add(this.rtSkillDescription);
            this.groupBox2.Controls.Add(this.lSkillGCD);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.lSkillCooldown);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.lSkillLabor);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lSkillMana);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lSkillCost);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.skillIcon);
            this.groupBox2.Controls.Add(this.lSkillName);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.lSkillID);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 447);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Skill Info";
            // 
            // lSkillTags
            // 
            this.lSkillTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lSkillTags.AutoSize = true;
            this.lSkillTags.Location = new System.Drawing.Point(6, 426);
            this.lSkillTags.Name = "lSkillTags";
            this.lSkillTags.Size = new System.Drawing.Size(25, 13);
            this.lSkillTags.TabIndex = 23;
            this.lSkillTags.Text = "???";
            // 
            // label123
            // 
            this.label123.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(6, 413);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(31, 13);
            this.label123.TabIndex = 22;
            this.label123.Text = "Tags";
            // 
            // rtSkillDescription
            // 
            this.rtSkillDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.rtSkillDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.rtSkillDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(192)))), ((int)(((byte)(171)))));
            this.rtSkillDescription.Location = new System.Drawing.Point(9, 120);
            this.rtSkillDescription.Name = "rtSkillDescription";
            this.rtSkillDescription.Size = new System.Drawing.Size(332, 290);
            this.rtSkillDescription.TabIndex = 10;
            this.rtSkillDescription.Text = "";
            // 
            // lSkillGCD
            // 
            this.lSkillGCD.AutoSize = true;
            this.lSkillGCD.Location = new System.Drawing.Point(97, 104);
            this.lSkillGCD.Name = "lSkillGCD";
            this.lSkillGCD.Size = new System.Drawing.Size(13, 13);
            this.lSkillGCD.TabIndex = 21;
            this.lSkillGCD.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 104);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(87, 13);
            this.label20.TabIndex = 20;
            this.label20.Text = "Global Cooldown";
            // 
            // lSkillCooldown
            // 
            this.lSkillCooldown.AutoSize = true;
            this.lSkillCooldown.Location = new System.Drawing.Point(97, 91);
            this.lSkillCooldown.Name = "lSkillCooldown";
            this.lSkillCooldown.Size = new System.Drawing.Size(13, 13);
            this.lSkillCooldown.TabIndex = 19;
            this.lSkillCooldown.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 91);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "Cooldown";
            // 
            // lSkillLabor
            // 
            this.lSkillLabor.AutoSize = true;
            this.lSkillLabor.Location = new System.Drawing.Point(97, 78);
            this.lSkillLabor.Name = "lSkillLabor";
            this.lSkillLabor.Size = new System.Drawing.Size(13, 13);
            this.lSkillLabor.TabIndex = 17;
            this.lSkillLabor.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Labor";
            // 
            // lSkillMana
            // 
            this.lSkillMana.AutoSize = true;
            this.lSkillMana.Location = new System.Drawing.Point(97, 65);
            this.lSkillMana.Name = "lSkillMana";
            this.lSkillMana.Size = new System.Drawing.Size(13, 13);
            this.lSkillMana.TabIndex = 15;
            this.lSkillMana.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Mana";
            // 
            // lSkillCost
            // 
            this.lSkillCost.AutoSize = true;
            this.lSkillCost.Location = new System.Drawing.Point(97, 51);
            this.lSkillCost.Name = "lSkillCost";
            this.lSkillCost.Size = new System.Drawing.Size(13, 13);
            this.lSkillCost.TabIndex = 13;
            this.lSkillCost.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Cost";
            // 
            // skillIcon
            // 
            this.skillIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skillIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.skillIcon.Location = new System.Drawing.Point(277, 51);
            this.skillIcon.Name = "skillIcon";
            this.skillIcon.Size = new System.Drawing.Size(64, 64);
            this.skillIcon.TabIndex = 11;
            this.skillIcon.Text = "???";
            this.skillIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lSkillName
            // 
            this.lSkillName.AutoSize = true;
            this.lSkillName.Location = new System.Drawing.Point(58, 29);
            this.lSkillName.Name = "lSkillName";
            this.lSkillName.Size = new System.Drawing.Size(43, 13);
            this.lSkillName.TabIndex = 3;
            this.lSkillName.Text = "<none>";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Name";
            // 
            // lSkillID
            // 
            this.lSkillID.AutoSize = true;
            this.lSkillID.Location = new System.Drawing.Point(58, 16);
            this.lSkillID.Name = "lSkillID";
            this.lSkillID.Size = new System.Drawing.Size(13, 13);
            this.lSkillID.TabIndex = 1;
            this.lSkillID.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(33, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Index";
            // 
            // tpSkillItems
            // 
            this.tpSkillItems.Controls.Add(this.dgvSkillProducts);
            this.tpSkillItems.Controls.Add(this.dgvSkillReagents);
            this.tpSkillItems.Controls.Add(this.label13);
            this.tpSkillItems.Controls.Add(this.labelSkillReagents);
            this.tpSkillItems.Location = new System.Drawing.Point(4, 4);
            this.tpSkillItems.Name = "tpSkillItems";
            this.tpSkillItems.Padding = new System.Windows.Forms.Padding(3);
            this.tpSkillItems.Size = new System.Drawing.Size(353, 453);
            this.tpSkillItems.TabIndex = 1;
            this.tpSkillItems.Text = "Items?";
            this.tpSkillItems.UseVisualStyleBackColor = true;
            // 
            // dgvSkillProducts
            // 
            this.dgvSkillProducts.AllowUserToAddRows = false;
            this.dgvSkillProducts.AllowUserToDeleteRows = false;
            this.dgvSkillProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSkillProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSkillProducts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSkillProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkillProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn9, this.dataGridViewTextBoxColumn10, this.dataGridViewTextBoxColumn11 });
            this.dgvSkillProducts.Location = new System.Drawing.Point(8, 313);
            this.dgvSkillProducts.Name = "dgvSkillProducts";
            this.dgvSkillProducts.ReadOnly = true;
            this.dgvSkillProducts.RowHeadersVisible = false;
            this.dgvSkillProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSkillProducts.Size = new System.Drawing.Size(338, 134);
            this.dgvSkillProducts.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Item ID";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 66;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.FillWeight = 200F;
            this.dataGridViewTextBoxColumn10.HeaderText = "Name";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 60;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 68;
            // 
            // dgvSkillReagents
            // 
            this.dgvSkillReagents.AllowUserToAddRows = false;
            this.dgvSkillReagents.AllowUserToDeleteRows = false;
            this.dgvSkillReagents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSkillReagents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSkillReagents.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSkillReagents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkillReagents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn6, this.dataGridViewTextBoxColumn7, this.dataGridViewTextBoxColumn8 });
            this.dgvSkillReagents.Location = new System.Drawing.Point(9, 26);
            this.dgvSkillReagents.Name = "dgvSkillReagents";
            this.dgvSkillReagents.ReadOnly = true;
            this.dgvSkillReagents.RowHeadersVisible = false;
            this.dgvSkillReagents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSkillReagents.Size = new System.Drawing.Size(338, 268);
            this.dgvSkillReagents.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Item ID";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 66;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 200F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Name";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 60;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 68;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 297);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Produced by this skill";
            // 
            // labelSkillReagents
            // 
            this.labelSkillReagents.AutoSize = true;
            this.labelSkillReagents.Location = new System.Drawing.Point(6, 7);
            this.labelSkillReagents.Name = "labelSkillReagents";
            this.labelSkillReagents.Size = new System.Drawing.Size(148, 13);
            this.labelSkillReagents.TabIndex = 0;
            this.labelSkillReagents.Text = "Required items to use this skill";
            // 
            // tpSkillExecution
            // 
            this.tpSkillExecution.Controls.Add(this.gbSkillPlotEventInfo);
            this.tpSkillExecution.Controls.Add(this.tvSkill);
            this.tpSkillExecution.Location = new System.Drawing.Point(4, 4);
            this.tpSkillExecution.Name = "tpSkillExecution";
            this.tpSkillExecution.Padding = new System.Windows.Forms.Padding(3);
            this.tpSkillExecution.Size = new System.Drawing.Size(353, 453);
            this.tpSkillExecution.TabIndex = 2;
            this.tpSkillExecution.Text = "Execution";
            this.tpSkillExecution.UseVisualStyleBackColor = true;
            // 
            // gbSkillPlotEventInfo
            // 
            this.gbSkillPlotEventInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventTargetUpdate);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventSourceUpdate);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventAoE);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventTickets);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP9);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP8);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP7);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP6);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP5);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP4);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP3);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP2);
            this.gbSkillPlotEventInfo.Controls.Add(this.lPlotEventP1);
            this.gbSkillPlotEventInfo.Location = new System.Drawing.Point(6, 307);
            this.gbSkillPlotEventInfo.Name = "gbSkillPlotEventInfo";
            this.gbSkillPlotEventInfo.Size = new System.Drawing.Size(341, 140);
            this.gbSkillPlotEventInfo.TabIndex = 1;
            this.gbSkillPlotEventInfo.TabStop = false;
            this.gbSkillPlotEventInfo.Text = "Parameters";
            // 
            // lPlotEventTargetUpdate
            // 
            this.lPlotEventTargetUpdate.AutoSize = true;
            this.lPlotEventTargetUpdate.Location = new System.Drawing.Point(6, 29);
            this.lPlotEventTargetUpdate.Name = "lPlotEventTargetUpdate";
            this.lPlotEventTargetUpdate.Size = new System.Drawing.Size(79, 13);
            this.lPlotEventTargetUpdate.TabIndex = 12;
            this.lPlotEventTargetUpdate.Text = "Target Update:";
            // 
            // lPlotEventSourceUpdate
            // 
            this.lPlotEventSourceUpdate.AutoSize = true;
            this.lPlotEventSourceUpdate.Location = new System.Drawing.Point(6, 16);
            this.lPlotEventSourceUpdate.Name = "lPlotEventSourceUpdate";
            this.lPlotEventSourceUpdate.Size = new System.Drawing.Size(82, 13);
            this.lPlotEventSourceUpdate.TabIndex = 11;
            this.lPlotEventSourceUpdate.Text = "Source Update:";
            // 
            // lPlotEventAoE
            // 
            this.lPlotEventAoE.AutoSize = true;
            this.lPlotEventAoE.Location = new System.Drawing.Point(124, 97);
            this.lPlotEventAoE.Name = "lPlotEventAoE";
            this.lPlotEventAoE.Size = new System.Drawing.Size(30, 13);
            this.lPlotEventAoE.TabIndex = 10;
            this.lPlotEventAoE.Text = "AoE:";
            // 
            // lPlotEventTickets
            // 
            this.lPlotEventTickets.AutoSize = true;
            this.lPlotEventTickets.Location = new System.Drawing.Point(6, 97);
            this.lPlotEventTickets.Name = "lPlotEventTickets";
            this.lPlotEventTickets.Size = new System.Drawing.Size(45, 13);
            this.lPlotEventTickets.TabIndex = 9;
            this.lPlotEventTickets.Text = "Tickets:";
            // 
            // lPlotEventP9
            // 
            this.lPlotEventP9.AutoSize = true;
            this.lPlotEventP9.Location = new System.Drawing.Point(233, 72);
            this.lPlotEventP9.Name = "lPlotEventP9";
            this.lPlotEventP9.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP9.TabIndex = 8;
            this.lPlotEventP9.Text = "9:";
            // 
            // lPlotEventP8
            // 
            this.lPlotEventP8.AutoSize = true;
            this.lPlotEventP8.Location = new System.Drawing.Point(233, 59);
            this.lPlotEventP8.Name = "lPlotEventP8";
            this.lPlotEventP8.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP8.TabIndex = 7;
            this.lPlotEventP8.Text = "8:";
            this.lPlotEventP8.Click += new System.EventHandler(this.label137_Click);
            // 
            // lPlotEventP7
            // 
            this.lPlotEventP7.AutoSize = true;
            this.lPlotEventP7.Location = new System.Drawing.Point(233, 46);
            this.lPlotEventP7.Name = "lPlotEventP7";
            this.lPlotEventP7.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP7.TabIndex = 6;
            this.lPlotEventP7.Text = "7:";
            // 
            // lPlotEventP6
            // 
            this.lPlotEventP6.AutoSize = true;
            this.lPlotEventP6.Location = new System.Drawing.Point(124, 72);
            this.lPlotEventP6.Name = "lPlotEventP6";
            this.lPlotEventP6.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP6.TabIndex = 5;
            this.lPlotEventP6.Text = "6:";
            // 
            // lPlotEventP5
            // 
            this.lPlotEventP5.AutoSize = true;
            this.lPlotEventP5.Location = new System.Drawing.Point(124, 59);
            this.lPlotEventP5.Name = "lPlotEventP5";
            this.lPlotEventP5.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP5.TabIndex = 4;
            this.lPlotEventP5.Text = "5:";
            // 
            // lPlotEventP4
            // 
            this.lPlotEventP4.AutoSize = true;
            this.lPlotEventP4.Location = new System.Drawing.Point(124, 46);
            this.lPlotEventP4.Name = "lPlotEventP4";
            this.lPlotEventP4.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP4.TabIndex = 3;
            this.lPlotEventP4.Text = "4:";
            // 
            // lPlotEventP3
            // 
            this.lPlotEventP3.AutoSize = true;
            this.lPlotEventP3.Location = new System.Drawing.Point(6, 72);
            this.lPlotEventP3.Name = "lPlotEventP3";
            this.lPlotEventP3.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP3.TabIndex = 2;
            this.lPlotEventP3.Text = "3:";
            // 
            // lPlotEventP2
            // 
            this.lPlotEventP2.AutoSize = true;
            this.lPlotEventP2.Location = new System.Drawing.Point(6, 59);
            this.lPlotEventP2.Name = "lPlotEventP2";
            this.lPlotEventP2.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP2.TabIndex = 1;
            this.lPlotEventP2.Text = "2:";
            // 
            // lPlotEventP1
            // 
            this.lPlotEventP1.AutoSize = true;
            this.lPlotEventP1.Location = new System.Drawing.Point(6, 46);
            this.lPlotEventP1.Name = "lPlotEventP1";
            this.lPlotEventP1.Size = new System.Drawing.Size(16, 13);
            this.lPlotEventP1.TabIndex = 0;
            this.lPlotEventP1.Text = "1:";
            // 
            // tvSkill
            // 
            this.tvSkill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSkill.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tvSkill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvSkill.ImageIndex = 0;
            this.tvSkill.ImageList = this.ilMiniIcons;
            this.tvSkill.ItemHeight = 24;
            this.tvSkill.Location = new System.Drawing.Point(6, 6);
            this.tvSkill.Name = "tvSkill";
            treeNode1.Name = "SkillNode";
            treeNode1.Text = "Skill";
            this.tvSkill.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode1 });
            this.tvSkill.SelectedImageIndex = 0;
            this.tvSkill.Size = new System.Drawing.Size(341, 295);
            this.tvSkill.TabIndex = 0;
            this.tvSkill.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSkill_AfterSelect);
            this.tvSkill.DoubleClick += new System.EventHandler(this.tvSkill_DoubleClick);
            // 
            // ilMiniIcons
            // 
            this.ilMiniIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMiniIcons.ImageStream")));
            this.ilMiniIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMiniIcons.Images.SetKeyName(0, "icon_question_yellow.png");
            this.ilMiniIcons.Images.SetKeyName(1, "icon_exclamation_yellow.png");
            this.ilMiniIcons.Images.SetKeyName(2, "icon_exclamation_green.png");
            this.ilMiniIcons.Images.SetKeyName(3, "icon_exclamation_blue.png");
            // 
            // btnSkillSearch
            // 
            this.btnSkillSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSkillSearch.Enabled = false;
            this.btnSkillSearch.Location = new System.Drawing.Point(477, 4);
            this.btnSkillSearch.Name = "btnSkillSearch";
            this.btnSkillSearch.Size = new System.Drawing.Size(79, 23);
            this.btnSkillSearch.TabIndex = 7;
            this.btnSkillSearch.Text = "Search";
            this.btnSkillSearch.UseVisualStyleBackColor = true;
            this.btnSkillSearch.Click += new System.EventHandler(this.BtnSkillSearch_Click);
            // 
            // dgvSkills
            // 
            this.dgvSkills.AllowUserToAddRows = false;
            this.dgvSkills.AllowUserToDeleteRows = false;
            this.dgvSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSkills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSkills.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSkills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkills.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.Column10 });
            this.dgvSkills.Location = new System.Drawing.Point(6, 32);
            this.dgvSkills.Name = "dgvSkills";
            this.dgvSkills.ReadOnly = true;
            this.dgvSkills.RowHeadersVisible = false;
            this.dgvSkills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSkills.Size = new System.Drawing.Size(550, 447);
            this.dgvSkills.TabIndex = 6;
            this.dgvSkills.SelectionChanged += new System.EventHandler(this.DgvSkills_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 43;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 200F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Description";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 85;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(188, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Search in Skill ID, Name or description";
            // 
            // tSkillSearch
            // 
            this.tSkillSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tSkillSearch.Location = new System.Drawing.Point(201, 6);
            this.tSkillSearch.Name = "tSkillSearch";
            this.tSkillSearch.Size = new System.Drawing.Size(270, 20);
            this.tSkillSearch.TabIndex = 4;
            this.tSkillSearch.TextChanged += new System.EventHandler(this.TSkillSearch_TextChanged);
            this.tSkillSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TSkillSearch_KeyDown);
            // 
            // tpZones
            // 
            this.tpZones.Controls.Add(this.groupBox5);
            this.tpZones.Controls.Add(this.groupBox4);
            this.tpZones.Controls.Add(this.groupBox3);
            this.tpZones.Controls.Add(this.btnZonesShowAll);
            this.tpZones.Controls.Add(this.btnSearchZones);
            this.tpZones.Controls.Add(this.dgvZones);
            this.tpZones.Controls.Add(this.label10);
            this.tpZones.Controls.Add(this.tZonesSearch);
            this.tpZones.Location = new System.Drawing.Point(4, 22);
            this.tpZones.Name = "tpZones";
            this.tpZones.Padding = new System.Windows.Forms.Padding(3);
            this.tpZones.Size = new System.Drawing.Size(926, 485);
            this.tpZones.TabIndex = 5;
            this.tpZones.Text = "Zones";
            this.tpZones.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label33);
            this.groupBox5.Controls.Add(this.lWorldGroupImageSizeAndPos);
            this.groupBox5.Controls.Add(this.lWorldGroupTargetID);
            this.groupBox5.Controls.Add(this.lWorldGroupName);
            this.groupBox5.Controls.Add(this.label43);
            this.groupBox5.Controls.Add(this.label35);
            this.groupBox5.Controls.Add(this.lWorldGroupImageMap);
            this.groupBox5.Controls.Add(this.label37);
            this.groupBox5.Controls.Add(this.label40);
            this.groupBox5.Controls.Add(this.lWorldGroupSizeAndPos);
            this.groupBox5.Location = new System.Drawing.Point(616, 360);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(301, 102);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "World Group Info";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 49);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(36, 13);
            this.label33.TabIndex = 40;
            this.label33.Text = "Image";
            // 
            // lWorldGroupImageSizeAndPos
            // 
            this.lWorldGroupImageSizeAndPos.AutoSize = true;
            this.lWorldGroupImageSizeAndPos.Location = new System.Drawing.Point(92, 49);
            this.lWorldGroupImageSizeAndPos.Name = "lWorldGroupImageSizeAndPos";
            this.lWorldGroupImageSizeAndPos.Size = new System.Drawing.Size(43, 13);
            this.lWorldGroupImageSizeAndPos.TabIndex = 41;
            this.lWorldGroupImageSizeAndPos.Text = "<none>";
            // 
            // lWorldGroupTargetID
            // 
            this.lWorldGroupTargetID.AutoSize = true;
            this.lWorldGroupTargetID.Location = new System.Drawing.Point(92, 82);
            this.lWorldGroupTargetID.Name = "lWorldGroupTargetID";
            this.lWorldGroupTargetID.Size = new System.Drawing.Size(13, 13);
            this.lWorldGroupTargetID.TabIndex = 35;
            this.lWorldGroupTargetID.Text = "0";
            // 
            // lWorldGroupName
            // 
            this.lWorldGroupName.AutoSize = true;
            this.lWorldGroupName.Location = new System.Drawing.Point(92, 16);
            this.lWorldGroupName.Name = "lWorldGroupName";
            this.lWorldGroupName.Size = new System.Drawing.Size(43, 13);
            this.lWorldGroupName.TabIndex = 36;
            this.lWorldGroupName.Text = "<none>";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 82);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(52, 13);
            this.label43.TabIndex = 34;
            this.label43.Text = "Target ID";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 16);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(35, 13);
            this.label35.TabIndex = 34;
            this.label35.Text = "Name";
            // 
            // lWorldGroupImageMap
            // 
            this.lWorldGroupImageMap.AutoSize = true;
            this.lWorldGroupImageMap.Location = new System.Drawing.Point(92, 69);
            this.lWorldGroupImageMap.Name = "lWorldGroupImageMap";
            this.lWorldGroupImageMap.Size = new System.Drawing.Size(13, 13);
            this.lWorldGroupImageMap.TabIndex = 39;
            this.lWorldGroupImageMap.Text = "0";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 36);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(76, 13);
            this.label37.TabIndex = 35;
            this.label37.Text = "Size && Position";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 69);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(60, 13);
            this.label40.TabIndex = 38;
            this.label40.Text = "Image Map";
            // 
            // lWorldGroupSizeAndPos
            // 
            this.lWorldGroupSizeAndPos.AutoSize = true;
            this.lWorldGroupSizeAndPos.Location = new System.Drawing.Point(92, 36);
            this.lWorldGroupSizeAndPos.Name = "lWorldGroupSizeAndPos";
            this.lWorldGroupSizeAndPos.Size = new System.Drawing.Size(43, 13);
            this.lWorldGroupSizeAndPos.TabIndex = 37;
            this.lWorldGroupSizeAndPos.Text = "<none>";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnFindDoodadsInZone);
            this.groupBox4.Controls.Add(this.labelZoneGroupRestrictions);
            this.groupBox4.Controls.Add(this.btnFindQuestsInZone);
            this.groupBox4.Controls.Add(this.btnFindNPCsInZone);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.lZoneGroupsSoundPackID);
            this.groupBox4.Controls.Add(this.lZoneGroupsDisplayName);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.btnZoneGroupsFreshWaterFish);
            this.groupBox4.Controls.Add(this.btnZoneGroupsSaltWaterFish);
            this.groupBox4.Controls.Add(this.lZoneGroupsName);
            this.groupBox4.Controls.Add(this.lZoneGroupsBuffID);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.label01);
            this.groupBox4.Controls.Add(this.label36);
            this.groupBox4.Controls.Add(this.lZoneGroupsSizePos);
            this.groupBox4.Controls.Add(this.lZoneGroupsPirateDesperado);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.lZoneGroupsImageMap);
            this.groupBox4.Controls.Add(this.lZoneGroupsFactionChatID);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.lZoneGroupsSoundID);
            this.groupBox4.Controls.Add(this.lZoneGroupsTargetID);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Location = new System.Drawing.Point(615, 142);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 212);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Zone Groups Info";
            // 
            // btnFindDoodadsInZone
            // 
            this.btnFindDoodadsInZone.Enabled = false;
            this.btnFindDoodadsInZone.Location = new System.Drawing.Point(188, 178);
            this.btnFindDoodadsInZone.Name = "btnFindDoodadsInZone";
            this.btnFindDoodadsInZone.Size = new System.Drawing.Size(85, 23);
            this.btnFindDoodadsInZone.TabIndex = 37;
            this.btnFindDoodadsInZone.Text = "Find Doodads";
            this.btnFindDoodadsInZone.UseVisualStyleBackColor = true;
            this.btnFindDoodadsInZone.Click += new System.EventHandler(this.btnFindDoodadsInZone_Click);
            // 
            // labelZoneGroupRestrictions
            // 
            this.labelZoneGroupRestrictions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelZoneGroupRestrictions.Location = new System.Drawing.Point(168, 120);
            this.labelZoneGroupRestrictions.Name = "labelZoneGroupRestrictions";
            this.labelZoneGroupRestrictions.Size = new System.Drawing.Size(124, 13);
            this.labelZoneGroupRestrictions.TabIndex = 36;
            this.labelZoneGroupRestrictions.Tag = "0";
            this.labelZoneGroupRestrictions.Text = "(Show Restrictions)";
            this.labelZoneGroupRestrictions.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.mainFormToolTip.SetToolTip(this.labelZoneGroupRestrictions, "Nothing to see here");
            this.labelZoneGroupRestrictions.Click += new System.EventHandler(this.labelZoneGroupRestrictions_Click);
            // 
            // btnFindQuestsInZone
            // 
            this.btnFindQuestsInZone.Enabled = false;
            this.btnFindQuestsInZone.Location = new System.Drawing.Point(97, 178);
            this.btnFindQuestsInZone.Name = "btnFindQuestsInZone";
            this.btnFindQuestsInZone.Size = new System.Drawing.Size(85, 23);
            this.btnFindQuestsInZone.TabIndex = 35;
            this.btnFindQuestsInZone.Text = "Find Quests";
            this.btnFindQuestsInZone.UseVisualStyleBackColor = true;
            this.btnFindQuestsInZone.Click += new System.EventHandler(this.btnFindQuestsInZone_Click);
            // 
            // btnFindNPCsInZone
            // 
            this.btnFindNPCsInZone.Enabled = false;
            this.btnFindNPCsInZone.Location = new System.Drawing.Point(6, 178);
            this.btnFindNPCsInZone.Name = "btnFindNPCsInZone";
            this.btnFindNPCsInZone.Size = new System.Drawing.Size(85, 23);
            this.btnFindNPCsInZone.TabIndex = 34;
            this.btnFindNPCsInZone.Text = "Find NPCs";
            this.btnFindNPCsInZone.UseVisualStyleBackColor = true;
            this.btnFindNPCsInZone.Click += new System.EventHandler(this.BtnFindNPCsInZone_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(165, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 32;
            this.label15.Text = "Sound Pack ID";
            // 
            // lZoneGroupsSoundPackID
            // 
            this.lZoneGroupsSoundPackID.AutoSize = true;
            this.lZoneGroupsSoundPackID.Location = new System.Drawing.Point(251, 84);
            this.lZoneGroupsSoundPackID.Name = "lZoneGroupsSoundPackID";
            this.lZoneGroupsSoundPackID.Size = new System.Drawing.Size(13, 13);
            this.lZoneGroupsSoundPackID.TabIndex = 33;
            this.lZoneGroupsSoundPackID.Text = "0";
            // 
            // lZoneGroupsDisplayName
            // 
            this.lZoneGroupsDisplayName.AutoSize = true;
            this.lZoneGroupsDisplayName.Location = new System.Drawing.Point(92, 16);
            this.lZoneGroupsDisplayName.Name = "lZoneGroupsDisplayName";
            this.lZoneGroupsDisplayName.Size = new System.Drawing.Size(43, 13);
            this.lZoneGroupsDisplayName.TabIndex = 31;
            this.lZoneGroupsDisplayName.Text = "<none>";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 16);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 13);
            this.label31.TabIndex = 30;
            this.label31.Text = "Display Name";
            // 
            // btnZoneGroupsFreshWaterFish
            // 
            this.btnZoneGroupsFreshWaterFish.Enabled = false;
            this.btnZoneGroupsFreshWaterFish.Location = new System.Drawing.Point(97, 149);
            this.btnZoneGroupsFreshWaterFish.Name = "btnZoneGroupsFreshWaterFish";
            this.btnZoneGroupsFreshWaterFish.Size = new System.Drawing.Size(85, 23);
            this.btnZoneGroupsFreshWaterFish.TabIndex = 29;
            this.btnZoneGroupsFreshWaterFish.Text = "Freshwater Fish";
            this.btnZoneGroupsFreshWaterFish.UseVisualStyleBackColor = true;
            this.btnZoneGroupsFreshWaterFish.Click += new System.EventHandler(this.BtnZoneGroupsFishLoot_Click);
            // 
            // btnZoneGroupsSaltWaterFish
            // 
            this.btnZoneGroupsSaltWaterFish.Enabled = false;
            this.btnZoneGroupsSaltWaterFish.Location = new System.Drawing.Point(6, 149);
            this.btnZoneGroupsSaltWaterFish.Name = "btnZoneGroupsSaltWaterFish";
            this.btnZoneGroupsSaltWaterFish.Size = new System.Drawing.Size(85, 23);
            this.btnZoneGroupsSaltWaterFish.TabIndex = 28;
            this.btnZoneGroupsSaltWaterFish.Text = "Saltwater Fish";
            this.btnZoneGroupsSaltWaterFish.UseVisualStyleBackColor = true;
            this.btnZoneGroupsSaltWaterFish.Click += new System.EventHandler(this.BtnZoneGroupsFishLoot_Click);
            // 
            // lZoneGroupsName
            // 
            this.lZoneGroupsName.AutoSize = true;
            this.lZoneGroupsName.Location = new System.Drawing.Point(92, 29);
            this.lZoneGroupsName.Name = "lZoneGroupsName";
            this.lZoneGroupsName.Size = new System.Drawing.Size(43, 13);
            this.lZoneGroupsName.TabIndex = 15;
            this.lZoneGroupsName.Text = "<none>";
            // 
            // lZoneGroupsBuffID
            // 
            this.lZoneGroupsBuffID.AutoSize = true;
            this.lZoneGroupsBuffID.Location = new System.Drawing.Point(92, 133);
            this.lZoneGroupsBuffID.Name = "lZoneGroupsBuffID";
            this.lZoneGroupsBuffID.Size = new System.Drawing.Size(13, 13);
            this.lZoneGroupsBuffID.TabIndex = 27;
            this.lZoneGroupsBuffID.Text = "0";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 29);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(35, 13);
            this.label38.TabIndex = 14;
            this.label38.Text = "Name";
            // 
            // label01
            // 
            this.label01.AutoSize = true;
            this.label01.Location = new System.Drawing.Point(6, 49);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(76, 13);
            this.label01.TabIndex = 14;
            this.label01.Text = "Size && Position";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 133);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(40, 13);
            this.label36.TabIndex = 26;
            this.label36.Text = "Buff ID";
            // 
            // lZoneGroupsSizePos
            // 
            this.lZoneGroupsSizePos.AutoSize = true;
            this.lZoneGroupsSizePos.Location = new System.Drawing.Point(92, 49);
            this.lZoneGroupsSizePos.Name = "lZoneGroupsSizePos";
            this.lZoneGroupsSizePos.Size = new System.Drawing.Size(43, 13);
            this.lZoneGroupsSizePos.TabIndex = 15;
            this.lZoneGroupsSizePos.Text = "<none>";
            // 
            // lZoneGroupsPirateDesperado
            // 
            this.lZoneGroupsPirateDesperado.AutoSize = true;
            this.lZoneGroupsPirateDesperado.Location = new System.Drawing.Point(251, 97);
            this.lZoneGroupsPirateDesperado.Name = "lZoneGroupsPirateDesperado";
            this.lZoneGroupsPirateDesperado.Size = new System.Drawing.Size(13, 13);
            this.lZoneGroupsPirateDesperado.TabIndex = 25;
            this.lZoneGroupsPirateDesperado.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 62);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(60, 13);
            this.label24.TabIndex = 16;
            this.label24.Text = "Image Map";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(165, 97);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(87, 13);
            this.label34.TabIndex = 24;
            this.label34.Text = "Pirate desperado";
            // 
            // lZoneGroupsImageMap
            // 
            this.lZoneGroupsImageMap.AutoSize = true;
            this.lZoneGroupsImageMap.Location = new System.Drawing.Point(92, 62);
            this.lZoneGroupsImageMap.Name = "lZoneGroupsImageMap";
            this.lZoneGroupsImageMap.Size = new System.Drawing.Size(13, 13);
            this.lZoneGroupsImageMap.TabIndex = 17;
            this.lZoneGroupsImageMap.Text = "0";
            // 
            // lZoneGroupsFactionChatID
            // 
            this.lZoneGroupsFactionChatID.AutoSize = true;
            this.lZoneGroupsFactionChatID.Location = new System.Drawing.Point(130, 120);
            this.lZoneGroupsFactionChatID.Name = "lZoneGroupsFactionChatID";
            this.lZoneGroupsFactionChatID.Size = new System.Drawing.Size(13, 13);
            this.lZoneGroupsFactionChatID.TabIndex = 23;
            this.lZoneGroupsFactionChatID.Text = "0";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 84);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 13);
            this.label22.TabIndex = 18;
            this.label22.Text = "Sound ID";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 120);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(118, 13);
            this.label32.TabIndex = 22;
            this.label32.Text = "Faction Chat Region ID";
            // 
            // lZoneGroupsSoundID
            // 
            this.lZoneGroupsSoundID.AutoSize = true;
            this.lZoneGroupsSoundID.Location = new System.Drawing.Point(92, 84);
            this.lZoneGroupsSoundID.Name = "lZoneGroupsSoundID";
            this.lZoneGroupsSoundID.Size = new System.Drawing.Size(13, 13);
            this.lZoneGroupsSoundID.TabIndex = 19;
            this.lZoneGroupsSoundID.Text = "0";
            // 
            // lZoneGroupsTargetID
            // 
            this.lZoneGroupsTargetID.AutoSize = true;
            this.lZoneGroupsTargetID.Location = new System.Drawing.Point(92, 97);
            this.lZoneGroupsTargetID.Name = "lZoneGroupsTargetID";
            this.lZoneGroupsTargetID.Size = new System.Drawing.Size(13, 13);
            this.lZoneGroupsTargetID.TabIndex = 21;
            this.lZoneGroupsTargetID.Text = "0";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 97);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 13);
            this.label30.TabIndex = 20;
            this.label30.Text = "Target ID";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.lZoneInstance);
            this.groupBox3.Controls.Add(this.btnFindTransferPathsInZone);
            this.groupBox3.Controls.Add(this.lZoneDisplayName);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.lZoneFactionID);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.lZoneGroupID);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.lZoneKey);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.lZoneName);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.lZoneID);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(615, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 127);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zone Info";
            // 
            // lZoneInstance
            // 
            this.lZoneInstance.AutoSize = true;
            this.lZoneInstance.Location = new System.Drawing.Point(94, 77);
            this.lZoneInstance.Name = "lZoneInstance";
            this.lZoneInstance.Size = new System.Drawing.Size(43, 13);
            this.lZoneInstance.TabIndex = 38;
            this.lZoneInstance.Text = "<none>";
            // 
            // btnFindTransferPathsInZone
            // 
            this.btnFindTransferPathsInZone.Location = new System.Drawing.Point(211, 98);
            this.btnFindTransferPathsInZone.Name = "btnFindTransferPathsInZone";
            this.btnFindTransferPathsInZone.Size = new System.Drawing.Size(85, 23);
            this.btnFindTransferPathsInZone.TabIndex = 37;
            this.btnFindTransferPathsInZone.Text = "Show Paths";
            this.btnFindTransferPathsInZone.UseVisualStyleBackColor = true;
            this.btnFindTransferPathsInZone.Click += new System.EventHandler(this.btnFindTransferPathsInZone_Click);
            // 
            // lZoneDisplayName
            // 
            this.lZoneDisplayName.AutoSize = true;
            this.lZoneDisplayName.Location = new System.Drawing.Point(92, 29);
            this.lZoneDisplayName.Name = "lZoneDisplayName";
            this.lZoneDisplayName.Size = new System.Drawing.Size(43, 13);
            this.lZoneDisplayName.TabIndex = 15;
            this.lZoneDisplayName.Text = "<none>";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 42);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(35, 13);
            this.label28.TabIndex = 14;
            this.label28.Text = "Name";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 77);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 13);
            this.label26.TabIndex = 10;
            this.label26.Text = "Instance";
            // 
            // lZoneFactionID
            // 
            this.lZoneFactionID.AutoSize = true;
            this.lZoneFactionID.Location = new System.Drawing.Point(92, 103);
            this.lZoneFactionID.Name = "lZoneFactionID";
            this.lZoneFactionID.Size = new System.Drawing.Size(13, 13);
            this.lZoneFactionID.TabIndex = 9;
            this.lZoneFactionID.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 103);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(56, 13);
            this.label23.TabIndex = 8;
            this.label23.Text = "Faction ID";
            // 
            // lZoneGroupID
            // 
            this.lZoneGroupID.AutoSize = true;
            this.lZoneGroupID.Location = new System.Drawing.Point(251, 64);
            this.lZoneGroupID.Name = "lZoneGroupID";
            this.lZoneGroupID.Size = new System.Drawing.Size(13, 13);
            this.lZoneGroupID.TabIndex = 7;
            this.lZoneGroupID.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(165, 64);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 13);
            this.label21.TabIndex = 6;
            this.label21.Text = "Group ID";
            // 
            // lZoneKey
            // 
            this.lZoneKey.AutoSize = true;
            this.lZoneKey.Location = new System.Drawing.Point(92, 64);
            this.lZoneKey.Name = "lZoneKey";
            this.lZoneKey.Size = new System.Drawing.Size(13, 13);
            this.lZoneKey.TabIndex = 5;
            this.lZoneKey.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 13);
            this.label19.TabIndex = 4;
            this.label19.Text = "Zone Key";
            // 
            // lZoneName
            // 
            this.lZoneName.AutoSize = true;
            this.lZoneName.Location = new System.Drawing.Point(92, 42);
            this.lZoneName.Name = "lZoneName";
            this.lZoneName.Size = new System.Drawing.Size(43, 13);
            this.lZoneName.TabIndex = 3;
            this.lZoneName.Text = "<none>";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 29);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(72, 13);
            this.label25.TabIndex = 2;
            this.label25.Text = "Display Name";
            // 
            // lZoneID
            // 
            this.lZoneID.AutoSize = true;
            this.lZoneID.Location = new System.Drawing.Point(92, 16);
            this.lZoneID.Name = "lZoneID";
            this.lZoneID.Size = new System.Drawing.Size(13, 13);
            this.lZoneID.TabIndex = 1;
            this.lZoneID.Text = "0";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(33, 13);
            this.label27.TabIndex = 0;
            this.label27.Text = "Index";
            // 
            // btnZonesShowAll
            // 
            this.btnZonesShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZonesShowAll.Location = new System.Drawing.Point(530, 11);
            this.btnZonesShowAll.Name = "btnZonesShowAll";
            this.btnZonesShowAll.Size = new System.Drawing.Size(79, 23);
            this.btnZonesShowAll.TabIndex = 8;
            this.btnZonesShowAll.Text = "Show All";
            this.btnZonesShowAll.UseVisualStyleBackColor = true;
            this.btnZonesShowAll.Click += new System.EventHandler(this.BtnZonesShowAll_Click);
            // 
            // btnSearchZones
            // 
            this.btnSearchZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchZones.Enabled = false;
            this.btnSearchZones.Location = new System.Drawing.Point(445, 11);
            this.btnSearchZones.Name = "btnSearchZones";
            this.btnSearchZones.Size = new System.Drawing.Size(79, 23);
            this.btnSearchZones.TabIndex = 7;
            this.btnSearchZones.Text = "Search";
            this.btnSearchZones.UseVisualStyleBackColor = true;
            this.btnSearchZones.Click += new System.EventHandler(this.BtnZonesSearch_Click);
            // 
            // dgvZones
            // 
            this.dgvZones.AllowUserToAddRows = false;
            this.dgvZones.AllowUserToDeleteRows = false;
            this.dgvZones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvZones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvZones.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvZones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dataGridViewTextBoxColumn12, this.dataGridViewTextBoxColumn13, this.Column15, this.Column11, this.Column13, this.Column14 });
            this.dgvZones.Location = new System.Drawing.Point(11, 38);
            this.dgvZones.Name = "dgvZones";
            this.dgvZones.ReadOnly = true;
            this.dgvZones.RowHeadersVisible = false;
            this.dgvZones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvZones.Size = new System.Drawing.Size(598, 441);
            this.dgvZones.TabIndex = 6;
            this.dgvZones.SelectionChanged += new System.EventHandler(this.DgvZones_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.FillWeight = 40F;
            this.dataGridViewTextBoxColumn12.HeaderText = "ID";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Internal Name";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.FillWeight = 60F;
            this.Column15.HeaderText = "Group ID";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.FillWeight = 60F;
            this.Column11.HeaderText = "Zone Key";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Display Text";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.FillWeight = 60F;
            this.Column14.HeaderText = "Is Closed";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Search in Zone ID, Key or Name";
            // 
            // tZonesSearch
            // 
            this.tZonesSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tZonesSearch.Location = new System.Drawing.Point(175, 11);
            this.tZonesSearch.Name = "tZonesSearch";
            this.tZonesSearch.Size = new System.Drawing.Size(264, 20);
            this.tZonesSearch.TabIndex = 4;
            this.tZonesSearch.TextChanged += new System.EventHandler(this.TZonesSearch_TextChanged);
            this.tZonesSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TZonesSearch_KeyDown);
            // 
            // tpTrade
            // 
            this.tpTrade.Controls.Add(this.lTradeRoute);
            this.tpTrade.Controls.Add(this.label133);
            this.tpTrade.Controls.Add(this.label134);
            this.tpTrade.Controls.Add(this.lTradeRatio);
            this.tpTrade.Controls.Add(this.lTradeProfit);
            this.tpTrade.Controls.Add(this.label132);
            this.tpTrade.Controls.Add(this.label121);
            this.tpTrade.Controls.Add(this.lbTradeDestination);
            this.tpTrade.Controls.Add(this.lbTradeSource);
            this.tpTrade.Location = new System.Drawing.Point(4, 22);
            this.tpTrade.Name = "tpTrade";
            this.tpTrade.Padding = new System.Windows.Forms.Padding(3);
            this.tpTrade.Size = new System.Drawing.Size(926, 485);
            this.tpTrade.TabIndex = 14;
            this.tpTrade.Text = "Trades";
            this.tpTrade.UseVisualStyleBackColor = true;
            // 
            // lTradeRoute
            // 
            this.lTradeRoute.AutoSize = true;
            this.lTradeRoute.Location = new System.Drawing.Point(440, 15);
            this.lTradeRoute.Name = "lTradeRoute";
            this.lTradeRoute.Size = new System.Drawing.Size(10, 13);
            this.lTradeRoute.TabIndex = 8;
            this.lTradeRoute.Text = "-";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(440, 55);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(35, 13);
            this.label133.TabIndex = 7;
            this.label133.Text = "Ratio:";
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(440, 31);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(34, 13);
            this.label134.TabIndex = 6;
            this.label134.Text = "Profit:";
            // 
            // lTradeRatio
            // 
            this.lTradeRatio.AutoSize = true;
            this.lTradeRatio.Location = new System.Drawing.Point(480, 55);
            this.lTradeRatio.Name = "lTradeRatio";
            this.lTradeRatio.Size = new System.Drawing.Size(13, 13);
            this.lTradeRatio.TabIndex = 5;
            this.lTradeRatio.Text = "0";
            // 
            // lTradeProfit
            // 
            this.lTradeProfit.AutoSize = true;
            this.lTradeProfit.Location = new System.Drawing.Point(480, 31);
            this.lTradeProfit.Name = "lTradeProfit";
            this.lTradeProfit.Size = new System.Drawing.Size(13, 13);
            this.lTradeProfit.TabIndex = 4;
            this.lTradeProfit.Text = "0";
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(221, 15);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(60, 13);
            this.label132.TabIndex = 3;
            this.label132.Text = "Destination";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(8, 15);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(69, 13);
            this.label121.TabIndex = 2;
            this.label121.Text = "Source Pack";
            // 
            // lbTradeDestination
            // 
            this.lbTradeDestination.FormattingEnabled = true;
            this.lbTradeDestination.Location = new System.Drawing.Point(224, 31);
            this.lbTradeDestination.Name = "lbTradeDestination";
            this.lbTradeDestination.Size = new System.Drawing.Size(210, 329);
            this.lbTradeDestination.TabIndex = 1;
            this.lbTradeDestination.SelectedIndexChanged += new System.EventHandler(this.lbTradeDestination_SelectedIndexChanged);
            // 
            // lbTradeSource
            // 
            this.lbTradeSource.FormattingEnabled = true;
            this.lbTradeSource.Location = new System.Drawing.Point(8, 31);
            this.lbTradeSource.Name = "lbTradeSource";
            this.lbTradeSource.Size = new System.Drawing.Size(210, 329);
            this.lbTradeSource.TabIndex = 0;
            this.lbTradeSource.SelectedIndexChanged += new System.EventHandler(this.lbTradeSource_SelectedIndexChanged);
            // 
            // openDBDlg
            // 
            this.openDBDlg.DefaultExt = "sqlite3";
            this.openDBDlg.FileName = "compact.sqlite3";
            this.openDBDlg.Filter = "SQLite3 files|*.sqlite3|All files|*.*";
            this.openDBDlg.Title = "Open Server DB File";
            // 
            // openGamePakFileDialog
            // 
            this.openGamePakFileDialog.Filter = "AA Game Pak|game_pak|All files|*.*";
            this.openGamePakFileDialog.RestoreDirectory = true;
            this.openGamePakFileDialog.Title = "Open game_pak";
            // 
            // mainFormToolTip
            // 
            this.mainFormToolTip.AutoPopDelay = 10000;
            this.mainFormToolTip.InitialDelay = 1000;
            this.mainFormToolTip.ReshowDelay = 100;
            this.mainFormToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            // 
            // ofdCustomPaths
            // 
            this.ofdCustomPaths.DefaultExt = "xml";
            this.ofdCustomPaths.Filter = "XML Files|*.xml|All Files|*.*";
            this.ofdCustomPaths.Title = "Open custom path";
            // 
            // ofdJsonData
            // 
            this.ofdJsonData.DefaultExt = "xml";
            this.ofdJsonData.Filter = "Json Files|*.json|All Files|*.*";
            this.ofdJsonData.Title = "Open custom json data";
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "icon_question_yellow.png");
            this.ilIcons.Images.SetKeyName(1, "icon_exclamation_yellow.png");
            this.ilIcons.Images.SetKeyName(2, "icon_exclamation_green.png");
            this.ilIcons.Images.SetKeyName(3, "icon_exclamation_blue.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 511);
            this.Controls.Add(this.tcViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "AAEmu.DBViewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcViewer.ResumeLayout(false);
            this.tbTables.ResumeLayout(false);
            this.tbTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimple)).EndInit();
            this.tpCurrentRecord.ResumeLayout(false);
            this.tpCurrentRecord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentData)).EndInit();
            this.tbLocalizer.ResumeLayout(false);
            this.tbLocalizer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalized)).EndInit();
            this.tpMap.ResumeLayout(false);
            this.tpMap.PerformLayout();
            this.tpV1.ResumeLayout(false);
            this.tpV1.PerformLayout();
            this.tpBuffs.ResumeLayout(false);
            this.tpBuffs.PerformLayout();
            this.tcBuffs.ResumeLayout(false);
            this.tcBuffs_Buffs.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.tcBuffs_Triggers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuffs)).EndInit();
            this.tpDoodads.ResumeLayout(false);
            this.tpDoodads.PerformLayout();
            this.tcDoodads.ResumeLayout(false);
            this.tpDoodadInfo.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tpDoodadFunctions.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoodadFuncGroups)).EndInit();
            this.tpDoodadTools.ResumeLayout(false);
            this.tpDoodadWorkflow.ResumeLayout(false);
            this.tpDoodadWorkflow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoodads)).EndInit();
            this.tbFactions.ResumeLayout(false);
            this.tbFactions.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactions)).EndInit();
            this.tpItems.ResumeLayout(false);
            this.tpItems.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.tpLoot.ResumeLayout(false);
            this.tpLoot.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoot)).EndInit();
            this.tpNPCs.ResumeLayout(false);
            this.tpNPCs.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNPCs)).EndInit();
            this.tpQuests.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuests)).EndInit();
            this.tpSkills.ResumeLayout(false);
            this.tpSkills.PerformLayout();
            this.tcSkillInfo.ResumeLayout(false);
            this.tpSkillInfo.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tpSkillItems.ResumeLayout(false);
            this.tpSkillItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkillProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkillReagents)).EndInit();
            this.tpSkillExecution.ResumeLayout(false);
            this.gbSkillPlotEventInfo.ResumeLayout(false);
            this.gbSkillPlotEventInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkills)).EndInit();
            this.tpZones.ResumeLayout(false);
            this.tpZones.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZones)).EndInit();
            this.tpTrade.ResumeLayout(false);
            this.tpTrade.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TreeView tvBuffTriggers;

        private System.Windows.Forms.TabControl tcBuffs;
        private System.Windows.Forms.TabPage tcBuffs_Buffs;
        private System.Windows.Forms.TabPage tcBuffs_Triggers;

        #endregion
        private System.Windows.Forms.ListBox lbTableNames;
        private System.Windows.Forms.TabControl tcViewer;
        private System.Windows.Forms.TabPage tbTables;
        private System.Windows.Forms.TabPage tpItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tItemSearch;
        private System.Windows.Forms.DataGridView dgvItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_Name_EN_US;
        private System.Windows.Forms.Button btnItemSearch;
        private System.Windows.Forms.ComboBox cbItemSearchLanguage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lItemCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lItemName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lItemID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lItemLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openDBDlg;
        private System.Windows.Forms.Button btnOpenServerDB;
        private System.Windows.Forms.TabPage tpLoot;
        private System.Windows.Forms.Button btnFindItemInLoot;
        private System.Windows.Forms.DataGridView dgvLoot;
        private System.Windows.Forms.Button btnLootSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tLootSearch;
        private System.Windows.Forms.RichTextBox rtItemDesc;
        private System.Windows.Forms.Label itemIcon;
        private System.Windows.Forms.Button btnFindGameClient;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tpSkills;
        private System.Windows.Forms.Button btnSkillSearch;
        private System.Windows.Forms.DataGridView dgvSkills;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tSkillSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label skillIcon;
        private System.Windows.Forms.RichTextBox rtSkillDescription;
        private System.Windows.Forms.Label lSkillName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lSkillID;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lSkillCost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lSkillGCD;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lSkillCooldown;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lSkillLabor;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lSkillMana;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnFindItemSkill;
        private System.Windows.Forms.OpenFileDialog openGamePakFileDialog;
        private System.Windows.Forms.TabPage tpCurrentRecord;
        private System.Windows.Forms.Label labelCurrentDataInfo;
        private System.Windows.Forms.DataGridView dgvCurrentData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.TabControl tcSkillInfo;
        private System.Windows.Forms.TabPage tpSkillInfo;
        private System.Windows.Forms.TabPage tpSkillItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridView dgvSkillProducts;
        private System.Windows.Forms.DataGridView dgvSkillReagents;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelSkillReagents;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.TabPage tpZones;
        private System.Windows.Forms.Button btnSearchZones;
        private System.Windows.Forms.DataGridView dgvZones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tZonesSearch;
        private System.Windows.Forms.Button btnZonesShowAll;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lZoneName;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lZoneID;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.Label lZoneKey;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lZoneGroupsSizePos;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lZoneFactionID;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lZoneGroupID;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lZoneGroupsName;
        private System.Windows.Forms.Label lZoneGroupsBuffID;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lZoneGroupsPirateDesperado;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label lZoneGroupsImageMap;
        private System.Windows.Forms.Label lZoneGroupsFactionChatID;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label lZoneGroupsSoundID;
        private System.Windows.Forms.Label lZoneGroupsTargetID;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnZoneGroupsFreshWaterFish;
        private System.Windows.Forms.Button btnZoneGroupsSaltWaterFish;
        private System.Windows.Forms.Label lZoneDisplayName;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lZoneGroupsDisplayName;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lZoneGroupsSoundPackID;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lWorldGroupImageSizeAndPos;
        private System.Windows.Forms.Label lWorldGroupTargetID;
        private System.Windows.Forms.Label lWorldGroupName;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lWorldGroupImageMap;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lWorldGroupSizeAndPos;
        private System.Windows.Forms.TabPage tpNPCs;
        private System.Windows.Forms.Button btnSearchNPC;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox tSearchNPC;
        private System.Windows.Forms.DataGridView dgvNPCs;
        private System.Windows.Forms.DataGridView dgvSimple;
        private System.Windows.Forms.Button btnSimpleSQL;
        private System.Windows.Forms.TextBox tSimpleSQL;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TabPage tbFactions;
        private System.Windows.Forms.Button btnSearchFaction;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox tSearchFaction;
        private System.Windows.Forms.DataGridView dgvFactions;
        private System.Windows.Forms.Button btnFactionsAll;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label lFactionName;
        private System.Windows.Forms.Label lFactionID;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column30;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column29;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lFactionOwnerName;
        private System.Windows.Forms.Label lFactionOwnerID;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label lFactionOwnerTypeID;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label LFactionPoliticalSystemID;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label lFactionGuardLink;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label lFactionAggroLink;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label lFactionDiplomacyLinkID;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label lFactionIsDiplomacyTarget;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label lFactionMotherID;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label lFactionHostileHaranya;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label lFactionHostileNuia;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label lFactionHostilePirate;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TabPage tpDoodads;
        private System.Windows.Forms.Button btnSearchDoodads;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox tSearchDoodads;
        private System.Windows.Forms.DataGridView dgvDoodads;
        private System.Windows.Forms.TabControl tcDoodads;
        private System.Windows.Forms.TabPage tpDoodadInfo;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lDoodadID;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label lDoodadOnceOneMan;
        private System.Windows.Forms.Label lDoodadName;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TabPage tpDoodadFunctions;
        private System.Windows.Forms.Label lDoodadModel;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label lDoodadMinTime;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label lDoodadPercent;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label lDoodadMgmtSpawn;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label lDoodadShowName;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label lDoodadOnceOneInteraction;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label lDoodadGroupID;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label lDoodadMilestoneID;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label lDoodadForceToDTopPriority;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label lDoodadUseCreatorFaction;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label lDoodadModelKindID;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label lDoodadMaxTime;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label lDoodadCollideVehicle;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label lDoodadCollideShip;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label lDoodadSimRadius;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label lDoodadTargetDecalSize;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label lDoodadUseTargetHighlight;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label lDoodadUseTargetSilhouette;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label lDoodadUseTargetDecal;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label lDoodadShowMinimap;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label lDoodadParentable;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label lDoodadLoadModelFromWorld;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label lDoodadForceUpAction;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label lDoodadMarkModel;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label lDoodadClimateID;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label lDoodadGrowthTime;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.Label lDoodadFactionID;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label lDoodadChildable;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.Label lDoodadRestrictZoneID;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.Label lDoodadNoCollision;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Label lDoodadDespawnOnCollision;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.Label lDoodadSaveIndun;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label lDoodadGroupName;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label lDoodadGroupRemovedByHouse;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label lDoodadGroupGuardOnFieldTime;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label lDoodadGroupIsExport;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.DataGridView dgvDoodadFuncGroups;
        private System.Windows.Forms.Label lDoodadFuncGroupID;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label lDoodadFuncGroupName;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label lDoodadFuncGroupModel;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label lDoodadFuncGroupComment;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label lDoodadFuncGroupPhaseMsg;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label lDoodadFuncGroupKindID;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label lDoodadFuncGroupIsMsgToZone;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label lDoodadFuncGroupSoundID;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label lDoodadFuncGroupSoundTime;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Button btnFindNPCsInZone;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label LLootGroupPackID;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Label LLootPackGroupNumber;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Button btnFindQuestsInZone;
        private System.Windows.Forms.TabPage tpQuests;
        private System.Windows.Forms.Button btnQuestsSearch;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TextBox tQuestSearch;
        private System.Windows.Forms.DataGridView dgvQuests;
        private System.Windows.Forms.Label lItemAddGMCommand;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label lNPCTemplate;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label lGMNPCSpawn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column34;
        private System.Windows.Forms.ComboBox cbItemSearchItemArmorSlotTypeList;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.ComboBox cbItemSearchItemCategoryTypeList;
        private System.Windows.Forms.Label labelZoneGroupRestrictions;
        private System.Windows.Forms.ToolTip mainFormToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column35;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column36;
        private System.Windows.Forms.TabPage tbLocalizer;
        private System.Windows.Forms.DataGridView dgvLocalized;
        private System.Windows.Forms.TextBox tSearchLocalized;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column48;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column49;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column51;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column50;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.TabPage tpV1;
        private System.Windows.Forms.TabPage tpBuffs;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label lBuffId;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Button btnSearchBuffs;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.TextBox tSearchBuffs;
        private System.Windows.Forms.DataGridView dgvBuffs;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column52;
        private System.Windows.Forms.FlowLayoutPanel flpBuff;
        private System.Windows.Forms.Label lBuffName;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label buffIcon;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.RichTextBox rtBuffDesc;
        private System.Windows.Forms.Label lBuffDuration;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.Label lSpace;
        private System.Windows.Forms.Label lBuffTags;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label lSkillTags;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label lNPCTags;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Label lItemTags;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Button btnExportDataForVieweD;
        private System.Windows.Forms.Button btnShowNPCsOnMap;
        private System.Windows.Forms.Button btnFindTransferPathsInZone;
        private System.Windows.Forms.Button btnExportNPCSpawnData;
        private System.Windows.Forms.Label lCurrentPakFile;
        private System.Windows.Forms.TabPage tpMap;
        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.Button btnFindAllTransferPaths;
        private System.Windows.Forms.TabPage tpDoodadTools;
        private System.Windows.Forms.Button btnShowDoodadOnMap;
        private System.Windows.Forms.Button btnFindAllQuestSpheres;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Button btnFindAllHousing;
        private System.Windows.Forms.Button btnFindAllSubzone;
        private System.Windows.Forms.TextBox eQuestSignSphereSearch;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.CheckBox cbQuestSignSphereSearchShowAll;
        private System.Windows.Forms.Button btnQuestFindRelatedOnMap;
        private System.Windows.Forms.Label lZoneInstance;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tFilterTables;
        private System.Windows.Forms.Button btnLoadCustomPaths;
        private System.Windows.Forms.OpenFileDialog ofdCustomPaths;
        private System.Windows.Forms.Label lBuffAddGMCommand;
        private System.Windows.Forms.Button btnFindDoodadsInZone;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column28;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column31;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column53;
        private System.Windows.Forms.Button btnLoadCustomAAEmuJson;
        private System.Windows.Forms.OpenFileDialog ofdJsonData;
        private System.Windows.Forms.Button btnShowEntityAreaShape;
        private System.Windows.Forms.TabPage tpSkillExecution;
        private System.Windows.Forms.TreeView tvSkill;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ImageList ilMiniIcons;
        private System.Windows.Forms.GroupBox gbSkillPlotEventInfo;
        private System.Windows.Forms.Label lPlotEventP9;
        private System.Windows.Forms.Label lPlotEventP8;
        private System.Windows.Forms.Label lPlotEventP7;
        private System.Windows.Forms.Label lPlotEventP6;
        private System.Windows.Forms.Label lPlotEventP5;
        private System.Windows.Forms.Label lPlotEventP4;
        private System.Windows.Forms.Label lPlotEventP3;
        private System.Windows.Forms.Label lPlotEventP2;
        private System.Windows.Forms.Label lPlotEventP1;
        private System.Windows.Forms.Label lPlotEventTickets;
        private System.Windows.Forms.Label lPlotEventTargetUpdate;
        private System.Windows.Forms.Label lPlotEventSourceUpdate;
        private System.Windows.Forms.Label lPlotEventAoE;
        private System.Windows.Forms.TabPage tpTrade;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.ListBox lbTradeDestination;
        private System.Windows.Forms.ListBox lbTradeSource;
        private System.Windows.Forms.Label lTradeRatio;
        private System.Windows.Forms.Label lTradeProfit;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label lTradeRoute;
        private System.Windows.Forms.Button btnExportDoodadSpawnData;
        private System.Windows.Forms.Label lDoodadPhaseFuncsActualType;
        private System.Windows.Forms.Label lDoodadPhaseFuncsActualId;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column32;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column33;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column54;
        private System.Windows.Forms.ComboBox cbItemSearchRange;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.TabPage tpDoodadWorkflow;
        private System.Windows.Forms.TreeView tvDoodadDetails;
        private System.Windows.Forms.CheckBox cbDoodadWorkflowHideEmpty;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvQuestWorkflow;
        private System.Windows.Forms.CheckBox cbQuestWorkflowHideEmpty;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.RichTextBox rtQuestText;
    }
}

