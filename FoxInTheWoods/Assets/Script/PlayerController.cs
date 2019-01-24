using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float normalSpeed;
    public float fastSpeed;

    private float moveSpeed;

    public CharacterController controller;
    public TextController textController;
    public TextController losePanel;

    public float gravityScale;
    public float jumpForce;

    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    private Vector3 moveDirection;

    private bool canCatch = false;
    public bool isAlive;

    private List<GameObject> objectInRange = new List<GameObject>();

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        moveSpeed = normalSpeed;
        isAlive = true;
	}

    // Update is called once per frame
    void Update () {

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = fastSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        //moveDirection = new Vector3(moveSpeed * Input.GetAxis("Horizontal"), moveDirection.y, moveSpeed * Input.GetAxis("Vertical"));
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed);
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.transform.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, moveSpeed * Time.deltaTime);
        }

        if (canCatch && Input.GetKeyDown(KeyCode.F))
        {
            Caught();
            textController.CloseMessagePanel();
        }

        if(!isAlive)
        {
            losePanel.OpenMessagePanel();
            Time.timeScale = 0;
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene("SampleScene");
                losePanel.CloseMessagePanel();
                Time.timeScale = 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Little Fox")
        {
            canCatch = true;
            textController.OpenMessagePanel();
            objectInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Little Fox")
        {
            canCatch = false;
            textController.CloseMessagePanel();
            objectInRange.Remove(other.gameObject);
        }
    }

    private void Caught()
    {
        foreach (GameObject gameObject in objectInRange)
            Destroy(gameObject);
    }
}
