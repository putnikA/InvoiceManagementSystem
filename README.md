# InvoiceManagementSystem
Web app for management of Invoices for a store owner


This app is for store owners and can be used to CREATE/READ/UPDATE/DELETE your suppliers and invoices you get from them when you order goods.
When creating Invoices you can choose a supplier from dropdown and fill up other input fields relevant to that invoice.
There is a page which shows all invoices/suppliers. Also there is an option to mark invoice as paid, invoice will then be transfered to
Paid page where all paid invoices are. There is an option to filter invoices by payment deadline or supplier name. 

Implementation missing is ability to filter paid invoices by payment deadline/date paid. 
For the latter it would require for app to track current date and to also display it in Paid view.



After cloning repository, open then 'appsettings.json' and edit:
"InvoiceModelContext": "Server=(localdb)\\mssqllocaldb;Database=InvoiceModelContext-40c8ad7b-310d-4900-84be-8f4d66933105;Trusted_Connection=True;MultipleActiveResultSets=true"
to match your local database. 
Next, open Tools/NuGet Package Manager/Package Manager Console and create migration Add-Migration <MigrationName> and Update-Database.
Finally, start the app!
