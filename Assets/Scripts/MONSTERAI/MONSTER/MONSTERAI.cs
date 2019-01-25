using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.AI;

public class MONSTERAI : MonoBehaviour {

    public TextAsset jsonM;
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
    public bool playerck;
    public bool monsterATing;
    public bool monsterRow;
    public bool playerRun;
    public GameObject playertaget;//플레이어 오브젝트 확인    
    public float pHP;//플레이어 HP 상태 확인
    public float pR;//플레이어 위치
    public CharacterController monstercon;
    public enum ENEMYSTATE
    {
        IDLE = 0,
        MOVE,
        ATTACK,
        DAMAGE,
        DEAD,
        boss
    }
    public ENEMYSTATE ememyState = ENEMYSTATE.IDLE;
    void Awake()
    {        
       // var C = JSON.Parse(jsonM.text);
        atMR = 3.5f;
        monsterAT = 1;
        monsterHP = 3;
        monsterSP = 1;
        rotationSpeed = 1;
    }    
    private void Update()
    {
        switch (ememyState)
        {
            case ENEMYSTATE.IDLE:
                GetComponent<Animator>().ResetTrigger("AT");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");
                if (playerck==true)
                {                   
                    ememyState = ENEMYSTATE.MOVE;
                }
                if (playerRun == true)
                {
                    ememyState = ENEMYSTATE.IDLE;
                }
                break;
            case ENEMYSTATE.MOVE:
                GetComponent<Animator>().ResetTrigger("AT");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().SetTrigger("MOVE");//움직이는동작               
                Moveobj();
                float distabce = (playertaget.transform.position - gameObject.transform.position).magnitude;
                Debug.Log(distabce);
                if (distabce < atMR)
                {
                    ememyState = ENEMYSTATE.ATTACK;
                }
                if (playerRun == true)
                {
                    ememyState = ENEMYSTATE.IDLE;
                }
                break;
            case ENEMYSTATE.ATTACK:
                Moveobj();
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");
                GetComponent<Animator>().SetTrigger("AT");
                Debug.Log("플레이 확인햇다 대미준다잉");
                float distabce1 = (playertaget.transform.position - transform.position).magnitude;               
                if (distabce1 > atMR)
                {
                    ememyState = ENEMYSTATE.MOVE;
                }
                if (playerRun == true)
                {
                    ememyState = ENEMYSTATE.IDLE;
                }
                break;
            case ENEMYSTATE.DAMAGE:
                --monsterHP;
                GetComponent<Animator>().SetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("AT");
                GetComponent<Animator>().ResetTrigger("MOVE");//데미지모션                                            
                ememyState = ENEMYSTATE.IDLE;
                if (monsterwhy == 1 && monsterHP < limtP)
                {
                    //보스몬스터면 HP따른 분기
                }
                if (monsterHP <= 0)
                {
                    ememyState = ENEMYSTATE.DEAD;
                }
                if (playerRun == true)
                {
                    ememyState = ENEMYSTATE.IDLE;
                }
                break;
            case ENEMYSTATE.DEAD:
                GetComponent<Animator>().ResetTrigger("AT");
                GetComponent<Animator>().ResetTrigger("HIT");
                GetComponent<Animator>().ResetTrigger("MOVE");
                GetComponent<Animator>().SetTrigger("DED");
                Destroy(gameObject, 1.5f);
                break;
            case ENEMYSTATE.boss:
                /*if()
                { }*/
                break;
            default:
                break;
        }
    }
    public void hit()
    {
        ememyState = ENEMYSTATE.DAMAGE;
    }
    public void Moveobj()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = playertaget.transform.position;
    }    
}
