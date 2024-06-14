using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" || other.name == "Collider")
        {
            GlobalsVar._magicRotate = 0f;
        }
    }
}
