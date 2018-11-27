using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMode {
    public static int numHumans = 0;
    public static int numZombies = 0;

    public static void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
