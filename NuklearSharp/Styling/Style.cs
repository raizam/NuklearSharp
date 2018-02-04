namespace NuklearSharp
{
	partial class Style
	{
		public UserFont font;
		public Cursor[] cursors = new Cursor[Nuklear.NK_CURSOR_COUNT];
		public Cursor cursor_active;
		public Cursor cursor_last;
		public int cursor_visible;
		public StyleText text = new StyleText();
		public StyleButton button = new StyleButton();
		public StyleButton contextual_button = new StyleButton();
		public StyleButton menu_button = new StyleButton();
		public StyleToggle option = new StyleToggle();
		public StyleToggle checkbox = new StyleToggle();
		public StyleSelectable selectable = new StyleSelectable();
		public StyleSlider slider = new StyleSlider();
		public StyleProgress progress = new StyleProgress();
		public StyleProperty property = new StyleProperty();
		public StyleEdit edit = new StyleEdit();
		public StyleChart chart = new StyleChart();
		public StyleScrollbar scrollh = new StyleScrollbar();
		public StyleScrollbar scrollv = new StyleScrollbar();
		public StyleTab tab = new StyleTab();
		public StyleCombo combo = new StyleCombo();
		public StyleWindow window = new StyleWindow();
	}
}
