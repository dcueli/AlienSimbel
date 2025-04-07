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
}
