using System.Runtime.CompilerServices;
using UnityEngine;

public class DataLayer : MonoBehaviour
{

    public static DataLayer Instance;
    public Constructor Constructor;
    public XMLParser XmlParser;

    public TextAsset Asset;

	void Start ()
	{
	    Instance = this;
        XmlParser = new XMLParser();
        XmlParser.GetNodes(Asset);
	}
	
}
