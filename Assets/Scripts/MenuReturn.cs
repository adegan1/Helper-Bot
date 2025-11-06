using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuReturn : MonoBehaviour
{
    public string menuScene;

    public void LoadMenu(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(menuScene);
    }
}
