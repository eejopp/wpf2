using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
namespace PRACT2.Model
{
    public class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class DateNote
    {
        public DateTime Date { get; set; }
        public Note Note { get; set; }

        public DateNote(DateTime date, Note note)
        {
            Date = date;
            Note = note;
        }
    }

    public class NoteManager
    {
        public List<DateNote> DateNotes { get; set; }

        public NoteManager()
        {
            DateNotes = new List<DateNote>();
        }

        public void AddNote(DateTime date, Note note)
        {
            DateNotes.Add(new DateNote(date, note));
        }

        public void UpdateNote(DateTime date, Note existingNote, Note updatedNote)
        {
            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;

        }

        public void RemoveNote(DateNote dateNote)
        {
            DateNotes.Remove(dateNote);           
        }

        public void SaveToFile(string filePath)
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, json);
        }

        public static NoteManager LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<NoteManager>(json);
        }
    }
}