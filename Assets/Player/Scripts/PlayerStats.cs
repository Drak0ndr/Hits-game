using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        public Slider _healthBar;
        public Slider _manaBar;

        void Update()
        {
            _healthBar.value = GlobalsVar.PlayerHP;
            _manaBar.value = GlobalsVar.PlayerMANA;
        }
    }
}

