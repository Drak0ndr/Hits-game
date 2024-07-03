using UnityEngine;

public class ShowPortal : MonoBehaviour
{
    public GameObject _portal;

    private void Update()
    {
        _portal.SetActive(true);
    }
}
