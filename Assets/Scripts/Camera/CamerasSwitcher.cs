using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasSwitcher : MonoBehaviour
{
    [Header("RCC_CAM")]
    [SerializeField]
    private GameObject _defaultCam;
    [SerializeField]
    private GameObject _customCam;

    public void ActivateCustomCam()
    {
        _customCam.SetActive(true);
        _defaultCam.SetActive(false);
    }
    public void ReturnToDefaultCam()
    {
        _defaultCam.SetActive(true);
        _customCam.SetActive(false);
    }
}
