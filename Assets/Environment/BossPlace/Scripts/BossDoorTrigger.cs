using Player;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorTrigger : MonoBehaviour
{
    public Animator _animator;
    public GameObject _player;
    public List<GameObject> _doors;

    private void OnTriggerStay(Collider other)
    {
        if ((other.name == "Player" || other.name == "Collider") 
            && !GlobalsVar.isFirstFight && !GlobalsVar.isFight)
        {
            _animator.SetBool("isOpen", true);

            GlobalsVar.isFight = true;

            foreach(GameObject door in _doors)
            {
                door.SetActive(false);
            }
        }
    }
}
