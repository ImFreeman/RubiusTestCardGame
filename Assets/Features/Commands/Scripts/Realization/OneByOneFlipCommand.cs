using Assets.Features.Cards.Scripts.Interfaces;
using Assets.Features.Cards.Scripts.Realisation;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Features.Command
{
    public class OneByOneFlipCommand : BaseFlipCommand
    {
        public OneByOneFlipCommand(
            ICardAnimation cardAnimation,
            List<ICardView> cardViews) : base(cardAnimation, cardViews)
        {
        }

        public override async UniTask<CommandResult> Do()
        {
            var rez = new CommandResult();
            await UniTask.WhenAll(_cardViews.Select(async card =>
            {                
                var requestCommand = new ImageRequestCommand(_cancellationTokenSource.Token);

                await _cardAnimation.FlipCardAsync(card, CardSide.Back);
                var requestResult = await requestCommand.Do();                

                if (requestResult.Status == CommandStatus.Failed)
                {
                    rez.Status = CommandStatus.Failed;                    
                }
                card.SetMainPicture((Texture2D)requestResult.Body);
            }));            
            foreach (var card in _cardViews)
            {
                if(_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    rez.Status = CommandStatus.Failed;
                    break;
                }    
                await _cardAnimation.FlipCardAsync(card, CardSide.Front);
            }
            return rez;
        }
    }
}