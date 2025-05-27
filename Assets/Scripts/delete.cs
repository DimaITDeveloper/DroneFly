using UnityEngine;

public class ResetDrone3Purchase : MonoBehaviour
{
    public void ResetPurchase()
    {
        PlayerPrefs.DeleteKey("Drone3Bought");
        PlayerPrefs.Save();
        Debug.Log("Покупка дрона 2 сброшена.");
    }
}
