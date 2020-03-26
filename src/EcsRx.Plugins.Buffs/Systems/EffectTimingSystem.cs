using System;
using EcsRx.Entities;
using EcsRx.Events;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.Buffs.Components;
using EcsRx.Plugins.Buffs.Events;
using EcsRx.Plugins.Buffs.Extensions;
using EcsRx.Scheduling;
using EcsRx.Systems;

namespace EcsRx.Plugins.Buffs.Systems
{
    public class EffectTimingSystem : IBasicSystem
    {
        public IEventSystem EventSystem { get; }
        public ITimeTracker TimeTracker { get; }

        public IGroup Group => new Group(typeof(EffectableComponent));
        

        public EffectTimingSystem(IEventSystem eventSystem, ITimeTracker timeTracker)
        {
            EventSystem = eventSystem;
            TimeTracker = timeTracker;
        }
        
        public void Process(IEntity entity)
        {
            var effectableComponent = entity.GetComponent<EffectableComponent>();
            var deltaTimeInMilliseconds = TimeTracker.ElapsedTime.DeltaTime.Milliseconds;
            
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