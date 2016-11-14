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

        public TodoItemPageX()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

			//#region toolbar
			//ToolbarItem tbi = null;
			//if (Device.OS == TargetPlatform.iOS)
			//{
			//	tbi = new ToolbarItem("share", "link.png", () =>
			//	{
					
			//	}, 0, 0);
			//}
			//#endregion
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
			if (todoItem.Comments == null)
				todoItem.Comments = "#No Comments";
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
			//Create an instance of ExcelEngine.
			using (ExcelEngine excelEngine = new ExcelEngine())
			{
				//Set the default application version as Excel 2013.
				excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;

				//Create a workbook with a worksheet
				IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

				//Access first worksheet from the workbook instance.
				IWorksheet worksheet = workbook.Worksheets[0];

				//Enabling formula calculation.
				worksheet.EnableSheetCalculations();

				worksheet["A1"].Text = "Items";
				worksheet["B1"].Text = "Quantity";
				worksheet["C1"].Text = "Rate";
				worksheet["D1"].Text = "Taxes";
				worksheet["E1"].Text = "Amount";

				//Set the column width in points.
				worksheet["A1:E1"].ColumnWidth = 10;

				//Set the style for header range.
				IRange headingRange = worksheet["A1:E1"];
				headingRange.CellStyle.Font.Bold = true;
				headingRange.CellStyle.ColorIndex = ExcelKnownColors.Light_green;

				worksheet["A2"].Text = "Product A";
				worksheet["A3"].Text = "Product B";
				worksheet["A4"].Text = "Product C";

				worksheet["B2"].Number = 2;
				worksheet["B3"].Number = 1;
				worksheet["B4"].Number = 1;

				//Applying Number formats to the specified range
				worksheet["C2:E4"].NumberFormat = "$##,##0.00";

				worksheet["C2"].Number = 99.00;
				worksheet["C3"].Number = 199.00;
				worksheet["C4"].Number = 149.00;

				//Applying formulae
				worksheet["D2:D4"].FormulaR1C1 = "=(RC[-2]*RC[-1])*0.07";

				worksheet["E2:E4"].FormulaR1C1 = "=(RC[-3]*RC[-2])+RC[-1]";

				//Save the workbook to stream in xlsx format. 
				MemoryStream stream = new MemoryStream();
				workbook.SaveAs(stream);

				workbook.Close();

				//Save the stream as a file in the device and invoke it for viewing
				Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("GettingStared.xlsx", "application/msexcel", stream);
			}
			
		}

        void cancelClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            this.Navigation.PopAsync();
        }


        //void speakClicked(object sender, EventArgs e)
        //{
        //    var todoItem = (TodoItem)BindingContext;
        //    DependencyService.Get<ITextToSpeech>().Speak(todoItem.SO + " " + todoItem.EO);
        //}

    }
}
