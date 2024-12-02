using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using System.Collections;

/*
사용자 이름

- 50 자
- 중간에 공백 X
- "Zack"  => "Zack#1234"
*/


public class AuthManager : MonoBehaviour
{
    [SerializeField] private Button signInButton;
    [SerializeField] private Button signOutButton;
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
        signOutButton.onClick.AddListener(() =>
        {
            // 로그아웃 요청
            AuthenticationService.Instance.SignOut();
        });

        await Test();
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

        // 로그아웃
        AuthenticationService.Instance.SignedOut += () =>
        {
            Debug.Log("로그 아웃");
            messageText.text = "";
        };

        // 로그인 실패
        AuthenticationService.Instance.SignInFailed += (ex) =>
        {
            Debug.Log(ex.Message);
            messageText.text += $"Login Failed : {ex.Message}\n";
        };
        // 세션 종료
        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("세션 종료됨!");
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


    // private async Task Test()
    // {
    //     await Task.Delay(10000);
    //     Debug.Log("10초 지남");
    // }

    // private IEnumerator Test2()
    // {
    //     yield return new WaitForSeconds(10.0f);
    //     Debug.Log("10초 지남");
    // }
}
