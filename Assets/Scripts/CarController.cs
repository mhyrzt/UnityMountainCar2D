using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody2D backTire;
    public Rigidbody2D frontTire;
    private Rigidbody2D carRigidBody;
    public float speed = 20;
    public float carTorque = 10;

    // Start is called before the first frame update
    void Start()
    {
        carRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AddTorque(-Input.GetAxis("Horizontal"));
    }

    public void AddTorque(float direction)
    {
        backTire.AddTorque(direction * speed * Time.deltaTime);
        frontTire.AddTorque(direction * speed * Time.deltaTime);
        carRigidBody.AddTorque(direction * carTorque * Time.deltaTime);
    }
}
