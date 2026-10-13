
namespace SinsEntityConverter
{
    public class PlanetModuleDefinition : Def
    {
        public string? planetUpgradeSlotType;
        public EntityFloat? planetUpgradeSlotCount;
        public string? planetModuleRoleType;

        public string? resourceExtractionType;
        public EntityFloat? cultureSpreadRate;
        public EntityBool? isAffectedBySimilarModuleCostResearch;
        public EntityFloat? placementRadius;
        public EntityFloat? nearPlacementDistOffset;
        public EntityFloat? farPlacementDistOffset;
        public EntityInt? spawnCount;
        public string? swapMineType;
        public EntityFloat? angularSpeed;
        public string? rotateSoundName;
        public string? rotateFacingType;
        public EntityBool? rotateConstantly;
        public EntityFloat? baseBuildTime;
        public string? buildEffectName;
        public EntityInt? baseCommandPoints;

        public string? cargoShipType;
        public EntityInt? maxCargoShipCount;
        public EntityFloat? cargoShipRespawnTime;
        public EntityFloat? baseTradeIncomeRate;
    }
}
