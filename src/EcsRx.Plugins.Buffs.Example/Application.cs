using EcsRx.Infrastructure;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Ninject;
using EcsRx.Plugins.Buffs.Example.Blueprints;
using EcsRx.Plugins.Buffs.Example.Database;
using EcsRx.Plugins.ReactiveSystems;

namespace EcsRx.Plugins.Buffs.Example
{
    public class Application : EcsRxApplication
    {
        public override IDependencyContainer Container { get; } = new NinjectDependencyContainer();

        public EffectDatabase Database { get; } = new EffectDatabase();

        protected override void LoadPlugins()
        {
            base.LoadPlugins();
            RegisterPlugin(new ReactiveSystemsPlugin());
            RegisterPlugin(new BuffsPlugin());
        }

        protected override void ApplicationStarted()
        {
            var defaultCollection = EntityDatabase.GetCollection();
            var buffBlueprint = new BuffedBlueprint(Database);
            defaultCollection.CreateEntity(buffBlueprint);
        }

    }
}