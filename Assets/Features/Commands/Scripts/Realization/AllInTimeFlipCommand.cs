using Assets.Features.Cards.Scripts.Interfaces;
using Assets.Features.Cards.Scripts.Realisation;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Features.Command
{
    public class AllInTimeFlipCommand : BaseFlipCommand
    {
        public AllInTimeFlipCommand(
            ICardAnimation cardAnimation, 
            List<ICardView> cardViews) : base(cardAnimation, cardViews)
        {
        }

        public override async UniTask<CommandResult> Do()
        {
            var rez = new CommandResult();
            rez.Status = CommandStatus.Success;
            var downloadAndFlipBack = _cardViews.Select(async card =>
            {                
                var requestCommand = new ImageRequestCommand(_cancellationTokenSource.Token);

                await _cardAnimation.FlipCardAsync(card, CardSide.Back);
                var requestResult = await requestCommand.Do();
                card.SetMainPicture((Texture2D)requestResult.Body);
            });
            await UniTask.WhenAll(downloadAndFlipBack);
            if(rez.Status == CommandStatus.Failed)
            {
                return rez;
            }
            await UniTask.WhenAll(_cardViews.Select(card => _cardAnimation.FlipCardAsync(card, CardSide.Front)));
            return rez;
        }
    }
}