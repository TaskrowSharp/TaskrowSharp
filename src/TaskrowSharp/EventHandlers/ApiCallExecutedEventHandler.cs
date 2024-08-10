using System.Net.Http;
using System;
using System.Net;

namespace TaskrowSharp.EventHandlers;

public delegate void ApiCallExecutedEventHandler(
    HttpMethod httpMethod,
    Uri fullUrl,
    HttpStatusCode httpStatusCode,
    bool isSuccess,
    string? jsonRequest,
    string? jsonResponse, 
    long elapsedMilliseconds);
