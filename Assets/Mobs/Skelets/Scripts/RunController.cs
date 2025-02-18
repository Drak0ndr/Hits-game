using UnityEngine;

public class RunController : MonoBehaviour
{
    public GameObject _player;
    public Animator _animator;

    private float runRange = 8f;
    private float speed = 2f;

    void FixedUpdate()
    {
        if (this._animator != null)
        {
            float dist = Vector3.Distance(_player.transform.position, transform.position);

            if (dist > runRange && _animator.GetBool("isIdle"))
            {
                _animator.SetBool("isRun", false);
            }

            else if (_animator.GetBool("isIdle"))
            {
                _animator.SetBool("isRun", true);

                float step = speed * Time.deltaTime;
                transform.LookAt(_player.transform.position);
                this.transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, step);
            }
        } 
    }
}
