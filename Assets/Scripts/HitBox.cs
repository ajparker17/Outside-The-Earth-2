using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Enemy health;
   
    public void OnRaycastHit(RayCAstWeapon weapon)
    {
        health.TakeDamage(weapon.damage);
    }
}
