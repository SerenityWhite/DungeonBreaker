using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEXTEX : MonoBehaviour
{
    public float atP;
    public string atS;
    public GameObject Pui;
    public void Textadd()
    {
        HUDText hudText = Pui.GetComponent<HUDText>();
        hudText.Add("hit", Color.white, 0f);
    } 
}
