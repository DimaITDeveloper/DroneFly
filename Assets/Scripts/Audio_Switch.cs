using UnityEngine;
using UnityEngine.UI;

public class SoundButtonsController : MonoBehaviour
{
    public AudioSource musicSource;

    public GameObject buttonOn;  // ������ "���� �������"
    public GameObject buttonOff; // ������ "���� ��������"

    private void Start()
    {
        // ���������� �������� ��������� ����� � ������� ������ ������
        UpdateButtons();
    }

    public void TurnSoundOn()
    {
        musicSource.mute = false;
        UpdateButtons();
    }

    public void TurnSoundOff()
    {
        musicSource.mute = true;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        bool isMuted = musicSource.mute;
        buttonOn.SetActive(!isMuted);
        buttonOff.SetActive(isMuted);
    }
}
