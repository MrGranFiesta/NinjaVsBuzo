using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Menu[] menus;

    private void Awake()
    {
        Debug.Log("MenuManager - Awake");
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        Debug.Log("MenuManager - OpenMenu1");
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                menus[i].Close();
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        Debug.Log("MenuManager - OpenMenu2");
        // First, close the currently open menu
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        Debug.Log("MenuManager - CloseMenu");
        menu.Close();
    }
}
