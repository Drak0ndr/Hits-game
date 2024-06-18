using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleRandom : StateMachineBehaviour
{  
   override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
   {
        animator.SetInteger("Id", Random.Range(0, 8));
   }
}
