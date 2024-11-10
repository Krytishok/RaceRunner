using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    public void LookCloser(Transform transform, Vector3 _offset)
    {
        gameObject.transform.position = transform.position + _offset;
    }
}
