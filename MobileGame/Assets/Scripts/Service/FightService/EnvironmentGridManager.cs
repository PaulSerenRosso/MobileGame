using Addressables;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace Service.Fight
{
public class EnvironmentGridManager : MonoBehaviour
{
    [SerializeField]
    private MovePoint[] _movePoints;

    private float[] _circleRadius;
    private int _movePointsByCircle;
    private int i;
    private int j;

    public void SetupGrid(float[] circles, int movePointsByCircle, string rendererAddressableName)
    {
        _circleRadius = circles;
        _movePointsByCircle = movePointsByCircle;
        AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>(rendererAddressableName,
            GenerateRendererMovePoint);
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

        _movePoints = new MovePoint[_circleRadius.Length * _movePointsByCircle + 1];
        for (i = 0; i < _circleRadius.Length; i++)
        {
            for (j = 0; j < _movePointsByCircle; j++)
            {
                var pos = allDirections[j] * _circleRadius[i];
                var rendererMovePoint = Object.Instantiate(gameObject, pos, Quaternion.identity);
                _movePoints[i * _movePointsByCircle + j] =
                    new MovePoint(rendererMovePoint.GetComponent<MeshRenderer>(), pos);
            }
        }

        var rendererMoveCenterPoint = Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity);
        _movePoints[^1] = new MovePoint(rendererMoveCenterPoint.GetComponent<MeshRenderer>(), Vector3.zero);

        Release(gameObject);

        GenerateNeighbors();
    }

    private void GenerateNeighbors()
    {
        for (i = 0; i < _movePointsByCircle; i++)
        {
            _movePoints[i].Neighbors.Add(_movePoints.Length-1);
            _movePoints[^1].Neighbors.Add(i);
        }

        // premier cercle voisin le point center
        // prochain précédent
        // prochain faut le dernier élément du cercle
        // précédent faut pas que cela soit le premier element du cercle
        // haut bas
        // haut il faut faire ça quand c'est le dernier cercle V
        // bas faut le faire pour le premier cercle V
        
        for (i = _movePointsByCircle; i < _movePoints.Length - 1; i++)
        {
            _movePoints[i].Neighbors.Add(i - _movePointsByCircle);
        }

        for (i = 0; i < _movePoints.Length - 1 - _movePointsByCircle; i++)
        {
            _movePoints[i].Neighbors.Add(i + _movePointsByCircle);
        }

        for (i = 0; i < _circleRadius.Length; i++)
        {
            int currentCircleIndex = i * _movePointsByCircle;
            for (j = 1; j < _movePointsByCircle; j++)
            {
                int currentIndex = currentCircleIndex + j;
                _movePoints[currentIndex].Neighbors.Add(currentIndex - 1);
            }

            for (j = 0; j < _movePointsByCircle - 1; j++)
            {
                int currentIndex = currentCircleIndex + j;
                _movePoints[currentIndex].Neighbors.Add(currentIndex + 1);
            }

            _movePoints[currentCircleIndex].Neighbors.Add(currentCircleIndex + _movePointsByCircle - 1);
            _movePoints[currentCircleIndex + _movePointsByCircle - 1].Neighbors.Add(currentCircleIndex);
        }

        foreach (var movePoint in _movePoints)
        {
            foreach (var neighbor in movePoint.Neighbors)
            {
                Debug.Log($"movePoint: {movePoint.Position}, Neighbor: {neighbor}");
            }
        }
    }
}
}
