using Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Darkening : MonoBehaviour
{
    public GameObject _uiCanvas;
    public GameObject _hint;
    public GameObject _deathCanvas;
    public GameObject _player;
    public GameObject _stone;
    public Animator _animator;

    private float darkeningSpeed = 0.2f;


    IEnumerator Start()
    {
        Destroy(_hint);

        _uiCanvas.SetActive(false);

        RawImage darkeningImage = GetComponent<RawImage>();
        Color color = darkeningImage.color;

        while(color.a < 1f)
        {
            color.a += darkeningSpeed * Time.deltaTime;
            darkeningImage.color = color;
            yield return null;
        }

        Invoke(nameof(ShowSecondPosition), 3f);
    }

    private void ShowSecondPosition()
    {
        _stone.SetActive(false);

        _player.transform.position = new Vector3(-147.9f, 15.76f, -264.7f);

        _uiCanvas.SetActive(true);
        _deathCanvas.SetActive(false);

        GlobalsVar.PlayerHP = 100f;

        _animator.SetTrigger("isNewPosition");
        _animator.SetBool("isDeathNow", false);

        GlobalsVar.isDeath = false;
        GlobalsVar.isWasInCamp = true;
    }
}
