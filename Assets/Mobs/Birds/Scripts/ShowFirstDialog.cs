using System.Collections.Generic;
using UnityEngine;

public class ShowFirstDialog : MonoBehaviour
{
    public GameObject _dialog;
    public GameObject _canvas;
    public GameObject _player;
    public List<GameObject> _images;

    private bool isShow = false;

    private void Update()
    {
        if (!isShow)
        {
            float dist = Vector3.Distance(_player.transform.position, transform.position);

            if (dist < 2.5f)
            {
                _images[0].SetActive(false);
                _images[1].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                _dialog.SetActive(true);

                isShow = true;

                Destroy(_canvas);

                Cursor.visible = true;
            }
        }
    }

    public void SetCursorVisibleFalse()
    {
        Cursor.visible = false;
    }

    public void SetCursorVisibleTrue()
    {
        Cursor.visible = true;
    }
}
