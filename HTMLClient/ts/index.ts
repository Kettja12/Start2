let indexTranslations = {
    'Password and password verification are not same.': 'Password and password verification are not same.',
    'User save success.': 'User save success.'
}

function initIndex() {
    document.addEventListener("DOMContentLoaded", async () => {
        if (localStorage.stateservice != undefined) {
            stateservice = JSON.parse(localStorage.stateservice)
        }
        document.getElementById(activepage).classList.toggle("active");
        if (stateservice != undefined) {
            if (stateservice.user != undefined) {
                document.getElementById("dashboard").classList.toggle("w3-hide");
                document.getElementById("reservation").classList.toggle("w3-hide");
                document.getElementById("logout").classList.toggle("w3-hide");
                document.getElementById("login").classList.toggle("w3-hide");
                (document.getElementById("fullname") as HTMLSpanElement).textContent =
                    stateservice.user.firstName + ' ' + stateservice.user.lastName;

            }
            if (stateservice.claims !== undefined) {
                if (isAdmin(stateservice.claims)) {
                    document.getElementById("management").classList.toggle("w3-hide");
                }
            }
        }
        else {
            if (activepage != "homepage") {
                activepage = "homepage"
                document.getElementById("homepage").classList.toggle("active")
            }

        }
        document.getElementById("showUser").addEventListener('click', async (e) => {
            let content = document.getElementById('divUserInformation');
            if (content == null) {
                content = await loadControl('account/userinformation');
                loadScript('../scripts/account/userinformation.js', 'initUserInformation', e);
            }
            else {
                initUserInformation(e)
            }
        });
        document.getElementById("logout").addEventListener("click", () => {
            localStorage.removeItem('stateservice');
            window.location.href = '/';
        });
        document.getElementById("login").addEventListener("click", () => {
            localStorage.removeItem('stateservice');
            window.location.href = 'account/login.html';
        });
        document.getElementById("homepage").addEventListener("click",
            async () => {
                await loadpage('homepage');
                let content = document.getElementById("homepage");
                if (content.classList.contains("active") == false) {
                    content.classList.toggle("active");
                }
                content = document.getElementById("dashboard");
                if (content.classList.contains("active") == true) {
                    content.classList.toggle("active");
                }
            });
        document.getElementById("dashboard").addEventListener("click",
            async () => {
                await loadpage('dashboard');
                let content = document.getElementById("homepage");
                if (content.classList.contains("active") === true) {
                    content.classList.toggle("active");
                }
                content = document.getElementById("dashboard");
                if (content.classList.contains("active") == false) {
                    content.classList.toggle("active");
                }
            });
        await loadpage(activepage);
    });

    async function loadpage(control: string) {
        activepage = control
        if (activepage === 'homepage') {
            let content = document.getElementById('divhomepage');
            if (content == null) {
                content = await loadControl('homepage');
            }
            if (content.classList.contains("w3-hide")) {
                content.classList.toggle("w3-hide");
            }
            content = document.getElementById("divDashboard");
            if (content !== null) {
                if (content.classList.contains("w3-hide") === false) {
                    content.classList.toggle("w3-hide");
                }
            }
        }
        if (activepage === 'dashboard') {
            let content = document.getElementById('divDashboard');
            if (content == null) {
                content = await loadControl('dashboard/dashboard');
                loadScript('../scripts/dashboard/dashboard.js', 'initDashboard', undefined);

            }
            if (content.classList.contains("w3-hide")) {
                content.classList.toggle("w3-hide");
            }
            content = document.getElementById("divhomepage");
            if (content.classList.contains("w3-hide") === false) {
                content.classList.toggle("w3-hide");
            }
        }


    }



}
