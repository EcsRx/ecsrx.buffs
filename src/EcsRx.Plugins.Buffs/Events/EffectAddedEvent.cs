using EcsRx.Entities;
using EcsRx.Plugins.Buffs.Models;

namespace EcsRx.Plugins.Buffs.Events
{
    public class EffectAddedEvent
    {
        public IEntity Entity { get; set; }
        public ActiveEffect ActiveEffect { get; set; }
    }
}