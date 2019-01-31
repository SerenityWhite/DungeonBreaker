using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerScript : MonoBehaviour
{
    //https://mrbinggrae.tistory.com/entry/InScope-RPG-51-StackingItems

    private int stackSize;
    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public GameObject weaponBagChk;
    public GameObject armorBagChk;
    public GameObject accessoryBagChk;
    public GameObject potionBagChk;
    public GameObject nullBagChk;

    public UIGrid weaponBag;
    public UIGrid armorBag;
    public UIGrid accessoryBag;
    public UIGrid potionBag;
    public UIGrid nullBag;

    public UIScrollView m_scrollView;

    // 만든 SampleItem을 복사해서 만들기 위해 선언

    public GameObject testInstansBox;

    public GameObject m_gObjWeaponSampleItem;
    public GameObject m_gObjShieldSampleItem;
    public GameObject m_gObjHelmetSampleItem;
    public GameObject m_gObjGloveSampleItem;
    public GameObject m_gObjPantsSampleItem;
    public GameObject m_gObjRingSampleItem;
    public GameObject m_gObjArmorSampleItem;
    public GameObject m_gObjbootsSampleItem;

    // 새로 만들어진 아이템들을 모아둠(삭제 및 수정 등을 하기위해서)
    public List<ItemScript> m_lItems = new List<ItemScript>();


    // 아래 list는 온오프 활성화 하려고 ( 사용 안할 수 도 있음)
    public List<ItemScript> m_slotitem = new List<ItemScript>();


    void Start()
    {
        
    }

    
    void Update()
    {

    }

    public void TestMakeItem()
    {
        if (m_lItems.Count < 30)
        {
            // 새로 만들어서 그리드 자식으로 넣음
            GameObject boxSampleItem = NGUITools.AddChild(nullBag.gameObject, testInstansBox);
            // 관리를 위해 만든걸 리스트에 넣어둠
            ItemScript itemScript = boxSampleItem.GetComponent<ItemScript>();
            m_lItems.Add(itemScript);
            // 그리드와 스크롤뷰를 재정렬
            nullBag.Reposition();
            m_scrollView.ResetPosition();
        }
        if(m_lItems.Count == 30)
        {
            Debug.Log("인벤 full . 더 이상 가질 수 없습니다.");
        }
        
    }

    public void TapClick()
    {
        if (weaponBagChk.GetComponent<TweenAlpha>().to == 1 && weaponBagChk.GetComponent<TweenAlpha>().from == 0)
        {
            weaponBag.gameObject.SetActive(true);
            if (weaponBag.gameObject.activeSelf == true)
            {
                armorBag.gameObject.SetActive(false);
                accessoryBag.gameObject.SetActive(false);
                potionBag.gameObject.SetActive(false);
                nullBag.gameObject.SetActive(false);
            }
        }
        if (armorBagChk.GetComponent<TweenAlpha>().to == 1 && armorBagChk.GetComponent<TweenAlpha>().from == 0)
        {
            armorBag.gameObject.SetActive(true);
            if (armorBag.gameObject.activeSelf == true)
            {
                weaponBag.gameObject.SetActive(false);
                accessoryBag.gameObject.SetActive(false);
                potionBag.gameObject.SetActive(false);
                nullBag.gameObject.SetActive(false);
            }
        }
        if (accessoryBagChk.GetComponent<TweenAlpha>().to == 1 && accessoryBagChk.GetComponent<TweenAlpha>().from == 0)
        {
            accessoryBag.gameObject.SetActive(true);
            if (accessoryBag.gameObject.activeSelf == true)
            {
                armorBag.gameObject.SetActive(false);
                weaponBag.gameObject.SetActive(false);
                potionBag.gameObject.SetActive(false);
                nullBag.gameObject.SetActive(false);
            }
        }
        if (potionBagChk.GetComponent<TweenAlpha>().to == 1 && potionBagChk.GetComponent<TweenAlpha>().from == 0)
        {
            potionBag.gameObject.SetActive(true);
            if (potionBag.gameObject.activeSelf == true)
            {
                armorBag.gameObject.SetActive(false);
                accessoryBag.gameObject.SetActive(false);
                weaponBag.gameObject.SetActive(false);
                nullBag.gameObject.SetActive(false);
            }
        }
        if (nullBagChk.GetComponent<TweenAlpha>().to == 1 && nullBagChk.GetComponent<TweenAlpha>().from == 0)
        {
            nullBag.gameObject.SetActive(true);
            if (nullBag.gameObject.activeSelf == true)
            {
                weaponBag.gameObject.SetActive(false);
                armorBag.gameObject.SetActive(false);
                accessoryBag.gameObject.SetActive(false);
                potionBag.gameObject.SetActive(false);
            }
        }
    }

    // 삭제 함수
    private void ClearOne(ItemScript itemScript)
    {
        for (int nIndex = 0; nIndex < m_lItems.Count; nIndex++)
        {
            // 인자로 넘어온것과 같으면 리스트에서 제거하고
            // GameObject를 없애서 화면에서 지워줌
            if (itemScript == m_lItems[nIndex])
            {
                DestroyImmediate(m_lItems[nIndex].gameObject);
                m_lItems.RemoveAt(nIndex);
                break;
            }
        }
    }

    // 아이템이 선택되면 이 함수가 호출
    // 넘어온 정보를 가지고 이것저것 설정
    // 선택 프레임도 활성/비활성 시켜줌
    //(ItemScript itemScript)이 스크립트가 들어가있는 오브젝트 누를시 아래 함수 실행
    public void SelectItem(ItemScript itemScript)
    {

    }
}
