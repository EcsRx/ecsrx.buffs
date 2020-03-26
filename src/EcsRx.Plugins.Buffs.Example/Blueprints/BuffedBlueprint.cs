using System;
using System.Threading.Tasks;
using EcsRx.Blueprints;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Plugins.Buffs.Components;
using EcsRx.Plugins.Buffs.Example.Database;
using EcsRx.Plugins.Buffs.Models;

namespace EcsRx.Plugins.Buffs.Example.Blueprints
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
            // Add our effectable component from the Buffs plugin
            var effectableComponent = entity.AddComponent<EffectableComponent>();

            // Add in some buffs to see them be applied and automatically run for a duration
            var effectOne = new ActiveEffect {Effect = Database.GetEffectByName("Poison I") };
            effectableComponent.ActiveEffects.Add(effectOne);

            var effectTwo = new ActiveEffect { Effect = Database.GetEffectByName("Iron Skin") };
            effectableComponent.ActiveEffects.Add(effectTwo);

            var effectThree = new ActiveEffect { Effect = Database.GetEffectByName("Nightvision") };
            effectableComponent.ActiveEffects.Add(effectThree);

            var effectFour = new ActiveEffect { Effect = Database.GetEffectByName("Regen") };
            effectableComponent.ActiveEffects.Add(effectFour);
        }
    }
}