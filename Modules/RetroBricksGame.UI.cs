using System;
using RetroBricksGame.UI.Abstract;

namespace RetroBricksGame.UI
{
    class MenuInfo : MenuInteract
    {
        private string description;

        public override int getId()
        {
            return 3;
        }

        public MenuInfo(string title) : base(title) {}
        public MenuInfo(string title, string description) : base(title)
        {
            this.description = description;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }
        public string getDescription()
        {
            return description;
        }
    }
    class MenuButton : MenuInteract
    {
        private Action action;

        public override int getId()
        {
            return 0;
        }

        public MenuButton(string title) : base(title) { }
        public MenuButton(string title, Action action) : base(title)
        {
            this.action = action;
        }

        public void setAction(Action action)
        {
            this.action = action;
        }
        public Action GetAction()
        {
            return action;
        }
    }
    class Range<T>
    {
        private T start, end;

        public Range(T start, T end)
        {
            this.start = start;
            this.end = end;
        }

        public void setStart(T start)
        {
            this.start = start;
        }
        public T getStart()
        {
            return start;
        }

        public void setEnd(T end)
        {
            this.end = end;
        }
        public T getEnd()
        {
            return end;
        }
    }
    class MenuSelectRange : MenuInteract
    {
        private Range<int> range;
        private int selected;
        private Action<int> selectionChanged;

        public override int getId()
        {
            return 1;
        }

        public MenuSelectRange(string title, Range<int> range) : base(title)
        {
            this.range = range;
            selected = range.getStart();
        }

        public void setSelectionChanged(Action<int> selectionChanged)
        {
            this.selectionChanged = selectionChanged;
        }
        public Action<int> getSelectionChanged()
        {
            return selectionChanged;
        }

        public void setSelected(int selected)
        {
            if (selected < range.getStart() || selected > range.getEnd())
                return;

            this.selected = selected;

            if (selectionChanged != null)
                selectionChanged(selected);
        }
        public int getSelected()
        {
            return selected;
        }

        public void setRange(Range<int> range)
        {
            this.range = range;
            selected = range.getStart();
        }
        public Range<int> getRange()
        {
            return range;
        }
    }
    class MenuSelect : MenuInteract
    {
        string[] options;
        int selected;
        private Action<int> selectionChanged;

        public override int getId()
        {
            return 2;
        }

        public MenuSelect(string title) : base(title) { }
        public MenuSelect(string title, params string[] options) : base(title)
        {
            this.options = new string[options.Length];
            Array.Copy(options, this.options, options.Length);

            selected = 0;
        }

        public void setSelectionChanged(Action<int> selectionChanged)
        {
            this.selectionChanged = selectionChanged;
        }
        public Action<int> getSelectionChanged()
        {
            return selectionChanged;
        }

        public void setOptions(params string[] options)
        {
            this.options = new string[options.Length];
            Array.Copy(options, this.options, options.Length);

            selected = 0;
        }
        public string[] getOptions()
        {
            return options;
        }

        public void setSelected(int selected)
        {
            if (selected < 0 || selected >= options.Length)
                return;

            this.selected = selected;

            if (selectionChanged != null)
                selectionChanged(selected);
        }
        public int getSelected()
        {
            return selected;
        }
    }
}