using System;
using System.Collections.Generic;
using EcsRx.Events;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Plugins.Buffs.Events;
using EcsRx.Systems;

namespace EcsRx.Plugins.Buffs.Example.Systems
{
    public class LoggingSystem : IManualSystem
    {
        private readonly IList<IDisposable> _subscriptions;

        public IEventSystem EventSystem { get; }
        public IGroup Group => new EmptyGroup();

        public LoggingSystem(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
            _subscriptions = new List<IDisposable>();
        }

        public void StartSystem(IObservableGroup group)
        {
            EventSystem.Receive<EffectTickedEvent>()
                .Subscribe(x => 
                    Console.WriteLine($"{x.ActiveEffect.Effect.Name} Ticked For {x.ActiveEffect.Effect.Potency}"))
                .AddTo(_subscriptions);

            EventSystem.Receive<EffectAddedEvent>()
                .Subscribe(x => 
                    Console.WriteLine($"{x.ActiveEffect.Effect.Name} Has Been Applied"))
                .AddTo(_subscriptions);

            EventSystem.Receive<EffectExpiredEvent>()
                .Subscribe(x =>
                    Console.WriteLine($"{x.ActiveEffect.Effect.Name} Has Expired"))
                .AddTo(_subscriptions);
        }
        
        public void StopSystem(IObservableGroup group)
        {
            _subscriptions.DisposeAll();
        }
    }
}
