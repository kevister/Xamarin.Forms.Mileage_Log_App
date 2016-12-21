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

		//when clicked, stores the date selected by the user
		void DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
		{
			dateSelected = datePicker.Date;
		}

		//when clicked, save the item
		void saveClicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			if (dateSelected.Year != 0001)          //make sure user selected a different date, otherwise use today's date
				todoItem.TimeStamp = dateSelected;
			todoItem.Label = todoItem.Comments + " ON " + todoItem.TimeStamp.Date.ToString("d");
			if (todoItem.Label == null)
				todoItem.Comments = "#No Name";
			App.Database.SaveItem(todoItem);
			this.Navigation.PopAsync();
		}

		//when clicked, delete the item
		void deleteClicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Database.DeleteItem(todoItem.ID);
			this.Navigation.PopAsync();
		}

		//when clicked, export this item and this item only (note this is different from the toolbar export)
		void exportClicked(object sender, EventArgs e)
		{
			if (this.todoItemLocal == null)		//user hasn't saved the item yet
			{
				DisplayAlert("Error", "Please save the entry first.", "Okay");
			}
			else
				DependencyService.Get<IEmail>().sendEmail(this.todoItemLocal);		//delegate to dependency service in Todo.iOS
		}

		void cancelClicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			this.Navigation.PopAsync();
		}

	}
}
