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
                    EntityType.Squad => new SquadDefinition(),
                    EntityType.Fighter => new FighterDefinition(),
                    EntityType.Frigate => new FrigateDefinition(),
                    EntityType.SpaceMine => new SpaceMineDefinition(),
                    EntityType.Starbase => new StarbaseDefinition(),
                    EntityType.CannonShell => new CannonShellDefinition(),
                    EntityType.CapitalShip => new CapitalShipDefinition(),
                    EntityType.Titan => new TitanDefinition(),
                    EntityType.PlanetModuleStandard => new PlanetModuleStandardDefinition(),
                    EntityType.PlanetModuleShipFactory => new PlanetModuleStandardDefinition(),
                    EntityType.PlanetModuleHangarDefense => new PlanetModuleHangarDefenseDefinition(),
                    EntityType.PlanetModuleWeaponDefense => new PlanetModuleMissileDefenseDefinition(),
                    EntityType.PlanetModuleRefinery => new PlanetModuleCargoDefinition(),
                    EntityType.PlanetModuleTradePort => new PlanetModuleCargoDefinition(),
                    EntityType.Planet => new PlanetDefinition(),
                    EntityType.Star => new StarDefinition(),
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
