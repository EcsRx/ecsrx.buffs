using System;
using System.Collections.Generic;
using Assets.EcsRxPlugins.Buffs.Systems;
using EcsRx.Systems;
using EcsRx.Unity.Plugins;
using Zenject;

namespace Assets.EcsRxPlugins.Buffs
{
    public class BuffsPlugin : IEcsRxPlugin
    {
        public string Name { get { return "Buffs Plugin"; } }
        public Version Version { get { return new Version(0,1,0); } }

        public void SetupDependencies(DiContainer container)
        {
            container.Bind<EffectAddedSystem>().ToSelf().AsSingle();
            container.Bind<EffectTimingSystem>().ToSelf().AsSingle();
        }

        public IEnumerable<ISystem> GetSystemForRegistration(DiContainer container)
        {
            return new List<ISystem>
            {
                container.Resolve<EffectAddedSystem>(),
                container.Resolve<EffectTimingSystem>()
            };
        }
    }
}