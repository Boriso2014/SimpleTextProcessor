# Simple Test Processor
Simple Test Processor is a web application that allows to perform CRUD operations on simple text files. The web application has a simple text editor that allows users:
- To type text and save it as a text file on the server-side
- To upload text files via a local computer, including large files
- To fetch back a specific file from the saved files, and update its content via the web text editor
- To delete saved files

Large files upload and download chunk by chunk.

Thit is a demo application. It does not contain any authentication/authoriuzation stuff. Text files keep on the server side, but the better solution is to save them somewhere outside of the server (e.g. Amazon S3, Azure Blob storage). It can be done by appropriate implementation IFileProcessWrapper abstraction.

Thech stack: .NET 6, C#, Angular 13.
