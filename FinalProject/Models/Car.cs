using HelixToolkit.Wpf;
using FinalProject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace FinalProject.Models
{
    class Car : BaseNotifyPropertyChanged
    {
        string manufacturer;
        string assembly;
        string info;
        string pathTo3D;
        string model;
        string bodyStyle;
        string carClass;
        public string Manufacturer
        {
            get => manufacturer;
            set
            {
                manufacturer = value;
                Notify();
            }
        }//BMW
        public string Assembly
        {
            get => assembly;
            set
            {
                assembly = value;
                Notify();
            }
        }          //Germany
        public string CarClass
        {
            get => carClass;
            set
            {
                carClass = value;
                Notify();
            }
        }         //Sports car(S)
        public string BodyStyle
        {
            get => bodyStyle;
            set
            {
                bodyStyle = value;
                Notify();
            }
        }          //2-door coupé
        public string Model
        {
            get => model;
            set
            {
                model = value;
                Notify();
            }
        }           //all-wheel-drive
        public string Info
        {
            get => info;
            set
            {
                info = value;
                Notify();
            }
        }
        public ModelVisual3D modelV3D;
        public ModelVisual3D ModelV3D
        {
            get; set;
        }

        public string PathTo3D
        {
            get => pathTo3D;
            set
            {
                pathTo3D = value;
                Notify();
            }
        }
        public override string ToString()
        {
            return $"{Manufacturer} {Model}";
        }
        public void Get3D()
        {
            try
            {
                ModelImporter importer = new ModelImporter();
                Model3DGroup model3DGroup = new Model3DGroup();
                model3DGroup.Children.Add(importer.Load(PathTo3D));

                ModelVisual3D modelVisual3D = new ModelVisual3D();
                modelVisual3D.Content = model3DGroup;
                modelV3D = null;
                modelV3D = modelVisual3D;
            }
            catch { modelV3D = null; }
        }
    }
}