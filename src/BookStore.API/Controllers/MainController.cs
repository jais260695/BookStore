using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    //The others Controllers will inherit from this Controller.
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        //REST — Representational State Transfer
        //A RESTfull API is an application program interface (API) that uses HTTP requests to get or manipulate data.
        //REST, or REpresentational State Transfer, is an architectural style for providing standards between computer systems on the web,making it easier for systems to communicate with each other.
        //In the REST architecture, the client sends a request to get or modify resources, and the server sends a response to those requests.To do those requests, it is used the HTTP Verbs.

        //HTTP Verbs
        //HTTP defines a set of request methods to indicate the desired action to be performed for a given resource.
        //Although they can also be nouns, these request methods are sometimes referred to as HTTP verbs.
        //The basics verbs are:
        //GET — which request a representation of a specific resource. It is used only to return data.
        //POST — which is used to submit an entity to a specific resource.We generally use POST to create a new resource.
        //PUT — which is used to replace all the properties of a resource.We generally use PUT to update a resource.
        //PATCH — which is used to apply partial modifications.
        //DELETE — which is used to delete a resource.

        //Status Code
        //The status code is returned in the result of the API.When the client makes a request to an API, it will return a status code from the server after the request is made.The mains status code are:
        //200 — Ok: This status means that the request was successful.
        //201 — Created: This status means that the request has succeeded and a new resource has been created as a result.This is usually returned after a POST request.
        //204 — No Content: We can return this status when we do not want to return anything.
        //400 — BadRequest: This is a generic status for error.It means that the server could not understand the request due to invalid syntax.
        //401 — Unauthorized: This status means that the client is not authenticated, and he should authenticate to do the request.
        //403 — Forbidden: This status means that the client is authenticated but he does not have permission to do what he is trying to do. Unlike 401, the client’s identity is known to the server.
        //404 — Not found: This status means that the server could not find the requested resource.
        //500 — Internal Server Error: This is a generic answer from REST API, it means that the server has encountered a situation it doesn’t know how to handle.
        //503 — Service Unavailable: This status means that the server is not ready to handle the request.

    }
}
