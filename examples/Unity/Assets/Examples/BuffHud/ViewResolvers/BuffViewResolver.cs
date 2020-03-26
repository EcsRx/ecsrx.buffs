using System;
using System.Collections.Generic;
using Assets.EcsRxPlugins.Buffs.Components;
using Assets.Examples.BuffHud.Bindings;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Systems;
using EcsRx.Unity.Components;
using EcsRx.Unity.Systems;
using EcsRxPlugins.Buffs;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Examples.BuffHud.ViewResolvers
{
    public class BuffUISystem : ISetupSystem, ITeardownSystem
    {
        private readonly IGroup _targetGroup = new Group(typeof(EffectableComponent), typeof(ViewComponent));

        private readonly GameObject _buffPrefab;
        private readonly GameObject _buffPanelGameObject;

        private readonly IDictionary<IEntity, IDisposable> _entitySubscriptions;
        private readonly IDictionary<ActiveEffect, IDisposable> _effectSubscription;

        public IGroup TargetGroup
        {
            get { return _targetGroup; }
        }

        public BuffUISystem()
        {
            _entitySubscriptions = new Dictionary<IEntity, IDisposable>();
            _effectSubscription = new Dictionary<ActiveEffect, IDisposable>();

            _buffPrefab = (GameObject)Resources.Load("Prefabs/Buff");
            _buffPanelGameObject = GameObject.Find("BuffPanel");
        }

        public void Setup(IEntity entity)
        {
            var effectableComponent = entity.GetComponent<EffectableComponent>();
            var additionSubscription = effectableComponent.ActiveEffects.ObserveAdd().Subscribe(EntityAdded);
            var removeSubscription = effectableComponent.ActiveEffects.ObserveRemove().Subscribe(EntityRemove);
            _entitySubscriptions.Add(entity, new CompositeDisposable(additionSubscription, removeSubscription));

            foreach (var effect in effectableComponent.ActiveEffects)
            { CreateViewForEffect(effect); }
        }

        public void Teardown(IEntity entity)
        {
            var effectableComponent = entity.GetComponent<EffectableComponent>();
            effectableComponent.ActiveEffects.ForEachRun(x => _effectSubscription[x].Dispose());
            _entitySubscriptions[entity].Dispose();

        }

        public void BindView(GameObject view, ActiveEffect effect)
        {
            var buffImage = view.transform.FindChild("Image").GetComponent<Image>();
            var buffText = view.transform.FindChild("Text").GetComponent<Text>();

            var updateSubscription = Observable.EveryUpdate().Subscribe(x =>
            {
                var timeLeft = effect.Effect.Duration - effect.ActiveTime;
                var timeSpan = TimeSpan.FromMilliseconds(timeLeft);
                buffText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            });

            _effectSubscription.Add(effect, updateSubscription);
        }

        public GameObject ResolveView(ActiveEffect effect)
        {
            var gameObject = Object.Instantiate(_buffPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            gameObject.name = string.Format("buff-{0}", effect.Effect.Id);
            return gameObject;
        }

        public void CreateViewForEffect(ActiveEffect effect)
        {
            var effectView = ResolveView(effect);
            effectView.transform.SetParent(_buffPanelGameObject.transform);

            BindView(effectView, effect);
        }

        public void RemoveViewForEffect(ActiveEffect effect)
        {
            var effectGoName = string.Format("buff-{0}", effect.Effect.Id);
            var effectGo = _buffPanelGameObject.transform.FindChild(effectGoName);
            Object.Destroy(effectGo.gameObject);
        }

        public void EntityAdded(CollectionAddEvent<ActiveEffect> collectionAddEvent)
        {
            CreateViewForEffect(collectionAddEvent.Value);
        }

        public void EntityRemove(CollectionRemoveEvent<ActiveEffect> collectionRemoveEvent)
        {
            _effectSubscription[collectionRemoveEvent.Value].Dispose();
            _effectSubscription.Remove(collectionRemoveEvent.Value);
            RemoveViewForEffect(collectionRemoveEvent.Value);
        }
    }
}
