using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCrowd : MonoBehaviour
{
    enum Mask
    {
        Red,
        Green,
        Blue,
        Yellow
    }


    [SerializeField]
    private int crowdSize = 50;

    [SerializeField]
    private int arenaSize = 50;

    [SerializeField]
    private float fieldOfView = 50;

    [SerializeField]
    private Guy crowdPerson;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < crowdSize; i++)
        {
            Guy guy = Instantiate(crowdPerson, new Vector3(Random.Range(-arenaSize / 2, arenaSize / 2), 10, Random.Range(-arenaSize / 2, arenaSize / 2)), Quaternion.identity);

            switch(i % 4)
            {
                case 0:
                    guy.SetColor(Color.green);
                    break;
                case 1:
                    guy.SetColor(Color.red);
                    break;
                case 2:
                    guy.SetColor(Color.blue);
                    break;
                case 3:
                    guy.SetColor(Color.yellow);
                    break;

            }
        }
        StartCoroutine(SetEnnemies());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SetEnnemies()
    {
        Collider[] guys = Physics.OverlapSphere(transform.position, fieldOfView).Where(c => Guy.GetGuy(c.gameObject) != null).ToArray();
        foreach (Collider col in guys)
        {
            Guy guy = Guy.GetGuy(col.gameObject);

            float closest = float.MaxValue;
            Guy closestGuy = null;
            foreach (Collider col2 in guys)
            {
                Guy guy2 = Guy.GetGuy(col2.gameObject);
                if(!guy.IsSameColor(guy2))
                {
                    float dist = Vector3.Distance(guy.transform.position, guy2.transform.position);
                    if (dist > 0 && dist < closest)
                    {
                        closest = dist;
                        closestGuy = guy2;
                    }
                }
            }

            guy.follow(closestGuy);

        }
        yield return new WaitForSeconds(1);
        StartCoroutine(SetEnnemies());
    }
}
