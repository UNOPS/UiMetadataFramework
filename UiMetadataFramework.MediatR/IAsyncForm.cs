﻿namespace UiMetadataFramework.MediatR
{
	using global::MediatR;
	using UiMetadataFramework.Core;

	public interface IAsyncForm<in TRequest, TResponse> :
		IAsyncRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : FormResponse
	{
	}
}