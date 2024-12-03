using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class RemoteConfigManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private int scale;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log($"Player 로그인 : {AuthenticationService.Instance.PlayerId}");
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        // Remote Config 이벤트 연결
        RemoteConfigService.Instance.FetchCompleted += _ =>
        {
            moveSpeed = RemoteConfigService.Instance.appConfig.GetFloat("move_speed");
            turnSpeed = RemoteConfigService.Instance.appConfig.GetFloat("turn_speed");
            scale = RemoteConfigService.Instance.appConfig.GetInt("scale");

            GameObject.Find("Mummy_Mon").transform.localScale = Vector3.one * scale;
        };

        await GetRemoteConfigData();
    }

    private struct userAtt { };
    private struct appAtt { };

    private async Task GetRemoteConfigData()
    {
        await RemoteConfigService.Instance.FetchConfigsAsync(new userAtt(), new appAtt());
    }
}
