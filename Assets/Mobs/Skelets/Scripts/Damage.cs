using UnityEngine;

namespace Player
{
    public class Damage : MonoBehaviour
    {
        public void DamageToPlayer()
        {
            GlobalsVar.PlayerHP -= 0.5f;
        }
    }
}

