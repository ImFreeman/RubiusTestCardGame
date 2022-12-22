using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

namespace Assets.Features.Cards.Scripts.Realisation
{
    [Serializable]
    public struct CardAnimationModel
    {
        public int ID;
        public float Speed;
        public Ease Ease;
        public Vector3 Rotation;
    }    

    [CreateAssetMenu(fileName = "CardAnimationConfig", menuName = "ScriptableObjects/CardAnimationConfig", order = 2)]
    public class CardAnimationConfig : ScriptableObject
    {
        [SerializeField] private CardAnimationModel[] animModels;
        [NonSerialized] private bool _inited;

        private Dictionary<int, CardAnimationModel> _dict;
        public CardAnimationModel? Get(int id)
        {
            if (!_inited)
            {
                Init();
            }
            if (_dict.ContainsKey(id))
            {
                return _dict[id];
            }

            Debug.LogError($"There no such card animation model with id {id}");
            return null;
        }

        private void Init()
        {
            _dict = new Dictionary<int, CardAnimationModel>();
            foreach (var model in animModels)
            {
                _dict.Add(model.ID, model);
            }
            _inited = true;
        }
    }
}