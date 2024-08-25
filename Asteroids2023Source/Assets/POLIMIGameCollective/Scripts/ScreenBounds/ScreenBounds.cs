using UnityEngine;

/// <summary>
/// Contains static methods and fields for determining screen bounds
/// </summary>
namespace POLIMIGameCollective
{
        
    public class ScreenBounds
    {
        private static Vector3 _bounds = Vector3.zero;
        private static float _left = 0;
        private static float _right = 0;
        private static float _top = 0;
        private static float _bottom = 0;
        private static float _width = 0;
        private static float _height = 0;
        private static bool _validBounds = false;

        public static float Left { get { return _left; } }
        public static float Right { get { return _right; } }
        public static float Top { get { return _top; } }
        public static float Bottom { get { return _bottom; } }

        public static float Width { get { return _width; } }

        public static float Height { get { return _height; } }
        
        public static bool ValidBounds { get { return _validBounds; } }

        // Start is called before the first frame update
        public static void ComputeScreenBounds()
        {
            _bounds = GetScreenBounds();
            _left = -_bounds.x;
            _right = _bounds.x;
            _top = _bounds.y;
            _bottom = -_bounds.y;
            _width = _right - _left;
            _height = _top - _bottom;
            _validBounds = true;
        }
        
        private static Vector3 GetScreenBounds(float spriteBorder=0f)
        {
            Vector3 screenVector = new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z);

            return Camera.main.ScreenToWorldPoint(screenVector);
        }

        public static Vector2 RandomTopPosition()
        {
            float horizontalPosition = Random.Range(Left, Right);
            return new Vector2(horizontalPosition, Top);
        }

        public static Vector2 GetRandomPosition()
        {
            float targetVerticalPos = Random.Range(-_bounds.y, _bounds.y);
            float targetHorizontalPos = Random.Range(-_bounds.x, _bounds.x);

            return new Vector2(targetHorizontalPos, targetVerticalPos);
        }

        public static bool OutOfBounds(Vector2 position)
        {
            float x = Mathf.Abs(position.x);
            float y = Mathf.Abs(position.y);

            return (x > _left || y > _top);
        }
    }
}

