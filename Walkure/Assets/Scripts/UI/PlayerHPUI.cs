using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField, Header("HP")]
    private GameObject[] hpObjArray = new GameObject[0];

    private int hpLen;

    private Image[] hpImageArray;

    private PlayerController pc;

    private float currentHp;

    private float heartNum;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        currentHp = pc.Hp;

        heartNum = 5;
        hpLen = hpObjArray.Length;

        hpImageArray = new Image[hpLen];

        for (int i = 0; i < hpLen; i++)
        {
            hpImageArray[i] = hpObjArray[i].GetComponentInChildren<Image>();
        }


    }


    void Update()
    {
        LifeControll();
    }

    private void LifeControll()
    {
        currentHp = pc.Hp;

        heartNum = currentHp / 2;

        //HPが偶数の時
        if (currentHp % 2 == 0)
        {
            for (int i = 0; i < hpLen; i++)
            {
                if (i > heartNum - 1)
                {
                    hpImageArray[i].fillAmount = 1;
                    hpObjArray[i].SetActive(false);
                }

                else
                {
                    hpObjArray[i].SetActive(true);
                    hpImageArray[i].fillAmount = 1;
                }
            }
        }

        //HPが奇数の時
        else
        {
            for (int i = 0; i < hpLen; i++)
            {
                if (i > heartNum)
                {
                    hpImageArray[i].fillAmount = 1;
                    hpObjArray[i].SetActive(false);
                }
                
                else if (i == (int)heartNum)
                {
                    hpObjArray[i].SetActive(true);
                    hpImageArray[i].fillAmount = 0.5f;
                }


                else
                {
                    hpObjArray[i].SetActive(true);
                    hpImageArray[i].fillAmount = 1;
                }
            }
        }
    }
}
