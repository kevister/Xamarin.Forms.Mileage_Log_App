using System.Collections.Generic;

namespace Todo
{
	public interface IEmail
	{
		void sendEmail(TodoItem todoItemMail);

		void sendEmail(List<TodoItem> list, DatePicked dp);
	}
}
