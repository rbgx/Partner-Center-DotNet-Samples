﻿// -----------------------------------------------------------------------
// <copyright file="UpdateSubscription.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Samples.Subscriptions
{
    /// <summary>
    /// A scenario that updates an existing customer subscription.
    /// </summary>
    public class UpdateSubscription : BasePartnerScenario
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubscription"/> class.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        public UpdateSubscription(IScenarioContext context) : base("Update existing customer subscription", context)
        {
        }

        /// <summary>
        /// Executes the scenario.
        /// </summary>
        protected override void RunScenario()
        {
            var partnerOperations = this.Context.UserPartnerOperations;
            var customerId = this.ObtainCustomerId();
            var subscriptionId = this.ObtainSubscriptionId(customerId, "Enter the ID of the subscription to update");

            this.Context.ConsoleHelper.StartProgress("Retrieving customer subscription");
            var existingSubscription = partnerOperations.Customers.ById(customerId).Subscriptions.ById(subscriptionId).Get();
            this.Context.ConsoleHelper.StopProgress();
            this.Context.ConsoleHelper.WriteObject(existingSubscription, "Existing subscription");

            this.Context.ConsoleHelper.StartProgress("Incrementing subscription quantity");
            existingSubscription.Quantity++;
            var updatedSubscription = partnerOperations.Customers.ById(customerId).Subscriptions.ById(subscriptionId).Patch(existingSubscription);
            this.Context.ConsoleHelper.StopProgress();

            this.Context.ConsoleHelper.WriteObject(updatedSubscription, "Updated subscription");
        }
    }
}
