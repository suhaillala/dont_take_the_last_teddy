using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics
{
    // 2D array for total wins by both players
    static int[,] totalWins = new int[2, 6];

    /// <summary>
    /// Add listener for GameOver event
    /// </summary>
    public static void Initialize()
    {
        EventManager.AddGameOverListener(StoreStatistics);
    }

    /// <summary>
    /// Store statistics for different players
    /// </summary>
    /// <param name="name">Player name</param>
    /// <param name="player1Diff">Player 1 difficulty</param>
    /// <param name="player2Diff">Player 2 difficulty</param>
    static void StoreStatistics(PlayerName name, Difficulty player1Diff, Difficulty player2Diff)
    {
        if (player1Diff == Difficulty.Easy && player2Diff == Difficulty.Hard)
        {
            totalWins[(int)name, 5]++;
        }
        else
        {
            totalWins[(int)name, (int)player1Diff + (int)player2Diff]++;
        }
    }

    /// <summary>
    /// Retrieve results for a given player and difficulty combination
    /// </summary>
    /// <param name="name">Player name</param>
    /// <param name="player1Diff">Player 1 difficulty</param>
    /// <param name="player2Diff">Player 2 difficulty</param>
    /// <returns></returns>
    public static int RetrieveResult(PlayerName name, Difficulty player1Diff, Difficulty player2Diff)
    {
        if (player1Diff == Difficulty.Easy && player2Diff == Difficulty.Hard)
        {
            return totalWins[(int)name, 5];
        }
        else
        {
            return totalWins[(int)name, (int)player1Diff + (int)player2Diff];
        }
    }
}
