using Assets.EcsRxPlugins.Buffs.Components;
using Assets.EcsRxPlugins.Buffs.Events;
using EcsRx.Entities;
using EcsRx.Events;
using EcsRx.Groups;
using EcsRx.Systems;
using EcsRxPlugins.Buffs;
using UniRx;

namespace Assets.EcsRxPlugins.Buffs.Systems
{
    public class EffectAddedSystem : IReactToDataSystem<ActiveEffect>
    {
        public IEventSystem EventSystem { get; private set; }

        public IGroup TargetGroup
        {
            get { return new Group(typeof(EffectableComponent)); }
        }

        public EffectAddedSystem(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
        }

        public IObservable<ActiveEffect> ReactToData(IEntity entity)
        {
            var effectableComponent = entity.GetComponent<EffectableComponent>();
            return effectableComponent.ActiveEffects.ObserveAdd().Select(x => x.Value);
        }

        public void Execute(IEntity entity, ActiveEffect reactionData)
        {
            var effectAddedEvent = new EffectAddedEvent { ActiveEffect = reactionData, Entity = entity };
            EventSystem.Publish(effectAddedEvent);
        }
    }
}