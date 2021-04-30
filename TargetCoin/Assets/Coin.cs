using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float numberOfHits;
    public GameObject ball;
    public float limit = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        numberOfHits = 0f;
        RandomMove();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == ball)
        {
            numberOfHits = numberOfHits + 1f;
            RandomMove();
        }
    }

    public void RandomMove()
    {
        transform.localPosition = new Vector3(Random.Range(-limit, limit), transform.localPosition.y, Random.Range(-limit, limit));
    }
}
