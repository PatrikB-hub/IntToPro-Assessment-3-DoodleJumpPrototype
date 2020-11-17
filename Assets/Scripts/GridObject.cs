using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleJump.Player;
using DoodleJump.Managers;

namespace DoodleJump.Object.Cell
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridObject : MonoBehaviour
    {
        private float platformHalfCheckBox = 0.6f;
        private float playerHalfCheckBox = 0.6f;

        private GameObject gridLocation;

        #region Colour vars
        private SpriteRenderer sPRenderer;

        [SerializeField]
        private Color originalGridColour;
        [SerializeField]
        private Color platformGridColour;
        [SerializeField]
        private Color playerGridColour;
        [SerializeField]
        private Color playerAndPlatformGridColour;

        #endregion

        void Start()
        {
            LocationGrid temp = FindObjectOfType<LocationGrid>();
            gridLocation = temp.gameObject;
            if (gridLocation == null)
            {
                Debug.LogError("Grid Origin location object not found");
            }
            sPRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            sPRenderer.color = ChangeColour();
        }
        
        private Color ChangeColour()
        {
            Color finalColour = (PlatformCheck() && PlayerCheck() ? playerAndPlatformGridColour : PlayerCheck() ? playerGridColour : PlatformCheck() ? platformGridColour : originalGridColour);
            return finalColour;
        }

        private bool PlatformCheck()
        {
            foreach (GameObject gO in PlatformManager.instance.platforms)
            {
                if (gO.transform.position.y > gridLocation.transform.position.y)
                {
                    if (!(gO.transform.position.x <= transform.position.x - platformHalfCheckBox) && !(gO.transform.position.x >= transform.position.x + platformHalfCheckBox))
                    {
                        if (!(gO.transform.position.z <= transform.position.z - platformHalfCheckBox) && !(gO.transform.position.z >= transform.position.z + platformHalfCheckBox))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool PlayerCheck()
        {
            if (DJPlayer.instance.transform.position.y > gridLocation.transform.position.y)
            {
                if (!(DJPlayer.instance.transform.position.x <= transform.position.x - playerHalfCheckBox) && !(DJPlayer.instance.transform.position.x >= transform.position.x + playerHalfCheckBox))
                {
                    if (!(DJPlayer.instance.transform.position.z <= transform.position.z - playerHalfCheckBox) && !(DJPlayer.instance.transform.position.z >= transform.position.z + playerHalfCheckBox))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
