using UnityEngine;

public class ShowBook : MonoBehaviour
{
    public GameObject _book;
    public GameObject _canvas;
    public GameObject _player;

    private bool isShow = false;

    private void Update()
    {
        if (!isShow)
        {
            float dist = Vector3.Distance(_player.transform.position, transform.position);

            if (dist < 3f)
            {
                _canvas.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.O) && _canvas.activeInHierarchy)
            {
                _book.SetActive(true);

                isShow = true;

                Destroy(_canvas);

                Cursor.visible = true;
            }
        }
    }
}
