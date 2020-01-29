using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAEnemyController : BaseEnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cruising) { Cruise(); }
        if (Input.GetKeyDown(KeyCode.K)) { TakeDamage(health); }
        if (!((Vector2)transform.position - oldPosition).Equals(Vector3.zero) && faceForward) { FaceForward((Vector2)transform.position - oldPosition); }
    }

    private void LateUpdate()
    {
        crashSpeed = -(Vector2.Distance(transform.position, oldPosition) / (Time.time - oldPositionTime) * 2.0f);
        oldPosition = transform.position;
        oldPositionTime = Time.time;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
