using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isSpawnedEffect = false;
    public GameObject explosionEffect;
    private Transform target;
    public float speed;

    public ParticleSystem particleEffect1;
    public ParticleSystem particleEffect2;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (target == null)
        {
            transform.position = Vector3.forward;
            BulletDestroyedEvent();
        }
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.transform.position) <= 0.25f)
            {
                HitTarget();
            }
        }
        
    }

    void HitTarget()
    {
        BulletDestroyedEvent();
        Destroy(target.gameObject, 0.1f);
    }

    void BulletDestroyedEvent()
    {
        if (!isSpawnedEffect)
        {
            GameObject tempGO = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(tempGO, 5f);
            isSpawnedEffect = true;
        }
        particleEffect1.Stop();
        particleEffect2.Stop();
        particleEffect1.gameObject.transform.parent = null;
        particleEffect2.gameObject.transform.parent = null;
        Destroy(particleEffect1.gameObject, 5f);
        Destroy(particleEffect2.gameObject, 5f);
        
        Destroy(gameObject, 0.1f);
    }
}
