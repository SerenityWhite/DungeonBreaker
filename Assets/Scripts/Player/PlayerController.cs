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

    }

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

    }

    public PLAYERSTATES playerstate;

    float exitTime = 0.5f;


    //MyCharacterActions characterActions;




    public GameObject playerObj;
    public CharacterController CharacterController;
    public Animator playercharacter;



    private Vector3 _moveVector; //플레이어 이동벡터 
    private Transform _transform;



    public float move_Speed;









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

        HandleInput();



        if (Input.GetKeyDown(KeyCode.Space))
        {

            playerstate = PLAYERSTATES.ATTACK01;

            Debug.Log(playerstate);
            Debug.Log("어택01");
        }
        else
        {
            playercharacter.SetBool("isAttack01", false);
            //StartCoroutine(WaitAttack01());
        }
        
      






        //CharacterAction();

        switch (playerstate)
        {
            case PLAYERSTATES.IDLE02:

               

                break;


            case PLAYERSTATES.RUN:


                transform.Translate(0, 0, move_Speed * Time.deltaTime);

                break;


            case PLAYERSTATES.ATTACK01:

                playercharacter.SetBool("isAttack01", true);

               


                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                  
                //    playerstate = PLAYERSTATES.ATTACK02;
                //    Debug.Log(playerstate);
                //    Debug.Log("공격버튼눌림");

                //}
                //else
                //{

                //    playercharacter.SetBool("isAttack01", false);
                //    playerstate = PLAYERSTATES.IDLE02;
                //}




                break;


            case PLAYERSTATES.ATTACK02:

                playercharacter.SetBool("isAttack02", true);

               

                break;


            case PLAYERSTATES.ATTACK03:

                playercharacter.SetBool("isAttack03", true);

              

                break;


            case PLAYERSTATES.DAMAGE:
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
    }


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

    }



    //public void CharacterAction()
    //{
    //    var InputDevice = InputManager.ActiveDevice;


    //    Debug.Log(InputDevice.Action1);

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        playercharacter.SetBool("isAttack01", true);


    //    }
    //    else
    //    {
    //        playercharacter.SetBool("isAttack01", false);
    //    }
    //}




    IEnumerator CheckAnimationState()
    {

        while (!playercharacter.GetCurrentAnimatorStateInfo(0)
        .IsName("isAttack01"))
        {
            //전환 중일 때 실행되는 부분
            yield return null;
        }

        while (playercharacter.GetCurrentAnimatorStateInfo(0)
        .normalizedTime < exitTime)
        {
            //애니메이션 재생 중 실행되는 부분
            yield return null;
        }

        //애니메이션 완료 후 실행되는 부분

    }


    //=================================================차지에니메이션 대기=====================================//
    IEnumerator WaitAttack01()
    {
        int i = 0;
        while (i < 1)
        {
            //yield return new WaitForSeconds(0.2f);

            yield return null;

            //playercharacter.SetBool("isAttack01", false);

           

            //if (Input.GetKeyDown(KeyCode.Space))
            //{

            //    playerstate = PLAYERSTATES.ATTACK02;

            //    Debug.Log(playerstate);
            //    Debug.Log("어택02");
            //}
            //else
            //{
            //    playercharacter.SetBool("isAttack01", false);
            //}



            

           
           

            i++;

        }

    }

    IEnumerator WaitAttack02()
    {
        int i = 0;
        while (i < 1)
        {




            yield return new WaitForSeconds(0.3f);

            playercharacter.SetBool("isAttack02", false);


            i++;

        }

    }

    IEnumerator WaitAttack03()
    {
        int i = 0;
        while (i < 1)
        {

            yield return new WaitForSeconds(0.1f);

            playercharacter.SetBool("isAttack03", false);


            i++;

        }

    }

}
