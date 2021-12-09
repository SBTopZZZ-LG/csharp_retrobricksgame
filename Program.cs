using RetroBricksGame.UI;

namespace RetroBricksGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu startMenu = new Menu();

            MenuButton startGameButton = new MenuButton("Play Game");
            startGameButton.setAction(delegate ()
            {
                startMenu.stopPrint();

                Arena game = new Arena();
                game.Play();

                startMenu.print();
            });

            MenuButton gameSettingsButton = new MenuButton("Game Settings");
            gameSettingsButton.setAction(delegate ()
            {
                Menu gameSettingsMenu = new Menu();

                MenuSelectRange arenaWidth = new MenuSelectRange("Arena Width", new Range<int>(6, 12));
                arenaWidth.setSelected(Arena.ARENA_WIDTH);
                arenaWidth.setSelectionChanged(delegate (int new_selection)
                {
                    Arena.ARENA_WIDTH = new_selection;
                });

                MenuSelectRange arenaHeight = new MenuSelectRange("Arena Height", new Range<int>(20, 35));
                arenaHeight.setSelected(Arena.ARENA_HEIGHT);
                arenaHeight.setSelectionChanged(delegate (int new_selection)
                {
                    Arena.ARENA_HEIGHT = new_selection;
                });

                MenuButton resetSettingsButton = new MenuButton("Reset settings");
                resetSettingsButton.setAction(delegate ()
                {
                    arenaWidth.setSelected(9);
                    arenaHeight.setSelected(25);
                });

                MenuButton goBackButton = new MenuButton("Go back");
                goBackButton.setAction(delegate ()
                {
                    gameSettingsMenu.stopPrint();

                    startMenu.print();
                });

                gameSettingsMenu.addElement(arenaWidth);
                gameSettingsMenu.addElement(arenaHeight);
                gameSettingsMenu.addElement(resetSettingsButton);
                gameSettingsMenu.addElement(goBackButton);

                gameSettingsMenu.print();
                startMenu.stopPrint();
            });

            MenuButton aboutDeveloperButton = new MenuButton("About Dev");
            aboutDeveloperButton.setAction(delegate ()
            {
                Menu aboutDevMenu = new Menu();

                MenuInfo myInfo = new MenuInfo("About Developer");
                myInfo.setDescription("Hi! My name is Saumitra Topinkatti.\n" +
                    "I am 19 years old, I love programming, eating, sleeping, and gaming.\n" +
                    "I'm an undergraduate student in KLS GIT, Belgaum.\n" +
                    "My favourite programming languages: C++, C#, Java, Javascript, Dart\n" +
                    "My favourite frameworks: Flutter, React.js, Firebase, MongoDB, MySQL, Node.js, Express.js");

                MenuButton linkedInButton = new MenuButton("View my LinkedIn");
                linkedInButton.setAction(delegate ()
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://www.linkedin.com/in/saumitra-topinkatti-45a577208/",
                        UseShellExecute = true
                    });
                });

                MenuButton githubButton = new MenuButton("View my GitHub");
                githubButton.setAction(delegate ()
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://github.com/SBTopZZZ-LG",
                        UseShellExecute = true
                    });
                });

                MenuButton instaButton = new MenuButton("View my Instagram");
                instaButton.setAction(delegate ()
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://www.instagram.com/__s_btop_zzz_/",
                        UseShellExecute = true
                    });
                });

                MenuButton goBackButton = new MenuButton("Go back");
                goBackButton.setAction(delegate ()
                {
                    aboutDevMenu.stopPrint();

                    startMenu.print();
                });

                aboutDevMenu.addElement(myInfo);
                aboutDevMenu.addElement(linkedInButton);
                aboutDevMenu.addElement(githubButton);
                aboutDevMenu.addElement(instaButton);
                aboutDevMenu.addElement(goBackButton);

                aboutDevMenu.print();
                startMenu.stopPrint();
            });

            MenuButton exitButton = new MenuButton("Exit");
            exitButton.setAction(delegate ()
            {
                startMenu.stopPrint();
            });

            startMenu.addElement(startGameButton);
            startMenu.addElement(gameSettingsButton);
            startMenu.addElement(aboutDeveloperButton);
            startMenu.addElement(exitButton);

            startMenu.print();
        }
    }
}
