using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    public TMP_Text titleTextObject;
    public string titleText;
    public string newText;

    public void TaskChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TaskEnableObject(GameObject obj)
    {
        titleTextObject.text = newText;
        obj.SetActive(true);
    }

    public void TaskDisableObject(GameObject obj)
    {
        titleTextObject.text = titleText;
        obj.SetActive(false);
    }

    public void TaskCloseApplication()
    {
        Application.Quit();
    }
}
