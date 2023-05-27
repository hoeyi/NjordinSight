﻿using Ichosys.DataModel.Annotations;
using Microsoft.AspNetCore.Components;
using NjordinSight.EntityModel;
using NjordinSight.UserInterface;
using NjordinSight.Web.Components.Shared;
using NjordinSight.Web.Controllers;
using System;
using System.Threading.Tasks;

namespace NjordinSight.Web.Components.Generic
{
    /// <summary>
    /// Base component for viewing details of a <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">The model type.</typeparam>
    public partial class ModelDetail<TModel> : ModelPage<TModel>, INavigationSource
        where TModel : class, new()
    {
        /// <summary>
        /// Gets or sets the model for which details are provided. 
        /// </summary>
        protected TModel Model { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <see cref="IController{TModel}"/> for this component.
        /// </summary>
        [Inject]
        protected IController<TModel> Controller { get; set; } = default!;

        /// <summary>
        /// Default implementation of a <see cref="EventCallback{TValue}"/> that returns
        /// a completed <see cref="Task"/>.
        /// </summary>
        /// <returns>A completed <see cref="Task"/>.</returns>
        protected virtual Task HandleValidSubmit() => Task.CompletedTask;

        /// <inheritdoc/>
        protected override MenuRoot CreateSectionNavigationMenu() 
        {
            if (Model is null || ModelNoun is null)
                throw new InvalidOperationException(
                    message: ModelDetail.CreateSectionNavigationMenu_InvalidOperationException
                        .Format(nameof(Model), nameof(ModelNoun)));

            string editCaption = Strings.Caption_EditSingle.Format(ModelNoun?.GetSingular());
            string indexCaption = Strings.Caption_NavigateBackTo.Format(ModelNoun?.GetPlural());

            return new()
            {
                IconKey = "reorder-four",
                Children = new()
                {
                    // Add return to index button.
                    new MenuItem()
                    {
                        IconKey = "caret-back-circle",
                        Caption = indexCaption,
                        UriRelativePath = $"{IndexUriRelativePath}"
                    },
                    // Add edit button.
                    new MenuItem()
                    {
                        IconKey = "pencil",
                        Caption = editCaption,
                        UriRelativePath = FormatEditUri(GetKeyValueOrDefault<int>(Model))
                    }
                }
            };
        }

    }
}
