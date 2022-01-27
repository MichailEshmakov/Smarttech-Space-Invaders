using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _muzzleEnd;
    [SerializeField] private Transform _muzzleStart;
    [SerializeField] private float _rechargeTime;

    private float _rechargeTimeRemaind = 0;

    public void TryFire()
    {
        if (_rechargeTimeRemaind <= 0)
        {
            Bullet newBullet = Instantiate(_bulletPrefab, _muzzleEnd.position, _muzzleEnd.rotation);
            newBullet.Init(_muzzleEnd.position - _muzzleStart.position);
            _rechargeTimeRemaind = _rechargeTime;
            StartCoroutine(Recharge());
        }    
    }

    private IEnumerator Recharge()
    {
        while (_rechargeTimeRemaind > 0)
        {
            _rechargeTimeRemaind -= Time.deltaTime;
            yield return null;
        }
    }
}
