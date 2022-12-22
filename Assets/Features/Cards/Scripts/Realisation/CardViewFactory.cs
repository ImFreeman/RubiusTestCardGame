using Zenject;
using UnityEngine;

namespace Assets.Features.Cards.Scripts.Realisation
{
    public readonly struct CardViewProtocol
    {
        public readonly string Name;
        public readonly string Discription;
        public readonly Sprite FrontSprite;
        public readonly Sprite BackSprite;
        public readonly Transform Container;

        public CardViewProtocol(
            string name,
            string discription,
            Sprite faceSprite,
            Sprite backSprite,
            Transform container)
        {
            Name = name;
            Discription = discription;
            FrontSprite = faceSprite;
            BackSprite = backSprite;
            Container = container;
        }
    }
    public class CardViewFactory : PlaceholderFactory<CardViewProtocol, CardView>
    {        
    }
}