let language = navigator.language;
language = language.substring(0, 2)
localStorage.setItem('language', language);
let stateservice: stateserviceType = undefined;
if (localStorage.stateservice != undefined) {
    stateservice=JSON.parse(localStorage.stateservice)
}
let activepage = 'homepage';
let translation = {};