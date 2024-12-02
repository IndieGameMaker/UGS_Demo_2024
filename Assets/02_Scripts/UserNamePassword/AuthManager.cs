using TMPro;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Auth = Unity.Services.Authentication.AuthenticationService;

namespace UserNamePassword
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField userNameIf, passwordIf;
        [SerializeField] private Button signUpButton, signInButton;

        private async void Awake()
        {
            await UnityServices.InitializeAsync();
            Auth.Instance.SignedIn += () => Debug.Log("로그인 완료");
        }

        private void OnEnable()
        {
            // 회원가입 이벤트 연결
            signUpButton.onClick.AddListener(async () =>
            {
                await SignUp(userNameIf.text, passwordIf.text);
            });
            // 로그인 버튼 이벤트 연결
            signInButton.onClick.AddListener(async () =>
            {
                await SignIn(userNameIf.text, passwordIf.text);
            });
        }

        /*
            회원 이름 : 3 ~ 20자, 대소문자 구별없슴, [. - @]
            비밀 번호 : 8 ~ 30자, 대소문자 구별함, 영문자 대문자1, 소문자1, 숫자1, 특수문자1
        */
        // 로그인
        async Task SignIn(string userName, string password)
        {
            try
            {
                await Auth.Instance.SignInWithUsernamePasswordAsync(userName, password);
            }
            catch (AuthenticationException e)
            {
                Debug.Log(e.Message);
            }
            catch (RequestFailedException e)
            {
                Debug.Log(e.Message);
            }
        }

        // 회원가입
        async Task SignUp(string userName, string password)
        {
            try
            {
                await Auth.Instance.SignUpWithUsernamePasswordAsync(userName, password);
                Debug.Log("회원가입 성공");
            }
            catch (AuthenticationException e)
            {
                Debug.Log(e.Message);
            }
            catch (RequestFailedException e)
            {
                Debug.Log(e.Message);
            }
        }
    }


}
