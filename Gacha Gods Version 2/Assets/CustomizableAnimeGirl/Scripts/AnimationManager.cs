/*
アニメーションを管理するスクリプト
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomizableAnimeGirl
{
    public class AnimationManager : MonoBehaviour
    {
        public GameObject animatorObject;
        private Animator anim;
        private FaceUpdate faceUpdate;
        private AnimatorStateInfo currentState;
        private AnimatorStateInfo previousState;
        // Start is called before the first frame update
        void Start()
        {
            anim = animatorObject.GetComponent<Animator>();
            faceUpdate = animatorObject.GetComponent<FaceUpdate>();
            currentState = anim.GetCurrentAnimatorStateInfo(0);
            previousState = currentState;
        }

        // Update is called once per frame
        void Update()
        {
            // "Next"フラグがtrueの時の処理
            if (anim.GetBool("Next"))
            {
                // 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
                currentState = anim.GetCurrentAnimatorStateInfo(0);
                if (previousState.fullPathHash != currentState.fullPathHash)
                {
                    anim.SetBool("Next", false);
                    previousState = currentState;
                }
            }

            // "Back"フラグがtrueの時の処理
            if (anim.GetBool("Back"))
            {
                // 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
                currentState = anim.GetCurrentAnimatorStateInfo(0);
                if (previousState.fullPathHash != currentState.fullPathHash)
                {
                    anim.SetBool("Back", false);
                    previousState = currentState;
                }
            }

        }

        public void next()
        {
            anim.SetBool("Next", true);
        }

        public void prev()
        {
            anim.SetBool("Back", true);
        }

        public void changeFace(string str){
            faceUpdate.OnCallChangeFace(str);
        }
    }
}
