//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	28.05.2019		//
//		last time edited:	02.06.2019		//
//		times edited:		5				//
//------------------------------------------//
//notes:
//transfered once from older project version

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject
{
    public int m_WDamage = 0;
    public float m_WFireRate = 0f;

    private bool m_wInit = false;

    [HideInInspector]
    public Vulnerable m_owner = null;

    public bool WeaponSet
    {
        get
        {
            return m_wInit;
        }
        set
        {
            m_wInit = value;
        }
    }

    public Weapon(int _damage, float _fireRate)
    {
        m_WDamage = _damage;
        m_WFireRate = _fireRate;
    }

    public virtual void SetWeapon(Vulnerable _owner)
    {
        m_owner = _owner;
    }
}