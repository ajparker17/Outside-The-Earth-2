using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxForServer : MonoBehaviour
{
    public serverDamage server;

    public void OnRaycastHit(RayCAstWeapon weapon)
    {
        server.TakeDamage(weapon.damage);
    }
}
