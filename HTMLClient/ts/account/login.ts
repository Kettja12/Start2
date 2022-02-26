let loginTranslations = {
    "Username or password missing.": "Username or password missing.",
    "Failed to fetch": "Failed to fetch",
    "Login attempt failed.": "Login attempt failed.",
    "Unaccepted Content.": "Unaccepted Content."
}
document.addEventListener("DOMContentLoaded", () => {

    let c = document.getElementById("loginTranslations");
    loginTranslations = JSON.parse(c.innerHTML);
    document.getElementById("login").addEventListener("click", async () => {
        let data = {
            username: (document.getElementById('username') as HTMLInputElement).value,
            password: (document.getElementById('password') as HTMLInputElement).value
        };
        if (data.username === '' || data.password === '') {
            let e = document.getElementById('loginError');
            showError(e, translate(loginTranslations, 'Username or password missing.'));
            return;
        }
        let response = await apiPost("Account/Login", data);
        if (response.status === "OK") {
            if (response.data.user !== null) {
                stateservice = response.data;
                localStorage.setItem('stateservice', JSON.stringify(stateservice));
                window.location.href = '../';
                return;
            }
        }
        let e = document.getElementById('loginError');
        showError(e, translate(loginTranslations, response.message));
    });
});

