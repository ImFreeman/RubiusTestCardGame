using Zenject;
using Assets.Features.Cards.Scripts.Realisation;

namespace Assets.Features.Game.Scripts
{
    public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
    {
        public override void InstallBindings()
        {
            CardInstaller.Install(Container);

            Container
                .Bind<ApplicationStartup>()
                .AsSingle()
                .NonLazy();
        }
    }
}