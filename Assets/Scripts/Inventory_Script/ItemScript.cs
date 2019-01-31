using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    static ItemScript _instance;
    public static ItemScript Instance()
    {
        return _instance;
    }

    public string m_strName;

    private string m_strSpriteName;

    public UISprite m_sprIcon;

    public UISprite m_sprFrame;

    public GameObject m_equipLabel;

    public InventoryManagerScript m_cParent;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        m_sprFrame.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void SetSelected(bool bSelected)

    {
        m_sprFrame.gameObject.SetActive(bSelected);
    }

    public void SettingInfo(string spriteName)
    {
        m_sprIcon.spriteName = spriteName;
        gameObject.name = spriteName;
        m_strName = spriteName;
    }

    // 터치 하면 발생하는 이벤트
    // 전에 Button을 썼지만 OnClick으로 사용
    // OnClick은 NGUI에서 제공하는 함수로 터치하면 발생
    void OnClick()
    {
        Debug.Log(m_strName + " 이 클릭되었습니다.");

        // 부모에게 내가 선택 되었다고 알림


        //InventoryManagerScript 함수
        m_cParent.SelectItem(this);     // 이 함수는 조금 후에 만듬.우선 이렇게 작성.
    }
}
