using UnityEngine;
using Random = UnityEngine.Random;

public class Invaders : MonoBehaviour
{
    public Invador[] prefabs;
    public int rows = 5;
    public int colums = 11;
    public AnimationCurve speed;
    public Projectile missile;
    public float rateAttack = 1f;
    
    public int amountKilled { get; private set; }
    public int amountAlive => totalInvaders - amountKilled;
    public int totalInvaders => rows * colums;
    public float percentKilled => (float)amountKilled / (float)totalInvaders;

    private Vector3 _direction = Vector2.right;

    private void Awake()
    {
        for (int row = 0; row < rows; row++)
        {
            float width = 2f * (colums - 1);
            float height = 2f * (rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2f), 0);

            for (int col = 0; col < colums; col++)
            {
                Invador invader = Instantiate(prefabs[row], transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), rateAttack, rateAttack);
    }

    private void Update()
    {
        transform.position += _direction * speed.Evaluate(percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if(invader.gameObject.activeInHierarchy)
                continue;
            
            if(_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1f))
                AdvanceRow();
            else if(_direction == Vector3.left && invader.position.x >= (leftEdge.x + 1f))
                AdvanceRow();
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1f;

        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }

    private void MissileAttack()
    {
        foreach (Transform invader in transform)
        {
            if(invader.gameObject.activeInHierarchy)
                continue;
            
            if(Random.value < (1f / (float)amountAlive))
            {
                Instantiate(missile, transform.position, Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderKilled()
    {
        amountKilled++;
    }
}
