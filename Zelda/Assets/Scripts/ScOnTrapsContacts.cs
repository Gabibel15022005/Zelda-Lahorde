using UnityEngine;

public class ScOnTrapsContacts : MonoBehaviour
{
    public int DamageToDeal = 1;
    [SerializeField] float _power = 5f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScPlayerStats playerStats = collision.gameObject.GetComponent<ScPlayerStats>();
            playerStats.TakeDamage(DamageToDeal);
            playerStats.PushedBack(transform, _power);
        }
    }
}
