using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the statistics
/// </summary>
public class StatisticsDisplay : MonoBehaviour
{
    [SerializeField]
    Text easyEasyPlayer1Wins;
    [SerializeField]
    Text easyEasyPlayer2Wins;

    [SerializeField]
    Text mediumMediumPlayer1Wins;
    [SerializeField]
    Text mediumMediumPlayer2Wins;

    [SerializeField]
    Text hardHardPlayer1Wins;
    [SerializeField]
    Text hardHardPlayer2Wins;

    [SerializeField]
    Text easyMediumPlayer1Wins;
    [SerializeField]
    Text easyMediumPlayer2Wins;

    [SerializeField]
    Text easyHardPlayer1Wins;
    [SerializeField]
    Text easyHardPlayer2Wins;

    [SerializeField]
    Text mediumHardPlayer1Wins;
    [SerializeField]
    Text mediumHardPlayer2Wins;

    /// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // Display scores on the screen
        easyEasyPlayer1Wins.text = Statistics.RetrieveResult(PlayerName.Player1, Difficulty.Easy, Difficulty.Easy).ToString();
        easyMediumPlayer1Wins.text = Statistics.RetrieveResult(PlayerName.Player1, Difficulty.Easy, Difficulty.Medium).ToString();
        easyHardPlayer1Wins.text = Statistics.RetrieveResult(PlayerName.Player1, Difficulty.Easy, Difficulty.Hard).ToString();
        mediumMediumPlayer1Wins.text = Statistics.RetrieveResult(PlayerName.Player1, Difficulty.Medium, Difficulty.Medium).ToString();
        mediumHardPlayer1Wins.text = Statistics.RetrieveResult(PlayerName.Player1, Difficulty.Medium, Difficulty.Hard).ToString();
        hardHardPlayer1Wins.text = Statistics.RetrieveResult(PlayerName.Player1, Difficulty.Hard, Difficulty.Hard).ToString();

        easyEasyPlayer2Wins.text = Statistics.RetrieveResult(PlayerName.Player2, Difficulty.Easy, Difficulty.Easy).ToString();
        easyMediumPlayer2Wins.text = Statistics.RetrieveResult(PlayerName.Player2, Difficulty.Easy, Difficulty.Medium).ToString();
        easyHardPlayer2Wins.text = Statistics.RetrieveResult(PlayerName.Player2, Difficulty.Easy, Difficulty.Hard).ToString();
        mediumMediumPlayer2Wins.text = Statistics.RetrieveResult(PlayerName.Player2, Difficulty.Medium, Difficulty.Medium).ToString();
        mediumHardPlayer2Wins.text = Statistics.RetrieveResult(PlayerName.Player2, Difficulty.Medium, Difficulty.Hard).ToString();
        hardHardPlayer2Wins.text = Statistics.RetrieveResult(PlayerName.Player2, Difficulty.Hard, Difficulty.Hard).ToString();
    }
}
