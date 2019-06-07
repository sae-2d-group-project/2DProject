//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	28.05.2019		//
//		last time edited:	30.05.2019		//
//		times edited:		3				//
//------------------------------------------//
//notes:
//transfered once from older project version

using UnityEngine;

public class Entity : MonoBehaviour, IInitializes
{
    private bool m_init = false;

    //-------------------------------------------------------------------------------------------------------------------------------------------DAS MAß ALLER DINGE
    #region PROPERTIES------------------------------------------------------------------------------------------------------------------------------------PROPERTIES

    public bool Initialized
    {
        get
        {
            return m_init;
        }
        set
        {
            m_init = value;
        }
    }

    #endregion

    private void Start()
    {
        Init();
    }

    public virtual void Init() { }

    /// <summary>
    /// takes in entity variables creating a detailed entity debug log
    /// </summary>
    /// <param name="_vars">variable values that get stored in a debug log</param>
    public void DebugEntityVars(params object[] _vars)
    {
        string log = "ENTITY LOG: " + gameObject.name + ",\n" +
            "Entity Type: " + this + ",\n";
        if (_vars.Length > 0)
        {
            for (int i = 0; i < _vars.Length; ++i)
            {
                log += _vars[i] + ",\n";
            }
        }
        Debug.Log(log);
    }
}