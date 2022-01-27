using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _muzzleEnd;
    [SerializeField] private Transform _muzzleStart;
    [SerializeField] private float _rechargeTime;

    private float _rechargeTimeRemaind = 0;

    public event UnityAction ToFirePrepared;

    private void Start()
    {
        TryInvokePreparing();
    }

    public bool TryFire()
    {
        if (CanFire())
        {
            Bullet newBullet = Instantiate(_bulletPrefab, _muzzleEnd.position, _muzzleEnd.rotation);
            newBullet.Init(_muzzleEnd.position - _muzzleStart.position);
            _rechargeTimeRemaind = _rechargeTime;
            StartCoroutine(Recharge());
            return true;
        }

        return false;
    }

    public bool CanFire()
    {
        return _rechargeTimeRemaind <= 0;
    }

    private IEnumerator Recharge()
    {
        while (_rechargeTimeRemaind > 0)
        {
            _rechargeTimeRemaind -= Time.deltaTime;
            yield return null;
        }

        TryInvokePreparing();
    }

    private void TryInvokePreparing()
    {
        if (CanFire())
        {
            ToFirePrepared?.Invoke();
        }
    }
}
