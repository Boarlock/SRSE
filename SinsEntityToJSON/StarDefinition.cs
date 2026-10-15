
using System.Diagnostics;

namespace SinsEntityConverter
{
    public class StarDefinition : Def
    {
        public string? Header = "TXT2";
        public EntityInt? SinsArchiveVersion;
        public string? entityType;
        public string? coronaStartColor;
        public string? coronaEndColor;
        public EntityFloat? coronaStartColorDistance;
        public EntityFloat? coronaEndColorDistance;
        public string? farIcon;

        public EntityInt? coloredSkyboxMeshInfoCount;
        public List<MeshInfoStar>? coloredMeshInfo;
        public EntityInt? deepSpaceSkyboxMeshInfoCount;
        public List<MeshInfoStar>? deepSpaceMeshInfo;

        public string? iconTextureName;
        public EntityInt? dlcID;
        public List<string>? abilities;
        public string? hudIcon;
        public string? smallHudIcon;
        public string? infoCardIcon;
        public EntityFloat? minZoomDistanceMult;
        public EntityFloat? moveAreaRadius;
        public EntityFloat? hyperspaceExitRadius;
        public EntityBool? isWormhole;
        public EntityInt? maxStarBaseCountPerPlayer;
        public EntityInt? maxSpaceMineCountPerPlayer;
        public string? mainViewIcon;
        public string? picture;
        public string? MeshName;
        public EntityFloat? MaxMeshVisibilityDistanceToCamera;
        public EntityFloat? minShadow;
        public EntityFloat? maxShadow;

        private List<MeshInfoStar>? currentMeshInfoList;

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

                case "coronaStartColor":
                    coronaStartColor = value;
                    return true;

                case "coronaEndColor":
                    coronaEndColor = value;
                    return true;

                case "coronaStartColorDistance":
                    coronaStartColorDistance = new EntityFloat(value);
                    return true;

                case "coronaEndColorDistance":
                    coronaEndColorDistance = new EntityFloat(value);
                    return true;

                case "farIcon":
                    farIcon = value;
                    return true;

                case "coloredSkyboxMeshInfoCount":
                    int coloredMeshInfoCount = int.Parse(value);
                    coloredSkyboxMeshInfoCount = new EntityInt(coloredMeshInfoCount);

                    if (coloredMeshInfoCount > 0)
                    {
                        coloredMeshInfo = new();
                        currentMeshInfoList = coloredMeshInfo;
                    }
                    return true;

                case "deepSpaceSkyboxMeshInfoCount":
                    int deepSpaceMeshInfoCount = int.Parse(value);
                    deepSpaceSkyboxMeshInfoCount = new EntityInt(deepSpaceMeshInfoCount);

                    if (deepSpaceMeshInfoCount > 0)
                    {
                        deepSpaceMeshInfo = new();
                        currentMeshInfoList = deepSpaceMeshInfo;
                    }
                    return true;

                case "meshInfo":
                    currentMeshInfoList!.Add(new MeshInfoStar());
                    return true;

                case "meshName":
                    currentMeshInfoList![^1].meshName = value;
                    return true;

                case "shouldPrefer":
                    currentMeshInfoList![^1].shouldPrefer = new EntityBool(value);
                    return true;

                case "iconTextureName":
                    iconTextureName = value;
                    return true;

                case "dlcID":
                    dlcID = new EntityInt(value);
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

                case "mainViewIcon":
                    mainViewIcon = value;
                    return true;

                case "picture":
                    picture = value;
                    return true;

                case "MeshName":
                    MeshName = value;
                    return true;

                case "MaxMeshVisibilityDistanceToCamera":
                    MaxMeshVisibilityDistanceToCamera = new EntityFloat(value);
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

    public class MeshInfoStar
    {
        public string? meshName;
        public EntityBool? shouldPrefer;
    }
}
