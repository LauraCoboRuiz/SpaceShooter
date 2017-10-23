using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    public class InputManager
    {
        KeyboardState currentKeyboardState, previousKeyboardState;
        MouseState currentMouseState, previousMouseState;
        GamePadState currentGamePadState, previousGamePadState;

        public InputManager()
        {
            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;

            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;

            currentGamePadState = GamePad.GetState(0);
            previousGamePadState = currentGamePadState;
        }

        public void Update()
        {
            // Get Keyboard State
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            // Get Mouse State
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // Get GamePad state
            previousGamePadState = currentGamePadState;
			currentGamePadState = GamePad.GetState(0);
        }

        // Keyboard related functions
        public bool IsKeyPressed(Keys key)
        {
            if (previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key)) return true;
            else return false;
        }

        public bool IsKeyReleased(Keys key)
        {
            if (previousKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key)) return true;
            else return false;
        }

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        // Mouse related functions
        public bool IsMouseButtonPressed(int button)
        {
            switch (button)
            {
                case 0: if ((previousMouseState.LeftButton == ButtonState.Released) && (currentMouseState.LeftButton == ButtonState.Pressed)) return true; break;
                case 1: if ((previousMouseState.MiddleButton == ButtonState.Released) && (currentMouseState.MiddleButton == ButtonState.Pressed)) return true; break;
                case 2: if ((previousMouseState.RightButton == ButtonState.Released) && (currentMouseState.RightButton == ButtonState.Pressed)) return true; break;
                default: break;
            }

            return false;
        }
        
        public bool IsMouseButtonReleased(int button)
        {
            switch (button)
            {
                case 0: if ((previousMouseState.LeftButton == ButtonState.Pressed) && (currentMouseState.LeftButton == ButtonState.Released)) return true; break;
                case 1: if ((previousMouseState.MiddleButton == ButtonState.Pressed) && (currentMouseState.MiddleButton == ButtonState.Released)) return true; break;
                case 2: if ((previousMouseState.RightButton == ButtonState.Pressed) && (currentMouseState.RightButton == ButtonState.Released)) return true; break;
                default: break;
            }

            return false;
        }

        public Vector2 GetMousePosition()
        {
            return currentMouseState.Position.ToVector2();
        }

        // GamePad related functions
        public bool IsGamePadButtonPressed(Buttons button)
        {
            if (previousGamePadState.IsButtonUp(button) && currentGamePadState.IsButtonDown(button)) return true;
            else return false;
        }

        public bool IsGamePadButtonReleased(Buttons button)
        {
            if (previousGamePadState.IsButtonDown(button) && currentGamePadState.IsButtonUp(button)) return true;
            else return false;
        }
    }
}
