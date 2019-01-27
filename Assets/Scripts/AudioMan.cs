using System;
using UnityEngine;

public class AudioMan : MonoBehaviour
{
    private AudioSource[] myAudio;
    private int[] IndexesToPlay;

    void Start(){
        DontDestroyOnLoad(gameObject);
    }


    void Update(){
        if (IndexesToPlay?.Length > 0)
        {
            for (int i = IndexesToPlay.Length - 1 ; i >=0   ; i--){
                myAudio[IndexesToPlay[i]].Play(); //how to play sound
                Array.Resize(ref IndexesToPlay, IndexesToPlay.Length - 1);// cia taip? 
                //and then just let it play out till the end
            }
        }
    }

    void Awake(){
    }

    
    public void IWillNowPlaySound(AudioSource AudioGet){
        if (myAudio?.Length  > 0){
            for (int i =0; i<myAudio.Length;i++ ){
                if (myAudio[i].clip == AudioGet.clip)
                {
                    if (IndexesToPlay is null)
                    {
                        IndexesToPlay = new[] { i };
                    }
                    else
                    {
                        Array.Resize(ref IndexesToPlay, IndexesToPlay.Length + 1);// cia taip? 

                        IndexesToPlay[IndexesToPlay.Length - 1] = i; // just adding a new index
                        break; // jeigu sitas garsas jau yra liste, tai tiesiog paleisk
                    }
                } 
                else 
                {
                    // jei naujas garsas tai pridedame ji prie saraso garsu.
                    Array.Resize(ref myAudio, myAudio.Length +1 );
                    myAudio[myAudio.Length-1] = AudioGet;
                    IndexesToPlay[IndexesToPlay.Length-1] = i;
                }
            }
        }else{
            myAudio = new[] { AudioGet };
        }
    }

}