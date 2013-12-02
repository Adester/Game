using UnityEngine;
using System.Collections;
using UnityEditor;

[AddComponentMenu("Tactibru/GUI/Honor System")]
public class HonorSystemBehavior : EditorWindow
{
    public int playerHonorTotal;
    public int maxPlayerHonor;
    public int computerHonorTotal;
    public int maxComputerHonor;
    public int pSquadDeathValue; //value used by designer on player squad death
    public int cSquadDeathValue; // value used by designer on computer squad death
    public bool usingGameController;
    public GUIStyle style;
    public static int offensiveHonor;
    public static int defensiveHonor;
    public static int honorPenalty;
    public static bool inCombat = false;
    int playerSquadTotal;
    int computerSquadTotal;
    GameControllerBehaviour gameController;
    HUDController controller;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Grid").GetComponent<GameControllerBehaviour>();
        playerSquadTotal = 10;
        computerSquadTotal = 10;
        StartingHonor();

        if (maxPlayerHonor == 0)
            maxPlayerHonor = 100;
        if (maxComputerHonor == 0)
            maxComputerHonor = 100;
            
    }
    
    void OnGUI()
    {
        BaseHonor();
        SquadHonor();
    }

    public void BaseHonor()
    {
        if (GridBehavior.inCombat == false)
        {
            if (usingGameController)
            {
                //USING HUD CONTROLLER 
            }
            else
            {
                GUI.Box(new Rect(Screen.width * 0.75f, Screen.height * 0.05f, 190, 100), ("Player Honor Total: " + playerHonorTotal + "\n" + "Computer Honor Total: " + computerHonorTotal), style);
                EditorGUI.ProgressBar(new Rect(Screen.width * 0.625f, Screen.height * 0.06f, 150, 40), (playerHonorTotal / 100.0f) / (maxPlayerHonor / 100.0f), "Player Honor" + "\n \n" + playerHonorTotal);
                EditorGUI.ProgressBar(new Rect(Screen.width * 0.625f, Screen.height * 0.12f, 150, 40), (computerHonorTotal / 100.0f) / (maxComputerHonor / 100.0f), "Enemy Honor" + "\n \n" + computerHonorTotal);

                //Third option using drawtexture with
                //foreground and background images

            }
        }

        if (inCombat == true)
        {
            OverFiveCatch();
                        
            if (gameController.currentTurn == GameControllerBehaviour.UnitSide.player)
            {
                //player is offensive and recieves offensive honor
                //computer is defensive and receives defensive honor
                playerHonorTotal = playerHonorTotal + offensiveHonor - (defensiveHonor + honorPenalty);
                computerHonorTotal = computerHonorTotal + defensiveHonor - offensiveHonor;
                inCombat = false;
                offensiveHonor = 0;
                defensiveHonor = 0;
                honorPenalty = 0;
            }
            else
            {
                //player is defensive receives defensive honor and computer gets offensive honor
                computerHonorTotal = computerHonorTotal + offensiveHonor - defensiveHonor;
                playerHonorTotal = playerHonorTotal + defensiveHonor - (offensiveHonor + honorPenalty);
                inCombat = false;
                offensiveHonor = 0;
                defensiveHonor = 0;
                honorPenalty = 0;
            }
        }
    }

    void OverFiveCatch()
    {
        if (offensiveHonor > 5)
            offensiveHonor = offensiveHonor - 1;
        if (defensiveHonor > 5)
            defensiveHonor = defensiveHonor - 1;
    }

    void SquadHonor()
    {
        if (playerSquadTotal !=gameController.playerTeamTotal)
        {
            playerHonorTotal = playerHonorTotal - pSquadDeathValue;
            playerSquadTotal = gameController.playerTeamTotal;
        }

        if (computerSquadTotal !=gameController.enemyTeamTotal)
        {
            playerHonorTotal = playerHonorTotal + cSquadDeathValue;
            computerSquadTotal = gameController.enemyTeamTotal;
        }
    }

    void StartingHonor()
    {
        //Find the players units on the board
        //Added up all the honor from units and add to total at runtime

    }
}
