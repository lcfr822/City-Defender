using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool canFire = true;
    [SerializeField] private bool fullAuto = false;
    private Animator turretAnimator;
    private GameObject selectedRound;
    private Transform subassembly;

    public float minFirePower, maxFirePower = 0.0f;
    public GameObject[] roundPrefabs = new GameObject[3];
    public Vector3[] minMaxAzimuth = new Vector3[2];

    // Start is called before the first frame update
    void Start()
    {
        subassembly = transform.GetChild(0);
        turretAnimator = subassembly.GetComponent<Animator>();
        turretAnimator.speed = 3.0f;
        selectedRound = roundPrefabs[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate turret subassembly
        subassembly.LookAt2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1.0f);
        if(subassembly.eulerAngles.z > 90.0f && subassembly.eulerAngles.z < 300.0f) { subassembly.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f); }
        else if (subassembly.eulerAngles.z > 300.0f) { subassembly.eulerAngles = Vector3.zero; }

        // Fire turret
        if(Input.GetButtonDown("Fire1") && canFire && !fullAuto)
        {
            StartCoroutine(SemiAutoFire());
        }
        else if (Input.GetButton("Fire1") && canFire && fullAuto)
        {
            StartCoroutine(FullAutoFire());
        }
    }

    private IEnumerator SemiAutoFire()
    {
        canFire = false;
        turretAnimator.SetTrigger("Firing");
        
        GameObject firedRound = Instantiate(selectedRound, subassembly.TransformPoint(new Vector3(0.585f, 0.055f, 0.0f)), subassembly.rotation);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        firedRound.GetComponent<Rigidbody2D>().velocity = firedRound.transform.right * Vector2.Distance(transform.position, mousePos).Map(0, 20, minFirePower, maxFirePower);
        
        yield return new WaitForSeconds(0.5f);
        
        turretAnimator.ResetTrigger("Firing");
        canFire = true;
    }

    private IEnumerator FullAutoFire()
    {
        canFire = false;
        turretAnimator.SetTrigger("Firing");
        
        GameObject firedRound = Instantiate(selectedRound, subassembly.TransformPoint(new Vector3(0.585f, 0.055f, 0.0f)), subassembly.rotation);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        firedRound.GetComponent<Rigidbody2D>().velocity = firedRound.transform.right * Vector2.Distance(transform.position, mousePos).Map(0, 20, minFirePower, maxFirePower);
        
        yield return new WaitForSeconds(0.2f);

        turretAnimator.ResetTrigger("Firing");
        canFire = true;
    }
}
