using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool isRunning = false;
    

    public static void OnStartGame()
    {
        if (isRunning) return;

        UIManager.I.OnGameStarted();
        TouchHandler.I.OnGameStarted();
        PlayerController.I.OnGameStarted();
        CameraController.I.OnGameStarted();
        isRunning = true;
    }
    
}
