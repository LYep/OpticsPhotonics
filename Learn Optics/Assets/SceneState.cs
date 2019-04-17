using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneState : MonoBehaviour {
    public bool SavingScene;
    public GameObject ConcaveLens, ConvexLens, ConcaveMirror, ConvexMirror, LightEmitter;
    private int NumOfLens;
    private int NumOfRays;
    List<Vector3> PosOfLens;
    List<Vector3> PosOfRays;
    List<Transform> TransformOfLens;
    List<Transform> TransformOfRays;
    List<SerializeTransformRay> RaySave;
    List<SerializeTransformLens> LensSave;
    List<string> TypeOfLens;
    Transform RootScene;
    Transform InitialPlayer;
    GameObject CurrentObj;

    void Start () {
        SavingScene = false;
        //PosOfLens = new List<Vector3>();
        //  PosOfRays = new List<Vector3>();
        RaySave = new List<SerializeTransformRay>();
        LensSave = new List<SerializeTransformLens>();
        TransformOfLens = new List<Transform>();
        TransformOfRays = new List<Transform>();
        TypeOfLens = new List<string>();
        RootScene = GameObject.Find("Root").transform;
        InitialPlayer = GameObject.Find("Player").transform;
    }

   public void ClosePanel()
    {
        gameObject.GetComponent<Animator>().SetTrigger("PopOut");
    }
    public void CloseConfirmPanel()
    {
        gameObject.transform.GetChild(7).GetComponent<Animator>().SetTrigger("PopOut");
    }
    public void SaveOrLoad()
    {
        if (SavingScene)
        {
            SaveData.dataFile.BuildNumber = SceneManager.GetActiveScene().buildIndex;
            
            print("Build number is " + SceneManager.GetActiveScene().buildIndex);
            NumOfLens = 0;
            NumOfRays = 0;
            foreach (GameObject lightObj in GameObject.FindGameObjectsWithTag("LightEmitter"))
            {
                NumOfRays += 1;
                //----PosOfRays.Add(lightObj.transform.localPosition);
                //TransformOfRays.Add(lightObj.transform);
                RaySave.Add(new SerializeTransformRay(lightObj.transform));
                print(RaySave.Count + "num of ray saves");
            }
            foreach (GameObject LensObj in GameObject.FindGameObjectsWithTag("OpticalElement"))
            {
                NumOfLens += 1;
                // ----PosOfLens.Add(LensObj.transform.localPosition);
                //TransformOfLens.Add(LensObj.transform);
                //TypeOfLens.Add(LensObj.name);
                LensSave.Add(new SerializeTransformLens(LensObj.transform));
            }
            SaveData.dataFile.SaveSandbox(RaySave, LensSave);
            
        }
        else
        {
            //SceneManager.LoadScene(SaveData.dataFile.LoadSandbox()); doesn't work with build number
            print("loading");
            SaveData.dataFile.LoadSandbox();
            List<SerializeTransformRay> tempRays = SaveData.dataFile.RaysToLoad;
            for (int i = 0; i < tempRays.Count; i++)
            {
                print("adding ray num: " + i);
                CurrentObj = Instantiate(LightEmitter, new Vector3(0, 0),
            Quaternion.identity, InitialPlayer.transform);
                Vector3 newPos = new Vector3(tempRays[i].posX, tempRays[i].posY, tempRays[i].posZ);
                Quaternion newRotation = new Quaternion(tempRays[i].rotX, tempRays[i].rotY, tempRays[i].rotZ, 1);
                CurrentObj.transform.localPosition = newPos;
                CurrentObj.transform.rotation = newRotation;
            }
        }
        ClosePanel();
    }
}
[System.Serializable]
public class SerializeTransformRay
{
   // public Vector3 position;
   // public Quaternion rotation;
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ;

    public SerializeTransformRay(Transform item)
    {
        //this.position = item.localPosition;
        this.posX = item.localPosition.x;
        this.posY = item.localPosition.y;
        this.posZ = item.localPosition.z;
        this.rotX = item.rotation.x;
        this.rotY = item.rotation.y;
        this.rotZ = item.rotation.z;
        //this.rotation = item.rotation;
    }
}
[System.Serializable]
public class SerializeTransformLens
{
    //public Vector3 position;
   // public Vector3 scale;
    public float posX, posY, posZ;
    public float scaleX, scaleY, scaleZ;
    public string TypeLens;

    public SerializeTransformLens(Transform item)
    {
        //this.position = item.localPosition;
        //this.scale = item.localScale;
        this.posX = item.localPosition.x;
        this.posY = item.localPosition.y;
        this.posZ = item.localPosition.z;
        this.scaleX = item.localScale.x;
        this.scaleY = item.localScale.y;
        this.scaleZ = item.localScale.z;
        this.TypeLens = item.name;
    }
}