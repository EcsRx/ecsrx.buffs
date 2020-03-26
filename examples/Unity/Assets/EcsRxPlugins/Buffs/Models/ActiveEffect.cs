namespace EcsRxPlugins.Buffs
{
    public class ActiveEffect
    {
        public Effect Effect { get; set; }
        public float ActiveTime { get; set; }
        public float TimeSinceTick { get; set; }
    }
}