using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    public Transform player;     
    
   

    private Vector3 offset;

    private void Start()
    {
        player = GameObject.Find("PlayerTransform").GetComponent<Transform>();

        offset = transform.position - player.transform.position;
    }
    



    void Update()
    {
        

        transform.position = player.transform.position + offset;
       

    }
}




   
