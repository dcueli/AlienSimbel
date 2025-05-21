using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * ================================================================================================================================
 * CLASS AppCore
 *   Core of the VideoGame, 
 *   Where it is the config options
 * ================================================================================================================================
 *   Properties:
 *     .state<object>, global state and all of the components:
 * 		   - <menuOptions>, object who contains the options on the Main Menu
 *     .app<object>, general description of the App:
 * 		   - <(Here the App name)>, App shortname
 * 		   - <name>, App complete name
 * 		   - <description>, App short description
 * 		   - <url>, URL of the Service for the App (it means, the BackEnd)
 * 		   - <id>, Project ID
 *     .paths<object>, paths to directories of the App
 *     .images<Array>, names of the files for the static images
 * 
 * --------------------------------------------------------------------------------------------------------------------------------
 *   Métodos:
 *     .getMe(), devuelve la instancia actual de esta misma clase
 *     .setGlobalState(), modifica el estado global <this.state>
 *     .getGlobalState(), devuelve el estado global <this.state>
 *     .setUser(), establece/modifica el Usuario logueado actual en el estado global <this.state> y en el almacenamiento
 *     .getUser(), devuelve el Usuario logueado actual del estado global <this.state>
 * 		 .deleteUser(), borra el Usuario actual del almacenamiento y del estado global <this.state>, para establecer el objeto
 * 				<{ connecting: false, connected: false, data: {} }>
 * --------------------------------------------------------------------------------------------------------------------------------
 */

public static class Core {
  public const string NODE_ENV = Env.Dev;
  private static Dictionary<string, object> Scenes;
  private static Dictionary<string, object> State;
  private static Dictionary<string, object> Paths;

  public static Dictionary<string, object> scenes {
    get => Scenes;
    set => Scenes = value;
  }
  public static Dictionary<string, object> state {
    get => State;
    set => State = value;
  }
  public static Dictionary<string, object> paths {
    get => Paths;
    set => Paths = value;
  }
  static Core() {
		scenes = SetDefaultValues(
			new string[] { "main" },
			new string[] { "MainScn" }
		);
		
		// State contains Main Menu Options, Tracks, default language, current language
    state = SetDefaultValues();
	}		

	private static Dictionary<string, object> SetDefaultValues(
		string[] stats = null, 
		object[] values = null
	) {
		if (stats == null || values == null || stats.Length != values.Length)
			return new Dictionary<string, object>();

		return stats
			.Zip(values, (stat, value) => new { stat, value })
			.ToDictionary(pair => pair.stat, pair => pair.value);

	}
}