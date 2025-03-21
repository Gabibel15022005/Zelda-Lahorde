using UnityEngine;

public class ScPlayerDamageZone : MonoBehaviour
{
    public int DamageToDeal = 1;
    [SerializeField] float _power = 10f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // infliger les dégats que j'assignerai a DamageToDeal
        {
            ScEnemyStats enemy = collision.gameObject.GetComponent<ScEnemyStats>();
            enemy.TakeDamage(DamageToDeal);
            enemy.PushedBack(transform , _power);
        }

        if (collision.gameObject.CompareTag("Destroyable")) // fait 1 de dégats sur un objet brisable en plusieur fois
        {
            collision.gameObject.GetComponent<ScDestroyable>().TakeDamage(1);
        }
    }
}
