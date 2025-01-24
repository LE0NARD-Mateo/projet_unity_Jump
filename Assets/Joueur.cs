using TMPro;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int ForceSaut = 7;

    public int vitesse = 5;

    public TextMeshProUGUI TextScore;

    private int _score = 0;

    private Rigidbody2D _rb;

    private Animator _animator;

    private bool _toucheSol = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouvement = Input.GetAxisRaw("Horizontal");
        
        _rb.linearVelocity = new Vector2(mouvement* vitesse, _rb.linearVelocityY);

        if(Input.GetButtonDown("Jump") && _toucheSol) {
            _rb.AddForce(new Vector2(0, ForceSaut * 20));
        }

        _animator.SetBool("deplacement", mouvement != 0 );

        if(mouvement != 0){
            Vector3 localScale = transform.localScale;
            localScale.x = mouvement > 0 ? 1f : -1f;
            transform.localScale = localScale;
        }

    }

    private void OnCollisionEnter2D(Collision2D collider){

        if(collider.gameObject.CompareTag("Sol")){
            _toucheSol = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collider){

        if(collider.gameObject.CompareTag("Sol")){
            _toucheSol = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider){

        if(collider.gameObject.CompareTag("Argent")){

            _score ++;
            TextScore.text = "Score:" + _score;
            Destroy(collider.gameObject);
        }
    }
}
