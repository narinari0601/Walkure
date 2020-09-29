using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperienceUI : MonoBehaviour
{

    private PlayerController pc;

    private Slider expSlider;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        expSlider = GetComponentInChildren<Slider>();

        expSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var exp = pc.CurrentExperience / pc.LevelUpExperience;


        if (pc.CurrentExperience >= pc.LevelUpExperience)
        {
            exp = exp - pc.PlayerLevel + 1.0f;
        }


        if (pc.PlayerLevel >= pc.MaxLevel)
        {
            expSlider.value = 1;
            return;
        }

        expSlider.value = exp;
    }
}
