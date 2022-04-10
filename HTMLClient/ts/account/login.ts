import { login, getMessage } from "./accountapi.js";
let translations = {
    "Username or password missing.": "Username or password missing.",
    "Failed to fetch": "Failed to fetch",
    "Login attempt failed.": "Login attempt failed.",
    "Unaccepted Content.": "Unaccepted Content."
}
document.addEventListener("DOMContentLoaded", () => {

    let c = <HTMLElement> document.getElementById("translations");
    translations = JSON.parse(c.innerHTML);

    (document.getElementById("login") as HTMLElement).addEventListener("click", async () => {
        let data = {
            username: (document.getElementById('username') as HTMLInputElement).value,
            password: (document.getElementById('password') as HTMLInputElement).value
        };
        if (data.username === '' || data.password === '') {
            let e = document.getElementById('loginError');
            showError(e, translate(translations, 'Username or password missing.'));
            return;
        }
        let res = await login(data);
        if (res !== null) {
            stateservice = res;
            localStorage.setItem('stateservice', JSON.stringify(stateservice));
            window.location.href = '../';
            return;
        }
        let e = document.getElementById('loginError');
        let message = getMessage();
        if (message !== "")
            showError(e, translate(translations, message))
        else
            showError(e, translate(translations, "Login attempt failed."))
    });
});

