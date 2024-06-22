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
        public GameObject _value;
        public GameObject _explosion;
        public GameObject _firstDeathCanvas;
        public GameObject _deathCanvas;

        private bool isGetHit = false;
        private bool isStart = false;
        private bool alreadyIcrease = false;

        void Update()
        {
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

            else if(GlobalsVar.PlayerHP <= 0 && !GlobalsVar.isDeath)
            {
                GlobalsVar.isDeath = true;
                _healthBar.value = 0;
                _animator.SetTrigger("isDeath");
                _animator.SetBool("isDeathNow", true);
            }
           
            _manaBar.value = GlobalsVar.PlayerMANA <= 100 ? GlobalsVar.PlayerMANA : 100;

            if (_manaBar.value == 100)
            {
                _value.GetComponent<Image>().color = new Color32(0, 70, 225, 255);
            }
            else
            {
                _value.GetComponent<Image>().color = new Color32(0, 170, 225, 255);
            }

            if(GlobalsVar.isSpecialMagicalAbilities && !alreadyIcrease)
            {
                alreadyIcrease = true;

                Invoke(nameof(IncreaseMana), 0.5f);
            }
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
            else
            {
                Invoke(nameof(Death), 3f);
            } 
        }

        private void Death()
        {
            GlobalsVar.isFight = false;

            _deathCanvas.SetActive(true);

            _animator.SetBool("isDeathNow", false);
        }

        private void IncreaseMana()
        {
            GlobalsVar.PlayerMANA += 1f;

            alreadyIcrease = false;
        }
    }
}

