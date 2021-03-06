namespace NServiceBus.Routing
{
    /// <summary>
    /// Determines which instance of a given endpoint a message is to be sent.
    /// </summary>
    public abstract class DistributionStrategy
    {
        /// <summary>
        /// Creates a new <see cref="DistributionStrategy"/>.
        /// </summary>
        /// <param name="endpoint">The name of the endpoint this distribution strategy resolves instances for.</param>
        /// <param name="scope">The scope for this strategy.</param>
        protected DistributionStrategy(string endpoint, DistributionStrategyScope scope)
        {
            Guard.AgainstNullAndEmpty(nameof(endpoint), endpoint);

            Endpoint = endpoint;
            Scope = scope;
        }

        /// <summary>
        /// Selects a destination instance for a message from all known addresses of a logical endpoint.
        /// </summary>
        public abstract string SelectReceiver(string[] receiverAddresses);

        /// <summary>
        /// Selects a destination instance for a message from all known addresses of a logical endpoint.
        /// </summary>
        /// <remarks>When this method is overridden, do not call the base method. The base method implementation calls into <see cref="SelectReceiver"/> for backward compatibility reasons.</remarks>
        public virtual string SelectDestination(DistributionContext context)
        {
            return SelectReceiver(context.ReceiverAddresses);
        }

        /// <summary>
        /// The name of the endpoint this distribution strategy resolves instances for.
        /// </summary>
        public string Endpoint { get; }

        /// <summary>
        /// The scope of this strategy.
        /// </summary>
        public DistributionStrategyScope Scope { get; }
    }
}