using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int nbPointsForWin = 10;

    private Color[] teams = { Color.green, Color.red, Color.blue, Color.cyan };
    private int[] killed = new int[4]; 
    private int[] dead = new int[4];
    private List<Func<int, bool>> playerGameOver = new List<Func<int, bool>>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TimeManager(UnityEngine.Random.Range(30,60)));
        //StartCoroutine(TimeManager(3));
    }

    IEnumerator TimeManager(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;

        int maxValue = killed.Max();
        int maxIndex = killed.ToList().IndexOf(maxValue);

        Debug.Log(killed.ToString());

        foreach(var fun in playerGameOver)
        {
            fun(maxIndex);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerKilled(Guy killer, Guy killed)
    {
        for (int i = 0; i < 4; i++)
        {
            if (killer.GetColor() == teams[i])
            {
                this.killed[i]++;
            }
            if (killed.GetColor() == teams[i])
            {
                this.dead[i]++;
            }
        }
    }

    public void PlayerKilled(PlayerController killer, Guy killed)
    {
        for (int i = 0; i < 4; i++)
        {
            if (killer.GetColor() == teams[i])
            {
                this.killed[i]++;
            }
            if (killed.GetColor() == teams[i])
            {
                this.dead[i]++;
            }
        }
    }

    public int GetScoreForTeeam(int team)
    {
        return killed[team % killed.Length];
    }


    public void RegisterGameOverFunction(Func<int, bool> gameOverFunc)
    {
        playerGameOver.Add(gameOverFunc);
    }

}
