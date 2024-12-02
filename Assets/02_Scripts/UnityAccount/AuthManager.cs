using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
