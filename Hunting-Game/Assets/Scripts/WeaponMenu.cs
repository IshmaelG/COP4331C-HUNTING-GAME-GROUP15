using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Activate()
    {
        if (!SaveManager.instance.hasLoaded)
        {
            gameObject.SetActive(true);
        }   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
