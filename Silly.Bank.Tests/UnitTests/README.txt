Here you will code your Unit Tests. Unit doesn't always mean only one class or method. It could be in some cases, but it is not always the case.

With the time and experience, I have been convinced to test here implementation and complex business logic rules.

Implementation would be all those tests which you cannot talk about with a business person. You only can talk about them with tech people. 
For instance, the app settings are load correctly, the IoC container builds the three properly, the logs has the proper string patterns, 
the SSL certificate is registered, etc

Complex business logic rules would be a class (or set of classes) that contains complex calculations with endless paths and output. 
For example: rate and interest calculations, business days in a calendar, etc. It would be a lot of work to test those examples in a BDD style. 
In my humble opinion, the best approach here is to combine the two styles together. You could have few BDD scenarios with the happy paths, 
and a bunch of small Unit Test for the rest outputs and paths.
