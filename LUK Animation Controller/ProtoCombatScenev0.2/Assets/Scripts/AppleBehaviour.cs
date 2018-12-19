using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBehaviour : MonoBehaviour
{
    //SerializedField
    [SerializeField]
    int damage = 1;

    //Public
    public bool isScannable = true;
    public bool isFired = false;
    public float projectileSpeed = 3;
    public Vector2 targetPos;

    //Private
    Animator myAnim;
    CircleCollider2D myCol;
    bool col = false;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        myCol = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (isFired)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);
        }

        if (col || new Vector2(transform.position.x, transform.position.y) == targetPos)
        {
            Explosion();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isFired)
        {
            if (other.gameObject.name == "Player")
            {
                col = true;            
                other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
            }

            if (other.gameObject.name == "Horn")
            {
                col = true;
                other.gameObject.GetComponentInParent<EnemyHealthManager>().HurtEnemy(damage);
                other.gameObject.GetComponentInParent<EnemyHealthManager>().SendMessage("HornIsHit");
            }

            if (other.gameObject.name == "Shell")
            {
                col = true;
                other.gameObject.GetComponentInParent<EnemyHealthManager>().HurtEnemy(damage);
                other.gameObject.GetComponentInParent<EnemyHealthManager>().SendMessage("ShellIsHit");
            }

            if (other.gameObject.name == "Heart")
            {
                col = true;
                other.gameObject.GetComponentInParent<EnemyHealthManager>().HealEnemy(damage);
                other.gameObject.GetComponentInParent<EnemyHealthManager>().SendMessage("HeartIsHit");
            }
        }        
    }

    void Explosion()
    {
        isFired = false;
        myCol.enabled = !myCol.enabled;
        myAnim.SetTrigger("Explode");

        if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}