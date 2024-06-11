using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetBool("isClose", true);
    }
}
