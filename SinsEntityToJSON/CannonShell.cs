using System.Diagnostics;

namespace SinsEntityConverter
{
    public class CannonShellDefinition : Def
    {
        public string? Header = "TXT2";
        public EntityInt? SinsArchiveVersion;
        public string? entityType;
        public EntityFloat? acceleration;
        public EntityFloat? startingSpeed;
        public EntityFloat? maxSpeed;
        public string? buffToApplyOnImpact;
        public string? planetImpactEffectName;
        public PlanetImpactSounds? planetImpactSounds;
        public EntityFloat? planetImpactCameraShakeMaxSize;
        public EntityFloat? planetImpactCameraShakeStrength;
        public EntityFloat? planetImpactCameraShakeDuration;
        public string? meshName;
        public string? mainViewIcon;
        public string? picture;
        public string? nameStringID;
        public string? descStringID;
        public EntityBool? renderMesh;
        public EntityFloat? minZoomDistanceMult;
        public string? hudIcon;
        public string? smallHudIcon;
        public string? infoCardIcon;
        public EntityFloat? minShadow;
        public EntityFloat? maxShadow;


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

                case "acceleration":
                    acceleration = new EntityFloat(value);
                    return true;

                case "startingSpeed":
                    startingSpeed = new EntityFloat(value);
                    return true;

                case "maxSpeed":
                    maxSpeed = new EntityFloat(value);
                    return true;

                case "buffToApplyOnImpact":
                    buffToApplyOnImpact = value;
                    return true;

                case "planetImpactEffectName":
                    planetImpactEffectName = value;
                    return true;

                case "planetImpactSounds":
                    planetImpactSounds = new();
                    return true;

                case "soundCount":
                    planetImpactSounds!.soundCount = new EntityInt(value);
                    return true;

                case "sound":
                    planetImpactSounds!.sound ??= new();
                    planetImpactSounds!.sound.Add(value);
                    return true;

                case "planetImpactCameraShakeMaxSize":
                    planetImpactCameraShakeMaxSize = new EntityFloat(value);
                    return true;

                case "planetImpactCameraShakeStrength":
                    planetImpactCameraShakeStrength = new EntityFloat(value);
                    return true;

                case "planetImpactCameraShakeDuration":
                    planetImpactCameraShakeDuration = new EntityFloat(value);
                    return true;

                case "meshName":
                    meshName = value;
                    return true;

                case "mainViewIcon":
                    mainViewIcon = value;
                    return true;

                case "picture":
                    picture = value;
                    return true;

                case "nameStringID":
                    nameStringID = value;
                    return true;

                case "descStringID":
                    descStringID = value;
                    return true;

                case "renderMesh":
                    renderMesh = new EntityBool(value);
                    return true;

                case "minZoomDistanceMult":
                    minZoomDistanceMult = new EntityFloat(value);
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

    public class PlanetImpactSounds
    {
        public EntityInt? soundCount;
        public List<string>? sound;
    }
}
