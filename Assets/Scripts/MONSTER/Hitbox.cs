using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Hitbox : MonoBehaviour
{
    public TextAsset jsonM;//몬스터 데이터 받는곳?
    public bool ating;//공격중이니?
    public float intime;//히트박스 생성 시간은?    
    public float backpow;//넉백돼냐?넉백값은?
    public float atp;//데미지 적용해서 플레이어한테 주는값은?
    public float boxs;//히트박스 크기조절할꺼니?
    public GameObject hitdtaget;//맞는 대상이야?
    public GameObject myobj;//누구 데미지임?
    public GameObject intext;
    void Awake()
    {
        // var C = JSON.Parse(jsonM.text);       
    }
    private void Start()
    {                      
        hitdtaget = GameObject.Find("Player");
        //boxs 박스 크기값조정
    }
    void Update()
    {
        if (ating == true)
        {           
            Countmove();//쿨타임 함수
        }        
    }
    public void Countmove()
    {        
        intime -= Time.deltaTime;//제한시간 시작;
        if (intime <= 0)//제한시간 끝나면
        {
            ating = false;//공격모션끝 
            intime = 1;//초기화            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("데미지 적용");
            hitdtaget.GetComponent<Animator>().SetTrigger("HITP");
            hitdtaget.GetComponent<TEXTEX>().Textadd();
            //player backpow 적용 (넉백)
        }
    }
}
