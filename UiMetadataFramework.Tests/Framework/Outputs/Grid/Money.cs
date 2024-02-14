﻿// ReSharper disable UnusedMember.Global

namespace UiMetadataFramework.Tests.Framework.Outputs.Grid;

using UiMetadataFramework.Core.Binding;

[OutputFieldType("money", mandatoryCustomProperty: typeof(MoneyAttribute))]
public class Money
{
	public decimal Amount { get; set; }
	public string? Currency { get; set; }
}