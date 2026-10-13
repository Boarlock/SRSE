
namespace SinsEntityConverter
{
    public class CapshipDefinition : Def
    {
        public ArmorPointsFromExperience? ArmorPointsFromExperience;
        public CultureProtectRate? CultureProtectRate;
        public CommandPoints? CommandPoints;
        public weaponCooldownDecreasePerc? weaponCooldownDecreasePerc;
        public weaponDamageIncreasePerc? weaponDamageIncreasePerc;

        public MaxAntiMatterLevels? MaxAntiMatterLevels;
        public AntiMatterRestoreRateLevels? AntiMatterRestoreRateLevels;

        public string? roleType;
        public Effects? effects;
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
}
