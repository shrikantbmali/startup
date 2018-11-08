using System;

namespace Todoist.Data
{
    internal struct ToDoItem
    {
        public string Title { get; set; }
        public string Desciption { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime ReminderTime { get; set; }
    }
}