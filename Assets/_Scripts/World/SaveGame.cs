using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public static class SaveGame  {
    //not worki

    public static List<float> AllTowersXPos;
    public static List<float> AllTowersZPos;
    public static List<float> AllTowersYRot;
    public static List<string> AllTowersType;
    public static List<int> AllTowersRecipe;


    public static void SaveGameButton()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        
        AllTowersXPos = GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersXPos;
        AllTowersZPos = GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersZPos;
        AllTowersYRot = GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersYRot;
        AllTowersType = GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersType;
        AllTowersRecipe = GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersRecipe;


        bf.Serialize(stream, AllTowersXPos);
        bf.Serialize(stream, AllTowersZPos);
        bf.Serialize(stream, AllTowersYRot);
        bf.Serialize(stream, AllTowersType);
        bf.Serialize(stream, AllTowersRecipe);



        stream.Close();

    }

    public static void LoadGameButton(){

        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + Path.DirectorySeparatorChar + "/player.sav", FileMode.OpenOrCreate);

            List<float> AllTowersXPos = bf.Deserialize(stream) as List<float>;
            List<float> AllTowersZPos = bf.Deserialize(stream) as List<float>;
            List<float> AllTowersYRot = bf.Deserialize(stream) as List<float>;
            List<string> AllTowersType = bf.Deserialize(stream) as List<string>;
            List<int> AllTowersRecipe = bf.Deserialize(stream) as List<int>;

            stream.Close();

            GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersXPos = AllTowersXPos;
            GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersZPos = AllTowersZPos;
            GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersYRot = AllTowersYRot;
            GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersType = AllTowersType;
            GameObject.FindGameObjectWithTag("MACHINEPARENT").GetComponent<Savegame2>().AllTowersRecipe = AllTowersRecipe;

        }
     
    }
    

}
