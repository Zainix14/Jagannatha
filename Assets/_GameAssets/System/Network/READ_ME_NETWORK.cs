using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class READ_ME_NETWORK : MonoBehaviour
{
    /* 
    
    Pour les fonctions resaux:

        using UnityEngine.Networking
        MonoBehaviour devient NetworkBehaviour

        Tout OBJ ayant un NetworkBehaviour doit avoir un Componant NetworkIdentity (se met automatiquement si un NetworkBehaviour est glisser en componant)


    Deux Concepts Importants :

        ClientRPC = du serveur vers les clients
        Command = d'un client vers le serveur.


    Mgr_Network

        NetworkManager:    

            Prefab gerant la connection.
                Le Player Prefab represente le joueur dans la scene et il est crée automatiquement.
                C'est le seul OBJ a avoir l'autorité pour envoyer des Commands
        
            Registered Spawnable Prefab est la liste des Prefab spawnable.
            Tout OBJ "Dynamique" devant être syncronisé et instancié a un moment doit être ajouter dans cette liste.
                Exemple : Un projectile.

        NetworkHUD:

            Componant s'occuppant de la connextion entre les deux PC.
            Quand le jeu est lancé faire : 
                LAN Host sur le Pilote.
                LAN Connect sur le client.
                    Sur le meme PC (Symlink) adress = localhost 
                    Sur deux PC différent adress = IPv4 du PC Host (windows cmd => ipconfig)


    SC_NetWorkPlayer (Initialisation des OBJ Network)

        Ce SC n'est present que sur le Player Prefab (Network Manager).
        Il gère deux chose :
            Les Commands (Voir plus bas).
            L'Initialisation des OBJ Network.

        L'Initialisation des OBJ Network:
            Cet a dire les OBJ non present dans la scene crée et syncronisé lors de la connexion.
            L'OBJ doit être dans la Registered Spawnable Prefab.
            Il faut l'intentié (creation sur le serveur)
                GameObject GO_Name_Temp = (GameObject)Instantiate(GO_Name, position, rotation);
            Puis le Spawn (Copie sur les Clients)
                NetworkServer.Spawn(GO_Name_Temp);


    Syncro OBJ par SC_RPCsend (Server => Client) 

        [ClientRPC] (ligne au dessus de la fonction /!\ le com de la fonction doit commencer par "Rpc").
        L'appel de la fonction peut se faire coter serveur uniquement => securisé l'appel avec un if(isServeur)

        Le contenu de la Fonction ClientRPC va s'executer chez tout les Clients, la partie concerné du script va s'executer sur le dit script mais coter Client.
        Toute fonction RPC doit être dans SC_RPCsend.
        SC_RPCsend doit donc être sur tout objet voulant envoyé/recevoir des donnée.
        Ces Objets doivent avoir été Instancié et Spawn sauf Exeptions*.

        Il est fortement recommander que ces objets soit des prefabs (Obligatoire pour pouvoir être Spawn).

        *Exeptions : 
            Un OBJ unique peut theoriquement être dans les scenes de base et se syncronisé correctement avec se script.
            Le NetworkIdentity peut theoriquement suffire a faire la connection sans qu'ils soit Instancié puis Spawn.
            Ayant eus quelques problemes evitons au maximmum.
            Envisageable pour le Boss et le Mecha par exemple.

        //EXEMPLE POUR METTRE A JOUR LE VT3 D'UN OBJ SUR LE SERVER ET LE CLIENT DEPUIS LE SERVER
        [ClientRpc]
        public void RpcSendVt3(GameObject GO_Target, Vector3 vt3_Position)
        {
            GO_Target.transform.position = vt3_Position;
        }


    Syncro de Var par SC_SyncVariables (Server => Client)

        Ce SC est dans le Mgr_SyncVar.
        /!\ Il se crée automatiquement, pas besoin de le mettre dans la scene.

        Declaration d'une SyncVar :

        [SyncVar]
        public int n_TestInt_Sync = 0;

        Il faut bien remettre le [SyncVar] a chaques variables.
        Maintenant si cette Variable est modifié coter serveur elle le sera coter client.
        /!\ le sens Server => Client et le seul qui est possible. la modifier sur le Client ne la changera pas.

        Le but de ce SC est d'être une liste de SyncVar pour limité au minimum les SyncVar dans les autres SC.
        Il n'est theoriquement pas exclu de faire ceci mais jusqu'à maintenant cela c'est rarement avéré concluant.


    Command par le Player Asset (Client => Server)

        Les Commands servent a envoyer un ordre du Client vers le Server.
        Pour ceci l'objet coter Client souhaitant envoyer une command doit passer par le Player Prefab crée par le NetworkManager.
        Les commands doivent donc être dans le SC_SC_NetWorkPlayer.

        /!\ Nous utiliserons les command si possible uniquement pour qu'un OBJ modifie une variables sur lui même coter Client et Coter Server depuis le Client.

        Comme le Player Prefab et crée apres il ne peut pas être en référence.
        Voici un exemple :

        //FONCTION COTER CLIENT SUT l'OBJ
        void SendDestruct()
        {
            //SI SERVER EXECUTE DIRECTEMENT L'EQUIVALENT DU DU COMMAND
            if (isServer)
                b_IsDead_Sync = true;

            //SI CLIENT RECUPERE LE NET PLAYER (PLAYER PREFAB)
            if (!isServer)
            {
                GameObject[] NetPlayers = GameObject.FindGameObjectsWithTag("NetPlayer");
                foreach (GameObject NetPlayer in NetPlayers)
                {
                    //PREND LE PLAYER PREFAB CLIENT
                    if (!isServer)
                    {
                        //APPEL LE CMD ET LUI TRANSMET SON NET ID
                        NetPlayer.GetComponent<SC_NetWorkPlayer>().CmdIsDead(this.netId);
                    }
                }
            }                
        }

        //FONCTION COMMAND COTER CLIENT SUT lE PLAYER PREFAB
        [Command]
        public void CmdIsDead(NetworkInstanceId playerId)
        {
            NetworkServer.FindLocalObject(playerId).GetComponent<SC_Cover>().b_IsDead_Sync = true;   
        }


    */
}
