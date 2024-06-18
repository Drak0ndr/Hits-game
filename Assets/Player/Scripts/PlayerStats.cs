using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public Slider _healthBar;
        public Slider _manaBar;
        public Animator _animator;

        private bool isDeath = false;

        void Update()
        {
            if(_healthBar.value != GlobalsVar.PlayerHP && GlobalsVar.PlayerHP > 0)
            {
                if (_healthBar.value > GlobalsVar.PlayerHP)
                {
                    _animator.SetTrigger("isGetHit");
                }

                _healthBar.value = GlobalsVar.PlayerHP <= 100 ? GlobalsVar.PlayerHP : 100;
            }

            /*else if(GlobalsVar.PlayerHP <= 0 && !isDeath)
            {
                isDeath = true;
                _healthBar.value = 0;
                _animator.SetBool("isDeath", true);
            }*/
           
            _manaBar.value = GlobalsVar.PlayerMANA <= 100 ? GlobalsVar.PlayerMANA : 100;
        }

        public void ResetGetHit()
        {
            _animator.SetBool("isGetHit", false);
        }
    }
}

