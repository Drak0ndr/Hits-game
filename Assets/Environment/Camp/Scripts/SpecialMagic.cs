using Player;
using UnityEngine;

public class SpecialMagic : MonoBehaviour
{
    public GameObject _explotion;

    void Update()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        GameObject _fire = GameObject.Find("GreenFire(Clone)");

        if (_fire != null)
        {
            float dist = Vector3.Distance(_fire.transform.position, transform.position);

            if (dist < 1.5)
            {
                Destroy(this.gameObject);

                GameObject _newExplotion = Instantiate(_explotion, _player.transform.position, Quaternion.identity);
                Destroy(_newExplotion, 2f);

                GlobalsVar.PlayerMANA = 100f;
                GlobalsVar.isSpecialMagicalAbilities = true;
            }
        }
    }
}
