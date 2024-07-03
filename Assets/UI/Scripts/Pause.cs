using Player;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public List<GameObject> _images;
    public GameObject _escImage;

    private bool isPause = false;
    private bool isAlreadyPause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GlobalsVar.isTitles && !isPause)
        {
            SetPause();

            isPause = true;

            if (!isAlreadyPause)
            {
                _escImage.SetActive(false);

                isAlreadyPause = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !GlobalsVar.isTitles && isPause)
        {
            SetPlay();

            isPause = false;
        } 
    }

    private void SetPause()
    {
        _images[0].SetActive(false);
        _images[1].SetActive(true);

        Time.timeScale = 0f;
    }

    private void SetPlay()
    {
        _images[0].SetActive(true);
        _images[1].SetActive(false);

        Time.timeScale = 1f;
    }
}
