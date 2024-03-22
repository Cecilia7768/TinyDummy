using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public static event Action firstGameStartSetInit;

    private void Start()
    {
        if(GameSystem.isFirstGameStart)
        {
            firstGameStartSetInit?.Invoke();
            GameSystem.isFirstGameStart = false;
        }
    }

    #region 게임 속도
    public void OnClickDefault() => SetGameSpeed(1);
    public void OnClickX2() => SetGameSpeed(2);
    public void OnClickX4() => SetGameSpeed(4);
    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }
    #endregion
}
