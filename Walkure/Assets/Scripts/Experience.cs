using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{

    //[SerializeField, Header("消えるまでの時間")]
    //private float destroyTime = 5.0f;

    //private float destroyTimer;

    private float exp;

    private bool isGravity;

    private Transform target;

    void Start()
    {
        //Initialize();
    }

    public void Initialize()
    {
        //destroyTimer = 0;

        exp = 1;

        isGravity = false;

        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        Gravitate();
    }

    private void Gravitate()
    {
        if (isGravity)
        {
            var vel = (target.position - transform.position).normalized * Time.deltaTime * 3;

            transform.position += vel;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;

        if (obj.tag == "Player")
        {
            obj.GetComponent<PlayerController>().CurrentExperience += exp;
            Destroy(gameObject);
        }

        if (obj.tag == "ExpCol")
        {
            isGravity = true;
            target = obj.transform.parent;
        }
    }
}
