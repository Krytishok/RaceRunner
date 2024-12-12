using Boxophobic.Utils;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    private int _damage = 7;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Определяем направление отталкивания (от центра границы к машинке)
            Vector3 pushDirection = other.transform.position - transform.position;

            // Указываем силу отталкивания
            float pushForce = 2f; // Настраиваемая величина

            // Отталкиваем машинку
            other.GetComponent<CarController>().PushFromBorder(pushDirection, pushForce, _damage);
        }
    }

   
}
