using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using InControl;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    static PlayerController _instance = null;
    public static PlayerController Instance()
    {
        return _instance;
    }//인스턴스화
    public enum PLAYERSTATES
    {
        IDLE02 = 0,
        RUN,
        ATTACK01,
        ATTACK02,
        ATTACK03,
        DAMAGE,
        DIE,
        STUN,
        SKILL01,
        SKILL02,
        SKILL03,
        CHEER,
        IDLE01,
        WALK,
    }//상태정의
    public PLAYERSTATES playerstate;//상태
    float exitTime = 0.5f;//에니메이션 끝나는 변수 정의
    //MyCharacterActions characterActions;
    public GameObject playerObj;//플레이어 오브젝트
    public CharacterController CharacterController;//플레이어 콘트롤
    public Animator playercharacter;//플레이어 에니메이션 콘트롤
    private Vector3 _moveVector; //플레이어 이동벡터 
    private Transform _transform;//위치값
    public float move_Speed;//이동속도
    public float hp;//플레이어 HP
    public float mp;//플레이어 mp
    public float at;//플레이어 공격력
    public float df;//플레이어 방어력
    public float item01;//플레이어 소모 착용아이템
    public float combo;//기본공격 콤보 확인 변수
    public bool atingP;//공격중이니?
    public GameObject hitbox;//히트박스오브젝트
    public GameObject hitdtagetp;//누가대상이니?
    //public List<GameObject> mons = new List<GameObject>();
    void Start()
    {      
        /////////////////////////////////캐릭터 액션/////////////////////////////////////////
        //characterActions = new MyCharacterActions();
        //characterActions.Left.AddDefaultBinding(Key.LeftArrow);
        //characterActions.Left.AddDefaultBinding(InputControlType.DPadLeft);
        //characterActions.Right.AddDefaultBinding(Key.RightArrow);
        //characterActions.Right.AddDefaultBinding(InputControlType.DPadRight);
        //characterActions.Attack.AddDefaultBinding(Key.Space);
        //characterActions.Attack.AddDefaultBinding(InputControlType.Action1);
        /////////////////////////////////캐릭터 액션/////////////////////////////////////////
        playercharacter = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
        //조이스틱 관련//
        _transform = transform;      //Transform 캐싱 
        _moveVector = Vector3.zero;  //플레이어 이동벡터 초기화 
        //조이스틱 관련//
        //Debug.Log("조이스틱 이동속도 : " + move_Speed);
    }    

    void FixedUpdate()
    {
        if (hitbox.GetComponent<Playerhitbox>().hitdtaget != null)//히트타겟 있으면
        {
           hitdtagetp = hitbox.GetComponent<Playerhitbox>().hitdtaget;//히트대상 정의
        }//히트대상조건및정의

        HandleInput();
        //CharacterAction();  
        
        switch (playerstate)
        {
            case PLAYERSTATES.IDLE02:                
                if (Input.GetKeyDown(KeyCode.Space))//공격키를 입력시
                {
                    atingP = true;//공격으로 전환
                    playercharacter.SetBool("isAttack01", true);//에니메이션 적용
                    ATbut01();//공격버튼
                    Debug.Log(playerstate);
                    Debug.Log("어택01");
                    StartCoroutine(WaitAttack01());//공격하는동안
                    if (hitbox.GetComponent<Playerhitbox>().hitdtaget != null)
                    {//히트타겟에 적용
                        Debug.Log("데미지 적용-5");
                        hitdtagetp.GetComponent<TEXTEX>().Textadd();
                        if (GetComponentInParent<MONSTERAI>() != null)
                        {
                            hitdtagetp.GetComponent<MONSTERAI>().monsterHP -= 5;
                            hitdtagetp.GetComponent<MONSTERAI>().Hit();
                        }
                        if (GetComponentInParent<MONSTERAI>() == null)
                        {
                            hitdtagetp.GetComponent<MONSTERAI02>().monsterHP -= 5;
                            hitdtagetp.GetComponent<MONSTERAI02>().Hit();
                        }
                    }
                }//기본공격1             
                if (playercharacter.GetCurrentAnimatorStateInfo(0).IsName("ATTACK01"))
                {//기본공격1일때 공격키를 입력시
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        atingP = true;
                        playercharacter.SetBool("isAttack02", true);
                        ATbut02();
                        Debug.Log(playerstate);
                        Debug.Log("어택02");
                        StartCoroutine(WaitAttack02());
                        if (hitbox.GetComponent<Playerhitbox>().hitdtaget != null)
                        {
                            Debug.Log("데미지 적용-5");
                            hitdtagetp.GetComponent<TEXTEX>().Textadd();
                            if (GetComponentInParent<MONSTERAI>() != null)
                            {
                                hitdtagetp.GetComponent<MONSTERAI>().monsterHP -= 5;
                                hitdtagetp.GetComponent<MONSTERAI>().Hit();
                            }
                            if (GetComponentInParent<MONSTERAI>() == null)
                            {
                                hitdtagetp.GetComponent<MONSTERAI02>().monsterHP -= 5;
                                hitdtagetp.GetComponent<MONSTERAI02>().Hit();
                            }
                        }
                    }                    
                }//기본공격2
                if (playercharacter.GetCurrentAnimatorStateInfo(0).IsName("ATTACK02"))
                {//기본공격2일때 공격키입력시
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        atingP = true;
                        playercharacter.SetBool("isAttack03", true);
                        ATbut03();
                        Debug.Log(playerstate);
                        Debug.Log("어택03");
                        StartCoroutine(WaitAttack03());
                        if (hitbox.GetComponent<Playerhitbox>().hitdtaget != null)
                        {
                            Debug.Log("데미지 적용-5");
                            hitdtagetp.GetComponent<TEXTEX>().Textadd();
                            if (GetComponentInParent<MONSTERAI>() != null)
                            {
                                hitdtagetp.GetComponent<MONSTERAI>().monsterHP -= 5;
                                hitdtagetp.GetComponent<MONSTERAI>().Hit();
                            }
                            if (GetComponentInParent<MONSTERAI>() == null)
                            {
                                hitdtagetp.GetComponent<MONSTERAI02>().monsterHP -= 5;
                                hitdtagetp.GetComponent<MONSTERAI02>().Hit();
                            }
                        }
                    }
                    else
                    {                        
                    }
                }//기본공격3
                /*if (!playercharacter.GetCurrentAnimatorStateInfo(0).IsName("isAttack01"))
                 {                    
                     if (Input.GetKeyDown(KeyCode.Space))
                     {
                         playercharacter.SetBool("isAttack02", true);
                         ATbut02();
                         Debug.Log(playerstate);
                         Debug.Log("어택02");
                         StartCoroutine(WaitAttack02());
                     }
                 }*///예시문
                break;
            case PLAYERSTATES.RUN:                              
                transform.Translate(0, 0, move_Speed * Time.deltaTime);
                break;
            case PLAYERSTATES.ATTACK01:
                playercharacter.SetBool("isAttack01", true);
                break;
            case PLAYERSTATES.ATTACK02:
                playercharacter.SetBool("isAttack02", true);                      
                break;
            case PLAYERSTATES.ATTACK03:
                playercharacter.SetBool("isAttack03", true);                
                break;
            case PLAYERSTATES.DAMAGE:               
                playercharacter.SetBool("HITP", false);                
                break;
            case PLAYERSTATES.DIE:
                break;
            case PLAYERSTATES.STUN:
                break;
            case PLAYERSTATES.SKILL01:
                break;
            case PLAYERSTATES.SKILL02:
                break;
            case PLAYERSTATES.SKILL03:
                break;
            case PLAYERSTATES.CHEER:
                break;
            case PLAYERSTATES.IDLE01:
                break;
            case PLAYERSTATES.WALK:
                break;
            default:
                break;
        }
    }

    public void HandleInput()
    {
        _moveVector = PoolInput();
    }//조이스틱 정의
    public Vector3 PoolInput()
    {
        var InputDevice = InputManager.ActiveDevice;
        float h = InputDevice.LeftStickX;
        float v = InputDevice.LeftStickY;
        var input = new Vector3(InputDevice.LeftStickX, 0, InputDevice.LeftStickY);
        if (input != Vector3.zero)
        {
            transform.forward = input;
            playerstate = PLAYERSTATES.RUN;
            playercharacter.SetBool("isRun", true);            
        }
        else
        {            
                playercharacter.SetBool("isRun", false);
                playerstate = PLAYERSTATES.IDLE02;          
        }
        Vector3 moveDir = new Vector3(h, 0, v).normalized;
        return moveDir;
    }//컨트롤 계산

    public void CharacterAction()
    {/*
        var InputDevice = InputManager.ActiveDevice;
        Debug.Log(InputDevice.Action1);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playercharacter.SetBool("isAttack01", true);
        }
        else
        {
            playercharacter.SetBool("isAttack01", false);
        }*/
    }//에이메이션실행 예시문
    IEnumerator CheckAnimationState()
    {
        while (!playercharacter.GetCurrentAnimatorStateInfo(0).IsName("isAttack01"))
        {//플레이어 에니메이션이 isAttack01 아닌경우 동안 무엇을할것인가?
            //전환 중일 때 실행되는 부분
            Debug.Log("전환중");
            yield return null;
        }
        while (playercharacter.GetCurrentAnimatorStateInfo(0).normalizedTime < exitTime)
        {//플레이어 에니메이션이 실행이 끝나는동안
            //애니메이션 재생 중 실행되는 부분
            Debug.Log("실행중");
            yield return null;
        }
        //애니메이션 완료 후 실행되는 부분
    }//에니메이션실행중일때 예시문

    //=================================================차지에니메이션 대기=====================================//

    IEnumerator WaitAttack01()
    {        
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(0.4f);//0.4초후아래문실행
            playercharacter.SetBool("isAttack01", false);
            yield return new WaitForSeconds(0.4f);//0.4초후 아래문실행
            atingP = false;
            i++;
        }       
    }//콤보1코루틴

    IEnumerator WaitAttack02()
    {
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(0.5f);
            playercharacter.SetBool("isAttack02", false);
            yield return new WaitForSeconds(0.5f);
            atingP = false;
            i++;
        }
    }//콤보2코루틴

    IEnumerator WaitAttack03()
    {
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(0.7f);
            playercharacter.SetBool("isAttack03", false);
            yield return new WaitForSeconds(0.7f);
            atingP = false;
            i++;
        }
    }//콤보3코루틴

    IEnumerator Wait01()
    {
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(0.1f);
            playercharacter.SetBool("HITP", false);         
            i++;
        }
    }////경직쿨타임 코루틴

    public void ATbut01()
    {
        Debug.Log("어택01");
        atingP = true;
        playerstate = PLAYERSTATES.ATTACK01;
        combo+= 1;
    }//기본공격 버튼1
    public void ATbut02()
    {
        Debug.Log("어택02");
        atingP = true;
        playerstate = PLAYERSTATES.ATTACK02;
        combo = 2;
    }//기본공격 버튼2
    public void ATbut03()
    {
        Debug.Log("어택03");
        atingP = true;
        playerstate = PLAYERSTATES.ATTACK03;       
    }//기본공격 버튼3

    public void Skiil01()
    {
        /*playerstate = PLAYERSTATES.ATTACK01;
         Debug.Log(playerstate);
         Debug.Log("어택01");*/
    }//스킬공격 버튼1
    public void Skiil02()
    {
        /*playerstate = PLAYERSTATES.ATTACK01;
        Debug.Log(playerstate);
        Debug.Log("어택01");*/
    }//스킬공격 버튼2
    public void Skiil03()
    {
        /*playerstate = PLAYERSTATES.ATTACK01;
        Debug.Log(playerstate);
        Debug.Log("어택01");*/
    }//스킬공격 버튼3
    public void Hiting()
    {
        playercharacter.SetBool("HITP", true);
        StartCoroutine(Wait01());
        playerstate = PLAYERSTATES.DAMAGE;
    }
}
