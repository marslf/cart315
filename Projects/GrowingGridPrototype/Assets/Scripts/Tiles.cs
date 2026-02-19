using UnityEngine;
 
 public class Tile : MonoBehaviour
 {
     public bool isFilled = false;
     public Color tileColor; //color tiles
 
     private SpriteRenderer sr;
     
     private static Color[] primaries = { Color.red, Color.blue, Color.yellow };
        private static Color[] secondaries = {
            new Color(0f, 1f, 0f),     // green
            new Color(1f, 0.5f, 0f),   // orange
            new Color(0.5f, 0f, 0.5f)  // purple
        }; 
        
        void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            sr.color = Color.black; // empty at start
        }
        
        public void SetCoordinates(int x, int y) { }
        
        public void Fill(Color newColor)
        {
            isFilled = true;
            tileColor = newColor;
            sr.color = newColor;
        }

        public void Clear()
        {
            isFilled = false;
            sr.color = Color.black;
        }

        void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0) && isFilled)
            {
                Clear();
            }

            if (Input.GetMouseButtonDown(1) && !isFilled)
            {
                Fill(GetRandomPaletteColor());
            }
        }

        // tiered mutation logic
        public Color GetMutationColor()
        {
            if (tileColor == Color.white)
            {
                // Level 0 = can become white or any primary
                Color[] options = new Color[primaries.Length + 1];
                options[0] = Color.white;
                for (int i = 0; i < primaries.Length; i++) options[i + 1] = primaries[i];
                return options[Random.Range(0, options.Length)];
            }
            else if (System.Array.Exists(primaries, c => c == tileColor))
            {
                // Level 1 = can become itself or any secondary
                Color[] options = new Color[secondaries.Length + 1];
                options[0] = tileColor; // can stay the same
                for (int i = 0; i < secondaries.Length; i++) options[i + 1] = secondaries[i];
                return options[Random.Range(0, options.Length)];
            }
            else if (System.Array.Exists(secondaries, c => c == tileColor))
            {
                // Level 2 = stays the same
                return tileColor;
            }
            else
            {
                return Color.white;
            }
        }

        // For right click random fill
        public Color GetRandomPaletteColor()
        {
            Color[] palette = {
                Color.white,
                Color.red, Color.blue, Color.yellow,
                new Color(0f, 1f, 0f),
                new Color(1f, 0.5f, 0f),
                new Color(0.5f, 0f, 0.5f)
            };
            return palette[Random.Range(0, palette.Length)];
        }
 }