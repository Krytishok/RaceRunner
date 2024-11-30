using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private Animator animator; // ������ �� Animator ������������� �������
    private bool Active  = false; // ���� ��� �������� ���������
    //private CameraController camControll;
    public ParticleSystem partSystem;

    private void Start()
    {
        // ���� Animator �� ������������ �������
        animator = GetComponentInParent<Animator>();
        //camControll = FindFirstObjectByType<CameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ������ � ����� "Player" ������ � �������
        if (other.CompareTag("Player") && !(Active))
        {
            // ��������� ��������, ���� Animator ������
            if (animator != null)
            {
                partSystem.Play();
                animator.SetBool("ActiveRise", true); // ������� ��� ��������
                //camControll.CameraShake();
            }
            Active = true;
        }
    }
}
