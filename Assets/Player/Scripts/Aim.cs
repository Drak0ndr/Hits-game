using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    public Image _aim;

    private void Start()
    {
        _aim.enabled = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _aim.enabled = true;
        }

        if (Input.GetMouseButtonUp(2))
        {
            _aim.enabled = false;
        }
    }
}
