using UnityEngine;

public class BossDoorClose : MonoBehaviour
{
    public Animator _animator;
    public GameObject _player;

    private void Update()
    {
        if (_animator.GetBool("isOpen") && (_player.transform.position.x < 65f
            || _player.transform.position.z < 490f))
        {
            _animator.SetBool("isClose", true);
            _animator.SetBool("isOpen", false);

            Invoke(nameof(ResetDoorClose), 2f);
        }
    }

    private void ResetDoorClose()
    {
        _animator.SetBool("isClose", false);
    }
}
