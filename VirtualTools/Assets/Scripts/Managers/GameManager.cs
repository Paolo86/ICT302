﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public string MockStudentNumber; //TODO: remove this

    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject coreGameObject = GameObject.Find("Managers");
                m_instance = coreGameObject.AddComponent<GameManager>();
            }

            return m_instance;
        }
    }

    protected virtual void Awake()
    {
        if (m_instance == null)
            m_instance = GetComponent<GameManager>();
        else
            DestroyImmediate(this);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
  
    // Start is called before the first frame update
    // GameManager is set to be compiled after Player.cs, so when setting the game mode, the player reference is valid (see Project Setting -> Script execution order)
    void Start()
    {
        // Initialize other managers here
        GUIManager.Instance.Init();
        Player.Instance.Init();
        GUIManager.Instance.ConfigureCursor(true, CursorLockMode.Confined);
        Player.Instance.FreezePlayer(true);


        //GUIManager.Instance.GetMainCanvas().DogInstructionSequence(new string[] { "Left click to start!" }, () =>
        //{

        //    SessionManager.Instance.CreateSession(true, true);

        //});

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GUIManager.Instance.GetMainCanvas().ToggleEscapeMenu();
    }

    private void OnApplicationQuit()
    {
        SessionManager.Instance.OnQuit();
    }
}
