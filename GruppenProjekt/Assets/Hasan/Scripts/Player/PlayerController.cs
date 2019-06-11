//################################################################
//##												            ##
//##		script by:		gregor hempel & hasan sahin			##
//##		creation date:	14.05.2019				            ##
//##		change date:	6.6.2019				            ##
//##												            ##
//################################################################

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Actor, IInitializes, IHasFaction, ITakesDamage
{
    #region VARIABLES--------------------------------------------------------------------------------------------------------------------------------------VARIABLES
    private Vector3 m_movement = Vector3.zero;
    public Material[] m_RoomMaterials;
    public Material[] m_WallMaterials;
    public ListofList m_RoomWalls = new ListofList();
    private List<int> m_listIntList = new List<int>();
    private List<int> m_wallIntList = new List<int>();
    private ESelectedWeapon m_selected = 0;
    public float m_Speed = 10.0f;
    private float m_currentRecoil = 0f;
    public int m_Health = 5;
    private KeyBinds keyBinds;
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
            return UnityEngine.Random.Range(0f, m_currentRecoil) * (UnityEngine.Random.Range(0, 2) * 2 - 1);
        }
    }

    #endregion

    /// <summary>
    /// Hasan
    /// </summary>
    #region "List of list"
    [Serializable]
    public class GameObjectList
    {
        public List<GameObject> gameObject;
    }
    [Serializable]
    public class ListofList
    {
        public List<GameObjectList> list;
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
        keyBinds = FindObjectOfType<KeyBinds>();
        for (int i = 0; i < keyBinds.m_Keys.Count; i++)
        {
            Debug.Log(keyBinds.m_Keys[i]);
        }
    }
    void FixedUpdate()
    {
        if (Initialized)
        {
            if (Alive)
            {
                RotateHand();

                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                Move(x, y);
                Ray();
            }
        }
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

    private void Move(float _h, float _v)
    {
        m_movement.Set(_h, _v, 0);

        m_movement = m_movement.normalized * m_Speed * Time.deltaTime;

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
    /// <summary>
    /// Hasan
    /// </summary>
    public void Ray()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.tag != null)
            {
                if (raycastHit.collider.gameObject.layer == 9)
                {
                    for (int i = 0; i < m_RoomMaterials.Length; i++)
                    {
                        if (i == Int32.Parse(raycastHit.collider.tag))
                        {
                            m_RoomMaterials[i].color = Color.white;
                            if (i - 1 >= 0)
                            {
                                for (int j = 0; j < m_RoomWalls.list[i - 1].gameObject.Count; j++)
                                {
                                    //m_RoomWalls.list[i - 1].gameObject[j].SetActive(false);
                                    Wall(i - 1, false);
                                    m_listIntList.Add(i - 1);
                                    m_wallIntList.Add(j);
                                }
                            }
                            else
                            {
                                for (int k = 0; k < m_RoomWalls.list.Count; k++)
                                {
                                    if (m_listIntList != null && m_wallIntList != null)
                                    {
                                        for (int l = 0; l < m_listIntList.Count; l++)
                                            Wall(m_listIntList[l], m_wallIntList[l], true);
                                        //m_RoomWalls.list[m_listIntList[l]].gameObject[m_wallIntList[l]].SetActive(true);
                                        m_listIntList.Clear();
                                        m_wallIntList.Clear();
                                    }
                                }
                            }
                            continue;
                        }
                        m_RoomMaterials[i].color = Color.grey;
                    }
                }
            }
        }
        else
            return;
        // 7E7E7E
    }
    /// <summary>
    /// Hasan
    /// </summary>
    /// <param name="_list"></param>
    /// <param name="_setactive"></param>
    public void Wall(int _list, bool _setactive)
    {
        for (int j = 0; j < m_RoomWalls.list[_list].gameObject.Count; j++)
        {
            if (_setactive == false)
                m_RoomWalls.list[_list].gameObject[j].GetComponent<TilemapRenderer>().material = m_WallMaterials[1];
            //m_RoomWalls.list[_list].gameObject[j].SetActive(_setactive);
            else
            {
                m_RoomWalls.list[_list].gameObject[j].GetComponent<TilemapRenderer>().material = m_WallMaterials[0];
            }
        }
    }
    /// <summary>
    /// Hasan
    /// </summary>
    /// <param name="_list"></param>
    /// <param name="_go"></param>
    /// <param name="_setactive"></param>
    public void Wall(int _list, int _go, bool _setactive)
    {
        for (int j = 0; j < m_listIntList.Count; j++)
        {
            if (_setactive == true)
                m_RoomWalls.list[_list].gameObject[_go].GetComponent<TilemapRenderer>().material = m_WallMaterials[0];
            else
                m_RoomWalls.list[_list].gameObject[_go].GetComponent<TilemapRenderer>().material = m_WallMaterials[1];
            // m_RoomWalls.list[_list].gameObject[_go].SetActive(_setactive);
        }
    }
}