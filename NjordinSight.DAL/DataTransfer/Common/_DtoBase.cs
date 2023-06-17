﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NjordinSight.DataTransfer.Common
{
    /// <summary>
    /// Base class impelmenting <see cref="INotifyPropertyChanged"/> for inheriting DTO 
    /// classes.
    /// </summary>
    public abstract class DtoBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes the <see cref="PropertyChangedEventHandler"/> assigned to <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property whose setter is invoking the event.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}