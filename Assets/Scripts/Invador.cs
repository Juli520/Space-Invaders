using UnityEngine;

public class Invador : MonoBehaviour
{
    public int points = 100;
    public System.Action killed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            killed.Invoke();
            ScoreManager.Instance.AddScore(points);
            Destroy(gameObject);
        }
    }
}
