using UnityEngine;

public class Bonhomme_Player : MonoBehaviour
{
    public int forceSaut = 7;

    public int vitesse = 5;

    public int saut = 3;

    private Rigidbody2D _rb;

    private Animator _animator;

    private bool _toucheSol = false;

    private bool _dansLesAirs = false;

    private bool _chuteRalenti = false;

    private bool _doubleSautPossible = false;

    private bool _superSautPossible = true;

    private bool _chuteSautPossible = false;

    private bool _doubleSautConsummer= false;

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

        if(Input.GetButtonDown("Jump") && _toucheSol && _superSautPossible) {
            _rb.AddForce(new Vector2(0, forceSaut * 10));
            _doubleSautPossible = true;
            _superSautPossible = false;
            saut = 0;
        }
        if(Input.GetButtonDown("Jump") && _toucheSol && saut < 3) {
            _rb.AddForce(new Vector2(0, forceSaut * 25));
            _doubleSautPossible = true;
            saut += 1;
        }

        if(saut == 3){
            _superSautPossible = true;
        }

        if(Input.GetButtonDown("Jump") && _dansLesAirs && _doubleSautPossible) {
            _rb.AddForce(new Vector2(0, forceSaut * 25));
            _doubleSautPossible = false;
            _chuteSautPossible = true;
            _doubleSautConsummer = true;
        }

        if(Input.GetButtonDown("Jump") && _dansLesAirs && _chuteSautPossible) {
            _chuteRalenti = true;
        }

        
        if(Input.GetButtonUp("Jump") && _dansLesAirs || _toucheSol) {
            _chuteRalenti = false;
        }

        if (_chuteRalenti)
        {
            _rb.AddForce(new Vector3(0, 0.5f,0));
        }
        else
        {

        }

        _animator.SetBool("deplacement", mouvement != 0 );

        _animator.SetBool("jump", _dansLesAirs);

        _animator.SetBool("super_jump", _dansLesAirs && saut == 1);

        _animator.SetBool("double_jump", _dansLesAirs && _doubleSautConsummer);

        _animator.SetBool("chute_jump", _dansLesAirs && _chuteRalenti);

        if(mouvement != 0){
            Vector3 localScale = transform.localScale;
            localScale.x = mouvement > 0 ? 1f : -1f;
            transform.localScale = localScale;
        }

    }

    private void OnCollisionEnter2D(Collision2D collider){

        if(collider.gameObject.CompareTag("Sol")){
            _toucheSol = true;
            _dansLesAirs = false;
            _chuteSautPossible = false;
            _chuteRalenti = false;
            _doubleSautConsummer = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collider){

        if(collider.gameObject.CompareTag("Sol")){
            _toucheSol = false;
            _dansLesAirs = true;
        }
    }
}
