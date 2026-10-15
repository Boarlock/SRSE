using System.Diagnostics;

namespace SinsEntityConverter
{
    public class FrigateDefinition : Def
    {
        public string? Header = "TXT2";
        public EntityInt? SinsArchiveVersion;
        public string? entityType;
        public string? defaultAutoAttackRange;
        public EntityBool? defaultAutoAttackOn;
        public EntityBool? prefersToFocusFire;
        public EntityBool? usesFighterAttack;
        public EntityBool? autoJoinFleetDefault;
        public EntityBool? canBomb;
        public EntityBool? hasBombingLevels; 

        public EntityFloat? baseDamage;
        public baseDamageLevels? baseDamage_L;

        public EntityFloat? basePopulationKilled;
        public basePopulationKilledLevels? basePopulationKilled_L;

        public EntityFloat? bombingFreqTime;
        public EntityFloat? baseRange;
        public EntityFloat? bombTransitTime;
        public EntityInt? bombEffectCount;
        public EntityFloat? bombEffectAngleVariance;

        public WeaponEffects? bombEffectsDef;
        public WeaponEffectsBeam? bombEffectsDef_B;
        public WeaponEffectsMissile? bombEffectsDef_M;

        public EntityInt? typeCount;
        public string? frigateRoleType;
        public string? statCountType;
        public string? mainViewIcon;
        public string? picture;
        public string? NameStringID;
        public string? DescriptionStringID;
        public string? counterDescriptionStringID;
        public BasePrice? basePrice;
        public EntityFloat? slotCount;
        public EntityFloat? BuildTime;
        public EntityBool? hasLevels;

        public EntityFloat? ExperiencePointsForDestroying;
        public ExperiencePointsForDestroyingLevels? ExperiencePointsForDestroying_L;
        public EntityFloat? MaxHullPoints;
        public MaxHullPointsLevels? MaxHullPoints_L;
        public EntityFloat? MaxShieldPoints;
        public MaxShieldPointsLevels? MaxShieldPoints_L;
        public EntityFloat? HullPointRestoreRate;
        public HullPointRestoreRateLevels? HullPointRestoreRate_L;
        public EntityFloat? ShieldPointRestoreRate;
        public ShieldPointRestoreRateLevels? ShieldPointRestoreRate_L;
        public EntityFloat? BaseArmorPoints;
        public BaseArmorPointsLevels? BaseArmorPoints_L;
        public EntityFloat? maxMitigation;
        public maxMitigationLevels? maxMitigation_L;
        public EntityFloat? relationshipChangeRate;
        public relationshipChangeRateLevels? relationshipChangeRate_L;
        
        public Prerequisites? Prerequisites;
        public EntityInt? numRandomDebrisLarge;
        public EntityInt? numRandomDebrisSmall;
        public EntityInt? numSpecificDebris;
        public List<string>? specificDebrisMeshName;
        public string? armorType;
        public string? hudIcon;
        public string? smallHudIcon;
        public string? infoCardIcon;
        public EntityFloat? minZoomDistanceMult;
        public EntityInt? NumWeapons;
        public List<Weapon>? weapons;
        public EntityInt? m_weaponIndexForRange;
        public string? firingAlignmentType;
        public EntityFloat? firingTiltAngle;
        public Dictionary<BankDirection, EntityInt>? TargetCountPerBank;
        public EntityBool? canOnlyTargetStructures;
        public EntityBool? hasWeaponLevels;

        public weaponDamageMultLevels? weaponDamageMult;
        public weaponCooldownMultLevels? weaponCooldownMult;

        public EntityFloat? mass;
        public string? ShieldMeshName;
        public EntityBool? renderShield;
        public EntityFloat? maxAccelerationLinear;
        public EntityFloat? maxAccelerationStrafe;
        public EntityFloat? maxDecelerationLinear;
        public EntityFloat? maxAccelerationAngular;
        public EntityFloat? maxDecelerationAngular;
        public EntityFloat? maxSpeedLinear;
        public EntityFloat? maxRollRate;
        public EntityFloat? maxRollAngle;
        public EntityBool? useCustomHyperspaceEffects;
        public Dictionary<EntityInt, string>? squadTypeEntityDef;
        public Dictionary<EntityInt, EntityFloat>? squadAntiMatterCost;
        public EntityInt? maxNumCommandPoints;
        public List<NumSoundsFor>? NumSounds;
        public EntityInt? MeshNameInfoCount;
        public List<MeshNameInfo>? MeshNameInfo;

        public List<MeshNameInfoPrerequisites>? MeshNameInfoPrerequisites;
        public MeshNameInfoPrerequisites? prerequisites;

        public string? meshNameIncreasedEffectName;
        public string? meshNameDecreasedEffectName;
        public string? ExhaustParticleSystemName;
        public string? ExplosionName;
        public string? EngineSoundID;
        public List<string>? abilities;
        public EntityFloat? MaxAntiMatter;
        public EntityFloat? AntiMatterRestoreRate;
        public string? cargoType;
        public EntityFloat? maxCargoCapacity;
        public EntityInt? formationRank;
        public EntityFloat? minShadow;
        public EntityFloat? maxShadow;
        public EntityFloat? allegianceDecreasePerRoundtrip;


        private EffectsTypes effectsType = EffectsTypes.Unknown;
        private Prerequisites? currentPrerequisite;
        private WeaponEffects? currentEffects;
        private EffectSounds? currentSounds;
        private Levels? currentLevels;

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

                case "defaultAutoAttackRange":
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

                case "autoJoinFleetDefault":
                    autoJoinFleetDefault = new EntityBool(value);
                    return true;

                case "canBomb":
                    canBomb = new EntityBool(value);
                    return true;

                case "hasBombingLevels":
                    hasBombingLevels = new EntityBool(value); // Double check
                    return true;

                case "baseDamage":
                    if (value != "")
                        baseDamage = new EntityFloat(value);
                    else
                    {
                        baseDamage_L = new();
                        currentLevels = baseDamage_L;
                    }
                    return true;

                case "basePopulationKilled":
                    if (value != "")
                        basePopulationKilled = new EntityFloat(value);
                    else
                    {
                        basePopulationKilled_L = new();
                        currentLevels = basePopulationKilled_L;
                    }
                    return true;

                case "bombingFreqTime":
                    currentLevels = null;
                    bombingFreqTime = new EntityFloat(value);
                    return true;

                case "baseRange":
                    baseRange = new EntityFloat(value);
                    return true;

                case "bombTransitTime":
                    bombTransitTime = new EntityFloat(value);
                    return true;

                case "bombEffectCount":
                    bombEffectCount = new EntityInt(value);
                    return true;

                case "bombEffectAngleVariance":
                    bombEffectAngleVariance = new EntityFloat(value);
                    return true;

                case "bombEffectsDef":
                    effectsType = EffectsTypes.bombEffectsDef;
                    return true;

                case "weaponType":
                    if (effectsType == EffectsTypes.bombEffectsDef)
                    {
                        switch (value)
                        {
                            case "Projectile":
                                bombEffectsDef = new();
                                currentEffects = bombEffectsDef;
                                return true;
                            case "Beam":
                                bombEffectsDef_B = new();
                                currentEffects = bombEffectsDef_B;
                                return true;
                            case "Missile":
                                bombEffectsDef_M = new();
                                currentEffects = bombEffectsDef_M;
                                return true;
                        }
                    }
                    else if (effectsType == EffectsTypes.WeaponEffects)
                    {
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
                    }
                    else
                    {
                        Debug.WriteLine($"EffectType Unknown.");
                        return false;
                    }

                    Debug.WriteLine($"EffectType not set.");
                    return false;

                case "WeaponEffects":
                    effectsType = EffectsTypes.WeaponEffects;
                    return true;

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

                case "typeCount":
                    currentEffects = null;
                    currentSounds = null;
                    typeCount = new EntityInt(value);
                    return true;

                case "frigateRoleType":
                    frigateRoleType = value;
                    return true;

                case "statCountType":
                    statCountType = value;
                    return true;

                case "mainViewIcon":
                    mainViewIcon = value;
                    return true;

                case "picture":
                    picture = value;
                    return true;

                case "NameStringID":
                    NameStringID = value;
                    return true;

                case "DescriptionStringID":
                    DescriptionStringID = value;
                    return true;

                case "counterDescriptionStringID":
                    counterDescriptionStringID = value;
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

                case "slotCount":
                    slotCount = new EntityFloat(value);
                    return true;

                case "BuildTime":
                    BuildTime = new EntityFloat(value);
                    return true;

                case "hasLevels":
                    hasLevels = new EntityBool(value);
                    return true;

                case "ExperiencePointsForDestroying":
                    if (value != "")
                        ExperiencePointsForDestroying = new EntityFloat(value);
                    else
                    {
                        ExperiencePointsForDestroying_L = new();
                        currentLevels = ExperiencePointsForDestroying_L;
                    }
                    return true;

                case "MaxHullPoints":
                    if (value != "")
                        MaxHullPoints = new EntityFloat(value);
                    else
                    {
                        MaxHullPoints_L = new();
                        currentLevels = MaxHullPoints_L;
                    }
                    return true;

                case "MaxShieldPoints":
                    if (value != "")
                        MaxShieldPoints = new EntityFloat(value);
                    else
                    {
                        MaxShieldPoints_L = new();
                        currentLevels = MaxShieldPoints_L;
                    }
                    return true;

                case "HullPointRestoreRate":
                    if (value != "")
                        HullPointRestoreRate = new EntityFloat(value);
                    else
                    {
                        HullPointRestoreRate_L = new();
                        currentLevels = HullPointRestoreRate_L;
                    }
                    return true;

                case "ShieldPointRestoreRate":
                    if (value != "")
                        ShieldPointRestoreRate = new EntityFloat(value);
                    else
                    {
                        ShieldPointRestoreRate_L = new();
                        currentLevels = ShieldPointRestoreRate_L;
                    }
                    return true;

                case "BaseArmorPoints":
                    if (value != "")
                        BaseArmorPoints = new EntityFloat(value);
                    else
                    {
                        BaseArmorPoints_L = new();
                        currentLevels = BaseArmorPoints_L;
                    }
                    return true;

                case "maxMitigation":
                    if (value != "")
                        maxMitigation = new EntityFloat(value);
                    else
                    {
                        maxMitigation_L = new();
                        currentLevels = maxMitigation_L;
                    }
                    return true;

                case "relationshipChangeRate":
                    if (value != "")
                        relationshipChangeRate = new EntityFloat(value);
                    else
                    {
                        relationshipChangeRate_L = new();
                        currentLevels = relationshipChangeRate_L;
                    }
                    return true;

                case "Level:0":
                    currentLevels!.Level0 = new EntityFloat(value);
                    return true;

                case "Level:1":
                    currentLevels!.Level1 = new EntityFloat(value);
                    return true;

                case "Level:2":
                    currentLevels!.Level2 = new EntityFloat(value);
                    return true;

                case "Level:3":
                    currentLevels!.Level3 = new EntityFloat(value);
                    return true;

                case "Prerequisites":
                    currentLevels = null;
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

                case "numRandomDebrisLarge":
                    currentPrerequisite = null;
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

                case "minZoomDistanceMult":
                    minZoomDistanceMult = new EntityFloat(value);
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

                case "m_weaponIndexForRange":
                    currentEffects = null;
                    currentSounds = null;

                    m_weaponIndexForRange = new EntityInt(value);
                    return true;

                case "firingAlignmentType":
                    firingAlignmentType = value;
                    return true;

                case "firingTiltAngle":
                    firingTiltAngle = new EntityFloat(value);
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

                case "hasWeaponLevels":
                    hasWeaponLevels = new EntityBool(value);
                    return true;

                case "weaponDamageMult":
                    weaponDamageMult = new();
                    currentLevels = weaponDamageMult;
                    return true;

                case "weaponCooldownMult":
                    weaponCooldownMult = new();
                    currentLevels = weaponCooldownMult;
                    return true;

                case "mass":
                    currentLevels = null;
                    mass = new EntityFloat(value);
                    return true;

                case "ShieldMeshName":
                    ShieldMeshName = value;
                    return true;

                case "renderShield":
                    renderShield = new EntityBool(value);
                    return true;

                case "maxAccelerationLinear":
                    maxAccelerationLinear = new EntityFloat(value);
                    return true;

                case "maxAccelerationStrafe":
                    maxAccelerationStrafe = new EntityFloat(value);
                    return true;

                case "maxDecelerationLinear":
                    maxDecelerationLinear = new EntityFloat(value);
                    return true;

                case "maxAccelerationAngular":
                    maxAccelerationAngular = new EntityFloat(value);
                    return true;

                case "maxDecelerationAngular":
                    maxDecelerationAngular = new EntityFloat(value);
                    return true;

                case "maxSpeedLinear":
                    maxSpeedLinear = new EntityFloat(value);
                    return true;

                case "maxRollRate":
                    maxRollRate = new EntityFloat(value);
                    return true;

                case "maxRollAngle":
                    maxRollAngle = new EntityFloat(value);
                    return true;

                case "useCustomHyperspaceEffects":
                    useCustomHyperspaceEffects = new EntityBool(value);
                    return true;

                case "squadTypeEntityDef:0":
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

                case "maxNumCommandPoints":
                    maxNumCommandPoints = new EntityInt(value);
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
                    if (value != "Research")
                        MeshNameInfo![^1].criteriaType = value;
                    else
                    {
                        MeshNameInfoPrerequisites = new();

                        while (MeshNameInfo!.Count > 0)
                        {
                            MeshNameInfoPrerequisites.Add((MeshNameInfoPrerequisites)MeshNameInfo[0]);

                            MeshNameInfo.RemoveAt(0);
                        }

                        MeshNameInfoPrerequisites[^1].criteriaType = value;
                        MeshNameInfo = null;
                    }
                    return true;

                case "prerequisites":
                    MeshNameInfoPrerequisites![^1].prerequisites = new();
                    currentPrerequisite = MeshNameInfoPrerequisites![^1].prerequisites;
                    return true;

                case "meshNameIncreasedEffectName":
                    currentPrerequisite = null;
                    meshNameIncreasedEffectName = value;
                    return true;

                case "meshNameDecreasedEffectName":
                    meshNameDecreasedEffectName = value;
                    return true;

                case "ExhaustParticleSystemName":
                    ExhaustParticleSystemName = value;
                    return true;

                case "ExplosionName":
                    ExplosionName = value;
                    return true;

                case "EngineSoundID":
                    EngineSoundID = value;
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

                case "cargoType":
                    cargoType = value;
                    return true;

                case "maxCargoCapacity":
                    maxCargoCapacity = new EntityFloat(value);
                    return true;

                case "formationRank":
                    formationRank = new EntityInt(value);
                    return true;

                case "minShadow":
                    minShadow = new EntityFloat(value);
                    return true;

                case "maxShadow":
                    maxShadow = new EntityFloat(value);
                    return true;

                case "allegianceDecreasePerRoundtrip":
                    allegianceDecreasePerRoundtrip = new EntityFloat(value);
                    return true;

                default:
                    return false;
            }
        }
    }
}
