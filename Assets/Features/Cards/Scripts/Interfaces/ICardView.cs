using Assets.Features.Cards.Scripts.Realisation;
using UnityEngine;

namespace Assets.Features.Cards.Scripts.Interfaces
{    
    public interface ICardView
    {
        public RectTransform BodyTransform { get; }                
        public CardSide CurrentCardSide { get; }
        public void SetMainPicture(Texture2D texture);
        public void SetCardSide(CardSide side);
    }    
}