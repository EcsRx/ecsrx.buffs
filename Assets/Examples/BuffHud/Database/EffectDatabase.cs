using System.Collections;
using System.Collections.Generic;
using EcsRxPlugins.Buffs;

namespace Assets.EcsRx.Examples.Database
{
    public class EffectDatabase
    {
        public IList<Effect> Effects { get; private set; }

        public EffectDatabase()
        {
            Effects = new List<Effect>();

            Effects.Add(new Effect
            {
                Id = 1,
                Name = "Poison",
                Duration = 30000,
                Frequency = 1000
            });

            Effects.Add(new Effect
            {
                Id = 2,
                Name = "Iron Skin",
                Duration = 15000
            });

            Effects.Add(new Effect
            {
                Id = 3,
                Name = "Nightvision",
                Duration = 600000
            });

            Effects.Add(new Effect
            {
                Id = 4,
                Name = "Regen",
                Duration = 30000,
                Frequency = 3000
            });
        }
    }
}