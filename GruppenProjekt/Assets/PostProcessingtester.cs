using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingtester : MonoBehaviour
{
    PostProcessVolume m_volume;
    Vignette m_vignette;
    // Start is called before the first frame update
    void Start()
    {
        //Get the Post Processing Volume Component and set isGlobal to true
        m_volume = GetComponent<PostProcessVolume>();
        m_volume.isGlobal = true;

        m_vignette = ScriptableObject.CreateInstance<Vignette>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
