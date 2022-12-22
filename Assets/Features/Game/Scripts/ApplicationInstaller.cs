using Zenject;
using UnityEngine;
using Assets.Features.Cards.Scripts.Realisation;

namespace Assets.Features.Game.Scripts
{
    public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
    {
        [SerializeField] private int cardsModelID;
        [SerializeField] private int numberOfCards;
        public override void InstallBindings()
        {
            CardInstaller.Install(Container);

            Container
                .Bind<ApplicationStartup>()
                .AsSingle()
                .WithArguments(new object[] {new ApplicationStartupProtocol(cardsModelID, numberOfCards)})                
                .NonLazy();
        }
    }
}