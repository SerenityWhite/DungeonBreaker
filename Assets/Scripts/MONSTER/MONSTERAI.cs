using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.AI;

public class MONSTERAI : MonoBehaviour {

    public TextAsset jsonM;//몬스터 테이블
    public float ckP;//플레이어 체크거리
    public float ckRun;//플레이어 전투가 벗어난거리
    public float atMR;//몬스터 공격전급 범위
    public float rotationSpeed;//플레이어에게 회전값
    public float monsterAT;//몬스터 공격력
    public float monsterHP;//몬스터 HP
    public float monsterSP;//몬스터 움직이는 SPEEED;
    public float monsterSkill;//몬스터가 사용한 스킬
    public float monsterwhy;//몬스터 종류구분
    public float limtP;//몬스터 체력따른변화
    public bool playerck;//플레이어 발견 상태
    public bool monsterATing;//몬스터 공격중이다    
    public bool playerRun;//플레이어가 도망갓다 
    public GameObject playertaget;//플레이어 오브젝트 확인    
    public float pHP;//플레이어 HP 상태 확인
    public float pR;//플레이어 위치
    public CharacterController monstercon;//정규화를 위한 콘트롤          
    public GameObject hitboxObj;//데미지 줄수있는 범위 박스
    public Rigidbody rb;//몬스터 물리력 정의
    public float hitpow;//넉백거리//변수모음

    public enum ENEMYSTATE
    {
        IDLE = 0,
        MOVE,
        ATTACK,
        DAMAGE,
        DEAD,
        boss
    }
    public ENEMYSTATE ememyState = ENEMYSTATE.IDLE;//상태 정의
    
    void Awake()
    {        
       // var C = JSON.Parse(jsonM.text);       
        monsterAT = 1;
        monsterHP = 30;
        monsterSP = 1;
        rotationSpeed = 1;        
    }// 겜 시작전

    private void Start()
    {
        playertaget = GameObject.Find("Player");//플레이어오브젝트 정의
        rb = GetComponent<Rigidbody>();//오브젝트 물리력정의
    }//겜 시작후 먼해야야하는것

    private void Update()
    {        
        switch (ememyState)
        {
            case ENEMYSTATE.IDLE://대기상태
                GetComponent<Animator>().SetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("AT");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");//에니메이션 set
                if (playerck==true)//플레이어 찾으면
                {                   
                    ememyState = ENEMYSTATE.MOVE;
                }
                /*if (playerRun == true)//플레이어 도망시
                {
                    ememyState = ENEMYSTATE.IDLE;
                }*/
                break;
            case ENEMYSTATE.MOVE://이동 상태
                GetComponent<Animator>().ResetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("AT");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().SetTrigger("MOVE");//에니메이션 set         
                
                Moveobj();//타겟으로 이동

                float distabce = (playertaget.transform.position - gameObject.transform.position).magnitude;//공격거리정규화

                if (distabce <atMR)//공격거리 도달시
                {
                    ememyState = ENEMYSTATE.ATTACK;
                }

                /*if (playerRun == true)//플레이어 도망시
                {
                    ememyState = ENEMYSTATE.IDLE;
                }*/

                break;
            case ENEMYSTATE.ATTACK://공격 상태              
                GetComponent<Animator>().ResetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");
                GetComponent<Animator>().SetTrigger("AT"); //에니메이션 Set
                hitboxObj.GetComponent<Hitbox>().ating = true;//히트박스공격 감지 ON
                float distabce1 = (playertaget.transform.position - transform.position).magnitude;//공격거리정규화

                if (distabce1 > atMR)//공격거리 벗어낫을떄
                {
                    ememyState = ENEMYSTATE.MOVE;
                }
                /*if (playerRun == true)//플레이어 도망시
                {
                    ememyState = ENEMYSTATE.IDLE;
                }*/
                break;
            case ENEMYSTATE.DAMAGE://데미지 상태
                GetComponent<Animator>().SetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("STAE");               
                GetComponent<Animator>().ResetTrigger("AT");
                GetComponent<Animator>().ResetTrigger("MOVE");//에니메이션 Set       
                //코루틴 줄가?
                ememyState = ENEMYSTATE.IDLE;
                if (monsterHP <= 0)
                {
                    ememyState = ENEMYSTATE.DEAD;
                }
                /* if (monsterwhy == 1 && monsterHP < limtP)
                 {
                     //보스몬스터면 HP따른 분기
                 }*/
                /*if (playerRun == true)//플레이 도망확인
                {
                    ememyState = ENEMYSTATE.IDLE;
                } */              
                break;
            case ENEMYSTATE.DEAD:                              
                GetComponent<Animator>().ResetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("AT");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");
                GetComponent<Animator>().SetTrigger("DED");//에니메이션 Set
                Destroy(gameObject, 1f);//오브젝트 파괴
                break;
            case ENEMYSTATE.boss://보스 패턴
                /*if()
                { }*/
                break;
            default:
                break;
        }
    }//매프레임마다 작동

    public void Hit()
    {
        ememyState = ENEMYSTATE.DAMAGE;
        Vector3 a = transform.forward * -12;//뒤에숫자는 플레이어 한테 받게끔
        rb.velocity = a;
        StartCoroutine(Wait01());
        Debug.Log("hit적용중");        
    }//데미지를 받을때

    public void Moveobj()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = playertaget.transform.position;            
    }//타겟을 찾아라 타겟으로 이동하라
    
    IEnumerator Wait01()
    {
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(0.4f);
            rb.velocity = new Vector3(0, 0, 0);//해당 물리력 삭제
            i++;
        }
    }//쿨타임

}
