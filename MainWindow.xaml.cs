using System;
using System.Collections.Generic;
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
using PRACT2.Model;

namespace PRACT2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        NoteManager noteManager=new NoteManager();
        public MainWindow()
        {
            InitializeComponent();
            noteManager=NoteManager.LoadFromFile("notes.json");            
            listBox.ItemsSource = noteManager.DateNotes;
            listBox.DisplayMemberPath = "Date";

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listBox.SelectedItem == null) return;
            DateNote dateNote= listBox.SelectedItem as DateNote;            
            tbDescribe.Text = dateNote.Note.Description;
            tbTitle.Text = dateNote.Note.Title;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DateNote dateNote = listBox.SelectedItem as DateNote;
            dateNote.Note.Description = tbDescribe.Text;
            dateNote.Note.Title = tbTitle.Text;
            noteManager.SaveToFile("notes.json");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem==null) return;
            DateNote dateNote = listBox.SelectedItem as DateNote;
            noteManager.RemoveNote(dateNote);
            noteManager.SaveToFile("notes.json");
            //Обновляем список на экране
            listBox.ItemsSource = null;
            listBox.ItemsSource = noteManager.DateNotes;
            listBox.DisplayMemberPath = "Date";
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {

            noteManager.AddNote(dpDate.DisplayDate, new Note() { Description = tbDescribe.Text, Title = tbTitle.Text });
            noteManager.SaveToFile("notes.json");
            //Обновляем список на экране
            listBox.ItemsSource = null;
            listBox.ItemsSource = noteManager.DateNotes;
            listBox.DisplayMemberPath = "Date";
        }
    }
}
