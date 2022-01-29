using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _score.ValueChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _score.ValueChanged -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _scoreText.text = _score.Value.ToString();
    }
}
