using Assets.Features.Cards.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Assets.Features.Cards.Scripts.Realisation;
using System.Collections.Generic;

namespace Assets.Features.Command
{    
    public class FlipOnReadyCommand : BaseFlipCommand
    {
        public FlipOnReadyCommand(
            ICardAnimation cardAnimation,
            List<ICardView> cardViews) : base(cardAnimation, cardViews)
        {
        }        

        public override async UniTask<CommandResult> Do()
        {
            var rez = new CommandResult();
            rez.Status = CommandStatus.Success;
            await UniTask.WhenAll(_cardViews.Select(async card =>
            {                
                var requestCommand = new ImageRequestCommand(_cancellationTokenSource.Token);

                await _cardAnimation.FlipCardAsync(card, CardSide.Back);

                var requestResult = await requestCommand.Do();
                if(requestResult.Status == CommandStatus.Failed)
                {
                    rez.Status = CommandStatus.Failed;                    
                }

                card.SetMainPicture((Texture2D)requestResult.Body);
                await _cardAnimation.FlipCardAsync(card, CardSide.Front);
            }));            
            return rez;
        }
    }
}