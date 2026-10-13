
namespace SinsEntityConverter
{
    public class MineDefinition : Def
    {
        public string? MeshName;
        public EntityFloat? spawnActivationDelay;
        public EntityFloat? targetStartFollowRange;
        public EntityFloat? targetMaxFollowRange;
        public BasePrice? scuttleValue;
        public EntityFloat? baseScuttleTime;

        public EntityFloat? maxShieldPoints;
        public EntityFloat? shieldPointRestoreRate;
        public EntityFloat? baseMaxAntiMatter;
        public EntityFloat? baseAntiMatterRestoreRate;
    }
}
