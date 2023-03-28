using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Notes
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Note> notes = Serializer.Deserialize();    
            if (notes == null)
            {
                notes = new List<Note>();
            }
            Date.SelectedDate = DateTime.Now;
            Serializer.Serialize(notes);
            ViewNotes();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Note> notes = Serializer.Deserialize();
            try
            {
                notes[Notes_list.SelectedIndex - 1].name = Name.Text;
                notes[Notes_list.SelectedIndex - 1].description = Description.Text;
            }
            catch
            {
                ShowErrorSelect();
            }
            Serializer.Serialize(notes);
            ViewNotes();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Note> notes = Serializer.Deserialize();
            if (notes == null)
            {
                notes = new List<Note>();
            }
            Note note = new Note();
            if (string.IsNullOrEmpty(Name.Text) || string.IsNullOrWhiteSpace(Name.Text))
            {
                MessageBox.Show("Значение в поле (Название) не должно быть пустым");
            }
            else
            {
                if (SameNotes())
                {
                    note.name = Name.Text;
                    note.description = Description.Text;
                    note.date = DateTime.Parse(Date.Text);
                    notes.Add(note);
                    Serializer.Serialize(notes);
                    ViewNotes();
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Note> notes = Serializer.Deserialize();
            try { 
                notes.RemoveAt(Notes_list.SelectedIndex - 1);
            }
            catch
            {
                ShowErrorSelect();
            }
            Serializer.Serialize(notes);
            ViewNotes();
        }

        private void ViewNotes()
        {
            List<Note> notes = Serializer.Deserialize();
            List<String> CheckedNotes = new List<String>();
            if (notes != null)
            {
                CheckedNotes.Add(" ");
                foreach (Note note in notes)
                {
                    if (note.date == Date.SelectedDate)
                    {
                        CheckedNotes.Add(note.name);
                    }
                }
                Notes_list.ItemsSource = CheckedNotes;
                Description.Text = null;
                Name.Text = null; 
            }

        }

        private void ChangedDate(object sender, SelectionChangedEventArgs e)
        {
            ViewNotes();
        }

        private bool SameNotes()
        {
            List<Note> notes = Serializer.Deserialize();
            foreach (Note note in notes)
            {
                if (note.date == Date.SelectedDate)
                {
                    if (note.name == Name.Text)
                    {
                        MessageBox.Show("Такая запись уже есть") ;
                        return false;
                    }
                }
            }
            return true;
        }

        private void Notes_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Note> notes = Serializer.Deserialize();
            try
            {
                Name.Text = notes[Notes_list.SelectedIndex - 1].name.ToString();
                Description.Text = notes[Notes_list.SelectedIndex - 1].description.ToString();
            }
            catch { }
        }

        private void ShowErrorSelect()
        {
            MessageBox.Show("Нельзя удалять/изменять пустой слот");
        }
    }
}
