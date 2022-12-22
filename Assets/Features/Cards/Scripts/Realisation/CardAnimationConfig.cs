using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

namespace Assets.Features.Cards.Scripts.Realisation
{    
    [CreateAssetMenu(fileName = "CardAnimationConfig", menuName = "ScriptableObjects/CardAnimationConfig", order = 2)]
    public class CardAnimationConfig : ScriptableObject
    {        
        [SerializeField] private float speed;
        [SerializeField] private Ease ease;
        [SerializeField] private Vector3 rotation;

        public float Speed => speed;
        public Ease Ease => ease;
        public Vector3 Rotation => rotation;
    }
}