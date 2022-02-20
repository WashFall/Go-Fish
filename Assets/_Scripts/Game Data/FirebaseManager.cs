using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

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

    private void AnonymousLogin()
    {
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
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
                Debug.LogFormat("User Registerd: {0} ({1})",
                  newUser.Email, newUser.UserId);
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
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                  newUser.Email, newUser.UserId);
            }
        });
    }

    private void Logout()
    {
        auth.SignOut();
        Debug.Log("User signed out");
    }
}