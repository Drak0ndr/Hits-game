using Player;
using System.Threading.Tasks;
using UnityEngine;

public class ShowFourthDialog : MonoBehaviour
{
    public GameObject _fourthDialog;
    public GameObject _player;

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
        
        Cursor.visible = true;
    }

    public void Flight()
    {
        GlobalsVar.isBirdFlight = true;
    }
}
