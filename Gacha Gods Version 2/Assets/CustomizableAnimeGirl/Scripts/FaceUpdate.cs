/*
顔の表情をアニメーションさせるスクリプト
アニメーション内のeventで OnCallChangeFace(state名) を設定することで
アニメーション中に表情を変更することも可能
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomizableAnimeGirl
{
    public class FaceUpdate : MonoBehaviour
    {

        public AnimationClip[] animations;
        public bool isKeepFace = true;
        public float delayWeight = 0.05f;
        Animator anim;
        float current = 0;
        bool isCurrentIncrease = false;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if (!isKeepFace)
            {
                if (isCurrentIncrease)
                {
                    current = Mathf.Lerp(current, 1, delayWeight);
                    if (current >= 0.99)
                    {
                        isCurrentIncrease = false;
                    }

                }
                else
                {
                    current = Mathf.Lerp(current, 0, delayWeight);
                }
            }
            else
            {
                current = 1f;
            }
            anim.SetLayerWeight(1, current);
        }

        //表情切り替え用イベントコール
        public void OnCallChangeFace(string str)
        {
            foreach (var animation in animations)
            {
                if (str == animation.name)
                {
                    anim.CrossFadeInFixedTime(str, 0.3f);
                    isCurrentIncrease = true;
                    return;
                }
            }
            str = "default";
            anim.CrossFadeInFixedTime(str, 0.3f);
            isCurrentIncrease = true;
        }
    }
}