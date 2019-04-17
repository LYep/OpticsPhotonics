using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

//Creating a file on main menu scene to save tutorial states so user can skip
public class SaveData : MonoBehaviour {
    public static SaveData dataFile;
    public bool MainMenuTutorialActive;
    public bool SandBoxTutorialActive;
    public int BuildNumber;

   // public List<Transform> RaysToLoad;
   // public List<Transform> LensToLoad;
   // public List<string> TypeLensToLoad;

    public List<SerializeTransformRay> RaysToLoad;
    public List<SerializeTransformLens> LensToLoad;

    void Awake () {
		if(dataFile == null)
        {
            DontDestroyOnLoad(gameObject);
            dataFile = this;
        }
        else if(dataFile != this)
        {
            Destroy(gameObject);
        }
	}
    //not sure if i instantiate the bools here on start
    
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/tutorialInfo.REU");

        TutorialData data = new TutorialData();
        data.MainMenuTutorial = MainMenuTutorialActive;
        data.SandBoxTutorial = SandBoxTutorialActive;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/tutorialInfo.REU"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/tutorialInfo.REU", FileMode.Open);
            TutorialData data = (TutorialData)bf.Deserialize(file);
            file.Close();

            MainMenuTutorialActive = data.MainMenuTutorial;
            SandBoxTutorialActive = data.SandBoxTutorial;
        }
    }
   
    public void SaveSandbox(List<SerializeTransformRay> Rays, List<SerializeTransformLens> Lens)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SceneInfo.REU");
        print(Application.persistentDataPath);

        SceneData data = new SceneData();
        data.Rays = Rays;
        data.Lens = Lens;

        bf.Serialize(file, data);
        file.Close();

    }
    public void LoadSandbox()
    {
        if (File.Exists(Application.persistentDataPath + "/SceneInfo.REU"))
        {
            print("loading scene");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SceneInfo.REU", FileMode.Open);
            SceneData data = (SceneData)bf.Deserialize(file);
            file.Close();

            RaysToLoad = data.Rays;
            LensToLoad = data.Lens;
        }
        else
        {
            print("didnt work");
        }
    }
    /*public void SaveSandbox(List<Transform> Rays, List<Transform> Lens, List<string> TypeLens)
   {
       BinaryFormatter bf = new BinaryFormatter();
       FileStream file = File.Create(Application.persistentDataPath + "/SceneInfo.REU");
       print(Application.persistentDataPath);

       SceneData data = new SceneData();
       data.Number = BuildNumber;
       data.Rays = Rays;
       data.Lens = Lens;
       data.TypeLens = TypeLens;

       bf.Serialize(file, data);
       file.Close();

   }*/
}
[System.Serializable]
class TutorialData
{
    public bool MainMenuTutorial;
    public bool SandBoxTutorial;
}

[System.Serializable]
class SceneData
{
    public List<SerializeTransformRay> Rays;
    public List<SerializeTransformLens> Lens;
}
/*[System.Serializable]
class SceneData
{
    public bool Tester;
    public int Number;
    public List<Transform> Rays;
    public List<Transform> Lens;
    public List<string> TypeLens;
}*/
