using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    //SerializedField
    [SerializeField]
    float fireSpeed = 1;
    [SerializeField]
    float scanSpeed = 1;
    [SerializeField]
    float descanSpeed = 1;
    [Range(1, 10), SerializeField]
    float scanRange = 5;

    //Private
    int scanProgress = 0;
    bool hitting;
    GameObject objectHittedBefore;
    GameObject objectHitted;
    GameObject objectOnScan;
    GameObject scannedObject;
    float fireRate = 0;
    GameObject cloneProj;
    GameObject progBar;
    GameObject crystalSlot;

    void Start()
    {
        progBar = GameObject.Find("ScanProgressBar");
        crystalSlot = GameObject.Find("CrystalSlot");
    }

    void Update()
    {
        Vector2 crystalDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 crystalDirectionClamped = Vector2.ClampMagnitude(crystalDirection, scanRange);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, crystalDirection, scanRange); //raycast's definition
        Debug.DrawRay(transform.position, crystalDirectionClamped, Color.red); //draws the line

        progBar.GetComponent<ScanProgressBarAnim>().scanProgress = scanProgress; //send info to ProgressBar     

        //SCAN
        if (hit.collider != null)
        {
            if (Input.GetKey("mouse 1") && hit.collider.gameObject.tag == "ScannableObject" && hit.collider.gameObject != scannedObject)
            {
                objectHittedBefore = objectHitted;            
                objectHitted = hit.transform.gameObject;

                //If no registred objectOnScan => Entering
                if (objectOnScan == null)
                {
                    StopAllCoroutines();

                    //If it's a different object than the last one scanned => Reinitialise scanProgress
                    if (objectHittedBefore != objectHitted)
                    {
                        scanProgress = 0;
                    }

                    StartCoroutine(Scan());
                }
                //If hit object is the same as the registered one => Stay
                else if (objectOnScan.GetInstanceID() == objectHitted.GetInstanceID())
                {
                    if (scanProgress == 5)
                    {
                        scannedObject = objectOnScan;
                        crystalSlot.GetComponent<SpriteRenderer>().sprite = scannedObject.GetComponent<SpriteRenderer>().sprite; //AMELIORABLE
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

            cloneProj = (GameObject)Instantiate(scannedObject, transform.position, scannedObject.transform.rotation);
            cloneProj.GetComponent<ObjectBehaviour>().targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cloneProj.GetComponent<ObjectBehaviour>().isScannable = false;
            cloneProj.GetComponent<ObjectBehaviour>().isFired = true;
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