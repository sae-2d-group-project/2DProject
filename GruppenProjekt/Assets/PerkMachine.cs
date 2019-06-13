using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMachine : MonoBehaviour
{
    bool m_usable = true;
    float timer;
    protected int maxUse = 5;
    public string m_PerkName;
    public Material m_Color;

    void Start()
    {
        timer = PlayerController.m_PerkTimer;
    }

    void Update()
    {
        if (timer < PlayerController.m_PerkTimer)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        else
        {
            timer = PlayerController.m_PerkTimer; m_usable = true;
            m_Color.color = Color.white;
        }

        Debug.Log(timer);
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (m_usable)
            if (collider.tag == "Player" && maxUse != 0)
            {
                if (PlayerController.m_Health < 5)
                {
                    Canvas.m_interactable = true;
                    Canvas.m_iText = ("Press key '" + Canvas.m_kInt + "' to get Coke! " + maxUse + "x left");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        maxUse--; m_usable = true; timer = 0;
                        collider.SendMessage("GivePlayerPerk", m_PerkName);
                        m_Color.color = Color.black;
                    }
                }
            }
            else
                Canvas.m_interactable = true;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        Canvas.m_interactable = false;
    }
}
