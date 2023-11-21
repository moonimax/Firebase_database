using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase_Start : MonoBehaviour
{
    public Database_Script dataHandler;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerDataHandler 컴포넌트 가져오기 
       dataHandler = GetComponent<Database_Script>();

        // 예시: 플레이어 데이터 저장

        dataHandler.SavePlayerData("Player1", 5, 10000);
    }

}
