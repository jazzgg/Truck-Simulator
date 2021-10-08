using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Vector3 _offset;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _player.position + _offset, _speed * Time.deltaTime);
        transform.LookAt(_player.position);
    }
}
