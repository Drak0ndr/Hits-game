using UnityEngine;

public class MovementColumn : MonoBehaviour
{
    public Animator _animator;


    private void OnTriggerStay(Collider other)
    {
        _animator.SetBool("isMovement", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.SetBool("isMovement", false);
    }
}
