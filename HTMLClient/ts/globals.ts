let language = navigator.language;
"use strict";
let stateservice: stateserviceType = undefined;
if (localStorage.stateservice != undefined) {
    stateservice=JSON.parse(localStorage.stateservice)
}
//let apiserver = "https://localhost:7265/";
let apiserver = "https://jarikettunen.ddns.net/api/";

let activepage = 'homepage';