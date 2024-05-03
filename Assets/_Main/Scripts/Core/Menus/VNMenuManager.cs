using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VNMenuManager : MonoBehaviour
{
    [SerializeField] UIConfirmationMenu uiChoiceMenu => UIConfirmationMenu.instance;

    public static VNMenuManager instance;
    public GameObject buttons;

    private MenuPage activePage = null;
    private bool isOpen = false;

    [SerializeField] private CanvasGroup root;
    [SerializeField] private MenuPage[] pages;


    private CanvasGroupController rootCG;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rootCG = new CanvasGroupController(this, root);
    }

    private MenuPage GetPage(MenuPage.PageType pageType)
    {
        return pages.FirstOrDefault(page => page.pageType == pageType);
    }

    public void OpenSavePage()
    {
        var page = GetPage(MenuPage.PageType.SaveAndLoad);
        var slm = page.anim.GetComponentInParent<SaveAndLoadPage>();
        slm.menuFunction = SaveAndLoadPage.MenuFunction.save;
        
        OpenPage(page);

    }

    public void OpenLoadPage()
    {

        var page = GetPage(MenuPage.PageType.SaveAndLoad);
        var slm = page.anim.GetComponentInParent<SaveAndLoadPage>();
        slm.menuFunction = SaveAndLoadPage.MenuFunction.load;

        OpenPage(page);
    }

    public void OpenConfigPage()
    {
        var page = GetPage(MenuPage.PageType.Config);

        OpenPage(page);
    }


    public void OpenHelpPage()
    {
        var page = GetPage(MenuPage.PageType.Help);

        OpenPage(page);
    }

    private void OpenPage (MenuPage page)
    {
        if (page == null)
            return;

        if (activePage != null && activePage != page)
            activePage.Close();

        page.Open();
        activePage = page;

        if (!isOpen)
            OpenRoot();

        //HideButton();
    }

    public void OpenRoot()
    {
        rootCG.Show();
        rootCG.SetInteractableState(true);
        isOpen = true;
    }

    public void CloseRoot()
    {
        rootCG.Hide();
        rootCG.SetInteractableState(false);
        isOpen = false;

        //ShowButton();
    }

   /* public void HideButton()
    {
        buttons.gameObject.SetActive(false);
    }

    public void ShowButton()
    {
        buttons.gameObject.SetActive(true);
    }*/

    public void Click_Home()
    {
        VN_Configuration.activeConfig.Save();

        UnityEngine.SceneManagement.SceneManager.LoadScene(_MainMenu.main_menu_scene);
    }
    
    public void Click_Dorm()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_MainMenu.dorms);
    }

    public void Click_Quit()
    {
        uiChoiceMenu.Show(
            // Title
            "Quit to desktop?", 
            // Yes
            new UIConfirmationMenu.ConfirmationButton("Yes", () => Application.Quit()), 
            // No
            new UIConfirmationMenu.ConfirmationButton("No", null));
    }
}
