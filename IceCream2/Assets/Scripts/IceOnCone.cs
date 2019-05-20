using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class IceOnCone : MonoBehaviour
    {
        public GameObject cone;
        public GameObject choc;
        public GameObject strawb;
        public GameObject van;
        public Vector3 poss;
        private GameObject v1;
        private GameObject c1;
        private GameObject s1;
        // Start is called before the first frame update
        void Start()
        {
            poss = cone.transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name =="VanScoop") {
                v1 = Instantiate(van);    //, new Vector3(poss)
                v1.SetActive(true);
                cone.SetActive(false);
            } else if (other.gameObject.name == "ChocScoop") {
                c1 = Instantiate(choc);
                c1.SetActive(true);
                cone.SetActive(false);
            } else if (other.gameObject.name == "StrawbScoop") {
                s1 = Instantiate(strawb);
                s1.SetActive(true);
                cone.SetActive(false);
            }
        }
    }
}
