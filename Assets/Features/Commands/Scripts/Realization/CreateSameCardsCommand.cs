using Assets.Features.Cards.Scripts.Interfaces;
using Assets.Features.Cards.Scripts.Realisation;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Features.Command
{
    public readonly struct CreateSameCardsCommandProtocol
    {
        public readonly Transform Container;
        public readonly int Number;
        public readonly int ModelID;
        public CreateSameCardsCommandProtocol(Transform container, int number, int modelID)
        {
            Container = container;
            Number = number;
            ModelID = modelID;
        }
    }

    public class CreateSameCardsCommand : BaseCommand
    {
        private readonly CardViewFactory _cardViewFactory;
        private readonly ICardAnimation _cardAnimation;
        private readonly CardConfig _config;
        private readonly Transform _container;
        private readonly int _number;
        private readonly int _modelID;
        public CreateSameCardsCommand(
            CardViewFactory cardViewFactory,
            CardConfig config,
            ICardAnimation cardAnimation,
            CreateSameCardsCommandProtocol protocol)
        {
            _cardViewFactory = cardViewFactory;
            _container = protocol.Container;
            _number = protocol.Number;
            _config = config;
            _modelID = protocol.ModelID;
            _cardAnimation = cardAnimation;
        }

        public override async UniTask<CommandResult> Do()
        {
            var rez = new CommandResult();
            var model = _config.Get(_modelID);
            if (!model.HasValue)
            {
                rez.Status = CommandStatus.Failed;
                return rez;
            }
            rez.Status = CommandStatus.Success;
            var views = new List<ICardView>();
            var tasks = new List<UniTask>();

            for (int i = 0; i < _number; i++)
            {
                tasks.Add(UniTask.Create(async () =>
                {
                    var view = await GetCardView(model.Value);
                    views.Add(view);
                    _cardAnimation.FlipCardAsync(view, CardSide.Back).Forget();
                }));
            }
            await UniTask.WhenAll(tasks);
            tasks.Clear();
            rez.Body = views;
            return rez;
        }

        private async UniTask<ICardView> GetCardView(CardModel model)
        {            
            ICardView view =  _cardViewFactory.Create(
                            new CardViewProtocol(
                                model.Name,
                                model.Discription,
                                model.FaceSprite,
                                model.BackSprite,
                                _container));
            return view;
        }
    }
}