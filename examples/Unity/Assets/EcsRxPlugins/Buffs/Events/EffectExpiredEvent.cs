using EcsRx.Entities;
using EcsRxPlugins.Buffs;

namespace Assets.EcsRxPlugins.Buffs.Events
{
    public class EffectExpiredEvent
    {
        public IEntity Entity { get; set; }
        public ActiveEffect ActiveEffect { get; set; }
    }
}