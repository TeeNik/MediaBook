using System.Runtime.CompilerServices;
using UIWindow;
using UnityEngine;

public class DataLayer : MonoBehaviour
{
    public TextAsset Asset;
    [SerializeField] private Transform _bookRoot;

    public static DataLayer Instance { get; private set; }
    public Constructor Constructor { get; private set; }
    public XMLParser XmlParser { get; private set; }
    public PageController PageController { get; private set; }
    public BookResources BookResources { get; private set; }
    public CommandSubject Messages { get; private set; }
    public WindowController WindowController;
    public AuthController AuthController { get; private set; }

    void Start ()
	{
	    Instance = this;
        Messages = new CommandSubject();
        XmlParser = new XMLParser();
        BookResources = new BookResources();
        Constructor = new Constructor();
        PageController = new PageController();
        AuthController = new AuthController();
        WindowController.Init();

        var root = XmlParser.GetRoot(Asset);
        Constructor.Init(_bookRoot);
        Constructor.GenerateBook(root, PageController);
	}
	
}
