using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class TestSC : MonoBehaviour
{
    public TextAsset ItemTable;
    public int ItemLevelRange; //아이템 레벨 범위
    public int playerLV; //플레이어 레벨

    public string ItemName;
    public string ItemCategory;

    void Start()
    {
        ItemOptionRandom();
    }

    void ItemOptionRandom()
    {
        //아이템 대분류 테이블 가져오기
        var ItemData = JSON.Parse(ItemTable.text);

        //테이블 안에서 플레이어 레벨과 가장 가까운 아이템 가져오기
        for (int i = 0; i < ItemData.Count; i++)
        {
            if (ItemData[i]["ItemLevel"] >= playerLV && ItemData[i]["ItemLevel"] <= playerLV + ItemLevelRange)
            {
                Debug.Log(ItemData[i]["ItemLevel"]);
                ItemName = ItemData[i]["ItemName"];
            }
        }

        for (int i = 0; i < ItemData.Count; i++)
        {
            if (ItemName == null)
            {
                if (ItemData[i]["ItemLevel"] >= playerLV - ItemLevelRange && ItemData[i]["ItemLevel"] <= playerLV)
                {
                    Debug.Log("상위템이 없어서 하위템 불러옴");
                    Debug.Log(ItemData[i]["ItemLevel"]);
                    ItemName = ItemData[i]["ItemName"];
                }
            }
        }

        //골라낸 아이템 랜덤 돌리기
        for (int k = 0; k < ItemData.Count; k++)
        {
            if (ItemData[k]["ItemName"] == ItemName)
            {
                Debug.Log("아이템 랜덤 돌리기 시작");
                Debug.Log(ItemName);
                Debug.Log(ItemData[k]["ItemName"]);
                ItemCategory = ItemData[k]["ItemCategory"];
                int ItemRequestLevel = Random.Range(ItemData[k]["ItemRequestLevelMin"], ItemData[k]["ItemRequestLevelMax"] + 1);
                Debug.Log(ItemRequestLevel);
                int ItemBuffCreateMin = Random.Range(ItemData[k]["ItemBuffCreateMinMin"], ItemData[k]["ItemBuffCreateMinMax"] + 1);
                Debug.Log(ItemBuffCreateMin);
                int ItemBuffCreateMax = Random.Range(ItemData[k]["ItemBuffCreateMaxMin"], ItemData[k]["ItemBuffCreateMaxMax"] + 1);
                Debug.Log(ItemBuffCreateMax);
            }
        }
    }
}
