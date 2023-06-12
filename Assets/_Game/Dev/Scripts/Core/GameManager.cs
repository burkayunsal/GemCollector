using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool canStart = false, isRunning = false;
    

    public static void OnStartGame()
    {
        if (isRunning || !canStart) return;

        canStart = false;

        UIManager.I.OnGameStarted();
        TouchHandler.I.OnGameStarted();
        PlayerController.I.OnGameStarted();
        CameraController.I.OnGameStarted();
        isRunning = true;
    }
    

    public static void OnLevelCompleted()
    {
        isRunning = false;
        canStart = false;
        UIManager.I.OnSuccess();
        PlayerController.I.ForceStop();
    }

    public static void OnLevelFailed()
    {
        isRunning = false;
        canStart = false;
        UIManager.I.OnFail();
        PlayerController.I.ForceStop();
    }

    public static void ReloadScene(bool isSuccess)
    {
        //TODO SEND ANALYTICS EVENT

        if (isSuccess)
        {
            SaveLoadManager.IncreaseLevel();
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
