using System;
using System.Reactive.Linq;
using EcsRx.Entities;
using EcsRx.Events;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.Buffs.Components;
using EcsRx.Plugins.Buffs.Events;
using EcsRx.Plugins.Buffs.Models;
using EcsRx.Plugins.ReactiveSystems.Systems;

namespace EcsRx.Plugins.Buffs.Systems
{
    public class EffectAddedSystem : IReactToDataSystem<ActiveEffect>
    {
        public IEventSystem EventSystem { get; }

        public IGroup Group => new Group(typeof(EffectableComponent));

        public EffectAddedSystem(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
        }

        public IObservable<ActiveEffect> ReactToData(IEntity entity)
        {
            return entity.GetComponent<EffectableComponent>()
                .ActiveEffects
                .ObserveAdd()
                .Select(x => x.Value);
        }

        public void Process(IEntity entity, ActiveEffect reactionData)
        {
            var effectAddedEvent = new EffectAddedEvent { ActiveEffect = reactionData, Entity = entity };
            EventSystem.Publish(effectAddedEvent);
        }
    }
}