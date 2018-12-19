using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomBehaviour : MonoBehaviour
{
    //Public
    public bool isScannable = true;
    public bool isFired = false;
    public float projectileSpeed = 3;
    public Vector2 targetPos;

    void Update()
    {
        if (isFired)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);
        }
    }
}