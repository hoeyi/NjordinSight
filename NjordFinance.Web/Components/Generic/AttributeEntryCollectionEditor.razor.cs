﻿using NjordFinance.Model;
using NjordFinance.ModelService;
using System.Collections.Generic;
using NjordFinance.Model.ViewModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Components.Web;

namespace NjordFinance.Web.Components.Generic
{
    /// <summary>
    /// Base component for pages responsible for performaning CRUD operations on a collection 
    /// of <typeparamref name="TModelChild"/> as children of <typeparamref name="TModel"/> using 
    /// <typeparamref name="TViewModelParent"/> and <typeparamref name="TViewModelChild"/> view models.
    /// </summary>
    /// <typeparam name="TViewModelParent">The view model representing the 
    /// <typeparamref name="TModel"/> entity.</typeparam>
    /// <typeparam name="TViewModelChild">The view model that is a grouping of 
    /// <typeparamref name="TModelChild"/> entries.</typeparam>
    /// <typeparam name="TModel">The entity that is parent of this attribute entry collection.
    /// </typeparam>
    /// <typeparam name="TModelChild">The entity that represents a single entry in this collection.
    /// </typeparam>
    public partial class AttributeEntryCollectionEditor<
        TViewModelParent, TViewModelChild, TModel, TModelChild>
        : LocalizableComponent
        where TModel: class, new()
        where TModelChild : class, new()
        where TViewModelChild: IAttributeEntryGrouping<TModel, TModelChild>
        where TViewModelParent: IAttributeEntryViewModel<TModel, TModelChild, TViewModelChild>
    {
        /// <summary>
        /// Gets or sets <see cref="IReferenceDataService"/> used to query lookup and reference 
        /// data for this component.
        /// </summary>
        [Inject]
        IReferenceDataService ReferenceData { get; set; }

        /// <summary>
        /// Gets or sets the current <typeparamref name="TViewModelChild"/> instance.
        /// </summary>
        protected TViewModelChild? CurrentViewModelChild { get; set; }

        /// <summary>
        /// Gets the string codes representing the allowable <see cref="ModelAttribute"/> selections 
        /// for the <typeparamref name="TViewModelParent"/> instance worked using this component.
        /// </summary>
        protected string[] SupportedModelAttributeScopes { get; } = 
            IReferenceDataService
                .GetSupportedAttributeScopeCodes<TViewModelParent>();

        /// <summary>
        /// Gets or sets the valid entries to the currently selected attribute.
        /// </summary>
        protected IEnumerable<LookupModel> CurrentAttributeMemberLookup { get; set; }

        /// <summary>
        /// Gets or sets the allowable model attributes for this attribute entry view model.
        /// </summary>
        protected IEnumerable<ModelAttribute> AllowableModelAttributes { get; set; }

        /// <summary>
        /// Gets or sets whether the modal editor for the current <typeparamref name="TViewModelChild"/>  
        /// is drawn. Default is <see cref="false"/>.
        /// </summary>
        protected bool DrawViewModelChildModelEditor { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

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
                .WithDirectRelationship(a => a.ModelAttributeScopes);

            var attributeQuery = queryBuilder.SelectWhereAsync(
                predicate: attr => attr.ModelAttributeScopes.Any(
                    msc => SupportedModelAttributeScopes.Contains(msc.ScopeCode)),
                maxCount: 0);

            return await attributeQuery;
        }

        /// <summary>
        /// Adds a new <typeparamref name="TViewModelChild"/> to the <typeparamref name="TViewModelParent"/> 
        /// instance for this component for the given <see cref="ModelAttribute"/>.
        /// </summary>
        /// <param name="forModelAttribute"></param>
        /// <returns></returns>
        protected async Task AddEntryForGrouping(ModelAttribute forModelAttribute)
        {
            await OnChildViewSelect(ViewModel.AddNew(forAttribute: forModelAttribute));
        }

        /// <summary>
        /// Moves focus of the omponent to a view for the given <typeparamref name="TViewModelChild"/> 
        /// instance.
        /// </summary>
        /// <param name="childViewModel"></param>
        /// <returns></returns>
        protected async Task OnChildViewSelect(TViewModelChild childViewModel)
        {
            using var queryBuilder = ReferenceData
                .CreateQueryBuilder<ModelAttributeMember>()
                .WithDirectRelationship(a => a.Country);

            CurrentViewModelChild = childViewModel;
            CurrentAttributeMemberLookup = (await queryBuilder.SelectWhereAsync(a =>
                 a.AttributeId == childViewModel.ParentAttribute.AttributeId))
                .ToLookups(displaySelector: x =>
                {
                    if (x.Country is null)
                        return x.DisplayName;
                    else
                        return $"{x.Country.DisplayName} ({x.Country.IsoCode3})";
                });

            DrawViewModelChildModelEditor = 
                CurrentViewModelChild is not null && CurrentAttributeMemberLookup is not null;
        }
                
        /// <summary>
        /// Handles the close event of the modal editor form for a 
        /// <typeparamref name="TViewModelChild"/> instance.
        /// </summary>
        /// <param name="modalEventArgs">The event arguments.</param>
        /// <exception cref="NotSupportedException"></exception>
        protected void OnModalEditor_ForChildView_Close(
            ModalEventArgs<TViewModelChild> modalEventArgs)
        {
            DrawViewModelChildModelEditor = false;

            switch (modalEventArgs.Result)
            {
                case DialogResult.Delete:
                    ViewModel.RemoveExising(modalEventArgs.Model);
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    break;
                case DialogResult.Cancel:
                    break;
                default:
                    throw new NotSupportedException();
            };

            StateHasChanged();
        }
    }
}