using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodEffect;
    [SerializeField]
    private LayerMask IgnoreLayer;
    public void Shoot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreLayer))
        {
            UniversalHealth other = hit.transform.GetComponent<UniversalHealth>();
            if (other != null)
            {
                GameObject blood = Instantiate(_bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                blood.transform.parent = hit.transform;
                other.Damage(1);
                Animator bloodAnimator = blood.GetComponent<Animator>();
                if (bloodAnimator != null)
                {
                    Destroy(blood, blood.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
                }
            }
        } 
    }
}
