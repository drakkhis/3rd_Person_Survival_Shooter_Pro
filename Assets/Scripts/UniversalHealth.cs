using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _curHealth;
    [SerializeField]
    private int _minHealth = 1;
    // Start is called before the first frame update
    void Start()
    {
        _curHealth = _maxHealth;
    }

    public void Damage(int amount)
    {
        _curHealth -= amount;
        if (_curHealth < _minHealth)
        {
            Destroy(this.gameObject);
        }
    }
}
