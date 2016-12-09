using System;
using Xamarin.Forms;
using Syncfusion;
using Syncfusion.XlsIO;
using System.IO;

namespace Todo
{
    public partial class TodoItemPageX : ContentPage
    {
		private DateTime dateSelected;
		private TodoItem todoItemLocal;

        public TodoItemPageX()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

		}

		public TodoItemPageX(TodoItem todoItemInput)
		{
			InitializeComponent();

			this.todoItemLocal = todoItemInput;
		}

		void DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
		{
			dateSelected = datePicker.Date;
		}

        void saveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
			if (dateSelected.Year != 0001)			//make sure user selected a different date, otherwise use today's date
				todoItem.TimeStamp = dateSelected;
			todoItem.Label = todoItem.Comments + " ON " + todoItem.TimeStamp.Date.ToString("d");
			if (todoItem.Label == null)
				todoItem.Comments = "#No Name";
            App.Database.SaveItem(todoItem);
            this.Navigation.PopAsync();
		}

        void deleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            App.Database.DeleteItem(todoItem.ID);
            this.Navigation.PopAsync();
        }

		void exportClicked(object sender, EventArgs e)
		{
			DependencyService.Get<IEmail>().sendEmail(this.todoItemLocal);
		}

        void cancelClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            this.Navigation.PopAsync();
        }

    }
}
