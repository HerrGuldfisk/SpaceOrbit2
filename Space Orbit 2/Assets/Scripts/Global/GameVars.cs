/*
GameBalancing.cs
Håller samtliga variabler som vi ska använda för balancing

INSTUKTIONER:

Enable-a i scen:
- Lägg in "GameVars" prefaben i din scene

Lägg till variabler:
- Lägg till variabler som ska användas för balancing här
- Skriv variablerna som public static
- Lägg variablerna under rätt header
- Om det saknas en header, lägg till det du behöver

Använda variabler:
- Hämta variabeln i update (eller varje gång den är relevant) med: varName = FindObjectOfType<GameVars>().varName;
- ÄNDRA inte variablerna i andra script. Endast get.
- Se till att lyssna på variablerna så att du vet när de ändras från inspectorn

Detta system är väldigt begränsat just nu och kan uppgraderas i framtiden.
Möjliga uppgraderingar:

- Sliders för att ändra värden
- In-game UI (för att se och styra variablerna)
- Get-only så att man inte kan fucka med variablerna
- Fixa performance så att man inte måste hämta VARJE GÅNG
- Göra det möjligt för oss att individuellt lägga till nya variabler utan att vi behöver redigera samma script (för at undvika merge conflicts)

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVars : MonoBehaviour
{

    //lägg till dina variabler här

    [Header("Player")]
    public float playerSpeed = 1;

    [Header("Planets")]
    public float planetSize = 1;

    //-----------------

    //ETT TEST ATT SKRIVA MER AVANCERADE VARIABLER. SPARAR DETTA TILL SENARE. FOR NOW HÅLLER VI DET SIMPELT.

    // [Header("Test Variables")]
    // [SerializeField]
    // private int myAutoVar;
    // public int MyAutoVar
    // {
    //     get { return myAutoVar; }
    //     private set { myAutoVar = value; }
    // }
    // private int previousValue;

    // private void OnValidate()
    // {
    //     if (myAutoVar != previousValue)
    //     {
    //         previousValue = myAutoVar;
    //         print("variable changed!");
    //     }
    // }

}
