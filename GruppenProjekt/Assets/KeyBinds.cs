using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinds : MonoBehaviour
{
    /// <summary>
    /// [0]Forwardss;
    /// [1]Backwards;
    /// [2]Right;
    /// [3]Leftic;
    /// [4]Shoot;
    /// [5]Melee;
    /// [6]Secondary;
    /// [7]Primary;
    /// </summary>
    public List<string> m_Keys = new List<string>();
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
