using UnityEngine;

public class ShowBook : MonoBehaviour
{
    public GameObject _book;

    private void OnMouseDown()
    {
        _book.SetActive(true);   
    }
}
