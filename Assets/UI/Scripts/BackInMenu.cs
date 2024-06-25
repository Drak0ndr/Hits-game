using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackInMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && GlobalsVar.isTitles)
        {
            EndOfGame();
        }
    }
    private void EndOfGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
