using UnityEngine;

public class DroneSceneManager : MonoBehaviour
{
    public GameObject drone1;
    public GameObject drone2;
    public CameraAutoScroll cameraScroll;

    void Start()
    {
        int selectedDrone = PlayerPrefs.GetInt("SelectedDrone", 1);

        if (selectedDrone == 1)
        {
            drone1.SetActive(true);
            drone2.SetActive(false);
            cameraScroll.target = drone1.transform;
        }
        else
        {
            drone1.SetActive(false);
            drone2.SetActive(true);
            cameraScroll.target = drone2.transform;
        }
    }
}
