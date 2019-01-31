using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Playerhitbox : MonoBehaviour
{//플레이어 공격범위
    public TextAsset jsonM;//몬스터 데이터 받는곳?
    public bool ating;//공격중이니?
    public float intime;//히트박스 생성 시간은?         
    public float boxs;//히트박스 크기조절할꺼니?
    public GameObject hitdtaget;//맞는 대상이야?
    public GameObject myobj;//누구 데미지임?//변수모음

    void Awake()
    {
        // var C = JSON.Parse(jsonM.text);       
    }//겜시작건 먼저해야하는것

    private void Start()
    {
        myobj = GameObject.Find("Player");
        //boxs 박스 크기값조정
    }//겜시작후 먼저해야하는것

    void Update()
    {
        ating = myobj.GetComponent<PlayerController>().atingP;
    }//매프래임시
   
    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "Monster")
        {
            hitdtaget = col.gameObject;                       
            //player backpow 적용 (넉백)
        }
    }//범위에 들어올때

}

