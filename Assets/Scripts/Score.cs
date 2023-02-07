using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private bool _hasText;
    
    private int _points;
    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            if (_hasText)
                _scoreText.text = value.ToString();
        }
    }

    private void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        _hasText = _scoreText != null;
    }
}