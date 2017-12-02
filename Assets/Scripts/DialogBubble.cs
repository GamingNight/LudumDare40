using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;
using System.Linq;

public class DialogBubble : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	public GameObject vCurrentBubble = null; //just to make sure we cannot open multiple bubble at the same time.
	public bool IsTalking = false;
	public PixelBubble vBubble = new PixelBubble();
	public float positionX = 0;
	public float positionY = 0;
	public bool flipX;
	public bool flipY;
	private PixelBubble vActiveBubble = null;

    //show the right bubble on the current character
	void ShowBubble()
	{
		bool gotonextbubble = false;
			
		//make sure the bubble isn't already opened
			if (vCurrentBubble == null)
			{
				//make the character in talking status
				IsTalking = true;
				
				//cut the message into 24 characters
				string vTrueMessage = "";
				string cLine = "";
				int vLimit = 24;
				
				//cut each word in a text in 24 characters.
				foreach (string vWord in vBubble.vMessage.Split(' '))
				{
					if (cLine.Length + vWord.Length > vLimit)
					{
						vTrueMessage += cLine+System.Environment.NewLine;  
						
						//add a line break after
						cLine = ""; //then reset the current line
					}
					
					//add the current word with a space
					cLine += vWord+" ";
				}
				
				//add the last word
				vTrueMessage += cLine;
				GameObject vBubbleObject = null;
				
				//create bubble
				vBubbleObject = Instantiate(Resources.Load<GameObject> ("Customs/BubbleRectangle"));
			vBubbleObject.transform.position = transform.position + new Vector3(positionX, positionY, 0f); //move a little bit the teleport particle effect
				
				Color vNewBodyColor = new Color(vBubble.vBodyColor.r, vBubble.vBodyColor.g, vBubble.vBodyColor.b, 0f);
				Color vNewBorderColor = new Color(vBubble.vBorderColor.r, vBubble.vBorderColor.g, vBubble.vBorderColor.b, 0f);
				Color vNewFontColor = new Color(vBubble.vFontColor.r, vBubble.vFontColor.g, vBubble.vFontColor.b, 255f);
				
				//get all image below the main Object
				foreach (Transform child in vBubbleObject.transform)
				{
					SpriteRenderer vRenderer = child.GetComponent<SpriteRenderer> ();
					TextMesh vTextMesh = child.GetComponent<TextMesh> ();
					
					if (vRenderer != null && child.name.Contains("Body"))
					{
						//change the body color
						vRenderer.color = vNewBodyColor;
						vRenderer.sortingLayerName = "Other";
						vRenderer.flipX = flipX;
						vRenderer.flipY = flipY;
					}
					else if (vRenderer != null && child.name.Contains("Border"))
					{
						//change the border color
						vRenderer.color = vNewBorderColor;
						vRenderer.sortingLayerName = "Other";
						vRenderer.flipX = flipX;
						vRenderer.flipY = flipY;
					} 
					else if (vTextMesh != null && child.name.Contains("Message"))
					{
						//change the message and show it in front of everything
						vTextMesh.color = vNewFontColor;
						vTextMesh.text = vTrueMessage;
					    child.GetComponent<MeshRenderer>().sortingLayerName = "BubbleText";
						if (flipY)
							vTextMesh.transform.position = new Vector3 (vTextMesh.transform.position.x, vTextMesh.transform.position.y - 0.2f, vTextMesh.transform.position.z);
					}
				}
				
				vCurrentBubble = vBubbleObject; //attach it to the player
				vBubbleObject.transform.parent = transform; //make him his parent
			}
	}	

	void OnTriggerEnter2D(Collider2D other) 
	{
		if  (other.gameObject.tag == "Player")
		{
			ShowBubble();
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		
		if  (other.gameObject.tag == "Player")
		{
			if (vCurrentBubble != null) {
				vCurrentBubble.GetComponent<Appear> ().Disable ();
			}
		}
	}
}
