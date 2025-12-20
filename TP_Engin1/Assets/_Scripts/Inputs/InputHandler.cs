using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
   public event Action<Vector2> OnMove;
   public event Action OnJump;

   private PlayerInput _inputs;

   private static readonly Lazy<InputHandler> _instance = new Lazy<InputHandler>(() => new InputHandler());

   public static InputHandler Instance => _instance.Value;

   private void Awake()
   {
      _inputs = GetComponent<PlayerInput>();
   }

   private void OnEnable()
   {
      _inputs.actions["Move"].performed += OnMoveInput;
      _inputs.actions["Move"].canceled += OnMoveInput;

      _inputs.actions["Move"].performed += OnJumpInput;
   }

   private void OnMoveInput(InputAction.CallbackContext ctx)
   {
      OnMove?.Invoke(ctx.ReadValue<Vector2>());
   }
   
   private void OnJumpInput(InputAction.CallbackContext ctx)
   {
      OnJump?.Invoke();
   }
}
