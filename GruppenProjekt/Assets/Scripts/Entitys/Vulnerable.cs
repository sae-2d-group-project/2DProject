//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	28.05.2019		//
//		last time edited:	30.05.2019		//
//		times edited:		3				//
//------------------------------------------//
//notes:
//transfered once from older project version

using UnityEngine;

public class Vulnerable : Entity, IInitializes, IHasFaction
{
    //-------------------------------------------------------------------------------------------------------------------------------------------DAS MAß ALLER DINGE
    #region VARIABLES--------------------------------------------------------------------------------------------------------------------------------------VARIABLES

    public int m_MaxHP = 100;

    [HideInInspector]
    public EFaction m_Faction = 0;
    [HideInInspector]
    public EFaction m_TargetList = 0;

    private int m_currentHP = 100;
    private bool m_isAlive = true;

    #endregion

    #region PROPERTIES------------------------------------------------------------------------------------------------------------------------------------PROPERTIES

    public bool Alive
    {
        get
        {
            return m_isAlive;
        }
        private set
        {
            m_isAlive = value;
        }
    }

    public int HP
    {
        get
        {
            return Mathf.Clamp(m_currentHP, 0, m_MaxHP);
        }
        private set
        {
            m_currentHP = !m_Faction.HasFlag(EFaction.Invulnerable) ? Mathf.Clamp(value, 0, m_MaxHP) : m_currentHP;// if the actor is invulnerable current hp are set to currentHP
            Alive = (HP > 0) ? true : false;
        }
    }

    #endregion

    #region FLAGS----------------------------------------------------------------------------------------------------------------------------------------------FLAGS

    public void SetFlags(ref EFaction _enum, params EFaction[] _flags)
    {
        if (_flags.Length == 0) return;

        for (int i = 0; i < _flags.Length; ++i)
        {
            _enum |= _flags[i];
        }
    }

    public void UnsetFlags(ref EFaction _enum, params EFaction[] _flags)
    {
        if (_flags.Length == 0) return;

        for (int i = 0; i < _flags.Length; ++i)
        {
            _enum &= ~_flags[i];
        }
    }

    #endregion

    #region VULNERABLE------------------------------------------------------------------------------------------------------------------------------------VULNERABLE

    public virtual void TakeDamage(int _damage)
    {
        if (!Alive)
            return;

        HP -= Mathf.Abs(_damage);
    }

    public virtual void HealDamage(int _heal)
    {
        if (!Alive)
            return;

        HP += Mathf.Abs(_heal);
    }

    #endregion
}