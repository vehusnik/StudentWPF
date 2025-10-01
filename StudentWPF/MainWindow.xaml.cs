using StudentWPF.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentContext _db = new StudentContext();

        private readonly ObservableCollection<Student> _students = new ObservableCollection<Student>();

        private ICollectionView _studentsView;


        public MainWindow()
        {
            InitializeComponent();
            _db.EnsureCreatedAndSeed();

            foreach (var s in _db.Students.OrderBy(x => x.Id).ToList())
            {
                _students.Add(s);
            }

            _studentsView = CollectionViewSource.GetDefaultView(_students);
            _studentsView.SortDescriptions.Clear();
            _studentsView.SortDescriptions.Add(new SortDescription(nameof(Student.Id), ListSortDirection.Ascending));

            StudentsGrid.ItemsSource = _studentsView;
        }
        protected override void OnClosed(System.EventArgs e)
        {
            _db.Dispose();
            base.OnClosed(e);
        }
        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            string firstName = (TxtFirstName.Text ?? string.Empty).Trim();
            string lastName = (TxtLastName.Text ?? string.Empty).Trim();
            string email = (TxtEmail.Text ?? string.Empty).Trim();
            int year = 1;
            int.TryParse((TxtYear.Text ?? string.Empty).Trim(), out year);

            var s = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                year = year,
                Email = email
                
            };
            _db.Students.Add(s);
            _db.SaveChanges();

            _students.Add(s);
            StudentsGrid.SelectedItem = s;
            StudentsGrid.ScrollIntoView(s);

            TxtFirstName.Text = string.Empty;
            TxtLastName.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtYear.Text = string.Empty;
            }
            private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
        }
        private void BtnDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            
            if (StudentsGrid.SelectedItem is Student s)
            {
                var result = MessageBox.Show(this, $"Do you really want to delete student {s.FirstName} {s.LastName}?", "Delete student", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _db.Students.Remove(s);
                    _db.SaveChanges();
                    _students.Remove(s);
                }
            }
        }
    }
}