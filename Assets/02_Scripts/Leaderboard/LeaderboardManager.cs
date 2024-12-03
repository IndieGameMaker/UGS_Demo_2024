using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField scoreIf;
    [SerializeField] private Button scoreSaveButton;

    private const string LEADERBOARD_ID = "Ranking";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        scoreSaveButton.onClick.AddListener(async () =>
        {
            int score = int.Parse(scoreIf.text);
            await AddScore(score);
        });
    }

    // 점수 기록
    private async Task AddScore(int score)
    {
        var response = await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, score);
        var result = JsonConvert.SerializeObject(response);
        Debug.Log(result);
    }
}
