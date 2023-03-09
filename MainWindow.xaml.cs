using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            if (notes[Notes_list.SelectedIndex].name != "")
            {
                notes[Notes_list.SelectedIndex].name = Name.Text;
                notes[Notes_list.SelectedIndex].description = Description.Text;
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
            if (notes[Notes_list.SelectedIndex].name != "")
            {
                note.name = Name.Text;
                note.description = Description.Text;
                note.date = DateTime.Parse(Date.Text);
                notes.Add(note);
            }
            Serializer.Serialize(notes);
            ViewNotes();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Note> notes = Serializer.Deserialize();

            notes.RemoveAt(Notes_list.SelectedIndex);

            Serializer.Serialize(notes);
            ViewNotes();
        }

        private void ViewNotes()
        {
            List<Note> notes = Serializer.Deserialize();
            List<String> CheckedNotes = new List<String>();            
            if (notes != null) 
            {
                CheckedNotes.Add("");
                foreach (Note note in notes)
                {
                    if (note.date == Date.SelectedDate)
                    {
                        CheckedNotes.Add(note.name);
                    }
                }
                Notes_list.ItemsSource = CheckedNotes; 
            }
        }

        private void ChangedDate(object sender, SelectionChangedEventArgs e)
        {
            ViewNotes();
        }


        private void Notes_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Note> notes = Serializer.Deserialize();
            try
            {
                Name.Text = notes[Notes_list.SelectedIndex].name.ToString();
                Description.Text = notes[Notes_list.SelectedIndex].description.ToString();
            }
            catch { }
        }
    }
}
