using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject startButton;
    [SerializeField] CastleManager castleManager;

    private void Awake()
    {
        instance = this;
    }
    
    public void gameStart()
    {
        castleManager.activateCastle();
    }

}
