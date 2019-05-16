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

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float m_Speed = 10.0f;

    private Vector3 m_movement = Vector3.zero;
    private Rigidbody2D m_rb2d;
    public Material[] m_RoomMaterials;
    public ListofList m_RoomWalls = new ListofList();

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
        // for (int i = 0; i < m_RoomMaterials.Length; i++)
        // {
        //     m_RoomMaterials[i] = GetComponent<Material>();
        // }
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
                                int dsad = m_RoomWalls.list[i--].gameObject.Count;
                                for (int j = 0; j < m_RoomWalls.list[i--].gameObject.Count; j++)
                                {
                                    m_RoomWalls.list[i].gameObject[j].SetActive(false);
                                }
                            }
                            continue;
                        }
                        m_RoomMaterials[i].color = Color.grey;
                    }
                }
                Debug.Log(raycastHit.collider.tag);
            }
        }
        else
            return;
        // 7E7E7E
    }
}