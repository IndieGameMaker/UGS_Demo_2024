using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;

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
    }
}
