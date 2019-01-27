using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class AudionMan : MonoBehaviourPun
{
    private AudioSource[] myAudio;
    private int[] IndexesToPlay;

    void Start(){

    }


    void Update(){
        if (IndexesToPlay.Length > 0)
        {
            for (int i = IndexesToPlay.Length - 1 ; i >=0   ; i--){
                myAudio[IndexesToPlay[i]].Play(); //how to play sound
                IndexesToPlay.Array.Resize(ref result, result.Length - 1);// cia taip? 
                //and then just let it play out till the end
            }
        }
    }

    void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    
    public void IWillNowPlaySound(AudioSource AudioGet){
        if (myAudio.Length  > 0){
            for (int i =0; i<myAudio.Length;i++ ){
                if (myAudio[i].clip == AudioGet.clip)
                {
                IndexesToPlay.Array.Resize(ref result, result.Length + 1);// cia taip? 

                    IndexesToPlay[IndexesToPlay.Length-1] = i; // just adding a new index
                    break; // jeigu sitas garsas jau yra liste, tai tiesiog paleisk
                } 
                else 
                {
                    // jei naujas garsas tai pridedame ji prie saraso garsu.
                    myAudio.Array.Resize(ref result, IAsyncResult.Lenght +1 );
                    myAudio[myAudio.Length-1] = AudioGet;
                    IndexesToPlay = i;
                }
            }
        }else{
            myAudio[0] = AudioGet;
        }
    }

}