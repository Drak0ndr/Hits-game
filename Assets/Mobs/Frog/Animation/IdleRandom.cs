using UnityEngine;

public class IdleRandom : StateMachineBehaviour
{
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("IdleId", Random.Range(0, 8));
    }   
}
