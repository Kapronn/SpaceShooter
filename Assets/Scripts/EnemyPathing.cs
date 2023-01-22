using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [Header("Movement")]
    WaveConfig waveConfig;
    List<Transform> wayPoints;
    int wayPointsIndex = 0;
   

    private void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointsIndex].transform.position; //todo optimize
    }
    private void Update()
    {
        MovementOnWayPoints();
    }
    public void SetWayConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MovementOnWayPoints()
    {
        if (wayPointsIndex <= wayPoints.Count - 1)
        {
            var moveSpeed = waveConfig.GetMoveSpeed() * Time.deltaTime;
            var targetPosition = wayPoints[wayPointsIndex].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
            if (transform.position == targetPosition)
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
