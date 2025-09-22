using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    public GUISkin layout;
    GameObject thePuck;

    void Start()
    {
        thePuck = GameObject.FindGameObjectWithTag("Disco");
    }

    public static void Score(string wallID)
    {
        if (wallID == "Gol_Cima")
        {
            PlayerScore1++;
        }
        else
        {
            PlayerScore2++;
        }
    }

    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerScore2);
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "Reset"))
        {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            thePuck.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }
        if (PlayerScore1 == 5)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Player1 Ganhou");
            thePuck.SendMessage("DiscoReset", null, SendMessageOptions.RequireReceiver);
        }
        else if (PlayerScore2 == 5)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PlayerIA Ganhou");
            thePuck.SendMessage("DiscoReset", null, SendMessageOptions.RequireReceiver);
        }
    }
}
