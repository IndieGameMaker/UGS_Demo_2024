using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication.PlayerAccounts;
using System;
using Unity.Services.Authentication;

namespace UnityAccount
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField] private Button signInButton;
        [SerializeField] private Button signOutButton;
        [SerializeField] private Button playerNameSaveButton;

        [SerializeField] private TMP_InputField playerNameIf;
        [SerializeField] private TMP_Text messageText;

        async void Awake()
        {
            await UnityServices.InitializeAsync();

            PlayerAccountService.Instance.SignedIn += OnSignedIn;
            PlayerAccountService.Instance.SignedOut += () => Debug.Log("로그아웃");
        }

        private async void OnSignedIn()
        {
            // 엑세스 토큰 할당(추출)
            string accessToken = PlayerAccountService.Instance.AccessToken;
            // 비동기 방식으로 로그인처리
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("로그인 성공");
        }

        void OnEnable()
        {
            // 로그인
            signInButton.onClick.AddListener(async () =>
            {
                await PlayerAccountService.Instance.StartSignInAsync();
            });

            // 로그아웃
            signOutButton.onClick.AddListener(() =>
            {
                PlayerAccountService.Instance.SignOut();
            });
        }


    }
}
