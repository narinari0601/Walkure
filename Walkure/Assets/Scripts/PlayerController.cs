﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    //
    private float hp;

    private int playerLevel;

    //private const int MAXLEVEL = 5;

    [SerializeField, Header("最大レベル")]
    private int maxLevel = 5;

    private float currentExperience;  //現在の経験値

    [SerializeField, Header("レベルアップに必要な経験値")]
    private float levelUpExperience = 2;  //レベルアップに必要な経験値


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

    [SerializeField, Header("チャージゲージ")]
    private GameObject sliderObj = null;

    private Slider chargeSlider;

    public float Hp { get => hp; set => hp = value; }
    public float CurrentExperience { get => currentExperience; set => currentExperience = value; }
    public float LevelUpExperience { get => levelUpExperience;}
    public int PlayerLevel { get => playerLevel;}
    public int MaxLevel { get => maxLevel; }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();

        hp = 10;

        currentExperience = 0;

        playerLevel = (int)(currentExperience / levelUpExperience) + 1;

        velocity = Vector3.zero;
        playerDirection = PlayerDirection.UP;
        directionVector = new Vector3(0, 0, 1);

        normalDamage = playerLevel;

        chargeTimer = 0.0f;

        isCharge = false;

        chargeSlider = GetComponentInChildren<Slider>();
        chargeSlider.value = 0;
        sliderObj.SetActive(false);
    }
    
    private void Update()
    {
        Shot();

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    currentExperience++;
        //}
    }
    private void FixedUpdate()
    {
        PlayerMove();

        LevelUp();

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

        normalDamage = playerLevel;

        //押した瞬間
        if (Input.GetButtonDown("Fire1"))
        {
            var normalBullet = Instantiate(normalBulletPrefab, transform.position + directionVector / 5, Quaternion.Euler(90, 0, 0));

            normalBullet.GetComponent<NormalBullet>().Initialize(directionVector, normalDamage);
        }

        //押してる間
        if (Input.GetButton("Fire1"))
        {
            chargeTimer += Time.deltaTime;
        }

        //離した瞬間
        if (Input.GetButtonUp("Fire1"))
        {
            if (chargeTimer >= maxChargeTime)
            {
                var specialBullet = Instantiate(specialBulletPrefab, transform.position + directionVector / 5, Quaternion.Euler(90, 0, 0));

                specialBullet.GetComponent<SpecialBullet>().Initialize(directionVector);
            }

            chargeTimer = 0;

            isCharge = false;

            sliderObj.SetActive(false);
            chargeSlider.value = 0;
        }

        if (chargeTimer >= chargeStartTime)
        {
            isCharge = true;
            sliderObj.SetActive(true);
            chargeSlider.value = (chargeTimer - chargeStartTime) / (maxChargeTime - chargeStartTime);
        }
    }

    private void LevelUp()
    {
        

        playerLevel = (int)(currentExperience / levelUpExperience) + 1;

        if (playerLevel > maxLevel)
            playerLevel = maxLevel;
    }


    private void PlayerDead()
    {
        if (hp <= 0)
        {
            Debug.Log("がめおべら");
        }
    }

    public void Damage(float damage)
    {
        hp -= damage;

        if (hp < 0)
        {
            hp = 0;
        }
    }
}
