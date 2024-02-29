using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
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
                // 이미 존재하는 인스턴스 검색
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    // 게임 오브젝트 생성 후 GameManager 컴포넌트 추가
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    #endregion


}
