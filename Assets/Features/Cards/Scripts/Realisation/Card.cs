using Assets.Features.Cards.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Features.Cards.Scripts.Realisation
{
    public class Card : MonoBehaviour, ICard
    {
        [SerializeField] private RectTransform bodyTransform;

        public RectTransform BodyTransform => bodyTransform;

        
    }
}