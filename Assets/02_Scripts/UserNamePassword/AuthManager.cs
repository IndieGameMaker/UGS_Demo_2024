using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserNamePassword
{
    public class AuthManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField userNameIf, passwordIf;
        [SerializeField] private Button signUpButton, signInButton;
    }
}
