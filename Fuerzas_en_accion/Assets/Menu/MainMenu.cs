using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    private bool isopen=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isopen)
            {
                menu.SetActive(false);
                isopen = false;
            }
            else
            {
                menu.SetActive(true);
                isopen = true;
            }
            
        }
    }
}
