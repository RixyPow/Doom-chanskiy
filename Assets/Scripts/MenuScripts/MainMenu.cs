using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Camera _camera;
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
            _camera = GetComponent<Camera>();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    public void ExitGame(){
        Application.Quit();
    }
}
