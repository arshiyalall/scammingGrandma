using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject projectile;
    private bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            canShoot = false;
            StartCoroutine(ResetShoot());
        }
    }

    IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(0.4f);
        canShoot = true;
    }
}
