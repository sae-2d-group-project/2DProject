//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	31.05.2019		//
//		last time edited:	02.06.2019		//
//		times edited:		2				//
//------------------------------------------//
//notes:
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity, IInitializes
{
    public Rigidbody2D m_BulletBody2D;
    public CircleCollider2D m_BulletColl;

    private int m_bulletDamage = 0;
    private int m_bulletSpeed = 0;
    private int m_bulletRange = 0;

    private Vector2 m_sourcePos = Vector2.zero;

    private EFaction m_targets = 0;

    private bool m_bulletFired = false;
    private bool m_bulletHit = false;

    private void Awake()
    {
        if (m_BulletBody2D == null)
            gameObject.AddComponent<Rigidbody2D>();

        m_BulletBody2D.bodyType = RigidbodyType2D.Kinematic;                          // default bullet
        m_BulletBody2D.simulated = true;                                              // default bullet
        m_BulletBody2D.useFullKinematicContacts = false;                              // default bullet
        m_BulletBody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;  // default bullet
        m_BulletBody2D.sleepMode = RigidbodySleepMode2D.StartAwake;                   // default bullet
        m_BulletBody2D.interpolation = RigidbodyInterpolation2D.None;                 // default bullet
        m_BulletBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;           // default bullet

        if (m_BulletColl == null)
            gameObject.AddComponent<CircleCollider2D>();

        m_BulletColl.isTrigger = true;          // default bullet
        m_BulletColl.usedByEffector = false;    // default bullet
        m_BulletColl.radius = 0.1f;             // default bullet
    }

    public void FireBullet(EFaction _targets, int _damage, int _speed, int _range)
    {
        m_targets = _targets;
        m_bulletDamage = _damage;
        m_bulletSpeed = _speed;
        m_bulletRange = _range;

        m_sourcePos = transform.position;

        m_bulletFired = true;
    }

    private void FixedUpdate()
    {
        if (!m_bulletFired || m_bulletHit)
            return;

        if (Vector2.Distance(m_sourcePos, transform.position) > m_bulletRange)
            Destroy(gameObject);

        Vector2 velocity = transform.up * m_bulletSpeed;
        m_BulletBody2D.MovePosition(m_BulletBody2D.position + velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.isTrigger)
            return;

        m_bulletHit = true;

        Vulnerable hit = _other.GetComponentInParent<Vulnerable>();

        if (hit != null)
        {
            if (hit.m_Faction.HasFlag(m_targets))
            {
                hit.TakeDamage(m_bulletDamage);
            }
        }

        Destroy(gameObject);
    }
}