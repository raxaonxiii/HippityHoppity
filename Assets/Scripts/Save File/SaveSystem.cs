using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    const string fileType = "/hip.hop";
    public static void Save( GameManager gMan )
    {
        BinaryFormatter formatter = new BinaryFormatter( );
        string path = Application.persistentDataPath + fileType;
        FileStream stream = new FileStream( path, FileMode.Create );

        SaveData data = new SaveData( gMan );
        
        formatter.Serialize( stream, data );
        stream.Close( );
        Debug.Log( "Saving: " + path );
    }
    
    public static SaveData LoadSave( )
    {
        string path = Application.persistentDataPath + fileType;
        if( File.Exists( path ) )
        {
            BinaryFormatter formatter = new BinaryFormatter( );
            FileStream stream = new FileStream( path, FileMode.Open );
            SaveData data = formatter.Deserialize( stream ) as SaveData;
            stream.Close( );
            Debug.Log( "Load Score: " + path );
            return data;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter( );
            FileStream stream = new FileStream( path, FileMode.Create );
            SaveData data = new SaveData( );
            formatter.Serialize( stream, data );
            stream.Close( );
            Debug.Log( "Create Save for Score: " + path );
            return data;
        }
    }
}