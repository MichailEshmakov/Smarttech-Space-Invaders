using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverer : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

    public event UnityAction GameOvered;

    private void OnEnable()
    {
        _playerHealth.Dead += OnPlayerDead;
    }

    private void OnDisable()
    {
        _playerHealth.Dead -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        OverGame();
    }

    private void OverGame()
    {
        GameOvered?.Invoke();
        Time.timeScale = 0;
    }
}
