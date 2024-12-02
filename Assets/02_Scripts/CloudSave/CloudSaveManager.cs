using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class CloudSaveManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button singleDataSaveButton;

    async void Awake()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            string playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log($"로그인 성공 : {playerId}");
        };

        // 익명로그인 요청
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
