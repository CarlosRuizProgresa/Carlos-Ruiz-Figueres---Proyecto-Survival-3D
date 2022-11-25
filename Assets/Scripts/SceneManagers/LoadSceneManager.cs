using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class LoadSceneManager : MonoBehaviour
{
    // Singleton
    // 1. Nos instanciamos a nosotros mismos
    private static LoadSceneManager instance;
    
    // Otras variables
    public Image image;
    public float fadeSpeed;

    private void Awake()
    {
        // 2. Comprobamos nuestra existencia
        if (instance == null)
        {
            // 3. Nos inicializamos, creamos nuestra copia

            // This es una abrebiatura para decir LoadSceneManager
            instance = this;

            DontDestroyOnLoad(gameObject);

            image.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Metodo al que llamaremos desde fuera
    // para activar esta carga de escena
    public static void LoadScene(int buildIndex)
    {
        // TODO: Llamar al courtina de carga
        instance.StartCoroutine(instance.LoadNextScene(buildIndex));
    }
    
    // Courtina de carga y fade
    IEnumerator LoadNextScene(int buildIndex)
    {
        // 1. Activamos el image
        image.enabled = true;
        
        // 2. FadeIn de pantalla de carga
        float a = 0.0f;
        
        // Bajamos el alpha hasta 0 en un tiempo determinado
        while (a < 1.0f)
        {
            a += (1.0f / fadeSpeed) * Time.deltaTime;
            
            // Establecer en la imagen el nuevo alpha
            image.color = new Color(0, 0, 0, a);
            yield return null;
        }
        
        // 3. Carga asincrona de la escena
        AsyncOperation ao = SceneManager.LoadSceneAsync(buildIndex);
        while (ao.isDone == false)
        {
            yield return null;
        }
        // 4. FadeOut de la pantalla de carga
        a = 1.0f;
        
        // Bajamos el alpha hasta 0 en un tiempo determinado
        while (a > 0.0f)
        {
            a -= (1.0f / fadeSpeed) * Time.deltaTime;
            
            // Establecer en la imagen el nuevo alpha
            image.color = new Color(0, 0, 0, a);
            yield return null;
        }
        
        // 5. Desactivamos image
        image.enabled = false;
        
    }
    
}

