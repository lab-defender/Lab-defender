using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 20f;
    public GameObject hitEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void HitTarget()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation) as GameObject;
        Destroy(effect, 0.3f);
        Destroy(gameObject);
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float movement = speed * Time.deltaTime;

        if(direction.magnitude <= movement)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * movement, Space.World);
        
    }
}
