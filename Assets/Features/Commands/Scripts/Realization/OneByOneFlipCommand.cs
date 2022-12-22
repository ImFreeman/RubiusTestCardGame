using Assets.Features.Cards.Scripts.Interfaces;
using Assets.Features.Cards.Scripts.Realisation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Features.Command
{
    public class OneByOneFlipCommand : BaseFlipCommand
    {
        public OneByOneFlipCommand(
            ICardAnimation cardAnimation,
            ICardView[] cardViews) : base(cardAnimation, cardViews)
        {
        }

        public override async UniTask<CommandResult> Do()
        {
            await UniTask.WhenAll(_cardViews.Select(async card =>
            {                
                var requestCommand = new ImageRequestCommand(_cancellationTokenSource.Token);

                await _cardAnimation.FlipCardAsync(card, CardSide.Back);
                var requestResult = await requestCommand.Do();
                card.SetMainPicture((Texture2D)requestResult.Body);
            }));

            foreach (var card in _cardViews)
            {
                if(_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    break;
                }    
                await _cardAnimation.FlipCardAsync(card, CardSide.Front);
            }
            return new CommandResult();
        }
    }
}