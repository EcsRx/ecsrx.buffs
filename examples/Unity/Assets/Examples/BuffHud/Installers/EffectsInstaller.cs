using Assets.EcsRx.Examples.Database;
using Zenject;

namespace Assets.EcsRx.Examples.Installers
{
    public class EffectsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EffectDatabase>().ToSelf().AsSingle();
        }
    }
}