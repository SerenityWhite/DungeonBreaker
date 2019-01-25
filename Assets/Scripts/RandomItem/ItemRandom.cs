using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ItemRandom : MonoBehaviour
{
    public bool InvenMargin;
    public int playerLV;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void IventoryMargin() //인벤토리에 여유 공간이 있는지 체크
    {
        if (InvenMargin == true)
        {
            PlayerLevelCheck();
        }
        else
        {
            //인벤토리에 여유공간이 없습니다. 인벤토리를 비워주세요. 라는 메시지창 출력
        }
    }

    void PlayerLevelCheck() //현재 플레이어의 레벨을 체크
    {
        var ItemData = JSON.Parse("데이터 파일명");
        playerLV = ItemData["데이터 파일 안의 플레이어 레벨 항목"];
    }

    void NameRandom() //아이템 이름 랜덤 돌리는 함수
    {
        //pre(접두), suf(접미)
    }

    void STRRandom() //아이템 공격력 랜덤 돌리는 함수
    {

    }

    void DEFRandom() //아이템 방어력 랜덤 돌리는 함수
    {

    }

    void HPRandom() //아이템 HP 증가치 랜덤 돌리는 함수
    {

    }

    void MPRandom() //아이템 MP 증가치 랜덤 돌리는 함수
    {

    }

    void ItemOutput() //랜덤함수의 결과 수치를 전부 더해서 아이템 최종 출력
    {

    }
}
