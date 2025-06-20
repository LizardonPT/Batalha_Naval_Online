using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;

    public void OnLoginClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Email and Password cannot be empty.";
            return;
        }

        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password,
            TitleId = PlayFabSettings.staticSettings.TitleId
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        statusText.text = "Login successful!";
        SceneManager.LoadScene("LobbyScene");
        // Proceed to the next scene or functionality
        Debug.Log("Login successful: " + result.PlayFabId);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        statusText.text = "Login failed: " + error.ErrorMessage;
        Debug.LogError("Login failed: " + error.GenerateErrorReport());
    }
}
