﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField]
    private Slider speedSlider;

    private void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnSubmitName(string name)
    {
        Debug.Log("Nome: " + name);
    }

    public void OnSpeedValue(float speed)
    {
        PlayerPrefs.SetFloat("speed", speed);
    }
}
