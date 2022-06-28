using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //public int gunDemage = 1;
    public float fireRate = .25f;
    public float weponRange = 50f;
    //public float hitForce = 100f;
    public Transform gunEnd;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private LineRenderer laserLine;
    private float nexFire;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        nexFire = Time.time + fireRate;
        //turn laser effect on and off 
        StartCoroutine(ShotEffect());

        Vector3 rayOrigin = -transform.position;
        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, transform.forward, out hit, weponRange))
        {
            laserLine.SetPosition(1, other.gameObject.transform.position);
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (transform.forward * weponRange));
        }
    }
    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
