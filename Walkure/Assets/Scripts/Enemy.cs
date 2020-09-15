using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField,Header("最大HP")]
    private float maxHP = 0;

    private float currentHP;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
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
