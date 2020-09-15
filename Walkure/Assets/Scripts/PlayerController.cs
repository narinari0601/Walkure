using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    UPPERLEFT,
    UPPERRIGHT,
    LOWERLEFT,
    LOWERRIGHT,
}

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField, Header("移動速度")]
    private float speed = 1.0f;

    private Vector3 velocity;

    private PlayerDirection playerDirection;

    private Vector3 directionVector;

    [SerializeField, Header("通常弾プレハブ")]
    private GameObject playerBulletPrefab = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        playerDirection = PlayerDirection.DOWN;
        directionVector = Vector3.zero;
    }
    
    private void Update()
    {
        Shot();
    }
    private void FixedUpdate()
    {
        
        PlayerMove();
    }

    private void PlayerMove()
    {
        //キーボード入力
        float key_x = Input.GetAxisRaw("Horizontal");
        float key_z = Input.GetAxisRaw("Vertical");

        if (key_x == 0 && key_z == 0)
            return;

        #region　方向転換
        //どちらも押されているとき
        if (key_x != 0 && key_z != 0)
        {
            //左上
            if (key_x < 0 && key_z > 0)
            {
                playerDirection = PlayerDirection.UPPERLEFT;
            }

            //右上
            else if (key_x > 0 && key_z > 0)
            {
                playerDirection = PlayerDirection.UPPERRIGHT;
            }

            //左下
            else if (key_x < 0 && key_z < 0)
            {
                playerDirection = PlayerDirection.LOWERLEFT;
            }

            //右下
            else if (key_x > 0 && key_z < 0)
            {
                playerDirection = PlayerDirection.LOWERRIGHT;
            }
        }

        //左右だけ押されているとき
        else if (key_x != 0 && key_z == 0)
        {
            //左
            if (key_x < 0)
            {
                playerDirection = PlayerDirection.LEFT;
            }

            //右
            else if (key_x > 0)
            {
                playerDirection = PlayerDirection.RIGHT;
            }
        }

        //上下だけ押されているとき
        else if (key_x == 0 && key_z != 0)
        {
            //上
            if (key_z > 0)
            {
                playerDirection = PlayerDirection.UP;
            }

            //下
            else if (key_z < 0)
            {
                playerDirection = PlayerDirection.DOWN;
            }
        }

        #endregion

        directionVector = new Vector3(key_x, 0, key_z);

        //velocity.Set(key_x, 0, key_z);
        velocity = directionVector;
        velocity = velocity.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + velocity);
    }

    private void Shot()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            var playerBullet = Instantiate(playerBulletPrefab, transform.position + directionVector / 5, Quaternion.Euler(90, 0, 0));

            playerBullet.GetComponent<PlayerBullet>().Initialize(directionVector);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var gameObj = other.gameObject;

        if (gameObj.tag == "Enemy")
        {
            //Debug.Log(true);
        }
    }
}
