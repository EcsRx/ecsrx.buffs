using Assets.EcsRxPlugins.Buffs.Components;
using Assets.EcsRxPlugins.Buffs.Events;
using Assets.EcsRxPlugins.Buffs.Extensions;
using EcsRx.Entities;
using EcsRx.Events;
using EcsRx.Groups;
using EcsRx.Systems;
using UniRx;
using UnityEngine;

namespace Assets.EcsRxPlugins.Buffs.Systems
{
    public class EffectTimingSystem : IReactToGroupSystem
    {
        public IEventSystem EventSystem { get; private set; }

        public IGroup TargetGroup
        {
            get { return new Group(typeof(EffectableComponent)); }
        }

        public EffectTimingSystem(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
        }

        public IObservable<IGroupAccessor> ReactToGroup(IGroupAccessor @group)
        { return Observable.EveryUpdate().Select(x => @group); }

        public void Execute(IEntity entity)
        {
            var effectableComponent = entity.GetComponent<EffectableComponent>();
            var deltaTimeInMilliseconds = Time.deltaTime * 1000;
            
            for (var i = effectableComponent.ActiveEffects.Count-1; i >= 0; i--)
            {
                var activeBuff = effectableComponent.ActiveEffects[i];
                activeBuff.ActiveTime += deltaTimeInMilliseconds;
                activeBuff.TimeSinceTick += deltaTimeInMilliseconds;

                if (activeBuff.ActiveTime >= activeBuff.Effect.Duration)
                {
                    effectableComponent.ActiveEffects.RemoveAt(i);

                    var effectExpiredEvent = new EffectExpiredEvent { ActiveEffect = activeBuff, Entity = entity };
                    EventSystem.Publish(effectExpiredEvent);
                }
                
                if (activeBuff.Effect.Frequency > 0 && activeBuff.TimeSinceTick >= activeBuff.Effect.Frequency)
                {
                    activeBuff.TimeSinceTick -= activeBuff.Effect.Frequency;
                    var tickCount = activeBuff.TicksSoFar();

                    var effectExpiredEvent = new EffectTickedEvent
                    {
                        ActiveEffect = activeBuff,
                        Entity = entity,
                        TickNumber = tickCount
                    };
                    EventSystem.Publish(effectExpiredEvent);
                }
            }
        }
    }
}