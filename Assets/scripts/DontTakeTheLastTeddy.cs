using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Game manager
/// </summary>
public class DontTakeTheLastTeddy : MonoBehaviour
{
    Board board;
    Player player1;
    Player player2;
    Difficulty player1Diff;
    Difficulty player2Diff;

    // events invoked by class
    TakeTurn takeTurnEvent = new TakeTurn();
    GameOver gameOverEvent = new GameOver();
    UnityEvent gameStartEvent = new UnityEvent();

    // Number of games played
    int gamesPlayed = 0;

    // Pause between games in seconds
    Timer pause;

    // First player making the move
    PlayerName firstPlayer = PlayerName.Player1;

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        // Initialize statistics
        Statistics.Initialize();

        // retrieve board and player references
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();

        // register as invoker and listener
        EventManager.AddTakeTurnInvoker(this);
        EventManager.AddGameOverInvoker(this);
        EventManager.AddTurnOverListener(HandleTurnOverEvent);
        EventManager.AddGameStartInvoker(this);

        // Add pause timer and event listener
        pause = gameObject.AddComponent<Timer>();
        pause.AddTimerFinishedListener(HandlePauseFinishedEvent);
    }

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // Start the game
        if (gamesPlayed < 600)
        {
            StartGame(firstPlayer);
        }
        else
        {
            SceneManager.LoadScene("statistics");
        }

        // Initialize timer
        pause.Duration = 0.01f;
    }

    /// <summary>
    /// Adds the given listener for the TakeTurn event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddTakeTurnListener(UnityAction<PlayerName, Configuration> listener)
    {
        takeTurnEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the GameOver event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddGameOverListener(UnityAction<PlayerName, Difficulty, Difficulty> listener)
    {
        gameOverEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the GameStart event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddGameStartListener(UnityAction listener)
    {
        gameStartEvent.AddListener(listener);
    }

    /// <summary>
    /// Starts a game with the given player taking the
    /// first turn
    /// </summary>
    /// <param name="firstPlayer">player taking first turn</param>
    void StartGame(PlayerName firstPlayer)
    {
        // Change difficulty if 100 games have been played
        if (gamesPlayed % 100 == 0)
        {
            SwitchDifficulty(gamesPlayed / 100);
        }
        gamesPlayed++;

        // Invoke game start event
        gameStartEvent.Invoke();

        // set player difficulties
        player1.Difficulty = player1Diff;
        player2.Difficulty = player2Diff;

        // create new board
        board.CreateNewBoard();
        takeTurnEvent.Invoke(firstPlayer,
            board.Configuration);
    }

    /// <summary>
    /// Handles the TurnOver event by having the 
    /// other player take their turn
    /// </summary>
    /// <param name="player">who finished their turn</param>
    /// <param name="newConfiguration">the new board configuration</param>
    void HandleTurnOverEvent(PlayerName player, 
        Configuration newConfiguration)
    {
        board.Configuration = newConfiguration;

        // check for game over
        if (newConfiguration.Empty)
        {
            // fire event with winner
            if (player == PlayerName.Player1)
            {
                gameOverEvent.Invoke(PlayerName.Player2, player1Diff, player2Diff);
            }
            else
            {
                gameOverEvent.Invoke(PlayerName.Player1, player1Diff, player2Diff);
            }

            // Pause and start a new game
            if (gamesPlayed < 600)
            {
                pause.Run();

                // Alternate between players
                if (firstPlayer == PlayerName.Player1)
                {
                    firstPlayer = PlayerName.Player2;
                }
                else
                {
                    firstPlayer = PlayerName.Player1;
                }
            }
            else
            {
                SceneManager.LoadScene("statistics");
            }
        }
        else
        {
            // game not over, so give other player a turn
            if (player == PlayerName.Player1)
            {
                takeTurnEvent.Invoke(PlayerName.Player2,
                    newConfiguration);
            }
            else
            {
                takeTurnEvent.Invoke(PlayerName.Player1,
                    newConfiguration);
            }
        }
    }

    /// <summary>
    /// Handles PauseFinished event by starting a new game
    /// </summary>
    void HandlePauseFinishedEvent()
    {
        StartGame(firstPlayer);
    }

    /// <summary>
    /// Switches difficulty if 100 games have been played
    /// </summary>
    void SwitchDifficulty(int i)
    {
        switch(i)
        {
            case 0:
                player1Diff = Difficulty.Easy;
                player2Diff = Difficulty.Easy;
                break;
            case 1:
                player2Diff = Difficulty.Medium;
                break;
            case 2:
                player2Diff = Difficulty.Hard;
                break;
            case 3:
                player1Diff = Difficulty.Medium;
                player2Diff = Difficulty.Medium;
                break;
            case 4:
                player2Diff = Difficulty.Hard;
                break;
            default:
                player1Diff = Difficulty.Hard;
                player2Diff = Difficulty.Hard;
                break;
        }
    }
}
