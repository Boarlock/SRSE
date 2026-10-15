
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace SinsEntityConverter
{
    public class PlanetDefinition : Def
    {
        [JsonPropertyOrder(0)]
        public string? Header = "TXT2";
        [JsonPropertyOrder(1)]
        public EntityInt? SinsArchiveVersion;
        [JsonPropertyOrder(2)]
        public string? entityType;
        [JsonPropertyOrder(3)]
        public EntityInt? meshInfoCount;
        [JsonPropertyOrder(4)]
        public List<MeshInfoPlanet>? meshInfo;
        [JsonPropertyOrder(5)]
        public EntityFloat? minZoomDistanceMult;
        [JsonPropertyOrder(6)]
        public string? glowColor;
        [JsonPropertyOrder(7)]
        public string? ringColor;
        [JsonPropertyOrder(8)]
        public string? cloudLayerTextureName;
        [JsonPropertyOrder(9)]
        public PlanetResourceSetupInfo? planetResourceSetupInfo;
        [JsonPropertyOrder(10)]
        public EntityBool? isAsteroid;
        [JsonPropertyOrder(11)]
        public EntityFloat? healthRegenRate;
        [JsonPropertyOrder(12)]
        public PlanetUpgradeDef? planetUpgradeDef;
        [JsonPropertyOrder(13)]
        public EntityFloat? ringsChance;
        [JsonPropertyOrder(14)]
        public EntityBool? isColonizable;
        [JsonPropertyOrder(15)]
        public string? planetTypeForResearch;
        [JsonPropertyOrder(16)]
        public EntityInt? skyboxScalarsCount;
        [JsonPropertyOrder(17)]
        public List<SkyboxScalar>? skyboxScalars;
        [JsonPropertyOrder(18)]
        public EntityInt? requiredPlanetBonusesCount;
        [JsonPropertyOrder(19)]
        public List<string>? requiredBonuses;
        [JsonPropertyOrder(20)]
        public EntityInt? possibleRandomPlanetBonusesCount;
        [JsonPropertyOrder(21)]
        public List<string>? possibleBonuses;
        [JsonPropertyOrder(22)]
        public string? ambienceSoundID;
        [JsonPropertyOrder(23)]
        public EntityBool? renderAsVolcanic;
        [JsonPropertyOrder(24)]
        public PlanetRuinDef? planetRuinDef;
        [JsonPropertyOrder(25)]
        public EntityInt? dlcId;
        [JsonPropertyOrder(26)]
        public string? ShieldMeshName;
        [JsonPropertyOrder(27)]
        public EntityBool? renderShield;
        [JsonPropertyOrder(28)]
        public EntityFloat? moveAreaRadius;
        [JsonPropertyOrder(29)]
        public EntityFloat? hyperspaceExitRadius;
        [JsonPropertyOrder(30)]
        public EntityBool? isWormhole;
        [JsonPropertyOrder(31)]
        public EntityInt? maxStarBaseCountPerPlayer;
        [JsonPropertyOrder(32)]
        public EntityInt? maxSpaceMineCountPerPlayer;
        [JsonPropertyOrder(33)]
        public EntityFloat? minShadow;
        [JsonPropertyOrder(34)]
        public EntityFloat? maxShadow;
        [JsonPropertyOrder(35)]
        public List<string>? abilities;

        private AsteroidSetup? currentAsteroid;
        private BasePrice? currentPrice;
        private PathType? currentPath;
        private List<string>? currentBonus;

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

                case "meshInfoCount":
                    meshInfoCount = new EntityInt(value);
                    return true;

                case "meshInfo":
                    meshInfo ??= new();
                    meshInfo!.Add(new MeshInfoPlanet());
                    return true;

                case "typeNameStringID":
                    meshInfo![^1].typeNameStringID = value;
                    return true;

                case "asteroidTemplate":
                    meshInfo![^1].asteroidTemplate = value;
                    return true;

                case "dustCloudTemplate":
                    meshInfo![^1].dustCloudTemplate = value;
                    return true;

                case "meshName":
                    meshInfo![^1].meshName = value;
                    return true;

                case "cloudColor":
                    meshInfo![^1].cloudColor = value;
                    return true;

                case "nullMeshRadius":
                    meshInfo![^1].nullMeshRadius = new EntityFloat(value);
                    return true;

                case "nullMeshParticleEffect":
                    meshInfo![^1].nullMeshParticleEffect = value;
                    return true;

                case "hudIcon":
                    meshInfo![^1].hudIcon = value;
                    return true;

                case "smallHudIcon":
                    meshInfo![^1].smallHudIcon = value;
                    return true;

                case "infoCardIcon":
                    meshInfo![^1].infoCardIcon = value;
                    return true;

                case "mainViewIcon":
                    meshInfo![^1].mainViewIcon = value;
                    return true;

                case "undetectedMainViewIcon":
                    meshInfo![^1].undetectedMainViewIcon = value;
                    return true;

                case "picture":
                    meshInfo![^1].picture = value;
                    return true;

                case "shouldPrefer":
                    meshInfo![^1].shouldPrefer = new EntityBool(value);
                    return true;

                case "minZoomDistanceMult":
                    minZoomDistanceMult = new EntityFloat(value);
                    return true;

                case "glowColor":
                    glowColor = value;
                    return true;

                case "ringColor":
                    ringColor = value;
                    return true;

                case "cloudLayerTextureName":
                    cloudLayerTextureName = value;
                    return true;

                case "planetResourceSetupInfo":
                    planetResourceSetupInfo = new();
                    return true;

                case "asteroidSpawnAngleVariance":
                    planetResourceSetupInfo!.asteroidSpawnAngleVariance = new EntityFloat(value);
                    return true;

                case "totalMaxResourceAsteroids":
                    planetResourceSetupInfo!.totalMaxResourceAsteroids = new EntityInt(value);
                    return true;

                case "metalResourceAsteroidSetup":
                    planetResourceSetupInfo!.metalResourceAsteroidSetup = new();
                    currentAsteroid = planetResourceSetupInfo!.metalResourceAsteroidSetup;
                    return true;

                case "crystalResourceAsteroidSetup":
                    planetResourceSetupInfo!.crystalResourceAsteroidSetup = new();
                    currentAsteroid = planetResourceSetupInfo!.crystalResourceAsteroidSetup;
                    return true;

                case "neutralMetalResourceAsteroidSetup":
                    planetResourceSetupInfo!.neutralMetalResourceAsteroidSetup = new();
                    currentAsteroid = planetResourceSetupInfo!.neutralMetalResourceAsteroidSetup;
                    return true;

                case "neutralCrystalResourceAsteroidSetup":
                    planetResourceSetupInfo!.neutralCrystalResourceAsteroidSetup = new();
                    currentAsteroid = planetResourceSetupInfo!.neutralCrystalResourceAsteroidSetup;
                    return true;

                case "minCount":
                    currentAsteroid!.minCount = new EntityInt(value);
                    return true;

                case "maxCount":
                    currentAsteroid!.maxCount = new EntityInt(value);
                    return true;

                case "extractionRate":
                    currentAsteroid!.extractionRate = new EntityFloat(value);
                    return true;

                case "refineryRate":
                    currentAsteroid!.refineryRate = new EntityFloat(value);
                    return true;

                case "maxRefineryCount":
                    currentAsteroid!.maxRefineryCount = new EntityInt(value);
                    return true;

                case "isAsteroid":
                    currentAsteroid = null;
                    isAsteroid = new EntityBool(value);
                    return true;

                case "healthRegenRate":
                    healthRegenRate = new EntityFloat(value);
                    return true;

                case "planetUpgradeDef":
                    planetUpgradeDef = new();
                    return true;

                case "path:Population":
                    planetUpgradeDef!.populationPath = new();
                    currentPath = planetUpgradeDef!.populationPath;
                    return true;

                case "path:CivilianModules":
                    planetUpgradeDef!.civilianPath = new();
                    currentPath = planetUpgradeDef!.civilianPath;
                    return true;

                case "path:TacticalModules":
                    planetUpgradeDef!.tacticalPath = new();
                    currentPath = planetUpgradeDef!.tacticalPath;
                    return true;

                case "path:Home":
                    planetUpgradeDef!.homePath = new();
                    currentPath = planetUpgradeDef!.homePath;
                    return true;

                case "path:ArtifactLevel":
                    planetUpgradeDef!.artifactLevelPath = new();
                    currentPath = planetUpgradeDef!.artifactLevelPath;
                    return true;

                case "path:Infrastructure":
                    planetUpgradeDef!.infrastructurePath = new();
                    currentPath = planetUpgradeDef!.infrastructurePath;
                    return true;

                case "path:Social":
                    planetUpgradeDef!.socialPath = new();
                    currentPath = planetUpgradeDef!.socialPath;
                    return true;

                case "path:Industry":
                    planetUpgradeDef!.industryPath = new();
                    currentPath = planetUpgradeDef!.industryPath;
                    return true;

                case "path:Smuggler":
                    planetUpgradeDef!.smugglerPath = new();
                    currentPath = planetUpgradeDef!.smugglerPath;
                    return true;

                case "stageCount":
                    currentPath!.stageCount = new EntityInt(value);
                    return true;

                case "stage":
                    currentPath!.stages ??= new();
                    currentPath!.stages.Add(new Stage());
                    return true;

                case "price":
                    currentPath!.stages![^1].price = new BasePrice();
                    currentPrice = currentPath!.stages![^1].price;
                    return true;

                case "credits":
                    currentPrice!.Credits = new EntityFloat(value);
                    return true;

                case "metal":
                    currentPrice!.Metal = new EntityFloat(value);
                    return true;

                case "crystal":
                    currentPrice!.Crystal = new EntityFloat(value);
                    return true;

                case "upgradeTime":
                    currentPath!.stages![^1].upgradeTime = new EntityFloat(value);
                    return true;

                case "maxPopulation":
                    currentPath!.stages![^1].maxPopulation = new EntityFloat(value);
                    return true;

                case "populationGrowthRate":
                    currentPath!.stages![^1].populationGrowthRate = new EntityFloat(value);
                    return true;

                case "developmentTaxPenalty":
                    currentPath!.stages![^1].developmentTaxPenalty = new EntityFloat(value);
                    return true;

                case "maxModuleSlotCount:Civilian":
                    currentPath!.stages![^1].maxModuleSlotCount ??= new();
                    currentPath!.stages![^1].maxModuleSlotCount!.Add("Civilian", new EntityFloat(value));
                    return true;

                case "maxModuleSlotCount:Tactical":
                    currentPath!.stages![^1].maxModuleSlotCount ??= new();
                    currentPath!.stages![^1].maxModuleSlotCount!.Add("Tactical", new EntityFloat(value));
                    return true;

                case "maxModuleConstructorCount":
                    currentPath!.stages![^1].maxModuleConstructorCount = new EntityInt(value);
                    return true;

                case "isHomePlanet":
                    currentPath!.stages![^1].isHomePlanet = new EntityBool(value);
                    return true;

                case "homePlanetTaxRateBonus":
                    currentPath!.stages![^1].homePlanetTaxRateBonus = new EntityFloat(value);
                    return true;

                case "homePlanetMetalIncomeBonus":
                    currentPath!.stages![^1].homePlanetMetalIncomeBonus = new EntityFloat(value);
                    return true;

                case "homePlanetCrystalIncomeBonus":
                    currentPath!.stages![^1].homePlanetCrystalIncomeBonus = new EntityFloat(value);
                    return true;

                case "maxHealth":
                    currentPath!.stages![^1].maxHealth = new EntityFloat(value);
                    return true;

                case "tradeIncomeModifier":
                    currentPath!.stages![^1].tradeIncomeModifier = new EntityFloat(value);
                    return true;

                case "shipBuildRateModifier":
                    currentPath!.stages![^1].shipBuildRateModifier = new EntityFloat(value);
                    return true;

                case "cultureSpreadRateModifier":
                    currentPath!.stages![^1].cultureSpreadRateModifier = new EntityFloat(value);
                    return true;

                case "corruptionPercent":
                    currentPath!.stages![^1].corruptionPercent = new EntityFloat(value);
                    return true;

                case "smugglingPercent":
                    currentPath!.stages![^1].smugglingPercent = new EntityFloat(value);
                    return true;

                case "ringsChance":
                    currentPath = null;
                    currentPrice = null;
                    ringsChance = new EntityFloat(value);
                    return true;

                case "isColonizable":
                    isColonizable = new EntityBool(value);
                    return true;

                case "planetTypeForResearch":
                    planetTypeForResearch = value;
                    return true;

                case "skyboxScalarsCount":
                    skyboxScalarsCount = new EntityInt(value);
                    return true;

                case "skyboxScalars":
                    skyboxScalars ??= new();
                    skyboxScalars!.Add(new SkyboxScalar());
                    return true;

                case "diffuseScalar":
                    skyboxScalars![^1].diffuseScalar = new EntityFloat(value);
                    return true;

                case "ambientScalar":
                    skyboxScalars![^1].ambientScalar = new EntityFloat(value);
                    return true;

                case "requiredPlanetBonusesCount":
                    int requiredCount = int.Parse(value);
                    requiredPlanetBonusesCount = new EntityInt(requiredCount);

                    if (requiredCount > 0)
                    {
                        requiredBonuses = new();
                        currentBonus = requiredBonuses;
                    }
                    return true;

                case "possibleRandomPlanetBonusesCount":
                    int possibleCount = int.Parse(value);
                    possibleRandomPlanetBonusesCount = new EntityInt(possibleCount);

                    if (possibleCount > 0)
                    {
                        possibleBonuses = new();
                        currentBonus = possibleBonuses;
                    }
                    return true;

                case "bonus":
                    currentBonus!.Add(value);
                    return true;

                case "ambienceSoundID":
                    currentBonus = null;
                    ambienceSoundID = value;
                    return true;

                case "renderAsVolcanic":
                    renderAsVolcanic = new EntityBool(value);
                    return true;

                case "planetRuinDef":
                    planetRuinDef = new();
                    return true;

                case "ruinPlanetType":
                    planetRuinDef!.ruinPlanetType = value;
                    return true;

                case "attachType":
                    planetRuinDef!.attachType = value;
                    return true;

                case "effectName":
                    planetRuinDef!.effectName = value;
                    return true;

                case "startTime":
                    planetRuinDef!.startTime = new EntityFloat(value);
                    return true;

                case "soundID":
                    planetRuinDef!.soundID = value;
                    return true;

                case "creditsOnStrippedToTheCore":
                    planetRuinDef!.creditsOnStrippedToTheCore = new EntityFloat(value);
                    return true;

                case "metalOnStrippedToTheCore":
                    planetRuinDef!.metalOnStrippedToTheCore = new EntityFloat(value);
                    return true;

                case "crystalOnStrippedToTheCore":
                    planetRuinDef!.crystalOnStrippedToTheCore = new EntityFloat(value);
                    return true;

                case "extraScuttleTimeOnStrip":
                    planetRuinDef!.extraScuttleTimeOnStrip = new EntityFloat(value);
                    return true;

                case "dlcId":
                    dlcId = new EntityInt(value);
                    return true;

                case "ShieldMeshName":
                    ShieldMeshName = value;
                    return true;

                case "renderShield":
                    renderShield = new EntityBool(value);
                    return true;

                case "moveAreaRadius":
                    moveAreaRadius = new EntityFloat(value);
                    return true;

                case "hyperspaceExitRadius":
                    hyperspaceExitRadius = new EntityFloat(value);
                    return true;

                case "isWormhole":
                    isWormhole = new EntityBool(value);
                    return true;

                case "maxStarBaseCountPerPlayer":
                    maxStarBaseCountPerPlayer = new EntityInt(value);
                    return true;

                case "maxSpaceMineCountPerPlayer":
                    maxSpaceMineCountPerPlayer = new EntityInt(value);
                    return true;

                case "minShadow":
                    minShadow = new EntityFloat(value);
                    return true;

                case "maxShadow":
                    maxShadow = new EntityFloat(value);
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

                default:
                    return false;
            }
        }
    }

    public class MeshInfoPlanet
    {
        [JsonPropertyOrder(0)]
        public string? typeNameStringID;
        [JsonPropertyOrder(1)]
        public string? asteroidTemplate;
        [JsonPropertyOrder(2)]
        public string? dustCloudTemplate;
        [JsonPropertyOrder(3)]
        public string? meshName;
        [JsonPropertyOrder(4)]
        public string? cloudColor;
        [JsonPropertyOrder(5)]
        public EntityFloat? nullMeshRadius;
        [JsonPropertyOrder(6)]
        public string? nullMeshParticleEffect;
        [JsonPropertyOrder(7)]
        public string? hudIcon;
        [JsonPropertyOrder(8)]
        public string? smallHudIcon;
        [JsonPropertyOrder(9)]
        public string? infoCardIcon;
        [JsonPropertyOrder(10)]
        public string? mainViewIcon;
        [JsonPropertyOrder(11)]
        public string? undetectedMainViewIcon;
        [JsonPropertyOrder(12)]
        public string? picture;
        [JsonPropertyOrder(13)]
        public EntityBool? shouldPrefer;
    }

    public class PlanetResourceSetupInfo
    {
        [JsonPropertyOrder(0)]
        public EntityFloat? asteroidSpawnAngleVariance;
        [JsonPropertyOrder(1)]
        public EntityInt? totalMaxResourceAsteroids;
        [JsonPropertyOrder(2)]
        public AsteroidSetup? metalResourceAsteroidSetup;
        [JsonPropertyOrder(3)]
        public AsteroidSetup? crystalResourceAsteroidSetup;
        [JsonPropertyOrder(4)]
        public AsteroidSetup? neutralMetalResourceAsteroidSetup;
        [JsonPropertyOrder(5)]
        public AsteroidSetup? neutralCrystalResourceAsteroidSetup;
    }

    public class AsteroidSetup
    {
        [JsonPropertyOrder(0)]
        public EntityInt? minCount;
        [JsonPropertyOrder(1)]
        public EntityInt? maxCount;
        [JsonPropertyOrder(2)]
        public EntityFloat? extractionRate;
        [JsonPropertyOrder(3)]
        public EntityFloat? refineryRate;
        [JsonPropertyOrder(4)]
        public EntityInt? maxRefineryCount;
    }

    public class PlanetUpgradeDef
    {
        [JsonPropertyOrder(0)]
        public PathType? populationPath;
        [JsonPropertyOrder(1)]
        public PathType? civilianPath;
        [JsonPropertyOrder(2)]
        public PathType? tacticalPath;
        [JsonPropertyOrder(3)]
        public PathType? homePath;
        [JsonPropertyOrder(4)]
        public PathType? artifactLevelPath;
        [JsonPropertyOrder(5)]
        public PathType? infrastructurePath;
        [JsonPropertyOrder(6)]
        public PathType? socialPath;
        [JsonPropertyOrder(7)]
        public PathType? industryPath;
        [JsonPropertyOrder(8)]
        public PathType? smugglerPath;
    }

    public class PathType
    {
        [JsonPropertyOrder(0)]
        public EntityInt? stageCount;
        [JsonPropertyOrder(1)]
        public List<Stage>? stages;
    }

    public class SkyboxScalar
    {
        [JsonPropertyOrder(0)]
        public EntityFloat? diffuseScalar;
        [JsonPropertyOrder(1)]
        public EntityFloat? ambientScalar;
    }

    public class Stage
    {
        [JsonPropertyOrder(0)]
        public BasePrice? price;
        [JsonPropertyOrder(1)]
        public EntityFloat? upgradeTime;
        [JsonPropertyOrder(2)]
        public EntityFloat? maxPopulation;
        [JsonPropertyOrder(3)]
        public EntityFloat? populationGrowthRate;
        [JsonPropertyOrder(4)]
        public EntityFloat? developmentTaxPenalty;
        [JsonPropertyOrder(5)]
        public Dictionary<string, EntityFloat>? maxModuleSlotCount;
        [JsonPropertyOrder(6)]
        public EntityInt? maxModuleConstructorCount;
        [JsonPropertyOrder(7)]
        public EntityBool? isHomePlanet;
        [JsonPropertyOrder(8)]
        public EntityFloat? homePlanetTaxRateBonus;
        [JsonPropertyOrder(9)]
        public EntityFloat? homePlanetMetalIncomeBonus;
        [JsonPropertyOrder(10)]
        public EntityFloat? homePlanetCrystalIncomeBonus;
        [JsonPropertyOrder(11)]
        public EntityFloat? maxHealth;
        [JsonPropertyOrder(12)]
        public EntityFloat? tradeIncomeModifier;
        [JsonPropertyOrder(13)]
        public EntityFloat? shipBuildRateModifier;
        [JsonPropertyOrder(14)]
        public EntityFloat? cultureSpreadRateModifier;
        [JsonPropertyOrder(15)]
        public EntityFloat? corruptionPercent;
        [JsonPropertyOrder(16)]
        public EntityFloat? smugglingPercent;
    }

    public class PlanetRuinDef
    {
        [JsonPropertyOrder(0)]
        public string? ruinPlanetType;
        [JsonPropertyOrder(1)]
        public string? attachType;
        [JsonPropertyOrder(2)]
        public string? effectName;
        [JsonPropertyOrder(3)]
        public EntityFloat? startTime;
        [JsonPropertyOrder(4)]
        public string? soundID;
        [JsonPropertyOrder(5)]
        public EntityFloat? creditsOnStrippedToTheCore;
        [JsonPropertyOrder(6)]
        public EntityFloat? metalOnStrippedToTheCore;
        [JsonPropertyOrder(7)]
        public EntityFloat? crystalOnStrippedToTheCore;
        [JsonPropertyOrder(8)]
        public EntityFloat? extraScuttleTimeOnStrip;
    }
}
