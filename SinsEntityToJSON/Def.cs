using System.Globalization;
using System.Text.Json.Serialization;

namespace SinsEntityConverter
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(SquadDefinition), "Squad")]
    [JsonDerivedType(typeof(FighterDefinition), "Fighter")]
    [JsonDerivedType(typeof(FrigateDefinition), "Frigate")]
    [JsonDerivedType(typeof(SpaceMineDefinition), "SpaceMine")]
    [JsonDerivedType(typeof(StarbaseDefinition), "Starbase")]
    [JsonDerivedType(typeof(CannonShellDefinition), "CannonShell")]
    [JsonDerivedType(typeof(CapitalShipDefinition), "CapitalShip")]
    [JsonDerivedType(typeof(TitanDefinition), "Titan")]
    [JsonDerivedType(typeof(PlanetModuleStandardDefinition), "PlanetModuleStandard")]
    [JsonDerivedType(typeof(PlanetModuleHangarDefenseDefinition), "PlanetModuleHangarDefense")]
    [JsonDerivedType(typeof(PlanetModuleMissileDefenseDefinition), "PlanetModuleMissileDefense")]
    [JsonDerivedType(typeof(PlanetModuleCargoDefinition), "PlanetModuleCargo")]
    [JsonDerivedType(typeof(PlanetDefinition), "Planet")]
    [JsonDerivedType(typeof(StarDefinition), "Star")]
    public abstract class Def 
    {
        public abstract bool Apply(string key, string value);
    }

    public class BasePrice
    {
        [JsonPropertyOrder(0)]
        public EntityFloat? Credits;
        [JsonPropertyOrder(1)]
        public EntityFloat? Metal;
        [JsonPropertyOrder(2)]
        public EntityFloat? Crystal;
    }

    public class Research
    {
        [JsonPropertyOrder(0)]
        public string? Subject;
        [JsonPropertyOrder(1)]
        public EntityInt? Level;
    }

    public class NumSoundsFor
    {
        [JsonPropertyOrder(0)]
        public Dictionary<string, EntityInt>? For;
        [JsonPropertyOrder(1)]
        public List<string>? SoundID;

    }

    public struct EntityFloat
    {
        [JsonPropertyOrder(0)]
        public float? Value;
        [JsonPropertyOrder(1)]
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
        [JsonPropertyOrder(0)]
        public int? Value;
        [JsonPropertyOrder(1)]
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
        [JsonPropertyOrder(0)]
        public bool? Value;
        [JsonPropertyOrder(1)]
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
        [JsonPropertyOrder(0)]
        public EntityInt? NumResearchPrerequisites;
        [JsonPropertyOrder(1)]
        public List<Research>? ResearchPrerequisite;
        [JsonPropertyOrder(2)]
        public string? RequiredFactionNameID;
        [JsonPropertyOrder(3)]
        public EntityInt? RequiredCompletedResearchSubjects;
    }

    public class EffectSounds
    {
        [JsonPropertyOrder(0)]
        public EntityInt? soundCount;
        [JsonPropertyOrder(1)]
        public List<string> sounds = new();
    }

    public class SoundsFor
    {
        [JsonPropertyOrder(0)]
        public EntityInt? SoundCount;
        [JsonPropertyOrder(1)]
        public List<string> SoundIDs = new();

    }

    [JsonPolymorphic]
    [JsonDerivedType(typeof(ExperiencePointsForDestroyingLevels), "ExperiencePointsForDestroying")]
    [JsonDerivedType(typeof(MaxHullPointsLevels), "MaxHullPoints")]
    [JsonDerivedType(typeof(ExperiencePointsForDestroyingLevels), "ExperiencePointsForDestroying")]
    [JsonDerivedType(typeof(MaxHullPointsLevels), "MaxHullPoints")]
    [JsonDerivedType(typeof(MaxShieldPointsLevels), "MaxShieldPoints")]
    [JsonDerivedType(typeof(HullPointRestoreRateLevels), "HullPointRestoreRate")]
    [JsonDerivedType(typeof(ShieldPointRestoreRateLevels), "ShieldPointRestoreRate")]
    [JsonDerivedType(typeof(BaseArmorPointsLevels), "BaseArmorPoints")]
    [JsonDerivedType(typeof(maxMitigationLevels), "maxMitigation")]
    [JsonDerivedType(typeof(relationshipChangeRateLevels), "relationshipChangeRate")]
    [JsonDerivedType(typeof(weaponDamageMultLevels), "weaponDamageMult")]
    [JsonDerivedType(typeof(weaponCooldownMultLevels), "weaponCooldownMult")]
    public abstract class Levels
    {
        [JsonPropertyOrder(0)]
        public EntityFloat? Level0;
        [JsonPropertyOrder(1)]
        public EntityFloat? Level1;
        [JsonPropertyOrder(2)]
        public EntityFloat? Level2;
        [JsonPropertyOrder(3)]
        public EntityFloat? Level3;
    }

    [JsonPolymorphic]
    [JsonDerivedType(typeof(baseDamageLevels), "baseDamage")]
    [JsonDerivedType(typeof(basePopulationKilledLevels), "basePopulationKilled")]
    [JsonDerivedType(typeof(HullPointRestoreRateUpgradeable), "HullPointRestoreRate")]
    [JsonDerivedType(typeof(ShieldPointRestoreRateUpgradeable), "ShieldPointRestoreRate")]
    [JsonDerivedType(typeof(ArmorPointsFromExperienceUpgradeable), "ArmorPointsFromExperience")]
    [JsonDerivedType(typeof(maxMitigationUpgradeable), "maxMitigation")]
    [JsonDerivedType(typeof(MaxAntiMatterUpgradeable), "MaxAntiMatter")]
    [JsonDerivedType(typeof(AntiMatterRestoreRateUpgradeable), "AntiMatterRestoreRate")]
    [JsonDerivedType(typeof(CultureProtectRateUpgradeable), "CultureProtectRate")]
    [JsonDerivedType(typeof(CommandPointsUpgradeable), "CommandPoints")]
    [JsonDerivedType(typeof(weaponCooldownDecreasePercUpgradeable), "weaponCooldownDecreasePerc")]
    [JsonDerivedType(typeof(weaponDamageIncreasePercUpgradeable), "weaponDamageIncreasePerc")]
    public class Upgradeable
    {
        [JsonPropertyOrder(0)]
        public EntityFloat? StartValue;
        [JsonPropertyOrder(1)]
        public EntityFloat? ValueIncreasePerLevel;
    }

    public class MaxHullPointsUpgradeable : Upgradeable { }
    public class MaxShieldPointsUpgradeable : Upgradeable { }
    public class HullPointRestoreRateUpgradeable : Upgradeable { }
    public class ShieldPointRestoreRateUpgradeable : Upgradeable { }
    public class ArmorPointsFromExperienceUpgradeable : Upgradeable { }
    public class maxMitigationUpgradeable : Upgradeable { }
    public class MaxAntiMatterUpgradeable : Upgradeable { }
    public class AntiMatterRestoreRateUpgradeable : Upgradeable { }
    public class CultureProtectRateUpgradeable : Upgradeable { }
    public class CommandPointsUpgradeable : Upgradeable { }
    public class weaponCooldownDecreasePercUpgradeable : Upgradeable { }
    public class weaponDamageIncreasePercUpgradeable : Upgradeable { }


    public class baseDamageLevels : Levels { }
    public class basePopulationKilledLevels : Levels { }
    public class ExperiencePointsForDestroyingLevels : Levels { }
    public class MaxHullPointsLevels : Levels { }
    public class MaxShieldPointsLevels : Levels { }
    public class HullPointRestoreRateLevels : Levels { }
    public class ShieldPointRestoreRateLevels : Levels { }
    public class BaseArmorPointsLevels : Levels { }
    public class maxMitigationLevels : Levels { }
    public class relationshipChangeRateLevels : Levels { }
    public class weaponDamageMultLevels : Levels { }
    public class weaponCooldownMultLevels : Levels { }

    public class DamageEnums
    {
        [JsonPropertyOrder(0)]
        public string? AttackType;
        [JsonPropertyOrder(1)]
        public string? DamageAffectType;
        [JsonPropertyOrder(2)]
        public string? DamageApplyType;
        [JsonPropertyOrder(3)]
        public string? DamageType;
        [JsonPropertyOrder(4)]
        public string? WeaponClassType;
    }

    public enum BankDirection
    {
        Front,
        Back,
        Left,
        Right
    }

    [JsonPolymorphic]
    [JsonDerivedType(typeof(WeaponEffects), "Projectile")]
    [JsonDerivedType(typeof(WeaponEffectsBeam), "Beam")]
    [JsonDerivedType(typeof(WeaponEffectsMissile), "Missile")]
    public class WeaponEffects
    {
        [JsonPropertyOrder(0)]
        public string? weaponType;
        [JsonPropertyOrder(1)]
        public EntityInt? burstCount;
        [JsonPropertyOrder(2)]
        public EntityFloat? burstDelay;
        [JsonPropertyOrder(3)]
        public EntityFloat? fireDelay;
        [JsonPropertyOrder(4)]
        public string? muzzleEffectName;
        [JsonPropertyOrder(5)]
        public EntityFloat? muzzleSoundMinRespawnTime;
        [JsonPropertyOrder(6)]
        public EffectSounds? muzzleSounds;
        [JsonPropertyOrder(7)]
        public string? hitEffectName;
        [JsonPropertyOrder(8)]
        public EffectSounds? hitHullEffectSounds;
        [JsonPropertyOrder(9)]
        public EffectSounds? hitShieldsEffectSounds;
        [JsonPropertyOrder(10)]
        public string? projectileTravelEffectName;
    }

    public class WeaponEffectsBeam : WeaponEffects
    {
        [JsonPropertyOrder(11)]
        public EffectSounds? beamEffectSounds;
        [JsonPropertyOrder(12)]
        public string? beamGlowTextureName;
        [JsonPropertyOrder(13)]
        public string? beamCoreTextureName;
        [JsonPropertyOrder(14)]
        public EntityFloat? beamWidth;
        [JsonPropertyOrder(15)]
        public string? beamGlowColor;
        [JsonPropertyOrder(16)]
        public string? beamCoreColor;
        [JsonPropertyOrder(17)]
        public EntityFloat? beamTilingRate;
    }

    public class WeaponEffectsMissile : WeaponEffects
    {
        [JsonPropertyOrder(18)]
        public string? missileTravelEffectName;
        [JsonPropertyOrder(19)]
        public EntityFloat? missileStartTurningDistance;
        [JsonPropertyOrder(20)]
        public EntityFloat? missileSlowTurnRate;
        [JsonPropertyOrder(21)]
        public EntityFloat? missileMaxSlowTurnTime;

    }

    [JsonPolymorphic]
    [JsonDerivedType(typeof(WeaponResearch), "Weapon")]
    public class Weapon
    {
        [JsonPropertyOrder(0)]
        public string? WeaponType;
        [JsonPropertyOrder(1)]
        public DamageEnums? damageEnums;
        [JsonPropertyOrder(2)]
        public Dictionary<BankDirection, EntityFloat>? DamagePerBank;
        [JsonPropertyOrder(3)]
        public EntityFloat? Range;
        [JsonPropertyOrder(4)]
        public EntityFloat? PreBuffCooldownTime;
        [JsonPropertyOrder(5)]
        public EntityBool? CanFireAtFighter;
        [JsonPropertyOrder(6)]
        public EntityBool? SynchronizedTargeting;
        [JsonPropertyOrder(7)]
        public EntityFloat? PointStaggerDelay;
        [JsonPropertyOrder(8)]
        public EntityFloat? TravelSpeed;
        [JsonPropertyOrder(9)]
        public EntityFloat? Duration;
        [JsonPropertyOrder(10)]
        public string? fireConstraintType;

        [JsonPropertyOrder(12)]
        public WeaponEffects? WeaponEffects;
        [JsonPropertyOrder(13)]
        public WeaponEffectsBeam? WeaponEffects_B;
        [JsonPropertyOrder(14)]
        public WeaponEffectsMissile? WeaponEffects_M;
    }

    public class WeaponResearch : Weapon
    {
        [JsonPropertyOrder(11)]
        public Prerequisites? researchPrerequisites;
    }

    [JsonPolymorphic]
    [JsonDerivedType(typeof(MeshNameInfoPrerequisites), "Prerequisites")]
    [JsonDerivedType(typeof(MeshNameInfoRequirements), "Requirements")]
    public class MeshNameInfo
    {
        [JsonPropertyOrder(0)]
        public string? meshName;
        [JsonPropertyOrder(1)]
        public string? criteriaType;
    }

    public class MeshNameInfoPrerequisites : MeshNameInfo
    {
        [JsonPropertyOrder(3)]
        public Prerequisites? prerequisites;
    }

    public class MeshNameInfoRequirements : MeshNameInfo
    {
        [JsonPropertyOrder(3)]
        public Requirements? levelRequirements;
    }

    public class Effects
    {
        public string? chargeUpEffectName;
        public string? chargeUpEffectNameBetweenStars;
        public string? chargeUpEffectNameDestabilized;
        public string? chargeUpEffectSoundID;
        public string? travelEffectName;
        public string? travelEffectNameBetweenStars;
        public string? travelEffectNameDestabilized;
        public string? travelEffectSoundID;
        public string? exitEffectName;
        public string? exitEffectSoundID;
    }

    public enum EffectsTypes
    {
        bombEffectsDef,
        WeaponEffects,
        Unknown
    }
}
