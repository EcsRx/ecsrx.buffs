using EcsRx.Entities;
using EcsRx.Plugins.Buffs.Models;

namespace EcsRx.Plugins.Buffs.Events
{
    public class EffectTickedEvent
    {
        public IEntity Entity { get; set; }
        public ActiveEffect ActiveEffect { get; set; }
        public int TickNumber { get; set; }
    }
}