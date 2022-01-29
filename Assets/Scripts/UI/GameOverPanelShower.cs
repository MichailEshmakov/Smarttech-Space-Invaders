using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelShower : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameOverer _gameOverer;

    private void OnEnable()
    {
        _gameOverer.GameOvered += OnGameOvered;
    }

    private void OnDisable()
    {
        _gameOverer.GameOvered -= OnGameOvered;
    }

    private void OnGameOvered()
    {
        _gameOverPanel.SetActive(true);
    }
}
