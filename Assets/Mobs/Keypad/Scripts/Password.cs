using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Threading.Tasks;

public class Password : MonoBehaviour
{
    public GameObject _password;
    public GameObject _player;
    public GameObject _lock;
    public List<GameObject> Doors = new List<GameObject>();

    private List<String> Keys = new List<String>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

    private string _rightPassword = "666";
    private string _currPassword = "";

    private bool isOpen = false;
    private bool isWarning = false;

    private async void Update()
    {
        float dist = Vector3.Distance(_player.transform.position, transform.position);

        if (!isOpen && dist < 3f && !isWarning)
        {
            foreach (String keyCode in Keys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (_currPassword.Length < 3)
                    {
                        _currPassword += keyCode;
                    }
                }
            }

            _password.GetComponent<TextMeshPro>().text = _currPassword;

            if (_currPassword.Length == 3)
            {
                if (_currPassword == _rightPassword)
                {
                    isOpen = true;

                    _lock.GetComponent<Rigidbody>().isKinematic = false;

                    await Task.Delay(2000);

                    Doors[0].transform.localPosition = new Vector3(-0.028f, 0.0554455f, -0.0557f);
                    Doors[0].transform.Rotate(0, 90, 0);

                    Doors[1].transform.localPosition = new Vector3(0.028f, 0.0554455f, -0.0557f);
                    Doors[1].transform.Rotate(0, -90, 0);
                }
                else
                {
                    isWarning = true;

                    await Task.Delay(200);

                    _password.GetComponent<TextMeshPro>().text = "!!!";

                    await Task.Delay(500);

                    _currPassword = "";

                    isWarning = false;
                }
            }
        }
    }
}
