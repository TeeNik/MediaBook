using System.Collections;
using System.Runtime.CompilerServices;
using UIWindow;
using UnityEngine;
using UnityEngine.Networking;

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
        SendRequest();
	}

    public void SendRequest()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}
