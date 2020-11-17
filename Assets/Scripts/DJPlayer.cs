using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleJump.Object.Platform;
using DoodleJump.Managers;

namespace DoodleJump.Player
{
    public class DJPlayer : MonoBehaviour
    {
        public static DJPlayer instance;
        public Rigidbody rb;

        [SerializeField]
        private GameObject locationGridObject;
        private LocationGrid locationGrid;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            locationGrid = locationGridObject.GetComponent<LocationGrid>();
        }

        private void Update()
        {
            HorizontalAndForwardMovement(5f);
            SpawnOpposite();
        }

        private void HorizontalAndForwardMovement(float _forceAmount)
        {
            if (Input.GetKeyDown(KeyCode.W) 
                || Input.GetKeyDown(KeyCode.S) 
                || Input.GetKeyDown(KeyCode.DownArrow) 
                || Input.GetKeyDown(KeyCode.UpArrow))
            {
                //forward and back
                float fAndB = Input.GetAxisRaw("Vertical");
                rb.AddForce(Vector3.forward * fAndB * _forceAmount, ForceMode.VelocityChange);
            }
            if (Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.D)
                || Input.GetKeyDown(KeyCode.LeftArrow)
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                //left and right
                float lAndR = Input.GetAxisRaw("Horizontal");
                rb.AddForce(Vector3.right * lAndR * _forceAmount, ForceMode.VelocityChange);
            }

        }

        public bool GoingUp()
        {
            if (rb.velocity.y > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SpawnOpposite()
        {
            if (Mathf.Abs(transform.position.x) > locationGrid.gridSideLength * 0.5f)
            {
                Vector3 playerPosition = transform.position;
                playerPosition.x = -playerPosition.x;
                playerPosition.z = -playerPosition.z;
                transform.position = playerPosition;
            }
            else if (Mathf.Abs(transform.position.z) > locationGrid.gridSideLength * 0.5f)
            {
                Vector3 playerPosition = transform.position;
                playerPosition.x = -playerPosition.x;
                playerPosition.z = -playerPosition.z;
                transform.position = playerPosition;
            }
            else
            {
                return;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Platform>() != null)
            {
                Platform platform = collision.gameObject.GetComponent<Platform>();
                rb.AddForce(Vector3.up * (platform.JumpForce), ForceMode.Impulse);
                platform = null;
            }
        }
    }
}
