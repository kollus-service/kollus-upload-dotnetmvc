// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function makeFormBodyParameters(parameters) {
    let formBody = [];
    for (let property in parameters) {
        let encodedKey = encodeURIComponent(property);
        let encodedValue = encodeURIComponent(parameters[property]);
        formBody.push(encodedKey + "=" + encodedValue);
    }
    formBody = formBody.join("&");
    return formBody;
}