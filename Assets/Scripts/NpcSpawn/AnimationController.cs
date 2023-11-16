using UnityEngine;

namespace Ursaanimation.CubicFarmAnimals
{
    public class AnimationController : MonoBehaviour
    {
        public Animator animator;
        public string walkForwardAnimation = "walk_forward";
        public string walkBackwardAnimation = "walk_backwards";
        public string runForwardAnimation = "run_forward";
        public string turn90LAnimation = "turn_90_L";
        public string turn90RAnimation = "turn_90_R";
        public string trotAnimation = "trot_forward";
        public string sittostandAnimation = "sit_to_stand";
        public string standtositAnimation = "stand_to_sit";
        public string eatingAnimation = "eating";
        public string hitReactionAnimation = "hit_reaction";
        public string deathAnimation = "death";
        public string walkForwareRootMotionAnimation = "walk_forward_root_motion";
        public string shuffleLeftAnimation = "shuffle_L";



        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {

            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    animator.Play(walkForwardAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.S))
            //{
            //    animator.Play(walkBackwardAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    animator.Play(runForwardAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.A))
            //{
            //    animator.Play(turn90LAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.D))
            //{
            //    animator.Play(turn90RAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    animator.Play(trotAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha4))
            //{
            //    animator.Play(sittostandAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha3))
            //{
            //    animator.Play(standtositAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.E))
            //{
            //    animator.Play(eatingAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.H))
            //{
            //    animator.Play(hitReactionAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    animator.Play(deathAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.R))
            //{
            //    animator.Play(walkForwareRootMotionAnimation);
            //}
            //else if (Input.GetKeyDown(KeyCode.F))
            //{
            //    animator.Play(shuffleLeftAnimation);
            //}
        }
    }
}

