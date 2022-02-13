using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    Ray ray;
    RaycastHit raycastHit;
    public float timeTransition;
    public TMPro.TMP_Text text;
    [SerializeField]
    GameObject MainCam;
    CinemachineVirtualCamera[] camerasOfCurrentObj;
    public static CinemachineVirtualCameraBase lastCamera;
    [SerializeField]
    GameObject ButtonPanel;
    [SerializeField]
    GameObject MainCamPanel;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            if (Physics.Raycast(ray, out raycastHit, 100))
            {
                if (lastCamera != null)
                {
                    lastCamera.Priority = 10;
                }
                camerasOfCurrentObj = null;
                camerasOfCurrentObj = raycastHit.transform.gameObject.GetComponentsInChildren<CinemachineVirtualCamera>();
                ButtonPanel.SetActive(false);
                MainCamPanel.SetActive(true);
                if (camerasOfCurrentObj.Length > 1)
                {
                    StartCoroutine(changeCamera(timeTransition));

                }
                else
                {
                    lastCamera = raycastHit.transform.gameObject.GetComponentInChildren<CinemachineVirtualCameraBase>();
                    raycastHit.transform.gameObject.GetComponentInChildren<CinemachineVirtualCameraBase>().Priority = 20;
                    MatchTextToCamera(lastCamera);
                }
            }

        }
    }

    public void ChangeToCamera(int i, CinemachineVirtualCamera camera)
    {
        if (lastCamera != null)
        {
            lastCamera.Priority = 10;
        }
        lastCamera = camera;
        camera.Priority = 20;
        ButtonPanel.SetActive(false);
        MainCamPanel.SetActive(true);

    }
    public void ChangeToMainCam()
    {
        StopAllCoroutines();
        if (lastCamera != null)
        {
            lastCamera.Priority = 10;
        }
        lastCamera = MainCam.GetComponent<CinemachineFreeLook>();
        MainCam.GetComponent<CinemachineFreeLook>().Priority = 20;
        ButtonPanel.SetActive(true);
        MainCamPanel.SetActive(false);
        text.enabled = false;
    }
        public IEnumerator changeCamera(float time)
        {
            for (int i = 0; i < camerasOfCurrentObj.Length; i++)
            {
                camerasOfCurrentObj[i].Priority = 20;
                lastCamera = camerasOfCurrentObj[i];
            StartCoroutine(changeText(time, camerasOfCurrentObj[i]));
                yield return new WaitForSeconds(time);
                if(i != camerasOfCurrentObj.Length-1)
                {
                    camerasOfCurrentObj[i].Priority = 0;
                }
            }
        }

        public IEnumerator changeCamera(float time, CinemachineVirtualCamera[] camerasOfCurrentObj)
        {
            ButtonPanel.SetActive(false);
            MainCamPanel.SetActive(true);
            for (int i = 0; i < camerasOfCurrentObj.Length; i++)
            {
                camerasOfCurrentObj[i].Priority = 20;
                lastCamera = camerasOfCurrentObj[i];
                StartCoroutine(changeText(time, camerasOfCurrentObj[i]));
                yield return new WaitForSeconds(time);
                if (i != camerasOfCurrentObj.Length-1)
                {
                    camerasOfCurrentObj[i].Priority = 0;
                }
            }
        }
        IEnumerator changeText(float time, CinemachineVirtualCamera currentCamera)
        {
            yield return new WaitForSeconds(time / 2);
            MatchTextToCamera(currentCamera);

        }

        public void MatchTextToCamera(CinemachineVirtualCameraBase currentCamera)
        {
            text.enabled = true;
            text.text = currentCamera.Name;
        }
}
