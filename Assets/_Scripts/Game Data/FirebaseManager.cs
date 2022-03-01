using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    FirebaseAuth auth;

    public Button loginButton;
    public Button createAccountButton;
    public Button playAnonymousButton;

    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    public TMP_InputField createEmail;
    public TMP_InputField createPassword;
    public TMP_InputField confirmPassword;

    public delegate void SignInHandler();
    public SignInHandler OnSignIn;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;
        });

        loginButton.onClick.AddListener(() => Login(loginEmail.text, loginPassword.text));

        createAccountButton.onClick.AddListener(() =>
            CreateAccount(createEmail.text, createPassword.text, confirmPassword.text));

        playAnonymousButton.onClick.AddListener(() => AnonymousLogin());
    }

    public void AnonymousLogin()
    {
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                OnSignIn?.Invoke();
            }
        });
    }

    private void CreateAccount(string email, string password, string passwordCheck)
    {
        Debug.Log("Starting Registration");

        if(password != passwordCheck)
        {
            Debug.LogError("Passwords not the same");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                OnSignIn?.Invoke();
            }
        });
    }

    private void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                OnSignIn?.Invoke();
            }
        });
    }

    public void PlayerDataLoaded()
    {
        SceneManager.LoadScene("MainMenu");
    }
}