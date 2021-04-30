using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Runtime.CompilerServices;

public class PlatformAgent : Agent
{

    public GameObject ball;
    Vector3 ballInitPos;

    public GameObject coin;
    public float factorCoin = 10f;

    public float maxAngle = 30f;

    public int counter = 0;
    public int maxIter = 1500;

    // Start is called before the first frame update
    void Start()
    {
        ballInitPos = ball.transform.position;
    }

    public override void OnEpisodeBegin()
    {
        ball.transform.position = ballInitPos;
        ball.GetComponent<Rigidbody>().velocity = new Vector3();
        ball.GetComponent<Rigidbody>().angularVelocity = new Vector3();

        counter = 0;


    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.rotation.x);
        sensor.AddObservation(transform.rotation.z);

        sensor.AddObservation(transform.position.x - ball.transform.position.x);
        sensor.AddObservation(transform.position.y - ball.transform.position.y);
        sensor.AddObservation(transform.position.z - ball.transform.position.z);

        sensor.AddObservation(ball.GetComponent<Rigidbody>().velocity.x);
        sensor.AddObservation(ball.GetComponent<Rigidbody>().velocity.y);
        sensor.AddObservation(ball.GetComponent<Rigidbody>().velocity.z);

        sensor.AddObservation(transform.position.x - coin.transform.position.x);
        sensor.AddObservation(transform.position.y - coin.transform.position.y);
        sensor.AddObservation(transform.position.z - coin.transform.position.z);
        
    }

    public float ClipAngle(float angle, float min, float max)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + min);
        return Mathf.Min(angle, max);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // action[0] = rotation x, action[1] = rotation z 
        

        counter = counter + 1;

        Vector3 rotation = transform.rotation.eulerAngles + new Vector3(actions.ContinuousActions[0], 0f, actions.ContinuousActions[1]);
        rotation.y = 0f;
        rotation.x = ClipAngle(rotation.x, -maxAngle, maxAngle);
        rotation.z = ClipAngle(rotation.z, -maxAngle, maxAngle);

        transform.eulerAngles = rotation;

        if (Vector3.Distance(transform.position,ball.transform.position) > 10)
        {
            SetReward(coin.GetComponent<Coin>().numberOfHits * factorCoin);
            coin.GetComponent<Coin>().numberOfHits = 0f;
            SetReward(-1f);
            EndEpisode();
        }
        else if (counter > maxIter)
        {
            SetReward(coin.GetComponent<Coin>().numberOfHits * factorCoin);
            coin.GetComponent<Coin>().numberOfHits = 0f;
            EndEpisode();
        }
        else
        {
            SetReward(0.1f);
        }
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Vertical");
        continuousActions[1] = -Input.GetAxis("Horizontal");
    }

}
