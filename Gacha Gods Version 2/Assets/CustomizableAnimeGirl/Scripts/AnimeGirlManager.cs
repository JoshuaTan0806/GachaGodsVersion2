/*
Script to manage 3D model of AnimeGirl
Replace meshes and materials, resize chest bones, etc.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CustomizableAnimeGirl
{
    public enum MESH_TYPE
    {
        HAIR_FRONT,
        HAIR_SIDE,
        HAIR_BACK,
        UPPER_BODY,
        LOWER_BODY,
        UNDERWEAR,
        UNDERSHIRT,
        SHOES,
        GLOVES,
        HEADGEAR,
    }
    public enum MATERIAL_TYPE
    {
        EYE,
        EYEBROWS,
        EYELASHES,
        LEGS,
        UBODY,
        LBODY,
        FACE
    }
    public class AnimeGirlManager : MonoBehaviour
    {

        public Transform breastL;
        public Transform breastR;
        public Transform hips;
        public Transform uBody;
        public Transform lBody;
        public Transform face;
        public Transform hairFront;
        public Transform hairSide;
        public Transform hairBack;
        public Transform lowerBody;
        public Transform upperBody;
        public Transform underwear;
        public Transform undershirt;
        public Transform shoes;
        public Transform gloves;
        public Transform headgear;

        public Transform legs;
        public Transform eyebrows;
        public Transform eyelashes;
        public Transform eye;
        public List<Mesh> uBodyMeshes;
        public List<Mesh> lBodyMeshes;
        // Start is called before the first frame update
        void Start() { }

        // Update is called once per frame
        void Update()
        {

        }

#if UNITY_EDITOR
        // Method to automatically detect each Transform
        [ContextMenu("AutoDetectGameObjects")]
        public void AutoDetectGameObjects()
        {
            breastL = transform.Find("bone/hips/spine/chest/upperchest/breast.L");
            breastR = transform.Find("bone/hips/spine/chest/upperchest/breast.R");
            hips = transform.Find("bone/hips");
            uBody = transform.Find("UBody");
            lBody = findTransform("LBody");
            face = findTransform("Face");
            legs = findTransform("Legs");
            eyebrows = findTransform("Eyebrows");
            eyelashes = findTransform("Eyelashes");
            eye = findTransform("Eye");
            hairFront = findTransform("Hair_Front");
            hairSide = findTransform("Hair_Side");
            hairBack = findTransform("Hair_Back");
            lowerBody = findTransform("LowerBody");
            upperBody = findTransform("UpperBody");
            underwear = findTransform("Underwear");
            undershirt = findTransform("Undershirt");
            shoes = findTransform("Shoes");
            gloves = findTransform("Gloves");
            headgear = findTransform("Headgear");

        }

        private Transform findTransform(string name)
        {
            Transform t = transform.Find(name);
            if (t == null)
            {
                t = Instantiate(uBody, transform);
                t.name = name;
                SkinnedMeshRenderer skinnedMeshRenderer = t.GetComponent<SkinnedMeshRenderer>();
                skinnedMeshRenderer.sharedMesh = null;
                skinnedMeshRenderer.material = null;
            }
            return t;
        }
#endif

        // Change breast size
        public void setBreastsSize(float size)
        {
            if (breastL != null) breastL.localScale = new Vector3(size, size, size);
            if (breastR != null) breastR.localScale = new Vector3(size, size, size);
        }

        private SkinnedMeshRenderer getRenderer(MESH_TYPE meshType)
        {
            SkinnedMeshRenderer smr = null;
            switch (meshType)
            {
                case MESH_TYPE.HAIR_FRONT:
                    smr = hairFront.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.HAIR_SIDE:
                    smr = hairSide.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.HAIR_BACK:
                    smr = hairBack.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.UPPER_BODY:
                    smr = upperBody.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.LOWER_BODY:
                    smr = lowerBody.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.UNDERWEAR:
                    smr = underwear.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.UNDERSHIRT:
                    smr = undershirt.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.SHOES:
                    smr = shoes.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.GLOVES:
                    smr = gloves.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MESH_TYPE.HEADGEAR:
                    smr = headgear.GetComponent<SkinnedMeshRenderer>();
                    break;
            }
            return smr;
        }

        private SkinnedMeshRenderer getRenderer(MATERIAL_TYPE materialType)
        {

            SkinnedMeshRenderer smr = null;
            switch (materialType)
            {
                case MATERIAL_TYPE.EYE:
                    smr = eye.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MATERIAL_TYPE.EYEBROWS:
                    smr = eyebrows.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MATERIAL_TYPE.EYELASHES:
                    smr = eyelashes.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MATERIAL_TYPE.LEGS:
                    smr = legs.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MATERIAL_TYPE.UBODY:
                    smr = uBody.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MATERIAL_TYPE.LBODY:
                    smr = lBody.GetComponent<SkinnedMeshRenderer>();
                    break;
                case MATERIAL_TYPE.FACE:
                    smr = face.GetComponent<SkinnedMeshRenderer>();
                    break;
            }
            return smr;

        }

        // Change hairstyle and equipment mesh
        public void setMesh(Mesh mesh, Material[] materials, MESH_TYPE meshType)
        {
            SkinnedMeshRenderer smr = getRenderer(meshType);
            if (smr != null)
            {
                smr.sharedMesh = mesh;
                smr.materials = materials;
            }
        }
        // Change the material of eyes, eyebrows, eyelashes and legs
        public void setMaterial(Material material, MATERIAL_TYPE materialType)
        {
            SkinnedMeshRenderer smr = getRenderer(materialType);
            if (smr != null)
            {
                if (materialType == MATERIAL_TYPE.FACE)
                {
                    Material[] materialsCopy = smr.sharedMaterials;
                    materialsCopy[0] = material;
                    smr.sharedMaterials = materialsCopy;
                }
                else
                {
                    smr.material = material;
                }
            }
        }

        public void setMesh(Mesh mesh, Material material, MESH_TYPE meshType)
        {
            Material[] materials = new Material[1];
            materials[0] = material;
            setMesh(mesh, materials, meshType);
        }

        // Change the mesh of the body
        // Set a body with transparent joints to prevent clothes from penetrating
        // (0: Normal 1: Shoulder transparent 2: Arm transparent)
        public void setBodyMesh(MATERIAL_TYPE materialType, int bodyType, Color color)
        {
            if (materialType == MATERIAL_TYPE.UBODY)
            {
                if (uBodyMeshes.Count > 0)
                {
                    SkinnedMeshRenderer bodyMesh = getRenderer(MATERIAL_TYPE.UBODY);
                    int bodyid = bodyType;
                    if (bodyid > 2) bodyid = 2;
                    bodyMesh.sharedMesh = uBodyMeshes[bodyid];
                    bodyMesh.material.color = color;
                    if (bodyType == 0)
                    {
                        undershirt.gameObject.SetActive(true);
                        lowerBody.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (bodyType == 3)
                        {
                            lowerBody.gameObject.SetActive(false);
                        }
                        else
                        {
                            lowerBody.gameObject.SetActive(true);

                        }
                        undershirt.gameObject.SetActive(false);
                    }
                }
            }
            else if (materialType == MATERIAL_TYPE.LBODY)
            {
                if (lBodyMeshes.Count > 0)
                {
                    SkinnedMeshRenderer bodyMesh = getRenderer(MATERIAL_TYPE.LBODY);
                    int bodyid = bodyType;
                    if (bodyid > 2) bodyid = 2;
                    bodyMesh.sharedMesh = lBodyMeshes[bodyid];
                    bodyMesh.material.color = color;
                    if (bodyType == 0)
                    {
                        underwear.gameObject.SetActive(true);
                    }
                    else
                    {
                        underwear.gameObject.SetActive(false);
                    }
                }
            }
            SkinnedMeshRenderer faceMesh = getRenderer(MATERIAL_TYPE.FACE);
            faceMesh.material.color = color;
            int faceLength = faceMesh.materials.Length;
            if (faceLength >= 5)
            {
                faceMesh.materials[3].color = color;
                faceMesh.materials[4].color = color;
            }
        }

        // Change hairstyle and equipment mesh color
        public void setMeshColor(MESH_TYPE meshType, Color color)
        {
            SkinnedMeshRenderer smr = getRenderer(meshType);
            if (smr != null)
            {
                smr.material.color = color;
                smr.material.SetColor("_BaseColor", color);
            }
        }

        // Change the color of the material for the eyes, eyebrows, eyelashes, and legs
        public void setMaterialColor(MATERIAL_TYPE materialType, Color color)
        {
            SkinnedMeshRenderer smr = getRenderer(materialType);
            if (smr != null)
            {
                smr.material.color = color;
                smr.material.SetColor("_BaseColor", color);
            }
        }

        // Get hairstyle, equipment mesh color
        public Color getMeshColor(MESH_TYPE meshType)
        {
            SkinnedMeshRenderer smr = getRenderer(meshType);
            return smr.material.color;
        }

        // Get the color of the material for the eyes, eyebrows, eyelashes and legs
        public Color getMaterialColor(MATERIAL_TYPE materialType)
        {
            SkinnedMeshRenderer smr = getRenderer(materialType);
            return smr.material.color;
        }

    }
}