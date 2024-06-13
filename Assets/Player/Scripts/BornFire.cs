using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem.HID;
using Player;

public class BornFire : MonoBehaviour
{
    public GameObject _fire;
    private void Start()
    {
        _fire.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if(GlobalsVar.isBasicMagicalAbilities) {
                Born();
            }   
        }
    }

    private async void Born()
    {
        await Task.Delay(400);
        _fire.SetActive(true);
        await Task.Delay(1400);
        _fire.SetActive(false);
    }
}
