using UnityEngine;

public class Ball : MonoBehaviour {
    // configuaration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    // Start is called before the first frame update
    void Start() {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myRigidBody2D.isKinematic = true; // Keeps it forom being effected by gravity
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (!hasStarted) {
            LockToPaddle();
            LauchOnMouseClick();
        }
    }

    private void LauchOnMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            hasStarted = true;
            myRigidBody2D.isKinematic = false;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockToPaddle() {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 velocityTweak = new Vector2 
            (Random.Range(0, randomFactor), 
             Random.Range(0, randomFactor));

        if (hasStarted) {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
