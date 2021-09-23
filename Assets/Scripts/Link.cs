using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Link : MonoBehaviour {

	public string site;
	public void OpenLinkJSPlugin() {
		#if !UNITY_EDITOR
		openWindow(site);
		#endif
	}

	
	[DllImport("__Internal")]
	private static extern void openWindow(string url);
	
}