using System;
using Addressables;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;
using Object = UnityEngine.Object;

namespace Environment.MoveGrid
{
    public class GridManager : MonoBehaviour
    {
        public MovePoint[] MovePoints;
        private GridSO _gridSo;
        private int _i;
        private int _j;
        private Transform _rendererPointParent;
        private Action _callback;

        public void SetupGrid(GridSO so,
            Action callback)
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>(_gridSo.RendererMovePointAdressableName,
                GenerateRendererMovePoints);
            _callback = callback;
        }

        private void GenerateRendererMovePoints(GameObject gameObject)
        {
            _rendererPointParent = new GameObject().transform;
            Vector3[] allDirections = new Vector3[_gridSo.MovePoints];

            float angleBetweenTwoCandidatePos = Mathf.PI * 2 / _gridSo.MovePoints;

            for (var (i, currentAngle) = (0, (float)0);
                 i < _gridSo.MovePoints;
                 i++, currentAngle += angleBetweenTwoCandidatePos)
            {
                allDirections[i] = new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle)).normalized;
            }

            MovePoints = new MovePoint[_gridSo.CircleRadius.Length * _gridSo.MovePoints];
            for (_i = 0; _i < _gridSo.CircleRadius.Length; _i++)
            {
                for (_j = 0; _j < _gridSo.MovePoints; _j++)
                {
                    var pos = allDirections[_j] * _gridSo.CircleRadius[_i];
                    var rendererMovePoint = Object.Instantiate(gameObject, pos, Quaternion.identity);
                    MovePoints[_i * _gridSo.MovePoints + _j] =
                        new MovePoint(rendererMovePoint.GetComponent<MeshRenderer>(), pos);
                    MovePoints[_i * _gridSo.MovePoints + _j].MeshRenderer.transform.parent = _rendererPointParent;
                }
            }

            /*
            var rendererMoveCenterPoint = Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            MovePoints[^1] = new MovePoint(rendererMoveCenterPoint.GetComponent<MeshRenderer>(), Vector3.zero);*/
            Release(gameObject);

            GenerateNeighbors();
            _callback?.Invoke();
        }

        private void GenerateNeighbors()
        {
            /*
            for (_i = 0; _i < _movePointsByCircle; _i++)
            {
                MovePoints[_i].Neighbors.Add(MovePoints.Length - 1);
                MovePoints[^1].Neighbors.Add(_i);
            }
            */

            for (_i = _gridSo.MovePoints; _i < MovePoints.Length; _i++)
            {
                MovePoints[_i].NeighborTopIndex = (_i - _gridSo.MovePoints);
            }

            for (_i = 0; _i < MovePoints.Length - _gridSo.MovePoints; _i++)
            {
                MovePoints[_i].NeighborDownIndex = (_i + _gridSo.MovePoints);
            }

            for (_i = 0; _i < _gridSo.CircleRadius.Length; _i++)
            {
                int currentCircleIndex = _i * _gridSo.MovePoints;
                for (_j = 1; _j < _gridSo.MovePoints; _j++)
                {
                    int currentIndex = currentCircleIndex + _j;
                    MovePoints[currentIndex].NeighborLeftIndex = currentIndex - 1;
                }

                for (_j = 0; _j < _gridSo.MovePoints - 1; _j++)
                {
                    int currentIndex = currentCircleIndex + _j;
                    MovePoints[currentIndex].NeighborRightIndex = (currentIndex + 1);
                }

                MovePoints[currentCircleIndex].NeighborLeftIndex = (currentCircleIndex + _gridSo.MovePoints - 1);
                MovePoints[currentCircleIndex + _gridSo.MovePoints - 1].NeighborRightIndex = (currentCircleIndex);
            }
        }

        public void UpdateOnAllMovePointIfIsOccupied()
        {
            foreach (var movePoint in MovePoints)
            {
                if ((movePoint.LocalPosition + _rendererPointParent.position).sqrMagnitude >
                    _gridSo.CircleEnvironnementSqRadius)
                {
                    movePoint.IsOccupied = true;
                }
                else
                {
                    movePoint.IsOccupied = false;
                }
            }
        }

        public bool CheckIfOneMovePointInCirclesIsOccupied(int[] circleIndexes, Vector3 offset)
        {
            for (_i = 0; _i < circleIndexes.Length; _i++)
            {
                int currentCircleIndex = _i * _gridSo.MovePoints;
                for (_j = 0; _j < _gridSo.MovePoints; _j++)
                {
                    int currentIndex = currentCircleIndex + _j;
                    if ((MovePoints[currentIndex].LocalPosition + offset).sqrMagnitude >
                        _gridSo.CircleEnvironnementSqRadius)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public void MoveGrid(Vector3 pos)
        {
            _rendererPointParent.position = pos;
            UpdateOnAllMovePointIfIsOccupied();
        }

        public bool CheckIfMovePointInIsCircles(int index, params int[] circlesIndexes)
        {
            for (int i = 0; i < circlesIndexes.Length; i++)
            {
                var gridSoMovePoints = _gridSo.MovePoints * (circlesIndexes[i]);
                if (index >= gridSoMovePoints && index <= gridSoMovePoints + _gridSo.MovePoints)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetIndexCircleFromMovePoint(int movePointIndex)
        {
            return movePointIndex % _gridSo.MovePoints;
        }

        public int GetIndexMovePointFromStartMovePointLineWithCircle(int startMovePointIndex, int circleReached)
        {
            return GetModuloIndex(startMovePointIndex) * (circleReached + 1);
        }

        public int GetIndexMovePointFromStartMovePointLine(int startMovePointIndex, int indexMovedAmount)
        {
            return startMovePointIndex + indexMovedAmount * _gridSo.MovePoints;
        }
        
        public int GetModuloIndex(int movePointIndex)
        {
            return movePointIndex % _gridSo.MovePoints;
        }

        public int GetIndexMovePointFromStartMovePointCircle(int startMovePointIndex, int indexMovedAmount)
        {
            var neighborIndex = MovePoints[startMovePointIndex].NeighborLeftIndex;
            indexMovedAmount--;
            if (indexMovedAmount > 0)
            {
                return GetIndexMovePointFromStartMovePointCircle(neighborIndex, indexMovedAmount);
            }
            return neighborIndex;
        }
    }
}