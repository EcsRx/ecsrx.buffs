using Assets.EcsRx.Examples.Blueprints;
using Assets.EcsRx.Examples.Database;
using EcsRx.Plugins.Buffs;
using EcsRx.Zenject;
using Zenject;

namespace Assets.EcsRx.Examples
{
    public class Application : EcsRxApplicationBehaviour
    {
        [Inject]
        public EffectDatabase Database { get; private set; }

        public Application(EffectDatabase database)
        {
            Database = database;
        }

        protected override void LoadPlugins()
        {
            base.LoadPlugins();
            RegisterPlugin(new BuffsPlugin());
        }

        protected override void ApplicationStarted()
        {
            var collection = EntityDatabase.GetCollection();

            var buffBlueprint = new BuffedBlueprint(Database);
            var buffedEntity = collection.CreateEntity(buffBlueprint);
        }
    }
}