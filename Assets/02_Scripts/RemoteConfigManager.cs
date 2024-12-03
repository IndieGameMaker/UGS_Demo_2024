using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class RemoteConfigManager : MonoBehaviour
{
    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Player 로그인 : {AuthenticationService.Instance.PlayerId}");
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
