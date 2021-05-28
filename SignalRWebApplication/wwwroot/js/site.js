// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44368/api/weatherstations/hubs/subscriberHub").build();

connection.on("newWeatherUpdate", function (weather) {
    console.log(weather);
});

