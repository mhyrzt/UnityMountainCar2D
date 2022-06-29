using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CarAgent : Agent
{
    [SerializeField] private Rigidbody2D backTire;
    [SerializeField] private Rigidbody2D frontTire;
    [SerializeField] private float speed = 75.0F;
    [SerializeField] private float carTorque = 10.0F;
    [SerializeField] private Transform goal;
    private Rigidbody2D carRigidBody;
    private Vector3 position;

    void Start()
    {
        this.carRigidBody = GetComponent<Rigidbody2D>();
    }

    public override void OnEpisodeBegin()
    {
        this.transform.position = new Vector3(-5.16994F, -9.41631F, 0.0F);
        this.transform.rotation = Quaternion.Euler(Vector3.zero);

        this.carRigidBody.angularVelocity = 0F;
        this.carRigidBody.velocity = Vector2.zero;

        this.frontTire.angularVelocity = 0F;
        this.frontTire.velocity = Vector2.zero;

        this.backTire.angularVelocity = 0F;
        this.backTire.velocity = Vector2.zero;

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position.x);
        sensor.AddObservation(this.carRigidBody.velocity.x);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        SetReward(-1);
        this.AddTorque(actions);
        if (this.transform.position.x >= this.goal.position.x)
        {
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SetReward(-1);
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> action = actionsOut.DiscreteActions;
        float direction = -Input.GetAxisRaw("Horizontal");
        action[0] = this.GetDirectionIndex(direction);
    }

    private void AddTorque(float direction)
    {
        this.backTire.AddTorque(direction * this.speed * Time.deltaTime);
        this.frontTire.AddTorque(direction * this.speed * Time.deltaTime);
        this.carRigidBody.AddTorque(direction * this.carTorque * Time.deltaTime);
    }
    private void AddTorque(ActionBuffers actions)
    {
        this.AddTorque(this.GetDirection(actions));
    }

    private int GetDirection(ActionBuffers actions)
    {
        int value = actions.DiscreteActions[0];
        if (value == 0 || value == 1)
        {
            return value;
        }
        return -1;
    }

    private int GetDirectionIndex(float direction)
    {
        if (direction == 1)
            return 1;
        if (direction == -1)
            return 2;
        return 0;
    }

}
