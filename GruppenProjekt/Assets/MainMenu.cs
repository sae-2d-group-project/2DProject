using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject m_Settings;
    public GameObject m_SetAKey;
    public TMP_InputField[] tMP_InputFields;
    public KeyBinds keyBinds;

    int sIndex;
    int check = 0;
    bool isSelected = false;
    public void Start()
    {
        for (int i = 0; i < tMP_InputFields.Length; i++)
        {
            tMP_InputFields[i].readOnly = true;
            keyBinds.m_Keys.Add(tMP_InputFields[i].text);
        }
    }

    public void Update()
    {
        if (m_Settings.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isSelected)
            {
                m_Settings.SetActive(false);
                isSelected = false;
            }
            if (isSelected)
                Key();
        }
    }

    public void StartGame()
    {
        // Load GameScene
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
        for (int i = 0; i < tMP_InputFields.Length; i++)
        {
            keyBinds.m_Keys[i] = tMP_InputFields[i].text;
        }
    }
    public void Settings()
    {
        if (m_Settings.activeInHierarchy)
            m_Settings.SetActive(false);
        else
            m_Settings.SetActive(true);
    }
    public void Credits()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Key()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                m_SetAKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isSelected = false;
                    m_SetAKey.SetActive(false);
                    check = 0;
                }
                check++;
                if (check == 2)
                {
                    for (int i = 0; i < tMP_InputFields.Length; i++)
                    {
                        if (vKey.ToString() == tMP_InputFields[i].text)
                        {
                            tMP_InputFields[i].text = "";
                        }
                    }
                    tMP_InputFields[sIndex].text = vKey.ToString();
                    check = 0;
                    isSelected = false;
                    m_SetAKey.SetActive(false);
                }
            }
        }
    }
    public void IndexSetter(TMP_InputField tMP_InputField)
    {
        for (int i = 0; i < tMP_InputFields.Length; i++)
            if (tMP_InputFields[i] == tMP_InputField)
            { sIndex = i; isSelected = true; Debug.Log(tMP_InputFields[i].text); }
    }
}