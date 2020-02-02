using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAEnemyController : BaseEnemyController
{
    private float lerpStartTime = 0.0f;
    private Vector2 startPosition = Vector2.zero;
    private Animator effectAnimator;

    // Start is called before the first frame update
    void Start()
    {
        effectAnimator = transform.GetChild(0).GetComponent<Animator>();
        transform.position = new Vector3(transform.position.x, Random.Range(-5.5f, 9.25f), 0.0f);
        startPosition = transform.position;
        endPosition.y = startPosition.y;
        lerpStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cruising) { transform.position = Cruise(startPosition, lerpStartTime); }
        if (Input.GetKeyDown(KeyCode.K)) { TakeDamage(health); }
        if (!((Vector2)transform.position - OldPosition).Equals(Vector3.zero) && FaceDirectionOfTravel) { FaceForward((Vector2)transform.position - OldPosition); }
    }

    private void LateUpdate()
    {
        CrashSpeed = -(Vector2.Distance(transform.position, OldPosition) / (Time.time - OldPositionTime) * 2.0f);
        OldPosition = transform.position;
        OldPositionTime = Time.time;
    }

    public override void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            base.TakeFatalDamage();
            StartCoroutine(CrashRoutine());
        }
        else { base.TakeDamage(damage); }
    }

    protected IEnumerator CrashRoutine()
    {
        effectAnimator.SetTrigger("Fireball");
        yield return new WaitForSeconds(0.4f);
        effectAnimator.ResetTrigger("Fireball");
        effectAnimator.SetTrigger("Grounded");
    }
}
