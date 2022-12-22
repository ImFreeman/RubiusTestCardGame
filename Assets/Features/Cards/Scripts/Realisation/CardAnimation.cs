using Assets.Features.Cards.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Assets.Features.Cards.Scripts.Realisation
{        
    public class CardAnimation : ICardAnimation
    {
        private readonly CardAnimationConfig _config;
        public CardAnimation(CardAnimationConfig config)
        {
            _config = config;
        }

        public async UniTask FlipCardAsync(ICardView card, CardSide cardSide)
        {
            if (card.CurrentCardSide == cardSide)
            {
                return;
            }            

            await FlipCard(card.BodyTransform, _config.Rotation, _config.Speed, _config.Ease);
            card.SetCardSide(cardSide);
            await FlipCard(card.BodyTransform, Vector3.zero, _config.Speed, _config.Ease);
        }

        private async UniTask FlipCard(Transform cardTransform, Vector3 rotation, float speed, Ease ease)
        {
            await DOTween.Sequence()
               .Join(cardTransform.DORotate(rotation, speed))                              
               .SetEase(ease)
               .AsyncWaitForCompletion();
        }
    }
}