using Zenject;
using Assets.Features.Cards.Scripts.Interfaces;
using System.Collections.Generic;
using Assets.Features.Command;
using System;

namespace Assets.Features.UI.Scripts
{
    public readonly struct UIControllerProtocol
    {
        public readonly UIMainCanvas MainCanvas;
        public readonly List<ICardView> CardViews;
        public UIControllerProtocol(UIMainCanvas mainCanvas, List<ICardView> cardViews)
        {
            MainCanvas = mainCanvas;
            CardViews = cardViews;
        }
    }

    public class UIController : IDisposable
    {
        private readonly UIMainCanvas _mainCanvas;
        private readonly List<ICardView> _cards;
        private readonly IInstantiator _instantiator;

        private int _currentIndex = 0;
        private BaseCommand _flipCommand;
        public UIController(
            IInstantiator instantiator,
            UIControllerProtocol protocol)
        {
            _mainCanvas = protocol.MainCanvas;
            _cards = protocol.CardViews;
            _instantiator = instantiator;
        }

        public void Init()
        {
            _mainCanvas.DropDownValueChange += DropDownValueChangeHandler;
            _mainCanvas.PlayButtonClickEvent += PlayButtonClickEventHandler;
            _mainCanvas.CancelButtonClickEvent += CancelButtonClickEventHandler;
            _mainCanvas.SetCancelButtonActive(false);
            _mainCanvas.SetPlayButtonActive(true);
        }

        public void Dispose()
        {
            _mainCanvas.DropDownValueChange -= DropDownValueChangeHandler;
            _mainCanvas.PlayButtonClickEvent -= PlayButtonClickEventHandler;
            _mainCanvas.CancelButtonClickEvent -= CancelButtonClickEventHandler;
            _flipCommand.Cancel();
            _cards.Clear();
        }

        private void CancelButtonClickEventHandler()
        {
            _flipCommand.Cancel();
            _mainCanvas.SetPlayButtonActive(true);
            _mainCanvas.SetCancelButtonActive(false);            
        }

        private async void PlayButtonClickEventHandler()
        {
            _mainCanvas.SetPlayButtonActive(false);
            _mainCanvas.SetCancelButtonActive(true);
            switch (_currentIndex)
            {
                case 0:
                    _flipCommand = _instantiator.Instantiate<AllInTimeFlipCommand>(new object[] { _cards });
                    break;
                case 1:
                    _flipCommand = _instantiator.Instantiate<OneByOneFlipCommand>(new object[] { _cards });
                    break;
                case 2:
                    _flipCommand = _instantiator.Instantiate<FlipOnReadyCommand>(new object[] { _cards });
                    break;
                default:
                    _flipCommand = _instantiator.Instantiate<AllInTimeFlipCommand>(new object[] { _cards });
                    break;
            }            
            await _flipCommand.Do();
            _mainCanvas.SetPlayButtonActive(true);
            _mainCanvas.SetCancelButtonActive(false);
        }

        private void DropDownValueChangeHandler(object sender, int e)
        {
            _currentIndex = e;
        }        
    }
}