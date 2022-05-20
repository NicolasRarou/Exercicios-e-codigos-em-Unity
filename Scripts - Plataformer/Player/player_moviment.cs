using UnityEngine;

public class player_moviment : MonoBehaviour
{
    private Rigidbody2D body; // variável usada como referência para o visual studio do componente no unity
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;

    [SerializeField]private LayerMask groundlayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float jumpPower;
    [SerializeField]private float speed; // float de controle de velocidade, [SerializeField] permite controle dentro do Unity invés do Visual Studio

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); // GetComponente permite pegar o componente no unity e guarda-lo na variável, criando assim uma referência a ele dentro do Visual Studio
        anim = GetComponent<Animator>(); // Pega referência da animação para o objeto
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // a variável irá guardar o Input original.

        if (horizontalInput > 0.01f)// Permite que o jogador mude a direção quando se movendo entre esquerda e direita
        {
            transform.localScale = Vector3.one;

        } else if (horizontalInput < -0.01f) { 

            transform.localScale = new Vector3(-1, 1, 1); 
        }

        // estabelece os parametros da animação
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

        if(wallJumpCoolDown > 0.2f)// lógica de pular na parede
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            


            if (onWall() && !isGrounded()) {

                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }

            else
                body.gravityScale = 5;

            if (Input.GetKey(KeyCode.Space)) //GetKey tem a mesma função do Get.Component mas como teclas invés de componentes. 
            { 
                jump();

            }

        }
        else
            wallJumpCoolDown += Time.deltaTime;

    }
    private void jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("Jump");

        } else if(onWall()&& !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 2, 3);

            wallJumpCoolDown = 0;
        }

    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
