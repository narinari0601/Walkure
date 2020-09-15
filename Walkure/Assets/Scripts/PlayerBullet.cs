using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField, Header("弾速")]
    private float speed = 1.0f;

    private Vector3 velocity;


    [SerializeField, Header("消えるまでの時間")]
    private float lostTime = 1.0f;

    private float currentTimer;  //出現してからの時間

    void Start()
    {
        //Initialize();
    }

    public void Initialize(Vector3 directionVector)
    {
        currentTimer = 0;

        velocity = directionVector.normalized * speed * 0.01f;
    }


    private void FixedUpdate()
    {
        transform.position += velocity;

        currentTimer += Time.deltaTime;

        if (currentTimer >= lostTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;

        if(obj.tag=="Enemy")
        {
            Destroy(gameObject);
        }
    }
}
