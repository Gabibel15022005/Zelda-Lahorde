using UnityEngine;
using UnityEngine.SceneManagement;

public class ScChest : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    ScShowInteraction _interaction;
    Animator _animator;
    bool _hasAlreadyBeenOpen = false;
    bool _isInteractable = true;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _interaction = GetComponent<ScShowInteraction>();

        if (PlayerPrefs.HasKey($"{gameObject.name}InScene({SceneManager.GetActiveScene().name})"))
        {
            _hasAlreadyBeenOpen = true;
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (_isInteractable) 
        {
            _isInteractable = false;
            _interaction.DestroyCanva();
            _animator.Play("OpenChest");
        }
    }

    void DropItem() // called in _animator.Play("OpenChest");
    {
        if (!_hasAlreadyBeenOpen)
        {
            PlayerPrefs.SetInt($"{gameObject.name}InScene({SceneManager.GetActiveScene().name})", 1);
            PlayerPrefs.Save();
            _hasAlreadyBeenOpen = true;
            Instantiate(_prefab, transform.position, Quaternion.identity);
        }
    }
}
