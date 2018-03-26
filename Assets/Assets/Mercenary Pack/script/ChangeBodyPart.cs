using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeBodyPart : MonoBehaviour {

	private SkinnedMeshRenderer oldSmr = null;
	private SkinnedMeshRenderer newSmr = null;

	private Object oldObj = null;
	private Object newObj = null;

	private GameObject oldInstance = null;
	private GameObject newInstance = null;

	public GameObject pantPart;   		//partIndex 1
	public GameObject backpackPart;		//partIndex 2
	public GameObject headPart;			//partIndex 3
	public GameObject clothesPart;		//partIndex 4


	PlayerController pController;


	//private const int COMBINE_TEXTURE_MAX = 2048;
	//private const string COMBINE_DIFFUSE_TEXTURE = "_MainTex";


	void Start () {

		pController = gameObject.GetComponent<PlayerController> ();
	}
			

	public void ChangePart(int partIndex, string partName , bool bVisible)
	{
		newObj = Resources.Load("PartsFolder/" + partName);
		newInstance = Instantiate(newObj) as GameObject;

		if (partIndex == 1) {
			oldSmr = pantPart.GetComponent<SkinnedMeshRenderer> ();
		}
		if (partIndex == 2) {
			oldSmr = backpackPart.GetComponent<SkinnedMeshRenderer> ();
		}
		if (partIndex == 3) {
			oldSmr = headPart.GetComponent<SkinnedMeshRenderer> ();
		}
		if (partIndex == 4) {
			oldSmr = clothesPart.GetComponent<SkinnedMeshRenderer> ();
		}

		oldSmr.enabled = bVisible;
		pController.RiflePlacement ();


		newSmr = newInstance.GetComponentInChildren<SkinnedMeshRenderer>();

		Transform[] oldBones = gameObject.GetComponentsInChildren<Transform>();
		Transform[] newBones = newSmr.bones;

		List<Transform> bones = new List<Transform>();
		foreach (Transform bone in newBones)
		{
			foreach (Transform oldBone in oldBones)
			{
				if (bone != null && oldBone != null)
				{
					if (bone.name != oldBone.name)
					{
						continue;
					}
					bones.Add(oldBone);
				}
			}
		}

		oldSmr.bones = bones.ToArray();
		oldSmr.sharedMesh = newSmr.sharedMesh;
		oldSmr.sharedMaterial = newSmr.sharedMaterial;
	
		GameObject.DestroyImmediate(newInstance);
		GameObject.DestroyImmediate(newSmr);

		//CombineObject ();

	}





	//public void CombineObject ( ){
	//
	//	SkinnedMeshRenderer[] meshes = gameObject.GetComponentsInChildren<SkinnedMeshRenderer> ();
	//
	//
	//	// Fetch all bones of the skeleton
	//	List<Transform> transforms = new List<Transform>();
	//	transforms.AddRange(gameObject.GetComponentsInChildren<Transform>(true));
	//
	//	List<Material> materials = new List<Material>();//the list of materials
	//	List<CombineInstance> combineInstances = new List<CombineInstance>();//the list of meshes
	//	List<Transform> bones = new List<Transform>();//the list of bones
	//
	//	// Below informations only are used for merge materilas(bool combine = true)
	//	List<Vector2[]> oldUV = null;
	//	Material newMaterial = null;
	//	Texture2D newDiffuseTex = null;
	//
	//	// Collect information from meshes
	//	for (int i = 0; i < meshes.Length; i ++)
	//	{
	//		SkinnedMeshRenderer smr = meshes[i];
	//		materials.AddRange(smr.materials); // Collect materials
	//		// Collect meshes
	//		for (int sub = 0; sub < smr.sharedMesh.subMeshCount; sub++)
	//		{
	//			CombineInstance ci = new CombineInstance();
	//			ci.mesh = smr.sharedMesh;
	//			ci.subMeshIndex = sub;
	//			combineInstances.Add(ci);
	//		}
	//		// Collect bones
	//		for (int j = 0 ; j < smr.bones.Length; j ++)
	//		{
	//			int tBase = 0;
	//			for (tBase = 0; tBase < transforms.Count; tBase ++)
	//			{
	//				if (smr.bones[j].name.Equals(transforms[tBase].name))
	//				{
	//					bones.Add(transforms[tBase]);
	//					break;
	//				}
	//			}
	//		}
	//	}
	//
	//	// merge materials
	//	if (true)
	//	{
	//		newMaterial = new Material (Shader.Find ("Mobile/Diffuse"));
	//		oldUV = new List<Vector2[]>();
	//		// merge the texture
	//		List<Texture2D> Textures = new List<Texture2D>();
	//		for (int i = 0; i < materials.Count; i++)
	//		{
	//			Textures.Add(materials[i].GetTexture(COMBINE_DIFFUSE_TEXTURE) as Texture2D);
	//		}
	//
	//		newDiffuseTex = new Texture2D(COMBINE_TEXTURE_MAX, COMBINE_TEXTURE_MAX, TextureFormat.RGBA32, true);
	//		Rect[] uvs = newDiffuseTex.PackTextures(Textures.ToArray(), 0);
	//		newMaterial.mainTexture = newDiffuseTex;
	//
	//		// reset uv
	//		Vector2[] uva, uvb;
	//		for (int j = 0; j < combineInstances.Count; j++)
	//		{
	//			uva = (Vector2[])(combineInstances[j].mesh.uv);
	//			uvb = new Vector2[uva.Length];
	//			for (int k = 0; k < uva.Length; k++)
	//			{
	//				uvb[k] = new Vector2((uva[k].x * uvs[j].width) + uvs[j].x, (uva[k].y * uvs[j].height) + uvs[j].y);
	//			}
	//			oldUV.Add(combineInstances[j].mesh.uv);
	//			combineInstances[j].mesh.uv = uvb;
	//		}
	//	}
	//
	//	// Create a new SkinnedMeshRenderer
	//	SkinnedMeshRenderer oldSKinned = gameObject.GetComponent<SkinnedMeshRenderer> ();
	//	if (oldSKinned != null) {
	//
	//		GameObject.DestroyImmediate (oldSKinned);
	//	}
	//	SkinnedMeshRenderer r = gameObject.AddComponent<SkinnedMeshRenderer>();
	//	r.sharedMesh = new Mesh();
	//	r.sharedMesh.CombineMeshes(combineInstances.ToArray());// Combine meshes
	//	r.bones = bones.ToArray();// Use new bones
	//	if (true)
	//	{
	//		r.material = newMaterial;
	//		for (int i = 0 ; i < combineInstances.Count ; i ++)
	//		{
	//			combineInstances[i].mesh.uv = oldUV[i];
	//		}
	//	}else
	//	{
	//		r.materials = materials.ToArray();
	//	}
	//}
}
