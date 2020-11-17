using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleJump.Player;

namespace DoodleJump.Managers
{
    public class PlatformManager : MonoBehaviour
    {
        public static PlatformManager instance;

        public List<GameObject> platforms = new List<GameObject>();

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

        void Update()
        {
            foreach (GameObject item in platforms)
            {
                if (DJPlayer.instance.GoingUp())
                {
                    item.SetActive(false);
                }
                else
                {
                    item.SetActive(true);
                }
            }
        }
    }
}
