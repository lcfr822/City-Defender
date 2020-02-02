using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    private bool recalculateLerp = true;
    private float timeToLerp = 3.0f;

    protected bool Cruising { get; set; } = true;
    protected bool FaceDirectionOfTravel { get; set; } = true;
    protected float CrashSpeed { get; set; } = 0.0f;
    protected float OldPositionTime { get; set; } = 0.0f;
    protected Vector2 OldPosition { get; set; } = Vector2.zero;
    protected Vector2 endPosition = new Vector2(-20.0f, 9.25f);

    public int health = 500;
    public float speed = 1.0f;
    public float[] minMaxClimbAngles = new float[] { 20.0f, 30.0f };
    public GameObject[] weaponPrefabs = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-5.5f, 9.25f), 0.0f);
        endPosition.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected Vector2 Cruise(Vector2 startPosition, float lerpStartTime)
    {
        Vector2 finalPosition = Vector2.zero;

        if (recalculateLerp) {
            startPosition = transform.position;
            timeToLerp = Vector2.Distance(startPosition, endPosition) / speed;
            recalculateLerp = false;
        }

        float timeSinceStarted = Time.time - lerpStartTime;
        float percentComplete = timeSinceStarted / timeToLerp;

        finalPosition = Vector3.Lerp(startPosition, endPosition, percentComplete);
        if(percentComplete >= 1.0f)
        {
            FindObjectOfType<EnemyWaveManager>().existingEnemies--;
            Destroy(gameObject);
        }

        return finalPosition;
    }

    protected void FaceForward(Vector3 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }

    public virtual void TakeFatalDamage()
    {
        Cruising = false;
        health = 0;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().mass = 100;
        GetComponent<Rigidbody2D>().velocity = new Vector2(CrashSpeed, 0.0f);
        FindObjectOfType<EnemyWaveManager>().existingEnemies--;
    }

    private IEnumerator GroundingRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.ToLower().Equals("ground")){
            FaceDirectionOfTravel = false;
            StartCoroutine(GroundingRoutine());
        }
    }
}
