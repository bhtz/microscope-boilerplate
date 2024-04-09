# Documentation as code

> Documentation as code web application

* ✅ ASP NET static files
* 🚫 OIDC / Cookie authentication
* ✅ vitepress SSG
* ✅ Mermaid support
* ✅ Documentation templates (#product, #ADR, #PRD, #guidelines, ...)
* ✅ Opiniated guidelines (architecture, product, ...)

### Create new documentation as code project
```console
dotnet new mcsp_doc -n Acme.Doc
```

### Template options
* -G, --Guidelines    
    * Whether to include microscope opiniated guidelines or not.
    * Type : bool
    * Default : false

* -P, --Product
    * Whether to include product documentation or not.
    * Type : bool
    * Default : false

* -T, --Tech
    * Whether to include tech documentation or not.
    * Type : bool
    * Default : false

* -B, --Blog
    * Whether to include blog or not.
    * Type : bool
    * Default : false

* -O, --Organization  
    * Whether to include organization & governance doc or not.
    * Type : bool
    * Default : false