using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class RegisterScript : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;
    public TMP_Text statusText;

    public void OnRegisterClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string username = usernameInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
        {
            statusText.text = "Email, Password, and Username cannot be empty.";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            Username = username,
            RequireBothUsernameAndEmail = true,
            TitleId = PlayFabSettings.staticSettings.TitleId
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        statusText.text = "Registration successful!";
        Debug.Log("Registration successful: " + result.PlayFabId);
        // Optionally, can redirect to the login screen or main menu
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        statusText.text = "Registration failed: " + error.ErrorMessage;
        Debug.LogError("Registration failed: " + error.GenerateErrorReport());
    }
}
