using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public PlayerController pCtrl;

    public GameObject m_parrentGO;
    private GameObject[] m_healths;
    public Sprite m_redLife;
    public Sprite m_blackLife;
    // Start is called before the first frame update
    void Start()
    {
        Health();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_healths.Length; i++)
        {
            if (i < pCtrl.m_Health)
                m_healths[i].GetComponent<Image>().sprite = m_redLife;
            else
                m_healths[i].GetComponent<Image>().sprite = m_blackLife;
        }
    }
    void Health()
    {
        m_healths = new GameObject[pCtrl.m_Health];
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
