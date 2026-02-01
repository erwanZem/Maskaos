using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class Guy : MonoBehaviour
{
    [SerializeField]
    private Color color;

    [SerializeField]
    private float speed = 100;

    [SerializeField]
    private float attackSpeed = 2;

    [SerializeField]
    private AudioClip claqueClip;

    [SerializeField]
    private AudioClip dieClip;

    private Guy ennemy = null;

    private AudioSource audioSource;

    private int hp = 10;

    protected int power = 1;

    protected bool punching = false;

    protected Rigidbody body;

    protected GameManager gameManager;
    protected SpawnCrowd spawnCrowd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        spawnCrowd = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnCrowd>();
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody>();
        power = Random.Range(3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive())
        {
            StartCoroutine(Die());
        }
        else
        {
            if (ennemy != null)
            {
                transform.LookAt(ennemy.transform);
                //transform.Translate((transform.forward).normalized * speed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, ennemy.transform.position, speed);

                //body.AddForce(transform.forward * speed);
                transform.Translate(transform.forward * speed * Time.deltaTime);

                Debug.DrawRay(transform.position, transform.forward, Color.black);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (IsAlive())
        {
            Guy colGuy = GetGuy(collision.collider.gameObject);
            if (colGuy != null)
            {
                if (!IsSameColor(colGuy))
                {
                    audioSource?.PlayOneShot(claqueClip);
                    if (!punching)
                    {
                        StartCoroutine(Punch(colGuy));
                    }
                }
            }
        }
    }


    public static Guy GetGuy(GameObject collider)
    {
        return collider.GetComponent("Guy") as Guy;
    }

    public void SetColor(Color color)
    {
        this.color = color;
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.color = color;
        }       
    }
    public Color GetColor()
    {
        return color; 
    }

    public bool IsSameColor(Guy otherGuy)
    {
        return GetColor() == otherGuy.GetColor();
    }

    public void follow(Guy guy)
    {
        ennemy = guy;
    }

    private IEnumerator Punch(Guy poorGuy)
    {
        //punching = true;
        yield return new WaitForSeconds(1/attackSpeed);
        if (poorGuy != null && poorGuy.body != null && IsAlive())
        {
            poorGuy.body.AddForce(transform.forward * power * 10, ForceMode.Impulse);
            if (poorGuy.ApplyDamage(power))
            {
                gameManager.PlayerKilled(this, poorGuy);

                if(Random.Range(1,4) == 4)
                {
                    spawnCrowd.SpawnGuys(10);
                }
            }
        }
        //punching = false;
        yield return new WaitForEndOfFrame();
    }

    public bool IsAlive()
    {
        return hp > 0;
    }

    public bool ApplyDamage(int damage)
    {
        hp -= damage;
        return hp <= 0;
    }

    private IEnumerator Die()
    {
        audioSource.PlayOneShot(dieClip);
        yield return new WaitForSeconds(dieClip.length);
        Destroy(gameObject);
    }
}
