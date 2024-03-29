﻿import { initDashboard,removeItem } from "./dashboard/dashboard.js"
import { initUserInformation } from "./account/userinformation.js"

document.addEventListener('DOMContentLoaded', async () => {
    await loadpage("homepage");
    toggleClassOn("homepage", 'active');
    toggleClassOff('divHomepage', 'w3-hide');
    if (localStorage.stateservice != undefined) {
        stateservice = JSON.parse(localStorage.stateservice)
    }
    if (stateservice != undefined) {
        if (stateservice.user != undefined) {
            toggleClassOff('dashboard', 'w3-hide');
            toggleClassOff('reservation', 'w3-hide');
            toggleClassOff('showUser', 'w3-hide');
            toggleClassOff('logout', 'w3-hide');
            toggleClassOn('login', 'w3-hide');
            (document.getElementById('fullname') as HTMLSpanElement).textContent =
                stateservice.user.firstName + ' ' + stateservice.user.lastName;
        }
        if (stateservice.user.claims !== undefined) {
            if (isAdmin(stateservice.user.claims)) {
                toggleClassOff('management', 'w3-hide');
            }
        }
    }
    (document.getElementById("showUser") as HTMLElement).addEventListener('click', async (e) => {
        let content = document.getElementById('divUserInformation');
        if (content == null) {
            content = await loadControl('account/userinformation');
            initUserInformation(e)
        }
    });
    (document.getElementById("logout") as HTMLElement).addEventListener("click", () => {
        localStorage.removeItem('stateservice');
        window.location.href = '/';
    });
    (document.getElementById("login") as HTMLElement).addEventListener("click", () => {
        localStorage.removeItem('stateservice');
        window.location.href = 'account/login.html';
    });
    (document.getElementById("homepage") as HTMLElement).addEventListener("click",
        async () => {
            await loadpage('homepage');

            toggleClassOn('homepage', 'active');
            toggleClassOff('divHomepage', 'w3-hide');

            toggleClassOff('dashboard', 'active');
            toggleClassOn('divDashboard', 'w3-hide');
        });
    (document.getElementById("dashboard") as HTMLElement).addEventListener("click",
        async () => {
            await loadpage('dashboard');

            toggleClassOff('homepage', 'active');
            toggleClassOn('divHomepage', 'w3-hide');

            toggleClassOn('dashboard', 'active');
            toggleClassOff('divDashboard', 'w3-hide');
        });
});
async function loadpage(page: string) {
    let content = document.getElementById('div' + capitalizeFirstLetter(page));
    if (content === null) {
        if (page === 'homepage') {
            content = await loadControl('homepage');
        }
        if (page === 'dashboard') {
            content = await loadControl('dashboard/dashboard');
            initDashboard();
        }
    }
}
