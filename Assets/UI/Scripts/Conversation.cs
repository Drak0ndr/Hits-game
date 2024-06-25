using UnityEngine;
using Player;

public class Conversation : MonoBehaviour
{
    public GameObject _uiCanvas;
    public GameObject _deathCanvas;
    public GameObject _player;
    public Animator _animator;
    public Transform _revivalPosition; 

    private void LateUpdate()
    {
        _uiCanvas.SetActive(false);

        Cursor.visible = true;

        _player.transform.position = _revivalPosition.position;
    }

    public void Revival()
    {
        _uiCanvas.SetActive(true);

        Cursor.visible = false;

        GlobalsVar.PlayerHP = 100f;
        GlobalsVar.PlayerMANA = 0f;

        _animator.SetTrigger("isNewPosition");

        GlobalsVar.isDeath = false;

        _deathCanvas.SetActive(false);
    }
}
