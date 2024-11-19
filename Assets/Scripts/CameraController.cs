using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject _player;
    [SerializeField] private Animator _animator;

    public Vector3 _offset = new Vector3(0, 3.5f, 8f);




    public void Start()
    {
        _player = FindFirstObjectByType<GameManager>()._selectedCar.gameObject;
    }

    public void CameraShake()
    {
        Debug.Log(_animator.GetBool("GetDamage"));
        _animator.SetBool("GetDamage", true);
        StartCoroutine(ResumeWithDelay(0.5f));


    }
    private IEnumerator ResumeWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Æה¸ל נואכüםמו גנולÿ
        _animator.SetBool("GetDamage", false);
    }



    private void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;

    }
}
