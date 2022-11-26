using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;

    private CharacterController _controller;
	
    /// <summary>
    /// Start is called before the first frame update
	/// </summary>>
    private void Start()
    {
        
    }
	
	/// <summary>>
    /// Update is called once per frame
	/// </summary>>
    private void Update()
    {

    }

    private void Movement()
    {
        float xAxis, yAxis;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            
        }
    }
}
