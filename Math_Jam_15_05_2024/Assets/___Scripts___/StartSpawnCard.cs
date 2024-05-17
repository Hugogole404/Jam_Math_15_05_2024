using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartSpawnCard : MonoBehaviour
{
    public static StartSpawnCard Instance;

    
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        GameObject go = Instantiate(_prefab,
            new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
    }
}