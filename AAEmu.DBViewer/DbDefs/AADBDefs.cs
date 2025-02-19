using AAEmu.DBViewer.enums;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace AAEmu.DBViewer.DbDefs;

public class GameTranslation
{
    public long Idx = 0;
    public string Table = string.Empty;
    public string Field = string.Empty;
    public string Value = string.Empty;
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
    CutDown = 1,
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

public class GameItem
{
    // Actual DB entries
    public long Id = 0;
    public string Name = string.Empty;
    public long CategoryId = 1;
    public long Level = 1;
    public string Description = string.Empty;
    public long Price = 0;
    public long Refund = 0;
    public long MaxStackSize = 1;
    public long IconId = 1;
    public bool Sellable = false;
    public long FixedGrade = -1;
    public long UseSkillId = 0;
    public long ImplId = 0;

    // Linked
    public GameItemArmors ItemArmors = null;
    public GameItemWeapons ItemWeapons = null;

    // Helpers
    public string NameLocalized = string.Empty;
    public string DescriptionLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameItemCategories
{
    // Actual DB entries
    public long Id = 0;
    public string Name = "None";

    // Helpers
    public string NameLocalized = "None";

    public override string ToString()
    {
        return NameLocalized + " (" + Id.ToString() + ")";
    }

    public string DisplayListName => ToString();

    public string DisplayListValue => Id.ToString();
}

public class GameItemArmors
{
    // Actual DB entries
    public long Id = 0;
    public long ItemId = 0;
    public long SlotTypeId = 0;
    public bool OrUnitReqs = false;
}

public class GameItemWeapons
{
    // Actual DB entries
    public long Id = 0;
    public long ItemId = 0;
    public long HoldableId = 0;
    public bool OrUnitReqs = false;
}

public class GameSkills
{
    // Actual DB entries
    public long Id = 0;
    public string Name = string.Empty;
    public string Desc = string.Empty;
    public string WebDesc = string.Empty;
    public long Cost = 0;
    public long IconId = 0;
    public bool Show = false;
    public long CooldownTime = 0;
    public long CastingTime = 0;
    public bool IgnoreGlobalCooldown = false;
    public bool DefaultGcd = true;
    public long CustomGcd = 0;
    public long EffectDelay = 0;
    public long AbilityId = 0;
    public long ManaCost = 0;
    public long TimingId = 0;
    public long ConsumeLp = 0;
    public bool FirstReagentOnly = false;
    public long PlotId = 0;
    public bool OrUnitReqs = false;

    // Helpers
    public string NameLocalized = string.Empty;
    public string DescriptionLocalized = string.Empty;
    public string WebDescriptionLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameSkillEffects
{
    // Actual DB entries skill_effects
    public long Id = 0;
    public long SkillId = 0;
    public long EffectId = 0;
    public long Weight = 0;
    public long StartLevel = 0;
    public long EndLevel = 0;
    public bool Friendly = false;
    public bool NonFriendly = false;
    public long TargetBuffTagId = 0;
    public long TargetNoBuffTagId = 0;
    public long SourceBuffTagId = 0;
    public long SourceNoBuffTagId = 0;
    public long Chance = 0;
    public bool Front = false;
    public bool Back = false;
    public long TargetNpcTagId = 0;
    public long ApplicationMethodId = 0;
    public bool SynergyText = false;
    public bool ConsumeSourceItem = false;
    public long ConsumeItemId = 0;
    public long ConsumeItemCount = 0;
    public bool AlwaysHit = false;
    public long ItemSetId = 0;
    public bool InteractionSuccessHit = false;
}

public class GameEffects
{
    // Actual DB entries effects
    public long Id = 0;
    public long ActualId = 0;
    public string ActualType = "";
}

public class GameNpc
{
    // Actual DB entries
    public long Id = 0;
    public string Name = string.Empty;
    public long CharRaceId = 0;
    public long NpcGradeId = 0;
    public long NpcKindId = 0;
    public long Level = 0;
    public long ModelId = 0;
    public long FactionId = 0;
    public long NpcTemplateId = 0;
    public long EquipBodiesId = 0;
    public long EquipClothsId = 0;
    public long EquipWeaponsId = 0;
    public bool SkillTrainer = false;
    public long AiFileId = 0;
    public bool Merchant = false;
    public long NpcNicknameId = 0;
    public bool Auctioneer = false;
    public bool ShowNameTag = false;
    public bool VisibleToCreatorOnly = false;
    public bool NoExp = false;
    public long PetItemId = 0;
    public long BaseSkillId = 0;
    public bool TrackFriendship = false;
    public bool Priest = false;
    public string Comment1 = string.Empty;
    public long NpcTendencyId = 0;
    public bool Blacksmith = false;
    public bool Teleporter = false;
    public float Opacity = 0.0f;
    public bool AbilityChanger = false;
    public float Scale = 1.0f;
    public string Comment2 = string.Empty;
    public string Comment3 = string.Empty;
    public float SightRangeScale = 0.0f;
    public float SightFovScale = 0.0f;
    public long MilestoneId = 0;
    public float AttackStartRangeScale = 0.0f;
    public bool Aggression = false;
    public float ExpMultiplier = 1.0f;
    public long ExpAdder = 0;
    public bool Stabler = false;
    public bool AcceptAggroLink = false;
    public long RecruitingBattleFieldId = 0;
    public float ReturnDistance = 0;
    public long NpcAiParamId = 0;
    public bool NonPushableByActor = false;
    public bool Banker = false;
    public long AggroLinkSpecialRuleId = 0;
    public float AggroLinkHelpDist = 0.0f;
    public bool AggroLinkSightCheck = false;
    public bool Expedition = false;
    public long HonorPoint = 0;
    public bool Trader = false;
    public bool AggroLinkSpecialGuard = false;
    public bool AggroLinkSpecialIgnoreNpcAttacker = false;
    public string CommentWear = string.Empty;
    public float AbsoluteReturnDistance = 0.0f;
    public bool Repairman = false;
    public bool ActivateAiAlways = false;
    public string SoState = string.Empty;
    public bool Specialty = false;
    public long SoundPackId = 0;
    public long SpecialtyCoinId = 0;
    public bool UseRangeMod = false;
    public long NpcPostureSetId = 0;
    public long MateEquipSlotPackId = 0;
    public long MateKindId = 0;
    public long EngageCombatGiveQuestId = 0;
    public long TotalCustomId = 0;
    public bool NoApplyTotalCustom = false;
    public bool BaseSkillStrafe = false;
    public float BaseSkillDelay = 0.0f;
    public long NpcInteractionSetId = 0;
    public bool UseAbuserList = false;
    public bool ReturnWhenEnterHousingArea = false;
    public bool LookConverter = false;
    public bool UseDdcmsMountSkill = false;
    public bool CrowdEffect = false;
    public float FxScale = 1.0f;
    public bool Translate = false;
    public bool NoPenalty = false;
    public bool ShowFactionTag = false;

    // Helpers
    public string NameLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameQuestMonsterGroups
{
    public long Id;
    public string Name;
    public long CategoryId;
    public string NameLocalized = string.Empty;
}

public class GameQuestMonsterNpcs
{
    public long Id;
    public long QuestMonsterGroupId;
    public long NpcId;
}

public class GameSkillItems
{
    public long Id = 0;
    public long SkillId = 0;
    public long ItemId = 0;
    public long Amount = 0;
}

public class GameZone
{
    private const string MainWorld = "main_world";

    public long Id = 0;
    public string Name = string.Empty;
    public long ZoneKey = 0;
    public long GroupId = 0;
    public bool Closed = false;
    public string DisplayText = string.Empty;
    public long FactionId = 0;
    public long ZoneClimateId = 0;
    public bool AboxShow = false; // no idea what this is, seems to be always set to false

    // Helpers
    public string DisplayTextLocalized = string.Empty;
    public string SearchString = string.Empty;
    public string GamePakZoneTransferPathXml => $"game/worlds/{MainWorld}/level_design/zone/{ZoneKey}/client/transfer_path.xml";
    public string GamePakZoneHousingXml => $"game/worlds/{MainWorld}/level_design/zone/{ZoneKey}/client/housing_area.xml";
    public string GamePakSubZoneXml => $"game/worlds/{MainWorld}/level_design/zone/{ZoneKey}/client/subzone_area.xml";
}

public class GameZoneGroups
{
    public long Id = 0;
    public string Name = string.Empty;
    public RectangleF PosAndSize = new();
    public long ImageMap = 0;
    public long SoundId = 0;
    public long TargetId = 0;
    public string DisplayText = string.Empty;
    public long FactionChatRegionId = 0;
    public long SoundPackId = 0;
    public bool PirateDesperado = false;
    public long FishingSeaLootPackId = 0;
    public long FishingLandLootPackId = 0;
    public long BuffId = 0;

    // Helpers
    public string DisplayTextLocalized = string.Empty;
    public string SearchString = string.Empty;

    public string GamePakZoneNpCsDat(string instanceName = "main_world")
    {
        return "game/worlds/" + instanceName + "/map_data/npc_map/" + Name + ".dat";
    }

    public string GamePakZoneDoodadsDat(string instanceName = "main_world")
    {
        return "game/worlds/" + instanceName + "/map_data/doodad_map/" + Name + ".dat";
    }

    public override string ToString()
    {
        return AaDb.GetTranslationById(Id, "zone_groups", "display_text", Name);
    }
}

public class GameWorldGroups
{
    public long Id = 0;
    public string Name = string.Empty;
    public long TargetId = 0;
    public long ImageMap = 0;
    public Rectangle PosAndSize = new Rectangle();
    public Rectangle ImagePosAndSize = new Rectangle();

    // 10.x (might be earlier)
    public string MapTargetType = string.Empty;
    public long MapTargetId = 0;

    // Helpers
    public string SearchString = string.Empty;
}

public class GameSystemFaction
{
    public long Id = 0;
    public string Name = string.Empty;
    public string OwnerName = string.Empty;
    public long OwnerTypeId = 0;
    public long OwnerId = 0;
    public long PoliticalSystemId = 0;
    public long MotherId = 0;
    public bool AggroLink = false;
    public bool GuardHelp = false;
    public bool IsDiplomacyTgt = false;
    public long DiplomacyLinkId = 0;

    // Helpers
    public string NameLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameSystemFactionRelation
{
    public long Id = 0;
    public long Faction1Id = 0;
    public long Faction2Id = 0;
    public long StateId = 0;
}

public class GameDoodadGroup
{
    // TABLE doodad_groups
    public long Id = 0;
    public string Name = string.Empty;
    public bool IsExport = false;
    public long GuardOnFieldTime = 0;
    public bool RemovedByHouse = false;

    // Helpers
    public string NameLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameDoodad
{
    // TABLE doodad_almighties
    public long Id = 0;
    public string Name = string.Empty;
    public string Model = string.Empty;
    public bool OnceOneMan = false;
    public bool OnceOneInteraction = false;
    public bool ShowName = false;
    public bool MgmtSpawn = false;
    public long Percent = 0;
    public long MinTime = 0;
    public long MaxTime = 0;
    public long ModelKindId = 0;
    public bool UseCreatorFaction = false;
    public bool ForceTodTopPriority = false;
    public long MilestoneId = 0;
    public long GroupId = 0;
    public bool ShowMinimap = false;
    public bool UseTargetDecal = false;
    public bool UseTargetSilhouette = false;
    public bool UseTargetHighlight = false;
    public float TargetDecalSize = 0.0f;
    public long SimRadius = 0;
    public bool CollideShip = false;
    public bool CollideVehicle = false;
    public long ClimateId = 0;
    public bool SaveIndun = false;
    public string MarkModel = string.Empty;
    public bool ForceUpAction = false;
    public bool LoadModelFromWorld = false;
    public bool Parentable = false;
    public bool Childable = false;
    public long FactionId = 0;
    public long GrowthTime = 0;
    public bool DespawnOnCollision = false;
    public bool NoCollision = false;
    public long RestrictZoneId = 0;
    public bool Translate = false;

    // Helpers
    public string NameLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameDoodadFunc
{
    // TABLE doodad_funcs
    public long Id = 0;
    public long DoodadFuncGroupId = 0;
    public long ActualFuncId = 0;
    public string ActualFuncType = string.Empty;
    public long NextPhase = 0;
    public long SoundId = 0;
    public long FuncSkillId = 0;
    public long PermId = 0;
    public long ActCount = 0;
    public bool PopupWarn = false;
    public bool ForbidOnClimb = false;
}

public class GameDoodadFuncGroup
{
    // TABLE doodad_func_groups
    public long Id = 0;
    public string Model = string.Empty;
    public long DoodadAlmightyId = 0;
    public long DoodadFuncGroupKindId = 0;
    public string PhaseMsg = string.Empty;
    public long SoundId = 0;
    public string Name = string.Empty;
    public long SoundTime = 0;
    public string Comment = string.Empty;
    public bool IsMsgToZone = false;

    // Helpers
    public string NameLocalized = string.Empty;
    public string PhaseMsgLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameDoodadPhaseFunc
{
    // TABLE doodad_phase_funcs
    public long Id = 0;
    public long DoodadFuncGroupId = 0;
    public long ActualFuncId = 0;
    public string ActualFuncType = string.Empty;
}

public class GameQuestContexts
{
    // TABLE quest_contexts
    public long Id = 0;
    public string Name = string.Empty;
    public long CategoryId = 0;
    public bool Repeatable = false;
    public long Level = 0;
    public bool Selective = false;
    public bool Successive = false;
    public bool RestartOnFail = false;
    public long ChapterIdx = 0;
    public long QuestIdx = 0;
    // public long milestone_id = 0;
    public bool LetItDone = false;
    public long DetailId = 0;
    public long ZoneId = 0;
    public long Degree = 0;
    public bool UseQuestCamera = false;
    public long Score = 0;
    public bool UseAcceptMessage = false;
    public bool UseCompleteMessage = false;
    public long GradeId = 0;

    public string NameLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameQuestContextText
{
    // TABLE quest_context_texts
    public long Id = 0;
    public long QuestContextTextKindId = 0;
    public long QuestContextId = 0;
    public string Text = string.Empty;

    public string TextLocalized = string.Empty;
}

public class GameQuestCategory
{
    // TABLE quest_category
    public long Id = 0;
    public string Name = string.Empty;

    public string NameLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameQuestAct
{
    // TABLE quest_acts
    public long Id = 0;
    public long QuestComponentId = 0;
    public long ActDetailId = 0;
    public string ActDetailType = string.Empty;
}

public class GameQuestActConAcceptNpc
{
    // TABLE quest_act_con_accept_npcs
    public long Id = 0;
    public long NpcId = 0;
}


public class GameQuestComponent
{
    // TABLE quest_components
    public long Id = 0;
    public long QuestContextId = 0;
    public long ComponentKindId = 0;
    public long NextComponent = 0;
    public long NpcAiId = 0;
    public long NpcId = 0;
    public long SkillId = 0;
    public bool SkillSelf = false;
    public string AiPathName = string.Empty;
    public long AiPathTypeId = 0;
    public long SoundId = 0;
    public long NpcSpawnerId = 0;
    public bool PlayCinemaBeforeBubble = false;
    public long AiCommandSetId = 0;
    public bool OrUnitReqs = false;
    public long CinemaId = 0;
    public long SummaryVoiceId = 0;
    public bool HideQuestMarker = false;
    public long BuffId = 0;
}

public class GameQuestComponentText
{
    // TABLE quest_component_texts
    public long Id = 0;
    public long QuestComponentId = 0;
    public long QuestComponentTextKindId = 0;
    public string Text = string.Empty;

    public string TextLocalized = string.Empty;
}

public class GameTags
{
    public long Id = 0;
    public string Name = string.Empty;
    public string Desc = string.Empty;

    // Helpers
    public string NameLocalized = string.Empty;
    public string DescLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameTaggedValues
{
    public long Id = 0;
    public long TagId = 0;
    public long TargetId = 0;
}

public class GameBuff
{
    public long Id = 0;
    public string Name = string.Empty;
    public string Desc = string.Empty;
    public long IconId = 0;
    public long Duration = 0;
    public Dictionary<string, string> Others = new();

    // Helpers
    public string NameLocalized = string.Empty;
    public string DescLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameBuffTrigger
{
    public long Id = 0;
    public long BuffId = 0;
    public long EventId = 0;
    public long EffectId = 0;
}

public class GameBuffModifier
{
    public long Id = 0;
    public long OwnerId = 0;
    public string OwnerType = string.Empty;
    public long BuffId = 0;
    public long TagId = 0;
    public BuffAttribute BuffAttributeId = 0;
    public long UnitModifierTypeId = 0;
    public long Value = 0;
    public bool Synergy = false;
}

public class GameZoneGroupBannedTags
{
    // TABLE zone_group_banned_tags(
    public long Id = 0;
    public long ZoneGroupId = 0;
    public long TagId = 0;
    public long BannedPeriodsId = 0;
    public string Usage = string.Empty;
}

public class GameTransfers
{
    // TABLE transfers
    public long Id = 0;
    public long ModelId = 0;
    public float PathSmoothing = 8f;
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
    public long OwnerId = 0;
    public string OwnerType = string.Empty;
    public string PathName = string.Empty;
    public float WaitTimeStart = 0f;
    public float WaitTimeEnd = 0f;
}

public class QuestSphereEntry
{
    public string WorldId = string.Empty;
    public int ZoneKey = -1;
    public int QuestId = -1;
    public int ComponentId = -1;
    public float X = 0.0f;
    public float Y = 0.0f;
    public float Z = 0.0f;
    public float Radius = 0.0f;
}

public class GamePlot
{
    public long Id = 0;
    public string Name = string.Empty;
    public long TargetTypeId = 0;
}

public class GamePlotEvent
{
    public long Id = 0;
    public long PlotId = 0;
    public long Postion = 0;
    public string Name = string.Empty;
    public long SourceUpdateMethodId = 0;
    public long TargetUpdateMethodId = 0;
    public long TargetUpdateMethodParam1 = 0;
    public long TargetUpdateMethodParam2 = 0;
    public long TargetUpdateMethodParam3 = 0;
    public long TargetUpdateMethodParam4 = 0;
    public long TargetUpdateMethodParam5 = 0;
    public long TargetUpdateMethodParam6 = 0;
    public long TargetUpdateMethodParam7 = 0;
    public long TargetUpdateMethodParam8 = 0;
    public long TargetUpdateMethodParam9 = 0;
    public long Tickets = 0;
    public bool AeoDiminishing = false;
}

public class GamePlotNextEvent
{
    public long Id = 0;
    public long EventId = 0;
    public long Postion = 0;
    public long NextEventId = 0;
    public long Delay = 0;
    public long Speed = 0;
    // Not loading the rest for now
}

public class GamePlotEventCondition
{
    public long Id = 0;
    public long EventId = 0;
    public long ConditionId = 0;
    public long Postion = 0;
    public long SourceId = 0;
    public long TargetId = 0;
    public bool NotifyFailure = false;
}

public class GamePlotEffect
{
    public long Id = 0;
    public long EventId = 0;
    public long Position = 0;
    public long SourceId = 0;
    public long TargetId = 0;
    public long ActualId = 0;
    public string ActualType = "";
}

public class GamePlotCondition
{
    public long Id = 0;
    public bool NotCondition = false;
    public long KindId = 0;
    public long Param1 = 0;
    public long Param2 = 0;
    public long Param3 = 0;
}

public class GameNpcSpawnerNpc
{
    public long Id = 0;
    public long NpcSpawnerId = 0;
    public long MemberId = 0;
    public string MemberType = string.Empty;
    public float Weight = 0f;
}

public class GameNpcSpawner
{
    public long Id = 0;
    public long NpcSpawnerCategoryId = 0;
    public string Name = string.Empty;
    public string Comment = string.Empty;
    public long MaxPopulation = 0;
    public float StartTime = 0f;
    public float EndTime = 0f;
    public float DestroyTime = 0f;
    public float SpawnDelayMin = 0f;
    public bool ActivationState = false;
    public bool SaveIndun = false;
    public long MinPopulation = 0;
    public float TestRadiusNpc = 0f;
    public float TestRadiusPc = 0f;
    public long SuspendSpawnCount = 0;
    public float SpawnDelayMax = 0f;
}

public class GameSpecialties
{
    public long Id = 0;
    public long RowZoneGroupId = 0;
    public long ColZoneGroupId = 0;
    public long Ratio = 0;
    public long Profit = 0;
    public bool VendorExist = false;
}

public class GameLoot
{
    public long Id = 0;
    public long Group = 0;
    public long ItemId = 0;
    public long DropRate = 0;
    public long MinAmount = 1;
    public long MaxAmount = 1;
    public long LootPackId = 0;
    public long GradeId = 0;
    public bool AlwaysDrop = false;
}

public class GameLootGroup
{
    public long Id = 0;
    public long PackId = 0;
    public long GroupNo = 0;
    public long DropRate = 0;
    public long ItemGradeDistributionId = 0;
}


public class GameLootPackDroppingNpc
{
    public long Id = 0;
    public long NpcId = 0;
    public long LootPackId = 0;
    public bool DefaultPack = false;
}

public class GameLootActAbilityGroup
{
    public long Id = 0;
    public long LootPackId = 0;
    public long LootGroupId = 0;
    public long MaxDice = 0;
    public long MinDice = 0;
}

public class GameSlaves
{
    public long Id = 0;
    public string Name = string.Empty;
    public long ModelId = 0;
    public bool Mountable = false;
    public float OffsetX = 0f;
    public float OffsetY = 0f;
    public float OffsetZ = 0f;
    public float ObbPosX = 0f;
    public float ObbPosY = 0f;
    public float ObbPosZ = 0f;
    public float ObbSizeX = 0f;
    public float ObbSizeY = 0f;
    public float ObbSizeZ = 0f;
    public long PortalSpawnFxId = 0;
    public float PortalScale = 0f;
    public float PortalTime = 0f;
    public long PortalDespawnFxId = 0;
    public long Hp25DoodadCount = 0;
    public long Hp50DoodadCount = 0;
    public long Hp75DoodadCount = 0;
    public float SpawnXOffset = 0f;
    public float SpawnYOffset = 0f;
    public long FactionId = 0;
    public long Level = 0;
    public long Cost = 0;
    public long SlaveKindId = 0;
    public long SpawnValidAreaRange = 0;
    public long SlaveInitialItemPackId = 0;
    public long SlaveCustomizingId = 0;
    public bool Customizable = false;

    public string NameLocalized = string.Empty;
    public string SearchText = string.Empty;
}

public class GameModel
{
    public long Id = 0;
    public string Comment = string.Empty;
    public long SubId = 0;
    public string SubType = string.Empty;
    public float DyingTime = 0f;
    public long SoundMaterialId = 0;
    public bool Big = false;
    public float TargetDecalSize = 0f;
    public bool UseTargetDecal = false;
    public bool UseTargetSilhouette = false;
    public bool UseTargetHighlight = false;
    public string Name = string.Empty;
    public float CameraDistance = 0f;
    public bool ShowNameTag = false;
    public float NameTagOffset = 0f;
    public long SoundPackId = 0;
    public bool DespawnDoodadOnCollision = false;
    public bool PlayMountAnimation = false;
    public bool Selectable = false;
    public long MountPoseId = 0;
    public float CameraDistanceForWideAngle = 0f;
}

public class GameNpSkills
{
    // Actual DB entries
    public long Id = 0;
    public long OwnerId = 0;
    public string OwnerType = string.Empty;
    public long SkillId = 0;
    public SkillUseConditionKind SkillUseConditionId = 0;
    public float SkillUseParam1 = 0f;
    public float SkillUseParam2 = 0f;
}

public enum SkillUseConditionKind
{
    None = -1,
    InCombat = 0,
    InIdle = 1,
    OnDeath = 2,
    InAlert = 3,
    InDead = 4,
    OnSpawn = 5,
    OnDespawn = 6,
    OnAlert = 7
}

public class GameNpcInitialBuffs
{
    // Actual DB entries
    public long Id = 0;
    public long NpcId = 0;
    public long BuffId = 0;
}

public class GameNpcInteractions
{
    // Actual DB entries
    public long Id = 0;
    public long NpcInteractionSetId = 0;
    public long SkillId = 0;
}

public class GameAiFiles
{
    // Actual DB entries
    public long Id = 0;
    public string Name = string.Empty;
    public string ParamTemplate = string.Empty;
}

public class GameAiCommands
{
    // Actual DB entries
    public long Id = 0;
    public long CmdSetId = 0;
    public long CmdId = 0;
    public long Param1 = 0;
    public string Param2 = string.Empty;
}

public class GamePassiveBuff
{
    public long Id = 0;
    public long AbilityId = 0;
    public long Level = 0;
    public long BuffId = 0;
    public long ReqPoints = 0;
    public bool Active = false;
}

public class GameSlavePassiveBuff
{
    public long Id = 0;
    public long OwnerId = 0;
    public string OwnerType = string.Empty;
    public long PassiveBuffId = 0;
}

public class GameSlaveInitialBuff
{
    public long Id = 0;
    public long SlaveId = 0;
    public long BuffId = 0;
}

public class GameNpPassiveBuff
{
    public long Id = 0;
    public long OwnerId = 0;
    public string OwnerType = string.Empty;
    public long PassiveBuffId = 0;
}

public class GameMountSkill
{
    public long Id = 0;
    public string Name = string.Empty;
    public long SkillId = 0;
}

public class GameSlaveMountSkill
{
    public long Id = 0;
    public long SlaveId = 0;
    public long MountSkillId = 0;
}

public class GameSlaveBinding
{
    public long Id = 0;
    public long OwnerId = 0;
    public string OwnerType = string.Empty;
    public long SlaveId = 0;
    public long AttachPointId = 0;
}

public class GameSlaveDoodadBinding
{
    public long Id = 0;
    public long OwnerId = 0;
    public string OwnerType = string.Empty;
    public long AttachPointId = 0;
    public long DoodadId = 0;
    public bool Persist = false;
    public float Scale = 0f;
}

public class GameScheduleItem
{
    public long Id = 0;
    public string Name = string.Empty;
    public int KindId = 0;
    public int StYear = 0;
    public int StMonth = 0;
    public int StDay = 0;
    public int StHour = 0;
    public int StMin = 0;
    public int EdYear = 0;
    public int EdMonth = 0;
    public int EdDay = 0;
    public int EdHour = 0;
    public int EdMin = 0;
    public long GiveTerm = 0;
    public long GiveMax = 0;
    public long ItemId = 0;
    public long ItemCount = 0;
    public long PremiumGradeId = 0;
    public bool ActiveTake = false;
    public bool OnAir = false;
    public string ToolTip = string.Empty;
    public bool ShowWherever = false;
    public bool ShowWhenever = false;
    public string IconPath = string.Empty;
    public string EnableKeyString = string.Empty;
    public string DisableKeyString = string.Empty;
    public string LabelKeyString = string.Empty;

    public override string ToString()
    {
        var isActive = OnAir ? " ON AIR" : "";
        return $"{Id:000}{isActive} [{KindId}]: {AaDb.GetTranslationById(Id, "schedule_items", "name", Name)}";
    }
}

public class GameGameSchedules
{
    public long Id = 0;
    public string Name = string.Empty;
    public AaDayOfWeek DayOfWeekId = 0;
    public long StartTime = 0;
    public long EndTime = 0;
    public long StYear = 0;
    public long StMonth = 0;
    public long StDay = 0;
    public long StHour = 0;
    public long StMin = 0;
    public long EdYear = 0;
    public long EdMonth = 0;
    public long EdDay = 0;
    public long EdHour = 0;
    public long EdMin = 0;
    public long StartTimeMin = 0;
    public long EndTimeMin = 0;

    public override string ToString()
    {
        return $"{Id:000} : {AaDb.GetTranslationById(Id, "game_schedules", "name", Name)}";
    }
}

public class GameScheduleQuest
{
    public long Id = 0;
    public long GameScheduleId = 0;
    public long QuestId = 0;
}

public class GameScheduleDoodads
{
    public long Id = 0;
    public long GameScheduleId = 0;
    public long DoodadId = 0;
}

public class GameScheduleSpawners
{
    public long Id = 0;
    public long GameScheduleId = 0;
    public long SpawnerId = 0;
}

public class GameTowerDefs
{
    public long Id = 0;

    public string Name = string.Empty;
    public string StartMsg = string.Empty;
    public string EndMsg = string.Empty;
    public float Tod = 0f;
    public float FirstWaveAfter = 0f;
    public long TargetNpcSpawnerId = 0;
    public long KillNpcId = 0;
    public long KillNpcCount = 0;
    public float ForceEndTime = 0f;
    public long TodDayInterval = 0;
    public string TitleMsg = string.Empty;
    public long MilestoneId = 0;

    // Helpers
    public string NameLocalized = string.Empty;
    public string StartMsgLocalized = string.Empty;
    public string EndMsgLocalized = string.Empty;
    public string TitleMsgLocalized = string.Empty;

    public override string ToString()
    {
        return $"{Id}: {TitleMsgLocalized}";
    }
}

public class GameTowerDefProgs
{
    public long Id = 0;
    public long TowerDefId = 0;
    public string Msg = string.Empty;
    public float CondToNextTime = 0f;
    public bool CondCompByAnd = false;

    // Helpers
    public string MsgLocalized = string.Empty;
}

public class GameTowerDefProgSpawnTargets
{
    public long Id = 0;
    public long TowerDefProgId = 0;
    public long SpawnTargetId = 0;
    public string SpawnTargetType = string.Empty;
    public bool DespawnOnNextStep = false;
}

public class GameTowerDefProgKillTargets
{
    public long Id = 0;
    public long TowerDefProgId = 0;
    public long KillTargetId = 0;
    public string KillTargetType = string.Empty;
    public long KillCount = 0;
}

public class GameConflictZones
{
    public long ZoneGroupId = 0;
    public long NumKills0 = 0;
    public long NumKills1 = 0;
    public long NumKills2 = 0;
    public long NumKills3 = 0;
    public long NumKills4 = 0;
    public long NoKillMin0 = 0;
    public long NoKillMin1 = 0;
    public long NoKillMin2 = 0;
    public long NoKillMin3 = 0;
    public long NoKillMin4 = 0;
    public long ConflictMin = 0;
    public long WarMin = 0;
    public long PeaceMin = 0;
    public long PeaceProtectedFactionId = 0;
    public long NuiaReturnPointId = 0;
    public long HariharaReturnPointId = 0;
    public long WarTowerDefId = 0;
    public long PeaceTowerDefId = 0;
    public bool Closed = false;
}

public class GameSpheres
{
    public long Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public bool EnterOrLeave { get; set; } = false;
    public long SphereDetailId { get; set; } = 0;
    public string SphereDetailType { get; set; } = string.Empty;
    public long TriggerConditionId { get; set; } = 0;
    public long TriggerConditionTime { get; set; } = 0;
    public string TeamMsg { get; set; } = string.Empty;
    public long CategoryId { get; set; } = 0;
    public bool OrUnitReqs { get; set; } = false;
    public bool IsPersonalMsg { get; set; } = false;
    // public long milestone_id { get; set; } = 0;
    // public bool name_tr { get; set; } = false;
    // public bool team_msg_tr { get; set; } = false;
}

public class GameUnitReqs
{
    public uint Id { get; set; }
    public uint OwnerId { get; set; }
    /// <summary>
    /// Possible values: AchievementObjective, AiEvent, ItemArmor, ItemWeapon, QuestComponent, Skill, Sphere
    /// </summary>
    public string OwnerType { get; set; }
    public GameUnitReqsKind KindId { get; set; }
    public uint Value1 { get; set; }
    public uint Value2 { get; set; }
}

public class GameUiTexts
{
    public long Id = 0;
    public string Key = string.Empty;
    public string Text = string.Empty;
    public long CategoryId = 0;

    // Helper
    public long InCategoryIdx = 0;
}

public class GameUnitModifiers
{
    public long Id = 0;
    public long OwnerId = 0;
    public string OwnerType = string.Empty;
    public UnitAttribute UnitAttributeId = 0;
    public long UnitModifierTypeId = 0;
    public long Value = 0;
    public long LinearLevelBonus = 0;
}

public class GameAchievements
{
    public long Id = 0;
    public string Name = string.Empty;
    public string Summary = string.Empty;
    public string Description = string.Empty;
    public long CategoryId = 0;
    public long SubCategoryId = 0;
    public long ParentAchievementId = 0;
    public bool IsActive = false;
    public bool IsHidden = false;
    public long Priority = 0;
    public bool OrUnitReqs = false;
    public bool CompleteOr = false;
    public long CompleteNum = 0;
    public long ItemId = 0;
    public long IconId = 0;

    public string NameLocalized = string.Empty;
    public string DescriptionLocalized = string.Empty;
    public string SummaryLocalized = string.Empty;
    public string SearchString = string.Empty;
}

public class GameAchievementObjectives
{
    public long Id = 0;
    public long AchievementId = 0;
    public bool OrUnitReqs = false;
    public long RecordId = 0;
}

public class GameItemGrades
{
    public long Id = 0;
    public string Name = string.Empty;
    public long GradeOrder = 0;
    public Color ColorArgb = Color.Black;
    /*
    public Color ColorArgbSecond = Color.Black;
    */
    public long IconId = 0;
    public long StatMultiplier = 0;
    public long RefundMultiplier = 0;
    public float VarHoldableDps = 0f;
    public float VarHoldableArmor = 0f;
    public float VarHoldableMagicDps = 0f;
    public float VarWearableArmor = 0f;
    public float VarWearableMagicResistance = 0f;
    public float VarHoldableHealDps = 0f;
    public float DurabilityValue = 0f;
    public long UpgradeRatio = 0;
    /*
    public long GradeEnchantSuccessRatio = 0;
    public long GradeEnchantGreatSuccessRatio = 0;
    public long GradeEnchantBreakRatio = 0;
    public long GradeEnchantDowngradeRatio = 0;
    public long GradeEnchantCost = 0;
    public long GradeEnchantDowngradeMin = 0;
    public long GradeEnchantDowngradeMax = 0;
    public long CurrencyId = 0;
    */

    public string NameLocalized = string.Empty;
}

public class GameItemGradeDistributions
{
    public long Id = 0;
    // public string Name = string.Empty;
    public Dictionary<long, long> Weights = new();
}

internal static class AaDb
{
    public static Dictionary<string, GameTranslation> DbTranslations = new();
    public static Dictionary<long, GameItemCategories> DbItemsCategories = new();
    public static Dictionary<long, GameItem> DbItems = new();
    public static Dictionary<long, GameItemArmors> DbItemArmors = new();
    public static Dictionary<long, GameItemWeapons> DbItemWeapons = new();
    public static Dictionary<long, GameEffects> DbEffects = new();
    public static Dictionary<long, GameSkills> DbSkills = new();
    public static Dictionary<long, GameSkillEffects> DbSkillEffects = new();
    public static Dictionary<long, GameNpSkills> DbNpSkills = new();
    public static Dictionary<long, GameMountSkill> DbMountSkills = new();
    public static Dictionary<long, GameSlaveMountSkill> DbSlaveMountSkills = new();
    public static Dictionary<long, GameNpc> DbNpCs = new();
    public static Dictionary<long, GameQuestMonsterGroups> DbQuestMonsterGroups = new();
    public static Dictionary<long, GameQuestMonsterNpcs> DbQuestMonsterNpcs = new();
    public static Dictionary<long, string> DbIcons = new();
    public static Dictionary<long, GameSkillItems> DbSkillReagents = new();
    public static Dictionary<long, GameSkillItems> DbSkillProducts = new();
    public static Dictionary<long, GameZone> DbZones = new();
    public static Dictionary<long, GameZoneGroups> DbZoneGroups = new();
    public static Dictionary<long, GameConflictZones> DbConflictZones = new();
    public static Dictionary<long, GameWorldGroups> DbWorldGroups = new();
    public static Dictionary<long, GameSystemFaction> DbGameSystemFactions = new();
    public static Dictionary<long, GameSystemFactionRelation> DbGameSystemFactionRelations = new();
    public static Dictionary<long, GameDoodad> DbDoodadAlmighties = new();
    public static Dictionary<long, GameDoodadGroup> DbDoodadGroups = new();
    public static Dictionary<long, GameDoodadFunc> DbDoodadFuncs = new();
    public static Dictionary<long, GameDoodadFuncGroup> DbDoodadFuncGroups = new();
    public static Dictionary<long, GameDoodadPhaseFunc> DbDoodadPhaseFuncs = new();
    public static Dictionary<long, GameQuestCategory> DbQuestCategories = new();
    public static Dictionary<long, GameQuestContexts> DbQuestContexts = new();
    public static Dictionary<long, GameQuestContextText> DbQuestContextTexts = new();
    public static Dictionary<long, GameQuestAct> DbQuestActs = new();
    public static Dictionary<long, GameQuestActConAcceptNpc> DbQuestActConAcceptNpc = new();
    public static Dictionary<long, GameQuestComponent> DbQuestComponents = new();
    public static Dictionary<long, GameQuestComponentText> DbQuestComponentTexts = new();
    public static Dictionary<long, GameTags> DbTags = new();
    public static Dictionary<long, GameTaggedValues> DbTaggedBuffs = new();
    public static Dictionary<long, GameTaggedValues> DbTaggedItems = new();
    public static Dictionary<long, GameTaggedValues> DbTaggedNpCs = new();
    public static Dictionary<long, GameTaggedValues> DbTaggedSkills = new();
    public static Dictionary<long, GameZoneGroupBannedTags> DbZoneGroupBannedTags = new();
    public static Dictionary<long, GameBuff> DbBuffs = new();
    public static Dictionary<long, GameBuffTrigger> DbBuffTriggers = new();
    public static Dictionary<long, GameBuffModifier> DbBuffModifiers = new();
    public static Dictionary<long, GamePassiveBuff> DbPassiveBuffs = new();
    public static Dictionary<long, GameNpPassiveBuff> DbNpPassiveBuffs = new();
    public static Dictionary<long, GameSlavePassiveBuff> DbSlavePassiveBuffs = new();
    public static Dictionary<long, GameSlaveInitialBuff> DbSlaveInitialBuffs = new();
    public static Dictionary<long, GameTransfers> DbTransfers = new();
    public static List<GameTransferPaths> DbTransferPaths = new();
    public static List<QuestSphereEntry> PakQuestSignSpheres = new();
    public static Dictionary<long, GamePlot> DbPlots = new();
    public static Dictionary<long, GamePlotEvent> DbPlotEvents = new();
    public static Dictionary<long, GamePlotNextEvent> DbPlotNextEvents = new();
    public static Dictionary<long, GamePlotEventCondition> DbPlotEventConditions = new();
    public static Dictionary<long, GamePlotEffect> DbPlotEffects = new();
    public static Dictionary<long, GamePlotCondition> DbPlotConditions = new();
    public static Dictionary<long, GameNpcSpawnerNpc> DbNpcSpawnerNpcs = new();
    public static Dictionary<long, GameNpcSpawner> DbNpcSpawners = new();
    public static Dictionary<long, GameSpecialties> DbSpecialities = new();
    public static Dictionary<long, GameLoot> DbLoots = new();
    public static Dictionary<long, GameLootGroup> DbLootGroups = new();
    public static Dictionary<long, GameLootPackDroppingNpc> DbLootPackDroppingNpc = new();
    public static Dictionary<long, GameLootActAbilityGroup> DbLootActAbilityGroups = new();
    public static Dictionary<long, GameSlaves> DbSlaves = new();
    public static Dictionary<long, GameSlaveBinding> DbSlaveBindings = new();
    public static Dictionary<long, GameSlaveDoodadBinding> DbSlaveDoodadBindings = new();
    public static Dictionary<long, GameModel> DbModels = new();
    public static Dictionary<long, GameNpcInitialBuffs> DbNpcInitialBuffs = new();
    public static Dictionary<long, GameNpcInteractions> DbNpcInteractions = new();
    public static Dictionary<long, GameAiFiles> DbAiFiles = new();
    public static Dictionary<long, GameAiCommands> DbAiCommands = new();
    public static Dictionary<long, GameScheduleItem> DbScheduleItems = new();
    public static Dictionary<long, GameGameSchedules> DbGameSchedules = new();
    public static Dictionary<long, GameScheduleQuest> DbScheduleQuest = new();
    public static Dictionary<long, GameScheduleDoodads> DbScheduleDoodads = new();
    public static Dictionary<long, GameScheduleSpawners> DbScheduleSpawners = new();
    public static Dictionary<long, GameTowerDefs> DbTowerDefs = new();
    public static Dictionary<long, GameTowerDefProgs> DbTowerDefProgs = new();
    public static Dictionary<long, GameTowerDefProgSpawnTargets> DbTowerDefProgSpawnTargets = new();
    public static Dictionary<long, GameTowerDefProgKillTargets> DbTowerDefProgKillTargets = new();
    public static Dictionary<long, GameSpheres> DbSpheres = new();
    public static Dictionary<long, GameUnitReqs> DbUnitReqs = new();
    public static Dictionary<long, GameUiTexts> DbUiTexts = new();
    public static Dictionary<long, GameUnitModifiers> DbUnitModifiers = new();
    public static Dictionary<long, GameAchievements> DbAchievements = new();
    public static Dictionary<long, GameAchievementObjectives> DbAchievementObjectives = new();
    public static Dictionary<long, GameItemGrades> DbItemGrades = new();
    public static Dictionary<long, GameItemGradeDistributions> DbItemGradeDistributions = new();

    public static Dictionary<long, Dictionary<long, Dictionary<long, GameAchievements>>> CompiledGroupedAchievements = new();

    public static void Clear()
    {
        DbTranslations = new();
        DbItemsCategories = new();
        DbItems = new();
        DbItemArmors = new();
        DbItemWeapons = new();
        DbEffects = new();
        DbSkills = new();
        DbSkillEffects = new();
        DbNpSkills = new();
        DbMountSkills = new();
        DbSlaveMountSkills = new();
        DbNpCs = new();
        DbQuestMonsterGroups = new();
        DbQuestMonsterNpcs = new();
        DbIcons = new();
        DbSkillReagents = new();
        DbSkillProducts = new();
        DbZones = new();
        DbZoneGroups = new();
        DbConflictZones = new();
        DbWorldGroups = new();
        DbGameSystemFactions = new();
        DbGameSystemFactionRelations = new();
        DbDoodadAlmighties = new();
        DbDoodadGroups = new();
        DbDoodadFuncs = new();
        DbDoodadFuncGroups = new();
        DbDoodadPhaseFuncs = new();
        DbQuestCategories = new();
        DbQuestContexts = new();
        DbQuestContextTexts = new();
        DbQuestActs = new();
        DbQuestActConAcceptNpc = new();
        DbQuestComponents = new();
        DbQuestComponentTexts = new();
        DbTags = new();
        DbTaggedBuffs = new();
        DbTaggedItems = new();
        DbTaggedNpCs = new();
        DbTaggedSkills = new();
        DbZoneGroupBannedTags = new();
        DbBuffs = new();
        DbBuffTriggers = new();
        DbBuffModifiers = new();
        DbPassiveBuffs = new();
        DbNpPassiveBuffs = new();
        DbSlavePassiveBuffs = new();
        DbSlaveInitialBuffs = new();
        DbTransfers = new();
        DbTransferPaths = new();
        PakQuestSignSpheres = new();
        DbPlots = new();
        DbPlotEvents = new();
        DbPlotNextEvents = new();
        DbPlotEventConditions = new();
        DbPlotEffects = new();
        DbPlotConditions = new();
        DbNpcSpawnerNpcs = new();
        DbNpcSpawners = new();
        DbSpecialities = new();
        DbLoots = new();
        DbLootGroups = new();
        DbLootPackDroppingNpc = new();
        DbLootActAbilityGroups = new();
        DbSlaves = new();
        DbSlaveBindings = new();
        DbSlaveDoodadBindings = new();
        DbModels = new();
        DbNpcInitialBuffs = new();
        DbNpcInteractions = new();
        DbAiFiles = new();
        DbAiCommands = new();
        DbScheduleItems = new();
        DbGameSchedules = new();
        DbScheduleQuest = new();
        DbScheduleDoodads = new();
        DbScheduleSpawners = new();
        DbTowerDefs = new();
        DbTowerDefProgs = new();
        DbTowerDefProgSpawnTargets = new();
        DbTowerDefProgKillTargets = new();
        DbSpheres = new();
        DbUnitReqs = new();
        DbUiTexts = new();
        DbUnitModifiers = new();
        DbAchievements = new();
        DbAchievementObjectives = new();
        DbItemGrades = new();
        DbItemGradeDistributions = new();

        CompiledGroupedAchievements = new();
    }

    public static string GetTranslationById(long idx, string table, string field, string defaultValue = "$NODEFAULT")
    {
        string res = string.Empty;
        string k = table + ":" + field + ":" + idx.ToString();
        if (DbTranslations != null && DbTranslations.TryGetValue(k, out GameTranslation val))
            res = val.Value;
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

    public static string GetTranslatedUiText(string uiTextKey, long categoryId, string defaultText)
    {
        var uiText = DbUiTexts.Values.FirstOrDefault(x => x.Key.Equals(uiTextKey, StringComparison.InvariantCultureIgnoreCase) && x.CategoryId == categoryId);
        if (uiText == null)
        {
            return defaultText;
        }

        return GetTranslationById(uiText.Id, "ui_texts", "text", defaultText);
    }

    public static string GetFactionName(long factionId, bool addId = false)
    {
        if (DbGameSystemFactions.TryGetValue(factionId, out var faction))
        {
            if (addId)
                return faction.NameLocalized + " (" + factionId + ")";
            else
                return faction.NameLocalized;
        }
        else
        if (factionId == 0)
        {
            if (addId)
                return "None (0)";
            else
                return "None";
        }
        else
        {
            if (addId)
                return "FactionID " + factionId.ToString();
            else
                return string.Empty;
        }
    }

    public static long GetFactionHostility(long f1, long f2)
    {
        foreach (var fr in DbGameSystemFactionRelations)
        {
            if (fr.Value.Faction1Id == f1 && fr.Value.Faction2Id == f2)
            {
                return fr.Value.StateId;
            }
            if (fr.Value.Faction1Id == f2 && fr.Value.Faction2Id == f1)
            {
                return fr.Value.StateId;
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

    public static void SetFactionRelationLabel(GameSystemFaction thisFaction, long targetFactionId, ref Label targetLabel)
    {
        var n = GetFactionHostility(thisFaction.Id, targetFactionId);
        if (n == 0 && thisFaction.MotherId != 0)
        {
            n = GetFactionHostility(thisFaction.MotherId, targetFactionId);
        }
        if (n == 0 && thisFaction.DiplomacyLinkId != 0)
        {
            n = GetFactionHostility(thisFaction.DiplomacyLinkId, targetFactionId);
        }
        targetLabel.Text = GetFactionHostileName(n);
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

    private static string FloatToCoordinates(double f)
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

    public static string CoordinatesToSextant(float x, float y)
    {
        var res = string.Empty;
        // https://www.reddit.com/r/archeage/comments/3dak17/datamining_every_location_of_everything_in/

        var fx = x / 1024f - 21f;
        var fy = y / 1024f - 28f;
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
        res += FloatToCoordinates(fx);
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
        res += FloatToCoordinates(fy);

        return res;
    }

    public static PointF SextantToCoordinates(float longitude, float latitude)
    {
        var ux = (longitude + 21f) * 1024f;
        var uy = (latitude + 28f) * 1024f;
        return new PointF(ux, uy);
    }

    public static GameZone GetZoneByKey(long zoneKey)
    {
        foreach (var z in DbZones)
            if (z.Value.ZoneKey == zoneKey)
                return z.Value;
        return null;
    }

    public static List<GameNpcSpawnerNpc> GetNpcSpawnerNpcsByNpcId(long id)
    {
        var res = new List<GameNpcSpawnerNpc>();
        foreach (var nsn in DbNpcSpawnerNpcs)
        {
            if (nsn.Value.MemberId == id && nsn.Value.MemberType.ToLower() == "npc")
                res.Add(nsn.Value);
        }
        return res;
    }
}