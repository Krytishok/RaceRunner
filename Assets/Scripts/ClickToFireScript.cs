using UnityEngine;

public class ClickToFireScript : MonoBehaviour
{
    [SerializeField] GameObject _buttonBody;

    private CarController _player;
    private EnemyController _enemy;

    private int _numberOfShots = 3;


    private void Start()
    {
        _buttonBody.SetActive(false);
        _player = FindFirstObjectByType<CarController>();


    }
    public void SetClickButton(bool flag)
    {
        _buttonBody.SetActive(flag);
    }

    public void OnClick()
    {

        FindFirstObjectByType<EnemyController>().GetDamage(_player._firePower);
    }


}
