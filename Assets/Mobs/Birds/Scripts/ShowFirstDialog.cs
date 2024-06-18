using UnityEngine;

public class ShowFirstDialog : MonoBehaviour
{
    public GameObject _dialog;
    public GameObject _canvas;

    private bool isShow = false;

    private void OnMouseDown()
    {
        if (!isShow)
        {
            _dialog.SetActive(true);

            isShow = true;

            Destroy(_canvas);

            this.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
