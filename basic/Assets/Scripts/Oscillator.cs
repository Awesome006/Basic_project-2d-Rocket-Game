using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   [DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    
    [SerializeField]
    Vector3 momentVector;
     [Range(0,2)]
    [SerializeField] float MomentFactor;
    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Offset = momentVector * MomentFactor;
        transform.position = Offset + startingPos;
    }
}
