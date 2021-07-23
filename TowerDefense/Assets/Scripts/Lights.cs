using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] private Light render;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Shadow") == 0)
        {
            render.shadows = LightShadows.None;
        }
        else
        {
            render.shadows = LightShadows.Hard;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
