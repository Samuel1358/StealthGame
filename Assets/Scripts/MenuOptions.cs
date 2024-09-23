using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void Exit()
    {
        Application.Quit();
    }

}
