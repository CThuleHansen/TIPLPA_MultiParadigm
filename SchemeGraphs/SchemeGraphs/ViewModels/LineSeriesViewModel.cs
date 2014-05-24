using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SchemeGraphs.Annotations;

namespace SchemeGraphs.ViewModels
{
    /// <summary>
    /// Model used to bind onto the view for easier verification.
    /// </summary>
    public class LineSeriesViewModel : INotifyPropertyChanged
    {
        public LineSeriesViewModel()
        {
            Function = string.Empty;
            Name = string.Empty;
            XFrom = "1";
            XTo = "5";
            Dx = "0,000001";
            Samples = "5";
        }

        #region properties for view

        private string function;
        public string Function
        {
            get { return function; }
            set { SetField(ref function, value, "Function"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value, "Name"); }
        }

        private string xfrom;
        public string XFrom
        {
            get { return xfrom; }
            set { SetField(ref xfrom, value, "XFrom"); }
        }
        
        private string xto;
        public string XTo
        {
            get { return xto; }
            set { SetField(ref xto, value, "XTo"); }
        }

        private string dx;
        public string Dx
        {
            get { return dx; }
            set { SetField(ref dx, value, "Dx"); }
        }

        private string samples;
        public string Samples
        {
            get { return samples; }
            set { SetField(ref samples, value, "Samples"); }
        }

        private bool hasDerivative;
        public bool HasDerivative
        {
            get { return hasDerivative; }
            set { SetField(ref hasDerivative, value, "HasDerivative"); }
        }

        private bool hasIntegral;
        public bool HasIntegral
        {
            get { return hasIntegral; } 
            set { SetField(ref hasIntegral, value, "HasIntegral"); }
        }

        #endregion

        #region INotifyPropertyChanged

        /// <summary>
        /// A smart general mechanism to detect changes in a property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

