﻿using Ichosys.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NjordinSight.EntityModel.Context;
using NjordinSight.EntityModel;
using NjordinSight.EntityModelService.Abstractions;

namespace NjordinSight.EntityModelService
{
    /// <summary>
    /// The class for servicing single CRUD requests against the <see cref="ReportConfiguration"/> 
    /// data store.
    /// </summary>
    internal class ReportConfigurationService : ModelService<ReportConfiguration>
    {
        /// <summary>
        /// Creates a new <see cref="ReportConfigurationService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public ReportConfigurationService(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
            Reader = new ModelReaderService<ReportConfiguration>(
                contextFactory, modelMetadata, logger);
            Writer = new ModelWriterService<ReportConfiguration>(
                contextFactory, modelMetadata, logger);
        }
    }
}