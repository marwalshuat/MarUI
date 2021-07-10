﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public float hitPoints = 3f;
    public float maxHitPoints = 3f;
    public Image healthBar;

    public float mana = 3f;
    public float maxMana = 3f;
    public Image manaBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = hitPoints / maxHitPoints;
        manaBar.fillAmount = mana / maxMana;
    }
}
