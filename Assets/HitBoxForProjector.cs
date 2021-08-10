using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxForProjector : MonoBehaviour
{
    public ProjectorDamage p_damage;

    public void OnRaycastHit(RayCAstWeapon weapon)
    {
        p_damage.TakeDamage(weapon.damage);
    }
}
