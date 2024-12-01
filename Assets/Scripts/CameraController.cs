using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Animator _animator;





    

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

    public void CameraAcceleration(bool flag)
    {
        _animator.SetBool("GetNitro", flag);
    }

    public void CameraToGun(bool flag)
    {
        _animator.SetBool("GetWeapon", flag);
    }



    
}
