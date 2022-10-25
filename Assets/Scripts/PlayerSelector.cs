using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSelector : MonoBehaviour
{
    public GameObject[] players;
    public GameObject rightArrow, leftArrow;
    public  int checkPlayer, csp;
    public static int sp;
    private int selectedPlayerNow;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPlayer == (players.Length - 1))
            rightArrow.SetActive(false);
        else
            rightArrow.SetActive(true);

        if(checkPlayer == 0)
            leftArrow.SetActive(false);
        else
            leftArrow.SetActive(true);
    }

    public void NextPlayer()
    {        
        checkPlayer++;
        csp = sp;
            for (selectedPlayerNow = 0; selectedPlayerNow < players.Length; selectedPlayerNow++)
            {
                if(players[selectedPlayerNow] != players[checkPlayer])
                {
                    players[selectedPlayerNow].SetActive(false);
                }
            }
            
            players[checkPlayer].SetActive(true);    
    }

    public void PreviousPlayer()
    {        
        checkPlayer--;
        csp = sp;
            for (int n = 0; n < players.Length; n++)
            {
                if(players[n] != players[checkPlayer])
                {
                    players[n].SetActive(false);
                }
            }
            
            players[checkPlayer].SetActive(true);    
    }

    
    public void Ok()
    {
        sp = checkPlayer;
        SceneManager.LoadScene("Game");
    }
    public void OnCancel()
    {
        SceneManager.LoadScene("Game");
    }
}
