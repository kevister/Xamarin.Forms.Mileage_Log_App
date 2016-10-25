using System;
using Xamarin.Forms;

namespace Todo
{
    public partial class TodoItemPageX : ContentPage
    {
		private DateTime dateSelected;

        public TodoItemPageX()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);
        }

		void DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
		{
			dateSelected = datePicker.Date;
		}

        void saveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
			todoItem.TimeStamp = dateSelected;
            App.Database.SaveItem(todoItem);
            this.Navigation.PopAsync();
		}

        void deleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            App.Database.DeleteItem(todoItem.ID);
            this.Navigation.PopAsync();
        }

        void cancelClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            this.Navigation.PopAsync();
        }

        void speakClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            DependencyService.Get<ITextToSpeech>().Speak(todoItem.SO + " " + todoItem.EO);
        }

    }
}
