using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/*THIS SAVING SYSTEM IS DEPRECATED AND IS VULNERABLE FOR ANY ONLINE GAMES, WILL UPDATE TO A JSON BASED SAVE SYSTEM IF I RETURN TO THIS PROJECT IN THE FUTURE*/

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        //loads last scene saved in
        public IEnumerator LoadLastScene(string saveFile)
        {
            //get dictionary from save file
            Dictionary<string, object> state = LoadFile(saveFile);

            //returns index for current scene
            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            //if last scene was saved to file, set buildIndex to that scene
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)state["lastSceneBuildIndex"];
            }

            //load scene indicated by index value
            yield return SceneManager.LoadSceneAsync(buildIndex);

            //load from file 
            RestoreState(state);
        }

        //Gets dictionary of save state, captures current values for state, saves to file
        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        //loads state from save file
        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        //deletes save file at file path
        public void Delete(string saveFile)
        {
            File.Delete(GetPathFromSaveFile(saveFile));
        }

        //saves to file with filestream
        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);

            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, state);
            }
        }

        //Creates Dictionary if none, otherwise open saveFile dictionary and deserializes
        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        //Captures state to dictionary for each in with SaveableEntity, saves active scene to be able to load it
        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIndentifier()] = saveable.CaptureState();
            }

            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        //loads state from dictionary
        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id =saveable.GetUniqueIndentifier();

                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }

        //gets save file from file path
        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
