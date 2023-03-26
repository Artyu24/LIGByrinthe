using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chronometer : MonoBehaviour
{
    [SerializeField] float chronoTime;
    [SerializeField] bool isRunning;

    private void Awake()
    {
        
    }

    void Start()
    {
        ResetChrono();
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
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
