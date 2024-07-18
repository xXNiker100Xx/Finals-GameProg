using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject tabMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            tabMenu.SetActive(true);
        }
        else
        {
            tabMenu.SetActive(false);
        }
    }
}
