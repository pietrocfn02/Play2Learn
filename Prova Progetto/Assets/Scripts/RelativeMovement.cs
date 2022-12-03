using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private static float rotSpeed = 15.0f;
    private static int moveSpeed = 1;
    private AudioSource _soundSource;
    [SerializeField] private AudioClip footStepSound;
    private float _footStepSoundLength;
    private bool _step;

    private float jumpSpeed = 6f;
    private float gravity = -9.8f;
    private float terminalVelocity = -10.0f;
    private float minFall = -1.2f;
    private float _vertSpeed;
    private ControllerColliderHit _contact;

    private Animator _animator;
    private CharacterController _charController;
    
    IEnumerator WaitForFootSteps(float stepsLength) {
        _step = false;
        yield return new WaitForSeconds(stepsLength);
        _step = true;
    }
    
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
        _animator = GetComponent<Animator>();
        _soundSource = GetComponent<AudioSource>();
        _step = true;
        _footStepSoundLength = 0.30f;
    }

    // Update is called once per frame
    void Update() {
        
        Vector3 movement = Vector3.zero;
        
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (_charController.velocity.magnitude > 1f && _step) {
            _soundSource.PlayOneShot(footStepSound);
            StartCoroutine(WaitForFootSteps(_footStepSoundLength));
        }
        if (horInput != 0 || vertInput != 0) {
             _animator.SetFloat("speed", 1f);
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;
            transform.rotation = Quaternion.LookRotation(movement);
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation,direction, rotSpeed * Time.deltaTime);
        } 


        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        _animator.SetFloat("speed", movement.magnitude);

        if (hitGround) {
            if (Input.GetButtonDown("Jump")) {
                _vertSpeed = jumpSpeed;
                                
            } else {
                _vertSpeed = minFall;
                _animator.SetBool("jump", false);
            }
        } else {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity) {
                        _vertSpeed = terminalVelocity;
            }
            _animator.SetBool("jump", true);
            if (_charController.isGrounded) {
                if (Vector3.Dot(movement, _contact.normal) < 0.01f) {
                    movement = _contact.normal * moveSpeed;
                    
                } else {
                    movement += _contact.normal * moveSpeed;
                    
                }
            }
        }    
        movement.y = _vertSpeed;
        movement *= Time.deltaTime;
        _charController.Move(movement);
    }
    
    public static void SetMovementSpeed(int speed){
        moveSpeed = speed;
    }
}
