using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MONSTERck : MonoBehaviour {
    public Vector3 monsRE;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어를 발견햇습니다.");
            monsRE = transform.position; 
           GetComponentInParent<MONSTERAI>().playerck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("시야 벗어났습니다.");
            GetComponentInParent<MONSTERAI>().playerck = false;
        }
    }
}
