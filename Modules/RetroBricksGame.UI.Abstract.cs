namespace RetroBricksGame.UI.Abstract {
    public abstract class MenuInteract
    {
        protected string title;

        public abstract int getId();

        public MenuInteract(string title)
        {
            this.title = title;
        }

        public void setTitle(string title)
        {
            this.title = title;
        }
        public string getTitle()
        {
            return title;
        }
    }
}