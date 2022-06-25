using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform position;
    // Definir um eixo de rotação e movimentar a câmera nesse eixo.
    // Quando o jogador rotar no eixo X (tecla W ou S), posicionar a câmera olhando diretamenta para esse eixo.
    void Start()
    {
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        position.rotation = new Quaternion(0f,0f,0f,0f);
    }
}
