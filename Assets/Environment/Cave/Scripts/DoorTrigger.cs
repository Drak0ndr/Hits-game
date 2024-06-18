using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if(_animator != null)
        {
            _animator.SetBool("isClose", true);
        } 
    }
}
