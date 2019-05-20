//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem.Sample
{
    public class TargetHitEffect : MonoBehaviour
    {
        public Collider targetCollider;
        public Vector3 target;
        public GameObject spawnObjectOnCollision;
        public Text scoreText;
        public Text flavourText;
        public int Score;
        public GameObject cone;
        //public bool colorSpawnedObject = true;

        public bool destroyOnTargetCollision = true;
        private void Start()
        {
            //flavourText.text = ("Vanilla");
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider == targetCollider)
            {
                int ranFlav = Random.Range(0, 2);
                Debug.Log(this.name);
                if (flavourText.text == "Vanilla" && this.name == "VanillaCone(Clone)") {
                    Debug.Log("Vanilla");
                    Score = int.Parse(scoreText.text);
                    Score += 1;
                    scoreText.text = (Score).ToString("0");

                } else if (flavourText.text == "Chocolate" && this.name == "Chocolatecone(Clone)") {
                    Debug.Log("Vanilla");
                    Score = int.Parse(scoreText.text);
                    Score += 1;
                    scoreText.text = (Score).ToString("0");
                } else if (flavourText.text == "Strawberry" && this.gameObject.name == "StrawbCone(Clone)") {
                    Debug.Log("Vanilla");
                    Score = int.Parse(scoreText.text);
                    Score += 1;
                    scoreText.text = (Score).ToString("0");
                }
                cone.SetActive(true);
                ContactPoint contact = collision.contacts[0];
                RaycastHit hit;

                if (ranFlav == 0) { flavourText.text = "Vanilla"; }
                if (ranFlav == 1) { flavourText.text = "Chocolate"; }
                if (ranFlav == 2) { flavourText.text = "Strawberry"; }
                float backTrackLength = 1f;
                Ray ray = new Ray(contact.point - (-contact.normal * backTrackLength), -contact.normal);
                if (collision.collider.Raycast(ray, out hit, 2))
                {
                    //GameObject spawned = GameObject.Instantiate(spawnObjectOnCollision);
                    //spawned.transform.position = target;
                    /*
                     if (colorSpawnedObject)
                    {
                        Renderer renderer = collision.gameObject.GetComponent<Renderer>();
                        Texture2D tex = (Texture2D)renderer.material.mainTexture;
                        Color color = tex.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);

                        if (color.r > 0.7f && color.g > 0.7f && color.b < 0.7f)
                            color = Color.yellow;
                        else if (Mathf.Max(color.r, color.g, color.b) == color.r)
                            color = Color.red;
                        else if (Mathf.Max(color.r, color.g, color.b) == color.g)
                            color = Color.green;
                        else
                            color = Color.yellow;

                        color *= 15f;

                        GameObject spawned = GameObject.Instantiate(spawnObjectOnCollision);
                        spawned.transform.position = contact.point;
                        spawned.transform.forward = ray.direction;

                        Renderer[] spawnedRenderers = spawned.GetComponentsInChildren<Renderer>();
                        for (int rendererIndex = 0; rendererIndex < spawnedRenderers.Length; rendererIndex++)
                        {
                            Renderer spawnedRenderer = spawnedRenderers[rendererIndex];
                            spawnedRenderer.material.color = color;
                            if (spawnedRenderer.material.HasProperty("_EmissionColor"))
                            {
                                spawnedRenderer.material.SetColor("_EmissionColor", color);
                            }
                        }
                    }
                    */
                }
                //Debug.DrawRay(ray.origin, ray.direction, Color.cyan, 5, true);

                if (destroyOnTargetCollision)
                    Destroy(this.gameObject);
            }
        }
    }
}