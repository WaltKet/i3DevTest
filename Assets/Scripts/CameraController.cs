using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cinemachineVirtualCameras;
    public TMPro.TMP_Text text;
    // Start is called before the first frame update
    int counter = 0;
    public int transitionTime;
    void Start()
    {
        StartCoroutine(changeCamera(transitionTime));
    }
    IEnumerator changeCamera(int time)
    {
        yield return new WaitForSeconds(time);
        foreach(var camera in cinemachineVirtualCameras)
        {
            camera.Priority = 20;
            StartCoroutine(changeText(time, camera));
            yield return new WaitForSeconds(time);
            text.enabled = false;
            camera.Priority = 0;

        }
        StartCoroutine(changeCamera(time));
    }
    IEnumerator changeText(int time, CinemachineVirtualCamera currentCamera)
    {
        yield return new WaitForSeconds(time / 2);
        text.enabled = true;
        text.text = currentCamera.Name;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
