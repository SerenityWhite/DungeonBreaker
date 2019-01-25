using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerarotation : MonoBehaviour {

    public GameObject mainc;
    float cx;
    float cz;

    void Update()
    {
        cx = mainc.transform.position.x + 5;
        cz = mainc.transform.position.z;
        transform.position = new Vector3(cx, 7, cz);
    }
}
