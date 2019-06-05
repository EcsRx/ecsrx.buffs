using System;
using EcsRx.Components;
using EcsRx.Plugins.Buffs.Models;
using EcsRx.ReactiveData.Collections;

namespace EcsRx.Plugins.Buffs.Components
{
    public class EffectableComponent : IComponent, IDisposable
    {
        public ReactiveCollection<ActiveEffect> ActiveEffects { get; }

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