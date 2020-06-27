using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private Enemy _AI;

    private void Start()
    {
        _AI = transform.GetComponentInParent<Enemy>();
        if (_AI == null)
        {
            Debug.Log("AI is Null");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _AI.AttackTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        _AI.AttackTriggerExit(other);
    }
}