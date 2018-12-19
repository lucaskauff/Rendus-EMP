using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanProgressBarAnim : MonoBehaviour
{
    //Private
    Animator myAnim;

    //Public
    public int scanProgress = 0;

    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        myAnim.SetInteger("ScanProgress", scanProgress);
    }
}