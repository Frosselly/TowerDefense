using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Transform towerToPlace;
    [SerializeField] private LayerMask whereToPlace;

    private Transform towerCopy;

    private void Start()
    {
        towerCopy = Instantiate(towerToPlace, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 500, whereToPlace))
        {
            towerCopy.position = hitInfo.point;
            towerCopy.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }

        if (Input.GetMouseButtonDown(0))
        {
            towerCopy = Instantiate(towerToPlace, Vector3.zero, Quaternion.identity);
        }
        
    }
}
