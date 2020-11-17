using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleJump.Managers;

namespace DoodleJump.Object.Platform
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private float jumpForce = 5f;

        public float JumpForce { get { return jumpForce; } }

        private void Start()
        {
            PlatformManager.instance.platforms.Add(gameObject);
        }



    }
}
