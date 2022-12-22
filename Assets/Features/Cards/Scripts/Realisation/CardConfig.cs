using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Features.Cards.Scripts.Realisation
{
    [Serializable]
    public struct CardModel
    {
        public int ID;        
        public string Name;
        public string Discription;
        public Sprite FaceSprite;
        public Sprite BackSprite;        
    }

    [CreateAssetMenu(fileName = "CardConfig", menuName = "ScriptableObjects/CardConfig", order = 1)]
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private CardModel[] cardModels;

        [NonSerialized] private bool _inited;

        private Dictionary<int, CardModel> _dict;

        public CardModel? Get(int id)
        {
            if(!_inited)
            {
                Init();
            }
            if(_dict.ContainsKey(id))
            {
                return _dict[id];
            }

            Debug.LogError($"There no such card model with id {id}");
            return null;
        }

        private void Init()
        {
            _dict = new Dictionary<int, CardModel>();
            foreach (var model in cardModels)
            {
                _dict.Add(model.ID, model);
            }
            _inited = true;
        }
    }
}