﻿using UnityEngine;
using System.Collections;
using System.IO;

public class IMG2Sprite : MonoBehaviour {

	// This script loads a PNG or JPEG image from disk and returns it as a Sprite
	// Drop it on any GameObject/Camera in your scene (singleton implementation)
	//
	// Usage from any other script:
	// MySprite = IMG2Sprite.instance.LoadNewSprite(FilePath, [PixelsPerUnit (optional)])

	private static IMG2Sprite _instance;

	public static IMG2Sprite instance
	{
		get    
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.

			if(_instance == null)
				_instance = GameObject.FindObjectOfType<IMG2Sprite>();
			return _instance;
		}
	}

    public Sprite LoadNewSprite(Texture2D texture, float PixelsPerUnit = 100.0f)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Sprite NewSprite = new Sprite();
        Texture2D SpriteTexture = texture;
        NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }

    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f) {

		// Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

		Sprite NewSprite = new Sprite();
		Texture2D SpriteTexture = LoadTexture(FilePath);
        if (SpriteTexture == null)
        {
            Debug.Log(FilePath);
        }
		NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), PixelsPerUnit);

		return NewSprite;
	}

	public Texture2D LoadTexture(string FilePath) {

		// Load a PNG or JPG file from disk to a Texture2D
		// Returns null if load fails

		Texture2D Tex2D;
		byte[] FileData;

        
        if (FilePath.EndsWith(".dxt"))
        {
            Tex2D = LoadTextureDXT.Load(FilePath, false);
            return Tex2D;
        }
		if (File.Exists(FilePath)){
#if (!UNITY_WSA) || UNITY_EDITOR
			FileData = File.ReadAllBytes(FilePath);
#else
            FileData = UnityEngine.Windows.File.ReadAllBytes(FilePath);
#endif
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
			if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
				return Tex2D;                 // If data = readable -> return texture
		}  
		return null;                     // Return null if load failed
	}
}