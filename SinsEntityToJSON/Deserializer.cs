using System.Globalization;
using System.Numerics;

namespace SinsEntityConverter
{
    public class Deserializer
    {
        public static string? OutputDir;
        private readonly DataRepresentations data;
        private readonly Action<RenderCode, SinsFile?, string> render;

        public Deserializer(
            DataRepresentations data,
            Action<RenderCode, SinsFile?, string> render)
        {
            this.data = data;
            this.render = render;
        }

        public void Deserialize(string appDir)
        {
            OutputDir = Path.Combine(appDir, "Output JSON");

            if (!Directory.Exists(OutputDir))
                Directory.CreateDirectory(OutputDir);

            foreach (SinsFile file in data.loadedFiles!)
            {
                EntityType type = TypeFinder(file);

                if (type == EntityType.Planet)
                {
                    PlanetDeserialize(file);

                    continue;
                }

                Def? def = null;
                switch (type)
                {
                    case EntityType.Fighter:
                        def = new FighterDefinition();
                        break;
                    case EntityType.Frigate:
                        def = new FrigateDefinition();
                        break;
                    case EntityType.Capship:
                        def = new CapshipDefinition();
                        break;
                    case EntityType.PlanetModule:
                        def = new PlanetModuleDefinition();
                        break;
                    case EntityType.Mine:
                        def = new MineDefinition();
                        break;
                }

                if (def == null)
                {
                    file.OperationCode = OperationCode.InvalidExtension;
                    render(RenderCode.ConversionFailed, file, "");
                    data.curFiles!.Remove(file);
                    continue;
                }

                WeaponEffects? currentEffects = null;
                Prerequisites? currentPrerequisites = null;
                EffectSounds? currentSounds = null;
                SoundsFor? currentSoundsFor = null;
                BasePrice? currentPrice = null;
                Levels? currentLevels = null;

                int specificDebrisCount = -1;
                int researchCount = -1;
                int weaponCount = -1;
                int meshCount = -1;

                foreach (string line in File.ReadLines(file.Path!))
                {
                    string trimmed = line.Trim(); // Removes whitespace off both ends

                    if (string.IsNullOrEmpty(trimmed) ||
                        trimmed == "TXT2")
                        continue;

                    int separator = trimmed.IndexOf(' '); // Space in the middle

                    string key;
                    string value;

                    if (separator < 0)
                    {
                        key = trimmed;
                        value = "";
                    }
                    else
                    {
                        key = trimmed[..separator]; // Take chars up to, not including seperator
                        value = trimmed[(separator + 1)..].Trim(); // Get everything after the seperator trimming for safety
                    }

                    if (value.Contains("\""))
                        value = value.Trim('"');

                    try
                    {
                        switch (key)
                        {
                            case "SinsArchiveVersion":
                                def.SinsArchiveVersion = new EntityInt(value);
                                break;
                            case "entityType":
                                def.entityType = value;
                                break;
                            case "defaultAutoAttackRange":
                                def.defaultAutoAttackRange = value;
                                break;
                            case "defaultAutoAttackOn":
                                def.defaultAutoAttackOn = new EntityBool(value);
                                break;
                            case "prefersToFocusFire":
                                def.prefersToFocusFire = new EntityBool(value);
                                break;
                            case "usesFighterAttack":
                                def.usesFighterAttack = new EntityBool(value);
                                break;
                            case "autoJoinFleetDefault":
                                def.autoJoinFleetDefault = new EntityBool(value);
                                break;
                            case "canBomb":
                                def.canBomb = new EntityBool(value);
                                break;

                            case "hasBombingLevels":
                                def.hasBombingLevels = new EntityBool(value);
                                break;
                            case "baseDamage":
                                def.baseDamage = new EntityFloat(value);
                                break;
                            case "basePopulationKilled":
                                def.basePopulationKilled = new EntityFloat(value);
                                break;
                            case "bombingFreqTime":
                                def.bombingFreqTime = new EntityFloat(value);
                                break;
                            case "baseRange":
                                def.baseRange = new EntityFloat(value);
                                break;
                            case "bombTransitTime":
                                def.bombTransitTime = new EntityFloat(value);
                                break;
                            case "bombEffectCount":
                                def.bombEffectCount = new EntityInt(value);
                                break;
                            case "bombEffectAngleVariance":
                                def.bombEffectAngleVariance = new EntityFloat(value);
                                break;

                            case "bombEffectsDef":
                                def.bombEffectsDef = new();
                                currentEffects = def.bombEffectsDef;
                                break;
                            case "weaponType":
                                currentEffects!.weaponType = value;
                                break;
                            case "burstCount":
                                currentEffects!.burstCount = new EntityInt(value);
                                break;
                            case "burstDelay":
                                currentEffects!.burstDelay = new EntityFloat(value);
                                break;
                            case "fireDelay":
                                currentEffects!.fireDelay = new EntityFloat(value);
                                break;
                            case "muzzleEffectName":
                                currentEffects!.muzzleEffectName = value;
                                break;
                            case "muzzleSoundMinRespawnTime":
                                currentEffects!.muzzleSoundMinRespawnTime = new EntityFloat(value);
                                break;

                            case "muzzleSounds":
                                currentEffects!.muzzleSounds = new();
                                currentSounds = currentEffects!.muzzleSounds;
                                break;
                            case "hitHullEffectSounds":
                                currentEffects!.hitHullEffectSounds = new();
                                currentSounds = currentEffects!.hitHullEffectSounds;
                                break;
                            case "hitShieldsEffectSounds":
                                currentEffects!.hitShieldsEffectSounds = new();
                                currentSounds = currentEffects!.hitShieldsEffectSounds;
                                break;
                            case "beamEffectSounds":
                                currentEffects!.beamEffectSounds = new();
                                currentSounds = currentEffects!.beamEffectSounds;
                                break;
                            case "soundCount":
                                if (currentSounds != null)
                                    currentSounds.soundCount = new EntityInt(value);
                                break;
                            case "sound":
                                if (currentSounds != null)
                                    currentSounds.sounds.Add(value);
                                break;

                            case "typeCount":
                                currentEffects = null;
                                currentSounds = null;
                                ((FrigateDefinition)def).typeCount = new EntityInt(value);
                                break;
                            case "planetUpgradeSlotCount":
                                ((PlanetModuleDefinition)def).planetUpgradeSlotCount = new EntityFloat(value);
                                break;
                            case "frigateRoleType":
                                ((FrigateDefinition)def).frigateRoleType = value;
                                break;
                            case "planetModuleRoleType":
                                ((PlanetModuleDefinition)def).planetModuleRoleType = value;
                                break;
                            case "statCountType":
                                def.statCountType = value;
                                break;
                            case "planetUpgradeSlotType":
                                ((PlanetModuleDefinition)def).planetUpgradeSlotType = value;
                                break;

                            case "mainViewIcon":
                                def.mainViewIcon = value;
                                break;
                            case "picture":
                                def.picture = value;
                                break;
                            case "NameStringID":
                                currentLevels = null;
                                def.NameStringID = value;
                                break;
                            case "DescriptionStringID":
                                def.DescriptionStringID = value;
                                break;
                            case "nameStringID":
                                currentLevels = null;
                                def.nameStringID = value;
                                break;
                            case "counterDescriptionStringID":
                                def.counterDescriptionStringID = value;
                                break;

                            case "basePrice":
                                def.basePrice = new BasePrice();
                                currentPrice = def.basePrice;
                                break;
                            case "scuttleValue":
                                ((MineDefinition)def).scuttleValue = new BasePrice();
                                currentPrice = ((MineDefinition)def).scuttleValue;
                                break;

                            case "credits":
                                currentPrice!.Credits = new EntityFloat(value);
                                break;
                            case "metal":
                                currentPrice!.Metal = new EntityFloat(value);
                                break;
                            case "crystal":
                                currentPrice!.Crystal = new EntityFloat(value);
                                break;

                            case "baseScuttleTime":
                                currentPrice = null;
                                ((MineDefinition)def).baseScuttleTime = new EntityFloat(value);
                                break;
                            case "slotCount":
                                currentPrice = null;
                                def.slotCount = new EntityFloat(value);
                                break;
                            case "BuildTime":
                                def.BuildTime = new EntityFloat(value);
                                break;
                            case "hasLevels":
                                ((FrigateDefinition)def).hasLevels = new EntityBool(value);
                                break;
                            case "resourceExtractionType":
                                ((PlanetModuleDefinition)def).resourceExtractionType = value;
                                break;

                            case "ExperiencePointsForDestroying":
                                if (value != "")
                                {
                                    def.ExperiencePointsForDestroying = new EntityFloat(value);
                                }
                                else
                                {
                                    def.ExpLevels = new();
                                    currentLevels = def.ExpLevels;
                                }
                                break;
                            case "MaxHullPoints":
                                if (value != "")
                                {
                                    def.MaxHullPoints = new EntityFloat(value);
                                }
                                else
                                {
                                    def.MaxHullLevels = new();
                                    currentLevels = def.MaxHullLevels;
                                }
                                break;
                            case "MaxShieldPoints":
                                if (value != "")
                                {
                                    def.MaxShieldPoints = new EntityFloat(value);
                                }
                                else
                                {
                                    def.MaxShieldLevels = new();
                                    currentLevels = def.MaxShieldLevels;
                                }
                                break;
                            case "HullPointRestoreRate":
                                if (value != "")
                                {
                                    def.HullPointRestoreRate = new EntityFloat(value);
                                }
                                else
                                {
                                    def.HullRestoreLevels = new();
                                    currentLevels = def.HullRestoreLevels;
                                }
                                break;
                            case "ShieldPointRestoreRate":
                                if (value != "")
                                {
                                    def.ShieldPointRestoreRate = new EntityFloat(value);
                                }
                                else
                                {
                                    def.ShieldRestoreLevels = new();
                                    currentLevels = def.ShieldRestoreLevels;
                                }
                                break;
                            case "BaseArmorPoints":
                                if (value != "")
                                {
                                    def.BaseArmorPoints = new EntityFloat(value);
                                }
                                else
                                {
                                    def.ArmorLevels = new();
                                    currentLevels = def.ArmorLevels;
                                }
                                break;
                            case "ArmorPointsFromExperience":
                                ((CapshipDefinition)def).ArmorPointsFromExperience = new();
                                currentLevels = ((CapshipDefinition)def).ArmorPointsFromExperience;
                                break;
                            case "CultureProtectRate":
                                ((CapshipDefinition)def).CultureProtectRate = new();
                                currentLevels = ((CapshipDefinition)def).CultureProtectRate;
                                break;
                            case "maxShieldMitigation":
                                def.maxShieldMitigation = new EntityFloat(value);
                                break;
                            case "maxMitigation":
                                if (value != "")
                                {
                                    def.maxMitigation = new EntityFloat(value);
                                }
                                else
                                {
                                    def.MitigationLevels = new();
                                    currentLevels = def.MitigationLevels;
                                }
                                break;
                            case "MaxAntiMatter":
                                if (value != "")
                                {
                                    def.MaxAntiMatter = new EntityFloat(value);
                                }
                                else
                                {
                                    ((CapshipDefinition)def).MaxAntiMatterLevels = new();
                                    currentLevels = ((CapshipDefinition)def).MaxAntiMatterLevels;
                                }
                                break;
                            case "AntiMatterRestoreRate":
                                if (value != "")
                                {
                                    def.AntiMatterRestoreRate = new EntityFloat(value);
                                }
                                else
                                {
                                    ((CapshipDefinition)def).AntiMatterRestoreRateLevels = new();
                                    currentLevels = ((CapshipDefinition)def).AntiMatterRestoreRateLevels;
                                }
                                break;
                            case "relationshipChangeRate":
                                if (value != "")
                                {
                                    ((FrigateDefinition)def).relationshipChangeRate = new EntityFloat(value);
                                }
                                else
                                {
                                    ((FrigateDefinition)def).RelationshipLevels = new();
                                    currentLevels = ((FrigateDefinition)def).RelationshipLevels;
                                }
                                break;

                            case "Level:0":
                                if (currentLevels != null)
                                    currentLevels.Level0 = new EntityFloat(value);
                                break;

                            case "Level:1":
                                if (currentLevels != null)
                                    currentLevels.Level1 = new EntityFloat(value);
                                break;

                            case "Level:2":
                                if (currentLevels != null)
                                    currentLevels.Level2 = new EntityFloat(value);
                                break;

                            case "Level:3":
                                if (currentLevels != null)
                                    currentLevels.Level3 = new EntityFloat(value);
                                break;
                            case "StartValue":
                                if (currentLevels != null)
                                    currentLevels.StartValue = new EntityFloat(value);
                                break;
                            case "ValueIncreasePerLevel":
                                if (currentLevels != null)
                                    currentLevels.ValueIncreasePerLevel = new EntityFloat(value);
                                break;

                            case "Prerequisites":
                                researchCount = -1;
                                currentLevels = null;
                                def.Prerequisites = new();
                                currentPrerequisites = def.Prerequisites;
                                break;
                            case "NumResearchPrerequisites":
                                currentPrerequisites!.NumResearchPrerequisites = new EntityInt(value);
                                break;
                            case "ResearchPrerequisite":
                                if (currentPrerequisites!.ResearchPrerequisite == null)
                                    currentPrerequisites.ResearchPrerequisite = new();
                                currentPrerequisites.ResearchPrerequisite!.Add(new Research());
                                researchCount++;
                                break;
                            case "Subject":
                                currentPrerequisites!.ResearchPrerequisite![researchCount].Subject = value;
                                break;
                            case "Level":
                                currentPrerequisites!.ResearchPrerequisite![researchCount].Level = new EntityInt(value);
                                break;
                            case "RequiredFactionNameID":
                                currentPrerequisites!.RequiredFactionNameID = value;
                                break;
                            case "RequiredCompletedResearchSubjects":
                                currentPrerequisites!.RequiredCompletedResearchSubjects = new EntityInt(value);
                                break;

                            case "cultureSpreadRate":
                                ((PlanetModuleDefinition)def).cultureSpreadRate = new EntityFloat(value);
                                break;
                            case "isAffectedBySimilarModuleCostResearch":
                                ((PlanetModuleDefinition)def).isAffectedBySimilarModuleCostResearch = new EntityBool(value);
                                break;
                            case "placementRadius":
                                ((PlanetModuleDefinition)def).placementRadius = new EntityFloat(value);
                                break;
                            case "nearPlacementDistOffset":
                                ((PlanetModuleDefinition)def).nearPlacementDistOffset = new EntityFloat(value);
                                break;
                            case "farPlacementDistOffset":
                                ((PlanetModuleDefinition)def).farPlacementDistOffset = new EntityFloat(value);
                                break;
                            case "spawnCount":
                                ((PlanetModuleDefinition)def).spawnCount = new EntityInt(value);
                                break;
                            case "swapMineType":
                                ((PlanetModuleDefinition)def).swapMineType = value;
                                break;
                            case "angularSpeed":
                                ((PlanetModuleDefinition)def).angularSpeed = new EntityFloat(value);
                                break;
                            case "rotateSoundName":
                                ((PlanetModuleDefinition)def).rotateSoundName = value;
                                break;
                            case "rotateFacingType":
                                ((PlanetModuleDefinition)def).rotateFacingType = value;
                                break;
                            case "rotateConstantly":
                                ((PlanetModuleDefinition)def).rotateConstantly = new EntityBool(value);
                                break;
                            case "roleType":
                                ((CapshipDefinition)def).roleType = value;
                                break;
                            case "numRandomDebrisLarge":
                                currentPrerequisites = null;
                                def.numRandomDebrisLarge = new EntityInt(value);
                                break;
                            case "numRandomDebrisSmall":
                                def.numRandomDebrisSmall = new EntityInt(value);
                                break;
                            case "numSpecificDebris":
                                def.numSpecificDebris = new EntityInt(value);
                                break;
                            case "specificDebrisMeshName":
                                if (def.specificDebrisMeshName == null)
                                    def.specificDebrisMeshName = new();
                                def.specificDebrisMeshName.Add(value);
                                specificDebrisCount++;
                                break;

                            case "armorType":
                                def.armorType = value;
                                break;
                            case "hudIcon":
                                def.hudIcon = value;
                                break;
                            case "smallHudIcon":
                                def.smallHudIcon = value;
                                break;
                            case "infoCardIcon":
                                def.infoCardIcon = value;
                                break;
                            case "minZoomDistanceMult":
                                def.minZoomDistanceMult = new EntityFloat(value);
                                break;
                            case "NumWeapons":
                                def.NumWeapons = new EntityInt(value);
                                break;
                            case "Weapon":
                                if (def.weapons == null)
                                    def.weapons = new();
                                def.weapons.Add(new Weapon());
                                weaponCount++;
                                break;
                            case "WeaponType":
                                def.weapons![weaponCount].WeaponType = value;
                                break;
                            case "damageEnums":
                                def.weapons![weaponCount].damageEnums = new DamageEnums();
                                break;
                            case "AttackType":
                                def.weapons![weaponCount].damageEnums!.AttackType = value;
                                break;
                            case "DamageAffectType":
                                def.weapons![weaponCount].damageEnums!.DamageAffectType = value;
                                break;
                            case "DamageApplyType":
                                def.weapons![weaponCount].damageEnums!.DamageApplyType = value;
                                break;
                            case "DamageType":
                                def.weapons![weaponCount].damageEnums!.DamageType = value;
                                break;
                            case "WeaponClassType":
                                def.weapons![weaponCount].damageEnums!.WeaponClassType = value;
                                break;
                            case "DamagePerBank:FRONT":
                                if (def.weapons![weaponCount].DamagePerBank == null)
                                    def.weapons![weaponCount].DamagePerBank = new();
                                def.weapons![weaponCount].DamagePerBank!.Add(BankDirection.Front, new EntityFloat(value));
                                break;
                            case "DamagePerBank:BACK":
                                if (def.weapons![weaponCount].DamagePerBank == null)
                                    def.weapons![weaponCount].DamagePerBank = new();
                                def.weapons![weaponCount].DamagePerBank!.Add(BankDirection.Back, new EntityFloat(value));
                                break;
                            case "DamagePerBank:LEFT":
                                if (def.weapons![weaponCount].DamagePerBank == null)
                                    def.weapons![weaponCount].DamagePerBank = new();
                                def.weapons![weaponCount].DamagePerBank!.Add(BankDirection.Left, new EntityFloat(value));
                                break;
                            case "DamagePerBank:RIGHT":
                                if (def.weapons![weaponCount].DamagePerBank == null)
                                    def.weapons![weaponCount].DamagePerBank = new();
                                def.weapons![weaponCount].DamagePerBank!.Add(BankDirection.Right, new EntityFloat(value));
                                break;
                            case "Range":
                                def.weapons![weaponCount].Range = new EntityFloat(value);
                                break;
                            case "PreBuffCooldownTime":
                                def.weapons![weaponCount].PreBuffCooldownTime = new EntityFloat(value);
                                break;
                            case "CanFireAtFighter":
                                def.weapons![weaponCount].CanFireAtFighter = new EntityBool(value);
                                break;
                            case "SynchronizedTargeting":
                                def.weapons![weaponCount].SynchronizedTargeting = new EntityBool(value);
                                break;
                            case "PointStaggerDelay":
                                def.weapons![weaponCount].PointStaggerDelay = new EntityFloat(value);
                                break;
                            case "TravelSpeed":
                                def.weapons![weaponCount].TravelSpeed = new EntityFloat(value);
                                break;
                            case "Duration":
                                def.weapons![weaponCount].Duration = new EntityFloat(value);
                                break;
                            case "fireConstraintType":
                                def.weapons![weaponCount].fireConstraintType = value;
                                break;

                            case "researchPrerequisites":
                                researchCount = -1;
                                ((PlanetModuleDefinition)def).weapons![weaponCount].researchPrerequisites = new();
                                currentPrerequisites = ((PlanetModuleDefinition)def).weapons![weaponCount].researchPrerequisites;
                                break;

                            case "WeaponEffects":
                                def.weapons![weaponCount].WeaponEffects = new();
                                currentEffects = def.weapons![weaponCount].WeaponEffects;
                                break;
                            case "projectileTravelEffectName":
                                currentEffects!.projectileTravelEffectName = value;
                                break;
                            case "missileTravelEffectName":
                                currentEffects!.missileTravelEffectName = value;
                                break;
                            case "missileStartTurningDistance":
                                currentEffects!.missileStartTurningDistance = new EntityFloat(value);
                                break;
                            case "missileSlowTurnRate":
                                currentEffects!.missileSlowTurnRate = new EntityFloat(value);
                                break;
                            case "missileMaxSlowTurnTime":
                                currentEffects!.missileMaxSlowTurnTime = new EntityFloat(value);
                                break;
                            case "hitEffectName":
                                currentEffects!.hitEffectName = value;
                                break;
                            case "beamGlowTextureName":
                                currentEffects!.beamGlowTextureName = value;
                                break;
                            case "beamCoreTextureName":
                                currentEffects!.beamCoreTextureName = value;
                                break;
                            case "beamWidth":
                                currentEffects!.beamWidth = new EntityFloat(value);
                                break;
                            case "beamGlowColor":
                                currentEffects!.beamGlowColor = value;
                                break;
                            case "beamCoreColor":
                                currentEffects!.beamCoreColor = value;
                                break;
                            case "beamTilingRate":
                                currentEffects!.beamTilingRate = new EntityFloat(value);
                                break;

                            case "m_weaponIndexForRange":
                                currentEffects = null;
                                currentSounds = null;
                                def.m_weaponIndexForRange = new EntityInt(value);
                                break;
                            case "firingAlignmentType":
                                def.firingAlignmentType = value;
                                break;
                            case "firingTiltAngle":
                                ((FrigateDefinition)def).firingTiltAngle = new EntityFloat(value);
                                break;
                            case "TargetCountPerBank:FRONT":
                                if (def.TargetCountPerBank == null)
                                    def.TargetCountPerBank = new();
                                def.TargetCountPerBank!.Add(BankDirection.Front, new EntityInt(value));
                                break;
                            case "TargetCountPerBank:BACK":
                                if (def.TargetCountPerBank == null)
                                    def.TargetCountPerBank = new();
                                def.TargetCountPerBank!.Add(BankDirection.Back, new EntityInt(value));
                                break;
                            case "TargetCountPerBank:LEFT":
                                if (def.TargetCountPerBank == null)
                                    def.TargetCountPerBank = new();
                                def.TargetCountPerBank!.Add(BankDirection.Left, new EntityInt(value));
                                break;
                            case "TargetCountPerBank:RIGHT":
                                if (def.TargetCountPerBank == null)
                                    def.TargetCountPerBank = new();
                                def.TargetCountPerBank!.Add(BankDirection.Right, new EntityInt(value));
                                break;
                            case "canOnlyTargetStructures":
                                def.canOnlyTargetStructures = new EntityBool(value);
                                break;
                            case "hasWeaponLevels":
                                ((FrigateDefinition)def).hasWeaponLevels = new EntityBool(value);
                                break;

                            case "weaponDamageMult":
                                if (value == "")
                                {
                                    ((FrigateDefinition)def).weaponDamageMult = new();
                                    currentLevels = ((FrigateDefinition)def).weaponDamageMult;
                                }
                                break;
                            case "weaponCooldownMult":
                                if (value == "")
                                {
                                    ((FrigateDefinition)def).weaponCooldownMult = new();
                                    currentLevels = ((FrigateDefinition)def).weaponCooldownMult;
                                }
                                break;

                            case "mass":
                                currentLevels = null;
                                def.mass = new EntityFloat(value);
                                break;
                            case "formationOffsetDistance":
                                currentLevels = null;
                                ((FighterDefinition)def).formationOffsetDistance = new EntityFloat(value);
                                break;

                            case "ShieldMeshName":
                                def.ShieldMeshName = value;
                                break;
                            case "renderShield":
                                def.renderShield = new EntityBool(value);
                                break;
                            case "maxAccelerationLinear":
                                def.maxAccelerationLinear = new EntityFloat(value);
                                break;
                            case "maxAccelerationStrafe":
                                def.maxAccelerationStrafe = new EntityFloat(value);
                                break;
                            case "maxDecelerationLinear":
                                def.maxDecelerationLinear = new EntityFloat(value);
                                break;
                            case "maxAccelerationAngular":
                                def.maxAccelerationAngular = new EntityFloat(value);
                                break;
                            case "maxDecelerationAngular":
                                def.maxDecelerationAngular = new EntityFloat(value);
                                break;
                            case "maxSpeedLinear":
                                def.maxSpeedLinear = new EntityFloat(value);
                                break;
                            case "maxRollRate":
                                def.maxRollRate = new EntityFloat(value);
                                break;
                            case "maxRollAngle":
                                def.maxRollAngle = new EntityFloat(value);
                                break;
                            case "useCustomHyperspaceEffects":
                                def.useCustomHyperspaceEffects = new EntityBool(value);
                                break;

                            case "effects":
                                ((CapshipDefinition)def).effects = new();
                                break;
                            case "chargeUpEffectName":
                                ((CapshipDefinition)def).effects!.chargeUpEffectName = value;
                                break;
                            case "chargeUpEffectNameBetweenStars":
                                ((CapshipDefinition)def).effects!.chargeUpEffectNameBetweenStars = value;
                                break;
                            case "chargeUpEffectNameDestabilized":
                                ((CapshipDefinition)def).effects!.chargeUpEffectNameDestabilized = value;
                                break;
                            case "chargeUpEffectSoundID":
                                ((CapshipDefinition)def).effects!.chargeUpEffectSoundID = value;
                                break;
                            case "travelEffectName":
                                ((CapshipDefinition)def).effects!.travelEffectName = value;
                                break;
                            case "travelEffectNameBetweenStars":
                                ((CapshipDefinition)def).effects!.travelEffectNameBetweenStars = value;
                                break;
                            case "travelEffectNameDestabilized":
                                ((CapshipDefinition)def).effects!.travelEffectNameDestabilized = value;
                                break;
                            case "travelEffectSoundID":
                                ((CapshipDefinition)def).effects!.travelEffectSoundID = value;
                                break;
                            case "exitEffectName":
                                ((CapshipDefinition)def).effects!.exitEffectName = value;
                                break;
                            case "exitEffectSoundID":
                                ((CapshipDefinition)def).effects!.exitEffectSoundID = value;
                                break;

                            case "squadTypeEntityDef:0":
                                if (def.squadTypeEntityDef == null)
                                    def.squadTypeEntityDef = new();
                                def.squadTypeEntityDef!.Add(new EntityInt(0), value);
                                break;
                            case "squadTypeEntityDef:1":
                                if (def.squadTypeEntityDef == null)
                                    def.squadTypeEntityDef = new();
                                def.squadTypeEntityDef!.Add(new EntityInt(1), value);
                                break;
                            case "squadTypeEntityDef:2":
                                if (def.squadTypeEntityDef == null)
                                    def.squadTypeEntityDef = new();
                                def.squadTypeEntityDef!.Add(new EntityInt(2), value);
                                break;
                            case "squadTypeEntityDef:3":
                                if (def.squadTypeEntityDef == null)
                                    def.squadTypeEntityDef = new();
                                def.squadTypeEntityDef!.Add(new EntityInt(3), value);
                                break;
                            case "squadAntiMatterCost:0":
                                if (def.squadAntiMatterCost == null)
                                    def.squadAntiMatterCost = new();
                                def.squadAntiMatterCost!.Add(new EntityInt(0), new EntityFloat(value));
                                break;
                            case "squadAntiMatterCost:1":
                                if (def.squadAntiMatterCost == null)
                                    def.squadAntiMatterCost = new();
                                def.squadAntiMatterCost!.Add(new EntityInt(1), new EntityFloat(value));
                                break;
                            case "squadAntiMatterCost:2":
                                if (def.squadAntiMatterCost == null)
                                    def.squadAntiMatterCost = new();
                                def.squadAntiMatterCost!.Add(new EntityInt(2), new EntityFloat(value));
                                break;
                            case "squadAntiMatterCost:3":
                                if (def.squadAntiMatterCost == null)
                                    def.squadAntiMatterCost = new();
                                def.squadAntiMatterCost!.Add(new EntityInt(3), new EntityFloat(value));
                                break;
                            case "maxNumCommandPoints":
                                ((FrigateDefinition)def).maxNumCommandPoints = new EntityInt(value);
                                break;
                            case "NumSoundsFor:ONATTACKORDERISSUED":
                                if (def.SoundsForEvents == null)
                                    def.SoundsForEvents = new();
                                currentSoundsFor = new SoundsFor
                                {
                                    SoundCount = new EntityInt(value)
                                };
                                def.SoundsForEvents!.Add("ONATTACKORDERISSUED", currentSoundsFor);
                                break;
                            case "NumSoundsFor:ONCREATION":
                                if (def.SoundsForEvents == null)
                                    def.SoundsForEvents = new();
                                currentSoundsFor = new SoundsFor
                                {
                                    SoundCount = new EntityInt(value)
                                };
                                def.SoundsForEvents!.Add("ONCREATION", currentSoundsFor);
                                break;
                            case "NumSoundsFor:ONGENERALORDERISSUED":
                                if (def.SoundsForEvents == null)
                                    def.SoundsForEvents = new();
                                currentSoundsFor = new SoundsFor
                                {
                                    SoundCount = new EntityInt(value)
                                };
                                def.SoundsForEvents!.Add("ONGENERALORDERISSUED", currentSoundsFor);
                                break;
                            case "NumSoundsFor:ONSELECTED":
                                if (def.SoundsForEvents == null)
                                    def.SoundsForEvents = new();
                                currentSoundsFor = new SoundsFor
                                {
                                    SoundCount = new EntityInt(value)
                                };
                                def.SoundsForEvents!.Add("ONSELECTED", currentSoundsFor);
                                break;
                            case "NumSoundsFor:ONSTARTPHASEJUMP":
                                if (def.SoundsForEvents == null)
                                    def.SoundsForEvents = new();
                                currentSoundsFor = new SoundsFor
                                {
                                    SoundCount = new EntityInt(value)
                                };
                                def.SoundsForEvents!.Add("ONSTARTPHASEJUMP", currentSoundsFor);
                                break;

                            case "SoundID":
                                if (currentSoundsFor != null)
                                    currentSoundsFor!.SoundIDs.Add(value); // Reference value updates in Dictionary
                                break;

                            case "MeshNameInfoCount":
                                def.MeshNameInfoCount = new EntityInt(value);
                                break;
                            case "MeshNameInfo":
                                if (def.MeshNames == null)
                                    def.MeshNames = new();
                                def.MeshNames.Add(new MeshNameInfo());
                                meshCount++;
                                break;
                            case "meshName":
                                if (def.MeshNames != null)
                                    def.MeshNames![meshCount].meshName = value;
                                else
                                    ((FighterDefinition)def).meshName = value;
                                break;
                            case "criteriaType":
                                def.MeshNames![meshCount].criteriaType = value;
                                break;
                            case "prerequisites":
                                researchCount = -1;
                                def.MeshNames![meshCount].prerequisites = new();
                                currentPrerequisites = def.MeshNames![meshCount].prerequisites;
                                break;

                            case "exhaustParticleSystemName":
                                ((FighterDefinition)def).exhaustParticleSystemName = value;
                                break;
                            case "exhaustTrailTextureName":
                                ((FighterDefinition)def).exhaustTrailTextureName = value;
                                break;
                            case "exhaustTrailWidth":
                                ((FighterDefinition)def).exhaustTrailWidth = new EntityFloat(value);
                                break;
                            case "engineSoundID":
                                ((FighterDefinition)def).engineSoundID = value;
                                break;

                            case "meshNameIncreasedEffectName":
                                def.meshNameIncreasedEffectName = value;
                                break;
                            case "meshNameDecreasedEffectName":
                                def.meshNameDecreasedEffectName = value;
                                break;
                            case "ExhaustParticleSystemName":
                                def.ExhaustParticleSystemName = value;
                                break;
                            case "ExplosionName":
                                def.ExplosionName = value;
                                break;
                            case "MeshName":
                                ((MineDefinition)def).MeshName = value;
                                break;
                            case "spawnActivationDelay":
                                ((MineDefinition)def).spawnActivationDelay = new EntityFloat(value);
                                break;
                            case "targetStartFollowRange":
                                ((MineDefinition)def).targetStartFollowRange = new EntityFloat(value);
                                break;
                            case "targetMaxFollowRange":
                                ((MineDefinition)def).targetMaxFollowRange = new EntityFloat(value);
                                break;

                            case "CommandPoints":
                                ((CapshipDefinition)def).CommandPoints = new();
                                currentLevels = ((CapshipDefinition)def).CommandPoints;
                                break;
                            case "weaponCooldownDecreasePerc":
                                ((CapshipDefinition)def).weaponCooldownDecreasePerc = new();
                                currentLevels = ((CapshipDefinition)def).weaponCooldownDecreasePerc;
                                break;
                            case "weaponDamageIncreasePerc":
                                ((CapshipDefinition)def).weaponDamageIncreasePerc = new();
                                currentLevels = ((CapshipDefinition)def).weaponDamageIncreasePerc;
                                break;

                            case "EngineSoundID":
                                currentLevels = null;
                                def.EngineSoundID = value;
                                break;
                            case "ability:0":
                                if (def.abilities == null)
                                    def.abilities = new();
                                def.abilities.Add(value);
                                break;
                            case "ability:1":
                                if (def.abilities == null)
                                    def.abilities = new();
                                def.abilities.Add(value);
                                break;
                            case "ability:2":
                                if (def.abilities == null)
                                    def.abilities = new();
                                def.abilities.Add(value);
                                break;
                            case "ability:3":
                                if (def.abilities == null)
                                    def.abilities = new();
                                def.abilities.Add(value);
                                break;
                            case "ability:4":
                                if (def.abilities == null)
                                    def.abilities = new();
                                def.abilities.Add(value);
                                break;
                            case "cargoType":
                                ((FrigateDefinition)def).cargoType = value;
                                break;
                            case "maxCargoCapacity":
                                ((FrigateDefinition)def).maxCargoCapacity = new EntityFloat(value);
                                break;
                            case "formationRank":
                                def.formationRank = new EntityInt(value);
                                break;
                            case "minShadow":
                                def.minShadow = new EntityFloat(value);
                                break;
                            case "maxShadow":
                                def.maxShadow = new EntityFloat(value);
                                break;
                            case "allegianceDecreasePerRoundtrip":
                                ((FrigateDefinition)def).allegianceDecreasePerRoundtrip = new EntityFloat(value);
                                break;

                            case "maxHullPoints":
                                def.maxHullPoints = new EntityFloat(value);
                                break;
                            case "hullPointRestoreRate":
                                def.hullPointRestoreRate = new EntityFloat(value);
                                break;
                            case "maxShieldPoints":
                                ((MineDefinition)def).maxShieldPoints = new EntityFloat(value);
                                break;
                            case "shieldPointRestoreRate":
                                ((MineDefinition)def).shieldPointRestoreRate = new EntityFloat(value);
                                break;
                            case "armorPoints":
                                def.armorPoints = new EntityFloat(value);
                                break;
                            case "experiencePointsForDestroying":
                                def.experiencePointsForDestroying = new EntityFloat(value);
                                break;
                            case "baseMaxAntiMatter":
                                ((MineDefinition)def).baseMaxAntiMatter = new EntityFloat(value);
                                break;
                            case "baseAntiMatterRestoreRate":
                                ((MineDefinition)def).baseAntiMatterRestoreRate = new EntityFloat(value);
                                break;

                            case "baseBuildTime":
                                ((PlanetModuleDefinition)def).baseBuildTime = new EntityFloat(value);
                                break;
                            case "buildEffectName":
                                ((PlanetModuleDefinition)def).buildEffectName = value;
                                break;
                            case "baseCommandPoints":
                                ((PlanetModuleDefinition)def).baseCommandPoints = new EntityInt(value);
                                break;
                            case "cargoShipType":
                                ((PlanetModuleDefinition)def).cargoShipType = value;
                                break;
                            case "maxCargoShipCount":
                                ((PlanetModuleDefinition)def).maxCargoShipCount = new EntityInt(value);
                                break;
                            case "cargoShipRespawnTime":
                                ((PlanetModuleDefinition)def).cargoShipRespawnTime = new EntityFloat(value);
                                break;
                            case "baseTradeIncomeRate":
                                ((PlanetModuleDefinition)def).baseTradeIncomeRate = new EntityFloat(value);
                                break;
                            default:
                                Console.WriteLine($"No match found. Key: {key} | Value: {value}");
                                break;
                        }
                    }
                    catch (NullReferenceException)
                    {
                        file.OperationCode = OperationCode.ConversionFailed;
                        render(RenderCode.ConversionFailed, file, "");
                        data.curFiles!.Remove(file);
                        continue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        file.OperationCode = OperationCode.ConversionFailed;
                        render(RenderCode.ConversionFailed, file, "");
                        data.curFiles!.Remove(file);
                        continue;
                    }
                    catch (Exception)
                    {
                        file.OperationCode = OperationCode.ConversionFailed;
                        render(RenderCode.ConversionFailed, file, "");
                        data.curFiles!.Remove(file);
                        continue;
                    }
                }

                JSONSerializer.Serialize(def, file, render, data);
            }

            render(RenderCode.ConversionComplete, null, "");
        }

        public void PlanetDeserialize(SinsFile file)
        {
            PlanetDefinition def = new PlanetDefinition();
            BasePrice? currentPrice = null;
            PathType? currentPath = null;
            AsteroidSetup? currentAsteroid = null;

            int stageCount = -1;
            int skyboxCount = -1;

            foreach (string line in File.ReadLines(file.Path!))
            {
                string trimmed = line.Trim(); // Removes whitespace off both ends

                if (string.IsNullOrEmpty(trimmed) ||
                    trimmed == "TXT2")
                    continue;

                int separator = trimmed.IndexOf(' '); // Space in the middle

                string key;
                string value;

                if (separator < 0)
                {
                    key = trimmed;
                    value = "";
                }
                else
                {
                    key = trimmed[..separator]; // Take chars up to, not including seperator
                    value = trimmed[(separator + 1)..].Trim(); // Get everything after the seperator trimming for safety
                }

                if (value.Contains("\""))
                    value = value.Trim('"');

                try
                {
                    switch (key)
                    {
                        case "SinsArchiveVersion":
                            def.SinsArchiveVersion = new EntityInt(value);
                            break;
                        case "entityType":
                            def.entityType = value;
                            break;
                        case "meshInfoCount":
                            def.meshInfoCount = new EntityInt(value);
                            break;

                        case "meshInfo":
                            def.meshInfo = new();
                            break;
                        case "typeNameStringID":
                            def.meshInfo!.typeNameStringID = value;
                            break;
                        case "asteroidTemplate":
                            def.meshInfo!.asteroidTemplate = value;
                            break;
                        case "dustCloudTemplate":
                            def.meshInfo!.dustCloudTemplate = value;
                            break;
                        case "meshName":
                            def.meshInfo!.meshName = value;
                            break;
                        case "cloudColor":
                            def.meshInfo!.cloudColor = value;
                            break;
                        case "nullMeshRadius":
                            def.meshInfo!.nullMeshRadius = new EntityFloat(value);
                            break;
                        case "nullMeshParticleEffect":
                            def.meshInfo!.nullMeshParticleEffect = value;
                            break;
                        case "hudIcon":
                            def.meshInfo!.hudIcon = value;
                            break;
                        case "smallHudIcon":
                            def.meshInfo!.smallHudIcon = value;
                            break;
                        case "infoCardIcon":
                            def.meshInfo!.infoCardIcon = value;
                            break;
                        case "mainViewIcon":
                            def.meshInfo!.mainViewIcon = value;
                            break;
                        case "undetectedMainViewIcon":
                            def.meshInfo!.undetectedMainViewIcon = value;
                            break;
                        case "picture":
                            def.meshInfo!.picture = value;
                            break;
                        case "shouldPrefer":
                            def.meshInfo!.shouldPrefer = new EntityBool(value);
                            break;

                        case "minZoomDistanceMult":
                            def.minZoomDistanceMult = new EntityFloat(value);
                            break;
                        case "glowColor":
                            def.glowColor = value;
                            break;
                        case "ringColor":
                            def.ringColor = value;
                            break;
                        case "cloudLayerTextureName":
                            def.cloudLayerTextureName = value;
                            break;

                        case "planetResourceSetupInfo":
                            def.planetResourceSetupInfo = new();
                            break;
                        case "asteroidSpawnAngleVariance":
                            def.planetResourceSetupInfo!.asteroidSpawnAngleVariance = new EntityFloat(value);
                            break;
                        case "totalMaxResourceAsteroids":
                            def.planetResourceSetupInfo!.totalMaxResourceAsteroids = new EntityInt(value);
                            break;

                        case "metalResourceAsteroidSetup":
                            def.planetResourceSetupInfo!.metalResourceAsteroidSetup = new();
                            currentAsteroid = def.planetResourceSetupInfo!.metalResourceAsteroidSetup;
                            break;
                        case "crystalResourceAsteroidSetup":
                            def.planetResourceSetupInfo!.crystalResourceAsteroidSetup = new();
                            currentAsteroid = def.planetResourceSetupInfo!.crystalResourceAsteroidSetup;
                            break;
                        case "neutralMetalResourceAsteroidSetup":
                            def.planetResourceSetupInfo!.neutralMetalResourceAsteroidSetup = new();
                            currentAsteroid = def.planetResourceSetupInfo!.neutralMetalResourceAsteroidSetup;
                            break;
                        case "neutralCrystalResourceAsteroidSetup":
                            def.planetResourceSetupInfo!.neutralCrystalResourceAsteroidSetup = new();
                            currentAsteroid = def.planetResourceSetupInfo!.neutralCrystalResourceAsteroidSetup;
                            break;

                        case "minCount":
                            currentAsteroid!.minCount = new EntityInt(value);
                            break;
                        case "maxCount":
                            currentAsteroid!.maxCount = new EntityInt(value);
                            break;
                        case "extractionRate":
                            currentAsteroid!.extractionRate = new EntityFloat(value);
                            break;
                        case "refineryRate":
                            currentAsteroid!.refineryRate = new EntityFloat(value);
                            break;
                        case "maxRefineryCount":
                            currentAsteroid!.maxRefineryCount = new EntityInt(value);
                            break;

                        case "isAsteroid":
                            currentAsteroid = null;
                            def.isAsteroid = new EntityBool(value);
                            break;
                        case "healthRegenRate":
                            def.healthRegenRate = new EntityFloat(value);
                            break;

                        case "planetUpgradeDef":
                            def.planetUpgradeDef = new();
                            break;
                        case "path:Population":
                            stageCount = -1;
                            def.planetUpgradeDef!.populationPath = new();
                            currentPath = def.planetUpgradeDef!.populationPath;
                            break;
                        case "path:CivilianModules":
                            stageCount = -1;
                            def.planetUpgradeDef!.civilianPath = new();
                            currentPath = def.planetUpgradeDef!.civilianPath;
                            break;
                        case "path:TacticalModules":
                            stageCount = -1;
                            def.planetUpgradeDef!.tacticalPath = new();
                            currentPath = def.planetUpgradeDef!.tacticalPath;
                            break;
                        case "path:Home":
                            stageCount = -1;
                            def.planetUpgradeDef!.homePath = new();
                            currentPath = def.planetUpgradeDef!.homePath;
                            break;
                        case "path:ArtifactLevel":
                            stageCount = -1;
                            def.planetUpgradeDef!.artifactLevelPath = new();
                            currentPath = def.planetUpgradeDef!.artifactLevelPath;
                            break;
                        case "path:Infrastructure":
                            stageCount = -1;
                            def.planetUpgradeDef!.infrastructurePath = new();
                            currentPath = def.planetUpgradeDef!.infrastructurePath;
                            break;
                        case "path:Social":
                            stageCount = -1;
                            def.planetUpgradeDef!.socialPath = new();
                            currentPath = def.planetUpgradeDef!.socialPath;
                            break;
                        case "path:Industry":
                            stageCount = -1;
                            def.planetUpgradeDef!.industryPath = new();
                            currentPath = def.planetUpgradeDef!.industryPath;
                            break;
                        case "path:Smuggler":
                            stageCount = -1;
                            def.planetUpgradeDef!.smugglerPath = new();
                            currentPath = def.planetUpgradeDef!.smugglerPath;
                            break;

                        case "stageCount":
                            currentPath!.stageCount = new EntityInt(value);
                            break;

                        case "stage":
                            if (currentPath!.stages == null)
                                currentPath!.stages = new();
                            currentPath!.stages.Add(new Stage());
                            stageCount++;
                            break;

                        case "price":
                            currentPath!.stages![stageCount].price = new BasePrice();
                            currentPrice = currentPath!.stages![stageCount].price;
                            break;
                        case "credits":
                            currentPrice!.Credits = new EntityFloat(value);
                            break;
                        case "metal":
                            currentPrice!.Metal = new EntityFloat(value);
                            break;
                        case "crystal":
                            currentPrice!.Crystal = new EntityFloat(value);
                            break;
                        case "upgradeTime":
                            currentPath!.stages![stageCount].upgradeTime = new EntityFloat(value);
                            break;
                        case "maxPopulation":
                            currentPath!.stages![stageCount].maxPopulation = new EntityFloat(value);
                            break;
                        case "populationGrowthRate":
                            currentPath!.stages![stageCount].populationGrowthRate = new EntityFloat(value);
                            break;
                        case "developmentTaxPenalty":
                            currentPath!.stages![stageCount].developmentTaxPenalty = new EntityFloat(value);
                            break;
                        case "maxModuleSlotCount:Civilian":
                            if (currentPath!.stages![stageCount].maxModuleSlotCount == null)
                                currentPath!.stages![stageCount].maxModuleSlotCount = new();
                            currentPath!.stages![stageCount].maxModuleSlotCount!.Add("Civilian", new EntityFloat(value));
                            break;
                        case "maxModuleSlotCount:Tactical":
                            if (currentPath!.stages![stageCount].maxModuleSlotCount == null)
                                currentPath!.stages![stageCount].maxModuleSlotCount = new();
                            currentPath!.stages![stageCount].maxModuleSlotCount!.Add("Tactical", new EntityFloat(value));
                            break;
                        case "maxModuleConstructorCount":
                            currentPath!.stages![stageCount].maxModuleConstructorCount = new EntityInt(value);
                            break;
                        case "isHomePlanet":
                            currentPath!.stages![stageCount].isHomePlanet = new EntityBool(value);
                            break;
                        case "homePlanetTaxRateBonus":
                            currentPath!.stages![stageCount].homePlanetTaxRateBonus = new EntityFloat(value);
                            break;
                        case "homePlanetMetalIncomeBonus":
                            currentPath!.stages![stageCount].homePlanetMetalIncomeBonus = new EntityFloat(value);
                            break;
                        case "homePlanetCrystalIncomeBonus":
                            currentPath!.stages![stageCount].homePlanetCrystalIncomeBonus = new EntityFloat(value);
                            break;
                        case "maxHealth":
                            currentPath!.stages![stageCount].maxHealth = new EntityFloat(value);
                            break;
                        case "tradeIncomeModifier":
                            currentPath!.stages![stageCount].tradeIncomeModifier = new EntityFloat(value);
                            break;
                        case "shipBuildRateModifier":
                            currentPath!.stages![stageCount].shipBuildRateModifier = new EntityFloat(value);
                            break;
                        case "cultureSpreadRateModifier":
                            currentPath!.stages![stageCount].cultureSpreadRateModifier = new EntityFloat(value);
                            break;
                        case "corruptionPercent":
                            currentPath!.stages![stageCount].corruptionPercent = new EntityFloat(value);
                            break;
                        case "smugglingPercent":
                            currentPath!.stages![stageCount].smugglingPercent = new EntityFloat(value);
                            break;

                        case "ringsChance":
                            currentPath = null;
                            currentPrice = null;
                            def.ringsChance = new EntityFloat(value);
                            break;
                        case "isColonizable":
                            def.isColonizable = new EntityBool(value);
                            break;
                        case "planetTypeForResearch":
                            def.planetTypeForResearch = value;
                            break;
                        case "skyboxScalarsCount":
                            def.skyboxScalarsCount = new EntityInt(value);
                            break;

                        case "skyboxScalars":
                            if (def.skyboxScalars == null)
                                def.skyboxScalars = new();
                            def.skyboxScalars.Add(new skyboxScalar());
                            skyboxCount++;
                            break;
                        case "diffuseScalar":
                            def.skyboxScalars![skyboxCount].diffuseScalar = new EntityFloat(value);
                            break;
                        case "ambientScalar":
                            def.skyboxScalars![skyboxCount].ambientScalar = new EntityFloat(value);
                            break;

                        case "requiredPlanetBonusesCount":
                            def.requiredPlanetBonusesCount = new EntityInt(value);
                            break;
                        case "possibleRandomPlanetBonusesCount":
                            def.possibleRandomPlanetBonusesCount = new EntityInt(value);
                            break;
                        case "bonus":
                            def.bonus = value;
                            break;
                        case "ambienceSoundID":
                            def.ambienceSoundID = value;
                            break;
                        case "renderAsVolcanic":
                            def.renderAsVolcanic = new EntityBool(value);
                            break;

                        case "planetRuinDef":
                            def.planetRuinDef = new();
                            break;
                        case "ruinPlanetType":
                            def.planetRuinDef!.ruinPlanetType = value;
                            break;
                        case "attachType":
                            def.planetRuinDef!.attachType = value;
                            break;
                        case "effectName":
                            def.planetRuinDef!.effectName = value;
                            break;
                        case "startTime":
                            def.planetRuinDef!.startTime = new EntityFloat(value);
                            break;
                        case "soundID":
                            def.planetRuinDef!.soundID = value;
                            break;
                        case "creditsOnStrippedToTheCore":
                            def.planetRuinDef!.creditsOnStrippedToTheCore = new EntityFloat(value);
                            break;
                        case "metalOnStrippedToTheCore":
                            def.planetRuinDef!.metalOnStrippedToTheCore = new EntityFloat(value);
                            break;
                        case "crystalOnStrippedToTheCore":
                            def.planetRuinDef!.crystalOnStrippedToTheCore = new EntityFloat(value);
                            break;
                        case "extraScuttleTimeOnStrip":
                            def.planetRuinDef!.extraScuttleTimeOnStrip = new EntityFloat(value);
                            break;

                        case "dlcId":
                            def.dlcId = new EntityInt(value);
                            break;
                        case "ShieldMeshName":
                            def.ShieldMeshName = value;
                            break;
                        case "renderShield":
                            def.renderShield = new EntityBool(value);
                            break;
                        case "moveAreaRadius":
                            def.moveAreaRadius = new EntityFloat(value);
                            break;
                        case "hyperspaceExitRadius":
                            def.hyperspaceExitRadius = new EntityFloat(value);
                            break;
                        case "isWormhole":
                            def.isWormhole = new EntityBool(value);
                            break;
                        case "maxStarBaseCountPerPlayer":
                            def.maxStarBaseCountPerPlayer = new EntityInt(value);
                            break;
                        case "maxSpaceMineCountPerPlayer":
                            def.maxSpaceMineCountPerPlayer = new EntityInt(value);
                            break;
                        case "minShadow":
                            def.minShadow = new EntityFloat(value);
                            break;
                        case "maxShadow":
                            def.maxShadow = new EntityFloat(value);
                            break;
                        case "ability:0":
                            if (def.abilities == null)
                                def.abilities = new();
                            def.abilities.Add(value);
                            break;
                        case "ability:1":
                            if (def.abilities == null)
                                def.abilities = new();
                            def.abilities.Add(value);
                            break;
                        case "ability:2":
                            if (def.abilities == null)
                                def.abilities = new();
                            def.abilities.Add(value);
                            break;
                        case "ability:3":
                            if (def.abilities == null)
                                def.abilities = new();
                            def.abilities.Add(value);
                            break;
                        case "ability:4":
                            if (def.abilities == null)
                                def.abilities = new();
                            def.abilities.Add(value);
                            break;
                    }
                }
                catch (NullReferenceException)
                {
                    file.OperationCode = OperationCode.ConversionFailed;
                    render(RenderCode.ConversionFailed, file, "");
                    data.curFiles!.Remove(file);
                    continue;
                }
                catch (IndexOutOfRangeException)
                {
                    file.OperationCode = OperationCode.ConversionFailed;
                    render(RenderCode.ConversionFailed, file, "");
                    data.curFiles!.Remove(file);
                    continue;
                }
                catch (Exception)
                {
                    file.OperationCode = OperationCode.ConversionFailed;
                    render(RenderCode.ConversionFailed, file, "");
                    data.curFiles!.Remove(file);
                    continue;
                }
            }

            JSONSerializer.Serialize(def, file, render, data);
        }

        private static EntityType TypeFinder(SinsFile file)
        {
            foreach (string line in File.ReadLines(file.Path!))
            {
                string trimmed = line.Trim(); 

                if (string.IsNullOrEmpty(trimmed) ||
                    trimmed == "TXT2")
                    continue;

                int separator = trimmed.IndexOf(' ');

                string key;
                string value;

                if (separator < 0)
                {
                    key = trimmed;
                    value = "";
                }
                else
                {
                    key = trimmed[..separator]; 
                    value = trimmed[(separator + 1)..].Trim(); 
                }

                if (value.Contains("\""))
                    value = value.Trim('"');

                if (key == "entityType")
                {
                    if (Enum.TryParse(value, ignoreCase: true, out EntityType type))
                        return type;
                }
            }

            return EntityType.Unknown;
        }
    }
}
