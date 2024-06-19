using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Darkening : MonoBehaviour
{
    public GameObject UICanvas;
    public GameObject DeathCanvas;
    public GameObject _player;
    public Animator _animator;
    private float darkeningSpeed = 0.2f;


    IEnumerator Start()
    {
        UICanvas.SetActive(false);

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
        _player.transform.position = new Vector3(-147.9f, 15.76f, -264.7f);

        UICanvas.SetActive(true);
        DeathCanvas.SetActive(false);

        GlobalsVar.PlayerHP = 100f;

        _animator.SetTrigger("isNewPosition");
    }
}
