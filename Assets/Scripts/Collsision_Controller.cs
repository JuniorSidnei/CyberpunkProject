using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cyberpunk
{
    public class Collsision_Controller : MonoBehaviour
    {
        #region variables

        [Header("Layers")]
        public LayerMask groundLayer;

        [Space]
        [SerializeField] private bool onGround;
        [SerializeField] private bool onWall;
        [SerializeField] private bool onRightWall;
        [SerializeField] private bool onLeftWall;
        [SerializeField] private int wallSide;

        [Header("Collision")]

        public float collisionRadius = 0.25f;
        public Vector2 bottomOffset, rightOffset, leftOffset;
        private Color debugCollisionColor = Color.red;
        
        #endregion


        #region properties
        
        public bool OnGround {
            get => onGround;
            set => onGround = value;
        }

        public bool OnWall {
            get => onWall;
            set => onWall = value;
        }

        public bool OnRightWall {
            get => onRightWall;
            set => onRightWall = value;
        }

        public bool OnLeftWall {
            get => onLeftWall;
            set => onLeftWall = value;
        }

        public int WallSlide {
            get => wallSide;
            set => wallSide = value;
        }
        
        #endregion
        
        private void Update() {
            
            //ground
            onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
            //wall
            onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) 
                     || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

            //rightWall
            onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
            //leftWall
            onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

            //wallSide
            wallSide = onRightWall ? -1 : 1;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

            Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
            Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
            Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        }
    }
}