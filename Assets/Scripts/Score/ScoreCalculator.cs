using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private TrailerTask _task;
    private RCC_TruckTrailer _trailer;
    private bool isRecording { get; set; } = false;

    private List<Vector3> _wayPoints;

    private float _startTime;
    private float _customTime;
    public void RecordTime()
    {
        isRecording = true;
        _startTime = Time.time;
    }
    public float CalculateScore()
    {
        isRecording = false;

        var timeDifferent =  Time.time - _startTime;
        var distances = new List<float>(100);
        var distanceSum = 0f;

        for (int i = 0; i < _wayPoints.Count; i++)
        {
            if (i % 2 == 0) continue;

            if (i == _wayPoints.Count - 1)
            {
                distances.Add(Vector3.Distance(_wayPoints[i], _wayPoints[_wayPoints.Count - 1]));
            }
            else distances.Add(Vector3.Distance(_wayPoints[i], _wayPoints[i + 1]));
        }

        foreach (var distance in distances)
        {
            distanceSum += distance;
        }

        print(distanceSum - (timeDifferent / 2));
        return distanceSum - (timeDifferent / 2);
    }
    private void RecordWayPoints(Vector3 trailerPos)
    {
        _wayPoints.Add(trailerPos);
    }

    private void Start()
    {
        _task = GetComponent<TrailerTask>();
        _trailer = _task.GetTrailer();

        _wayPoints = new List<Vector3>(100);
    }
    private void Update()
    {

        if (isRecording && _customTime < Time.time && _task.isDone == false)
        {
            _customTime = Time.time + 1;
            RecordWayPoints(_trailer.transform.position);
        }
        else if (isRecording && _task.isDone)
        {
            CalculateScore();
        }
    }
   
}
