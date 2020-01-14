using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private Rigidbody playerBody;   // can't use name rigidbody as name due to old naming issue .  This a reference to our Rigidbody

    [SerializeField]
    private int Coins;


    private Vector3 inputVector;    // gets input from xyz
    private bool jump;

    private Game game;

    [SerializeField]
    private TMPro.TextMeshProUGUI coinText;

    private Joystick joystick;


    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();     // This is a Generic Method

        game = FindObjectOfType<Game>();

        joystick = FindObjectOfType<Joystick>();

    }

    // Update is called once per frame
    void Update()       // inputs go here to check as often as possible for player input or button presses
    {

        inputVector = new Vector3(joystick.Horizontal * 10f + Input.GetAxis("Horizontal") * 10f, playerBody.velocity.y, joystick.Vertical * 10f + Input.GetAxis("Vertical") * 10f);   // Input manager in Unity defines
        transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));        // Makes a smooth keyboard player move. GetAxisRaw above makes it snappy

               
        if(Input.GetButtonDown("Jump"))     // check for input but applied the physics in FixedUpdate();
        {
            jump = true;
        }

    }

    private void FixedUpdate()      // fires at a fixed 50 fps
    {
        playerBody.velocity = inputVector;

        if(jump && IsGrounded())        //makes sure you are on the ground
        {
            playerBody.AddForce(Vector3.up * 20f, ForceMode.Impulse);  // you can adjust the rigidbody mass to set jump height & you can adjust Physics Gravity in input manager which was set to -20 here in unity
            jump = false;
        }
    }

    bool IsGrounded()
    {
        float distance = GetComponent<Collider>().bounds.extents.y + 0.01f;          // size of your player for the ray to travel
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.Raycast(ray, distance);
    }

    // Ray is used to check for collision on bottom of player to see if the floor is beneath


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wolf"))
        {
            game.ReloadCurrentLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Coin":
                Coins++;
                Destroy(other.gameObject);
                coinText.text = string.Format("Coins\n{0}", Coins);
                break;
            case "Goal":
                other.GetComponent<Goal>().CheckForCompletion(Coins);
                break;
        }
    }
}
