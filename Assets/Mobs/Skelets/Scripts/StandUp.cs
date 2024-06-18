using UnityEngine;
using System.Threading.Tasks;

public class StandUp : MonoBehaviour
{
    public GameObject _player;
    public Animator _animator;

    private float standUpRange = 4f;

    private void Update()
    {
        if(this.gameObject != null)
        {
            float dist = Vector3.Distance(_player.transform.position, transform.position);

            if (dist < standUpRange)
            {
                _animator.SetBool("isStandUp", true);
                AsyncStandUp();
            }
        }
    }

    private async void AsyncStandUp()
    {
        await Task.Delay(3000);

        if (_animator != null)
        {
            _animator.SetBool("isIdle", true);
        }
    }
}
