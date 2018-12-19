using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    //SerializedField
    [SerializeField]
    float projectileSpeed = 5;

    //Public
    public bool isScannable = true;
    public bool isFired = false;
    public Vector2 targetPos;       

    void Update()
    {
        if(!isScannable)
        {
            gameObject.tag = "Untagged";
        }

        if (isFired)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);

            if (new Vector2(transform.position.x, transform.position.y) == targetPos)
            {
                isFired = false;
            }
        }
    }
}