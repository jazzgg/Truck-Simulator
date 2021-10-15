using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigatorArrow : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private Transform _player;

    private Vector3 _target;
    public void SetTarget(Vector3 target)
    {
        transform.position = target + _offset;
        _target = target;
    }
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_target == null) return;

        transform.position = _player.position + _offset;
        transform.LookAt(_target);
    }
 
}
