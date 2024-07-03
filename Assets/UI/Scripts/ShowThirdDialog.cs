using Player;
using UnityEngine;

public class ShowThirdDialog : MonoBehaviour
{
    public GameObject _dialog;

    void Update()
    {
        if (GlobalsVar.isThirdDialog)
        {
            _dialog.SetActive(true);

            GlobalsVar.isThirdDialog = false;   
        }
    }
}
