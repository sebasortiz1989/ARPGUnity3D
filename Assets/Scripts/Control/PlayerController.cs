using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;
using System;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] CursorMapping[] _cursorMappings = null;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        // Cached reference
        private Health health;
        private Fighter _fighter;

        enum CursorType
        {
            None,
            Movement,
            Combat,
            UI
        }

        private void Awake()
        {
            health = GetComponent<Health>();
            _fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())// Confusing but this is if pointer is over game object that is a UI
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!_fighter.CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    _fighter.Attack(target.gameObject);
                }
                SetCursor(CursorType.Combat);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType _cursorType)
        {
            CursorMapping _mapping = GetCursorMapping(_cursorType);
            Cursor.SetCursor(_mapping.texture, _mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType _type)
        {
            foreach (CursorMapping mapping in _cursorMappings)
            {
                if (mapping.type == _type)
                {
                    return mapping;
                }
            }
            return _cursorMappings[0];
        }

        private bool InteractWithMovement()
        {
            RaycastHit hitInformation;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInformation);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hitInformation.point);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
