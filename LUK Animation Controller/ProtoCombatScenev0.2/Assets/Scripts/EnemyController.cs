using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //SerializedField
    [SerializeField]
    float rotationSpeed = 1.5f;
    [SerializeField]
    float fireSpeed = 1;
    [SerializeField]
    float projSpeed = 3;
    [SerializeField]
    float targetDistance = 2;
    [SerializeField]
    float stunTime = 1;

    //Private
    GameObject horn;
    GameObject shell;
    GameObject heart;
    float fireRate = 0;
    GameObject cloneProj;
    bool isStunned = false;

    //Public
    public GameObject projectile;
    public Transform spawner;
    public Transform target;

    void Start()
    {
        horn = GameObject.Find("Horn");
        shell = GameObject.Find("Shell");
        heart = GameObject.Find("Heart");

        projectile = GameObject.Find("Apple");
    }

    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position, transform.TransformDirection(Vector3.up));
        Quaternion rotationSup = new Quaternion(0, 0, rotation.z, rotation.w);

        if (isStunned)
        {
            StartCoroutine(Stun());
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationSup, rotationSpeed * Time.deltaTime);

            if (Time.time > fireRate)
            {
                fireRate = Time.time + fireSpeed;

                cloneProj = (GameObject)Instantiate(projectile, spawner.position, projectile.transform.rotation);
                cloneProj.GetComponent<AppleBehaviour>().targetPos = transform.position + ((target.position - transform.position) * targetDistance);
                cloneProj.GetComponent<AppleBehaviour>().projectileSpeed = projSpeed;
                cloneProj.GetComponent<AppleBehaviour>().isScannable = true;
                cloneProj.GetComponent<AppleBehaviour>().isFired = true;
            }
        }        
    }

    public void HornIsHit()
    {
        horn.GetComponent<Animator>().SetTrigger("IsHit");
    }

    public void ShellIsHit()
    {
        //isStunned = true;
        shell.GetComponent<Animator>().SetTrigger("IsHit");
    }

    public void HeartIsHit()
    {
        heart.GetComponent<Animator>().SetTrigger("IsHit");
    }

    IEnumerator Stun ()
    {
        Debug.Log("Stunned!");
        yield return new WaitForSeconds(stunTime);

        isStunned = false;
    }
}