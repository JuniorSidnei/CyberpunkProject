using UnityEngine;

namespace CharacterSystem {
    
    [System.Serializable]
    public class MoveAction : CharacterAction {
        
        public Character2D.Status UnallowedStatus;
        public float Speed = 50;
        public float InAirDrag = 0.5f, InGroundDrag = 14;
        
        private InputSource m_input;
        
        protected override void OnConfigure() {
            m_input = Character2D.Input;
            Character2D.LocalDispatcher.Subscribe<OnCharacterUpdate>(OnCharacterUpdate);
        }
        
        private void OnCharacterUpdate(OnCharacterUpdate ev) {
            if (Character2D.HasStatus(UnallowedStatus)) {
                return;
            }
            
            Character2D.Rigidbody.drag = Character2D.HasStatus(Character2D.Status.OnGround) ? InGroundDrag : InAirDrag;
            
            if (m_input.HasAction(InputAction.Button2)) {//right
                Character2D.Rigidbody.AddForce(Vector3.right * Speed * Time.deltaTime);
            }
            if (m_input.HasAction(InputAction.Button3)) {//left
                Character2D.Rigidbody.AddForce(Vector3.left * Speed * Time.deltaTime);
            }
        }
    }
}