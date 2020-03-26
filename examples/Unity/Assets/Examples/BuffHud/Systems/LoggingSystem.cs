using System;
using System.Collections.Generic;
using EcsRx.Events;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Plugins.Buffs.Events;
using EcsRx.Systems;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Examples.BuffHud.Systems
{
    public class LoggingSystem : IManualSystem
    {
        private const int MaxLogEntries = 14;
        private readonly IList<IDisposable> _subscriptions;
        private readonly Text _logText;

        private int _logCount;

        public IEventSystem EventSystem { get; set; }

        public IGroup Group => new EmptyGroup();

        public LoggingSystem(IEventSystem eventSystem)
        {
            EventSystem = eventSystem;
            _logText = GameObject.Find("LogText").GetComponent<Text>();
            _subscriptions = new List<IDisposable>();
        }

        public void StartSystem(IObservableGroup group)
        {
            var tickSubscription = EventSystem.Receive<EffectTickedEvent>().Subscribe(x =>
            {
                var logMessage = $"{x.ActiveEffect.Effect.Name} Ticked For {x.ActiveEffect.Effect.Potency}";
                UpdateLog(logMessage);
            });

            var effectAddedSubscription = EventSystem.Receive<EffectAddedEvent>().Subscribe(x =>
            {
                var logMessage = $"{x.ActiveEffect.Effect.Name} Has Been Applied";
                UpdateLog(logMessage);
            });

            var effectRemovedSubscription = EventSystem.Receive<EffectExpiredEvent>().Subscribe(x =>
            {
                var logMessage = $"{x.ActiveEffect.Effect.Name} Has Expired";
                UpdateLog(logMessage);
            });

            _subscriptions.Add(tickSubscription);
            _subscriptions.Add(effectAddedSubscription);
            _subscriptions.Add(effectRemovedSubscription);
        }

        public void UpdateLog(string logNote)
        {
            _logText.text += logNote + "\n";
            _logCount++;

            if (_logCount >= MaxLogEntries)
            {
                _logText.text = "";
                _logCount = 0;
            }
        }

        public void StopSystem(IObservableGroup group)
        {
            _subscriptions.DisposeAll();
        }
    }
}
