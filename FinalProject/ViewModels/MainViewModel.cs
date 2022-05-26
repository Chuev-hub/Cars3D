using HelixToolkit.Wpf;
using Newtonsoft.Json;
using FinalProject.Infrastructure;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Media3D;


namespace FinalProject.ViewModels
{
    class MainViewModel : BaseNotifyPropertyChanged
    {
        public ICommand AZCommand { get; private set; }
        public ICommand ZACommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public Language L { get; set; } = new Language();
        public ICommand ImportCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand ChangeThemeCommand { get; private set; }
        public ICommand ChangeLgCommand { get; private set; }
       
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }


        public ICommand OkAddCommand { get; private set; }
        public ICommand PathAddCommand { get; private set; }
        public ICommand OkEditCommand { get; private set; }
        public ICommand PathEditCommand { get; private set; }

        bool isWhiteTheme = false;
        bool isEn = true;


        MainWindow mv;
        public event PropertyChangedEventHandler PropertyChanged2;
           
        private void ChangeCar()
        {
            if(selectedCar.modelV3D == null)
            mv.model.Content = null;
            else
            mv.model.Content = SelectedCar.modelV3D.Content;
            PropertyChanged2?.Invoke(mv.model.Content, new PropertyChangedEventArgs("Content"));
            mv.h.Camera.LookDirection = new Vector3D(-2, -5, 0);
            mv.h.Camera.UpDirection = new Vector3D(0, 0, 1);
            mv.h.ZoomExtents();
        }
        public Car selectedCar;
        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                ChangeCar();
                Notify();
            }
        }
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        public MainViewModel()
        {

            L.ToEn();
            InitCommands();
            mv = System.Windows.Application.Current.MainWindow as MainWindow;
            string json = File.ReadAllText(Environment.CurrentDirectory +"\\Data\\data.json");
            Cars = JsonConvert.DeserializeObject<ObservableCollection<Car>>(json);
            
            foreach (var c in Cars)
                c.Get3D();

        }

        private void InitCommands()
        {
            ExitCommand = new RelayCommand(x => { Environment.Exit(0); });
            ImportCommand = new RelayCommand(x =>
            {

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Json|*.json";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(dlg.FileName);
                    Cars = null;
                    Cars = JsonConvert.DeserializeObject<ObservableCollection<Car>>(json);
                    foreach (var c in Cars)
                        c.Get3D();
                }
               
                mv.listBoxCars.ItemsSource = null;
                mv.listBoxCars.ItemsSource = Cars;
                
            });
            ExportCommand = new RelayCommand(x =>
            {
                    
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Json|*.json";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (var c in Cars)
                        c.ModelV3D = null;
                    foreach (var c in Cars)
                        c.modelV3D = null;
                    string json = JsonConvert.SerializeObject(Cars);
                    File.WriteAllText(dlg.FileName,json);
                    foreach (var c in Cars)
                        c.Get3D();
                }
            });
            ChangeLgCommand = new RelayCommand(x =>
            {
                isEn = !isEn;
                if (isEn)
                    L.ToEn();
                else
                    L.ToUk();
            });


            AZCommand = new RelayCommand(x =>
            {
                Car temp;
                for (int i = 0; i < Cars.Count - 1; i++)
                {
                    for (int j = i + 1; j < Cars.Count; j++)
                    {
                        if (Cars[i].Manufacturer[0] > Cars[j].Manufacturer[0])
                        {
                            temp = Cars[i];
                            Cars[i] = Cars[j];
                            Cars[j] = temp;
                        }
                    }
                }
                mv.listBoxCars.ItemsSource = null;
                mv.listBoxCars.ItemsSource = Cars;
            });


            ZACommand = new RelayCommand(x =>
            {
                Car temp;
                for (int i = 0; i < Cars.Count - 1; i++)
                {
                    for (int j = i + 1; j < Cars.Count; j++)
                    {
                        if (Cars[i].Manufacturer[0] < Cars[j].Manufacturer[0])
                        {
                            temp = Cars[i];
                            Cars[i] = Cars[j];
                            Cars[j] = temp;
                        }
                    }
                }
                mv.listBoxCars.ItemsSource = null;
                mv.listBoxCars.ItemsSource = Cars;
            });



            CancelCommand = new RelayCommand(x =>
            {
                mv.AddGrid.Visibility = Visibility.Hidden;
                mv.EditGrid.Visibility = Visibility.Hidden;
                mv.AssemblyTextBox.Text = "";
                mv.BodyStyleTextBox.Text = "";
                mv.ClassTextBox.Text = "";
                mv.AboutTextBox.Text = "";
                mv.ManufacturerTextBox.Text = "";
                mv.ModelTextBox.Text = "";
                mv.PathTextBox.Text = "";
                mv.PathTextBox.Text = "ObjectsPro\\none.obj";
            });
            AddCommand = new RelayCommand(x =>
            {
                mv.AddGrid.Visibility = Visibility.Visible;
                mv.PathTextBox.Text = "ObjectsPro\\none.obj";
            });
            PathAddCommand = new RelayCommand(x =>
            {

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Obj|*.obj";
                if (dlg.ShowDialog() == DialogResult.OK)
                    mv.PathTextBox.Text = dlg.FileName;

            });
            OkAddCommand = new RelayCommand(x =>
            {

                Car c = new Car()
                {
                    Assembly = mv.AssemblyTextBox.Text,
                    BodyStyle = mv.BodyStyleTextBox.Text,
                    CarClass = mv.ClassTextBox.Text,
                    Info = mv.AboutTextBox.Text,
                    Manufacturer = mv.ManufacturerTextBox.Text,
                    Model = mv.ModelTextBox.Text,
                    PathTo3D = mv.PathTextBox.Text
                };
                c.Get3D();
                Cars.Add(c);
                mv.AssemblyTextBox.Text = "";
                mv.BodyStyleTextBox.Text = "";
                mv.ClassTextBox.Text = "";
                mv.AboutTextBox.Text = "";
                mv.ManufacturerTextBox.Text = "";
                mv.ModelTextBox.Text = "";
                mv.PathTextBox.Text = "ObjectsPro\\none.obj";
                mv.AddGrid.Visibility = Visibility.Hidden;
            });
            PathEditCommand = new RelayCommand(x =>
            {

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Obj|*.obj";
                if (dlg.ShowDialog() == DialogResult.OK)
                    mv.EditPathTextBox.Text = dlg.FileName;

            });
            OkEditCommand = new RelayCommand(x =>
            {


                SelectedCar.Assembly = mv.EditAssemblyTextBox.Text;
                SelectedCar.BodyStyle = mv.EditBodyStyleTextBox.Text;
                SelectedCar.CarClass = mv.EditClassTextBox.Text;
                SelectedCar.Info = mv.EditAboutTextBox.Text;
                SelectedCar.Manufacturer = mv.EditManufacturerTextBox.Text;
                SelectedCar.Model = mv.EditModelTextBox.Text;
                SelectedCar.PathTo3D = mv.EditPathTextBox.Text;
                SelectedCar.Get3D();
                mv.EditAssemblyTextBox.Text = "";
                mv.EditBodyStyleTextBox.Text = "";
                mv.EditClassTextBox.Text = "";
                mv.EditAboutTextBox.Text = "";
                mv.EditManufacturerTextBox.Text = "";
                mv.EditModelTextBox.Text = "";
                mv.EditPathTextBox.Text = "";
                mv.EditGrid.Visibility = Visibility.Hidden;
                ChangeCar();
                mv.listBoxCars.ItemsSource = null;
                mv.listBoxCars.ItemsSource = Cars;
            });
            EditCommand = new RelayCommand(x => 
            {
                try
                {

                    mv.EditGrid.Visibility = Visibility.Visible;
                    mv.EditAssemblyTextBox.Text = SelectedCar.Assembly;
                    mv.EditBodyStyleTextBox.Text = SelectedCar.BodyStyle;
                    mv.EditClassTextBox.Text = SelectedCar.CarClass;
                    mv.EditAboutTextBox.Text = SelectedCar.Info;
                    mv.EditManufacturerTextBox.Text = SelectedCar.Manufacturer;
                    mv.EditModelTextBox.Text = SelectedCar.Model;
                    mv.EditPathTextBox.Text = SelectedCar.PathTo3D;
                }
                catch { mv.EditGrid.Visibility = Visibility.Hidden; }
            });
            RemoveCommand = new RelayCommand(x => 
            {
                try
                {


                    int i = Cars.IndexOf(selectedCar);
                    if (i == mv.listBoxCars.Items.Count - 1) i--;
                    selectedCar.ModelV3D = null;
                    selectedCar.modelV3D = null;
                    Cars.Remove(selectedCar);
                    mv.model.Content = null;
                    PropertyChanged2?.Invoke(mv.model.Content, new PropertyChangedEventArgs("Content"));
                    mv.listBoxCars.SelectedIndex = i;
                }
                catch { }


            });
            ChangeThemeCommand = new RelayCommand(x =>
            {
                isWhiteTheme = !isWhiteTheme;
                if (isWhiteTheme)
                    mv.Resources.Source = new Uri(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)) + "\\Themes\\White.xaml");
                else
                    mv.Resources.Source = new Uri(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)) + "\\Themes\\Black.xaml");
            });
        }
    }
}
