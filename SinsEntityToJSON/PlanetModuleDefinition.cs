
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace SinsEntityConverter
{
    public class PlanetModuleStandardDefinition : Def
    {
        [JsonPropertyOrder(0)]
        public string? Header = "TXT2";
        [JsonPropertyOrder(1)]
        public EntityInt? SinsArchiveVersion;
        [JsonPropertyOrder(2)]
        public string? entityType;
        [JsonPropertyOrder(3)]
        public EntityFloat? minZoomDistanceMult;
        [JsonPropertyOrder(4)]
        public List<string>? abilities;
        [JsonPropertyOrder(5)]
        public EntityFloat? MaxAntiMatter;
        [JsonPropertyOrder(6)]
        public EntityFloat? AntiMatterRestoreRate;
        [JsonPropertyOrder(7)]
        public EntityInt? numRandomDebrisLarge;
        [JsonPropertyOrder(8)]
        public EntityInt? numRandomDebrisSmall;
        [JsonPropertyOrder(9)]
        public EntityInt? numSpecificDebris;
        [JsonPropertyOrder(10)]
        public List<string>? specificDebrisMeshName;
        [JsonPropertyOrder(11)]
        public string? armorType;
        [JsonPropertyOrder(12)]
        public string? hudIcon;
        [JsonPropertyOrder(13)]
        public string? smallHudIcon;
        [JsonPropertyOrder(14)]
        public string? infoCardIcon;
        [JsonPropertyOrder(15)]
        public string? planetUpgradeSlotType;
        [JsonPropertyOrder(16)]
        public EntityFloat? planetUpgradeSlotCount;
        [JsonPropertyOrder(17)]
        public BasePrice? basePrice;
        [JsonPropertyOrder(18)]
        public string? NameStringID;
        [JsonPropertyOrder(19)]
        public string? DescriptionStringID;
        [JsonPropertyOrder(20)]
        public string? planetModuleRoleType;
        [JsonPropertyOrder(21)]
        public string? statCountType;
        [JsonPropertyOrder(22)]
        public Prerequisites? Prerequisites;
        [JsonPropertyOrder(23)]
        public EntityFloat? MaxHullPoints;
        [JsonPropertyOrder(24)]
        public EntityFloat? HullPointRestoreRate;
        [JsonPropertyOrder(25)]
        public EntityFloat? BaseArmorPoints;
        [JsonPropertyOrder(26)]
        public EntityFloat? MaxShieldPoints;
        [JsonPropertyOrder(27)]
        public EntityFloat? ShieldPointRestoreRate;
        [JsonPropertyOrder(28)]
        public EntityFloat? maxShieldMitigation;
        [JsonPropertyOrder(29)]
        public EntityFloat? ExperiencePointsForDestroying;
        [JsonPropertyOrder(30)]
        public string? resourceExtractionType;
        [JsonPropertyOrder(31)]
        public EntityFloat? cultureSpreadRate;
        [JsonPropertyOrder(32)]
        public EntityBool? isAffectedBySimilarModuleCostResearch;
        [JsonPropertyOrder(33)]
        public EntityFloat? placementRadius;
        [JsonPropertyOrder(34)]
        public EntityFloat? nearPlacementDistOffset;
        [JsonPropertyOrder(35)]
        public EntityFloat? farPlacementDistOffset;
        [JsonPropertyOrder(36)]
        public EntityInt? spawnCount;
        [JsonPropertyOrder(37)]
        public string? swapMineType;
        [JsonPropertyOrder(38)]
        public EntityFloat? angularSpeed;
        [JsonPropertyOrder(39)]
        public string? rotateSoundName;
        [JsonPropertyOrder(40)]
        public string? rotateFacingType;
        [JsonPropertyOrder(41)]
        public EntityBool? rotateConstantly;
        [JsonPropertyOrder(42)]
        public string? ShieldMeshName;
        [JsonPropertyOrder(43)]
        public EntityBool? renderShield;
        [JsonPropertyOrder(44)]
        public List<NumSoundsFor>? NumSounds;
        [JsonPropertyOrder(45)]
        public EntityInt? MeshNameInfoCount;
        [JsonPropertyOrder(46)]
        public List<MeshNameInfo>? MeshNameInfo;
        [JsonPropertyOrder(49)]
        public string? meshNameIncreasedEffectName;
        [JsonPropertyOrder(50)]
        public string? meshNameDecreasedEffectName;
        [JsonPropertyOrder(51)]
        public string? ExplosionName;
        [JsonPropertyOrder(52)]
        public string? mainViewIcon;
        [JsonPropertyOrder(53)]
        public string? picture;
        [JsonPropertyOrder(54)]
        public EntityFloat? minShadow;
        [JsonPropertyOrder(55)]
        public EntityFloat? maxShadow;
        [JsonPropertyOrder(56)]
        public EntityInt? formationRank;
        [JsonPropertyOrder(57)]
        public EntityFloat? baseBuildTime;
        [JsonPropertyOrder(58)]
        public string? buildEffectName;

        protected virtual bool ApplySpecialized(string key, string value)
        {
            return false;
        }

        protected Weapon? currentWeapon;
        protected Prerequisites? currentPrerequisite;
        protected WeaponEffects? currentEffects;
        protected EffectSounds? currentSounds;


        public override bool Apply(string key, string value)
        {
            switch (key)
            {
                case "SinsArchiveVersion":
                    SinsArchiveVersion = new EntityInt(value);
                    return true;

                case "entityType":
                    entityType = value;
                    return true;

                case "minZoomDistanceMult":
                    minZoomDistanceMult = new EntityFloat(value);
                    return true;

                case "ability:0":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "ability:1":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "ability:2":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "ability:3":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "ability:4":
                    abilities ??= new();
                    abilities.Add(value);
                    return true;

                case "MaxAntiMatter":
                    MaxAntiMatter = new EntityFloat(value);
                    return true;

                case "AntiMatterRestoreRate":
                    AntiMatterRestoreRate = new EntityFloat(value);
                    return true;

                case "numRandomDebrisLarge":
                    numRandomDebrisLarge = new EntityInt(value);
                    return true;

                case "numRandomDebrisSmall":
                    numRandomDebrisSmall = new EntityInt(value);
                    return true;

                case "numSpecificDebris":
                    numSpecificDebris = new EntityInt(value);
                    return true;

                case "specificDebrisMeshName":
                    specificDebrisMeshName ??= new();
                    specificDebrisMeshName.Add(value);
                    return true;

                case "armorType":
                    armorType = value;
                    return true;

                case "hudIcon":
                    hudIcon = value;
                    return true;

                case "smallHudIcon":
                    smallHudIcon = value;
                    return true;

                case "infoCardIcon":
                    infoCardIcon = value;
                    return true;

                case "planetUpgradeSlotType":
                    planetUpgradeSlotType = value;
                    return true;

                case "planetUpgradeSlotCount":
                    planetUpgradeSlotCount = new EntityFloat(value);
                    return true;

                case "basePrice":
                    basePrice = new BasePrice();
                    return true;

                case "credits":
                    basePrice!.Credits = new EntityFloat(value);
                    return true;

                case "metal":
                    basePrice!.Metal = new EntityFloat(value);
                    return true;

                case "crystal":
                    basePrice!.Crystal = new EntityFloat(value);
                    return true;

                case "NameStringID":
                    NameStringID = value;
                    return true;

                case "DescriptionStringID":
                    DescriptionStringID = value;
                    return true;

                case "planetModuleRoleType":
                    planetModuleRoleType = value;
                    return true;

                case "statCountType":
                    statCountType = value;
                    return true;

                case "Prerequisites":
                    Prerequisites = new();
                    currentPrerequisite = Prerequisites;
                    return true;

                case "NumResearchPrerequisites":
                    currentPrerequisite!.NumResearchPrerequisites = new EntityInt(value);
                    return true;

                case "ResearchPrerequisite":
                    currentPrerequisite!.ResearchPrerequisite ??= new();
                    currentPrerequisite.ResearchPrerequisite!.Add(new Research());
                    return true;

                case "Subject":
                    currentPrerequisite!.ResearchPrerequisite![^1].Subject = value;
                    return true;

                case "Level":
                    currentPrerequisite!.ResearchPrerequisite![^1].Level = new EntityInt(value);
                    return true;

                case "RequiredFactionNameID":
                    currentPrerequisite!.RequiredFactionNameID = value;
                    return true;

                case "RequiredCompletedResearchSubjects":
                    currentPrerequisite!.RequiredCompletedResearchSubjects = new EntityInt(value);
                    return true;

                case "MaxHullPoints":
                    currentPrerequisite = null;
                    MaxHullPoints = new EntityFloat(value);
                    return true;

                case "HullPointRestoreRate":
                    HullPointRestoreRate = new EntityFloat(value);
                    return true;

                case "BaseArmorPoints":
                    BaseArmorPoints = new EntityFloat(value);
                    return true;

                case "MaxShieldPoints":
                    MaxShieldPoints = new EntityFloat(value);
                    return true;

                case "ShieldPointRestoreRate":
                    ShieldPointRestoreRate = new EntityFloat(value);
                    return true;

                case "maxShieldMitigation":
                    maxShieldMitigation = new EntityFloat(value);
                    return true;

                case "ExperiencePointsForDestroying":
                    ExperiencePointsForDestroying = new EntityFloat(value);
                    return true;

                case "resourceExtractionType":
                    resourceExtractionType = value;
                    return true;

                case "cultureSpreadRate":
                    cultureSpreadRate = new EntityFloat(value);
                    return true;

                case "isAffectedBySimilarModuleCostResearch":
                    isAffectedBySimilarModuleCostResearch = new EntityBool(value);
                    return true;

                case "placementRadius":
                    placementRadius = new EntityFloat(value);
                    return true;

                case "nearPlacementDistOffset":
                    nearPlacementDistOffset = new EntityFloat(value);
                    return true;

                case "farPlacementDistOffset":
                    farPlacementDistOffset = new EntityFloat(value);
                    return true;

                case "spawnCount":
                    spawnCount = new EntityInt(value);
                    return true;

                case "swapMineType":
                    swapMineType = value;
                    return true;

                case "angularSpeed":
                    angularSpeed = new EntityFloat(value);
                    return true;

                case "rotateSoundName":
                    rotateSoundName = value;
                    return true;

                case "rotateFacingType":
                    rotateFacingType = value;
                    return true;

                case "rotateConstantly":
                    rotateConstantly = new EntityBool(value);
                    return true;

                case "ShieldMeshName":
                    ShieldMeshName = value;
                    return true;

                case "renderShield":
                    renderShield = new EntityBool(value);
                    return true;

                case string k when k.StartsWith("NumSoundsFor:"):
                    NumSounds ??= new();
                    NumSounds.Add(new NumSoundsFor
                    {
                        For = new() { { k["NumSoundsFor:".Length..], new EntityInt(value) } },
                        SoundID = new()
                    });
                    return true;

                case "SoundID":
                    NumSounds![^1].SoundID!.Add(value);
                    return true;

                case "MeshNameInfoCount":
                    MeshNameInfoCount = new EntityInt(value);
                    return true;

                case "MeshNameInfo":
                    MeshNameInfo ??= new();
                    MeshNameInfo.Add(new MeshNameInfo());
                    return true;

                case "meshName":
                    MeshNameInfo![^1].meshName = value;
                    return true;

                case "criteriaType":
                    MeshNameInfo![^1].criteriaType = value;
                    return true;

                case "meshNameIncreasedEffectName":
                    currentPrerequisite = null;
                    meshNameIncreasedEffectName = value;
                    return true;

                case "meshNameDecreasedEffectName":
                    meshNameDecreasedEffectName = value;
                    return true;

                case "ExplosionName":
                    ExplosionName = value;
                    return true;

                case "mainViewIcon":
                    mainViewIcon = value;
                    return true;

                case "picture":
                    picture = value;
                    return true;

                case "minShadow":
                    minShadow = new EntityFloat(value);
                    return true;

                case "maxShadow":
                    maxShadow = new EntityFloat(value);
                    return true;

                case "formationRank":
                    formationRank = new EntityInt(value);
                    return true;

                case "baseBuildTime":
                    baseBuildTime = new EntityFloat(value);
                    return true;

                case "buildEffectName":
                    buildEffectName = value;
                    return true;

                default:
                    if (ApplySpecialized(key, value))
                        return true;

                    Debug.WriteLine($"No match found. Key: {key} | Value: {value}");
                    return false;
            }
        }
    }

    public class PlanetModuleHangarDefenseDefinition : PlanetModuleStandardDefinition
    {
        [JsonPropertyOrder(47)]
        public List<MeshNameInfoPrerequisites>? meshNameInfoPrerequisites;
        [JsonPropertyOrder(48)]
        public MeshNameInfoPrerequisites? prerequisites;

        [JsonPropertyOrder(59)]
        public Dictionary<EntityInt, string>? squadTypeEntityDef;
        [JsonPropertyOrder(60)]
        public Dictionary<EntityInt, EntityFloat>? squadAntiMatterCost;
        [JsonPropertyOrder(61)]
        public EntityInt? NumWeapons;
        [JsonPropertyOrder(62)]
        public List<Weapon>? weapons;
        [JsonPropertyOrder(63)]
        public List<WeaponResearch>? weaponsResearch;
        [JsonPropertyOrder(64)]
        public EntityInt? m_weaponIndexForRange;
        [JsonPropertyOrder(65)]
        public string? firingAlignmentType;
        [JsonPropertyOrder(66)]
        public Dictionary<BankDirection, EntityInt>? TargetCountPerBank;
        [JsonPropertyOrder(67)]
        public EntityBool? canOnlyTargetStructures;
        [JsonPropertyOrder(68)]
        public EntityInt? baseCommandPoints;
        [JsonPropertyOrder(69)]
        public string? defaultAutoAttackRange;
        [JsonPropertyOrder(70)]
        public EntityBool? defaultAutoAttackOn;
        [JsonPropertyOrder(71)]
        public EntityBool? prefersToFocusFire;
        [JsonPropertyOrder(72)]
        public EntityBool? usesFighterAttack;

        protected override bool ApplySpecialized(string key, string value)
        {
            switch (key)
            {
                case "prerequisites":
                    meshNameInfoPrerequisites = new();

                    for (int i = 0; i < MeshNameInfo!.Count; i++)
                    {
                        MeshNameInfoPrerequisites item = new()
                        {
                            criteriaType = MeshNameInfo[i].criteriaType,
                            meshName = MeshNameInfo[i].meshName
                        };

                        meshNameInfoPrerequisites.Add(item);
                    }

                    meshNameInfoPrerequisites![^1].prerequisites = new();
                    currentPrerequisite = meshNameInfoPrerequisites[^1].prerequisites;

                    MeshNameInfo = null;
                    return true;

                case "squadTypeEntityDef:0":
                    currentPrerequisite = null;
                    squadTypeEntityDef ??= new();
                    squadTypeEntityDef!.Add(new EntityInt(0), value);
                    return true;

                case "squadTypeEntityDef:1":
                    squadTypeEntityDef ??= new();
                    squadTypeEntityDef!.Add(new EntityInt(1), value);
                    return true;

                case "squadTypeEntityDef:2":
                    squadTypeEntityDef ??= new();
                    squadTypeEntityDef!.Add(new EntityInt(2), value);
                    return true;

                case "squadTypeEntityDef:3":
                    squadTypeEntityDef ??= new();
                    squadTypeEntityDef!.Add(new EntityInt(3), value);
                    return true;

                case "squadAntiMatterCost:0":
                    squadAntiMatterCost ??= new();
                    squadAntiMatterCost!.Add(new EntityInt(0), new EntityFloat(value));
                    return true;

                case "squadAntiMatterCost:1":
                    squadAntiMatterCost ??= new();
                    squadAntiMatterCost!.Add(new EntityInt(1), new EntityFloat(value));
                    return true;

                case "squadAntiMatterCost:2":
                    squadAntiMatterCost ??= new();
                    squadAntiMatterCost!.Add(new EntityInt(2), new EntityFloat(value));
                    return true;

                case "squadAntiMatterCost:3":
                    squadAntiMatterCost ??= new();
                    squadAntiMatterCost!.Add(new EntityInt(3), new EntityFloat(value));
                    return true;

                case "NumWeapons":
                    NumWeapons = new EntityInt(value);
                    return true;

                case "Weapon":
                    weapons ??= new();
                    weapons.Add(new Weapon());
                    currentWeapon = weapons[^1];
                    return true;

                case "WeaponType":
                    currentWeapon!.WeaponType = value;
                    return true;

                case "damageEnums":
                    currentWeapon!.damageEnums = new DamageEnums();
                    return true;

                case "AttackType":
                    currentWeapon!.damageEnums!.AttackType = value;
                    return true;

                case "DamageAffectType":
                    currentWeapon!.damageEnums!.DamageAffectType = value;
                    return true;

                case "DamageApplyType":
                    currentWeapon!.damageEnums!.DamageApplyType = value;
                    return true;

                case "DamageType":
                    currentWeapon!.damageEnums!.DamageType = value;
                    return true;

                case "WeaponClassType":
                    currentWeapon!.damageEnums!.WeaponClassType = value;
                    return true;

                case "DamagePerBank:FRONT":
                    currentWeapon!.DamagePerBank ??= new();
                    currentWeapon!.DamagePerBank!.Add(BankDirection.Front, new EntityFloat(value));
                    return true;

                case "DamagePerBank:BACK":
                    currentWeapon!.DamagePerBank ??= new();
                    currentWeapon!.DamagePerBank!.Add(BankDirection.Back, new EntityFloat(value));
                    return true;

                case "DamagePerBank:LEFT":
                    currentWeapon!.DamagePerBank ??= new();
                    currentWeapon!.DamagePerBank!.Add(BankDirection.Left, new EntityFloat(value));
                    return true;

                case "DamagePerBank:RIGHT":
                    currentWeapon!.DamagePerBank ??= new();
                    currentWeapon!.DamagePerBank!.Add(BankDirection.Right, new EntityFloat(value));
                    return true;

                case "Range":
                    currentWeapon!.Range = new EntityFloat(value);
                    return true;

                case "PreBuffCooldownTime":
                    currentWeapon!.PreBuffCooldownTime = new EntityFloat(value);
                    return true;

                case "CanFireAtFighter":
                    currentWeapon!.CanFireAtFighter = new EntityBool(value);
                    return true;

                case "SynchronizedTargeting":
                    currentWeapon!.SynchronizedTargeting = new EntityBool(value);
                    return true;

                case "PointStaggerDelay":
                    currentWeapon!.PointStaggerDelay = new EntityFloat(value);
                    return true;

                case "TravelSpeed":
                    currentWeapon!.TravelSpeed = new EntityFloat(value);
                    return true;

                case "Duration":
                    currentWeapon!.Duration = new EntityFloat(value);
                    return true;

                case "fireConstraintType":
                    currentWeapon!.fireConstraintType = value;
                    return true;

                case "researchPrerequisites":
                    weaponsResearch = new();

                    for (int i = 0; i < weapons!.Count; i++)
                    {
                        WeaponResearch item = new()
                        {
                            WeaponType = weapons[i].WeaponType,
                            damageEnums = weapons[i].damageEnums,
                            DamagePerBank = weapons[i].DamagePerBank,
                            Range = weapons[i].Range,
                            PreBuffCooldownTime = weapons[i].PreBuffCooldownTime,
                            CanFireAtFighter = weapons[i].CanFireAtFighter,
                            SynchronizedTargeting = weapons[i].SynchronizedTargeting,
                            PointStaggerDelay = weapons[i].PointStaggerDelay,
                            TravelSpeed = weapons[i].TravelSpeed,
                            Duration = weapons[i].Duration,
                            fireConstraintType = weapons[i].fireConstraintType
                        };

                        weaponsResearch.Add(item);
                    }

                    weaponsResearch![^1].researchPrerequisites = new();
                    currentPrerequisite = weaponsResearch![^1].researchPrerequisites;

                    weapons = null;
                    currentWeapon = weaponsResearch![^1];
                    return true;

                case "WeaponEffects":
                    currentPrerequisite = null;
                    return true;

                case "weaponType":
                    switch (value)
                    {
                        case "Projectile":
                            currentWeapon!.WeaponEffects = new();
                            currentEffects = currentWeapon!.WeaponEffects;
                            return true;
                        case "Beam":
                            currentWeapon!.WeaponEffects_B = new();
                            currentEffects = currentWeapon!.WeaponEffects_B;
                            return true;
                        case "Missile":
                            currentWeapon!.WeaponEffects_M = new();
                            currentEffects = currentWeapon!.WeaponEffects_M;
                            return true;
                    }

                    return false;

                case "burstCount":
                    currentEffects!.burstCount = new EntityInt(value);
                    return true;

                case "burstDelay":
                    currentEffects!.burstDelay = new EntityFloat(value);
                    return true;

                case "fireDelay":
                    currentEffects!.fireDelay = new EntityFloat(value);
                    return true;

                case "muzzleEffectName":
                    currentEffects!.muzzleEffectName = value;
                    return true;

                case "muzzleSoundMinRespawnTime":
                    currentEffects!.muzzleSoundMinRespawnTime = new EntityFloat(value);
                    return true;

                case "muzzleSounds":
                    currentEffects!.muzzleSounds = new();
                    currentSounds = currentEffects!.muzzleSounds;
                    return true;

                case "hitEffectName":
                    currentEffects!.hitEffectName = value;
                    return true;

                case "hitHullEffectSounds":
                    currentEffects!.hitHullEffectSounds = new();
                    currentSounds = currentEffects!.hitHullEffectSounds;
                    return true;

                case "hitShieldsEffectSounds":
                    currentEffects!.hitShieldsEffectSounds = new();
                    currentSounds = currentEffects!.hitShieldsEffectSounds;
                    return true;

                case "projectileTravelEffectName":
                    currentEffects!.projectileTravelEffectName = value;
                    return true;

                case "beamEffectSounds":
                    ((WeaponEffectsBeam)currentEffects!).beamEffectSounds = new();
                    currentSounds = ((WeaponEffectsBeam)currentEffects!).beamEffectSounds;
                    return true;

                case "beamGlowTextureName":
                    ((WeaponEffectsBeam)currentEffects!).beamGlowTextureName = value;
                    return true;

                case "beamCoreTextureName":
                    ((WeaponEffectsBeam)currentEffects!).beamCoreTextureName = value;
                    return true;

                case "beamWidth":
                    ((WeaponEffectsBeam)currentEffects!).beamWidth = new EntityFloat(value);
                    return true;

                case "beamGlowColor":
                    ((WeaponEffectsBeam)currentEffects!).beamGlowColor = value;
                    return true;

                case "beamCoreColor":
                    ((WeaponEffectsBeam)currentEffects!).beamCoreColor = value;
                    return true;

                case "beamTilingRate":
                    ((WeaponEffectsBeam)currentEffects!).beamTilingRate = new EntityFloat(value);
                    return true;

                case "missileTravelEffectName":
                    ((WeaponEffectsMissile)currentEffects!).missileTravelEffectName = value;
                    return true;

                case "missileStartTurningDistance":
                    ((WeaponEffectsMissile)currentEffects!).missileStartTurningDistance = new EntityFloat(value);
                    return true;

                case "missileSlowTurnRate":
                    ((WeaponEffectsMissile)currentEffects!).missileSlowTurnRate = new EntityFloat(value);
                    return true;

                case "missileMaxSlowTurnTime":
                    ((WeaponEffectsMissile)currentEffects!).missileMaxSlowTurnTime = new EntityFloat(value);
                    return true;

                case "soundCount":
                    if (currentSounds != null)
                        currentSounds.soundCount = new EntityInt(value);
                    return true;

                case "sound":
                    if (currentSounds != null)
                        currentSounds.sounds.Add(value);
                    return true;

                case "m_weaponIndexForRange":
                    currentWeapon = null;
                    currentSounds = null;
                    currentEffects = null;

                    m_weaponIndexForRange = new EntityInt(value);
                    return true;

                case "firingAlignmentType":
                    firingAlignmentType = value;
                    return true;

                case "TargetCountPerBank:FRONT":
                    TargetCountPerBank ??= new();
                    TargetCountPerBank!.Add(BankDirection.Front, new EntityInt(value));
                    return true;

                case "TargetCountPerBank:BACK":
                    TargetCountPerBank ??= new();
                    TargetCountPerBank!.Add(BankDirection.Back, new EntityInt(value));
                    return true;

                case "TargetCountPerBank:LEFT":
                    TargetCountPerBank ??= new();
                    TargetCountPerBank!.Add(BankDirection.Left, new EntityInt(value));
                    return true;

                case "TargetCountPerBank:RIGHT":
                    TargetCountPerBank ??= new();
                    TargetCountPerBank!.Add(BankDirection.Right, new EntityInt(value));
                    return true;

                case "canOnlyTargetStructures":
                    canOnlyTargetStructures = new EntityBool(value);
                    return true;

                case "baseCommandPoints":
                    baseCommandPoints = new EntityInt(value);
                    return true;

                case "defaultAutoAttackRange":
                    defaultAutoAttackRange = value;
                    return true;

                case "defaultAutoAttackOn":
                    defaultAutoAttackOn = new EntityBool(value);
                    return true;

                case "usesFighterAttack":
                    usesFighterAttack = new EntityBool(value);
                    return true;

                case "prefersToFocusFire":
                    prefersToFocusFire = new EntityBool(value);
                    return true;

                default:
                    return false;
            }
        }
    }

    public class PlanetModuleMissileDefenseDefinition : PlanetModuleStandardDefinition
    {
        [JsonPropertyOrder(47)]
        public List<MeshNameInfoPrerequisites>? meshNameInfoPrerequisites;
        [JsonPropertyOrder(48)]
        public MeshNameInfoPrerequisites? prerequisites;

        [JsonPropertyOrder(59)]
        public string? defaultAutoAttackRange;
        [JsonPropertyOrder(60)]
        public EntityBool? defaultAutoAttackOn;
        [JsonPropertyOrder(61)]
        public EntityBool? prefersToFocusFire;
        [JsonPropertyOrder(62)]
        public EntityBool? usesFighterAttack;
        [JsonPropertyOrder(63)]
        public EntityInt? NumWeapons;
        [JsonPropertyOrder(64)]
        public List<Weapon>? weapons;
        [JsonPropertyOrder(65)]
        public EntityInt? m_weaponIndexForRange;
        [JsonPropertyOrder(66)]
        public string? firingAlignmentType;
        [JsonPropertyOrder(67)]
        public Dictionary<BankDirection, EntityInt>? TargetCountPerBank;
        [JsonPropertyOrder(68)]
        public EntityBool? canOnlyTargetStructures;
        

        protected override bool ApplySpecialized(string key, string value)
        {
            switch (key)
            {
                case "prerequisites":
                    meshNameInfoPrerequisites = new();

                    for (int i = 0; i < MeshNameInfo!.Count; i++)
                    {
                        MeshNameInfoPrerequisites item = new()
                        {
                            criteriaType = MeshNameInfo[i].criteriaType,
                            meshName = MeshNameInfo[i].meshName
                        };

                        meshNameInfoPrerequisites.Add(item);
                    }

                    meshNameInfoPrerequisites![^1].prerequisites = new();
                    currentPrerequisite = meshNameInfoPrerequisites[^1].prerequisites;

                    MeshNameInfo = null;
                    return true;

                case "defaultAutoAttackRange":
                    currentPrerequisite = null;
                    defaultAutoAttackRange = value;
                    return true;

                case "defaultAutoAttackOn":
                    defaultAutoAttackOn = new EntityBool(value);
                    return true;

                case "prefersToFocusFire":
                    prefersToFocusFire = new EntityBool(value);
                    return true;

                case "usesFighterAttack":
                    usesFighterAttack = new EntityBool(value);
                    return true;

                case "NumWeapons":
                    NumWeapons = new EntityInt(value);
                    return true;

                case "Weapon":
                    weapons ??= new();
                    weapons.Add(new Weapon());
                    return true;

                case "WeaponType":
                    weapons![^1].WeaponType = value;
                    return true;

                case "damageEnums":
                    weapons![^1].damageEnums = new DamageEnums();
                    return true;

                case "AttackType":
                    weapons![^1].damageEnums!.AttackType = value;
                    return true;

                case "DamageAffectType":
                    weapons![^1].damageEnums!.DamageAffectType = value;
                    return true;

                case "DamageApplyType":
                    weapons![^1].damageEnums!.DamageApplyType = value;
                    return true;

                case "DamageType":
                    weapons![^1].damageEnums!.DamageType = value;
                    return true;

                case "WeaponClassType":
                    weapons![^1].damageEnums!.WeaponClassType = value;
                    return true;

                case "DamagePerBank:FRONT":
                    weapons![^1].DamagePerBank ??= new();
                    weapons![^1].DamagePerBank!.Add(BankDirection.Front, new EntityFloat(value));
                    return true;

                case "DamagePerBank:BACK":
                    weapons![^1].DamagePerBank ??= new();
                    weapons![^1].DamagePerBank!.Add(BankDirection.Back, new EntityFloat(value));
                    return true;

                case "DamagePerBank:LEFT":
                    weapons![^1].DamagePerBank ??= new();
                    weapons![^1].DamagePerBank!.Add(BankDirection.Left, new EntityFloat(value));
                    return true;

                case "DamagePerBank:RIGHT":
                    weapons![^1].DamagePerBank ??= new();
                    weapons![^1].DamagePerBank!.Add(BankDirection.Right, new EntityFloat(value));
                    return true;

                case "Range":
                    weapons![^1].Range = new EntityFloat(value);
                    return true;

                case "PreBuffCooldownTime":
                    weapons![^1].PreBuffCooldownTime = new EntityFloat(value);
                    return true;

                case "CanFireAtFighter":
                    weapons![^1].CanFireAtFighter = new EntityBool(value);
                    return true;

                case "SynchronizedTargeting":
                    weapons![^1].SynchronizedTargeting = new EntityBool(value);
                    return true;

                case "PointStaggerDelay":
                    weapons![^1].PointStaggerDelay = new EntityFloat(value);
                    return true;

                case "TravelSpeed":
                    weapons![^1].TravelSpeed = new EntityFloat(value);
                    return true;

                case "Duration":
                    weapons![^1].Duration = new EntityFloat(value);
                    return true;

                case "fireConstraintType":
                    weapons![^1].fireConstraintType = value;
                    return true;

                case "WeaponEffects":
                    return true;

                case "weaponType":
                    switch (value)
                    {
                        case "Projectile":
                            weapons![^1].WeaponEffects = new();
                            currentEffects = weapons![^1].WeaponEffects;
                            return true;
                        case "Beam":
                            weapons![^1].WeaponEffects_B = new();
                            currentEffects = weapons![^1].WeaponEffects_B;
                            return true;
                        case "Missile":
                            weapons![^1].WeaponEffects_M = new();
                            currentEffects = weapons![^1].WeaponEffects_M;
                            return true;
                    }

                    return false;

                case "burstCount":
                    currentEffects!.burstCount = new EntityInt(value);
                    return true;

                case "burstDelay":
                    currentEffects!.burstDelay = new EntityFloat(value);
                    return true;

                case "fireDelay":
                    currentEffects!.fireDelay = new EntityFloat(value);
                    return true;

                case "muzzleEffectName":
                    currentEffects!.muzzleEffectName = value;
                    return true;

                case "muzzleSoundMinRespawnTime":
                    currentEffects!.muzzleSoundMinRespawnTime = new EntityFloat(value);
                    return true;

                case "muzzleSounds":
                    currentEffects!.muzzleSounds = new();
                    currentSounds = currentEffects!.muzzleSounds;
                    return true;

                case "hitEffectName":
                    currentEffects!.hitEffectName = value;
                    return true;

                case "hitHullEffectSounds":
                    currentEffects!.hitHullEffectSounds = new();
                    currentSounds = currentEffects!.hitHullEffectSounds;
                    return true;

                case "hitShieldsEffectSounds":
                    currentEffects!.hitShieldsEffectSounds = new();
                    currentSounds = currentEffects!.hitShieldsEffectSounds;
                    return true;

                case "projectileTravelEffectName":
                    currentEffects!.projectileTravelEffectName = value;
                    return true;

                case "beamEffectSounds":
                    ((WeaponEffectsBeam)currentEffects!).beamEffectSounds = new();
                    currentSounds = ((WeaponEffectsBeam)currentEffects!).beamEffectSounds;
                    return true;

                case "beamGlowTextureName":
                    ((WeaponEffectsBeam)currentEffects!).beamGlowTextureName = value;
                    return true;

                case "beamCoreTextureName":
                    ((WeaponEffectsBeam)currentEffects!).beamCoreTextureName = value;
                    return true;

                case "beamWidth":
                    ((WeaponEffectsBeam)currentEffects!).beamWidth = new EntityFloat(value);
                    return true;

                case "beamGlowColor":
                    ((WeaponEffectsBeam)currentEffects!).beamGlowColor = value;
                    return true;

                case "beamCoreColor":
                    ((WeaponEffectsBeam)currentEffects!).beamCoreColor = value;
                    return true;

                case "beamTilingRate":
                    ((WeaponEffectsBeam)currentEffects!).beamTilingRate = new EntityFloat(value);
                    return true;

                case "missileTravelEffectName":
                    ((WeaponEffectsMissile)currentEffects!).missileTravelEffectName = value;
                    return true;

                case "missileStartTurningDistance":
                    ((WeaponEffectsMissile)currentEffects!).missileStartTurningDistance = new EntityFloat(value);
                    return true;

                case "missileSlowTurnRate":
                    ((WeaponEffectsMissile)currentEffects!).missileSlowTurnRate = new EntityFloat(value);
                    return true;

                case "missileMaxSlowTurnTime":
                    ((WeaponEffectsMissile)currentEffects!).missileMaxSlowTurnTime = new EntityFloat(value);
                    return true;

                case "soundCount":
                    if (currentSounds != null)
                        currentSounds.soundCount = new EntityInt(value);
                    return true;

                case "sound":
                    if (currentSounds != null)
                        currentSounds.sounds.Add(value);
                    return true;

                case "m_weaponIndexForRange":
                    currentEffects = null;
                    currentSounds = null;

                    m_weaponIndexForRange = new EntityInt(value);
                    return true;

                case "firingAlignmentType":
                    firingAlignmentType = value;
                    return true;

                case "TargetCountPerBank:FRONT":
                    TargetCountPerBank ??= new();
                    TargetCountPerBank!.Add(BankDirection.Front, new EntityInt(value));
                    return true;

                case "TargetCountPerBank:BACK":
                    TargetCountPerBank ??= new();
                    TargetCountPerBank!.Add(BankDirection.Back, new EntityInt(value));
                    return true;

                case "TargetCountPerBank:LEFT":
                    TargetCountPerBank ??= new();
                    TargetCountPerBank!.Add(BankDirection.Left, new EntityInt(value));
                    return true;

                case "TargetCountPerBank:RIGHT":
                    TargetCountPerBank ??= new();
                    TargetCountPerBank!.Add(BankDirection.Right, new EntityInt(value));
                    return true;

                case "canOnlyTargetStructures":
                    canOnlyTargetStructures = new EntityBool(value);
                    return true;

                default:
                    return false;
            }
        }
    }

    public class PlanetModuleCargoDefinition : PlanetModuleStandardDefinition
    {
        [JsonPropertyOrder(47)]
        public string? cargoShipType;
        [JsonPropertyOrder(48)]
        public EntityInt? maxCargoShipCount;

        [JsonPropertyOrder(59)]
        public EntityFloat? cargoShipRespawnTime;
        [JsonPropertyOrder(60)]
        public EntityFloat? baseTradeIncomeRate;

        protected override bool ApplySpecialized(string key, string value)
        {
            switch (key)
            {
                case "cargoShipType":
                    cargoShipType = value;
                    return true;

                case "maxCargoShipCount":
                    maxCargoShipCount = new EntityInt(value);
                    return true;

                case "cargoShipRespawnTime":
                    cargoShipRespawnTime = new EntityFloat(value);
                    return true;

                case "baseTradeIncomeRate":
                    baseTradeIncomeRate = new EntityFloat(value);
                    return true;

                default:
                    return false;
            }
        }
    }
}
