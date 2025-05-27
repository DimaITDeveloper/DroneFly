using UnityEngine;
using UnityEngine.UI;

public class DroneSkinSwitcher : MonoBehaviour
{
    public GameObject drone1;
    public GameObject drone2;

    public Button buyDrone2Button;
    public Button selectDrone1Button;
    public Button selectDrone2Button;

    private bool isDrone2Bought;
    private int selectedDrone;

    private const int drone2Price = 10;

    void Start()
    {
        isDrone2Bought = PlayerPrefs.GetInt("Drone2Bought", 0) == 1;
        selectedDrone = PlayerPrefs.GetInt("SelectedDrone", 1);

        buyDrone2Button.gameObject.SetActive(!isDrone2Bought);
        selectDrone1Button.gameObject.SetActive(true);
        selectDrone2Button.gameObject.SetActive(isDrone2Bought);

        UpdateActiveDrone();

        buyDrone2Button.onClick.RemoveAllListeners();
        buyDrone2Button.onClick.AddListener(BuyDrone2);

        selectDrone1Button.onClick.RemoveAllListeners();
        selectDrone1Button.onClick.AddListener(SelectDrone1);

        selectDrone2Button.onClick.RemoveAllListeners();
        selectDrone2Button.onClick.AddListener(SelectDrone2);
    }

    private void UpdateActiveDrone()
    {
        if (selectedDrone == 1)
        {
            drone1.SetActive(true);
            drone2.SetActive(false);
        }
        else if (selectedDrone == 2 && isDrone2Bought)
        {
            drone1.SetActive(false);
            drone2.SetActive(true);
        }
        else
        {
            drone1.SetActive(true);
            drone2.SetActive(false);
            selectedDrone = 1;
            PlayerPrefs.SetInt("SelectedDrone", selectedDrone);
        }
    }

    public void BuyDrone2()
    {
        if (CoinManager.instance == null)
        {
            Debug.LogError("CoinManager не найден!");
            return;
        }

        if (CoinManager.instance.totalCoins >= drone2Price && !isDrone2Bought)
        {
            CoinManager.instance.SpendCoins(drone2Price);
            isDrone2Bought = true;
            PlayerPrefs.SetInt("Drone2Bought", 1);

            buyDrone2Button.gameObject.SetActive(false);
            selectDrone2Button.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Недостаточно монет или дрон уже куплен");
        }
    }

    public void SelectDrone1()
    {
        selectedDrone = 1;
        PlayerPrefs.SetInt("SelectedDrone", selectedDrone);
        UpdateActiveDrone();
    }

    public void SelectDrone2()
    {
        if (!isDrone2Bought)
        {
            Debug.Log("Дрон 2 не куплен!");
            return;
        }

        selectedDrone = 2;
        PlayerPrefs.SetInt("SelectedDrone", selectedDrone);
        UpdateActiveDrone();
    }
}
