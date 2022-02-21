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
        await loadpage(activepage);

    });
    document.getElementById("logout").addEventListener("click", () => {
        localStorage.removeItem('stateservice');
        window.location.href = '/';
    });
    document.getElementById("login").addEventListener("click", () => {
        localStorage.removeItem('stateservice');
        window.location.href = 'account/login.html';
    });
    document.getElementById("homepage").addEventListener("click", async () => {
        let content = document.getElementById("homepage");
        if (content.classList.contains("active") == false) {
            content.classList.toggle("active");
        }
        content = document.getElementById("dashboard");
        if (content.classList.contains("active") == true) {
            content.classList.toggle("active");
        }
        await loadpage('homepage');
    });
    document.getElementById("dashboard").addEventListener("click", async () => {
        let content = document.getElementById("homepage");
        if (content.classList.contains("active") === true) {
            content.classList.toggle("active");
        }
        content = document.getElementById("dashboard");
        if (content.classList.contains("active") == false) {
            content.classList.toggle("active");
        }
        await loadpage('dashboard');
    });

    async function loadpage(control: string) {
        activepage = control
        if (activepage === 'homepage') {
            let content = document.getElementById("divhomepage");
            if (content.innerHTML === "") {
                content.innerHTML = await fetchHtmlAsText("homepage.html");
                content.classList.toggle("w3-hide");
            }
            if (content.classList.contains("w3-hide")) {
                content.classList.toggle("w3-hide");
            }
            content = document.getElementById("divdashboard");
            if (content.classList.contains("w3-hide") === false) {
                content.classList.toggle("w3-hide");
            }
        }
        if (activepage === 'dashboard') {
            let content = document.getElementById("divdashboard");
            if (content.innerHTML === "") {
                content.innerHTML = await fetchHtmlAsText("dashboard/dashboard.html");
                content.classList.toggle("w3-hide");
            }
            if (content.classList.contains("w3-hide")) {
                content.classList.toggle("w3-hide");
            }
            content = document.getElementById("divhomepage");
            if (content.classList.contains("w3-hide") === false) {
                content.classList.toggle("w3-hide");
            }
        }
        document.getElementById("showUser").addEventListener("click", async (e) => {
            let content = document.getElementById("userinformation");
            if (content.innerHTML === "") {
                content.innerHTML = await fetchHtmlAsText("account/userinformation.html");
                document.getElementById("saveuser").addEventListener("click", async () => saveuserInformation());
            }
            let xpos = e.pageX - 300;
            (document.getElementById('username') as HTMLInputElement).value = stateservice.user.username;
            (document.getElementById('lastname') as HTMLInputElement).value = stateservice.user.lastName;
            (document.getElementById('firstname') as HTMLInputElement).value = stateservice.user.firstName;
            (document.getElementById('isadmin') as HTMLInputElement).checked = isAdmin(stateservice.claims);
            content.style.position = 'absolute';
            content.style.left = xpos + "px";
            content.classList.toggle("w3-hide");
        });
    }

    async function saveuserInformation() {
        let oldpassword = (document.getElementById('oldpassword') as HTMLInputElement).value
        let newpassword = (document.getElementById('newpassword') as HTMLInputElement).value
        let passwordverification = (document.getElementById('passwordverification') as HTMLInputElement).value

        if (newpassword != passwordverification) {
            return "Password and password verification are not same.";
        }
        stateservice.user.firstName = (document.getElementById('firstname') as HTMLInputElement).value
        stateservice.user.lastName = (document.getElementById('lastname') as HTMLInputElement).value

        let response = await apiPost("account/saveuser", stateservice.user);
        let e = document.getElementById('infomessage');
        localStorage.setItem('stateservice', JSON.stringify(stateservice));
        if (response.status === "OK") {
            let m = translate(indexTranslations, "User save success.");
            if (newpassword !== '') {
                let data: SavePasswordModel = {
                    newPassword: newpassword,
                    oldPassword: oldpassword,
                    username: stateservice.user.username
                }
                response = await apiPost("account/savePassword", data);
                if (response.status === "OK") {
                    m = m + ' ' + translate(indexTranslations, response.data);
                }
                else {
                    m = m + ' ' + translate(indexTranslations, response.message);

                }
     
            }
            if (response.status === "OK") {
                let content = document.getElementById("userinformation");
                content.classList.toggle("w3-hide");
            }
            showError(e, m);

        }
        else {
            showError(e, translate(indexTranslations, response.message));
        }

    }


}
