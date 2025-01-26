using HCore;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    [SerializeField] private BoxCollider blowArea;
    [SerializeField] private float blowForce = 5;
    private Rigidbody[] myBodies;

    private void Awake()
    {
        myBodies = GetComponentsInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var hits = Physics.OverlapBox(blowArea.bounds.center, blowArea.bounds.extents);
        foreach (var hit in hits)
        {
            var rb = hit.GetComponentInParent<Rigidbody>();
            if (rb != null && !myBodies.Contains(rb))
            {
                var direction = (rb.transform.position - transform.position);
                rb.AddForce(direction.normalized * blowForce, ForceMode.Force);
            }
        }
    }
}
