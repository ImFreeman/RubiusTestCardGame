using Assets.Features.Cards.Scripts.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace Assets.Features.Command
{
    public class BaseFlipCommand : BaseCommand
    {
        protected readonly ICardAnimation _cardAnimation;
        protected readonly List<ICardView> _cardViews;
        protected readonly CancellationTokenSource _cancellationTokenSource;
        public BaseFlipCommand(
            ICardAnimation cardAnimation,
            List<ICardView> cardViews)
        {
            _cardAnimation = cardAnimation;
            _cardViews = cardViews;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public override void Dispose()
        {
            _cancellationTokenSource.Cancel();           
        }
        public override void Cancel()
        {
            Dispose();            
        }
    }
}