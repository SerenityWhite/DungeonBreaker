using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MONSTERRUNck : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("희미하게보입니다.");            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어가 도망쳤습니다");            
            GetComponentInParent<MONSTERAI>().playerRun = true;
        }
    }    
}
