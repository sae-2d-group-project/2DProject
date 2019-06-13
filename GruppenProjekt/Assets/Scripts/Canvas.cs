//------------------------------------------//
//		script by:			Hasan Sahin 	//
//		date of creation:	06.06.2019		//
//		last time edited:	10.06.2019		//
//------------------------------------------//
//notes:
//
<<<<<<< HEAD:GruppenProjekt/Assets/Scripts/Canvas.cs
=======
using System;
using TMPro;
>>>>>>> master:GruppenProjekt/Assets/Scripts/Canvas.cs
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{

    private GameObject[] m_healths;
    public PlayerController pCtrl;
    public GameObject m_parrentGO;
    public GameObject m_Interactable;
    public Sprite m_redLife;
    public Sprite m_blackLife;
    public TMP_Text m_interText;
    public static string m_Remaining;
    public static string m_kInt;
    public static string m_iText;
    public static bool m_interactable;


    // Start is called before the first frame update
    void Start()
    {
        Health();
<<<<<<< HEAD:GruppenProjekt/Assets/Scripts/Canvas.cs
=======
        m_kInt = FindObjectOfType<KeyBinds>().GetComponent<KeyBinds>().m_Keys[8];
>>>>>>> master:GruppenProjekt/Assets/Scripts/Canvas.cs
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_healths.Length; i++)
        {
<<<<<<< HEAD:GruppenProjekt/Assets/Scripts/Canvas.cs
            if (i < pCtrl.m_Health)
=======
            if (i < PlayerController.m_Health)
>>>>>>> master:GruppenProjekt/Assets/Scripts/Canvas.cs
                m_healths[i].GetComponent<Image>().sprite = m_redLife;
            else
                m_healths[i].GetComponent<Image>().sprite = m_blackLife;
        }
<<<<<<< HEAD:GruppenProjekt/Assets/Scripts/Canvas.cs
    }
    void Health()
    {
        m_healths = new GameObject[pCtrl.m_Health];
=======
        if (m_interactable)
        {
            m_Interactable.SetActive(true);
            m_interText.text = m_iText;
        }
        else
            m_Interactable.SetActive(false);
    }
    void Health()
    {
        m_healths = new GameObject[PlayerController.m_Health];
>>>>>>> master:GruppenProjekt/Assets/Scripts/Canvas.cs
        for (int i = 0; i < m_healths.Length; i++)
        {
            if (i == 0)
            {
                m_healths[i] = new GameObject("Health: " + i);
                m_healths[i].AddComponent<Image>();
                m_healths[i].transform.parent = m_parrentGO.transform;
                m_healths[i].transform.position = m_parrentGO.transform.position;
                m_healths[i].GetComponent<Image>().sprite = m_redLife;
            }
            else
            {
                m_healths[i] = new GameObject("Health: " + i);
                m_healths[i].AddComponent<Image>();
                m_healths[i].transform.parent = m_parrentGO.transform;
                m_healths[i].transform.position = m_parrentGO.transform.position;
                m_healths[i].GetComponent<Image>().sprite = m_redLife;
                m_healths[i].transform.position = m_healths[i - 1].transform.position + new Vector3(100, 0, 0);
            }
        }
    }
}
