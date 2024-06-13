using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBook : MonoBehaviour
{
    public GameObject _book;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        _book.SetActive(true);   
    }
}
