using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScBombeBehaviour : MonoBehaviour
{
    [SerializeField] float _timeBeforeBoom = 3f;
    SpriteRenderer _sprite;
    [SerializeField] Color _midTimeColor;
    [SerializeField] Color _closeTimeColor;
    [SerializeField] float _explosionSize = 2f;
    [SerializeField] int _damage = 2;
    [SerializeField] float _explosionPower = 20;
    Gamepad _gamepad;


    void Start()
    {
        _gamepad = Gamepad.current;
        _sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(_timeBeforeBoom / 3);
        _sprite.color = _midTimeColor;
        yield return new WaitForSeconds(_timeBeforeBoom / 3);
        _sprite.color = _closeTimeColor;
        yield return new WaitForSeconds(_timeBeforeBoom / 3);
        Boom();
    }

    void Boom()
    {
        // lancer l'animation d'explosion

        Collider2D[] colliderInRange = Physics2D.OverlapCircleAll(transform.position, _explosionSize);

        foreach (Collider2D collider in colliderInRange)
        {
            if (collider.TryGetComponent(out ScPlayerStats player))
            {
                player.TakeDamage(_damage);
                player.PushedBack(transform, _explosionPower);
            }

            if (collider.TryGetComponent(out ScEnemyStats enemy))
            {
                enemy.TakeDamage(_damage);
                enemy.PushedBack(transform, _explosionPower);
            }

            if (collider.TryGetComponent(out ScDestroyable obj))
            {
                obj.TakeDamage(_damage);
                obj.PushedBack(transform, _explosionPower);
            }

            if (collider.TryGetComponent(out ScActivable mécanisme))
            {
                if (mécanisme.gameObject.CompareTag("ActivableByPlayer"))
                {
                    mécanisme.Activate();
                }
            }
        }

        Debug.Log("Boom");
        _gamepad.SetMotorSpeeds(1,1);

        DestroyBombe(); // A retirer une fois l'animation fini
    }
    public void DestroyBombe() // a appelé en unity event à la fin de l'animation
    {
        _gamepad.SetMotorSpeeds(0,0);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionSize);
    }
}
