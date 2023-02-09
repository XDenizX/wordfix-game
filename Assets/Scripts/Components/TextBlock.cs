using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class TextBlock : MonoBehaviour
    {
        [SerializeField]
        private CharacterBlock characterBlockPrefab;

        private readonly List<CharacterBlock> _characterBlocks = new();
    
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _characterBlocks.ForEach(block => Destroy(block.gameObject));
                _characterBlocks.Clear();
            
                foreach (char character in value)
                {
                    var block = Instantiate(characterBlockPrefab, transform);
                    block.Character = character;
                    _characterBlocks.Add(block);
                }
            }
        }
    }
}