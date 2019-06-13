//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	28.05.2019		//
//		last time edited:	02.06.2019		//
//		times edited:		3				//
//------------------------------------------//
//notes:
//transfered once from older project version

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Vulnerable, IInitializes, IHasFaction, ITakesDamage
{
    #region VARIABLES--------------------------------------------------------------------------------------------------------------------------------------VARIABLES

    public Rigidbody2D m_rigid2D;
    public float m_MoveSpeed = 5f;

    public GameObject m_RotationAnchor;
    public GameObject m_GunMuzzle;

    public MeleeWeapon m_Melee;
    public RangedWeapon m_Secondary;
    public RangedWeapon m_Primary;

    #endregion

    public override void Init()
    {
        m_rigid2D = GetComponent<Rigidbody2D>();
        if (m_rigid2D == null)
            m_rigid2D = gameObject.AddComponent<Rigidbody2D>();

        m_rigid2D.gravityScale = 0;                                                 // default rigidbody2D
        m_rigid2D.bodyType = RigidbodyType2D.Dynamic;                               // default rigidbody2D
        m_rigid2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;     // default rigidbody2D
        m_rigid2D.rotation = 0;                                                     // default rigidbody2D
        m_rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;              // default rigidbody2D

        if (m_RotationAnchor == null)
            m_RotationAnchor = new GameObject("RotationAnchor");

        m_RotationAnchor.transform.SetParent(transform);
        m_RotationAnchor.transform.position = new Vector3(0,0,0);
        m_RotationAnchor.transform.rotation = Quaternion.identity;

        if (m_GunMuzzle == null)
            m_GunMuzzle = new GameObject("GunMuzzle");

        m_GunMuzzle.transform.SetParent(m_RotationAnchor.transform);
        m_GunMuzzle.transform.position = new Vector3(0, 0.5f, 0);
        m_GunMuzzle.transform.rotation = Quaternion.identity;
    }
}