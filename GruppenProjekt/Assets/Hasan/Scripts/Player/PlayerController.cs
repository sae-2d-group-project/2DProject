//####################################################
//##												##
//##		script by:		gregor hempel			##
//##		creation date:	14.05.2019				##
//##		change date:	14.05.2019				##
//##												##
//####################################################

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float m_Speed = 10.0f;

    private Vector3 m_movement = Vector3.zero;
    private Rigidbody2D m_rb2d;
    public Material[] m_RoomMaterials;
    public Material[] m_WallMaterials;
    public ListofList m_RoomWalls = new ListofList();
    private List<int> m_listIntList = new List<int>();
    private List<int> m_wallIntList = new List<int>();

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

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        // LookAtMouse();
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Ray();
        Move(h, v);
    }

    private void LookAtMouse()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);// FIX CAMERA.MAIN
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        Vector2 mousePos = Input.mousePosition;

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Move(float _h, float _v)
    {
        m_movement.Set(_h, _v, 0);

        m_movement = m_movement.normalized * m_Speed * Time.deltaTime;

        m_rb2d.MovePosition(transform.position + m_movement);
    }

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