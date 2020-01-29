using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAShellManager : BaseShellManager
{
    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = transform.position - oldPosition;
        if (!moveDirection.Equals(Vector3.zero) && faceForward)
        {
            FaceForward(moveDirection);
        }

        if (explosion != null && explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            Destroy(explosion);
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        oldPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        faceForward = false;
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());

        HandleCollision(collision);
        DetonateShell();
    }
}
