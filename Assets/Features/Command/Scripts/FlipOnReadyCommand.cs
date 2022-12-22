using Assets.Features.Cards.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Assets.Features.Cards.Scripts.Realisation;

namespace Assets.Features.Command.Scripts
{    
    public class FlipOnReadyCommand : Command
    {
        private readonly ICardAnimation _cardAnimation;
        private readonly ICardView[] _cardViews;
        private readonly CancellationTokenSource _cancellationTokenSource;       
        public FlipOnReadyCommand(
            ICardAnimation cardAnimation,
            ICardView[] cardViews)
        {
            _cardAnimation = cardAnimation;
            _cardViews = cardViews;
            _cancellationTokenSource = new CancellationTokenSource(); 
        }

        public override void Cancel()
        {
            _cancellationTokenSource.Cancel();
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