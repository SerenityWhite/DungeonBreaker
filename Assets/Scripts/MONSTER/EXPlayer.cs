using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EXPlayer : MonoBehaviour {
    public Vector3 movepoint;
    public Camera came;
    public GameObject monster;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public void Update()
    {
        GetComponent<Animator>().SetBool("HIT", false);
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;        
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;       
        transform.Translate(0, 0, translation);        
        transform.Rotate(0, rotation, 0);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ATskill1();
        }
    }
    public void Moveobj()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = movepoint;
    }
    public void ATskill1()
    {
        monster.GetComponent<MONSTERAI>().hit();
        GetComponent<Animator>().SetBool("HIT", true);
    }
}
