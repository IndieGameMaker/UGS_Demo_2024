using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication.PlayerAccounts;

namespace UnityAccount
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField] private Button signInButton;
        [SerializeField] private Button signOutButton;
        [SerializeField] private Button playerNameSaveButton;

        [SerializeField] private TMP_InputField playerNameIf;
        [SerializeField] private TMP_Text messageText;
    }
}
