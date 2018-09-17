using UnityEngine;
using System.Collections;


[System.Serializable]
public class Boundary
{
    // public Boundry limits
    public float xMin,xMax,zMin,zMax;
}

public class PlayerController : MonoBehaviour {

    // Public variables
    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float firerate;

    // Private variables
    private Rigidbody rb;
    private float nextFire;
    private AudioSource audioSource ;

    // upon starting the game
    void Start()
    {
        //get Rigidbody Component from Player Object
        rb = GetComponent<Rigidbody>();

        //get Audio Component from Player Object
        audioSource = GetComponent<AudioSource >();
    }
	
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + firerate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }

    // after everyframe
    void FixedUpdate()
    {
        // apply horizontal and vertical movement to the directional Inputs (Up, down, left, right)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create movement by applying Horizontal and Vertical movement
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply Movement and Speed to the Velocity of the Rigidbody
        rb.velocity = movement * speed;

        // Apply boundries to the Player Object
        rb.position = new Vector3 
        (
           Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
           0.0f, 
           Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );

        // Apply Tilt to the Player Object
        rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
