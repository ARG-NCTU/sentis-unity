using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2f;
    public float jumpForce = 60f;
    public float rotationSpeed = 60f;
    private bool isGrounded;
    public Animator anim;
    public GameObject Carrot;
    public GameObject grabPosition_;
    public GameObject putPosition_;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

    }

    void Grab(GameObject gameObject)
    {
        // Set the gameObject as child of grabPosition
        gameObject.transform.parent = grabPosition_.transform;

        // To avoid bugs, set object velocity and angular velocity to 0
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        // Set the gameObject transform position to grabPosition
        gameObject.transform.position = grabPosition_.transform.position;

        Collider collider = gameObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    void Update()
    {

        Vector3 moveDirection = Vector3.zero;
        Transform playerTransform = this.transform;

        if (EventSystem.current.IsPointerOverGameObject() || IsAnyInputFieldFocused())
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += playerTransform.forward;
            anim.Play("Walk");
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -playerTransform.forward;
            anim.Play("Walk");
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += -playerTransform.right;
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += playerTransform.right;
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Grab(Carrot);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Carrot.transform.parent = putPosition_.transform;
            Carrot.transform.position = putPosition_.transform.position; ;
        }

        if (Input.GetKey(KeyCode.R))
        {
            StandUp();
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("jump", true);
        }
    }

    void StandUp()
    {
        // transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(new Vector3(3.68093038f, 189.38353f, 352.060425f));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    bool IsAnyInputFieldFocused()
    {
        foreach (var inputField in GameObject.FindObjectsOfType<UnityEngine.UI.InputField>())
        {
            if (inputField.isFocused)
            {
                return true;
            }
        }

        return false;
    }

}
