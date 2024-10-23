using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        GunSystem weapon = collision.gameObject.GetComponent<GunSystem>();

        if (weapon)
        {
            weapon.AddAmmo(weapon.maxAmmoSize);
            Destroy(gameObject);
        }
    }
}
