using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public Slider _healthBar;
        public Slider _manaBar;
        public Animator _animator;
        public GameObject _explosion;
        public GameObject _uiCanvas;
        public GameObject _firstDeathCanvas;

        private bool isDeath = false;
        private bool isGetHit = false;
        private bool isStart = false;

        void Update()
        {
            /*print(_healthBar.value);*/

            if(_healthBar.value != GlobalsVar.PlayerHP && GlobalsVar.PlayerHP > 0)
            {
                if (_healthBar.value > GlobalsVar.PlayerHP)
                {
                    _animator.SetTrigger("isGetHit");

                    isGetHit = true;
                }

                if(GlobalsVar.isFirstFight && isGetHit && !isStart)
                {
                    isStart = true;

                    Invoke(nameof(DecreaseHP), 2f);
                }

                _healthBar.value = GlobalsVar.PlayerHP <= 100 ? GlobalsVar.PlayerHP : 100;
            }

            else if(GlobalsVar.PlayerHP <= 0 && !isDeath)
            {
                isDeath = true;
                _healthBar.value = 0;
                _animator.SetTrigger("isDeath");
            }
           
            _manaBar.value = GlobalsVar.PlayerMANA <= 100 ? GlobalsVar.PlayerMANA : 100;
        }

        public void ResetGetHit()
        {
            _animator.SetBool("isGetHit", false);
        }

        private void DecreaseHP()
        { 
            GameObject newExplosion = Instantiate(_explosion, 
                this.gameObject.transform.position + new Vector3(0f, 1.2f, 0f), Quaternion.identity);

            Destroy(newExplosion, 0.8f);

            GlobalsVar.PlayerHP -= 3f;

            isStart = false;
        }

        public void FirstDeath()
        {
            if (GlobalsVar.isFirstFight)
            {
                GlobalsVar.isFight = false;
                GlobalsVar.isFirstFight = false;

                _firstDeathCanvas.SetActive(true);
            } 
        }
    }
}

