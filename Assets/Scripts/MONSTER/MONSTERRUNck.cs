using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MONSTERRUNck : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("배틀범위 돌입");            
        }
    }//배틀범위

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("플레이어가 도망쳤습니다");
            if (GetComponentInParent<MONSTERAI>() != null)
            {
                GetComponentInParent<MONSTERAI>().playerRun = true;
            }
            if (GetComponentInParent<MONSTERAI02>() != null)
            {
                GetComponentInParent<MONSTERAI02>().playerRun = true;
            }
        }
    }//플레이어 도망치는거 확인
}
