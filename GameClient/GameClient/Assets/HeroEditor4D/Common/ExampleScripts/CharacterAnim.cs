using Assets.HeroEditor4D.Common.CharacterScripts;
using HeroEditor4D.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.ExampleScripts
{
    /// <summary>
    /// An example of how to handle user input, play animations and move a character.
    /// </summary>
    public class CharacterAnim : MonoBehaviour
    {
        public Character4D Character;
        public bool InitDirection;
        public int MovementSpeed;
        public bool moving;
        public void Start()
        {
            Character.AnimationManager.SetState(CharacterState.Idle);
            if (InitDirection) Character.SetDirection(Vector2.down);
        }

        public void Update()
        {
            Move();
            ChangeState();
            Actions();
        }
        private void Move()
        {
            if (!moving) Character.AnimationManager.SetState(CharacterState.Idle);
            else Character.AnimationManager.SetState(CharacterState.Walk);
        }

        //Skill
        public void Melee2H() => Character.AnimationManager.Slash(Character.WeaponType == WeaponType.Melee2H);
        private void Actions()
        {
            if (Input.GetKeyDown(KeyCode.Q)) Character.AnimationManager.Slash(Character.WeaponType == WeaponType.Melee2H);
            //ShowCrossbow
            else if (Input.GetKeyDown(KeyCode.R)) Character.AnimationManager.ShotBow();
        }

        private void ChangeState()
        {
            if (Input.GetKeyDown(KeyCode.A)) Character.AnimationManager.Die();

            if (Input.GetKeyDown(KeyCode.S)) Character.AnimationManager.Hit();
        }

    }
}