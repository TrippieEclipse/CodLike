using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum AnimType 
{
    Run,
    Shoot,
    Reload
}
public class Shooting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText; 

    [SerializeField] private Animator weaponAnimator;

    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float fireDelay;

    [SerializeField] private bool readyToFire = true;

    [SerializeField] private bool reloading = false;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform playerBodyTrans;

    [SerializeField] private float fireTime;

    [SerializeField] private GameObject bulletImpact;

    [SerializeField] private float cdTimer;

    [SerializeField] private int ammo;
    [SerializeField] private int magazineCount;
    [SerializeField] private int magazineSize;


    private void Update()
    {
        ammoText.text = magazineCount + " / " + ammo;

        if (Input.GetButtonDown("Fire1")) 
        {
            ActivateAnim(AnimType.Shoot, true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ActivateAnim(AnimType.Shoot, false);
        }
        if (Input.GetButton("Fire1") && !reloading) 
        {
            fireTime += Time.deltaTime;

            if(cdTimer >= 30) 
            {
                fireTime = 0f;
            }
            if(readyToFire && magazineCount > 0) 
            {
                Shoot();
                magazineCount--;
                cdTimer = 0;
            }
        }
        else 
        {
            cdTimer ++;
        }
        if (Input.GetKeyDown(KeyCode.R) && magazineCount != magazineSize && ammo > 0) 
        {
            reloading = true;
            ActivateAnim(AnimType.Reload, null);
            Invoke("Reload", 1f);
        }
    }

    public void Shoot()
    {
        Vector3 fireDirection = shootPoint.transform.forward;

        Quaternion shootRotation = Quaternion.LookRotation(fireDirection);

        Debug.DrawRay(shootPoint.position, shootRotation * Vector3.forward, Color.black);

        RaycastHit hit;

        if (Physics.Raycast(shootPoint.position, shootRotation * Vector3.forward, out hit, range))                // Raycast checks to see if player is 
        {
            if(hit.transform.gameObject.tag != "Player") 
            {
                GameObject impactObj = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }

        }

        readyToFire = false;

        Invoke("SetReadyToFire", fireDelay);
    }

    public void SetReadyToFire()
    {
        readyToFire = true;
    }

    public void ActivateAnim(AnimType type, bool? setActive) 
    { 
        if(type == AnimType.Run) 
        {
            if ((bool)setActive) 
            { 
            
            }
            else 
            {
            
            }
        }

        else if(type == AnimType.Shoot) 
        {
            if ((bool)setActive) 
            { 
            
            }
            else 
            {
            
            }
        }

        else if(type == AnimType.Reload) 
        {
            weaponAnimator.SetTrigger("Reload");
        }
    }

    public void Reload() 
    {

        int bulletsNeeded = magazineSize - magazineCount;
        if (ammo - bulletsNeeded >= 0)
        {
            magazineCount += bulletsNeeded;
            ammo -= bulletsNeeded;
        }
        else
        {
            magazineCount += ammo;
            ammo = 0;
        }
        
        reloading = false;
    }

}
