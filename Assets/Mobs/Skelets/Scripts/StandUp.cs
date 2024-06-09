using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UIElements;

public class StandUp : MonoBehaviour
{
    public GameObject _player;
    public Animator _animator;

    private float standUpRange = 6f;

    private void Update()
    {
        float dist = Vector3.Distance(_player.transform.position, transform.position);

        if(dist < standUpRange)
        {
            _animator.SetBool("isStandUp", true);
            AsyncStandUp();
        }
    }

    private async void AsyncStandUp()
    {
        await Task.Delay(3000);
        _animator.SetBool("isIdle", true);
    }
}
