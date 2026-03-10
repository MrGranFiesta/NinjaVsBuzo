using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;

    private void Start()
    {
        Debug.Log("Menu - Start");
        gameObject.SetActive(open);
    }

    public void Open()
    {
        Debug.Log("Menu - Open");
        open = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("Menu - Close");
        open = false;
        gameObject.SetActive(false);
    }
}
