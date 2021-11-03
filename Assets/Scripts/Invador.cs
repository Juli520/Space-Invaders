using UnityEngine;

public class Invador : MonoBehaviour
{
    public System.Action killed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            killed.Invoke();
            gameObject.SetActive(false);
        }
    }
}
