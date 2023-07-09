using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private enum MotionState
    {
        idle,
        walking,
        running,
        dashing
    }

    //visible in inspector for debugging
    [SerializeField] private MotionState motionState = MotionState.idle;

    //set from inspector
    [SerializeField] private float walkSpeed, runSpeed, dashSpeed, staminaChangeSpeed;
    [SerializeField] private float dashTimeSeconds, dashCooldownSeconds;
    [SerializeField] private float stamina;
    [SerializeField] private Image staminaBar, dashBar;

    //direction handling
    private Vector3 moveDir;
    private Vector3 nonzeroDir = new Vector3(1, 0, 0);

    //internal variables
    private Rigidbody2D rb;
    private int zRotation;
    private bool canDash = true, runReleased = true;
    private float speed;
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
        if (motionState != MotionState.dashing)
        {
            //get move direction
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            if (moveDir != Vector3.zero)
            {
                nonzeroDir = moveDir;
            }

            //Run handling
            if (Input.GetAxisRaw("Run") != 0)
            {
                if (stamina > 0)
                {
                    motionState = MotionState.running; //mode

                    //stamina handling
                    stamina -= Mathf.Clamp(staminaChangeSpeed * Time.deltaTime, 0, 100);
                    staminaBar.fillAmount = stamina / 100;

                    speed = runSpeed;
                }
                else
                {
                    motionState = MotionState.walking;
                    speed = walkSpeed;
                }
                runReleased = false;
            }
            else
            {
                motionState = MotionState.walking;
                speed = walkSpeed;

                if (stamina < 100 && runReleased)
                {
                    stamina += Mathf.Clamp(staminaChangeSpeed * Time.deltaTime, 0, 100);
                    staminaBar.fillAmount = stamina / 100;
                }
                runReleased = true;
            }

            //override move state in case of idling (do not alert guards by running in the same place?)
            if (moveDir == Vector3.zero)
            {
                motionState = MotionState.idle;
            }

            //set 8-axis rotation
            for (int i = 0; i < 8; i++)
            {
                if (moveDir == eightAxis[i])
                {
                    zRotation = i * 45;
                    break;
                }
            }
            transform.rotation = Quaternion.Euler(0, 0, zRotation);

            //Dash handling
            if (Input.GetAxisRaw("Dash") != 0 && canDash)
            {
                StartCoroutine(Dash());
                return; //break out of walk/run/idle mode
            }

            //do moton
            rb.velocity = Vector3.Normalize(moveDir) * Time.deltaTime * speed;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        dashBar.color = Color.gray;
        dashBar.fillAmount = 0;

        motionState = MotionState.dashing;
        rb.velocity = nonzeroDir * dashSpeed; //dash

        yield return new WaitForSeconds(dashTimeSeconds);

        motionState = MotionState.idle;
        StartCoroutine(RefillDash());
    }

    private IEnumerator RefillDash()
    {
        float timeWaited = 0;

        while (timeWaited < dashCooldownSeconds)
        {
            yield return new WaitForSeconds(0.1f);
            timeWaited += 0.1f;
            dashBar.fillAmount = timeWaited / dashCooldownSeconds;
        }

        canDash = true;
        dashBar.color = Color.white;
    }
}