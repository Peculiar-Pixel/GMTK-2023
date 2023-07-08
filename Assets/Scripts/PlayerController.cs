using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private bool disabled = false;
    [SerializeField] private float dashDisabledTimeSeconds = 0.5f;

    private Rigidbody2D rb;
    private Vector3 motion;
    private int rotation;

    private Vector3[] eightAxis = new Vector3[] { new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(-1, 1, 0), new Vector3(-1, 0, 0), new Vector3(-1, -1, 0), new Vector3(0, -1, 0), new Vector3(1, -1, 0) };

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.Log("Player rigidbody missing");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!disabled)
        {
            //Walk/Run direction
            motion = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            //8-axis rotation
            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

            for (int i = 0; i < 8; i++)
            {
                if (dir == eightAxis[i])
                {
                    rotation = i * 45;
                    break;
                }
            }

            transform.rotation = Quaternion.Euler(0, 0, rotation);

            //Dash handling
            if (Input.GetAxis("Dash") != 0)
            {
                Debug.Log("Dash start");
                StartCoroutine(DisableForSeconds(dashDisabledTimeSeconds));
                //cancel current movement
                motion = Vector3.zero;

                //do dash
            }

            //do moton
            rb.velocity = motion * Time.deltaTime * speed;
        }
    }

    private IEnumerator DisableForSeconds(float seconds)
    {
        disabled = true;
        yield return new WaitForSecondsRealtime(seconds);
        disabled = false;
        Debug.Log("Dash end");
    }
}