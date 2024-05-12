using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void OpenScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
