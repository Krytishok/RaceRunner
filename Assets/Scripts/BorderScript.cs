using UnityEngine;

public class BorderScript : MonoBehaviour
{
    [SerializeField] private float bounceForce = 20f; // Сила отталкивания

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Определяем направление отталкивания (направление от границы к машине)
                Vector3 bounceDirection = (other.transform.position - transform.position).normalized;

                // Убираем движение вдоль оси Z (только горизонтальное)
                bounceDirection.z = 0;

                // Добавляем силу отталкивания
                rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

                // Отключаем движение машины временно
                other.GetComponent<CarController>()?.ChangeMoveCoef(0f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Восстанавливаем движение
            other.GetComponent<CarController>()?.ChangeMoveCoef(1f);
        }
    }
}
