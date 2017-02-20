using Assets.EcsRx.Examples.Blueprints;
using Assets.EcsRx.Examples.Database;
using Assets.EcsRxPlugins.Buffs;
using EcsRx.Unity;
using Zenject;

namespace Assets.EcsRx.Examples
{
    public class Application : EcsRxApplication
    {
        [Inject]
        public EffectDatabase Database { get; private set; }

        public Application(EffectDatabase database)
        {
            Database = database;
        }

        protected override void ApplicationStarting()
        {
            RegisterPlugin(new BuffsPlugin());
            RegisterAllBoundSystems();
        }

        protected override void ApplicationStarted()
        {
            var defaultPool = PoolManager.GetPool();

            var buffBlueprint = new BuffedBlueprint(Database);
            var buffedEntity = defaultPool.CreateEntity(buffBlueprint);
        }
    }
}