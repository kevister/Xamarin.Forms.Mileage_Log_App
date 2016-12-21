using System.Collections.Generic;

namespace Todo
{
	//interface for email dependency service
	public interface IEmail
	{
		void sendEmail(TodoItem todoItemMail);

		void sendEmail(List<TodoItem> list, DatePicked dp);
	}
}
