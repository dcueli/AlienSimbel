using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
     #region Variables
    // Variables para el guardado con getter y setters
   
    private int gameNumber = 1;
    public int GameNumber
    {
        get => gameNumber;
        set => gameNumber = value;
    }

    private string playerName = "Player1";
    public string PlayerName
    {
        get => playerName;
        set => playerName = value;
    }

    private float timePlayed = 0f;
    public float TimePlayed
    {
        get => timePlayed;
        set => timePlayed = value;
    }

    private int deathCount = 0;
    public int DeathCount
    {
        get => deathCount;
        set => deathCount = value;
    }

    private int stage = 1;
    public int Stage
    {
        get => stage;
        set => stage = value;
    }

    private int level = 1;
    public int Level
    {
        get => level;
        set => level = value;
    }
    
    
    // Ruta del archivo de guardado
    private string savePath = "/savefile.json"; // Ruta del archivo de guardado
    private FicheroGuardado saveFile; // Objeto para guardar la información
    #endregion

    // Método para guardar el juego
    [ContextMenu("Guardar Juego")]
    public void SaveGame()
    {
        // Validación de campos obligatorios
        if (string.IsNullOrWhiteSpace(playerName))
            throw new System.Exception("El nombre del jugador está vacío.");

        if (stage < 1 || stage > 4)
            throw new System.Exception("La fase debe estar entre 1 y 4.");

        if (level < 1)
            throw new System.Exception("El nivel debe ser mayor o igual a 1.");

        // Crear el objeto de guardado
        saveFile = new FicheroGuardado
        {
            numeroPartida = gameNumber,
            nombreJugador = playerName,
            tiempoJugado = timePlayed,
            numeroMuertes = deathCount,
            fase = stage,
            nivel = level
        };

        // Convertir el objeto a JSON
        string json = saveFile.AJson();

        // escribe fichero en persistent data

        string fullPath = Application.persistentDataPath + savePath;
        System.IO.File.WriteAllText(fullPath, json);
        Debug.Log("Juego guardado en: " + fullPath);
    }
}
