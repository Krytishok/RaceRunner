using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField] Transform _garagePostion;
    [SerializeField] Transform _mainMenuPosition;
    [SerializeField] Transform _selectCarPosition;


    private void Start()
    {
        ToMainMenuView();
    }

    public void ToGarageView()
    {
        transform.position = _garagePostion.position;
        transform.rotation = _garagePostion.rotation;
    }
    public void ToMainMenuView()
    {
        transform.position = _mainMenuPosition.position;
        transform.rotation = _mainMenuPosition.rotation;
    }
    public void ToSelectView()
    {
        transform.position = _selectCarPosition.position;
        transform.rotation = _selectCarPosition.rotation;
    }
    public void ToPosition(Transform inputTransform)
    {
        transform.position = inputTransform.position;
        transform.rotation = inputTransform.rotation;
    }
}
