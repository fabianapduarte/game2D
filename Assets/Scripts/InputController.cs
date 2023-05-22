using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Vector2 lastMousePosition;
    public bool gamepadOn = false;

    public static InputController instance = null;

    private string pcIconsPath = "Interfaces/Icons/pc"; // Nome da pasta 1
    private string gamepadIconsPath = "Interfaces/Icons/gamepad"; // Nome da pasta 2
    private Dictionary<string, Sprite> pcIcons = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> gamepadIcons = new Dictionary<string, Sprite>();

    // Start is called before the first frame update
    void Start(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        lastMousePosition = Mouse.current.position.ReadValue();
        LoadImages(pcIconsPath, pcIcons);
        LoadImages(gamepadIconsPath, gamepadIcons);
    }

    private void detectarEntrada() {
        //Image btnDinamico = GameObject.Find("imgDinamica").GetComponent<Image>();

        bool mouseMove = false;
        Vector2 currentMousePosition = new Vector2(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y);
        if (currentMousePosition != lastMousePosition){
            if (lastMousePosition != Vector2.zero){
                mouseMove = true;
            }
            lastMousePosition = Mouse.current.position.ReadValue();
        }

        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed || mouseMove){
            //btnDinamico.sprite = keyboard;
            if (gamepadOn == true){
                gamepadOn = false;
                Cursor.visible = true;
            }
        }

        if (Gamepad.current != null){
            foreach (InputControl control in Gamepad.current.allControls){
                if (control.IsPressed()){
                    //btnDinamico.sprite = gamepad;
                    //return true;
                    if (gamepadOn == false){
                        gamepadOn = true;
                        Cursor.visible = false;
                    }
                }
            }
        }
    }

    private void LoadImages(string path, Dictionary<string, Sprite> dicionario){
        string folderPath = Path.Combine(Application.dataPath, path); // Caminho completo para a pasta
        string[] imagePaths = Directory.GetFiles(folderPath); // Obter os caminhos completos de todas as imagens na pasta

        foreach (string imagePath in imagePaths){
            string imageName = Path.GetFileNameWithoutExtension(imagePath); // Nome do arquivo sem a extensão
            Sprite sprite = LoadSprite(imagePath); // Carregar o sprite da imagem

            if (sprite != null){
                dicionario.Add(imageName, sprite); // Adicionar o sprite ao dicionário com a chave correspondente
            }
        }
    }

    private Sprite LoadSprite(string path){
        byte[] bytes = File.ReadAllBytes(path); // Ler os bytes da imagem
        Texture2D texture = new Texture2D(2, 2); // Criar uma nova textura
        bool loadSuccess = texture.LoadImage(bytes); // Carregar a imagem na textura

        if (loadSuccess){
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f); // Criar e retornar o sprite
        }

        return null;
    }

    private void toggleIcons(Dictionary<string, Sprite> dicionario)
    {
        GameObject[] dynamicIcons = GameObject.FindGameObjectsWithTag("dynamicIcon");
        Image image;
        foreach (GameObject dynamicIcon in dynamicIcons){
            image = dynamicIcon.GetComponent<Image>();

            if (image != null){
                //Debug.Log(dynamicIcon.name);
                string spriteName = dynamicIcon.name;
                Sprite sprite;

                if (dicionario.TryGetValue(spriteName, out sprite)){
                    // sprite encontrado
                    image.sprite = sprite;
                }
            }
        }
    }

    // Update is called once per frame
    void Update(){
        detectarEntrada();
        //Debug.Log("Entrada de gamepad: "+gamepadOn);


        //muda icones da HUD pra icones de gamepad
        if (gamepadOn)
        {
            toggleIcons(gamepadIcons);
        }
        //muda icones da HUD pra icones de PC
        else
        {
            toggleIcons(pcIcons);
        }
    }
}
