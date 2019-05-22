using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    

    //Cahed
    List<Transform> wayPoints;
    int wayPointsIndex = 0;
    float movementThisFrame;

    float t = 0f;

    private void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    void Move()
    {
        if (wayPointsIndex <= wayPoints.Count - 1)
        {
            Vector3 targetPos = wayPoints[wayPointsIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
            if (transform.position == targetPos)
            {
                wayPointsIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
