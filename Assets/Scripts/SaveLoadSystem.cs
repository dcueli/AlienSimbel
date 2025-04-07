using UnityEngine;

public class SaveLoadSystem
{
     #region Variables
    // Ruta del archivo de guardado
    private string savePath = "/savefile.json"; // Ruta del archivo de guardado

    #endregion

    private SaveFile saveFile; // Instancia de la clase SaveFile

    // Método para guardar el juego
    [ContextMenu("Guardar Juego")]
    public void SaveGame(SaveFile saveFile)
    {

        // Convertir el objeto a JSON
        string json = JsonUtility.ToJson(saveFile, true); // true para formato legible (pretty print)

        // escribe fichero en persistent data
        string fullPath = Application.persistentDataPath + savePath;
        System.IO.File.WriteAllText(fullPath, json);
        Debug.Log("Juego guardado en: " + fullPath);
    }

    // Método para leer el archivo de guardado y deserializarlo
    public SaveFile LoadGame()
    {
        // Lee el archivo de guardado
        string fullPath = Application.persistentDataPath + savePath;
        if (System.IO.File.Exists(fullPath))
        {
            string json = System.IO.File.ReadAllText(fullPath);
            saveFile = JsonUtility.FromJson<SaveFile>(json);
            Debug.Log("Juego cargado desde: " + fullPath);
            return saveFile;
        }
        else
        {
            Debug.LogError("No se encontró el archivo de guardado en: " + fullPath);
            return null;
        }
    }

    // EJEMPLO DE USO
/*
public class GameManagerExample : MonoBehaviour
{

    SaveFile saveFile; // Instancia de la clase SaveFile

    SaveLoadSystem saveLoadSystem; // Instancia de la clase SaveLoadSystem

    public void guardarButton()
    {
        // EJEMPLO DE USO DEL SISTEMA DE GUARDADO

        saveLoadSystem = new SaveLoadSystem(); // Inicializa la instancia de SaveLoadSystem
        saveFile = new SaveFile(); // Inicializa la instancia de SaveFile
        saveFile.gameNumber = 1; // Establece el número de juego
        saveFile.playerName = "Jugador1"; // Establece el nombre del jugador
        saveFile.timePlayed = 120.5f; // Establece el tiempo jugado
        saveFile.deathCount = 3; // Establece el número de muertes
        saveFile.stage = 2; // Establece la fase
        saveFile.level = 5; // Establece el nivel

    }
    public void cargarButton()
    {
        // EJEMPLO CARGAR JUEGO (DEVUELVE UN OBJETO DE TIPO SaveFile)

        SaveFile saveFileLoaded = new SaveFile(); // Inicializa la instancia de SaveFile para cargar el juego
        saveFileLoaded = saveLoadSystem.LoadGame(); // Llama al método para cargar el juego y guarda el resultado en saveFileLoaded
        saveLoadSystem.SaveGame(saveFile); // Llama al método para guardar el juego
        Debug.Log("juego guardado " + saveFileLoaded.playerName); // Llama al método para cargar el juego y muestra el resultado en la consola
    }
}
    */
}
