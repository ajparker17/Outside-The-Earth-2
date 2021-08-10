using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCAstWeapon : MonoBehaviour
{
    class Bullet
    {
        public float Time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }
    public bool isFiring = false;
    public int fireRate = 25;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform raycastOrigin;
    public Transform rayCastDestination;
    public float realoadTime = 50;
    public float damage = 30f;

    public GameObject reloadUI;
    public int ammoCount = 30;
    public int clipSize =  30;

    public Ray ray;
    public RaycastHit hitInfo;
    float acumulatedTime;
    List<Bullet> bullets = new List<Bullet>();
    float maxLifeTime = 3.0f;
   
    public AmmoWidget ammoWidget;

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.Time) + (0.5f * gravity * bullet.Time * bullet.Time);
    }

    Bullet createBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.Time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

   public void startFiring()
    {
        isFiring = true;
        acumulatedTime = 0.0f;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        acumulatedTime += deltaTime;
        float fireInterval = 1f / fireRate;
        while (acumulatedTime >= 0.0f)
        {
            FireBullet();
            acumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullet(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    public void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.Time >= maxLifeTime );
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.Time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RayCastSegment(p0,p1,bullet);
        });
    }

    void RayCastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.green, 2.0f);
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            bullet.tracer.transform.position = hitInfo.point;
            bullet.Time = maxLifeTime;
        }
        else
        {
            bullet.tracer.transform.position = end;
        }

        var hitBox = hitInfo.collider.GetComponent<HitBox>();
        if (hitBox)
        {
            hitBox.OnRaycastHit(this);
        }

        var hitBox1 = hitInfo.collider.GetComponent<HitBoxForServer>();
        if (hitBox1)
        {
            hitBox1.OnRaycastHit(this);
        }

    }

    public void FireBullet()
    {

        if (ammoCount <= 0)
        {
            return;
        }
        ammoCount--;


        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (rayCastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = createBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);


    }

    public void stopFiring()
    {
        isFiring = false;
    }

    private void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.R) || ammoCount <= 0)
        {
            realoadTime--;
            reloadUI.SetActive(true);
            if(realoadTime <= 0) 
            {
                Reload();
                realoadTime = 50;
            }
            
        }

        if (isFiring)
        {
            ammoWidget.Refresh(ammoCount);
        }
    }


    void Reload()
    {
        Debug.Log("Reload Called");
        reloadUI.SetActive(false);
        ammoCount = clipSize;
        ammoWidget.Refresh(ammoCount);
    }

 
}
