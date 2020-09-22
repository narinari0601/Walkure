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

    private float hp;

    [SerializeField, Header("移動速度")]
    private float speed = 1.0f;

    private Vector3 velocity;

    private PlayerDirection playerDirection;

    private Vector3 directionVector;

    [SerializeField, Header("通常弾プレハブ")]
    private GameObject normalBulletPrefab = null;

    private float normalDamage;

    [SerializeField,Header("特殊弾プレハブ")]
    private GameObject specialBulletPrefab = null;

    [SerializeField,Header("チャージ時間")]
    private float maxChargeTime = 1.0f;

    [SerializeField,Header("ボタンを押してからチャージが始まるまでの時間")]
    private float chargeStartTime = 0.5f;

    private float chargeTimer;

    private bool isCharge;  //チャージ中ならtrue

    public float Hp { get => hp; set => hp = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        hp = 10;

        velocity = Vector3.zero;
        playerDirection = PlayerDirection.DOWN;
        directionVector = Vector3.zero;

        normalDamage = 1.0f;

        chargeTimer = 0.0f;

        isCharge = false;
    }
    
    private void Update()
    {
        Shot();
        

    }
    private void FixedUpdate()
    {
        PlayerMove();

        PlayerDead();

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

        if (!isCharge)
        {
            rb.MovePosition(transform.position + velocity);
        }

        
    }

    private void Shot()
    {

        //押した瞬間
        if (Input.GetKeyDown(KeyCode.Z))
        {
            var normalBullet = Instantiate(normalBulletPrefab, transform.position + directionVector / 5, Quaternion.Euler(90, 0, 0));

            normalBullet.GetComponent<NormalBullet>().Initialize(directionVector, normalDamage);
        }

        //押してる間
        if (Input.GetKey(KeyCode.Z))
        {
            chargeTimer += Time.deltaTime;
        }

        //離した瞬間
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (chargeTimer >= maxChargeTime)
            {
                var specialBullet = Instantiate(specialBulletPrefab, transform.position + directionVector / 5, Quaternion.Euler(90, 0, 0));

                specialBullet.GetComponent<SpecialBullet>().Initialize(directionVector);
            }

            chargeTimer = 0;

            isCharge = false;
        }

        if (chargeTimer >= chargeStartTime)
        {
            isCharge = true;
        }
    }

    private void PlayerDead()
    {
        if (hp <= 0)
        {
            Debug.Log("がめおべら");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var gameObj = other.gameObject;

        if (gameObj.tag == "Enemy")
        {
            hp--;

            if (hp < 0)
                hp = 0;
        }
    }
}
