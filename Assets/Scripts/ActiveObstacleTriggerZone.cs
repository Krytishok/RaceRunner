using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private Animator animator; // Ссылка на Animator родительского объекта
    private bool Active  = false; // Флаг для контроля генерации
    //private CameraController camControll;
    public ParticleSystem partSystem;

    private void Start()
    {
        // Ищем Animator на родительском объекте
        animator = GetComponentInParent<Animator>();
        //camControll = FindFirstObjectByType<CameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что объект с тегом "Player" входит в триггер
        if (other.CompareTag("Player") && !(Active))
        {
            // Запускаем анимацию, если Animator найден
            if (animator != null)
            {
                partSystem.Play();
                animator.SetBool("ActiveRise", true); // Укажите имя анимации
                //camControll.CameraShake();
            }
            Active = true;
        }
    }
}
