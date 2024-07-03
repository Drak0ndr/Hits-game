using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public List<GameObject> _images;

    private bool isPause = false;   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isPause)
        {
            SetPause();

            isPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
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
