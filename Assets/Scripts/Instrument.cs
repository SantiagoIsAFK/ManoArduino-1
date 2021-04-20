using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public AudioSource[] sources;
    public Animator m_animator;

    public bool[] touchedNote = new bool[5];

    // Update is called once per frame
    void Update()
    {
        sources[0].volume = HandMusicControl.instance.fingersValue[0];
        sources[1].volume = HandMusicControl.instance.fingersValue[1];
        sources[2].volume = HandMusicControl.instance.fingersValue[2];
        sources[3].volume = HandMusicControl.instance.fingersValue[3];
        sources[4].volume = HandMusicControl.instance.fingersValue[4];


        for (int i = 0; i<5; i++) {
            if (!touchedNote[i] && HandMusicControl.instance.GetNormalizedFinger(i) >= 0.5f) {
                touchedNote[i] = true;

                sources[i].Play();

            }


            if (HandMusicControl.instance.GetNormalizedFinger(i) < 0.5f)
            {
                touchedNote[i] = false;
            }
        }


        m_animator.SetFloat("miTime", HandMusicControl.instance.GetNormalizedFinger(0));
        m_animator.SetFloat("faTime", HandMusicControl.instance.GetNormalizedFinger(1));
        m_animator.SetFloat("solTime", HandMusicControl.instance.GetNormalizedFinger(2));
        m_animator.SetFloat("laTime", HandMusicControl.instance.GetNormalizedFinger(3));
        m_animator.SetFloat("siTime", HandMusicControl.instance.GetNormalizedFinger(4));
    }
}
