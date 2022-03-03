using System;

namespace NotesAppServer.Models
{
    public class Note
    {
        public class RegularNote
        {
            public string Type { get; set; }
            public int Id { get; set; }
            public string Email { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public DateTime SavedOn { get; set; }
        }

        public class ReminderNote
        {
            public string Type { get; set; }
            public int Id { get; set; }
            public string Email { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public DateTime DateTime { get; set; }
            public DateTime SavedOn { get; set; }
        }

        public class TodoNote
        {
            public string Type { get; set; }
            public int Id { get; set; }
            public string Email { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public DateTime Due { get; set; }
            public bool IsDone { get; set; }
            public DateTime SavedOn { get; set; }
        }

        public class BookmarkNote
        {
            public string Type { get; set; }
            public int Id { get; set; }
            public string Email { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public DateTime SavedOn { get; set; }
        }
    }
}
