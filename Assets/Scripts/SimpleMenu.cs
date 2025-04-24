using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor; // Needed to stop the editor play mode
#endif


public class SimpleMenu : MonoBehaviour
{

    public GameObject menuPanel;

    public MonoBehaviour playerInputScript;

    private void Start()
    {
       
        menuPanel.SetActive(true);

        
        Time.timeScale = 0f;

        if (playerInputScript != null)
            playerInputScript.enabled = false;
    }

    private void Update()
    {

        if (menuPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
           
            menuPanel.SetActive(false);
            Time.timeScale = 1f;
            if (playerInputScript != null)
                playerInputScript.enabled = true;
        }

      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false; // Stops the editor
#else
            Application.Quit(); 
#endif
        }
    }
}
