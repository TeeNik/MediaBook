using System.Collections.Generic;

public class PageController
{
    private List<PageElement> _pages;
    private int _current;

    public PageController()
    {
        _pages = new List<PageElement>();
    }

    public void AddPage(PageElement page)
    {
        _pages.Add(page);
    }

    public void NextPage()
    {
        OpenPage(_current + 1);
    }

    public void PrevPage()
    {
        OpenPage(_current - 1);
    }

    public void OpenPage(int number)
    {
        Assert.Inv(number < _pages.Count, "number < _pages.Count");
        Assert.Inv(number >= 0, "number >= 0");

        _pages[_current].gameObject.SetActive(false);
        _current = number;
        _pages[_current].gameObject.SetActive(true);
    }

    public void OpenPageById(string id)
    {
        Assert.Inv(id != null, "id != null");
        _pages[_current].gameObject.SetActive(false);
        var page = _pages.Find(p => p.Id == id);
        Assert.Inv(page != null, $"Page with id: {id} not found");
        _current = _pages.IndexOf(page);
        _pages[_current].gameObject.SetActive(true);
    }

}
