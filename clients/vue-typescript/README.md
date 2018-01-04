# UI Metadata vue typescript client

This project is initial template client implemented by Vue using typescript language for [UI Metadata Framework](https://github.com/UNOPS/UiMetadataFramework).

### How to run the project
#### 1. Run [`UiMetadataFramework.Web`](https://github.com/UNOPS/UiMetadataFramework/tree/develop/UiMetadataFramework.Web) project

#### 2. Install dependencies

```
npm install
```

#### 3. update UiMetadataFramework.Web address which is considered UI Metadata Framework webserver.
##### I'm using the default localhost server path for `UI Metadata framework webserver` (default is `http://localhost:62790`)

```javascript
`App.ts`

var coreServerUrl = "http://mysite.com";
```

#### 4. serve with hot reload at localhost:9000

```
npm run dev
```