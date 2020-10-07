using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolled : MonoBehaviour
{
    [SerializeField,Header("移動速度")]
    private float speed = 1.0f;

    [SerializeField,Header("巡回地点配列")]
    private GameObject[] routeTargetArray = new GameObject[0];  //巡回ルート配列

    private int routeTargetNum;  //現在の巡回ターゲット番号

    private Vector3 vector;

    void Start()
    {
        routeTargetNum = 0;

        vector = (routeTargetArray[routeTargetNum].transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Patroal();
    }

    private void Patroal()
    {
        vector = (routeTargetArray[routeTargetNum].transform.position - transform.position).normalized;
        transform.position += vector * speed * 0.01f;
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;

        if (obj == routeTargetArray[routeTargetNum])
        {
            if (routeTargetNum == routeTargetArray.Length - 1)
            {
                routeTargetNum = 0;
                //vector = (routeTargetArray[routeTargetNum].transform.position - transform.position).normalized;
                return;
            }

            routeTargetNum++;
            //vector = (routeTargetArray[routeTargetNum].transform.position - transform.position).normalized;
        }
    }
}
