using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int nbPointsForWin = 10;

    private Color[] teams = { Color.green, Color.red, Color.blue, Color.yellow };
    private int[] killed = new int[4]; 
    private int[] dead = new int[4];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
