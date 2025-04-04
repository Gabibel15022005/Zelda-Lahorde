using System.Collections;
using UnityEngine;

public class ScDeadlyTraps : MonoBehaviour
{
    Animator _animator;
    public bool IsFast = false;
    [SerializeField] float _delay = 0f;
    public bool IsLoop = true;
    void Start() 
    {
        _animator = GetComponent<Animator>();

        if (IsLoop)
        {
            StartCoroutine(StartAnimeWithDelay());
        }

        _animator.SetBool("IsLoop",IsLoop);
    }
    IEnumerator StartAnimeWithDelay()
    {
        yield return new WaitForSeconds(_delay);
        StartAnime();
    }

    public void StartAnime()
    {
        //Debug.Log("Started anime");
        if (IsFast) _animator.Play("IdleFast");
        else _animator.Play("Idle");
    }

    public void SetIsLoop(bool value)
    {
        IsLoop = value;
        _animator.SetBool("IsLoop",IsLoop);
    }
}
