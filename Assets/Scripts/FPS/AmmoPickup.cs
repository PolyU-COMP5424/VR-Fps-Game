using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoCount = 5;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        GunSystem weapon = collision.gameObject.GetComponent<GunSystem>();

        if (collision.collider.GetComponent<PlayerMovement>() != null)
        {
            weapon.AddAmmo(ammoCount);
            Destroy(gameObject);
        }
    }
}
