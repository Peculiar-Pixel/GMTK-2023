using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private bool controllerDisabled = false;
    [SerializeField] private int dashDisabledTimeMilliseconds = 200;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.Log("Player rigidbody missing");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontal, vertical, 0) * Time.deltaTime * speed;

        //if (Input.GetAxis("Dash")) ;
    }
}