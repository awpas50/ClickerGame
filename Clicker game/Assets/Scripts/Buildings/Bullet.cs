﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isSpawnedEffect = false;
    public GameObject explosionEffect;
    public Transform target;
    private float initialSpeed;
    [SerializeField] private float speed;

    public ParticleSystem particleEffect1;
    public ParticleSystem particleEffect2;

    public Vector3 dir;
    
    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void Start()
    {
        initialSpeed = speed;

        if (target != null)
        {
            dir = (transform.position - target.transform.position).normalized;
        }
    }
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (Time.timeScale == 0 && target == null)
        {
            speed = 0;
            transform.position -= dir * 0f;
        }
        else if(Time.timeScale == 0 && target != null)
        {
            speed = 0;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else if (target == null)
        {
            speed = initialSpeed;
            transform.position -= dir * 0.2f;
            Destroy(gameObject, 2f);
        }
        else if (target != null)
        {
            speed = initialSpeed;
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
        Destroy(gameObject, 0.1f);
        Destroy(target.gameObject, 0.1f);
    }

    void BulletDestroyedEvent()
    {
        if (!isSpawnedEffect)
        {
            // Audio
            int seed = Random.Range(0, 3);
            if (seed == 0)
            {
                AudioManager.instance.Play(SoundList.explosion1);
            }
            else if (seed == 1)
            {
                AudioManager.instance.Play(SoundList.explosion2);
            }
            else
            {
                AudioManager.instance.Play(SoundList.explosion3);
            }

            particleEffect2.Stop();
            Destroy(particleEffect2.gameObject, 1f);

            particleEffect1.Stop();
            particleEffect1.gameObject.transform.parent = null;
            Destroy(particleEffect1.gameObject, 3f);

            GameObject tempGO = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(tempGO, 3f);
            isSpawnedEffect = true;
        }
    }
}