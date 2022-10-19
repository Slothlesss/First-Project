using Assets.HeroEditor4D.Common.CharacterScripts;
using GameClient.Enums;
using HeroEditor4D.Common.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.ExampleScripts
{
    /// <summary>
    /// An example of how to handle user input, play animations and move a character.
    /// </summary>
    public class CharacterControls : MonoBehaviour
    {
        public Character4D Character;
        public bool InitDirection;
        public int MovementSpeed;
        public bool _moving;
        public void Start()
        {
            Character.AnimationManager.SetState(CharacterState.Idle);
            if (InitDirection) Character.SetDirection(Vector2.down);
        }

        public void Update()
        {
            SetDirection();
            Move();
            ChangeState();
            Actions();
        }

        private void SetDirection()
        {
            int[] array = new int[4];
            Vector2 directions;
            if (direction.directionRight == true)
            {
                array[0] = 1;
                directions = Vector2.left;
            }
            else if (direction.directionLeft == true)
            {
                array[1] = 1;
                directions = Vector2.right;
            }
            else if (direction.directionTop == true)
            {
                array[2] = 1;
                directions = Vector2.up;
            }
            else if (direction.directionDown == true) 
            {
                array[3] = 1;
                directions = Vector2.down; 
            }

            else return;
            Character.SetDirection(directions);
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data[1] = GameplayCode.Directions;
            data[2] = array;
            PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.GamePlay, data, false);
        }

        private void Move()
        {
            if (MovementSpeed == 0) return;
            var directions = Vector2.zero;
            if (direction.directionRight == true) directions += Vector2.left;
            else if (direction.directionLeft == true) directions += Vector2.right; 
            else if (direction.directionTop == true) directions += Vector2.up; 
            else if (direction.directionDown == true) directions += Vector2.down; 
            if (directions == Vector2.zero)
                {
                    if (_moving)
                    {
                        Character.AnimationManager.SetState(CharacterState.Idle);
                        _moving = false;

                        Dictionary<byte, object> data = new Dictionary<byte, object>();
                        data[1] = GameplayCode.MoveStop;
                        data[2] = _moving;
                        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.GamePlay, data, false);
                }
                }
                else
                {
                    Character.AnimationManager.SetState(CharacterState.Walk);
                    Character.transform.position += MovementSpeed * Time.deltaTime * (Vector3)directions.normalized;
                    _moving = true; 
                    var settings = new JsonSerializerSettings();
                    // This tells your serializer that multiple references are okay.
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    Dictionary<byte, object> data = new Dictionary<byte, object>();
                    data[1] = GameplayCode.Move;
                    data[2] = JsonConvert.SerializeObject(Character.transform.position, settings);
                    data[3] = _moving;
                    PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.GamePlay, data, false);
                }
        }

                //Skill
                public void Melee2H() => Character.AnimationManager.Slash(Character.WeaponType == WeaponType.Melee2H);
                private void Actions()
                {
                    if (Input.GetKeyDown(KeyCode.Q)) Character.AnimationManager.Slash(Character.WeaponType == WeaponType.Melee2H);
                    //ShowCrossbow
                    else if (Input.GetKeyDown(KeyCode.R)) Character.AnimationManager.ShotBow();                }

                private void ChangeState()
                {
                    if (Input.GetKeyDown(KeyCode.A)) Character.AnimationManager.Die();

                    if (Input.GetKeyDown(KeyCode.S)) Character.AnimationManager.Hit();
                }
            }
}