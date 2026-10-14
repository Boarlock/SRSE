/*
namespace SinsEntityConverter
{
    public class PlanetDefinition : Def
    {
        public EntityInt? meshInfoCount;
        public List<meshInfo>? meshInfo;
        public string? glowColor;
        public string? ringColor;
        public string? cloudLayerTextureName;
        public planetResourceSetupInfo? planetResourceSetupInfo;

        public EntityBool? isAsteroid;
        public EntityFloat? healthRegenRate;

        public planetUpgradeDef? planetUpgradeDef;

        public EntityFloat? ringsChance;
        public EntityBool? isColonizable;
        public string? planetTypeForResearch;
        public EntityInt? skyboxScalarsCount;

        public List<skyboxScalar>? skyboxScalars;

        public EntityInt? requiredPlanetBonusesCount;
        public List<string>? requiredBonuses;
        public EntityInt? possibleRandomPlanetBonusesCount;
        public List<string>? possibleBonuses;
        public string? ambienceSoundID;
        public EntityBool? renderAsVolcanic;

        public planetRuinDef? planetRuinDef;

        public EntityInt? dlcId;
        public EntityFloat? moveAreaRadius;
        public EntityFloat? hyperspaceExitRadius;
        public EntityBool? isWormhole;
        public EntityInt? maxStarBaseCountPerPlayer;
        public EntityInt? maxSpaceMineCountPerPlayer;
    }

    public class meshInfo
    {
        public string? typeNameStringID;
        public string? asteroidTemplate;
        public string? dustCloudTemplate;

        public string? meshName;

        public string? cloudColor;
        public EntityFloat? nullMeshRadius;
        public string? nullMeshParticleEffect;
        public string? hudIcon;
        public string? smallHudIcon;
        public string? infoCardIcon;
        public string? mainViewIcon;
        public string? undetectedMainViewIcon;
        public string? picture;
        public EntityBool? shouldPrefer;
    }

    public class planetResourceSetupInfo
    {
        public EntityFloat? asteroidSpawnAngleVariance;
        public EntityInt? totalMaxResourceAsteroids;
        public AsteroidSetup? metalResourceAsteroidSetup;
        public AsteroidSetup? crystalResourceAsteroidSetup;
        public AsteroidSetup? neutralMetalResourceAsteroidSetup;
        public AsteroidSetup? neutralCrystalResourceAsteroidSetup;
    }

    public class AsteroidSetup
    {
        public EntityInt? minCount;
        public EntityInt? maxCount;
        public EntityFloat? extractionRate;
        public EntityFloat? refineryRate;
        public EntityInt? maxRefineryCount;
    }

    public class planetUpgradeDef
    {
        public PathType? populationPath;
        public PathType? civilianPath;
        public PathType? tacticalPath;
        public PathType? homePath;
        public PathType? artifactLevelPath;
        public PathType? infrastructurePath;
        public PathType? socialPath;
        public PathType? industryPath;
        public PathType? smugglerPath;
    }

    public class PathType
    {
        public EntityInt? stageCount;
        public List<Stage>? stages;
    }

    public class skyboxScalar
    {
        public EntityFloat? diffuseScalar;
        public EntityFloat? ambientScalar;
    }

    public class Stage
    {
        public BasePrice? price;
        public EntityFloat? upgradeTime;
        public EntityFloat? maxPopulation;
        public EntityFloat? populationGrowthRate;
        public EntityFloat? developmentTaxPenalty;

        public Dictionary<string, EntityFloat>? maxModuleSlotCount;
        public EntityInt? maxModuleConstructorCount;

        public EntityBool? isHomePlanet;
        public EntityFloat? homePlanetTaxRateBonus;
        public EntityFloat? homePlanetMetalIncomeBonus;
        public EntityFloat? homePlanetCrystalIncomeBonus;

        public EntityFloat? maxHealth;

        public EntityFloat? tradeIncomeModifier;
        public EntityFloat? shipBuildRateModifier;
        public EntityFloat? cultureSpreadRateModifier;

        public EntityFloat? corruptionPercent;
        public EntityFloat? smugglingPercent;
    }

    public class planetRuinDef
    {
        public string? ruinPlanetType;
        public string? attachType;
        public string? effectName;
        public EntityFloat? startTime;
        public string? soundID;
        public EntityFloat? creditsOnStrippedToTheCore;
        public EntityFloat? metalOnStrippedToTheCore;
        public EntityFloat? crystalOnStrippedToTheCore;
        public EntityFloat? extraScuttleTimeOnStrip;
    }
}*/
