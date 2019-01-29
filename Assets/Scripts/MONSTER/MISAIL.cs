using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SimpleJSON;

public class MISAIL : MonoBehaviour
{
    public GameObject target;
    public Vector3 fp;
    // Start is called before the first frame update
    void Start()
    {
        target=GameObject.Find("Player");
        fp = target.transform.position;
        Destroy(gameObject, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Moveobj();        
    }
    public void Moveobj()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = fp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hit");
            Destroy(this);
        }
    }
}
