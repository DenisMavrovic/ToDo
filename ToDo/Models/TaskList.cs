using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
	public class TaskList
	{
		public int Id { get; set; }

		[ForeignKey("TaskId")]
		public List<Task>? Task { get; set; }
	}
}
