using System.Runtime.CompilerServices;
using UnityEngine;

public class DataLayer : MonoBehaviour
{

    public static DataLayer Instance;
    public Constructor Constructor;
    public XMLParser XmlParser;
    public PageController PageController;
    public TextAsset Asset;

	void Start ()
	{
	    Instance = this;
        XmlParser = new XMLParser();
        var root = XmlParser.GetRoot(Asset);
        PageController = new PageController();
        Constructor.GenerateBook(root, PageController);
	}
	
}
