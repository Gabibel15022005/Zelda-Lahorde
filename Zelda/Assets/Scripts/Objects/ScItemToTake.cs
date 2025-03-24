using UnityEngine;

public class ScItemToTake : MonoBehaviour
{
    [SerializeField] NewScriptableObjectScript _obj;
    private SpriteRenderer _sprite;
    [SerializeField] private int _quantity = 1;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        gameObject.name = _obj.name;

        if (_obj.Sprite != null)
        _sprite.sprite = _obj.Sprite;
        else 
        Debug.Log("This Object has no sprite");
    }

    public SpriteRenderer GetSprite() {return _sprite;}
    public NewScriptableObjectScript GetObject() {return _obj;}
    public int GetQuantity() {return _quantity;}
}
