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
    

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        currentHP = maxHP;

        hpSlider = GetComponentInChildren<Slider>();

        hpSlider.value = 1;
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

  
}
