using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        Instance = this;
    }
}
