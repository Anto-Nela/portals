using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStuff : MonoBehaviour
{
    //how often player can fire
    public float fireRate = .25f;
    //how far can shoot
    public float weponRange = 70f;
    //empty game object that  we will attach to 
    //mark the position at the end of the gun at which our lazer line will begin
    public Transform gunEnd;
    public Camera fpsCam;

    //takes array between 2 or more points in 3d space and draws straight line between each of them
    private LineRenderer laserLine;
    //holds time in which player will be allowed to fire again after firing
    private float nexFire;

    private Transform bluePortaltr;
    private Transform orangePortaltr;
    private SpriteRenderer bluePortal;
    private SpriteRenderer orangePortal;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        fpsCam = Component.FindObjectOfType<Camera>();
        
        bluePortaltr = GameObject.FindGameObjectWithTag("BluePortal").GetComponent<Transform>();
        orangePortaltr = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<Transform>();
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal").GetComponent<SpriteRenderer>();
        bluePortal.enabled = false;
        orangePortal = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<SpriteRenderer>();
        orangePortal.enabled = false;
    }

    void FixedUpdate()
    {
        //check if fire button is pressed and if enought time has passed
        if (Input.GetButtonDown("Fire1") && Time.time > nexFire)
        {
             Fire();
        }
       else if (Input.GetButtonDown("Fire2") && Time.time > nexFire)
        {
            Fire();
        } 

    }

    void Fire()
    {
            nexFire = Time.time + fireRate;

        //we need origin point for array
        // we want it to be at the center of the camera, so we use this (and 0.5)
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        //we need to hold the info returned form our ray if it hits a game object with a collider
        RaycastHit hit;

        // we need the start and end positions of the laser line when the player fires
        laserLine.SetPosition(0,gunEnd.position);

        // to cast the ray
        // store additional info about the object we hit(rigid body, collider, surface normal)
        //in our raycast hit using out hit
        if (Physics.Raycast(rayOrigin,fpsCam.transform.forward, out hit, weponRange))
        {
            //if it hits sth set dhe second position
            laserLine.SetPosition(1,hit.point);
 
            Collider wall = hit.collider;
            string tg = hit.collider.gameObject.tag;
            if (wall!=null && Input.GetMouseButtonDown(0))
            {
                if (tg=="Ground")
                {
                    bluePortaltr.rotation = Quaternion.Euler(90,0,0);
                    bluePortal.enabled = true;
                    bluePortaltr.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z - 0.02f);
                }
                if (tg=="Wall")
                {
                    bluePortaltr.rotation = Quaternion.Euler(0, 0, 0);
                    bluePortal.enabled = true;
                    bluePortaltr.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z - 0.02f);
                }
                if (tg == "Wall2")
                {
                    bluePortaltr.rotation = Quaternion.Euler(0, 90, 0);
                    bluePortal.enabled = true;
                    bluePortaltr.position = new Vector3(hit.point.x+0.1f, hit.point.y + 0.1f, hit.point.z - 0.02f);
                }
            }

            else if (wall != null && Input.GetMouseButtonDown(1))
            {
                if (tg=="Ground")
                {
                    orangePortaltr.rotation = Quaternion.Euler(90, 0, 0);
                    orangePortal.enabled = true;
                    orangePortaltr.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z - 0.02f);
                }
                if (tg=="Wall")
                {
                    orangePortaltr.rotation = Quaternion.Euler(0, 0, 0);
                    orangePortal.enabled = true;
                    orangePortaltr.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z - 0.02f);
                }
                if (tg == "Wall2")
                {
                    orangePortaltr.rotation = Quaternion.Euler(0, 90, 0);
                    orangePortal.enabled = true;
                    orangePortaltr.position = new Vector3(hit.point.x+0.1f, hit.point.y + 0.1f, hit.point.z - 0.02f);
                }

            }
        } 
    }

}
