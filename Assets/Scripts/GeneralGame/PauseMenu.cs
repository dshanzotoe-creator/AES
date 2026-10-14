using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu; 
    [SerializeField] InputAction pauseInput;

    bool _isPaused = false;

    private void Awake()
    {
        pauseInput.Enable();
    }


    private void Update()
    {
        Pause();
    }



    void Pause()
    {
        if (pauseInput.WasPressedThisFrame() && !_isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            _isPaused = true;
        }
        else if(pauseInput.WasPressedThisFrame() && _isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            _isPaused = false;
        }
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        _isPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
