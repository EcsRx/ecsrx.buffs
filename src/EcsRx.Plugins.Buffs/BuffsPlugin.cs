using System;
using System.Collections.Generic;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Infrastructure.Plugins;
using EcsRx.Plugins.Buffs.Systems;
using EcsRx.Systems;

namespace EcsRx.Plugins.Buffs
{
    public class BuffsPlugin : IEcsRxPlugin
    {
        public string Name => "Buffs Plugin";
        public Version Version => new Version(0,1,0);

        public void SetupDependencies(IDependencyContainer container)
        {
            container.Bind<EffectAddedSystem>();
            container.Bind<EffectTimingSystem>();
        }

        public IEnumerable<ISystem> GetSystemsForRegistration(IDependencyContainer container)
        {
            return new List<ISystem>
            {
                container.Resolve<EffectAddedSystem>(),
                container.Resolve<EffectTimingSystem>()
            };
        }
    }
}