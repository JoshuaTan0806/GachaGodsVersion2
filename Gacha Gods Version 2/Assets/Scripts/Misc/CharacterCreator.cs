using CustomizableAnimeGirl;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public static void SaveCharacter(string id, CustomizeManager.AnimeGirlData body, FaceEditManager.FaceEditData face)
    {
        var filePath = GetFilePath(id);

        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        using (FileStream fs = new FileStream(filePath + ".dat", FileMode.Create))
        {
            bf.Serialize(fs, body);
            bf.Serialize(fs, face);
        }
    }

    public static void SaveOutfit(string id, CustomizeManager.AnimeGirlData body)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(GetFilePath(id)))
            Directory.CreateDirectory(GetFilePath(id));

        using (FileStream fs = new FileStream(GetFilePath(id) + ".dat", FileMode.Create))
        {
            bf.Serialize(fs, body);
        }
    }

    public static (CustomizeManager.AnimeGirlData, FaceEditManager.FaceEditData) LoadCharacter(string id)
    {
        BinaryFormatter bf = new BinaryFormatter();

        CustomizeManager.AnimeGirlData animeGirlData = null;
        FaceEditManager.FaceEditData faceEditData = null;

        using (FileStream fs = new FileStream(GetFilePath(id) + ".dat", FileMode.Open))
        {
            if (fs.Length == 0) throw new IOException("/Save/" + id + ".dat is corrupted. The save file will be initialized.");
            animeGirlData = bf.Deserialize(fs) as CustomizeManager.AnimeGirlData;
            faceEditData = bf.Deserialize(fs) as FaceEditManager.FaceEditData;
        }

        return (animeGirlData, faceEditData);
    }

    public static CustomizeManager.AnimeGirlData LoadOutfit(string id)
    {
        BinaryFormatter bf = new BinaryFormatter();

        CustomizeManager.AnimeGirlData animeGirlData = null;

        using (FileStream fs = new FileStream(GetFilePath(id) + ".dat", FileMode.Open))
        {
            if (fs.Length == 0) throw new IOException("/Save/" + id + ".dat is corrupted. The save file will be initialized.");
            animeGirlData = bf.Deserialize(fs) as CustomizeManager.AnimeGirlData;
        }

        return animeGirlData;
    }

    static string GetFilePath(string name) { return Application.dataPath + "/Save/" + $"{name}"; }
}
