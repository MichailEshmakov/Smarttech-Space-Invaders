using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Health _health;
    [SerializeField] private float _maxStartShootingDelay;
    [SerializeField] private int _score;

    private bool _isFirstRow = false;
    private Coroutine StartShootingWaiting;

    public int Score => _score;

    public event UnityAction<Enemy> Dead;
    public event UnityAction BorderCollided;
    public event UnityAction EnemyCollided;

    private void OnValidate()
    {
        if (_health == null)
            _health = GetComponent<Health>();

        if (_weapon == null)
            _weapon = GetComponent<Weapon>();
    }

    private void OnEnable()
    {
        if (_isFirstRow && StartShootingWaiting == null)
            StartCoroutine(WaitShootingStart());

        _health.Dead += OnDead;
    }

    private void OnDisable()
    {
        _weapon.ToFirePrepared -= OnWeaponPrepared;
        _health.Dead -= OnDead;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SideBorder sideBorder))
        {
            BorderCollided?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            EnemyCollided?.Invoke();
        }
    }

    public void SetFirstRow()
    {
        _isFirstRow = true;

        if (enabled && StartShootingWaiting == null)
            StartCoroutine(WaitShootingStart());
    }

    private void OnDead()
    {
        Dead?.Invoke(this);
    }

    private void OnWeaponPrepared()
    {
        _weapon.TryFire();
    }

    private void StartFire()
    {
        _weapon.TryFire();
        _weapon.ToFirePrepared += OnWeaponPrepared;
    }

    private IEnumerator WaitShootingStart()
    {
        yield return new WaitForSeconds(Random.Range(0, _maxStartShootingDelay));
        StartFire();
        StartShootingWaiting = null;
    }
}