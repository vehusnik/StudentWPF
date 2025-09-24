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

            foreach (var s in _db.Students.OrderBy(x=>x.Id).ToList())
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
    }
}