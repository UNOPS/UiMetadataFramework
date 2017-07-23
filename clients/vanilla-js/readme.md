## `/form` lifecycle

* Resolve
  * Create `FormInstance`
  * Initialize `FormInstance` input fields from URL parameters
* Create Svelte component (`Form`)
* Initialize Svelte component (`Form`)
  * If `postOnLoad == true` and all required inputs have values, post the form
  * Select response handler and handle the response

## Default response handler

Default response handler will

1. set `FormInstance` output fields
2. render output fields
