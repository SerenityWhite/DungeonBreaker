using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ItemRandom : MonoBehaviour
{
    public bool InvenMargin; //인벤토리에 여유공간이 있으면 true, 없으면 false
    public int playerLV; //플레이어 레벨

    public List<TextAsset> ItemTable; //무기, 방어구 같은 대분류 테이블 파일 리스트
    public int ItemTableMin = 0; //대분류 테이블 파일 최소갯수
    public int ItemTableMax; //대분류 테이블 파일 최대갯수
    public int ItemLevelRange; //아이템 레벨 범위

    public string ItemName;
    public string ItemCategory;

    void Start()
    {
        playerLV = PlayerPrefs.GetInt("playerLV");
        //인벤토리 여유공간
        IventoryMargin();
    }

    void Update()
    {

    }

    void IventoryMargin() //인벤토리에 여유 공간이 있는지 체크
    {
        if (InvenMargin == true)
        {
            ItemOptionRandom();
        }
        else
        {
            //인벤토리에 여유공간이 없습니다. 인벤토리를 비워주세요. 라는 메시지창 출력
        }
    }

    void ItemOptionRandom()
    {
        //아이템 대분류 테이블 가져오기
        int ItemTableNum = Random.Range(ItemTableMin, ItemTableMax+1);
        var ItemData = JSON.Parse(ItemTable[ItemTableNum].text);

        //테이블 안에서 플레이어 레벨과 가장 가까운 아이템 가져오기
        for (int i = 0; i < ItemData.Count; i++)
        {
            if (ItemData[i]["ItemLevel"] >= playerLV && ItemData[i]["ItemLevel"] <= playerLV + ItemLevelRange)
            {
                for (int j = 0; j < ItemLevelRange + 1; j++)
                {
                    if (playerLV + j == ItemData[i]["ItemLevel"])
                    {
                        ItemName = ItemData[i]["ItemName"];
                        break;
                    }
                }
            }
            else
            {
                if (ItemData[i]["ItemLevel"] >= playerLV - ItemLevelRange && ItemData[i]["ItemLevel"] <= playerLV)
                {
                    for (int j = 0; j > -(ItemLevelRange + 1); j--)
                    {
                        if (playerLV + j == ItemData[i]["ItemLevel"])
                        {
                            ItemName = ItemData[i]["ItemName"];
                            break;
                        }
                    }
                }
            }
        }

        //아이템을 하나 골라낸 후에 랜덤 돌리기
        for (int k = 0; k < ItemData.Count; k++)
        {
            if (ItemData[k]["ItemName"] == ItemName)
            {
                ItemCategory = ItemData[k]["ItemCategory"];
                int ItemRequestLevel = Random.Range(ItemData[k]["ItemRequestLevelMin"], ItemData[k]["ItemRequestLevelMax"] + 1);
                int ItemBuffCreateMin = Random.Range(ItemData[k]["ItemBuffCreateMinMin"], ItemData[k]["ItemBuffCreateMinMax"] + 1);
                int ItemBuffCreateMax = Random.Range(ItemData[k]["ItemBuffCreateMaxMin"], ItemData[k]["ItemBuffCreateMaxMax"] + 1);

                //선택된 테이블이 무기일때 위 변수를 모두 적용
                //선택된 테이블이 방어구일때 ItemBuffCreateMin만 적용

                // 카테고리 이름을 기준으로 아이템 옵션 돌리기
            }
        }
    }

    void ItemOutput() //랜덤함수의 결과값을 전부 더해서 아이템 최종 출력
    {
        //옵션 몇 개 붙을지 돌리기 > 옵션 갯수에 따라 아이템 등급 출력 > 아이템명 라벨컬러 등급에 따라 조정 & 등급에 따라 라벨텍스트 출력 > 아이템 최종 출력
    }
}