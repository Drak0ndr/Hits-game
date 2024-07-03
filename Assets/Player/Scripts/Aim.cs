using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    public Image _aim;

    private bool isShow = false;

    private void Start()
    {
        _aim.enabled = false;

        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if(!isShow)
            {
                _aim.enabled = true;

                isShow = true;
            }
            else
            {
                _aim.enabled = false;

                isShow = false;
            }
        }
    }
}
