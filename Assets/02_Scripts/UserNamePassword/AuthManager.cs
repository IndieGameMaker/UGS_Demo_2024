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
        }

        private void OnEnable()
        {
            // 회원가입 이벤트 연결
            signUpButton.onClick.AddListener(async () =>
            {
                await SignUp(userNameIf.text, passwordIf.text);
            });
        }

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
