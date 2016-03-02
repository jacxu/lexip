using UnityEngine;
using System.Collections;

public static class MyController  {

    public static GameController GetGameController()
    {
        GameController gameController = null;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gameController == null)
        {
            Debug.Log("Cant find gameController script");
        }

        return gameController;
    }

    public static PlayerController GetPlayerController()
    {
        PlayerController gameController = null;

        GameObject gameControllerObject = GameObject.FindWithTag("Player");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<PlayerController>();
        }

        if (gameController == null)
        {
            Debug.Log("Cant find playerController script");
        }

        return gameController;
    }




}
