
namespace SinsEntityConverter
{
    public class SpaceMineDefinition : Def
    {
        public string? Header = "TXT2";
        public EntityInt? SinsArchiveVersion;
        public string? entityType;
        public List<string>? abilities;
        public EntityInt? numRandomDebrisLarge;
        public EntityInt? numRandomDebrisSmall;
        public EntityInt? numSpecificDebris;
        public List<string>? specificDebrisMeshName;
        public string? armorType;
        public string? hudIcon;
        public string? smallHudIcon;
        public string? infoCardIcon;
        public string? picture;
        public string? mainViewIcon;
        public EntityFloat? minZoomDistanceMult;
        public string? MeshName;
        public EntityFloat? spawnActivationDelay;
        public EntityFloat? targetStartFollowRange;
        public EntityFloat? targetMaxFollowRange;
        public string? nameStringID;
        public BasePrice? scuttleValue;
        public EntityFloat? baseScuttleTime;
        public string? ExplosionName;
        public EntityFloat? minShadow;
        public EntityFloat? maxShadow;
        public EntityFloat? mass;
        public EntityFloat? maxAccelerationLinear;
        public EntityFloat? maxAccelerationStrafe;
        public EntityFloat? maxDecelerationLinear;
        public EntityFloat? maxAccelerationAngular;
        public EntityFloat? maxDecelerationAngular;
        public EntityFloat? maxSpeedLinear;
        public EntityFloat? maxRollRate;
        public EntityFloat? maxRollAngle;
        public List<NumSoundsFor>? NumSounds;
        public EntityFloat? maxHullPoints;
        public EntityFloat? hullPointRestoreRate;
        public EntityFloat? maxShieldPoints;
        public EntityFloat? shieldPointRestoreRate;
        public EntityFloat? maxShieldMitigation;
        public EntityFloat? armorPoints;
        public EntityFloat? experiencePointsForDestroying;
        public EntityFloat? baseMaxAntiMatter;
        public EntityFloat? baseAntiMatterRestoreRate;


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

                case "picture":
                    picture = value;
                    return true;

                case "mainViewIcon":
                    mainViewIcon = value;
                    return true;

                case "minZoomDistanceMult":
                    minZoomDistanceMult = new EntityFloat(value);
                    return true;

                case "MeshName":
                    MeshName = value;
                    return true;

                case "spawnActivationDelay":
                    spawnActivationDelay = new EntityFloat(value);
                    return true;

                case "targetStartFollowRange":
                    targetStartFollowRange = new EntityFloat(value);
                    return true;

                case "targetMaxFollowRange":
                    targetMaxFollowRange = new EntityFloat(value);
                    return true;

                case "nameStringID":
                    nameStringID = value;
                    return true;

                case "scuttleValue":
                    scuttleValue = new BasePrice();
                    return true;

                case "credits":
                    scuttleValue!.Credits = new EntityFloat(value);
                    return true;

                case "metal":
                    scuttleValue!.Metal = new EntityFloat(value);
                    return true;

                case "crystal":
                    scuttleValue!.Crystal = new EntityFloat(value);
                    return true;

                case "baseScuttleTime":
                    baseScuttleTime = new EntityFloat(value);
                    return true;

                case "ExplosionName":
                    ExplosionName = value;
                    return true;

                case "minShadow":
                    minShadow = new EntityFloat(value);
                    return true;

                case "maxShadow":
                    maxShadow = new EntityFloat(value);
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

                case "maxHullPoints":
                    maxHullPoints = new EntityFloat(value);
                    return true;

                case "hullPointRestoreRate":
                    hullPointRestoreRate = new EntityFloat(value);
                    return true;

                case "maxShieldPoints":
                    maxShieldPoints = new EntityFloat(value);
                    return true;

                case "shieldPointRestoreRate":
                    shieldPointRestoreRate = new EntityFloat(value);
                    return true;

                case "maxShieldMitigation":
                    maxShieldMitigation = new EntityFloat(value);
                    return true;

                case "armorPoints":
                    armorPoints = new EntityFloat(value);
                    return true;

                case "experiencePointsForDestroying":
                    experiencePointsForDestroying = new EntityFloat(value);
                    return true;

                case "baseMaxAntiMatter":
                    baseMaxAntiMatter = new EntityFloat(value);
                    return true;

                case "baseAntiMatterRestoreRate":
                    baseAntiMatterRestoreRate = new EntityFloat(value);
                    return true;

                default:
                    return false;
            }
        }
    }
}
