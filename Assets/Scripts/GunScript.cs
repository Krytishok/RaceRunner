using UnityEngine;

public class GunScript : MonoBehaviour
{
    Transform _target;
    
    public void GetTarget(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        
    }

}
