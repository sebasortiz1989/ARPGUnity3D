using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.SavingC
{
    public class SavingSystemC : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string _path = GetPathFromSaveFile(saveFile);
            print("Saving to " + _path);
            using (FileStream stream = File.Open(_path, FileMode.Create)) //This will call close automatically
            {
                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 position = new SerializableVector3(playerTransform.position);
                formatter.Serialize(stream, position);
            }          
        }

        public void Load(string saveFile)
        {
            string _path = GetPathFromSaveFile(saveFile);
            print("Loading to " + _path);
            using (FileStream stream = File.Open(_path, FileMode.Open)) //This will call close automatically
            {
                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 position = (SerializableVector3)formatter.Deserialize(stream);
                playerTransform.position = position.ToVector();
            }
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        private byte[] SerializeVector(Vector3 _vector)
        {
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(_vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(_vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(_vector.y).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        private Vector3 DeserializeVector(byte[] _buffer)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(_buffer, 0);
            result.y = BitConverter.ToSingle(_buffer, 4);
            result.z = BitConverter.ToSingle(_buffer, 8);
            return result;
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}

