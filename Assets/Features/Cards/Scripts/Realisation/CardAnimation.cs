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

        public async UniTask FlipCardAsync(ICardView card, CardSide cardSide, int animID)
        {
            if (card.CurrentCardSide == cardSide)
            {
                return;
            }

            var animModel = _config.Get(animID).Value;

            await FlipCard(card.BodyTransform, animModel.Rotation, animModel.Speed, animModel.Ease);
            card.SetCardSide(cardSide);
            await FlipCard(card.BodyTransform, Vector3.zero, animModel.Speed, animModel.Ease);
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