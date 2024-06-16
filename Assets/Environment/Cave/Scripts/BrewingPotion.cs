using Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BrewingPotion : MonoBehaviour
{
    public GameObject _empty;
    public GameObject _potion;
    public GameObject _slash;
    public GameObject _dialog;

    private int countPotions = 0;
    private bool isMagic = false;
    void Update()
    {
        if (!isMagic)
        {
            GameObject[] potions = GameObject.FindGameObjectsWithTag("Potion");

            if (potions.Length > 0)
            {
                foreach (var potion in potions)
                {
                    float dist = Vector3.Distance(potion.transform.position, this.transform.position);

                    if (dist < 1f)
                    {
                        Destroy(potion.gameObject);

                        countPotions++;

                    }
                }
            }

            if (countPotions == 4)
            {
                _empty.SetActive(false);
                _potion.SetActive(true);

                StartMagic();
            }
        } 
    }

    private async void StartMagic()
    {
        isMagic = true;
        countPotions = 0;

        await Task.Delay(4000);

        _slash.SetActive(true);

        await Task.Delay(2000);

        Destroy(_slash);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 objPosition = player.transform.position;
        objPosition.z += 2.5f;
        objPosition.y += 1.5f;

        player.transform.position = objPosition;

        _empty.SetActive(true);
        _potion.SetActive(false);

        GlobalsVar.isBasicMagicalAbilities = true;

        GlobalsVar.HP = 100;

        await Task.Delay(1500);

        _dialog.SetActive(true);
    }
}
