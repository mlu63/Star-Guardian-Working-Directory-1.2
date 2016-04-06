using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public bool isEasy;
    public bool musicOn;
    public bool gameOver;

    // default values on launch
    void Start()
    {
        isEasy = true;
        musicOn = true;
        gameOver = false;
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}