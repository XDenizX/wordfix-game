using TMPro;
using UnityEngine;

namespace Components
{
    public class CharacterBlock : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI characterText;

        private char _character;
        public char Character
        {
            get => _character;
            set
            {
                _character = value;
                characterText.text = value.ToString();
            }
        }
    }
}