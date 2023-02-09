using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;

namespace Components
{
    public class InputLine : MonoBehaviour
    {
        public event EventHandler<string> TextEntered;
    
        [SerializeField]
        private CharacterBlock characterPrefab;

        [SerializeField]
        private InputController inputController;
    
        private readonly LinkedList<(char Character, CharacterBlock Block)> _currentInput = new();

        public void Clear()
        {
            foreach (var character in _currentInput)
            {
                Destroy(character.Block.gameObject);
            }
            _currentInput.Clear();
        }
    
        private void OnEnable()
        {
            inputController.BackspacePressed += OnBackspacePressed;
            inputController.ReturnPressed += OnReturnPressed;
            inputController.KeyPressed += OnKeyPressed;
        }
    
        private void OnDisable()
        {
            inputController.BackspacePressed -= OnBackspacePressed;
            inputController.ReturnPressed -= OnReturnPressed;
            inputController.KeyPressed -= OnKeyPressed;
        }

        private void OnKeyPressed(object sender, char character)
        {
            CharacterBlock block = Instantiate(characterPrefab, transform);
            block.Character = character;
        
            _currentInput.AddLast((character, block));
        }

        private void OnReturnPressed(object sender, EventArgs e)
        {
            var enteredText = GetInputText();
            TextEntered?.Invoke(this, enteredText);
        }

        private void OnBackspacePressed(object sender, EventArgs e)
        {
            if (_currentInput.Count == 0)
                return;
        
            var lastBlock = _currentInput.Last.Value;
            Destroy(lastBlock.Block.gameObject);
            _currentInput.RemoveLast();
        }

        private string GetInputText()
        {
            var characters = _currentInput
                .Select(tuple => tuple.Character)
                .ToArray();
        
            return new string(characters);
        }
    }
}