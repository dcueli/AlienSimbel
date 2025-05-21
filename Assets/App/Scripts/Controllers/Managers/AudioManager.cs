using UnityEngine;

/**
 * ================================================================================================
 * BaseGameManager
 * 	 Extends: 	 Singleton<BaseGameManager>
 *   Implements: IGameManager, IButtonsManager
 * ------------------------------------------------------------------------------------------------
 * TODO:
 * - Hacer Singleton la clase <AudioManager> con Singleton<T> como <BaseGameManager>
 * - Terminar de implementar el <BaseAudioManager>
 * - Terminar de implementar el <AudioManager>
 * - Agregar métodos para reproducir efectos de sonido
 * - Documentar clase
 * 
 * Añadir el componente Audio Source:
 * Añade un componente Audio Source al GameObject AudioManager.
 * En el script AudioManager asigna ese componente a la variable bgMusic. Puedes hacerlo arrastrándolo desde el inspector, o bien mediante código usando GetComponent<AudioSource>().
 * 
 * Usar el Audio Manager desde otros scripts:
 * Ej:
 * class {
 *  [SerializeField]public AudioClip musicaParaNivel1; * Asigna el clip de audio en el Inspector
 *  void Start() {
 *  	AudioManager.instance.PlayMusicaFondo(musicaParaNivel1);
 *  }
 * }
 * ================================================================================================
 */
class AudioManager : BaseAudioManager{
	public AudioSource bgMusic;

	protected override void Awake() {
		// 
	}

	public void PlayMusicaFondo(AudioClip clip) {
		bgMusic.clip = clip;
		bgMusic.Play();
	}

	public void StopMusicaFondo() {
		bgMusic.Stop();
	}
}