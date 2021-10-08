using MOBILEAPPLICATION.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MOBILEAPPLICATION.ViewModel
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        // this class holds an emtpy note for data transfer from one view to anther or creating a new note
        Note note = new Note();
        public Note Note
        {
            get => note;
            set
            {
                if (note == value)
                    return;
                note = value;
                OnPropertyChanged(nameof(note));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
