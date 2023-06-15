using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Basemovement : MonoBehaviour
{
    [Header("Rigidbody y movimiento")]//Movimiento lateral
    private Rigidbody2D Rigidbody;
    [SerializeField]
    private float Velocidad;
    private float moveinput;
    bool facingRight;

    [Header("GroundCheck")]//Groundcheck para salto y demas
    private bool IsGrounded;
    public Transform Feet;
    public LayerMask WhatIsGround;
    public float Groundcheck;

    [Header("Salto")]//Relacionado con logica de salto
    public bool Jetpack;
    bool Canjump;
    public float Jumpheight;
    private float Jumptimecounter;
    public float Jumptime;
    private bool IsJumping;
    private int Extrajumps = 1;
    public int Extrajumpsvalue;
    public Transform point;
    public float CheckRadius;
    public LayerMask GroundLayer;

    [Header("Deslizamiento")]//Dash agachao
    public bool Boots;
    public bool Candash = true;
    public float Dashspeed;
    public float Dashtime;
    public float Dashjump;
    public float Dashcooldown;


    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Movimiento (inputs y logica)
        moveinput = Input.GetAxisRaw("Horizontal");
        Rigidbody.velocity = new Vector2(moveinput * Velocidad, Rigidbody.velocity.y);
    }

    private void Update()
    {
        //Deslizamiento
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Deslizamiento();
        }
        if (Candash == false)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);
        }
        //Logica de GroundCheck
        if(IsGrounded == true)
        {
            Canjump = true;
            Extrajumpsvalue = Extrajumps;
        }
        else
        {
            Canjump = false;
        }
        IsGrounded = Physics2D.OverlapCircle(Feet.position, Groundcheck, WhatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && Canjump == true )
        {
            IsJumping = true;
            Jumptimecounter = Jumptime;
            Extrajumpsvalue -= 1;
            Rigidbody.velocity = Vector2.up * Jumpheight;
        }
        //Salto y futura logica de Doblesalto con el "Jetpack"
        if (Input.GetKeyDown(KeyCode.Space) && Extrajumpsvalue > 0 && IsGrounded == false && IsJumping == true && Jetpack == true)
        {
                Extrajumpsvalue -= 1;
                Jumptimecounter -= Time.deltaTime;
                Rigidbody.velocity = Vector2.up * Jumpheight;           
        }
        
        if(moveinput > 0 && facingRight)
        {
            Flip();
        }
        if (moveinput < 0 && !facingRight)
        {
            Flip();
        }
    }

    //Deslizamiento
    void Deslizamiento()
    {
        if (Candash && Boots == true)//"Boots" es el power up que permite deslizarte
        {
            StartCoroutine(IEDeslizamiento());
        }
    }
    //Enumerator de deslizamiento
    IEnumerator IEDeslizamiento()
    {
        Candash = false;
        Velocidad = Dashspeed;
        //Jumpheight = Dashjump;
        yield return new WaitForSeconds(Dashtime);
        Velocidad = 11f;
        //Jumpheight = 20f;
        yield return new WaitForSeconds(Dashcooldown);
        Candash = true;        
    }

    void Flip()
    {
        Vector3 currentscale = gameObject.transform.localScale;
        currentscale.x *= -1;
        gameObject.transform.localScale = currentscale;

        facingRight = !facingRight;
    }

}
