﻿using System.ComponentModel.DataAnnotations;

namespace NjordinSight.DataTransfer.Common
{
    public class ModelAttributeDto : DtoBase
    {
        private int _attributeId;
        private string _displayName;

        public int AttributeId
        {
            get { return _attributeId; }
            set
            {
                if (_attributeId != value)
                {
                    _attributeId = value;
                    OnPropertyChanged(nameof(AttributeId));
                }
            }
        }

        [Display(
            Name = nameof(ModelAttributeDto_SR.DisplayName_Name),
            Description = nameof(ModelAttributeDto_SR.DisplayName_Description),
            ResourceType = typeof(ModelAttributeDto_SR))]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    OnPropertyChanged(nameof(DisplayName));
                }
            }
        }
    }
}
