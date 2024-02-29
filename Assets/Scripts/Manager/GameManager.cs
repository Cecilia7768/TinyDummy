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
                // �̹� �����ϴ� �ν��Ͻ� �˻�
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    // ���� ������Ʈ ���� �� GameManager ������Ʈ �߰�
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    #endregion


}
