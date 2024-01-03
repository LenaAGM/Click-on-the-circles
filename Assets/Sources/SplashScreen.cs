using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SplashScreen : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
}