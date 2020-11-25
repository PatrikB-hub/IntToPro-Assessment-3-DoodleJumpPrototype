using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleJump.Player;

namespace DoodleJump.Managers
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private GameObject platformPrefab;

        public static PlatformManager instance;

        public List<GameObject> allPlatforms = new List<GameObject>();

        //LocationGrid instance
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
            locationGrid = (LocationGrid)FindObjectOfType(typeof(LocationGrid));
        }

        void Update()
        {
            SpawnPlatforms(TopPlatform(allPlatforms), 3f);

            PlayerUpPlatformsOff();
        }

        private void PlayerUpPlatformsOff()
        {
            foreach (GameObject item in allPlatforms)
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

        private GameObject TopPlatform(List<GameObject> _allPlatforms)
        {
            GameObject topPlatform = null;
            foreach (GameObject platform in _allPlatforms)
            {
                if (topPlatform == null)
                {
                    topPlatform = platform;
                }
                else
                {
                    if (platform.transform.position.y > topPlatform.transform.position.y)
                    {
                        topPlatform = platform;
                    }
                }
            }

            return topPlatform;
        }

        private void SpawnPlatforms(GameObject _topPlatformPosition, float _spawnAheadValue)
        {
            //player y position at current time
            float playerY = DJPlayer.instance.transform.position.y;

            // check if player is close to top platform
            if ((_topPlatformPosition.transform.position.y - playerY) <= _spawnAheadValue)
            {
                Debug.Log("NOW!!!");
                // use top platform position as starting point

                // make 2 random y values
                float randomY1 = Random.Range(playerY, playerY + 7f);
                float randomY2 = Random.Range(playerY, playerY + 7f);

                // make 2 more values for one x and one z
                bool xGreaterThanZero = _topPlatformPosition.transform.position.x > 0 ? true : false;
                float whichWayX = 0;
                float randomX = Random.Range(_topPlatformPosition.transform.position.x,
                                             whichWayX = xGreaterThanZero ? -(locationGrid.gridSideLength / 2) : locationGrid.gridSideLength / 2);
                Vector3 newPlatformChangeXPosition = _topPlatformPosition.transform.position;
                newPlatformChangeXPosition.x = randomX;
                newPlatformChangeXPosition.y = randomY1;

                bool zGreaterThanZero = _topPlatformPosition.transform.position.z > 0 ? true : false;
                float whichWayZ = 0;
                float randomZ = Random.Range(_topPlatformPosition.transform.position.z,
                                             whichWayZ = xGreaterThanZero ? -(locationGrid.gridSideLength / 2) : locationGrid.gridSideLength / 2);
                Vector3 newPlatformChangeZPosition = _topPlatformPosition.transform.position;
                newPlatformChangeXPosition.z = randomZ;
                newPlatformChangeXPosition.y = randomY2;

                // instantiate 2 platforms one with the x and one with z
                // when created store in a variable and add to platforms list
                GameObject newPlatformChangeX = Instantiate(platformPrefab, newPlatformChangeXPosition, Quaternion.identity, transform);
                allPlatforms.Add(newPlatformChangeX);

                GameObject newPlatformChangeZ = Instantiate(platformPrefab, newPlatformChangeZPosition, Quaternion.identity, transform);
                allPlatforms.Add(newPlatformChangeZ);

            }

        }

        // make a destroy funtion which filters through each platform
        // if a platform is too close to another destroy it
        // if it is lower than the grid destroy it

    }
}
