using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Damage : MonoBehaviour
    {
        public void DamageToPlayer()
        {
            GlobalsVar.PlayerHP -= 1f;
        }
    }
}

