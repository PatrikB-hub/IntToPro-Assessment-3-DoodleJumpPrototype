using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoodleJump.Object.Cell;

namespace DoodleJump.Managers
{
    public class LocationGrid : MonoBehaviour
    {
        private List<GridObject> gridObjects = new List<GridObject>();

        #region Grid Variables

        [SerializeField] private GameObject gridSourceObject;

        public int gridSideLength = 3;

        public int TotalCells
        {
            get
            {
                return gridSideLength * gridSideLength;
            }
        }

        public float GridOffset
        {
            get
            {
                return (float)(gridSideLength * 0.5f) - 0.5f;
            }
        }

        // x values
        private float column = 0;
        // z values
        private float row = 0;

        #endregion


        private void Start()
        {
            for (int i = 0; i < TotalCells; i++)
            {
                GameObject newCell = Instantiate(gridSourceObject, CorrectGridLocation(i, transform.position.y), gridSourceObject.transform.rotation, transform);
                // reset column index
                column = 0;
            }

            /*foreach (GridObject obj in gridObjects)
            {

            }*/
        }

        // first x position will be set
        // then z position 
        // 

        private Vector3 CorrectGridLocation(int _gridIndex, float _gridYIndex)
        {
            while (_gridIndex >= gridSideLength)
            {
                _gridIndex -= gridSideLength;
                column++;
            }
            row = _gridIndex;

            column -= GridOffset;
            row -= GridOffset;

            return new Vector3(column, _gridYIndex, row);




            /*while (row < gridSideLength)
            {

                return new Vector3(column, 0, row);

            }
            column++;
            row = 0;*/

        }


    }
}
