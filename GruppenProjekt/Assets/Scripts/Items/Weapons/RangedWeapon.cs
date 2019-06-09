//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	31.05.2019		//
//		last time edited:	02.06.2019		//
//		times edited:		3				//
//------------------------------------------//
//notes:
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public GameObject m_RWBulletPrefab;
    public EFireMode m_RWFireMode = EFireMode.Single;
    public float m_RWAccuracy = 0f;
    public float m_RWRecoil = 0f;
    public int m_RWMaxRange = 0;
    public int m_RWSpeed = 0;

    public RangedWeapon(GameObject _bulletPrefab, EFireMode _fireMode, int _damage, float _fireRate, float _accuracy, float _recoil, int _maxRange, int _speed)
        : base(_damage, _fireRate)
    {
        m_RWBulletPrefab = _bulletPrefab;
        m_RWFireMode = _fireMode;
        m_RWAccuracy = _accuracy;
        m_RWRecoil = _recoil;
        m_RWMaxRange = _maxRange;
        m_RWSpeed = _speed;
    }

    public override void SetWeapon(Vulnerable _owner)
    {
        base.SetWeapon(_owner);

        if (m_RWBulletPrefab == null)
            m_RWBulletPrefab = Resources.Load<GameObject>("BulletPrefab");

        WeaponSet = true;
    }

    public virtual void FireWeapon(Vector3 _pos, Quaternion _rot)
    {
        if (!WeaponSet)
            return;

        GameObject bullet = Instantiate(m_RWBulletPrefab, _pos, _rot);

        bullet.GetComponent<Bullet>().FireBullet(m_owner.m_TargetList, m_WDamage, m_RWSpeed, m_RWMaxRange);
    }
}