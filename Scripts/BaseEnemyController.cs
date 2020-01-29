using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour
{
    private bool recalculateLerp = true;
    private float timeToLerp = 3.0f;
    private float lerpStartTime = 0.0f;
    private Vector2 startPosition = Vector2.zero;
    private Vector2 endPosition = new Vector2(-20.0f, 9.25f);

    protected bool cruising = true;
    protected bool faceForward = true;
    protected float crashSpeed = 0.0f;
    protected float oldPositionTime = 0.0f;
    protected Vector2 oldPosition;

    public int health = 500;
    public float speed = 1.0f;
    public float[] minMaxClimbAngles = new float[] { 20.0f, 30.0f };
    public GameObject[] weaponPrefabs = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-5.5f, 9.25f), 0.0f);
        startPosition = transform.position;
        endPosition.y = startPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Cruise()
    {
        if (recalculateLerp) { 
            startPosition = transform.position;
            timeToLerp = Vector2.Distance(startPosition, endPosition) / speed;
            recalculateLerp = false;
        }

        float timeSinceStarted = Time.time - lerpStartTime;
        float percentComplete = timeSinceStarted / timeToLerp;

        transform.position = Vector3.Lerp(startPosition, endPosition, percentComplete);
        if(percentComplete >= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    protected void FaceForward(Vector3 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            cruising = false;
            health = 0;
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().mass = 100;
            GetComponent<Rigidbody2D>().velocity = new Vector2(crashSpeed, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.ToLower().Equals("ground")){
            faceForward = false;
        }
    }
}
