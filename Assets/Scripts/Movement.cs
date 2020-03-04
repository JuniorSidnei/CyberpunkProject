using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cyberpunk
{
    public class Movement : MonoBehaviour
    {
        #region Components
        
        private Collsision_Controller m_coll;
        private Rigidbody2D m_rb;
        
        #endregion

        #region variables
        
        [Header("Stats")]
        [SerializeField] private float m_speed;
        [SerializeField] private float m_jumpForce;
        [SerializeField] private float m_slideSpeed;
        [SerializeField] private float m_wallJumpLerp;
        [SerializeField] private float m_dashSpeed;
        
        [Space]
        [Header("Controllers")]
        [SerializeField] private bool m_canMove;
        [SerializeField] private bool m_wallGrab;
        [SerializeField] private bool m_wallJumped;
        [SerializeField] private bool m_wallSlide;
        [SerializeField] private bool m_isDashing;
        
        #endregion
        private void Start() {
            
            m_coll = GetComponent<Collsision_Controller>();
            m_rb = GetComponent<Rigidbody2D>();
        }

        
        private void Update() {
            //inputs
            GetAxis();
            GetAxisRaw();
            
            //Direction
            GetDirection();
            
            //Moving
            Walk();
        }

        private Vector2 GetAxis() {
            //get input
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        private Vector2 GetAxisRaw() {
            //get inpput raw
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        private Vector2 GetDirection() {
            //create a vector on input value
            return new Vector2(GetAxis().x, GetAxis().y);
        }

        private void Walk() {

            //cannot move
            if (!m_canMove) {
                return;
            }

            //on wall
            if (m_wallGrab) {
                return;
            }

            
            if (!m_wallJumped) {
                m_rb.velocity = new Vector2(GetDirection().x * m_speed * Time.deltaTime, m_rb.velocity.y);
            }
            else {
                m_rb.velocity = Vector2.Lerp(m_rb.velocity, (new Vector2(GetDirection().x * m_speed,
                    m_rb.velocity.y)), m_wallJumpLerp * Time.deltaTime);
            }

        }
    }
}