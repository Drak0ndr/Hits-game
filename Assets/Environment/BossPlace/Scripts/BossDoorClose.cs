using System.Collections.Generic;
using UnityEngine;

public class BossDoorClose : MonoBehaviour
{
    public Animator _animator;
    public GameObject _player;
    public List<GameObject> _doors;

    private void Update()
    {
        if (_animator.GetBool("isOpen") && (_player.transform.position.x < 65f
            || _player.transform.position.z < 490f))
        {
            _animator.SetBool("isClose", true);
            _animator.SetBool("isOpen", false);

            foreach (GameObject door in _doors)
            {
                door.SetActive(true);
            }

            Invoke(nameof(ResetDoorClose), 2f);
        }
    }

    private void ResetDoorClose()
    {
        _animator.SetBool("isClose", false);
    }
}
