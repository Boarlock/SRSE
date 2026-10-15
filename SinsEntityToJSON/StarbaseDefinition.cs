using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SinsEntityConverter
{
    public class StarbaseDefinition : Def
    {
        public string? Header = "TXT2";
        public EntityInt? SinsArchiveVersion;
        public string? entityType;
        public List<string>? abilities;
        public string? defaultAutoAttackRange;
        public EntityBool? defaultAutoAttackOn;
        public EntityBool? prefersToFocusFire;
        public EntityBool? usesFighterAttack;
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
        public Dictionary<BankDirection, EntityInt>? TargetCountPerBank;
        public EntityBool? canOnlyTargetStructures;
        public string? ShieldMeshName;
        public EntityBool? renderShield;
        public Dictionary<EntityInt, string>? squadTypeEntityDef;
        public Dictionary<EntityInt, EntityFloat>? squadAntiMatterCost;
        public string? mainViewIcon;
        public EntityFloat? experiencePointsForDestroying;
        public string? nameStringID;
        public string? picture;
        public string? statCountType;
        public string? creationSourceAbility;
        public EntityInt? maxUpgradeLevelCount;
        public EntityInt? UpgradeTypeCount;
        public List<string>? UpgradeType;
        public EntityInt? MeshNameInfoCount;
        public List<MeshNameInfoRequirements>? MeshNameInfo;
        public string? meshNameIncreasedEffectName;
        public string? meshNameDecreasedEffectName;
        public List<NumSoundsFor>? NumSounds;
        public string? ExplosionName;
        public EntityInt? formationRank;
        public EntityFloat? minShadow;
        public EntityFloat? maxShadow;
        public string? cargoShipType;
        public EntityInt? maxCargoShipCount;
        public EntityFloat? cargoShipRespawnTime;
        public EntityFloat? baseBuildTime;
        public string? buildEffectName;
        public EntityFloat? angularSpeed;
        public string? rotateSoundName;
        public string? rotateFacingType;
        public EntityBool? rotateConstantly;
        public EntityFloat? maxAccelerationLinear;
        public EntityFloat? maxAccelerationStrafe;
        public EntityFloat? maxDecelerationLinear;
        public EntityFloat? maxAccelerationAngular;
        public EntityFloat? maxDecelerationAngular;
        public EntityFloat? maxSpeedLinear;
        public EntityFloat? maxRollRate;
        public EntityFloat? maxRollAngle;
        public EntityBool? useCustomHyperspaceEffects;
        public EntityFloat? mass;


        private MeshNameInfoRequirements? currentMeshInfo;
        private LevelRequirement? currentRequirement;
        private WeaponEffects? currentEffects;
        private EffectSounds? currentSounds;

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

                case "ShieldMeshName":
                    ShieldMeshName = value;
                    return true;

                case "renderShield":
                    renderShield = new EntityBool(value);
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

                case "mainViewIcon":
                    mainViewIcon = value;
                    return true;

                case "experiencePointsForDestroying":
                    experiencePointsForDestroying = new EntityFloat(value);
                    return true;

                case "nameStringID":
                    nameStringID = value;
                    return true;

                case "picture":
                    picture = value;
                    return true;

                case "statCountType":
                    statCountType = value;
                    return true;

                case "creationSourceAbility":
                    creationSourceAbility = value;
                    return true;

                case "maxUpgradeLevelCount":
                    maxUpgradeLevelCount = new EntityInt(value);
                    return true;

                case "UpgradeTypeCount":
                    UpgradeTypeCount = new EntityInt(value);
                    return true;

                case "UpgradeType":
                    UpgradeType ??= new();
                    UpgradeType.Add(value);
                    return true;

                case "MeshNameInfoCount":
                    MeshNameInfoCount = new EntityInt(value);
                    return true;

                case "MeshNameInfo":
                    MeshNameInfo ??= new();
                    MeshNameInfo.Add(new MeshNameInfoRequirements());
                    currentMeshInfo = MeshNameInfo[^1];
                    return true;

                case "meshName":
                    currentMeshInfo!.meshName = value;
                    return true;

                case "criteriaType":
                    currentMeshInfo!.criteriaType = value;
                    return true;

                case "levelRequirements":
                    currentMeshInfo!.levelRequirements ??= new();
                    return true;

                case "levelRequirementCount":
                    currentMeshInfo!.levelRequirements!.levelRequirementCount = new EntityInt(value);
                    return true;

                case "levelRequirement":
                    currentMeshInfo!.levelRequirements!.levelRequirement ??= new();
                    currentMeshInfo!.levelRequirements!.levelRequirement.Add(new LevelRequirement());
                    currentRequirement = currentMeshInfo!.levelRequirements!.levelRequirement[^1];
                    return true;

                case "upgradePropertyType":
                    currentRequirement!.upgradePropertyType = value;
                    return true;

                case "upgradeLevel":
                    currentRequirement!.upgradeLevel = new EntityInt(value);
                    return true;

                case "meshNameIncreasedEffectName":
                    currentRequirement = null;
                    currentMeshInfo = null;
                    meshNameIncreasedEffectName = value;
                    return true;

                case "meshNameDecreasedEffectName":
                    meshNameDecreasedEffectName = value;
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

                case "ExplosionName":
                    ExplosionName = value;
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

                case "cargoShipType":
                    cargoShipType = value;
                    return true;

                case "maxCargoShipCount":
                    maxCargoShipCount = new EntityInt(value);
                    return true;

                case "cargoShipRespawnTime":
                    cargoShipRespawnTime = new EntityFloat(value);
                    return true;

                case "baseBuildTime":
                    baseBuildTime = new EntityFloat(value);
                    return true;

                case "buildEffectName":
                    buildEffectName = value;
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

                case "mass":
                    mass = new EntityFloat(value);
                    return true;

                default:
                    return false;

            }
        }
    }

    public class Requirements
    {
        public EntityInt? levelRequirementCount;
        public List<LevelRequirement>? levelRequirement;
    }

    public class LevelRequirement
    {
        public string? upgradePropertyType;
        public EntityInt? upgradeLevel;
    }
}
