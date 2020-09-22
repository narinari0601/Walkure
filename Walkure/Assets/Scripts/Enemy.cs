using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField,Header("最大HP")]
    private float maxHP = 0;

    private float currentHP;

    private Slider hpSlider;

    private PlayerController pc;
    

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        currentHP = maxHP;

        hpSlider = GetComponentInChildren<Slider>();

        hpSlider.value = 1;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        hpSlider.value = currentHP / maxHP;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void AttackedBullet(float damage)
    {
        currentHP -= damage;
    }


    public void AttackedSpecialBullet(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            pc.CurrentExperience++;
        }
    }
  
}
