//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	DD.MM.YYYY		//
//		last time edited:	DD.MM.YYYY		//
//		times edited:		X				//
//------------------------------------------//
//notes:
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : NPC, IInitializes, IHasFaction, ITakesDamage
{
    public override void Init()
    {
        base.Init();

        // SET FACTION
        m_Faction = 0;// clear
        SetFlags(ref m_Faction, EFaction.Hostile);// set
        // SET TARGETS
        m_TargetList = 0;// clear
        SetFlags(ref m_TargetList, EFaction.Friendly);// set

        Initialized = true;
    }

    private void Update()
    {
        if (Initialized)
        {
            if (!Alive)
            {
                Destroy(gameObject);
            }
        }
    }
}