using EcsRx.Entities;
using EcsRxPlugins.Buffs;

namespace Assets.EcsRxPlugins.Buffs.Events
{
    public class EffectTickedEvent
    {
        public IEntity Entity { get; set; }
        public ActiveEffect ActiveEffect { get; set; }
        public int TickNumber { get; set; }
    }
}