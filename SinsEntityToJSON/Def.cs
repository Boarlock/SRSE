using System.Globalization;

namespace SinsEntityConverter
{
    public enum EntityType
    {
        Fighter,
        Frigate,
        Capship,
        PlanetModule,
        Mine,
        Planet,
        Unknown
    }

    public class Def 
    {
        public EntityInt? SinsArchiveVersion;
        public string? nameStringID;
        public string? counterDescriptionStringID;
        public string? entityType;
        public List<string>? abilities;
        public string? defaultAutoAttackRange;
        public EntityBool? defaultAutoAttackOn;
        public EntityBool? prefersToFocusFire;
        public EntityBool? usesFighterAttack;
        public EntityBool? autoJoinFleetDefault;
        public EntityBool? canBomb;

        public EntityBool? hasBombingLevels;
        public EntityFloat? baseDamage;
        public EntityFloat? basePopulationKilled;
        public EntityFloat? bombingFreqTime;
        public EntityFloat? baseRange;
        public EntityFloat? bombTransitTime;
        public EntityInt? bombEffectCount;
        public EntityFloat? bombEffectAngleVariance;
        public WeaponEffects? bombEffectsDef;

        public string? mainViewIcon;
        public BasePrice? basePrice;
        public EntityFloat? slotCount;
        public EntityFloat? BuildTime;

        public EntityFloat? MaxHullPoints;
        public MaxHullPointsLevels? MaxHullLevels;
        public EntityFloat? MaxShieldPoints;
        public MaxShieldPointsLevels? MaxShieldLevels;
        public EntityFloat? HullPointRestoreRate;
        public HullPointRestoreRateLevels? HullRestoreLevels;
        public EntityFloat? ShieldPointRestoreRate;
        public ShieldPointRestoreRateLevels? ShieldRestoreLevels;
        public EntityFloat? maxMitigation;
        public maxMitigationLevels? MitigationLevels;
        public EntityFloat? MaxAntiMatter;
        public EntityFloat? AntiMatterRestoreRate;
        public EntityFloat? maxShieldMitigation;

        public EntityFloat? maxHullPoints;
        public EntityFloat? hullPointRestoreRate;
        public EntityFloat? armorPoints;
        public EntityFloat? experiencePointsForDestroying;

        public string? NameStringID;
        public string? DescriptionStringID;
        public string? picture;
        public string? statCountType;

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
        public Dictionary<BankDirection, EntityInt>? TargetCountPerBank;
        public EntityBool? canOnlyTargetStructures;
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
        public Dictionary<string, SoundsFor>? SoundsForEvents;
        public EntityInt? MeshNameInfoCount;
        public List<MeshNameInfo>? MeshNames;
        public string? meshNameIncreasedEffectName;
        public string? meshNameDecreasedEffectName;
        public string? ExhaustParticleSystemName;
        public string? ExplosionName;

        public string? EngineSoundID;
        public EntityInt? formationRank;
        public EntityFloat? minShadow;
        public EntityFloat? maxShadow;

        public EntityFloat? BaseArmorPoints;
        public BaseArmorPointsLevels? ArmorLevels;
        public EntityFloat? ExperiencePointsForDestroying;
        public ExperiencePointsForDestroyingLevels? ExpLevels;
    }

    public class BasePrice
    {
        public EntityFloat? Credits;
        public EntityFloat? Metal;
        public EntityFloat? Crystal;
    }

    public class Research
    {
        public string? Subject;
        public EntityInt? Level;
    }

    public struct EntityFloat
    {
        public float? Value;
        public bool? IsEmpty;

        public EntityFloat(string value)
        {
            IsEmpty = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                Value = null;
                IsEmpty = true;
            }
            else
                Value = float.Parse(value, CultureInfo.InvariantCulture);
        }
    }

    public struct EntityInt
    {
        public int? Value;
        public bool? IsEmpty;

        public EntityInt(string value)
        {
            IsEmpty = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                Value = null;
                IsEmpty = true;
            }
            else
                Value = int.Parse(value, CultureInfo.InvariantCulture);
        }

        public EntityInt(int value)
        {
            Value = value;
            IsEmpty = null;
        }
    }

    public struct EntityBool
    {
        public bool? Value;
        public bool? IsEmpty;

        public EntityBool(string value)
        {
            IsEmpty = null;

            if (string.IsNullOrWhiteSpace(value))
            {
                Value = null;
                IsEmpty = true;
            }
            else
                Value = bool.Parse(value);
        }
    }

    public class Prerequisites
    {
        public EntityInt? NumResearchPrerequisites;
        public List<Research>? ResearchPrerequisite;
        public string? RequiredFactionNameID;
        public EntityInt? RequiredCompletedResearchSubjects;
    }

    public class DamageEnums
    {
        public string? AttackType;
        public string? DamageAffectType;
        public string? DamageApplyType;
        public string? DamageType;
        public string? WeaponClassType;
    }

    public enum BankDirection
    {
        Front,
        Back,
        Left,
        Right
    }

    public class EffectSounds
    {
        public EntityInt? soundCount;
        public List<string> sounds = new();
    }

    public class SoundsFor
    {
        public EntityInt? SoundCount;
        public List<string> SoundIDs = new();

    }

    public abstract class Levels
    {
        public EntityFloat? Level0;
        public EntityFloat? Level1;
        public EntityFloat? Level2;
        public EntityFloat? Level3;

        public EntityFloat? StartValue;
        public EntityFloat? ValueIncreasePerLevel;
    }

    public class ExperiencePointsForDestroyingLevels : Levels {  }
    public class MaxHullPointsLevels : Levels { }
    public class MaxShieldPointsLevels : Levels { }
    public class HullPointRestoreRateLevels : Levels { }
    public class ShieldPointRestoreRateLevels : Levels { }
    public class BaseArmorPointsLevels : Levels { }
    public class maxMitigationLevels : Levels { }
    public class relationshipChangeRateLevels : Levels { }

    public class weaponDamageMultLevels : Levels { }
    public class weaponCooldownMultLevels : Levels { }

    public class ArmorPointsFromExperience : Levels { }
    public class MaxAntiMatterLevels : Levels { }
    public class AntiMatterRestoreRateLevels : Levels { }
    public class CultureProtectRate : Levels { }
    public class CommandPoints : Levels { }
    public class weaponCooldownDecreasePerc : Levels { }
    public class weaponDamageIncreasePerc : Levels { }

    public class WeaponEffects
    {
        public string? weaponType;
        public EntityInt? burstCount;
        public EntityFloat? burstDelay;
        public EntityFloat? fireDelay;
        public string? muzzleEffectName;
        public EntityFloat? muzzleSoundMinRespawnTime;
        public string? hitEffectName;
        public EffectSounds? muzzleSounds;
        public EffectSounds? hitHullEffectSounds;
        public EffectSounds? hitShieldsEffectSounds;
        public EffectSounds? beamEffectSounds;
        public string? projectileTravelEffectName;

        public string? missileTravelEffectName; 
        public EntityFloat? missileStartTurningDistance; 
        public EntityFloat? missileSlowTurnRate; 
        public EntityFloat? missileMaxSlowTurnTime;

        public string? beamGlowTextureName;
        public string? beamCoreTextureName;
        public EntityFloat? beamWidth;
        public string? beamGlowColor;
        public string? beamCoreColor;
        public EntityFloat? beamTilingRate;
    }

    public class Weapon
    {
        public string? WeaponType;
        public DamageEnums? damageEnums;
        public Dictionary<BankDirection, EntityFloat>? DamagePerBank;
        public EntityFloat? Range;
        public EntityFloat? PreBuffCooldownTime;
        public EntityBool? CanFireAtFighter;
        public EntityBool? SynchronizedTargeting;
        public EntityFloat? PointStaggerDelay;
        public EntityFloat? TravelSpeed;
        public EntityFloat? Duration;
        public string? fireConstraintType;
        public WeaponEffects? WeaponEffects;
        public Prerequisites? researchPrerequisites;
    }

    public class MeshNameInfo
    {
        public string? meshName;
        public string? criteriaType;
        public Prerequisites? prerequisites;
    }
}
