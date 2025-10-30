using UnityEngine;
using UnityEngine.InputSystem;

public class ExitGame : MonoBehaviour
{
    public void ExitApplication(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
