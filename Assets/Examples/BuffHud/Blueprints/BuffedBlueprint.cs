using Assets.EcsRx.Examples.Database;
using Assets.EcsRxPlugins.Buffs.Components;
using EcsRx.Blueprints;
using EcsRx.Entities;
using EcsRx.Unity.Components;
using EcsRxPlugins.Buffs;

namespace Assets.EcsRx.Examples.Blueprints
{
    public class BuffedBlueprint : IBlueprint
    {
        public EffectDatabase Database { get; private set; }

        public BuffedBlueprint(EffectDatabase database)
        {
            Database = database;
        }

        public void Apply(IEntity entity)
        {
            entity.AddComponent<ViewComponent>();

            var effectableComponent = entity.AddComponent<EffectableComponent>();

            var effectOne = new ActiveEffect {Effect = Database.Effects[0]};
            effectableComponent.ActiveEffects.Add(effectOne);

            var effectTwo = new ActiveEffect { Effect = Database.Effects[1] };
            effectableComponent.ActiveEffects.Add(effectTwo);

            var effectThree = new ActiveEffect { Effect = Database.Effects[2] };
            effectableComponent.ActiveEffects.Add(effectThree);

            var effectFour = new ActiveEffect { Effect = Database.Effects[3] };
            effectableComponent.ActiveEffects.Add(effectFour);
        }
    }
}