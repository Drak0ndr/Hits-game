using System.Collections.Generic;
using UnityEngine;

public class SpecialMagicController : MonoBehaviour
{
    public Animator _animator;
    public GameObject _player;
    public GameObject _spall;
    public GameObject _circle;
    public Camera _mainCamera;
    public List<GameObject> _spikes;
    public List<Transform> _hands;

    private GameObject circlePosition;
    private float shootForce = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (GameObject _spike in _spikes)
            {
                _spike.SetActive(true);
            }

            circlePosition = Instantiate(_circle, _player.transform.position, Quaternion.identity);

            _animator.SetTrigger("isSpecialMagic");
        }
    }

    public void Attack()
    {
        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 dirWithoutSpread = targetPoint - _hands[0].position;

        GameObject currentBullet = Instantiate(_spall, _hands[0].position + new Vector3(0f, 1f, 0f), Quaternion.identity);

        currentBullet.transform.forward = dirWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(dirWithoutSpread.normalized * shootForce, ForceMode.Impulse);

        Invoke(nameof(DestroySpalls), 2f);
    }

    private void DestroySpalls()
    {
        Destroy(circlePosition);

        foreach (GameObject _spike in _spikes)
        {
            _spike.SetActive(false);
        }
    }
}
