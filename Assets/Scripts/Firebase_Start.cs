using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase_Start : MonoBehaviour
{
    public Database_Script dataHandler;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerDataHandler ������Ʈ �������� 
       dataHandler = GetComponent<Database_Script>();

        // ����: �÷��̾� ������ ����

        dataHandler.SavePlayerData("Player1", 5, 10000);
    }

}
