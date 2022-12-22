using Assets.Features.Cards.Scripts.Realisation;
using Cysharp.Threading.Tasks;

namespace Assets.Features.Cards.Scripts.Interfaces
{
    public interface ICardAnimation
    {
        UniTask FlipCardAsync(ICardView card, CardSide cardSide);
    }
}