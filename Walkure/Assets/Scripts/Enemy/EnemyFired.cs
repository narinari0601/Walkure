using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFired : MonoBehaviour
{

    [SerializeField, Header("撃ちだす弾")]
    private GameObject fireObj = null;

    [SerializeField, Header("弾のダメージ")]
    private float fireDamage = 1;


    [SerializeField, Header("弾のクールタイム")]
    private float fireCoolTime = 3.0f;

    private float fireTimer;

    [SerializeField,Header("弾を撃った後の硬直時間")]
    private float stopTime = 1.0f;

    private float stopTimer;

    private Transform player;

    private Vector3 directionVector;

    private bool isMove;  //動けるならtrue



    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        fireTimer = 0;

        stopTimer = 0;

        player = GameObject.Find("Player").transform;

        directionVector = new Vector3(0, 0, -1);

        isMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.name == "MainCamera")
        {
            HomingMove();

            Fire();
        }
    }

        private void Fire()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer > fireCoolTime)
        {
            isMove = false;

            var angle = Vector3.Angle(-transform.forward, directionVector);

            angle *= Vector3.Cross(transform.forward, directionVector).y < 0 ? -1 : 1;

            var bullet = Instantiate(fireObj, transform.position, Quaternion.Euler(90, 0, angle));
            bullet.GetComponent<EnemyBullet>().Initialize(directionVector, fireDamage);
            fireTimer = 0;
        }
    }

    private void HomingMove()
    {
        directionVector = (player.position - transform.position).normalized;

        var vel = directionVector * 0.005f;


        if (isMove)
        {
            transform.position += vel;
        }

        else
        {
            stopTimer += Time.deltaTime;

            if (stopTimer >= stopTime)
            {
                isMove = true;
                stopTimer = 0;
            }
        }
        
    }
}
