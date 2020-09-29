using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageUI : MonoBehaviour
{
    private float damageTimer;

    [SerializeField, Header("ダメージが消えるまでの時間")]
    private float damageEndTime = 0.5f;

    private Text damageText;

    void Start()
    {

    }

    public void Initialize(float damage)
    {
        damageTimer = 0.0f;

        damageText = GetComponentInChildren<Text>();

        damageText.text = damage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;

        if (damageTimer > damageEndTime)
        {
            Destroy(gameObject);
        }
    }
}
