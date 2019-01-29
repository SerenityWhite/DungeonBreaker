using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class PlayerData : MonoBehaviour
{
    //인게임 자료 저장 공간 또는 플레이어 데이터 관리소(미정)
    public TextAsset jsonP;//플레이어 데이터 자료 받는곳미정 명칭 원하시면 애기하삼^^
    public bool InvenMargin;//인벤토리 박스가 꽉참?
    //public float ieven?; 인벤토리 자료형태 미정?
    public float GOLD;//플레이어 재화
    public float ATK;//플레이어 공격력
    public float DEF;//플레이어 방어력
    public int playerLV;//현재 플레이어 레벨
    public float playerLVUPP;//플레이어 레벨 업 필요한 경험치
    public float playerEXP;//현재 플레이어 경험치
    public float playerHP;//플레이어 HP 값
    public float playerMP;//플레이어 MP 값
    public float inplayerHP;//플레이어 HP 값
    public float inplayerMP;//플레이어 MP 값
    public float playerckAT;//플레이어 무기구착용값
    public float playerckDF;//플에이어 방어구착용값 
    public float playerckP01;//플레이어 착용 포션01 수정사항 애기해주시면 바로 수정합니다 수고!^^;
    public string PlayerPrefs1;//플레이어 프리펩스 활용 및 추가기능 및 공부 자료 습득("덕훈해당사항")


    private void Awake()//겜시작전 테이블 데이터 받기 변수 정의
    {
        // var C = JSON.Parse(jsonM.text) 예시
        /*playerMP = ST[stg]["P3"];
        pp2 = ST[stg]["P2"];
        pp3 = ST[stg]["P1"];
        ep1 = ST[stg]["E1"];제이선 데이터 받는 예제 개인작 할때 작용 잘됨 확인
        ep2 = ST[stg]["E2"];*/
    }

    //하단부터는 NGUI 버튼누르면 플레이어자료 호출할수있게함 보고 주석확인해주시고 추가 기능 피드백요망!
    public void LevelUP()//레벨업 함수
    {
        //gameObject.SetActive(true); 레벨업 효과 및 데이터 적용 상의후 적용
        playerLV = playerLV + 1;
    }
    public void ExpUP()//경험치 증가
    {
        if (playerEXP >= playerLVUPP)
        {//레벨시 경험치 계산 대략 만듬 작동문의 덕훈에게 피드백 요청
            float addEXP = playerLVUPP - playerEXP;
            playerEXP = 0;
            playerEXP += addEXP;
            LevelUP();
        }
    }
    public void ItemP()//아이템 증가
    {
        /*if (InvenMargin == false)
        {
            Debug.Log("아이템 꽉차 추가할수없습니다.");
        }*///시스템 예시 이해?맞나?
        //추가 팀원가 소통후 제작
    }    
    public void GoldP()//골드증가
    {
        //추가 팀원가 소통후 제작
    }
    public void GoldM()//골드 감소
    {
        //추가 팀원가 소통후 제작
    }
    public void ItemATadd()
    {
        //기존 착용은 착용아이템이랑교환
    }
    public void ItemDFadd()
    {
        //기존 착용은 착용아이템이랑교환
    }
    public void ItemPadd()
    {
        //기존 착용은 착용아이템이랑교환
    }
    public void SavePayer()//플레이어 데이터 로드 저장;
    {
        //추가 팀원후 상의후 제작
    }
    public void LodePlyer()//플레이어 데이터 로드
    {
        //추가 팀원후 상의후 제작
    }
}
