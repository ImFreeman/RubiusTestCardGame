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

        public CardViewProtocol(string name, string discription, Sprite faceSprite, Sprite backSprite)
        {
            Name = name;
            Discription = discription;
            FrontSprite = faceSprite;
            BackSprite = backSprite;
        }        
    }
    public class CardViewFactory : PlaceholderFactory<CardViewProtocol, CardView>
    {        
    }
}