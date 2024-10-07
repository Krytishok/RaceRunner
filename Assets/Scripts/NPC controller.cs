using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class NPCcontroller : MonoBehaviour
{

    [SerializeField] GameObject npc1;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] int speed;
    void Update()
    {
       transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
    }

}
