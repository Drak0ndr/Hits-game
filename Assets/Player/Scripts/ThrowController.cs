using Player;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public Animator _animator;
    public GameObject _player;
    public GameObject _stone;
    public GameObject _stoneInHand;
    public Camera _mainCamera;
    public Transform _hand;

    private float shootForce = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GlobalsVar.isFirstFight)
        {
            _animator.SetBool("isThrow", true);
            _stoneInHand.SetActive(true);   
        }
    }

    public void ThrowAttack()
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

        Vector3 dirWithoutSpread = targetPoint - _hand.position;

        GameObject currentBullet = Instantiate(_stone, _hand.position + new Vector3(0f, 1f, 0f), Quaternion.identity);

        currentBullet.transform.forward = dirWithoutSpread.normalized;

        if (currentBullet != null)
        {
            _stoneInHand.SetActive(false);

            currentBullet.GetComponent<Rigidbody>().AddForce(dirWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        } 
    }

    public void SetIsThrowFalse()
    {
        _animator.SetBool("isThrow", false);
    }
}
