using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;
    
    //public List<Transform> balls;
    public Transform respawnLocationsParent;

    public float ballShootForcePower = 1.5f;
    
    private Transform[] respawnLocations;
    private int respawnLocationIndex;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        respawnLocationIndex = 0;
        respawnLocations = new Transform[respawnLocationsParent.childCount];
        for (int i = 0; i < respawnLocations.Length; i++)
        {
            respawnLocations[i] = respawnLocationsParent.GetChild(i);
        }
    }
    
    public void RespawnBall(Transform ball, float delay)
    {
        Ball tempBall = ball.GetComponent<Ball>();
        if (tempBall.isSpawning)
        {
            return;
        }

        tempBall.isSpawning = true;
        
        respawnLocationIndex++;
        if (respawnLocationIndex >= respawnLocations.Length)
        {
            respawnLocationIndex = 0;
        }
        Vector3 pos = respawnLocations[respawnLocationIndex].position;
        
        StartCoroutine(RespawnBallCo(ball, delay, pos));
    }

    IEnumerator RespawnBallCo(Transform ball, float delay, Vector3 pos)
    {
        yield return new WaitForSeconds(delay);
        
        if (BallManager.Instance.HasTooMuchBalls())
        {
            BallManager.Instance.RemoveBall(ball);
        }
        else
        {
            ball.gameObject.SetActive(false);
        
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Ball>().ResetBall();
        
            ball.gameObject.SetActive(true);
            
            ball.transform.position = pos;

            float randomPower = Random.Range(ballShootForcePower - 2, ballShootForcePower + 2);
            
            ball.GetComponent<Rigidbody>().AddForce(-ball.position.normalized * randomPower, ForceMode.Impulse);
        }
    }
}
