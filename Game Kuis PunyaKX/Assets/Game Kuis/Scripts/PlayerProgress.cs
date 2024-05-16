using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress")]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progressLevel;

    }

    [SerializeField]
    private string _filename = "contoh.txt";

    [SerializeField]
    private string _startingLevelPackname = string.Empty;

    public MainData progressData = new MainData();

    public void SimpanProgress()
    {
        // sampel data
        //progressData.koin = 200;
        
        //if (progressData.progressLevel == null)
        //    progressData.progressLevel = new();

        //progressData.progressLevel.Add("Level Pack 1", 3);
        //progressData.progressLevel.Add("Level Pack 3", 5);

        // simpan Starting Data saat objek Dictionary tidak ada ketika dimuat/ di load
        if (progressData.progressLevel == null)  // cek dulu, apakah objek Dictionary sudah pernah dibuat , kalo udah dibuat berarti bukan null
        {
            progressData.progressLevel = new();  // karena null ( belum pernah dibuat) , maka buat baru
            progressData.koin = 0;              // inisial koin = 0 
            progressData.progressLevel.Add(_startingLevelPackname, 1); // inisial level pertama
        }

        // informasi penyimpanan data
#if UNITY_EDITOR
        string directory = Application.dataPath + "/Temporary/";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/ProgresLokal/";
#endif

        var path = directory + "/" + _filename;

        // membuat Directory temporary
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been created : " + directory);
        }

        // membuat file baru
        if (File.Exists(path))
        {
            File.Create(path).Dispose();

            Debug.Log("File created : " + path);
        }

        //var konten = $"{progressData.koin}\n";

        // foreach ( var i in progressData.progressLevel)
        // {
        //     konten += $"{i.Key} {i.Value}\n";
        // }
        //        File.WriteAllText(path, konten);

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        fileStream.Flush();

        // menyimpan data ke dalam file menggunakan binary writer

        var writer = new BinaryWriter(fileStream);

        writer.Write(progressData.koin);

        foreach ( var i in progressData.progressLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        //putuskan aliran memori dengan File
        writer.Dispose();


        Debug.Log($"{_filename} Berhasil disimpan");


    }

    public bool MuatProgress()
    {
        // informasi penyimpanan data
       // string directory = Application.dataPath + "/Temporary";

#if UNITY_EDITOR
        string directory = Application.dataPath + "/Temporary/";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/ProgresLokal/";
#endif

        string path = directory + "/" + _filename;

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try 
        {
            var reader = new BinaryReader(fileStream);

            try 
            {
                progressData.koin = reader.ReadInt32();

                if (progressData.progressLevel == null)
                    progressData.progressLevel = new();  // new() = new Dictionary<string, int>()

                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();

                    progressData.progressLevel.Add(namaLevelPack, levelKe);

                    Debug.Log($"{namaLevelPack} : {levelKe}");

                }


            }
            catch (System.Exception e)
            { 
                Debug.Log($"Error : Terjadi kesalahan saat MEMUAT progres binari.\n{e.Message}");

                reader.Dispose();

                fileStream.Dispose();

                return false;
            }
            
            fileStream.Dispose();

            Debug.Log($"{progressData.koin}; {progressData.progressLevel.Count}");

            return true;
        }
        catch (System.Exception e)
        {
            fileStream.Dispose();

            Debug.Log($" Error : Terjadi kesalahan saat memuat progres \n{e.Message}");
            
            return false;
        }
        
    }

}
