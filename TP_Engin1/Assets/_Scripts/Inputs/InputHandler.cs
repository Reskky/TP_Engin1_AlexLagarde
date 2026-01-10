using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
   public event Action<float> OnMove;
   public event Action OnJump;
   public event Action OnClick;

   [SerializeField] private PlayerInput _inputs;

   public static InputHandler Instance;

   private void Awake()
   {
      InitInputHandler();
   }

   private void InitInputHandler()
   {
      if (Instance && Instance != this)
         Destroy(gameObject);
      Instance = this;
      DontDestroyOnLoad(gameObject);
   }

   private void OnEnable()
   {
      _inputs.actions["MoveHorizontal"].performed += OnMoveInput;
      _inputs.actions["MoveHorizontal"].canceled += OnMoveInput;

      _inputs.actions["Jump"].performed += OnJumpInput;

      _inputs.actions["Click"].performed += OnClickInput;
   }
   
   
   private void OnDisable()
   {
      _inputs.actions["MoveHorizontal"].performed -= OnMoveInput;
      _inputs.actions["MoveHorizontal"].canceled -= OnMoveInput;

      _inputs.actions["Jump"].performed -= OnJumpInput;

      _inputs.actions["Click"].performed -= OnClickInput;
   }

   private void OnMoveInput(InputAction.CallbackContext ctx)
   {
      OnMove?.Invoke(ctx.ReadValue<float>());
   }
   
   private void OnJumpInput(InputAction.CallbackContext ctx)
   {
      OnJump?.Invoke();
   }
   
   private void OnClickInput(InputAction.CallbackContext ctx)
   {
      OnClick?.Invoke();
   }
}
