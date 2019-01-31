using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.AI;

public class MONSTERAI02 : MonoBehaviour
{
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
    public bool playerck;//플레이어 확인상태
    public bool monsterATing;//몬스터 공격상태
    public bool monsterRow;//원거리 몬스터공격
    public bool playerRun;//플레이어 도망감
    public GameObject playertaget;//플레이어 오브젝트 확인    
    public float pHP;//플레이어 HP 상태 확인
    public float pR;//플레이어 위치
    public CharacterController monstercon;//정규화를 윈한 컨트롤 정의
    public GameObject arow;//생성되는 투사체      
    public Rigidbody rb;//몬스터 물리력
    public float hitpow;//너백거리//변수모음
    public float i1;//limt 변수
    public float rs;//로테이션 스피드
    public GameObject my;
    public float att;//공격모션 딜레이

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
        playertaget = GameObject.Find("Player");//플레이어 오브젝트 정의
        rb = GetComponent<Rigidbody>();//리지드바디 정의
        my = gameObject;
    }//겜 시작후 먼해야야하는것

    private void Update()
    {
        switch (ememyState)
        {
            case ENEMYSTATE.IDLE://대기상태
                GetComponent<Animator>().SetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("AT01");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");//에니메이션 SET
                if (playerck == true)//플레이어 발견하면 이동상태로
                {
                    ememyState = ENEMYSTATE.MOVE;
                }
                /*if (playerRun == true)//플레이어가 도망가면 조건넣자 추가상의기능구현
                {
                    ememyState = ENEMYSTATE.IDLE;
                }*/
                break;
            case ENEMYSTATE.MOVE://이동 상태
                GetComponent<Animator>().ResetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("AT01");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().SetTrigger("MOVE");    //에니메이션 SET      
                float distabce = (playertaget.transform.position - gameObject.transform.position).magnitude;//공격거리 정규화
                if (distabce < atMR)//공격거리 도달시
                {
                    ememyState = ENEMYSTATE.ATTACK;
                }               
                if (distabce > atMR)
                {
                    Moveobj();//이동함수 호출               
                }
                /*if (playerRun == true)//플레이어가 도망가면 조건넣자 추가상의기능구현
                {
                    ememyState = ENEMYSTATE.IDLE;
                }*/
                break;
            case ENEMYSTATE.ATTACK://공격하는 상태               
                GetComponent<Animator>().ResetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");
                GetComponent<Animator>().SetTrigger("AT01");//에니메이션 SET                     
                Vector3 dir = playertaget.transform.position - transform.position;
                dir.y = 0f;
                dir.Normalize();
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rs * Time.deltaTime);
                if (i1== 0)
                {                  
                    Instantiate(arow,transform);
                    StartCoroutine(Wait02());
                    i1 = 1;
                }              
                float distabce1 = (playertaget.transform.position - transform.position).magnitude;//공격거리 정규화
                if (distabce1 > atMR)//공격거리와 멀어지면
                {                    
                    ememyState = ENEMYSTATE.MOVE;
                }
                /*if (playerRun == true)//플레이어가 도망가면 조건넣자 추가상의기능구현
                {
                    ememyState = ENEMYSTATE.IDLE;
                }*/
                break;
            case ENEMYSTATE.DAMAGE://데미지받은 상태
                GetComponent<Animator>().SetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("AT01");
                GetComponent<Animator>().ResetTrigger("MOVE");//에니메이션 SET                                  
                ememyState = ENEMYSTATE.IDLE;
                if (monsterHP <= 0)//몬스터 hp0 이면
                {
                    ememyState = ENEMYSTATE.DEAD;
                }
                /*if (monsterwhy == 1 && monsterHP < limtP)//패턴 분기점
                {
                    //보스몬스터면 HP따른 분기
                }*/

                /*if (playerRun == true)//플레이어가 도망가면 조건넣자 추가상의기능구현
                {
                    ememyState = ENEMYSTATE.IDLE;
                }*/
                break;
            case ENEMYSTATE.DEAD://죽은상태
                GetComponent<Animator>().ResetTrigger("STAE");
                GetComponent<Animator>().ResetTrigger("AT01");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");
                GetComponent<Animator>().SetTrigger("DED");//에니메이션 SET
                Destroy(gameObject, 1f);//오브젝트 사라짐
                break;
            case ENEMYSTATE.boss://보스 패턴 돌입
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
        Vector3 a = transform.forward * -5;
        rb.velocity = a;
        StartCoroutine(Wait01());
        Debug.Log("hit적용중");
    }//데미지를 받을때

    public void Moveobj()
    {        
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = playertaget.transform.position;
        
    }//타겟을 찾아고 타겟으로이동하라

    IEnumerator Wait01()
    {
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(0.4f);
            rb.velocity = new Vector3(0, 0, 0);
            i++;
        }
    }//쿨타임
    IEnumerator Wait02()
    {
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(att);
            i1 = 0;
            i++;
        }
    }//쿨타임2

}
