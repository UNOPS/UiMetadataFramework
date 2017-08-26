# UI Metadata Framework

UI Metadata Framework is a new way to build applications, which can *significantly* reduce the amount of effort required for building presentation layer. UI Metadata Framework is **client-server communication protocol, which allows client to generate user interfaces based on metadata provided by the server**. UI Metadata Framework's goals are

* to significantly reduce amount of effort required to develop application's UI
* to fully decouple client and server code and to enable development of general-purpose clients, which can generate UI for any server supporting UI Metadata Framework
* to allow application developers to target multiple platforms (web, android, ios, etc) with almost zero effort
* be language and platform agnostic

The core idea of UI Metadata Framework is that the server will send both data and metadata. The metadata will describe how to render that data.

The protocol has a couple of core concepts:

* Form - server-side function with metadata for its inputs and outputs.
* Form metadata - metadata for the *form*
* Form response - a combination of form's output (i.e. - data) and metadata describing how to process that output
* Input field - input parameter for the form. Form can have multiple input parameters, thus multiple input fields.
* Output field - output from the form. Form can have multiple output fields.
* Input field metadata - metadata describing how to render the input field.
* Output field metadata - metadata describing how to render the output field.
* Input field processor - a client-side function which can be invoked on a certain event.
* Response handler - a client-side function which accepts form response from server and presents it to the user in one way or another.

## Protocol implementation in .NET (UiMetadataFramework.Core)

`UiMetadataFramework.Core` library defines core concepts underlying the protocol. The library is implemented in .NET, however it can just as easily be implemented in any other programming language. 

This library also includes a **binding component**, which is basically a collection of .NET attributes that can be used to decorate a *form* with metadata. This later allows `MetadataBinder` class to collect *form's* metadata using .NET reflection.

## UiMetadataFramework.Basic

This library builds on top of `UiMetadataFramework.Core` to provide metadata for describing a set of standard UI components.

The library includes metadata implementation for these components:

* Input fields
* * String
* * Date
* * Dropdown
* * Number
* * Password
* * Boolean
* * Paginator - a special input field to pass pagination parameters.
* * Typeahead - allows specifying the data source for the typeahead control.
* * MultiSelect - like `typeahead`, it allows developer to specify data source for the field.
* Output fields
* * String
* * DateTime
* * Number
* * FormLink - a link to any other form, which user can navigate.
* * ActionList - collection of `FormLink`
* * InlineForm - allows embedding one form inside another.
* * Tabstrip - metadata for rendering tabs, each pointing to a separate form.
* * PaginatedData - allows rendering data with pagination controls (must be used together with `Paginator` input field)
* * TextValue - allows rendering any non-string value as string.