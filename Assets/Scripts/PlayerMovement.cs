using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private bool alive = true;
    public float speed;
    public float turnspeed;
    [SerializeField] GameObject tracks;
    private int trackNo = 1; //0 left, 1 middle, 2 right
    [SerializeField] LayerMask groundMask;
    [SerializeField] Rigidbody rb;
    public float jumpForce;

    void FixedUpdate()
    {
        if (!alive) return;
        
        Vector3 tarPos = Vector3.zero;
        tarPos.x = tracks.transform.GetChild(trackNo).position.x;
        tarPos.y = transform.position.y;
        tarPos.z = transform.position.z;
        //move horizontal
        transform.position = Vector3.MoveTowards(transform.position, tarPos, turnspeed * Time.fixedDeltaTime);
        //reset the x-axis
        tarPos.x = transform.position.x;
        //move forward
        tarPos.z = transform.position.z + speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, tarPos, speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            trackNo -= 1;
            trackNo = Mathf.Clamp(trackNo, 0, 2);
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            trackNo += 1;
            trackNo = Mathf.Clamp(trackNo, 0 ,2);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
        
    }

    void Jump()
    {
        //Check whether on ground
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGround = Physics.Raycast(transform.position, Vector3.down, (height/2) + 0.01f, groundMask);
        //If we are, jump
        if (isGround && alive)
            rb.AddForce(Vector3.up * jumpForce);
    }

    public void Die()
    {
        if (alive) {
            alive = false;
            gameManager.Restart();
        }
    }
}
