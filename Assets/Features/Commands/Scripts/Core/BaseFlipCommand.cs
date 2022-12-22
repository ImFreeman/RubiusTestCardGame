using Assets.Features.Cards.Scripts.Interfaces;
using System.Threading;

namespace Assets.Features.Command
{
    public class BaseFlipCommand : BaseCommand
    {
        protected readonly ICardAnimation _cardAnimation;
        protected readonly ICardView[] _cardViews;
        protected readonly CancellationTokenSource _cancellationTokenSource;
        public BaseFlipCommand(
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
    }
}