using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class ComeOutLabel : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPoint()
        {
            gameObject.SetActive(false);
        }

        public void ClearPoint()
        {
            gameObject.SetActive(true);
        }
    }
}