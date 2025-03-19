using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScPlayerInteract : MonoBehaviour
{
    Animator _animator;
    [SerializeField] float _size = 2f;
    [SerializeField] LayerMask _layerMask;
    private bool _isInteracting = false;
    private bool _canInteract = true;
    private ScPlayerMovement movement;
    [SerializeField] private Transform _posObjInteract;
    ScInventoryUIManager _inventoryManager;
    
    private bool _isInteractingAnim = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _inventoryManager = Camera.main.GetComponent<ScInGameUI>().GetInventoryManager();
        Debug.Log(_inventoryManager);
        movement = GetComponent<ScPlayerMovement>();
    }
    void Update()
    {
        CheckAround();
    }

    void CheckAround()
    {
        Collider2D[] itemCheck = Physics2D.OverlapCircleAll(transform.position, _size, _layerMask);

        if (itemCheck.Length > 0)
        {
            // faire apparaitre l'UI au dessus du joueur
        }

        if (itemCheck.Length > 0 && _isInteracting && _canInteract)
        {
            _canInteract = false;
            ScItemToTake item = itemCheck[0].GetComponent<ScItemToTake>();
            NewScriptableObjectScript obj = item.GetObject();

            Debug.Log($"Interact with {item}");

            if (PlayerPrefs.HasKey(obj.name))
            {
                Debug.Log($"Does have : {obj.name} in PlayerPrefs");
                PlayerPrefs.SetInt($"{obj.name}Qt", PlayerPrefs.GetInt($"{obj.name}Qt") + item.GetQuantity());
            }
            else
            {   
                Debug.Log($"Doesn't have : {obj.name} in PlayerPrefs");
                PlayerPrefs.SetString(obj.name, obj.name);  // sauvegarde le nom de l'item
                PlayerPrefs.SetInt($"{obj.name}Qt", 1);         // sauvegarde la quantité de l'item

                if (obj.IsConsommable)
                {
                    PlayerPrefs.SetInt($"{obj.name}IsConsommable",1); // sauvegarde si il s'agit d'un consommable ou non
                }
                else
                {
                    PlayerPrefs.SetInt($"{obj.name}IsConsommable",0);
                }
            }

            PlayerPrefs.Save();

            _inventoryManager.UpdateToolBarItems();

            movement.CantMove(); // empeche le joueur de bouger
            SetIsInteracting(); // met _isInteracting à true
            _animator.Play("Interact"); // joue l'anim

            item.transform.SetParent(_posObjInteract); // place le à la bonne position
            item.GetSprite().sortingOrder = 30; // met le sprite devant tout
            item.transform.position = _posObjInteract.position; // place le au centre
        }
    }
    public void DestroyObjectAfterAnimation()
    {
        Destroy(_posObjInteract.GetComponentInChildren<ScItemToTake>().gameObject);
        _canInteract = true;
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isInteracting = true;
        }
        else 
        {
            _isInteracting = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _size);
    }

    public void SetIsInteracting()
    {
        _isInteractingAnim = !_isInteractingAnim;
        _animator.SetBool("IsInteracting",_isInteractingAnim);
    }
}
