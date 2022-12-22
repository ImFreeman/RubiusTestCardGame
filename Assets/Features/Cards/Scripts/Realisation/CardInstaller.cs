using Assets.Features.Cards.Scripts.Interfaces;
using Zenject;

namespace Assets.Features.Cards.Scripts.Realisation
{
    public class CardInstaller : Installer<CardInstaller>
    {
        private const string PrefabPath = "Card/CardPrefab";
        private const string AnimeCardConfigPath = "Card/CardAnimationConfig";
        private const string CardConfigPath = "Card/CardConfig";
        public override void InstallBindings()
        {
            Container
                .Bind<CardConfig>()
                .FromScriptableObjectResource(CardConfigPath)
                .AsSingle();
            Container
                .Bind<CardAnimationConfig>()
                .FromScriptableObjectResource(AnimeCardConfigPath)
                .AsSingle();
            Container
                .Bind<ICardAnimation>()
                .To<CardAnimation>()
                .AsSingle();
            Container
                .BindFactory<CardViewProtocol, CardView, CardViewFactory>()
                .FromComponentInNewPrefabResource(PrefabPath)
                .AsSingle();
        }
    }
}