using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private float _startTime;

    public void RecordTime()
    {
        _startTime = Time.time;
    }
    public float CalculateScore(float distance)
    {
        var timeDifferent =  Time.time - _startTime;
        print(distance - timeDifferent);
        return Mathf.Clamp(distance - timeDifferent, 0, 100);
    }
}
