using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class ButtonCameraControls : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> carParts = new List<GameObject>();
    [SerializeField]
    CameraController cameraController;


    public void ButtonClicked(int i)
    {
        StopAllCoroutines();
        if (i < 8)
        {
            cameraController.ChangeToCamera(i, carParts[i].GetComponentInChildren<CinemachineVirtualCamera>());
            cameraController.MatchTextToCamera(carParts[i].GetComponentInChildren<CinemachineVirtualCamera>());
        }
        else
        {
            StartCoroutine(cameraController.changeCamera(cameraController.timeTransition, carParts[i].GetComponentsInChildren<CinemachineVirtualCamera>()));
        }
    }
    public void GoBackToMainCamera()
    {
        cameraController.ChangeToMainCam();

    }


}
