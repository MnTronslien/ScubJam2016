using UnityEngine;
using System.Collections;

public enum BigDisplayCam
{
    OverviewCam,
    FollowCam
}

public class BigDisplayController : MonoBehaviour {

	public BigDisplayCam CurrentCam;
    public GameObject[] Cameras;

    void Start()
    {
        SwapCam(BigDisplayCam.OverviewCam);
    }

    public void SwapCam(BigDisplayCam cameraType)
    {
        ActivateCam(false);
        CurrentCam = cameraType;
        ActivateCam(true);
    }

    private void ActivateCam(bool activate)
    {
        Cameras[(int) CurrentCam].GetComponent<Camera>().enabled = activate;
    }
}
