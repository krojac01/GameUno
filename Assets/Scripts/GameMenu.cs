using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{

    private GameObject menu;
    
    // Start is called before the first frame update
    void Start()
    {
        menu = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}
