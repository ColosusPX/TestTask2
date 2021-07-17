using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
