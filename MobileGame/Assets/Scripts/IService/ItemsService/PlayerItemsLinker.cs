using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Service.Items
{
    public class PlayerItemsLinker : MonoBehaviour
    {
        [SerializeField] Transform rightHandPivot;
        [SerializeField] Transform leftHandPivot;
        [SerializeField] private GameObject currentHandItem;

        public void SetHandItem(GameObject gameObject)
        {

        }

    }
}
