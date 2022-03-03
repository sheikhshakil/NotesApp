using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace NotesAppServer.Repository
{
    public class NotesRepository
    {
        private static List<Dictionary<string, string>> Notes = new List<Dictionary<string, string>>();

        public static bool SaveNote(object note)
        {
            try
            {
                Notes.Add(ConvertNoteToDict(note));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetNotes(string email, string filter = "")
        {
            List<Dictionary<string, string>> filteredNotes = new List<Dictionary<string, string>>();

            if (filter.Length == 0)
            {
                for (int i = 0; i < Notes.Count; i++)
                {
                    if (Convert.ToString(Notes[i]["Email"]).Equals(email))
                    {
                        filteredNotes.Add(Notes[i]);
                    }
                }

                return JsonConvert.SerializeObject(filteredNotes);
            }
            else
            {
                if (filter.Equals("day"))
                {
                    for (int i = 0; i < Notes.Count; i++)
                    {
                        if (Notes[i]["Email"].Equals(email))
                        {
                            if (Notes[i]["Type"].Equals("reminder"))
                            {
                                string noteDateTime = Notes[i]["DateTime"];
                                if (MatchDate(noteDateTime))
                                {
                                    filteredNotes.Add(Notes[i]);
                                }
                            }
                            if (Notes[i]["Type"].Equals("todo"))
                            {
                                string noteDue = Notes[i]["Due"];
                                if (MatchDate(noteDue))
                                {
                                    filteredNotes.Add(Notes[i]);
                                }
                            }
                        }
                    }
                    return JsonConvert.SerializeObject(filteredNotes);
                }
                else if (filter.Equals("week"))
                {
                    for (int i = 0; i < Notes.Count; i++)
                    {
                        if (Notes[i]["Email"].Equals(email))
                        {
                            if (Notes[i]["Type"].Equals("reminder"))
                            {
                                string noteDateTime = Notes[i]["DateTime"];
                                if (MatchWeek(noteDateTime))
                                {
                                    filteredNotes.Add(Notes[i]);
                                }
                            }
                            if (Notes[i]["Type"].Equals("todo"))
                            {
                                string noteDue = Notes[i]["Due"];
                                if (MatchWeek(noteDue))
                                {
                                    filteredNotes.Add(Notes[i]);
                                }
                            }
                        }
                    }
                    return JsonConvert.SerializeObject(filteredNotes);
                }
                else if (filter.Equals("month"))
                {
                    for (int i = 0; i < Notes.Count; i++)
                    {
                        if (Notes[i]["Email"].Equals(email))
                        {
                            if (Notes[i]["Type"].Equals("reminder"))
                            {
                                string noteDateTime = Notes[i]["DateTime"];
                                if (MatchMonth(noteDateTime))
                                {
                                    filteredNotes.Add(Notes[i]);
                                }
                            }
                            if (Notes[i]["Type"].Equals("todo"))
                            {
                                string noteDue = Notes[i]["Due"];
                                if (MatchMonth(noteDue))
                                {
                                    filteredNotes.Add(Notes[i]);
                                }
                            }
                        }
                    }
                    return JsonConvert.SerializeObject(filteredNotes);
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool UpdateTodoStatus(string noteId)
        {
            for (int i = 0; i < Notes.Count; i++)
            {
                if (Notes[i]["Id"].Equals(noteId))
                {
                    Notes[i]["IsDone"] = "true";
                    return true;
                }
            }
            return false;
        }

        private static bool MatchDate(string noteDateTime)
        {
            DateTime date = Convert.ToDateTime(noteDateTime).Date;
            if (date.Equals(DateTime.Now.Date))
            {
                return true;
            }
            return false;
        }

        private static bool MatchWeek(string noteDateTime)
        {
            DateTime datetime = Convert.ToDateTime(noteDateTime);
            int noteWeekNum = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(datetime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            int todayWeekNum = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            if (noteWeekNum.Equals(todayWeekNum))
            {
                return true;
            }
            return false;
        }

        private static bool MatchMonth(string noteDateTime)
        {
            int month = Convert.ToDateTime(noteDateTime).Month;
            if (month.Equals(DateTime.Now.Month))
            {
                return true;
            }
            return false;
        }

        private static Dictionary<string, string> ConvertNoteToDict(object note)
        {
            string json = JsonConvert.SerializeObject(note);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public static int GetNotesCount()
        {
            return Notes.Count;
        }
    }
}
