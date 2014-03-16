From CodeProject

http://www.codeproject.com/Articles/642444/Creating-and-consuming-a-simple-WCF-Service-withou

With thanks

Presented here as a complete solution for VS2013  and .Net 4.5.1


To start both client and host from Visual Studio

    Create a Visual Studio solution that contains both the client and server projects.

    Configure the solution to start both client and server processes when you choose Start on the Debug menu.

        In Solution Explorer, right-click the solution name.

        Click Set Startup Projects.

        In the Solution <name> Properties dialog box, select Multiple Startup Projects.

        In the Multiple Startup Projects grid, on the line that corresponds to the server project, click Action and choose Start.

        On the line that corresponds to the client project, click Action and choose Start.

        Click OK.


