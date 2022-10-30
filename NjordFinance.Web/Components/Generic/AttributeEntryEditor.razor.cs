﻿using Microsoft.AspNetCore.Components;
using NjordFinance.Model;
using NjordFinance.Model.ViewModel.Generic;
using NjordFinance.ModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NjordFinance.Web.Components.Generic
{
    public partial class AttributeEntryEditor<
        TViewModelParent, TViewModelChild, TModel, TModelChild>
        : LocalizableComponent
        where TModel : class, new()
        where TModelChild : class, new()
        where TViewModelChild : IAttributeEntryUnweightedGrouping<TModel, TModelChild>
        where TViewModelParent : IAttributeEntryUnweightedCollection<TModel, TModelChild, TViewModelChild>
    {
        /// <summary>
        /// Gets or sets <see cref="IReferenceDataService"/> used to query lookup and reference 
        /// data for this component.
        /// </summary>
        [Inject]
        IReferenceDataService ReferenceData { get; set; }

        /// <summary>
        /// Gets or sets the allowable model attributes for this attribute entry view model.
        /// </summary>
        protected IEnumerable<ModelAttribute> AllowableModelAttributes { get; set; }

        /// <summary>
        /// Gets the string codes representing the allowable <see cref="ModelAttribute"/> selections 
        /// for the <typeparamref name="TViewModelParent"/> instance worked using this component.
        /// </summary>
        protected string[] SupportedModelAttributeScopes { get; } =
            IReferenceDataService.GetSupportedAttributeScopeCodes<TViewModelParent>();

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;

            AllowableModelAttributes = await GetSupportedAttributesAsync();

            IsLoading = AllowableModelAttributes is null;
        }

        /// <summary>
        /// Gets the collection of allowable <see cref="ModelAttribute"/> instances for describing 
        /// the <typeparamref name="TModel"/> instance worked by this component.
        /// </summary>
        /// <returns></returns>
        protected async Task<IEnumerable<ModelAttribute>> GetSupportedAttributesAsync()
        {
            using var queryBuilder = ReferenceData
                .CreateQueryBuilder<ModelAttribute>()
                .WithDirectRelationship(a => a.ModelAttributeMembers)
                .WithDirectRelationship(a => a.ModelAttributeScopes);

            var attributeQuery = queryBuilder.SelectWhereAsync(
                predicate: attr => attr.ModelAttributeScopes.Any(
                    msc => SupportedModelAttributeScopes.Contains(msc.ScopeCode)),
                maxCount: 0);

            return await attributeQuery;
        }

        private IEnumerable<LookupModel> GetAttributeMembers(
            ModelAttribute modelAttribute) =>
            AllowableModelAttributes
                .FirstOrDefault(a => a.AttributeId == modelAttribute.AttributeId)
                .ModelAttributeMembers
                .ToLookups();

        /// <summary>
        /// Gets or sets whether the modal dialog for selecting an attribute is drawn. Default is
        /// <see cref="false" />.
        /// </summary>
        private bool DrawAttributeSelectorDialog { get; set; } = false;

        /// <summary>
        /// Gets the <see cref="Guid"/> uniquely identifying the form element for this control.
        /// </summary>
        private Guid FormGuid { get; } = Guid.NewGuid();

        /// <summary>
        /// Handles the close event of the modal form used to select a <see cref="ModelAttribute"/>
        /// for adding a new <typeparamref name="TViewModelChild" />.
        /// </summary>
        private void OnModalAttributeSelectorClosed(ModalEventArgs<ModelAttribute> modalEventArgs)
        {
            DrawAttributeSelectorDialog = false;

            switch (modalEventArgs.Result)
            {
                case DialogResult.OK:
                    if (modalEventArgs.Model is not null)
                        AddEntryForGrouping(modalEventArgs.Model);
                    break;
                case DialogResult.Cancel:
                    break;
                default:
                    throw new NotSupportedException();
            }

            // Redraw the component.
            StateHasChanged();
        }

        /// <summary>
        /// Adds a new <typeparamref name="TViewModelChild"/> to the <typeparamref name="TViewModelParent"/>
        /// instance for this component for the given <see cref="ModelAttribute"/>.
        /// </summary>
        /// <param name="forModelAttribute"></param>
        /// <returns></returns>
        protected void AddEntryForGrouping(ModelAttribute forModelAttribute)
        {
            ViewModel.AddEntryForGrouping(forModelAttribute);
        }
    }
}