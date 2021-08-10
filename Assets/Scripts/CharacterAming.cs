using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAming : MonoBehaviour
{
    public float turnSpeed = 15f;
    Camera mainCamera;
    RayCAstWeapon weapon;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RayCAstWeapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.startFiring();
        }

        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }

        weapon.UpdateBullet(Time.deltaTime);

        if (Input.GetButtonUp("Fire1"))
        {
            weapon.stopFiring();
        }
    }
}
