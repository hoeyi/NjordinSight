﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NjordinSight.DataTransfer.Common
{
    public class ModelAttributeScopeDto : DtoBase
    {
        private int _attributeId;
        private string _scopeCode;

        [Display(
            Name = nameof(ModelAttributeScopeDto_SR.AttributeId_Name),
            Description = nameof(ModelAttributeScopeDto_SR.AttributeId_Description),
            ResourceType = typeof(ModelAttributeScopeDto_SR))]
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
            Name = nameof(ModelAttributeScopeDto_SR.ScopeCode_Name),
            Description = nameof(ModelAttributeScopeDto_SR.ScopeCode_Description),
            ResourceType = typeof(ModelAttributeScopeDto_SR))]
        public string ScopeCode
        {
            get { return _scopeCode; }
            set
            {
                if (_scopeCode != value)
                {
                    _scopeCode = value;
                    OnPropertyChanged(nameof(ScopeCode));
                }
            }
        }
    }
}
