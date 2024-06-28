using Player;
using UnityEngine;

public class SpecialMagicListener : MonoBehaviour
{
    public GameObject _canvas;

    private bool isShow = false;

    void Update()
    {
        if (!isShow && GlobalsVar.isSpecialMagicalAbilities)
        {
            isShow = true;

            _canvas.SetActive(true);
        }
    }
}
