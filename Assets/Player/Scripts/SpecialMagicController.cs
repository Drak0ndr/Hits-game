using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpecialMagicController : MonoBehaviour
{
    List<string> specialMagicNames = new List<string>(new string[] { "SpecialMagic1", "SpecialMagic2",
    "SpecialMagic3", "SpecialMagic4", "SpecialMagic5"});
    public Animator _animator;
    public int combonum;
    public float exit;
    public float exitTime;

    public void ResetCombonum()
    {
        combonum = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && combonum < 5)
        {
            _animator.SetTrigger(specialMagicNames[combonum]);
            combonum++;
            exit = 0f;
        }

        if(combonum > 0)
        {
            exit += Time.deltaTime;

            if(exit > exitTime) {
                _animator.SetTrigger("Exit");
                combonum = 0;
            }
        }

        if (combonum == 5)
        {
            exitTime = _animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
        }
        else
        {
            exitTime = _animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
        }
    }
}
