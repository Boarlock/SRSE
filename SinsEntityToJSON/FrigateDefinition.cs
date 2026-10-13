
namespace SinsEntityConverter
{
    public class FrigateDefinition : Def
    {
        public EntityInt? typeCount;
        public string? frigateRoleType;
        public EntityBool? hasLevels;

        public EntityFloat? relationshipChangeRate;
        public relationshipChangeRateLevels? RelationshipLevels;

        public EntityFloat? firingTiltAngle;
        public EntityBool? hasWeaponLevels;

        public weaponDamageMultLevels? weaponDamageMult;
        public weaponCooldownMultLevels? weaponCooldownMult;

        public EntityInt? maxNumCommandPoints;
        public string? cargoType;
        public EntityFloat? maxCargoCapacity;
        public EntityFloat? allegianceDecreasePerRoundtrip;
    }
}
