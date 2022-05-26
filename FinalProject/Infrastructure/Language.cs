using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure
{
    class Language : BaseNotifyPropertyChanged
    {
        public string manufacturer;
        public string model;
        public string carClass;
        public string ok;
        public string cancel;
        public string bodyStyle;
        public string about;
        public string assembly;
        public string pathTo3DModel;
        public string az;
        public string za;
        public void ToEn()
        {
            Manufacturer = "Manufacturer:";
            Model = "Model:";
            CarClass = "Class:";
            Ok = "Ok";
            Cancel = "Cancel";
            BodyStyle = "Body Style:";
            About = "About:";
            Assembly = "Assembly:";
            PathTo3DModel = "Path To 3D Model";
            az = "A-Z";
            za = "Z-A";
        }
        public void ToUk()
        {
            Manufacturer = "Виробник:";
            Model = "Модель:";
            CarClass = "Клас:";
            Ok = "Ок";
            Cancel = "Відмінити";
            BodyStyle = "Стиль кузова:";
            About = "Інформація:";
            Assembly = "Зібрали в:";
            PathTo3DModel = "Шлях до 3д моделі";
            az = "А-Я";
            za = "Я-А";
        }
        public string Az
        {
            get => az;
            set
            {
                az = value;
                Notify();
            }
        }
        public string Za
        {
            get => za;
            set
            {
                za = value;
                Notify();
            }
        }
        public string Manufacturer
        {
            get => manufacturer;
            set
            {
                manufacturer = value;
                Notify();
            }
        }
        public string Model
        {
            get => model;
            set
            {
                model = value;
                Notify();
            }
        }
        public string CarClass
        {
            get => carClass;
            set
            {
                carClass = value;
                Notify();
            }
        }
        public string Ok
        {
            get => ok;
            set
            {
                ok = value;
                Notify();
            }
        }
        public string Cancel
        {
            get => cancel;
            set
            {
                cancel = value;
                Notify();
            }
        }
        public string BodyStyle
        {
            get => bodyStyle;
            set
            {
                bodyStyle = value;
                Notify();
            }
        }
        public string About
        {
            get => about;
            set
            {
                about = value;
                Notify();
            }
        }
        public string Assembly
        {
            get => assembly;
            set
            {
                assembly = value;
                Notify();
            }
        }
        public string PathTo3DModel
        {
            get => pathTo3DModel;
            set
            {
                pathTo3DModel = value;
                Notify();
            }
        }

    }
}
