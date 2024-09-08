using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform projectile;
    [SerializeField] Transform projectileSummonPosition;
    [SerializeField] private float fireRadius;
    [SerializeField] private LayerMask enemyLayer;

    private bool isPlaced;
    private bool isDisabled;

    private Collider[] enemies;

    private Vector3 targetScale;
    // Start is called before the first frame update
    void Start()
    {
        targetScale = Vector3.one * 50;
        StartCoroutine(SummonProjectile(targetScale, 5f)); 
    }

    // Update is called once per frame
    void Update()
    {
        enemies = Physics.OverlapSphere(transform.position, fireRadius, enemyLayer);
        if (!isDisabled && enemies.Length != 0)
        {
            print("In Range!");
            StartCoroutine(SummonProjectile(targetScale, 5f)); 
            isDisabled = true;
        }
        
        //increase local scale of projectile
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRadius);
    }

    IEnumerator SummonProjectile(Vector3 targetScale, float duration)
    {
        Transform instance = Instantiate(projectile, projectileSummonPosition.position, Quaternion.identity, transform);
        float currentTime = 0;
        Vector3 currentScale = transform.localScale; 
        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            instance.localScale = Vector3.Lerp(currentScale, targetScale, currentTime / duration);
            
            yield return null;
        }
        Destroy(instance.gameObject);
        yield return SummonProjectile(targetScale, 5f);
    }
}
