using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public static bool IsLevelCompleted(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level" + levelNumber + "_Completed", 0) == 1;
    }

    public static void IncrementAttempts(int levelNumber)
    {
        int currentAttempts = PlayerPrefs.GetInt("Level" + levelNumber + "_AttemptsTotal", 0);
        PlayerPrefs.SetInt("Level" + levelNumber + "_AttemptsTotal", currentAttempts + 1);
        PlayerPrefs.Save();
    }

    public static int GetTotalAttempts(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level" + levelNumber + "_AttemptsTotal", 0);
    }



}
