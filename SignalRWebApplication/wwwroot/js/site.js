// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44368/SubcriberHub").build();

connection.start().then(function() {
    console.log("Connect");
}).catch(function(err) {
    console.log("error "+ err);
});

connection.on("WeatherUpdate",
    function(weather) {
        console.log("ny vejr melding");
        var vejrString = JSON.stringify(weather);
        console.log(vejrString);
        //alert("ny vejrmelding");
    });

