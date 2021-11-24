using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Invaders : MonoBehaviourPun
{
    public static Invaders Instance = null; 
    
    public Invador[] prefabs;
    public AnimationCurve speed;
    public Vector3 direction { get; private set; } = Vector3.right;
    public System.Action killed;
    public System.Action<EnemyShip> killedShip;
    [SerializeField, HideInInspector] private Invador _invador;
    
    public int rows;
    public int colums;
    
    public Projectile missile;
    public float rateAttack = 1f;

    public int AmountKilled { get; private set; }
    public int AmountAlive => TotalInvaders - AmountKilled;
    public int TotalInvaders => rows * colums;
    public float PercentKilled => (float)AmountKilled / (float)TotalInvaders;

    private void Awake()
    {
        if(!photonView.IsMine) return;
        
        if (Instance == null)
            Instance = this;
        
        for (int row = 0; row < rows; row++)
        {
            float width = 2f * (colums - 1);
            float height = 2f * (rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2f), 0);

            for (int col = 0; col < colums; col++)
            {
                _invador = PhotonNetwork.Instantiate(prefabs[row].name, photonView.transform.position, Quaternion.identity).GetComponent<Invador>();
                _invador.SetParent();
                _invador.killed += InvaderKilled;
                
                Vector3 position = rowPosition;
                position.x += col * 2f;
                _invador.photonView.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), rateAttack, rateAttack);
    }

    private void Update()
    {
        float speed = this.speed.Evaluate(PercentKilled);
        transform.position += direction * speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
                continue;
            
            if(direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
                break;
            }
            else if(direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
                break;
            }
        }
    }

    private void AdvanceRow()
    {
        direction = new Vector3(-direction.x, 0.0f, 0.0f);
        
        Vector3 position = transform.position;
        position.y -= 1.0f;
        transform.position = position;
    }

    private void MissileAttack()
    {
        int amountAlive = AmountAlive;

        if (amountAlive == 0) 
            return;
        
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy) 
                continue;
            
            if (Random.value < (1.0f / (float)amountAlive))
            {
                PhotonNetwork.Instantiate(missile.name, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    public void InvaderKilled()
    {
        AmountKilled++;
        //killed.Invoke();
    }   
    
    public void ShipKilled(EnemyShip enemyShip)
    {
        AmountKilled++;
        //killedShip(enemyShip);
    }
}