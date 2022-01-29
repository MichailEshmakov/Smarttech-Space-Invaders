using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    private int _maxHealth;

    private void Awake()
    {
        _maxHealth = _health.Value;
    }

    private void OnEnable()
    {
        if (_maxHealth != 0)
            _health.ValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        _slider.value = (float)_health.Value / _maxHealth;
    }
}
