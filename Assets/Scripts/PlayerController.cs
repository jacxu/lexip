using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private GameController gc;


    public int speed, tilt;
    public Boundary boundary;

    public GameObject shotBlue;
    public GameObject shotRed;
    public GameObject playerExplosion;
    public GameObject playerHit;
    public Transform spawnSpot;

    public float  fireRate;

    private float nextFire;
    private bool shieldOn;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gc = MyController.GetGameController();
        shieldOn = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.U) && Time.time > nextFire && gc.BoltRedPoint != 0)
        {
            gc.BoltRedPoint--;
            nextFire = Time.time + fireRate;
            Instantiate(shotRed, spawnSpot.position - new Vector3(1, 0, 0), spawnSpot.rotation);
        }
        if (Input.GetKeyDown(KeyCode.I) && Time.time > nextFire && gc.BoltBluePoint != 0)
        {
            gc.BoltBluePoint--;
            nextFire = Time.time + fireRate;
            Instantiate(shotBlue, spawnSpot.position + new Vector3(1,0,0), spawnSpot.rotation);
        }

        shieldOn = false;
        if (Input.GetKey(KeyCode.J))
            ShieldOnOff("ShieldRed", true);
        else
            ShieldOnOff("ShieldRed", false);

        if (Input.GetKey(KeyCode.K))
            ShieldOnOff("ShieldBlue", true);
        else
            ShieldOnOff("ShieldBlue", false);

   



    }

    void ShieldOnOff(string name, bool value)
    {
        GameObject o = GameObject.Find("MyPlayer");
        Component[] trs = o.GetComponentsInChildren(typeof(Transform), true);
        foreach (Transform t in trs)
            if (t.name == name)
            {
                t.gameObject.SetActive(value);
                if (value) shieldOn = true;
            }
    }
 
    // Use this for initialization
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)

            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);          
     }


    void OnTriggerEnter(Collider other)
    {
        if (shieldOn)
            return;

        if (other.tag.Contains("EnemyBolt"))
        {
            gc.UpdateStats(-10, 0);
            StartCoroutine(PlayerHit(other.transform.position));
            Destroy(other.gameObject);
        }


    }

    public void DestroyPlayer()
    {
        if (transform != null && transform.gameObject != null)
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);       
            Destroy(transform.gameObject);
        }
    }

    IEnumerator PlayerHit(Vector3 pos)
    {
        Object obj = Instantiate(playerHit, pos + Vector3.up, transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Destroy(obj);
    }

}
