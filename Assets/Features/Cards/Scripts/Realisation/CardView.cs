using Assets.Features.Cards.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Features.Cards.Scripts.Realisation
{
    public class CardView : MonoBehaviour, ICardView
    {
        public RectTransform BodyTransform => bodyTransform;

        public CardSide CurrentCardSide => _currentSide;

        [SerializeField] private Image image;
        [SerializeField] private RectTransform bodyTransform;
        [SerializeField] private GameObject textSpace;
        [SerializeField] private TMP_Text nameLabel;
        [SerializeField] private TMP_Text descriptionLabel;        
        [SerializeField] private RawImage mainPicture;

        private Sprite _backSprite;
        private Sprite _frontSprite;
        private CardSide _currentSide;

        [Inject]
        private void Construct(CardViewProtocol protocol)
        {
            _backSprite = protocol.BackSprite;
            _frontSprite = protocol.FrontSprite;
            nameLabel.text = protocol.Name;
            descriptionLabel.text = protocol.Discription;
        }       

        public void SetMainPicture(Texture2D texture)
        {
            mainPicture.texture = texture;
        }
        
        public void SetCardSide(CardSide side)
        {
            var isFront = side == CardSide.Front;
            mainPicture.gameObject.SetActive(isFront);
            textSpace.SetActive(isFront);
            image.sprite = isFront ? _frontSprite : _backSprite;
            _currentSide = side;
        }
    }
}