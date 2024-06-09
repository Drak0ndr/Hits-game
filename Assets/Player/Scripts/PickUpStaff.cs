using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PickUpStaff : MonoBehaviour
{
    public GameObject _camera;
    private GameObject _currentItem;

    public float distance = 50f;

    bool canPickUp = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) PickUpAsync();
        if (Input.GetKeyDown(KeyCode.X)) Drop();
    }

    private async void PickUpAsync()
    {
        await Task.Delay(2000);

        RaycastHit hit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Staff")
            {
                if (canPickUp) Drop();

                _currentItem = hit.transform.gameObject;
                _currentItem.GetComponent<Rigidbody>().isKinematic = true;
                _currentItem.GetComponent<Collider>().isTrigger = true;
                _currentItem.transform.parent = transform;
                _currentItem.transform.localPosition = Vector3.zero;
                _currentItem.transform.localEulerAngles = new Vector3(22f, -187f, -17f);

                canPickUp = true;
            }
        }
    }

    private void Drop()
    {
        if (_currentItem != null)
        {
            _currentItem.transform.parent = null;
            _currentItem.GetComponent<Rigidbody>().isKinematic = false;
            _currentItem.GetComponent<Collider>().isTrigger = false;
            canPickUp = false;
            _currentItem = null;
        }  
    }
}
