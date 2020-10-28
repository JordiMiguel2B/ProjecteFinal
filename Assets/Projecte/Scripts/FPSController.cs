using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    private GameManager manager;
    private HUD hud;

    public float life;
    public float maxLife = 10;

    public Slider playerLife;
    public Image dashUI;
    


    [SerializeField] private float movementSpeed;
    [SerializeField] private float normalSpeed = 5f;



    private bool jump;
    [SerializeField] private float jumpCoolDown;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool holdingSpace;
    [SerializeField] private bool isGrounded;


    private bool dash;

    [SerializeField] private bool canDash;
    [SerializeField] private int dashCapacity;
    [SerializeField] private bool isDashing;
    [SerializeField] private float waitDash;
    [SerializeField] private float waitDash2;
    [SerializeField] private float timeToDash = 2f;
    [SerializeField] private float timeDashing = 0f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private int dashes = 2;
    [SerializeField] private bool twoDashes;









    private PlayerControls controls = null;

    private Rigidbody rb;


    //Missil
    public GameObject misil;
    public Transform inst;
    private Camera mainCamera;
    [SerializeField] private float misilCounter;
    [SerializeField] private float misilCadency;
    private bool misilShot;


    private ChangeWeapon change;




    // Start is called before the first frame update
    void Awake()
    {

        life = maxLife;
        playerLife.value = CalculateHealth();
        dashUI.fillAmount = CalculateDash();
        


        controls = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;

        mainCamera = Camera.main;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        rb = GetComponent<Rigidbody>();

        movementSpeed = normalSpeed;
        //canDash = true;
        waitDash = timeToDash;



        misilCadency = 2;
        misilShot = true;




        //arma =GameObject.FindGameObjectWithTag("Arma").GetComponent<Arma>();
        //metralleta = GameObject.FindGameObjectWithTag("Metralleta").GetComponent<Metralleta>();

    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame

    void FixedUpdate()
    {

        playerLife.value = CalculateHealth();
        dashUI.fillAmount = CalculateDash();
     



        //waitUIL = waitDash;
        //waitUIR = waitDash;

        Move();

        FixedUpdateJump();
        //FixedUpdateDash();

        waitDash += Time.deltaTime;
        waitDash2 += Time.deltaTime;

        if (waitDash >= timeToDash)
        {
            waitDash = timeToDash;
        }


        //if (dashes < 2)
        //{
        //    if (waitDash > timeToDash && dashes == 1 && twoDashes == false)
        //    {
        //        dashes++;
        //    }

        //    if (waitDash > timeToDash && dashes == 0 && twoDashes == true)
        //    {

        //        dashes++;
        //    }

        //    if (waitDash2 > timeToDash && twoDashes == true)
        //    {
        //        dashes++;
        //        twoDashes = false;
        //    }

        //}

        if (dashes < 2)
        {

            if (waitDash >= timeToDash / 2 && waitDash < timeToDash)
            {
                dashes = 1;
            }

            else if (waitDash == timeToDash)
            {
                dashes = 2;
            }
        }



        //if (dashes >= 2)
        //{
        //    dashes = 2;
        //}


        if (canDash)
        {
            movementSpeed = dashSpeed;
            timeDashing += Time.deltaTime;


            if (timeDashing > dashDuration)
            {
                movementSpeed = normalSpeed;
                canDash = false;
                timeDashing = 0;
                waitDash = waitDash - timeToDash / 2;
                dashes--;

                if (dashes <= 0)
                {
                    dashes = 0;
                }

                //if (dashes == 1)
                //{
                //    waitDash = 0;
                //}

                else if (dashes == 0)
                {
                    //waitDash2 = 0;
                    twoDashes = true;
                }

                if (twoDashes)
                {
                    waitDash = 0;
                }
            }
        }

        //misil

        if (!misilShot)
        {
            misilCounter += Time.deltaTime;
            if (misilCounter >= misilCadency)
            {
                hud.AlphaMisilNormal();  
                misilShot = true;
                misilCounter = 0;

            }
        }

        


    }

    public float CalculateHealth()
    {
        return life / maxLife;
    }

    public float CalculateDash()
    {
        return waitDash / timeToDash; 
    }




    public void Jumping()
    {
        jump = true;

    }

    //public void Dash()
    //{

    //    dash = true;
    //}

    public void Move()
    {
        FixedUpdateMove();
    }

    public void FixedUpdateMove()
    {
        var movementInput = controls.Gameplay.Move.ReadValue<Vector2>();

        var movement = new Vector3();

        movement.x = movementInput.x;
        movement.z = movementInput.y;


        movement.Normalize();

        transform.Translate(movement * movementSpeed * Time.deltaTime);

        //if (movement.x == 0 && movement.z == 0)
        //{
        //    canDash = false;
        //}

        Debug.Log(movement.x);

    }

    private void FixedUpdateJump()
    {
        jumpCoolDown -= Time.deltaTime;

        if (jump)
        {
            


            if (isGrounded)
            {
                jumpCoolDown = 0.2f;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);

            }

            jump = false;
        }

    }

    public void Dash()
    {
        //    var movementInput = controls.Gameplay.Move.ReadValue<Vector2>();

        //    var movement = new Vector3();

        //    movement.x = movementInput.x;
        //    movement.z = movementInput.y;


        //    movement.Normalize();
        //    if (dash) { 
        //        if (movement.x > 0)
        //        {
        //            transform.position += Vector3.back * dashSpeed * Time.deltaTime;
        //            rb.AddForce(Vector3.back * dashSpeed, ForceMode.Impulse);
        //            dash = false;
        //        }

        //        if (movement.x < 0)
        //        {
        //            transform.position += Vector3.forward * dashSpeed * Time.deltaTime;
        //            rb.AddForce(Vector3.forward * dashSpeed, ForceMode.Impulse);
        //            dash = false;
        //        }

        //        if (movement.z > 0)
        //        {
        //            transform.forward += Vector3.left * dashSpeed * Time.deltaTime;
        //            rb.AddForce(Vector3.left * dashSpeed, ForceMode.Impulse);
        //            dash = false;
        //        }

        //        if (movement.z < 0)
        //        {
        //            transform.position += Vector3.right * dashSpeed * Time.deltaTime;
        //            rb.AddForce(Vector3.right * dashSpeed, ForceMode.Impulse);
        //            dash = false;
        //        }
        //    }       

        if (waitDash >= timeToDash && dashes != 0 ||waitDash >= timeToDash / 2 && dashes != 0)
        {
            canDash = true;

        }



    }

    public void ShootMisil()
    {

        if (!misilShot)
        {
            return;
        }

        if (manager.pause == false)
        {
            hud.AlphaMisilTransp();
            misilShot = false;
            GameObject bulletObject = Instantiate(misil, inst.position, Quaternion.identity);
            bulletObject.transform.forward = mainCamera.transform.forward;
        }
       
    }

    public void GetDamage(float damage)
    {
        life -= damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DesblokMetralleta")
        {
            change = GameObject.FindGameObjectWithTag("Holder").GetComponent<ChangeWeapon>();
            change.hasMetralleta = true;
            

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    
}
