using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFirstDialog : MonoBehaviour
{
    public GameObject _dialog;

    private bool isShow = false;

    private void OnMouseDown()
    {
        if (!isShow)
        {
            _dialog.SetActive(true);

            isShow = true;
        }
    }
}
