using UnityEngine;

public class ScActivable : MonoBehaviour
{
    protected bool _isActivate = false;
    public virtual void Activate()
    {
        _isActivate = true;
    }
}
