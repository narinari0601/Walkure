using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUI : MonoBehaviour
{
    private PlayerController pc;

    private Text levelText;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        pc = GameObject.Find("Player").GetComponentInChildren<PlayerController>();

        levelText = GetComponentInChildren<Text>();

        levelText.text = "Lv" + pc.PlayerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.PlayerLevel == pc.MaxLevel)
        {
            levelText.text = "MAX";
        }

        else
        {
            levelText.text = "Lv" + pc.PlayerLevel;
        }
        
    }
}
