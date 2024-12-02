using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Authentication;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private Button signInButton;
    [SerializeField] private TMP_Text messageText;

    private async void Awake()
    {
        // UGS 초기화 콜백
        UnityServices.Initialized += () =>
        {
            messageText.text += "UnityServices Init! \n";
        };

        // UGS 초기화
        await UnityServices.InitializeAsync();
        // 이벤트 바인딩
        BindingEvents();

        // 버튼 이벤트 연결
        signInButton.onClick.AddListener(async () => await SignInAsync());
    }

    // 인증관련 이벤트 연결
    private void BindingEvents()
    {
        // 로그인 성공
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("로그인 성공");
            messageText.text += $"Player Id: {AuthenticationService.Instance.PlayerId}\n";
        };
    }


    // 익명 로그인 메소드
    private async Task SignInAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (AuthenticationException e)
        {
            Debug.Log(e.Message);
        }
    }
}
