using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField,Header("最大HP")]
    private float maxHP = 0;

    private float currentHP;

    //[SerializeField,Header("HPゲージオブジェクト")]
    //private GameObject hpSliderObj;

    ////[SerializeField,Header("HPが出ている時間")]
    ////private float hpActiveTime = 1.0f;

    //private const float HP_ACTIVE_TIME = 2.0f;

    //private float hpCurrentTimer;

    private Slider hpSlider;

    //private bool isDamage;

    private PlayerController pc;

    [SerializeField, Header("ダメージUI")]
    private GameObject damageObj = null;

    [SerializeField, Header("経験値プレハブ")]
    private GameObject expPrefab = null;

    [SerializeField,Header("経験値量")]
    private int expValue = 0;

    private int expSeed;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        currentHP = maxHP;

        hpSlider = GetComponentInChildren<Slider>();

        //hpSlider = hpSliderObj.GetComponent<Slider>();

        hpSlider.value = 1;

        //hpSliderObj.SetActive(false);

        //hpCurrentTimer = 0;

        //isDamage = false;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        expSeed = 0;
    }
    
    void Update()
    {
        expSeed++;

        if (expSeed > 1000)
            expSeed = 0;

        hpSlider.value = currentHP / maxHP;


        //ActiveHPSlider();


        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void AttackedBullet(float damage)
    {
        currentHP -= damage;
        //isDamage = true;
        //hpCurrentTimer = 0;
    }


    public void AttackedSpecialBullet(float damage)
    {
        currentHP -= damage;
        //isDamage = true;
        //hpCurrentTimer = 0;

        if (currentHP <= 0)
        {
            //pc.CurrentExperience++;
            DrapExp(expValue);
        }
    }

    //private void ActiveHPSlider()
    //{
    //    if (isDamage)
    //    {
    //        hpSliderObj.SetActive(true);
    //        hpCurrentTimer += Time.deltaTime;

    //        if (hpCurrentTimer >= HP_ACTIVE_TIME)
    //        {
    //            isDamage = false;
    //            hpCurrentTimer = 0;
    //            hpSliderObj.SetActive(false);
    //        }
    //    }
    //}

    private void DrapExp(int exp)
    {

        for (int i = 0; i < exp; i++)
        {
            if (i == 0)
            {
                var obj = Instantiate(expPrefab, transform.position, Quaternion.Euler(90, 0, 0));
                obj.GetComponent<Experience>().Initialize();
            }

            else
            {
                float x = Random.Range(-0.5f, 0.5f);
                Random.InitState(expSeed);
                expSeed++;

                float z = Random.Range(-0.3f, 0.3f);
                Random.InitState(expSeed);
                expSeed++;

                Vector3 pos = new Vector3(x, 0.1f, z);

                var obj = Instantiate(expPrefab, transform.position + pos, Quaternion.Euler(90, 0, 0));
                obj.GetComponent<Experience>().Initialize();
            }
        }
    }

    public void DamageEffect(float damage)
    {
        var obj = Instantiate(damageObj, transform.position + new Vector3(0, 0, GetComponent<Collider>().bounds.size.z / 2), Quaternion.identity);
        //obj.transform.SetParent(transform);
        obj.GetComponent<EnemyDamageUI>().Initialize(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        var gameObj = other.gameObject;

        if (gameObj.tag == "Player")
        {
            gameObj.GetComponent<PlayerController>().Damage(1);
        }
    }

}
