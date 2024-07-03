using UnityEngine;

public class ShowPumpkin : MonoBehaviour
{
    public GameObject _pumpkin;

    void Update()
    {
        _pumpkin.SetActive(true);
    }
}
