using NUnit.Framework;
using UnityEngine;

public class Guy : MonoBehaviour
{
    [SerializeField]
    private Color color;

    [SerializeField]
    private float speed = 100;

    [SerializeField]
    private AudioClip claqueClip;

    private Guy ennemy = null;

    private AudioSource audioSource;

    private Rigidbody rigidbody;

    private int hp = 10;

    protected int power = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        power = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive())
        {
            Destroy(gameObject);
        }
        else
        {
            if (ennemy != null)
            {
                transform.LookAt(ennemy.transform);
                //transform.Translate((transform.forward).normalized * speed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, ennemy.transform.position, speed);

                rigidbody.AddForce(transform.forward * speed);

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
                    colGuy.GetPunched(this);
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
        (GetComponent("Renderer") as Renderer).material.color = color;
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

    public void GetPunched(Guy meanGuy)
    {
        if (IsAlive() && meanGuy.IsAlive())
        {
            rigidbody.AddForce(meanGuy.transform.forward * meanGuy.power * 100);
            hp -= meanGuy.power;
        }
    }

    public bool IsAlive()
    {
        return hp > 0;
    }
}
