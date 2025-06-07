using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;


public class UnityServicesManager : MonoBehaviour
{
    async void Awake()
    {
        await InitializeServicesAsync();
    }

    private async Task InitializeServicesAsync()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in anonymously with ID: " + AuthenticationService.Instance.PlayerId);
        }
    }
}
