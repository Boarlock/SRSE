
using System.Diagnostics;

namespace SinsEntityConverter
{
    public class FighterDefinition : Def
    {
        public string? Header = "TXT2";
        public EntityInt? SinsArchiveVersion;
        public string? entityType;
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
        public string? nameStringID;
        public string? counterDescriptionStringID;
        public string? statCountType;
        public EntityFloat? formationOffsetDistance;
        public EntityFloat? mass;
        public EntityFloat? maxAccelerationLinear;
        public EntityFloat? maxAccelerationStrafe;
        public EntityFloat? maxDecelerationLinear;
        public EntityFloat? maxAccelerationAngular;
        public EntityFloat? maxDecelerationAngular;
        public EntityFloat? maxSpeedLinear;
        public EntityFloat? maxRollRate;
        public EntityFloat? maxRollAngle;
        public EntityInt? NumWeapons;
        public List<Weapon>? weapons;
        public EntityInt? m_weaponIndexForRange;
        public string? firingAlignmentType;
        public Dictionary<BankDirection, EntityInt>? TargetCountPerBank;
        public EntityBool? canOnlyTargetStructures;
        public string? meshName;
        public string? exhaustParticleSystemName;
        public string? ExplosionName;
        public string? exhaustTrailTextureName;
        public EntityFloat? exhaustTrailWidth;
        public string? engineSoundID;
        public EntityFloat? maxHullPoints;
        public EntityFloat? hullPointRestoreRate;
        public EntityFloat? armorPoints;
        public EntityFloat? maxMitigation;
        public EntityFloat? experiencePointsForDestroying;
        public EntityFloat? minShadow;
        public EntityFloat? maxShadow;

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

                case "nameStringID":
                    nameStringID = value;
                    return true;

                case "counterDescriptionStringID":
                    counterDescriptionStringID = value;
                    return true;

                case "statCountType":
                    statCountType = value;
                    return true;

                case "formationOffsetDistance":
                    formationOffsetDistance = new EntityFloat(value);
                    return true;

                case "mass":
                    mass = new EntityFloat(value);
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

                case "meshName":
                    meshName = value;
                    return true;

                case "exhaustParticleSystemName":
                    exhaustParticleSystemName = value;
                    return true;

                case "ExplosionName":
                    ExplosionName = value;
                    return true;

                case "exhaustTrailTextureName":
                    exhaustTrailTextureName = value;
                    return true;

                case "exhaustTrailWidth":
                    exhaustTrailWidth = new EntityFloat(value);
                    return true;

                case "engineSoundID":
                    engineSoundID = value;
                    return true;

                case "maxHullPoints":
                    maxHullPoints = new EntityFloat(value);
                    return true;

                case "hullPointRestoreRate":
                    hullPointRestoreRate = new EntityFloat(value);
                    return true;

                case "armorPoints":
                    armorPoints = new EntityFloat(value);
                    return true;

                case "maxMitigation":
                    maxMitigation = new EntityFloat(value);
                    return true;

                case "experiencePointsForDestroying":
                    experiencePointsForDestroying = new EntityFloat(value);
                    return true;

                case "minShadow":
                    minShadow = new EntityFloat(value);
                    return true;

                case "maxShadow":
                    maxShadow = new EntityFloat(value);
                    return true;

                default:
                    return false;
            }
        }
    }
}
