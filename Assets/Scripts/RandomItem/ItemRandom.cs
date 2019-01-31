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

    public string ItemName; //아이템 이름
    public string ItemCategory; //아이템 카테고리명
    public int ItemLevel; //아이템 레벨

    public bool highItemNull = false;
    public List<string> RandomItemName;

    public TextAsset OptionTable; //옵션 테이블 파일
    public string GradeName; //아이템 등급명
    public int OptionCount;

    public List<int> OptionNum;
    public List<int> FinalOpt;
    public List<int> FinalOptGroup;
    public string ItemPre; //아이템의 접두사
    public string ItemSuf; //아이템의 접미사

    public List<string> OptionName; //옵션 이름
    public List<int> OptionBuffMin; //옵션의 최소수치
    public List<int> OptionBuffMax; //옵션의 최대수치

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

        //테이블 안에서 플레이어 레벨과 가장 가까운 아이템 검색
        for (int i = 0; i < ItemData.Count; i++)
        {
            if (ItemData[i]["ItemLevel"] >= playerLV && ItemData[i]["ItemLevel"] <= playerLV + ItemLevelRange)
            {
                highItemNull = false;
                var Item = ItemData[i];

                int l = 1;
                do
                {
                    if (Item["ItemLevel"] == playerLV)
                    {
                        RandomItemName.Add(Item["ItemName"]);
                        break;
                    }
                    else
                    {
                        if (Item["ItemLevel"] == playerLV + l)
                        {
                            RandomItemName.Add(Item["ItemName"]);
                            break;
                        }
                        else
                        {
                            l++;
                        }
                    }
                } while (Item["ItemLevel"] > playerLV + ItemLevelRange);
            }
            else
            {
                highItemNull = true;
            }
        }
        if (highItemNull == true)
        {
            for (int j = 0; j < ItemData.Count; j++)
            {
                if (ItemData[j]["ItemLevel"] >= playerLV - ItemLevelRange && ItemData[j]["ItemLevel"] < playerLV)
                {
                    var Item = ItemData[j];

                    int m = 1;
                    do
                    {
                        if (Item["ItemLevel"] == playerLV - m)
                        {
                            RandomItemName.Add(Item["ItemName"]);
                            break;
                        }
                        else
                        {
                            m++;
                        }
                    } while (Item["ItemLevel"] > playerLV - ItemLevelRange);
                }
            }
        }

        //위에서 검색한 아이템 랜덤 돌려서 1개만 골라내기
        int RandomItemNum = Random.Range(0, RandomItemName.Count);
        ItemName = RandomItemName[RandomItemNum];

        //아이템을 하나 골라낸 후에 랜덤 돌리기
        for (int k = 0; k < ItemData.Count; k++)
        {
            if (ItemData[k]["ItemName"] == ItemName)
            {
                ItemLevel = ItemData[k]["ItemLevel"];
                ItemCategory = ItemData[k]["ItemCategory"]; // 카테고리 이름을 기준으로 아이템 옵션 돌릴 것
                int ItemRequestLevel = Random.Range(ItemData[k]["ItemRequestLevelMin"], ItemData[k]["ItemRequestLevelMax"] + 1);
                int ItemBuffCreateMin = Random.Range(ItemData[k]["ItemBuffCreateMinMin"], ItemData[k]["ItemBuffCreateMinMax"] + 1);
                int ItemBuffCreateMax = Random.Range(ItemData[k]["ItemBuffCreateMaxMin"], ItemData[k]["ItemBuffCreateMaxMax"] + 1);

                //선택된 테이블이 무기일때 위 변수를 모두 적용
                //선택된 테이블이 방어구일때 ItemBuffCreateMin만 적용

                ItemOptionRandom2();
            }
        }
    }

    void ItemOptionRandom2()
    {
        //아이템 옵션 테이블 가져오기
        var OptionData = JSON.Parse(OptionTable.text);

        //확률에 따라 아이템 등급 매기기
        float GradeProba = Random.Range(0, 100f);

        if (0 <= GradeProba && GradeProba < 80f)
        {
            GradeName = "Normal";
            OptionCount = 1;
        }
        if (80f <= GradeProba && GradeProba < 95f)
        {
            GradeName = "Magic";
            OptionCount = Random.Range(2, 5);
        }
        if (95f <= GradeProba && GradeProba < 98.9f)
        {
            GradeName = "Rare";
            OptionCount = Random.Range(5, 8);
        }
        if (98.9f <= GradeProba && GradeProba < 99f)
        {
            GradeName = "Legendary";
            OptionCount = 8;
        }

        //아이템 카테고리와 착용레벨 기준으로 옵션 걸러내기
        for (int i = 0; i < OptionData.Count; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                string ItemCategoryStr = "ItemCategoryDatas[" + j + "]";

                if (ItemCategory == OptionData[i][ItemCategoryStr])
                {
                    if (OptionData[i]["ItemLevelRestrictSt"] <= ItemLevel && ItemLevel <= OptionData[i]["ItemLevelRestrictEd"])
                    {
                        var Option = OptionData[i];
                        OptionNum.Add(Option["ItemAffixeDataKey"]);
                    }
                }
            }
        }

        //등급마다 정해진 수 만큼 옵션 랜덤 돌리기
        do
        {
            int FinalOption = Random.Range(0, OptionNum.Count);

            for (int i = 0; i < OptionData.Count; i++)
            {
                if (OptionData[i]["ItemAffixeDataKey"] == OptionNum[FinalOption])
                {
                    int optGroupNum = OptionData[i]["ItemAffixGroup"];
                    bool optGroup = FinalOptGroup.Contains(optGroupNum);
                    if (optGroup == false)
                    {
                        FinalOpt.Add(OptionNum[FinalOption]);
                        FinalOptGroup.Add(optGroupNum);
                    }
                }
            }
        } while (FinalOpt.Count < OptionCount);

        //최종 옵션에서 능력치 뽑아오기
        for (int i = 0; i < OptionData.Count; i++)
        {
            for (int j = 0; j < FinalOpt.Count; j++)
            {
                if (OptionData[i]["ItemAffixeDataKey"] == FinalOpt[j])
                {
                    OptionName.Add(OptionData[i]["BuffData[0]"]);

                    int BuffMin = Random.Range(OptionData[i]["BuffCreateMinMin[0]"], OptionData[i]["BuffCreateMinMax[0]"]);
                    int BuffMax = Random.Range(OptionData[i]["BuffCreateMaxMin[0]"], OptionData[i]["BuffCreateMaxMax[0]"]);

                    OptionBuffMin.Add(BuffMin);
                    OptionBuffMax.Add(BuffMax);           
                }
            }
        }

        //최종 옵션에서 접두사와 접미사로 각각 분류
        List<int> PreFinalOpt = new List<int>();
        List<int> SufFinalOpt = new List<int>();

        for (int i = 0; i < OptionData.Count; i++)
        {
            for (int j = 0; j < FinalOpt.Count; j++)
            {
                if (OptionData[i]["ItemAffixeDataKey"] == FinalOpt[j])
                {
                    string Pref = "ITEM_AFFIX_PREFIX";
                    string Suff = "ITEM_AFFIX_SUFFIX";
                    if (OptionData[i]["AffixType"] == Pref)
                    {
                        PreFinalOpt.Add(FinalOpt[j]);
                    }
                    if (OptionData[i]["AffixType"] == Suff)
                    {
                        SufFinalOpt.Add(FinalOpt[j]);
                    }
                }
            }
        }

        //옵션 최대레벨이 가장 높은 접두사와 접미사를 가져 오기
        for (int i = 0; i < OptionData.Count; i++)
        {
            for (int j = 0; j < FinalOpt.Count; j++)
            {
                if (OptionData[i]["ItemAffixeDataKey"] == PreFinalOpt[j])
                {
                    int optionLevel = OptionData[i]["ItemLevelRestrictEd"];
                    int optionLevelMax = 0;

                    if (optionLevel > optionLevelMax)
                    {
                        optionLevelMax = optionLevel;
                        ItemPre = OptionData[i]["ItemAffixeName"];
                    }
                }

                if (OptionData[i]["ItemAffixeDataKey"] == SufFinalOpt[j])
                {
                    int optionLevel = OptionData[i]["ItemLevelRestrictEd"];
                    int optionLevelMax = 0;

                    if (optionLevel > optionLevelMax)
                    {
                        optionLevelMax = optionLevel;
                        ItemSuf = OptionData[i]["ItemAffixeName"];
                    }
                }
            }
        }

        ItemOutput();
    }

    void ItemOutput() //랜덤함수의 결과값을 전부 더해서 아이템 최종 출력
    {
        //아이템명 라벨컬러 등급에 따라 조정 & 등급에 따라 라벨텍스트 출력 > 아이템 최종 출력
    }
}