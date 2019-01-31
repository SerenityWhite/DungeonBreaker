using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SimpleJSON;


public class MISAIL : MonoBehaviour
{//투사체
    public GameObject target;
    public GameObject how;
    public float rs;//회전값
    public float i1;//리미트값
    public float pows;//스피드값
    public bool nontarget;
    public int lmt;
    public Rigidbody rb;
    public float ded;

    void Start()
    {
        target=GameObject.Find("Player");        
        Destroy(gameObject, ded);
        rb = GetComponent<Rigidbody>();
        how = GetComponentInParent<MONSTERAI02>().my;
    }
  
    void Update()
    {
      
        if (nontarget == false)
        {
            Moveobj();
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0f;
            dir.Normalize();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rs * Time.deltaTime);
            transform.Rotate(-90, 0, 0);
        }
        else
        {                           
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0f;
            dir.Normalize();
            transform.Rotate(-90, 0, 0);
            Vector3 a = how.transform.forward * pows;
            rb.velocity = a;
        }
        
    }            

    public void Moveobj()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hit");
            target.GetComponent<PlayerController>().Hiting();
            Destroy(gameObject,0.2f);
        }
    }
    IEnumerator Wait02()
    {
        int i = 0;
        while (i < 1)
        {
            yield return new WaitForSeconds(0.8f);
            lmt = 1;
            i++;
        }
    }
}
