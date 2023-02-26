using System;
using Addressables;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;
using Object = UnityEngine.Object;

namespace Environment.MoveGrid
{
    public class EnvironmentGridManager : MonoBehaviour
    {
        public MovePoint[] MovePoints;

        private float[] _circleRadius;
        private int _movePointsByCircle;
        private int _i;
        private int _j;
        private Action _callback;

        public void SetupGrid(float[] circles, int movePointsByCircle, string rendererMovePointsAddressableName,
            Action callback)
        {
            _circleRadius = circles;
            _movePointsByCircle = movePointsByCircle;
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>(rendererMovePointsAddressableName,
                GenerateRendererMovePoint);
            _callback = callback;
        }

        private void GenerateRendererMovePoint(GameObject gameObject)
        {
            Vector3[] allDirections = new Vector3[_movePointsByCircle];

            float angleBetweenTwoCandidatePos = Mathf.PI * 2 / _movePointsByCircle;

            for (var (i, currentAngle) = (0, (float)0);
                 i < _movePointsByCircle;
                 i++, currentAngle += angleBetweenTwoCandidatePos)
            {
                allDirections[i] = new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle)).normalized;
            }

            MovePoints = new MovePoint[_circleRadius.Length * _movePointsByCircle];
            for (_i = 0; _i < _circleRadius.Length; _i++)
            {
                for (_j = 0; _j < _movePointsByCircle; _j++)
                {
                    var pos = allDirections[_j] * _circleRadius[_i];
                    var rendererMovePoint = Object.Instantiate(gameObject, pos, Quaternion.identity);
                    MovePoints[_i * _movePointsByCircle + _j] =
                        new MovePoint(rendererMovePoint.GetComponent<MeshRenderer>(), pos);
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

            for (_i = _movePointsByCircle; _i < MovePoints.Length - 1; _i++)
            {
                MovePoints[_i].NeighborTopIndex = (_i - _movePointsByCircle);
            }

            for (_i = 0; _i < MovePoints.Length - _movePointsByCircle; _i++)
            {
                MovePoints[_i].NeighborDownIndex = (_i + _movePointsByCircle);
            }

            for (_i = 0; _i < _circleRadius.Length; _i++)
            {
                int currentCircleIndex = _i * _movePointsByCircle;
                for (_j = 1; _j < _movePointsByCircle; _j++)
                {
                    int currentIndex = currentCircleIndex + _j;
                    MovePoints[currentIndex].NeighborLeftIndex = currentIndex - 1;
                }

                for (_j = 0; _j < _movePointsByCircle - 1; _j++)
                {
                    int currentIndex = currentCircleIndex + _j;
                    MovePoints[currentIndex].NeighborRightIndex = (currentIndex + 1);
                }

                MovePoints[currentCircleIndex].NeighborLeftIndex = (currentCircleIndex + _movePointsByCircle - 1);
                MovePoints[currentCircleIndex + _movePointsByCircle - 1].NeighborRightIndex = (currentCircleIndex);
            }
        }

        public bool CheckIfMovePointInIsLastCircle(int index)
        {
            return index >= _movePointsByCircle * (_circleRadius.Length - 1);
        }
    }
}