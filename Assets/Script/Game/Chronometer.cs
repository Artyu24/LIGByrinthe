using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    [SerializeField] Text textObject;
    [SerializeField] float chronoTime;
    [SerializeField] string chronoTimeString;
    [SerializeField] bool isRunning;

    private void Awake()
    {
        textObject.GetComponent<TextMeshPro>();
}

    void Start()
    {
        ResetChrono();
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        textObject.text = MathF.Round(chronoTime, 2).ToString();
        if (isRunning) chronoTime += Time.deltaTime;
        
    }

    public float GetTime()
    {
        return MathF.Round(chronoTime, 2);
    }

    public void StopChrono()
    {
        isRunning = false;
    }
    public void ResumeChrono()
    {
        isRunning = true;
    }

    public void ResetChrono()
    {
        chronoTime = 0;
    }
}
