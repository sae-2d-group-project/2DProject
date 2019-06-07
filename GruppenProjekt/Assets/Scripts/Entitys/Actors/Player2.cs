//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	28.05.2019		//
//		last time edited:	02.06.2019		//
//		times edited:		4				//
//------------------------------------------//
//notes:
//transfered once from older project version

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Actor, IInitializes, IHasFaction, ITakesDamage
{
    #region VARIABLES--------------------------------------------------------------------------------------------------------------------------------------VARIABLES

    private Vector3 m_movement = Vector3.zero;

    private ESelectedWeapon m_selected = 0;

    private float m_currentRecoil = 0f;

    private float m_randomRecoil = 0f;

    #endregion

    #region PROPERTIES------------------------------------------------------------------------------------------------------------------------------------PROPERTIES

    public float CurrentRecoil
    {
        get
        {
            return m_currentRecoil;
        }
        private set
        {
            m_currentRecoil = Mathf.Clamp(value, 0f, 0.2f);// TEST
        }
    }

    public float RandomRecoil
    {
        get
        {
            return Random.Range(0f, m_currentRecoil) * (Random.Range(0, 2) * 2 - 1);
        }
    }

    #endregion

    public override void Init()
    {
        base.Init();

        // SET FACTIONS
        m_Faction = 0;// clear
        SetFlags(ref m_Faction, EFaction.Friendly, EFaction.Human);// set
        // SET TARGETS
        m_TargetList = 0;// clear
        SetFlags(ref m_TargetList, EFaction.Hostile);// set

        m_Primary.SetWeapon(this);// TMP

        Initialized = true;
    }

    private void Update()
    {
        if (Initialized)
        {
            if (Alive)
            {
                if (CurrentRecoil > 0)
                    CurrentRecoil -= 0.025f * Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Alpha1) && m_Melee != null)
                {
                    m_selected = ESelectedWeapon.Melee;
                    m_Melee.SetWeapon(this);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2) && m_Secondary != null)
                {
                    m_selected = ESelectedWeapon.Secondary;
                    m_Secondary.SetWeapon(this);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3) && m_Primary != null)
                {
                    m_selected = ESelectedWeapon.Primary;
                    m_Primary.SetWeapon(this);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    switch (m_selected)
                    {
                        case ESelectedWeapon.Melee:

                            break;
                        case ESelectedWeapon.Secondary:
                            m_Secondary.FireWeapon(m_GunMuzzle.transform.position, m_RotationAnchor.transform.rotation);
                            CurrentRecoil += m_Secondary.m_RWRecoil;
                            break;
                        case ESelectedWeapon.Primary:
                            m_Primary.FireWeapon(m_GunMuzzle.transform.position, m_RotationAnchor.transform.rotation);
                            CurrentRecoil += m_Primary.m_RWRecoil;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Initialized)
        {
            if (Alive)
            {
                RotateHand();

                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                Move(x, y);
            }
        }
    }

    private void Move(float _x, float _y)
    {
        m_movement.Set(_x, _y, 0);

        m_movement = m_movement.normalized * m_MoveSpeed * Time.deltaTime;

        m_rigid2D.MovePosition(transform.position + m_movement);
    }

    private void RotateHand()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float deg = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;

        m_RotationAnchor.transform.rotation = Quaternion.AngleAxis(deg - 90, Vector3.forward);
    }

    private enum ESelectedWeapon : byte
    {
        Melee = 0,
        Secondary = 1 << 0,
        Primary = 1 << 1
    }
}