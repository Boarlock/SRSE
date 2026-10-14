using System.Diagnostics;
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

        public void Deserialize(string appDir, string tempDir)
        {
            OutputDir = Path.Combine(appDir, "Output JSON");

            if (!Directory.Exists(OutputDir))
                Directory.CreateDirectory(OutputDir);

            foreach (SinsFile file in data.loadedFiles!)
            {
                Debug.WriteLine($"Working on file - Name: {file.FileName}");

                string tempFilePath = Path.Combine(tempDir, Path.GetFileName(file.Path)!);

                File.Copy(file.Path!, tempFilePath, true);

                if (!File.Exists(tempFilePath))
                {
                    file.OperationCode = OperationCode.FileNotFound;
                    render(RenderCode.ConversionFailed, file, "");
                    data.curFiles!.Remove(file);
                    continue;
                }

                file.Path = tempFilePath;
                TypeFinder(file);

                Def? def = file.Type switch
                {
                    EntityType.Fighter => new FighterDefinition(),
                    EntityType.Frigate => new FrigateDefinition(),
                    EntityType.PlanetModuleStandard => new PlanetModuleStandardDefinition(),
                    EntityType.PlanetModuleShipFactory => new PlanetModuleStandardDefinition(),
                    EntityType.PlanetModuleHangarDefense => new PlanetModuleHangarDefenseDefinition(),
                    EntityType.PlanetModuleWeaponDefense => new PlanetModuleMissileDefenseDefinition(),
                    EntityType.PlanetModuleRefinery => new PlanetModuleCargoDefinition(),
                    EntityType.PlanetModuleTradePort => new PlanetModuleCargoDefinition(),
                    EntityType.CapitalShip => new CapitalShipDefinition(),
                    _ => null
                };

                if (def == null)
                {
                    file.OperationCode = OperationCode.InvalidExtension;
                    render(RenderCode.ConversionFailed, file, "");
                    data.curFiles!.Remove(file);
                    continue;
                }
                try
                {
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

                        if (!def.Apply(key, value)) // Returns false means no match found
                        {
                            Debug.WriteLine($"No Match Found. Key: {key} | Value: {value}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception while parsing. Exception: {ex}");
                }

                JSONSerializer.Serialize(def, file, render, data);
            }

            Directory.Delete(tempDir, true);
            render(RenderCode.Cleanup, null, "");
            render(RenderCode.ConversionComplete, null, "");
        }

        /*public void PlanetDeserialize(SinsFile file)
        {
            bool exceptionCaught = false;

            Def? def = null;
            switch (file.Type)
            {
                case EntityType.Planet:
                    def = new PlanetDefinition();
                    break;
                case EntityType.Star:
                    def = new StarDefinition();
                    break;
            }

            if (def == null)
            {
                file.OperationCode = OperationCode.InvalidExtension;
                render(RenderCode.ConversionFailed, file, "");
                data.curFiles!.Remove(file);
                return;
            }

            BasePrice? currentPrice = null;
            PathType? currentPath = null;
            AsteroidSetup? currentAsteroid = null;
            List<string>? currentBonuses = null;

            int meshInfoCount = -1;
            int stageCount = -1;
            int skyboxCount = -1;

            foreach (string line in File.ReadLines(file.Path!))
            {
                if (exceptionCaught)
                    return;

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
                            ((PlanetDefinition)def).meshInfoCount = new EntityInt(value);
                            break;

                        case "meshInfo":
                            ((PlanetDefinition)def).meshInfo ??= new();
                            ((PlanetDefinition)def).meshInfo!.Add(new meshInfo());
                            meshInfoCount++;
                            break;
                        case "typeNameStringID":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].typeNameStringID = value;
                            break;
                        case "asteroidTemplate":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].asteroidTemplate = value;
                            break;
                        case "dustCloudTemplate":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].dustCloudTemplate = value;
                            break;
                        case "meshName":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].meshName = value;
                            break;
                        case "cloudColor":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].cloudColor = value;
                            break;
                        case "nullMeshRadius":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].nullMeshRadius = new EntityFloat(value);
                            break;
                        case "nullMeshParticleEffect":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].nullMeshParticleEffect = value;
                            break;
                        case "hudIcon":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].hudIcon = value;
                            break;
                        case "smallHudIcon":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].smallHudIcon = value;
                            break;
                        case "infoCardIcon":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].infoCardIcon = value;
                            break;
                        case "mainViewIcon":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].mainViewIcon = value;
                            break;
                        case "undetectedMainViewIcon":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].undetectedMainViewIcon = value;
                            break;
                        case "picture":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].picture = value;
                            break;
                        case "shouldPrefer":
                            ((PlanetDefinition)def).meshInfo![meshInfoCount].shouldPrefer = new EntityBool(value);
                            break;

                        case "minZoomDistanceMult":
                            def.minZoomDistanceMult = new EntityFloat(value);
                            break;
                        case "glowColor":
                            ((PlanetDefinition)def).glowColor = value;
                            break;
                        case "ringColor":
                            ((PlanetDefinition)def).ringColor = value;
                            break;
                        case "cloudLayerTextureName":
                            ((PlanetDefinition)def).cloudLayerTextureName = value;
                            break;

                        case "planetResourceSetupInfo":
                            ((PlanetDefinition)def).planetResourceSetupInfo = new();
                            break;
                        case "asteroidSpawnAngleVariance":
                            ((PlanetDefinition)def).planetResourceSetupInfo!.asteroidSpawnAngleVariance = new EntityFloat(value);
                            break;
                        case "totalMaxResourceAsteroids":
                            ((PlanetDefinition)def).planetResourceSetupInfo!.totalMaxResourceAsteroids = new EntityInt(value);
                            break;

                        case "metalResourceAsteroidSetup":
                            ((PlanetDefinition)def).planetResourceSetupInfo!.metalResourceAsteroidSetup = new();
                            currentAsteroid = ((PlanetDefinition)def).planetResourceSetupInfo!.metalResourceAsteroidSetup;
                            break;
                        case "crystalResourceAsteroidSetup":
                            ((PlanetDefinition)def).planetResourceSetupInfo!.crystalResourceAsteroidSetup = new();
                            currentAsteroid = ((PlanetDefinition)def).planetResourceSetupInfo!.crystalResourceAsteroidSetup;
                            break;
                        case "neutralMetalResourceAsteroidSetup":
                            ((PlanetDefinition)def).planetResourceSetupInfo!.neutralMetalResourceAsteroidSetup = new();
                            currentAsteroid = ((PlanetDefinition)def).planetResourceSetupInfo!.neutralMetalResourceAsteroidSetup;
                            break;
                        case "neutralCrystalResourceAsteroidSetup":
                            ((PlanetDefinition)def).planetResourceSetupInfo!.neutralCrystalResourceAsteroidSetup = new();
                            currentAsteroid = ((PlanetDefinition)def).planetResourceSetupInfo!.neutralCrystalResourceAsteroidSetup;
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
                            ((PlanetDefinition)def).isAsteroid = new EntityBool(value);
                            break;
                        case "healthRegenRate":
                            ((PlanetDefinition)def).healthRegenRate = new EntityFloat(value);
                            break;

                        case "planetUpgradeDef":
                            ((PlanetDefinition)def).planetUpgradeDef = new();
                            break;
                        case "path:Population":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.populationPath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.populationPath;
                            break;
                        case "path:CivilianModules":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.civilianPath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.civilianPath;
                            break;
                        case "path:TacticalModules":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.tacticalPath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.tacticalPath;
                            break;
                        case "path:Home":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.homePath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.homePath;
                            break;
                        case "path:ArtifactLevel":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.artifactLevelPath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.artifactLevelPath;
                            break;
                        case "path:Infrastructure":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.infrastructurePath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.infrastructurePath;
                            break;
                        case "path:Social":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.socialPath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.socialPath;
                            break;
                        case "path:Industry":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.industryPath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.industryPath;
                            break;
                        case "path:Smuggler":
                            stageCount = -1;
                            ((PlanetDefinition)def).planetUpgradeDef!.smugglerPath = new();
                            currentPath = ((PlanetDefinition)def).planetUpgradeDef!.smugglerPath;
                            break;

                        case "stageCount":
                            currentPath!.stageCount = new EntityInt(value);
                            break;

                        case "stage":
                            currentPath!.stages ??= new();
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
                            currentPath!.stages![stageCount].maxModuleSlotCount ??= new();
                            currentPath!.stages![stageCount].maxModuleSlotCount!.Add("Civilian", new EntityFloat(value));
                            break;
                        case "maxModuleSlotCount:Tactical":
                            currentPath!.stages![stageCount].maxModuleSlotCount ??= new();
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
                            ((PlanetDefinition)def).ringsChance = new EntityFloat(value);
                            break;
                        case "isColonizable":
                            ((PlanetDefinition)def).isColonizable = new EntityBool(value);
                            break;
                        case "planetTypeForResearch":
                            ((PlanetDefinition)def).planetTypeForResearch = value;
                            break;
                        case "skyboxScalarsCount":
                            ((PlanetDefinition)def).skyboxScalarsCount = new EntityInt(value);
                            break;

                        case "skyboxScalars":
                            ((PlanetDefinition)def).skyboxScalars ??= new();
                            ((PlanetDefinition)def).skyboxScalars!.Add(new skyboxScalar());
                            skyboxCount++;
                            break;
                        case "diffuseScalar":
                            ((PlanetDefinition)def).skyboxScalars![skyboxCount].diffuseScalar = new EntityFloat(value);
                            break;
                        case "ambientScalar":
                            ((PlanetDefinition)def).skyboxScalars![skyboxCount].ambientScalar = new EntityFloat(value);
                            break;

                        case "requiredPlanetBonusesCount":
                            int requiredCount = int.Parse(value);
                            ((PlanetDefinition)def).requiredPlanetBonusesCount = new EntityInt(requiredCount);

                            if (requiredCount > 0)
                            {
                                ((PlanetDefinition)def).requiredBonuses = new();
                                currentBonuses = ((PlanetDefinition)def).requiredBonuses;
                            }
                            break;
                        case "possibleRandomPlanetBonusesCount":
                            int possibleCount = int.Parse(value);
                            ((PlanetDefinition)def).possibleRandomPlanetBonusesCount = new EntityInt(possibleCount);

                            if (possibleCount > 0)
                            {
                                ((PlanetDefinition)def).possibleBonuses = new();
                                currentBonuses = ((PlanetDefinition)def).possibleBonuses;
                            }
                            break;

                        case "bonus":
                            currentBonuses!.Add(value);
                            break;

                        case "ambienceSoundID":
                            currentBonuses = null;
                            ((PlanetDefinition)def).ambienceSoundID = value;
                            break;
                        case "renderAsVolcanic":
                            ((PlanetDefinition)def).renderAsVolcanic = new EntityBool(value);
                            break;

                        case "planetRuinDef":
                            ((PlanetDefinition)def).planetRuinDef = new();
                            break;
                        case "ruinPlanetType":
                            ((PlanetDefinition)def).planetRuinDef!.ruinPlanetType = value;
                            break;
                        case "attachType":
                            ((PlanetDefinition)def).planetRuinDef!.attachType = value;
                            break;
                        case "effectName":
                            ((PlanetDefinition)def).planetRuinDef!.effectName = value;
                            break;
                        case "startTime":
                            ((PlanetDefinition)def).planetRuinDef!.startTime = new EntityFloat(value);
                            break;
                        case "soundID":
                            ((PlanetDefinition)def).planetRuinDef!.soundID = value;
                            break;
                        case "creditsOnStrippedToTheCore":
                            ((PlanetDefinition)def).planetRuinDef!.creditsOnStrippedToTheCore = new EntityFloat(value);
                            break;
                        case "metalOnStrippedToTheCore":
                            ((PlanetDefinition)def).planetRuinDef!.metalOnStrippedToTheCore = new EntityFloat(value);
                            break;
                        case "crystalOnStrippedToTheCore":
                            ((PlanetDefinition)def).planetRuinDef!.crystalOnStrippedToTheCore = new EntityFloat(value);
                            break;
                        case "extraScuttleTimeOnStrip":
                            ((PlanetDefinition)def).planetRuinDef!.extraScuttleTimeOnStrip = new EntityFloat(value);
                            break;

                        case "dlcId":
                            ((PlanetDefinition)def).dlcId = new EntityInt(value);
                            break;
                        case "ShieldMeshName":
                            def.ShieldMeshName = value;
                            break;
                        case "renderShield":
                            def.renderShield = new EntityBool(value);
                            break;
                        case "moveAreaRadius":
                            ((PlanetDefinition)def).moveAreaRadius = new EntityFloat(value);
                            break;
                        case "hyperspaceExitRadius":
                            ((PlanetDefinition)def).hyperspaceExitRadius = new EntityFloat(value);
                            break;
                        case "isWormhole":
                            ((PlanetDefinition)def).isWormhole = new EntityBool(value);
                            break;
                        case "maxStarBaseCountPerPlayer":
                            ((PlanetDefinition)def).maxStarBaseCountPerPlayer = new EntityInt(value);
                            break;
                        case "maxSpaceMineCountPerPlayer":
                            ((PlanetDefinition)def).maxSpaceMineCountPerPlayer = new EntityInt(value);
                            break;
                        case "minShadow":
                            def.minShadow = new EntityFloat(value);
                            break;
                        case "maxShadow":
                            def.maxShadow = new EntityFloat(value);
                            break;
                        case "ability:0":
                            def.abilities ??= new();
                            def.abilities.Add(value);
                            break;
                        case "ability:1":
                            def.abilities ??= new();
                            def.abilities.Add(value);
                            break;
                        case "ability:2":
                            def.abilities ??= new();
                            def.abilities.Add(value);
                            break;
                        case "ability:3":
                            def.abilities ??= new();
                            def.abilities.Add(value);
                            break;
                        case "ability:4":
                            def.abilities ??= new();
                            def.abilities.Add(value);
                            break;
                        default:
                            Debug.WriteLine($"No match found. Key: {key} | Value: {value}");
                            break;
                    }
                }
                catch (Exception)
                {
                    file.OperationCode = OperationCode.ConversionFailed;
                    render(RenderCode.ConversionFailed, file, "");
                    data.curFiles!.Remove(file);
                    exceptionCaught = true;
                }
            }

            JSONSerializer.Serialize(def, file, render, data);
        }*/

        private static void TypeFinder(SinsFile file)
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
                    {
                        file.Type = type;
                        return;
                    }
                    else
                    {
                        file.Type = EntityType.Unknown;
                        return;
                    }
                }
            }

            file.Type = EntityType.Unknown;
        }
    }
}
