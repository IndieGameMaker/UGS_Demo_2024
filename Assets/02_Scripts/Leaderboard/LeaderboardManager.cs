using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
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

        AuthenticationService.Instance.SignedIn += () =>
        {
            var playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log($"로그인 : {playerId}");
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        scoreSaveButton.onClick.AddListener(async () =>
        {
            int score = int.Parse(scoreIf.text);
            await AddScore(score);
            await GetAllScores();

            // 유저 삭제
            await AuthenticationService.Instance.DeleteAccountAsync();
            // 로그인
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        });
    }

    // 점수 기록
    private async Task AddScore(int score)
    {
        var response = await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, score);
        var result = JsonConvert.SerializeObject(response);
        Debug.Log(result);
    }

    // 모든 점수 불러오기
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    public async Task GetAllScores()
    {
        var response = await LeaderboardsService.Instance.GetScoresAsync(LEADERBOARD_ID);
        Debug.Log($"Json : {JsonConvert.SerializeObject(response)}");

        entries = response.Results;

        string rank = "";
        foreach (var entry in entries)
        {
            rank += $"[{entry.Rank + 1}] {entry.PlayerName} / {entry.Score}\n";
        }
        Debug.Log(rank);
    }
}
