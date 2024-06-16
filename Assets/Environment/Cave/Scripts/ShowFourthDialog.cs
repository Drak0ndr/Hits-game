using Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ShowFourthDialog : MonoBehaviour
{
    public GameObject _fourthDialog;
    public GameObject _player;

    private float movementSpeed = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Player" || other.name == "Collider") && GlobalsVar.isBasicMagicalAbilities)
        {
            if(!GlobalsVar.isFourthDialog)
            {
                GlobalsVar.isFourthDialog = true;

                ShowDialog();
            }
        }
    }

    private async void ShowDialog()
    {
        await Task.Delay(2000);

        _fourthDialog.SetActive(true);      
    }

    public void Flight()
    {
        GlobalsVar.isBirdFlight = true;
    }
}
