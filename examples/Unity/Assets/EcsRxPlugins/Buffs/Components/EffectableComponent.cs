using System;
using EcsRx.Components;
using EcsRxPlugins.Buffs;
using UniRx;

namespace Assets.EcsRxPlugins.Buffs.Components
{
    public class EffectableComponent : IComponent, IDisposable
    {
        public ReactiveCollection<ActiveEffect> ActiveEffects { get; private set; }

        public EffectableComponent()
        {
            ActiveEffects = new ReactiveCollection<ActiveEffect>();
        }

        public void Dispose()
        {
            ActiveEffects.Dispose();
        }
    }
}