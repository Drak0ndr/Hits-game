using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public GameObject _button;
    public GameObject _inventory;
    public List<GameObject> _gameObjects;

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && _gameObjects.All(gameObject => gameObject.activeInHierarchy == false))
        {
            if(!isOpen)
            {
                _button.SetActive(false);
                _inventory.SetActive(true);

                isOpen = true;

                Cursor.visible = true;
            }
            else
            {
                _button.SetActive(true);
                _inventory.SetActive(false);

                isOpen = false;

                Cursor.visible = false;
            }
        }
    }

    public void SetIsOpenFalse()
    {
        isOpen = false;

        Cursor.visible = false;
    }
}
