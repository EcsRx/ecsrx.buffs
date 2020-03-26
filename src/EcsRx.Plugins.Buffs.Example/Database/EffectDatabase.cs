using System.Collections.Generic;
using System.Linq;
using EcsRx.Plugins.Buffs.Example.Types;
using EcsRx.Plugins.Buffs.Models;

namespace EcsRx.Plugins.Buffs.Example.Database
{
    public class EffectDatabase
    {
        public IList<Effect> Effects { get; }

        public Effect GetEffectByName(string name)
        { return Effects.SingleOrDefault(x => x.Name == name); }
        
        public EffectDatabase()
        {
            Effects = new List<Effect>
            {
                new Effect
                {
                    Id = 1, Name = "Poison I", Duration = 10000, Frequency = 1000, 
                    Type = (short) EffectTypes.ReduceHealth, Potency = 2.0f,
                    Description = "Makes the target lose a small amount of health every second"
                },
                new Effect
                {
                    Id = 2, Name = "Poison II", Duration = 20000, Frequency = 1000, 
                    Type = (short) EffectTypes.ReduceHealth, Potency = 4.0f,
                    Description = "Makes the target lose a large amount of health every second"
                },
                new Effect
                {
                    Id = 3, Name = "Iron Skin", Duration = 15000,
                    Type = (short) EffectTypes.AddArmour, Potency = 5.0f,
                    Description = "Become tougher than nails"
                },
                new Effect
                {
                    Id = 4, Name = "Nightvision", Duration = 600000, 
                    Type = (short) EffectTypes.AddViewDistance, Potency = 100.0f,
                    Description = "Lets you see in the dark for a period"
                },
                new Effect
                {
                    Id = 5, Name = "Regen", Duration = 20000, Frequency = 3000, 
                    Type = (short) EffectTypes.AddHealth, Potency = 10.0f,
                    Description = "Recover a small amount of health every few seconds"
                }
            };
        }
    }
}