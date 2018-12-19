using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalController : MonoBehaviour
{
    //SerializedField
    [SerializeField]
    float fireSpeed = 1;
    [SerializeField]
    float projSpeed = 3;
    [SerializeField]
    float scanSpeed = 1;
    [SerializeField]
    float descanSpeed = 1;
    [Range(1, 10), SerializeField]
    float maxScanRange = 5;
    [Range(1, 10), SerializeField]
    float maxShootRange = 5;

    //Private
    bool hitting = false;
    GameObject objectHittedBefore;
    GameObject objectHitted;
    GameObject objectOnScan;    
    float fireRate = 0;
    GameObject cloneProj;

    //Public
    public int scanProgress = 0;
    public GameObject scannedObject;
    public UIManager uiMan;

    void Update()
    {
        Vector2 playerPos = transform.position;
        Vector2 crystalDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 crystalOrigin = playerPos + crystalDirection.normalized;
        Vector2 crystalScanTarget = Vector2.ClampMagnitude(crystalDirection, maxScanRange);
        Vector2 crystalShootTarget = Vector2.ClampMagnitude(crystalDirection, maxShootRange);
        float scanRange = Vector2.Distance(crystalOrigin, playerPos + crystalScanTarget);

        RaycastHit2D hit = Physics2D.Raycast(crystalOrigin, crystalDirection, scanRange); //raycast's definition
        Debug.DrawRay(crystalOrigin, new Vector2(crystalScanTarget.x, (crystalScanTarget.y / 2)), Color.red); //draws the line in scene/debug BUGGÉ PUT1

        //SCAN
        if (hit.collider != null && Input.GetKey("mouse 1"))
        {
            if (hit.collider.gameObject.GetComponent<AppleBehaviour>().isScannable && hit.collider.gameObject != scannedObject && hit.collider.isTrigger)
            {
                objectHittedBefore = objectHitted;            
                objectHitted = hit.transform.gameObject;

                //If no registred objectOnScan
                if (objectOnScan == null)
                {
                    StopAllCoroutines();

                    //If it's a different object than the last one scanned => Reinitialise scanProgress
                    if (objectHittedBefore.tag != objectHitted.tag)
                    {
                        scanProgress = 0;
                    }

                    StartCoroutine(Scan());
                }
                //If hit object is the same as the registered one
                else if (objectOnScan.tag == objectHitted.tag)
                {
                    if (scanProgress == 5)
                    {
                        scannedObject = GameObject.FindWithTag(objectOnScan.tag);
                        uiMan.SendMessage("ChangeImageInCrystalSlot");
                        StopAllCoroutines();                        
                        scanProgress = 0;
                    }
                }
                //If new object hitted directly => Reinitialise scanProgress
                else
                {
                    scanProgress = 0;
                }

                hitting = true;
                objectOnScan = objectHitted;
            }
        }
        //No object hit => DESCAN
        else if (hitting)
        {
            StopAllCoroutines();
            StartCoroutine(DeScan());
            hitting = false;
            objectOnScan = null;           
        }

        //SHOOT the scanned object
        if (Input.GetKeyDown("mouse 0") && Time.time > fireRate)
        {
            fireRate = Time.time + fireSpeed;

            cloneProj = (GameObject)Instantiate(scannedObject, crystalOrigin, scannedObject.transform.rotation);
            //not optimized at all
            if (scannedObject.name == "Apple")
            {
                cloneProj.GetComponent<AppleBehaviour>().targetPos = playerPos + crystalShootTarget;
                cloneProj.GetComponent<AppleBehaviour>().projectileSpeed = projSpeed;
                cloneProj.GetComponent<AppleBehaviour>().isScannable = false;
                cloneProj.GetComponent<AppleBehaviour>().isFired = true;
            }
            else if (scannedObject.name == "Shroom")
            {
                cloneProj.GetComponent<ShroomBehaviour>().targetPos = playerPos + crystalShootTarget;
                cloneProj.GetComponent<ShroomBehaviour>().projectileSpeed = projSpeed;
                cloneProj.GetComponent<ShroomBehaviour>().isScannable = false;
                cloneProj.GetComponent<ShroomBehaviour>().isFired = true;
            }
        }
    }

    IEnumerator Scan()
    {
        while (scanProgress < 5)
        {
            Debug.Log(scanProgress);
            yield return new WaitForSeconds(scanSpeed);
            scanProgress++;
        }       
    }

    IEnumerator DeScan()
    {
        while (scanProgress > 0)
        {
            Debug.Log(scanProgress);
            yield return new WaitForSeconds(descanSpeed);
            scanProgress--;            
        }
    }    
}