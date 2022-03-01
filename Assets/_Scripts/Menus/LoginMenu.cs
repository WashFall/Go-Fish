using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour
{
    public void AnonymousPlay()
    {
        FindObjectOfType<FirebaseManager>().AnonymousLogin();

        SceneManager.LoadScene("MainMenu");
    }
}
