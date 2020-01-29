using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShellManager : MonoBehaviour
{
    protected bool faceForward = true;
    protected GameObject explosion;
    protected Vector3 oldPosition;

    public int[] minMaxDamages = new int[2];
    public GameObject shellExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void FaceForward(Vector3 moveDirection)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected virtual void DetonateShell()
    {
        explosion = Instantiate(shellExplosion, transform.position, Quaternion.identity);
        explosion.GetComponent<Animator>().SetTrigger("Detonate");
    }

    protected void HandleCollision(Collision2D collision)
    {
        BaseEnemyController controller = collision.gameObject.GetComponent<BaseEnemyController>();
        if (controller != null)
        {
            controller.TakeDamage(Random.Range(minMaxDamages[0], minMaxDamages[1]));
        }
    }
}
