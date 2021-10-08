using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveInpX;
    private float moveInpZ;
    private void Update()
    {
        moveInpX = Input.GetAxis("Horizontal");
        moveInpZ = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        Move(moveInpX, moveInpZ);
    }
    private void Move(float moveX, float MoveY)
    {
        transform.Translate(moveInpX * 10 *Time.deltaTime, 0, moveInpZ * 10 * Time.deltaTime);
    }
}

